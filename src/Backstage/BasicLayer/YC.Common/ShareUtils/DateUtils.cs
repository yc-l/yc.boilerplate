using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.Common.ShareUtils
{
    public class DateUtils
    {
        public static TimeSpan DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            TimeSpan ts = DateTime1.Subtract(DateTime2).Duration();
            return ts;
           
        }

        #region 操作说明

        //说明：
        //1.DateTime值类型代表了一个从公元0001年1月1日0点0分0秒到公元9999年12月31日23点59分59秒之间的具体日期时刻。因此，你可以用DateTime值类型来描述任何在想象范围之内的时间。一个DateTime值代表了一个具体的时刻
        //2.TimeSpan值包含了许多属性与方法，用于访问或处理一个TimeSpan值
        //下面的列表涵盖了其中的一部分：
        //Add：与另一个TimeSpan值相加。
        //Days:返回用天数计算的TimeSpan值。
        //Duration:获取TimeSpan的绝对值。
        //Hours:返回用小时计算的TimeSpan值
        //Milliseconds:返回用毫秒计算的TimeSpan值。
        //Minutes:返回用分钟计算的TimeSpan值。
        //Negate:返回当前实例的相反数。
        //Seconds:返回用秒计算的TimeSpan值。
        //Subtract:从中减去另一个TimeSpan值。
        //Ticks:返回TimeSpan值的tick数。
        //TotalDays:返回TimeSpan值表示的天数。
        //TotalHours:返回TimeSpan值表示的小时数。
        //TotalMilliseconds:返回TimeSpan值表示的毫秒数。
        //TotalMinutes:返回TimeSpan值表示的分钟数。
        //TotalSeconds:返回TimeSpan值表示的秒数。
        #endregion
    }
}
