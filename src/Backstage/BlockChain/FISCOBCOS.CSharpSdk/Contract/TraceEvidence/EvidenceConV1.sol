pragma solidity ^0.4.24;
pragma experimental ABIEncoderV2;
import "./Table.sol";

//存证合约
contract EvidenceConV1 {
      // event
      event RegisterEvent(string serviceId,string sn,string rid,string ts,string remarkJsonValue,address account,uint256 timeStamp);
      event SetPermitUserEvent(uint256 state,address msgAddress,address userAddress,bool setPermitState,uint256 timeStamp, string remark);
      event InsertDbErrorEvent(string serviceId,string sn,string rid,string ts,string remarkJsonValue,address account,uint256 timeStamp);

        //设定管理者
    address private _manager;
    

    
    //授权许可用户集合
  mapping(address=>bool) public permitUserMapping;

   //Modifier 定义一个修饰器，所有继承该修饰器的方法进行管理者验证
    modifier onlyManager(){
        require(msg.sender == _manager, "你不是管理者，无法进行对应操作！");      
        _;
    }
    constructor() public {
        // 构造函数中创建t_LawLetter表
        createTable();
       _manager = msg.sender;
    }
    
    
    //修改授权，address userAddress 用户地址 .bool permitState 授权状态 true 开启，false关闭
    function changePermitUser(address userAddress,bool permitState) public onlyManager returns(uint256){
          uint256 state=0;
         string memory stateMessage="";
         permitUserMapping[userAddress]=permitState;
         if(permitUserMapping[userAddress]==permitState){
                state=1;
               stateMessage="授权状态修改成功!";
            }else{
               stateMessage="授权状态修改失败!";
         }
          emit  SetPermitUserEvent(state,msg.sender,userAddress,permitState,now,stateMessage);
          return state;
    }
    

    function createTable() private {
        TableFactory tf = TableFactory(0x1001); 
        // 存证数据管理表, key : serviceId(业务Id),
        //field : typeName（业务类别）， dataValue(json业务上链数据)
        // 创建表
        tf.createTable("tb_TraceEvidence_pro", "serviceId","typeName,dataValue");
    }

    function openTable() private returns(Table) {
        TableFactory tf = TableFactory(0x1001);
        Table table = tf.openTable("tb_TraceEvidence_pro");
        return table;
    }

    /*
    描述 : 根据业务Id查询业务上链数据
    参数 ： 
             

    返回值：
           //field : typeName（业务类别）， dataValue(json业务上链数据)
            
    */
   
    function getServiceList(string serviceId) public view returns(int256,string[] memory, string[] memory) {
        // 打开表
        Table table = openTable();
         Condition condition= table.newCondition();
         condition.EQ("serviceId",serviceId);
        // 查询
        Entries entries = table.select(serviceId, condition);
         string[] memory data_list = new string[](
            uint256(entries.size())
        );
        
        string[] memory typeName_list = new string[](
            uint256(entries.size())
        );
        
        if (0 == uint256(entries.size())) {
           return (0, typeName_list,data_list);
        } else {
            
        for (int256 i = 0; i < entries.size(); ++i) {
            Entry entry = entries.get(i);
            data_list[uint256(i)] = entry.getString("dataValue");
           typeName_list[uint256(i)] = entry.getString("typeName");
           }
            return (entries.size(),typeName_list, data_list);
           
        }
    }




    /*
    描述 : 数据上链
    参数 ： 
        	  //field : typeName（业务类别）， dataValue(json业务上链数据)
    返回值：
            1  数据上链成功
            0  数据上链失败
            
    */
    function registerServiceData(string serviceId, string typeName,string dataValue) public returns(int256,string){
        //require(permitUserMapping[msg.sender]==true,"未授权用户，禁止存证上链操作！");
        int256 ret_code = 0;
         // 打开表
         Table table = openTable();
         Condition condition= table.newCondition();
         condition.EQ("dataValue",dataValue);
        // 查询
        Entries entries = table.select(serviceId, condition);
        if (0 != uint256(entries.size())) {
            return (0,"存证数据已存在，请确认后再上传！");
        } else{
             Entry entry = table.newEntry();
		     entry.set("serviceId", serviceId);
             entry.set("typeName", typeName);
             entry.set("dataValue",dataValue);
            // 插入
            int count = table.insert(serviceId, entry);
            if (count == 1) {
                  // 成功
                  ret_code = 1;
              return (ret_code,"存证数据上链，插入数据成功！");
            } else {
                   // 失败? 无权限或者其他错误
                 ret_code =0;
                 return (ret_code,"存证数据上链，插入数据失败！");
            }
            
        }
         
       
    }


}
