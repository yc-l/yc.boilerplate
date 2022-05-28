using Microsoft.VisualStudio.TestTools.UnitTesting;
using YC.Common;
using YC.CodeGenerate;
using YC.CodeGenerate.Dto;
using YC.Common.ShareUtils;
using System.Collections.Generic;

namespace YC.CodeGenerateTest
{
    [TestClass]
    public class CodeGenerateUnitTest
    {
        [TestMethod]
        public void ChangeJsonTest()
        {
            #region 第一种直接转换写入，格式比较乱
            //string dbConfigFilePath = System.Environment.CurrentDirectory + "\\DefaultConfig.json";
            //string tempJson = CodeGenerateConfig.GetConfigJson(dbConfigFilePath);
            //DatabaseConfig DatabaseConfig = CodeGenerateConfig.GetJsonList<DatabaseConfig>(tempJson);
            //DatabaseConfig.DefaultDBConnectionString = DatabaseConfig.DefaultDBConnectionString.Replace("{%dbName%}", "test");
            //bool result = CodeGenerateConfig.SetConfigJson(dbConfigFilePath, DatabaseConfig.ToJson()); 
            #endregion

            #region 第二种，格式不会变
            //string dbConfigFilePath = System.Environment.CurrentDirectory + "\\DefaultConfig.json";
            //string tempJson = CodeGenerateConfig.GetConfigJson(dbConfigFilePath).Replace("{%dbName%}", "test");
            //bool result = CodeGenerateConfig.SetConfigJson(dbConfigFilePath, tempJson);
            #endregion
        }

        /// <summary>
        /// 根据默认配置自动创建数据库
        /// </summary>
        [TestMethod]
        public void CreateDBTest()
        {

            using (CodeGenerateDBService codeGenerateDBRepository = new CodeGenerateDBService())
            {
                string msg = "";
                bool result = codeGenerateDBRepository.CreateDB(out msg);
                Assert.IsTrue(result);
            }

        }

        [TestMethod]
        public void CreateTableTest()
        {
            using (CodeGenerateDBService codeGenerateDBRepository = new CodeGenerateDBService())
            {
                string msg = "";
                GenerateDbTableConfig config = new GenerateDbTableConfig();
                config.GenerateDbTableEntityList = new List<string>();
                config.GenerateDbTableEntityList.Add("BCEvidence");//要生成的表
                //config.GenerateDbTableEntityList.Add("SysAuditLog");
                //config.GenerateDbTableEntityList.Add("SysUserSysOrganization");
                bool result = codeGenerateDBRepository.CreateTable(config,out msg, false, "YC.Model");
                Assert.IsTrue(result);
            }
        }  

        /// <summary>
        /// 代码生成器-生成对应代码
        /// </summary>
        [TestMethod]
        public void GenerateCodeTest()
        {
            #region 模板和生成代码路径配置
            TemplateDto templateDto = new TemplateDto();
            templateDto.AddOrEditDtoFilePath = System.AppContext.BaseDirectory + @"CodeTemplate\AddOrEditDtoTemplate.txt";
            templateDto.EntityDtoFilePath = System.AppContext.BaseDirectory + @"CodeTemplate\EntityDtoTemplate.txt";
            templateDto.ServiceFilePath = System.AppContext.BaseDirectory + @"CodeTemplate\AppServiceTemplate.txt";
            templateDto.IServiceFilePath = System.AppContext.BaseDirectory + @"CodeTemplate\IAppServiceTemplate.txt";
            templateDto.VueFilePath = System.AppContext.BaseDirectory + @"CodeTemplate\View\VueTemplate.txt";
            templateDto.TreeServiceFilePath = System.AppContext.BaseDirectory + @"CodeTemplate\TreeAppServiceTemplate.txt";
            templateDto.TreeIServiceFilePath = System.AppContext.BaseDirectory + @"CodeTemplate\TreeIAppServiceTemplate.txt";
            templateDto.TreeVueFilePath = System.AppContext.BaseDirectory + @"CodeTemplate\View\TreeVueTemplate.txt";
            templateDto.SaveDir = System.AppContext.BaseDirectory + "GenerateCode\\";//生成代码保存位置
            #endregion
            #region 配置默认生成规格
            List<GenerateCodeEnumType> wantToGenerateCodeTypeList = new List<GenerateCodeEnumType>();
            wantToGenerateCodeTypeList.Add(GenerateCodeEnumType.DefaultAddOrEditDto);//新增编辑Dto
            wantToGenerateCodeTypeList.Add(GenerateCodeEnumType.DefaultEntityDto);//实体Dto

            bool isTree = false;
            wantToGenerateCodeTypeList.Add(GenerateCodeEnumType.OtherCode);
            if (isTree)
            {
                wantToGenerateCodeTypeList.Add(GenerateCodeEnumType.TreeAppService);
                wantToGenerateCodeTypeList.Add(GenerateCodeEnumType.TreeIAppService);
                wantToGenerateCodeTypeList.Add(GenerateCodeEnumType.TreeVuePage);
            }
            else
            {
                wantToGenerateCodeTypeList.Add(GenerateCodeEnumType.DefaultAppService);
                wantToGenerateCodeTypeList.Add(GenerateCodeEnumType.DefaultIAppService);
                wantToGenerateCodeTypeList.Add(GenerateCodeEnumType.DefaultVuePage);
            }
            #endregion

            ///树形代码完成后，注意修改vue界面
            ///1、手动修改分类 typeName那边
            //2、手动修改编辑 复选框内容父节点
            //3、这里的节点 默认使用Label，父节点ParentId 两个固定的字段名称
            //4、树的根节点要为0

            List<string> generateEntityList = new List<string>();
            generateEntityList.Add("BCEvidence");
            GenerateCodeConfig generateCodeConfig = new GenerateCodeConfig();
            generateCodeConfig.Template = templateDto;
            generateCodeConfig.WantToGenerateCodeTypeList = wantToGenerateCodeTypeList;
            generateCodeConfig.GenerateEntityList = generateEntityList;
            generateCodeConfig.IsTree = isTree;
            generateCodeConfig.AssemblyName = "YC.Model";
            GenerateCodeService generateCodeRepository = new GenerateCodeService(generateCodeConfig);
            var result = generateCodeRepository.GenerateWork();
            Assert.IsTrue(result.Success);
        }
        /// <summary>
        /// Mysql代码生成器-生成对应表模型
        /// </summary>
        [TestMethod]
        public void GenerateEntityCodeTest()
        {
            #region 模板和生成代码路径配置
            string _entityFilePath = System.AppContext.BaseDirectory + @"CodeTemplate\EntityTemplate.txt";
            string _saveDir = System.AppContext.BaseDirectory + "GenerateCode\\";//生成代码保存位置
            #endregion
            GenerateCodeService generateCodeRepository = new GenerateCodeService();
            HashSet<string> _tables = new HashSet<string>();
            //若果是全表生成则不需要表名
            //_tables = null;
            //否则需要
            _tables.Add("sys_user");
            var result = generateCodeRepository.GenerateEntityCode(_tables, _entityFilePath, _saveDir);
            Assert.IsTrue(result.Success);
        }
    }
}
