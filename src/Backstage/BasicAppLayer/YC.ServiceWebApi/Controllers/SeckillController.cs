using CSRedis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using YC.Common.ShareUtils;
using YC.Core;

namespace YC.ServiceWebApi.Controllers
{
    public class SeckillController : BaseController
    {
        private CSRedisClient _csredis;
        public SeckillController(CSRedisClient csredis)
        {
            this._csredis = csredis;
            //_csredis.Set("ct", "aaa");
            //RedisHelper.Set("test", "abc");
            //var temp = RedisHelper.Get("test");

           

        }


        /// <summary>
        /// 模拟只调用一次
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IApiResult InitDbData()
        {
            string pt1 = "pt1";//商品系列1
            string pt2 = "pt2";//商品系列2

            #region 1.创建库存,定义两种商品，都有库存

            for (int i = 1; i < 10001; i++) {
                RedisHelper.HSet("Stock_" + pt1, i.ToString(), new Stock() { Id = i, ProductName = "s"+i, Status = 0 });
            }
            //RedisHelper.HSet("Stock_" + pt2, "1", new Stock() { Id = 1, ProductName = "s2", Status = 0 });
            //RedisHelper.HSet("Stock_" + pt2, "2", new Stock() { Id = 2, ProductName = "s2", Status = 0 });
            //RedisHelper.HSet("Stock_" + pt2, "3", new Stock() { Id = 3, ProductName = "s2", Status = 0 });
            #endregion
            string message = "创建模拟数据成功！";
            return ApiResult.Ok(message);
        }

        /// <summary>
        /// 秒杀活动 550 线程并发，单体服务可以处理；
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IApiResult SeckillWork(int userId,string productType)
        {
            string pt1 = "pt1";//商品系列1
            string pt2 = "pt2";//商品系列2

            int personId1 = 1;
            int personId2 = 2;

            string message = "";
            var lk = RedisHelper.Lock("CreateOrder", 10);//使用锁，内部使用guid 作为全局唯一
           
            #region 库存查询+下单购买

            string[] stockArray = RedisHelper.HVals("Stock_" + productType);
            var stockList = new List<Stock>();
            for (int i = 0; i < stockArray.Length; i++)
            {
                stockList.Add(stockArray[i].ToObject<Stock>());
            }
            if (stockArray == null)
            {
                message = $"用户：{userId}，抢购商品：{productType}，库存为空，抢购结束！";
                Console.WriteLine(message);
                lk.Unlock();
                return ApiResult.Ok("", message);
            }
            else {
                #region 2-下单
                //库存中可进行销售的是否还是有
                var stock = stockList.Where(x => x.Status == 0).FirstOrDefault();
                if (stock != null)//可以下单
                {
                    var order= RedisHelper.HGet("HSetOrder_" + productType, userId.ToString())? .ToObject<Order>();

                    if (order != null) {
                        message = $"用户: { userId},您已经成功抢购商品类型：{ productType},商品id: { order.Id},不能重复购买！";
                        Console.WriteLine(message);
                        lk.Unlock();
                        return ApiResult.Ok("", message);
                    }

                    //var d1 = RedisHelper.LPop("BuyList_"+productType);//队列最前排，出来
                    //RedisHelper.SRem("Set_buy_" + productType, userId);//指定商品抢购集合中的人也移除

                    //订单添加
                    RedisHelper.HSet("HSetOrder_" + productType, userId.ToString(), new Order() { Id = Guid.NewGuid().ToString(), ProductId = stock.Id, Date = DateTime.Now, Status = 0, BuyPersonId = userId });
                    stock.Status = 1;
                    //库存锁定
                    RedisHelper.HSet("Stock_" + productType, stock.Id.ToString(), stock);
                    message = $"用户: { userId},抢购商品类型：{ productType},商品id: { stock.Id}成功，请尽快付款！";                
                }
                else
                {
                    message = "已售罄 秒杀结束！";
                }
                Console.WriteLine(message);
                lk.Unlock();
                return ApiResult.Ok("",message);
               
                #endregion
            }
            #endregion

            #region 抢购 队列模式，还要使用配合线程处理业务，配合websocket 通知前端，较为麻烦
            /* //无序集合判别，是否存在该用户
             bool isExist = RedisHelper.SIsMember("Set_buy_" + productType, userId);
             if (isExist)
             {
                 Console.WriteLine($"用户：{userId}，抢购商品：{productType}，排队中......");
                 return ApiResult.Ok($"用户：{userId}，抢购商品：{productType}，排队中......");

             }
             else
             {

                 //查询指定的商品集合是否存在，用户id 作为key，订单作为value，每种商品每个人只能购买一单，作废需要剔除
                 //这里如果是放到关系型数据库，那么就可以支持作废订单，因为key 是订单号，购买者条件查询
                 var order = RedisHelper.HGet("HSetOrder_" + productType, userId.ToString())?.ToObject<Order>();
                 if (order != null)
                 {
                     //未付款或已支付状态
                     if (order.Status == 0)
                     {
                         message = $"用户：{userId}，您已经对商品类型：{productType}，商品编号：{order.ProductId}下单，请尽快付款！";
                         Console.WriteLine(message);

                     }
                     if (order.Status == 1)
                     {
                         message = $"用户：{userId}，您已经购买过商品，类型：{productType}，商品编号：{order.ProductId}，不可以再次购买！";
                         Console.WriteLine(message);

                     }
                     return ApiResult.Ok(message);
                 }
                 else
                 { //订单也为null，说明还没抢购，需要进入抢购队列
                     RedisHelper.RPush("BuyList_"+ productType, userId.ToString());//进入抢购队列

                     message = $"用户：{userId}，您已经进入商品类型：{productType}的抢购队列，请耐心等待！";
                     Console.WriteLine(message);
                     return ApiResult.Ok(message);
                 }

             }*/
            #endregion
            #region 之后要配合服务，订单超时未付款，库存状态恢复 

            #endregion

          
        }
    }

    
    /// <summary>
    /// 商品库存model测试阶段将product 混合一起，正常需要拆开
    /// </summary>

    public partial class Stock
    {
        public int Id { get; set; } // 秒杀编号
        public int Count { get; set; } // 秒杀商品数量 10 ,在本次模拟中本字段不使用，因为每个商品都是nft有唯一编号
        public string ProductName { get; set; } // 秒杀商品名称 手机

        /// <summary>
        /// 0 未出售，1 锁定，2 成交
        /// </summary>
        public int Status { get; set; }
    }

    public class Order
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public int ProductId { get; set; }
        public int BuyPersonId { get; set; }
        /// <summary>
        /// 0 未付款，1 已付款，2 付款超时结单
        /// </summary>
        public int Status { get; set; }
    }
}

