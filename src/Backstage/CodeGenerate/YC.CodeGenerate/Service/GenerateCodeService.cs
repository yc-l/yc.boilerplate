using Dapper;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
using YC.CodeGenerate.Dto;
using YC.Common.ShareUtils;

namespace YC.CodeGenerate
{
    public class GenerateCodeService
    {
        public TemplateDto _templateDto;
        public List<Type> _domainList = new List<Type>();//获取得到的程序集实体
        public List<GenerateCodeEnumType> _wantToGenerateCodeTypeList = null;//想要生成的代码类型
        public List<string> _generateEntityList = null;//想要生成的代码的实体列表
        public GenerateCodeConfig _generateCodeConfig;
        public GenerateCodeService()
        {
        }
        public GenerateCodeService(GenerateCodeConfig generateCodeConfig)
        {
            _templateDto = generateCodeConfig.Template;
            _wantToGenerateCodeTypeList = generateCodeConfig.WantToGenerateCodeTypeList;
            _generateEntityList = generateCodeConfig.GenerateEntityList;
            _generateCodeConfig = generateCodeConfig;
            FileUtils.CreateDirectory(_templateDto.SaveDir);
            List<Assembly> assemblyList = new List<Assembly>();
            assemblyList.Add(AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(generateCodeConfig.AssemblyName)));
            //1、加载程序集
            foreach (var assembly in assemblyList)
            {
                var list = assembly.GetExportedTypes();
                foreach (var l in list)
                {
                    if (l.IsDefined(typeof(System.ComponentModel.DataAnnotations.Schema.TableAttribute), false))
                    {
                        if (_generateEntityList.Count > 0)
                        {//如果指定生成实体存在，那么只生成对应的实体业务代码 

                            //string tableName = ((System.ComponentModel.DataAnnotations.Schema.TableAttribute)Attribute.GetCustomAttribute(l, typeof(System.ComponentModel.DataAnnotations.Schema.TableAttribute))).Name;
                            if (_generateEntityList.Exists(x => x == l.Name))
                                _domainList.Add(l);
                        }
                        else
                        {//默认生成全部有Table 特性注解的实体业务代码
                            _domainList.Add(l);
                        }

                    }
                }
            }
        }


        //生成代码入口
        public GenerateResult GenerateWork()
        {
            GenerateResult generateResult = new GenerateResult();

            StringBuilder errorInfo = new StringBuilder();
            StringBuilder successInfo = new StringBuilder();
            //2、遍历生成对应的代码
            foreach (var d in _domainList)
            {
                Type baseType = d;
                IEnumerable<PropertyInfo> props = baseType.GetProperties();

                foreach (var w in _wantToGenerateCodeTypeList)
                {

                    switch (w)
                    {
                        case GenerateCodeEnumType.DefaultAppService:
                            //生成AppService
                            var serviceResult = GenerateServiceCode(baseType, _templateDto.ServiceFilePath, _templateDto.SaveDir);
                            if (!serviceResult.Success)
                            { //创建失败
                                errorInfo.AppendLine(serviceResult.Message);//记录对应的结果
                            }
                            else
                            {//创建成功
                                successInfo.AppendLine(serviceResult.Message);
                            }
                            break;

                        case GenerateCodeEnumType.DefaultIAppService:
                            // 生成IAppService
                            var iServiceResult = GenerateIServiceCode(baseType, _templateDto.IServiceFilePath, _templateDto.SaveDir);
                            if (!iServiceResult.Success)
                            { //创建失败
                                errorInfo.AppendLine(iServiceResult.Message);//记录对应的结果
                            }
                            else
                            {//创建成功
                                successInfo.AppendLine(iServiceResult.Message);
                            }
                            break;

                        case GenerateCodeEnumType.DefaultAddOrEditDto:
                            // 生成AddOrEditDto
                            var addOrEditDtoResult = GenerateAddOrEditDtoCode(baseType, _templateDto.AddOrEditDtoFilePath, _templateDto.SaveDir);
                            if (!addOrEditDtoResult.Success)
                            { //创建失败
                                errorInfo.AppendLine(addOrEditDtoResult.Message);//记录对应的结果
                            }
                            else
                            {//创建成功
                                successInfo.AppendLine(addOrEditDtoResult.Message);
                            }
                            break;

                        case GenerateCodeEnumType.DefaultVuePage://生成vue 界面代码
                            var vueResult = GenerateVueCode(baseType, _templateDto.VueFilePath, _templateDto.SaveDir);
                            if (!vueResult.Success)
                            { //创建失败
                                errorInfo.AppendLine(vueResult.Message);//记录对应的结果
                            }
                            else
                            {//创建成功
                                successInfo.AppendLine(vueResult.Message);
                            }
                            break;

                        case GenerateCodeEnumType.OtherCode://其他代码
                            var otherCodeResult = GenerateOtherCode(baseType, _templateDto.SaveDir, _generateCodeConfig.IsTree);
                            if (!otherCodeResult.Success)
                            { //创建失败
                                errorInfo.AppendLine(otherCodeResult.Message);//记录对应的结果
                            }
                            else
                            {//创建成功
                                successInfo.AppendLine(otherCodeResult.Message);
                            }
                            break;

                        #region 树形代码
                        case GenerateCodeEnumType.TreeAppService:
                            //生成AppService
                            var serviceTreeResult = GenerateTreeServiceCode(baseType, _templateDto.TreeServiceFilePath, _templateDto.SaveDir);
                            if (!serviceTreeResult.Success)
                            { //创建失败
                                errorInfo.AppendLine(serviceTreeResult.Message);//记录对应的结果
                            }
                            else
                            {//创建成功
                                successInfo.AppendLine(serviceTreeResult.Message);
                            }
                            break;

                        case GenerateCodeEnumType.TreeIAppService:
                            // 生成IAppService
                            var iServiceTreeResult = GenerateTreeIServiceCode(baseType, _templateDto.TreeIServiceFilePath, _templateDto.SaveDir);
                            if (!iServiceTreeResult.Success)
                            { //创建失败
                                errorInfo.AppendLine(iServiceTreeResult.Message);//记录对应的结果
                            }
                            else
                            {//创建成功
                                successInfo.AppendLine(iServiceTreeResult.Message);
                            }
                            break;

                        case GenerateCodeEnumType.DefaultEntityDto:
                            // 生成AddOrEditDto
                            var entityDtoResult = GenerateEntityDtoCode(baseType, _templateDto.EntityDtoFilePath, _templateDto.SaveDir);
                            if (!entityDtoResult.Success)
                            { //创建失败
                                errorInfo.AppendLine(entityDtoResult.Message);//记录对应的结果
                            }
                            else
                            {//创建成功
                                successInfo.AppendLine(entityDtoResult.Message);
                            }
                            break;

                        case GenerateCodeEnumType.TreeVuePage://生成vue 界面代码
                            var treeVueResult = GenerateTreeVueCode(baseType, _templateDto.TreeVueFilePath, _templateDto.SaveDir);
                            if (!treeVueResult.Success)
                            { //创建失败
                                errorInfo.AppendLine(treeVueResult.Message);//记录对应的结果
                            }
                            else
                            {//创建成功
                                successInfo.AppendLine(treeVueResult.Message);
                            }
                            break;


                            #endregion

                    }

                }


            }
            if (errorInfo.Length > 0)
            {
                generateResult.Success = false;
                generateResult.Message = errorInfo.ToString();
            }
            else
            {

                generateResult.Success = true;
                generateResult.Message = successInfo.ToString();
            }

            return generateResult;
            //    GenerateRepositoryCode(path);
            //GenerateDtoCode(path);
            //GenerateVueCode(path);

        }

        /// <summary>
        /// 创建AppService代码
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="templateFilePath"></param>
        /// <param name="saveDir"></param>
        /// <returns></returns>
        private GenerateResult GenerateServiceCode(Type baseType, string templateFilePath, string saveDir)
        {
            GenerateResult generateResult = new GenerateResult();
            generateResult.Success = true;
            string templateContent = "";

            string idTypeName = "";//Id类型
            string entityName = "";//实体名称
            string repositoryName = "";//创建仓储对象命名
            string moduleName = "";//显示的模块名
            moduleName = GenerateCodeUtils.GetTableDisplayName(baseType);
            try
            {
                #region 业务操作
                //1、校验模板不能为空
                FileUtils.ReadFile(templateFilePath, out templateContent);

                if (string.IsNullOrWhiteSpace(templateContent))
                {
                    generateResult.Success = false;
                    generateResult.Message = "AppService模板文件内容读取为空！";
                    return generateResult;
                }

                var props = baseType.GetProperties();
                entityName = baseType.Name;//实体名称
                repositoryName = entityName.Replace(entityName[0], entityName[0].ToString().ToLower()[0]);//仓储名称是表名首字母小写
                ///字段数据类型映射
                foreach (var p in props)
                {
                    var peropertyTypeName = p.PropertyType.Name;

                    var noppedAttr = p.GetCustomAttributes().Where(x => x.GetType().Name == typeof(System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute).Name);
                    if (noppedAttr.Any())
                    {//如果存在NotMaped 说明不映射直接跳过
                        continue;
                    }

                    if (p.IsDefined(typeof(System.ComponentModel.DataAnnotations.KeyAttribute), false))
                    {
                        idTypeName = peropertyTypeName;//Id类型
                    }
                }
                //2、校验主键不能为空
                if (string.IsNullOrWhiteSpace(idTypeName))
                {
                    generateResult.Success = false;
                    generateResult.Message = "实体主键Key 不能为空！";
                    return generateResult;
                }
                //3、模板替换修改内容
                templateContent = templateContent.Replace("<%=tableName%>", entityName).Replace("<%=tempType%>", idTypeName).Replace("<%=paramName%>", repositoryName)
                    .Replace("<%=tableDisplayName%>", moduleName);

                saveDir = (saveDir.LastIndexOf("/") > 0 ? saveDir : saveDir + "/") + $"//{entityName}AppService//";
                FileUtils.CreateDirectory(saveDir);
                string savePath = saveDir + $"{entityName}AppService.cs";
                //4、写入文件
                bool isWriteSucces = FileUtils.AppendWriteFile(savePath, templateContent);
                if (isWriteSucces)
                {
                    generateResult.Success = true;
                    generateResult.Message = $"创建{entityName}Service代码成功.";
                    generateResult.Content = templateContent;
                }
                else
                {
                    generateResult.Success = false;
                    generateResult.Message = $"创建{entityName}service 失败，出错位置：写入文件.";

                }

                #endregion
            }
            catch (Exception ex)
            {
                generateResult.Success = false;
                generateResult.Message = $"创建{entityName}Service代码失败." + ex.ToString();
            }

            return generateResult;
        }

        /// <summary>
        /// 创建IAppService代码
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="templateFilePath"></param>
        /// <param name="saveDir"></param>
        /// <returns></returns>
        private GenerateResult GenerateIServiceCode(Type baseType, string templateFilePath, string saveDir)
        {
            GenerateResult generateResult = new GenerateResult();
            generateResult.Success = true;
            string templateContent = "";

            string idTypeName = "";//Id类型
            string entityName = "";//实体名称
            string repositoryName = "";//创建接口仓储对象命名
            string moduleName = "";//显示的模块名
            moduleName = GenerateCodeUtils.GetTableDisplayName(baseType);
            try
            {
                #region 业务操作
                //1、校验模板不能为空
                FileUtils.ReadFile(templateFilePath, out templateContent);

                if (string.IsNullOrWhiteSpace(templateContent))
                {
                    generateResult.Success = false;
                    generateResult.Message = "IAppService模板文件内容读取为空！";
                    return generateResult;
                }

                var props = baseType.GetProperties();
                entityName = baseType.Name;//实体名称

                //1、模板替换修改内容
                templateContent = templateContent.Replace("<%=tableName%>", entityName).Replace("<%=tableDisplayName%>", moduleName);

                saveDir = (saveDir.LastIndexOf("/") > 0 ? saveDir : saveDir + "/") + $"//{entityName}AppService//";
                FileUtils.CreateDirectory(saveDir);
                string savePath = saveDir + $"I{entityName}AppService.cs";
                //4、写入文件
                bool isWriteSucces = FileUtils.AppendWriteFile(savePath, templateContent);
                if (isWriteSucces)
                {
                    generateResult.Success = true;
                    generateResult.Message = $"创建I{entityName}Service代码成功.";
                    generateResult.Content = templateContent;
                }
                else
                {
                    generateResult.Success = false;
                    generateResult.Message = $"创建I{entityName}service 失败，出错位置：写入文件.";

                }

                #endregion
            }
            catch (Exception ex)
            {
                generateResult.Success = false;
                generateResult.Message = $"创建I{entityName}Service代码失败." + ex.ToString();

            }

            return generateResult;
        }

        /// <summary>
        /// 创建 AddOrEditDto
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="templateFilePath"></param>
        /// <param name="saveDir"></param>
        /// <returns></returns>
        private GenerateResult GenerateAddOrEditDtoCode(Type baseType, string templateFilePath, string saveDir)
        {
            GenerateResult generateResult = new GenerateResult();
            generateResult.Success = true;
            string templateContent = "";

            string idTypeName = "";//Id类型
            string entityName = "";//实体名称
            string moduleName = "";//显示的模块名
            moduleName = GenerateCodeUtils.GetTableDisplayName(baseType);
            try
            {
                #region 业务操作
                //1、校验模板不能为空
                FileUtils.ReadFile(templateFilePath, out templateContent);

                if (string.IsNullOrWhiteSpace(templateContent))
                {
                    generateResult.Success = false;
                    generateResult.Message = "DTO模板文件内容读取为空！";
                    return generateResult;
                }

                var props = baseType.GetProperties();
                entityName = baseType.Name;//实体名称

                StringBuilder sbModel = new StringBuilder();
                //排除字段不要生成
                string[] exceptPropeties = new string[] { "LastModificationTime", "LastModifierUserId", "DeletionTime", "DeleterUserId",
                    "CreationTime", "CreatorUserId", "IsActive", "IsDeleted", "TenantId" };

                ///字段数据类型映射
                foreach (var p in props)
                {
                    var peropertyTypeName = p.PropertyType.Name;
                    if (peropertyTypeName.ToLower().Contains("Nullable`1".ToLower()))
                    {

                        peropertyTypeName = $"Nullable<{p.PropertyType.GetProperties()[1].PropertyType.Name}>";
                    }
                    var noppedAttr = p.GetCustomAttributes().Where(x => x.GetType().Name == typeof(System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute).Name);
                    if (noppedAttr.Any())
                    {//如果存在NotMaped 说明不映射直接跳过
                        continue;
                    }

                    if (exceptPropeties.Contains(p.Name))
                    {//排除指定的字段映射
                        continue;
                    }

                    if (p.IsDefined(typeof(System.ComponentModel.DisplayNameAttribute), false))
                    {
                        var displayNameAttr = p.GetCustomAttribute<System.ComponentModel.DisplayNameAttribute>();
                        sbModel.AppendLine(" /// <summary>");
                        sbModel.AppendLine("///" + displayNameAttr.DisplayName);
                        sbModel.AppendLine("/// </summary>");

                    }
                    else if (p.IsDefined(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false))
                    {
                        var displayNameAttr = p.GetCustomAttribute<System.ComponentModel.DataAnnotations.DisplayAttribute>();
                        sbModel.AppendLine("/// <summary>");
                        sbModel.AppendLine("///" + displayNameAttr.Name);
                        sbModel.AppendLine("/// </summary>");
                    }
                    if (p.IsDefined(typeof(System.ComponentModel.DataAnnotations.KeyAttribute), false))//dto 主键默认直接用string
                    {
                        sbModel.AppendLine($"public string {p.Name} {{get;set;}}");

                    }
                    else
                    {
                        sbModel.AppendLine($"public {peropertyTypeName} {p.Name} {{get;set;}}");
                    }



                }

                //2、模板替换修改内容
                templateContent = templateContent.Replace("<%=tableName%>", entityName).Replace("<%=modelInfo%>", sbModel.ToString()).Replace("<%=tableDisplayName%>", moduleName);

                saveDir = (saveDir.LastIndexOf("/") > 0 ? saveDir : saveDir + "/") + $"//{entityName}AppService//" + "Dto//";
                FileUtils.CreateDirectory(saveDir);
                string savePath = saveDir + $"{entityName}AddOrEditDto.cs";
                //3、写入文件
                bool isWriteSucces = FileUtils.AppendWriteFile(savePath, templateContent);
                if (isWriteSucces)
                {
                    generateResult.Success = true;
                    generateResult.Message = $"创建{entityName}AddOrEditDto代码成功.";
                    generateResult.Content = templateContent;
                }
                else
                {
                    generateResult.Success = false;
                    generateResult.Message = $"创建{entityName}AddOrEditDto 失败，出错位置：写入文件.";

                }

                #endregion
            }
            catch (Exception ex)
            {
                generateResult.Success = false;
                generateResult.Message = $"创建{entityName}AddOrEditDto代码失败." + ex.ToString();

            }

            return generateResult;

        }

        /// <summary>
        /// 创建vue 界面
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="templateFilePath"></param>
        /// <param name="saveDir"></param>
        /// <returns></returns>
        private GenerateResult GenerateVueCode(Type baseType, string templateFilePath, string saveDir)
        {
            GenerateResult generateResult = new GenerateResult();
            generateResult.Success = true;
            string templateContent = "";

            StringBuilder sbTableColumns = new StringBuilder();//表格列拼接
            StringBuilder sbFormParams = new StringBuilder();//表单参数拼接
            StringBuilder sbFormValidateRules = new StringBuilder();//表单验证拼接

            string idTypeName = "";//Id类型
            string entityName = "";//实体名称


            string serviceObjName = "";//业务实例命名

            entityName = baseType.Name;//实体名称
            serviceObjName = entityName.Replace(entityName[0], entityName[0].ToString().ToLower()[0]);//业务实例命名是表名首字母小写
            string fileName = $"{entityName}s.vue";
            string moduleName = "";//显示的模块名
           
            moduleName= GenerateCodeUtils.GetTableDisplayName(baseType);
            try
            {
                #region 业务操作
                //1、校验模板不能为空
                FileUtils.ReadFile(templateFilePath, out templateContent);

                if (string.IsNullOrWhiteSpace(templateContent))
                {
                    generateResult.Success = false;
                    generateResult.Message = "Vue模板文件内容读取为空！";
                    return generateResult;
                }

                var props = baseType.GetProperties();



                //排除字段不要生成
                string[] exceptPropeties = new string[] { "LastModificationTime", "LastModifierUserId", "DeletionTime", "DeleterUserId",
                    "CreationTime", "CreatorUserId", "IsActive", "IsDeleted", "TenantId" };
                List<PropertyInfo> mappingPropertyInfoList = new List<PropertyInfo>();//收集需要映射的字段

                int index = 0;
                StringBuilder sbFormRows = new StringBuilder();


                ///字段数据类型映射
                foreach (var p in props)
                {
                    var peropertyTypeName = p.PropertyType.Name;
                    if (peropertyTypeName.ToLower().Contains("Nullable`1".ToLower()))
                    {

                        peropertyTypeName = $"Nullable<{p.PropertyType.GetProperties()[1].PropertyType.Name}>";
                    }
                    var noppedAttr = p.GetCustomAttributes().Where(x => x.GetType().Name == typeof(System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute).Name);
                    if (noppedAttr.Any())
                    {//如果存在NotMaped 说明不映射直接跳过
                        continue;
                    }

                    if (exceptPropeties.Contains(p.Name))
                    {//排除指定的字段映射
                        continue;
                    }
                    if (p.IsDefined(typeof(System.ComponentModel.DataAnnotations.KeyAttribute), false))//主键类型不映射，直接跳过
                    {
                        continue;
                    }

                    string displayName = "";//显示字段名称
                    #region 表格代码拼接
                    string tempColumn = VueTemplateCodesnippetConfig.TableColumn.Replace("{{keyName}}", p.Name.Replace(p.Name[0], p.Name[0].ToString().ToLower()[0]));

                    if (p.IsDefined(typeof(System.ComponentModel.DisplayNameAttribute), false))
                    {
                        var displayNameAttr = p.GetCustomAttribute<System.ComponentModel.DisplayNameAttribute>();
                        displayName = displayNameAttr.DisplayName;
                    }
                    else if (p.IsDefined(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false))
                    {
                        var displayAttr = p.GetCustomAttribute<System.ComponentModel.DataAnnotations.DisplayAttribute>();
                        displayName = displayAttr.Name;
                    }
                    tempColumn = tempColumn.Replace("{{displayName}}", displayName);
                    sbTableColumns.AppendLine(tempColumn);
                    #endregion
                    var propInfo = GenerateCodeUtils.GetTypePropertyInfo(p);
                    ///声明参数拼接
                    sbFormParams.AppendLine(VueTemplateCodesnippetConfig.AddOrEditFormParam(propInfo.TypeName).Replace("{{propertyName}}", propInfo.InitialsToLowerPropertyName));

                    var propertyInfo = GenerateCodeUtils.GetTypePropertyInfo(p);
                    StringBuilder sbValidate = new StringBuilder();//验证拼接
                    string tempFormPropertyRule = VueTemplateCodesnippetConfig.AddOrEditFormParamRules.Replace("{{propertyName}}", propertyInfo.InitialsToLowerPropertyName);

                    sbValidate.AppendLine(VueTemplateCodesnippetConfig.AddOrEditFormParamRules_Require.Replace("{{displayName}}", propertyInfo.DisPlayName));
                    if (propertyInfo.StringTypeLength > 0)
                    {

                        sbValidate.AppendLine(VueTemplateCodesnippetConfig.AddOrEditFormParamRules_StringMax.Replace("{{propertyLength}}", propertyInfo.StringTypeLength.ToString())
                            .Replace("{{displayName}}", propertyInfo.DisPlayName));

                    }
                    sbFormValidateRules.AppendLine(tempFormPropertyRule.Replace("{{validate}}", sbValidate.ToString()));

                    mappingPropertyInfoList.Add(p);
                }

                //表单拼接
                if (mappingPropertyInfoList.Count > 0)//必须是有字段情况下，才进行表单的映射生成
                {
                    var remainder = mappingPropertyInfoList.Count % 2;//余数
                    var merchant = mappingPropertyInfoList.Count / 2;//商
                    if (remainder == 0)
                    {//可以被2整除，划分两列

                        for (int i = 1; i <= merchant; i++)
                        {
                            var rowIndex = i * 2;
                            var colcumn1 = mappingPropertyInfoList[rowIndex - 1];
                            var colcumn2 = mappingPropertyInfoList[rowIndex - 2];
                            string displayName = "";//显示字段名称
                            string tempColumn = "";

                            var propCol1 = GenerateCodeUtils.GetTypePropertyInfo(colcumn1);
                            var propCol2 = GenerateCodeUtils.GetTypePropertyInfo(colcumn2);

                            StringBuilder sbFormCol = new StringBuilder();
                            sbFormCol.AppendLine(VueTemplateCodesnippetConfig.GetFormRowColumn().Replace("{{columnLable}}", propCol1.DisPlayName).Replace("{{columnProp}}", propCol1.InitialsToLowerPropertyName));
                            sbFormCol.AppendLine(VueTemplateCodesnippetConfig.GetFormRowColumn().Replace("{{columnLable}}", propCol2.DisPlayName).Replace("{{columnProp}}", propCol2.InitialsToLowerPropertyName));
                            sbFormRows.AppendLine(VueTemplateCodesnippetConfig.FormRow.Replace("{{RowData}}", sbFormCol.ToString()));
                        }

                    }
                    else
                    {
                        //不可以被二整除，最后一列划分为1
                        if (merchant >= 1)
                        { //>=1 说明至少存在两列以上
                            for (int i = 1; i <= merchant; i++)
                            {
                                var rowIndex = i * 2;
                                var colcumn1 = mappingPropertyInfoList[rowIndex - 1];
                                var colcumn2 = mappingPropertyInfoList[rowIndex - 2];
                                string displayName = "";//显示字段名称
                                string tempColumn = "";

                                var propCol1 = GenerateCodeUtils.GetTypePropertyInfo(colcumn1);
                                var propCol2 = GenerateCodeUtils.GetTypePropertyInfo(colcumn2);
                                StringBuilder sbFormCol = new StringBuilder();

                                sbFormCol.AppendLine(VueTemplateCodesnippetConfig.GetFormRowColumn().Replace("{{columnLable}}", propCol1.DisPlayName).Replace("{{columnProp}}", propCol1.InitialsToLowerPropertyName));
                                sbFormCol.AppendLine(VueTemplateCodesnippetConfig.GetFormRowColumn().Replace("{{columnLable}}", propCol2.DisPlayName).Replace("{{columnProp}}", propCol2.InitialsToLowerPropertyName));
                                sbFormRows.AppendLine(VueTemplateCodesnippetConfig.FormRow.Replace("{{RowData}}", sbFormCol.ToString()));
                            }
                            StringBuilder sbFormLastCol = new StringBuilder();
                            var lastPropCol = GenerateCodeUtils.GetTypePropertyInfo(mappingPropertyInfoList[mappingPropertyInfoList.Count - 1]);
                            sbFormLastCol.AppendLine(VueTemplateCodesnippetConfig.GetFormRowColumn().Replace("{{columnLable}}", lastPropCol.DisPlayName).Replace("{{columnProp}}", lastPropCol.InitialsToLowerPropertyName));
                            sbFormRows.AppendLine(VueTemplateCodesnippetConfig.FormRow.Replace("{{RowData}}", sbFormLastCol.ToString()));
                        }
                        else
                        { //说明小于两列（前面至少大于0已经过滤，所以这里不会为0）

                            StringBuilder sbFormCol = new StringBuilder();
                            var propCol1 = GenerateCodeUtils.GetTypePropertyInfo(mappingPropertyInfoList[0]);
                            sbFormCol.AppendLine(VueTemplateCodesnippetConfig.GetFormRowColumn().Replace("{{columnLable}}", propCol1.DisPlayName).Replace("{{columnProp}}", propCol1.InitialsToLowerPropertyName));
                            sbFormRows.AppendLine(VueTemplateCodesnippetConfig.FormRow.Replace("{{RowData}}", sbFormCol.ToString()));
                        }

                    }
                }


                //2、模板替换修改内容
                templateContent = templateContent.Replace("<%=tableName%>", entityName)
                     .Replace("<%=moduleName%>", moduleName)
                     .Replace("<%=serviceObjName%>", serviceObjName)
                     .Replace("{{table-Columns}}", sbTableColumns.ToString())
                     .Replace("{{form-Rows}}", sbFormRows.ToString())
                     .Replace("{{form-viewRows}}", sbFormRows.Replace("addOrEditForm", "viewForm").ToString())
                     .Replace("{{addOrEditForm-properties}}", sbFormParams.ToString())
                     .Replace("{{form-rules}}", sbFormValidateRules.ToString());

                saveDir = (saveDir.LastIndexOf("/") > 0 ? saveDir : saveDir + "/") + $"//View//{entityName}//";
                FileUtils.CreateDirectory(saveDir);
                string savePath = saveDir + fileName;
                //3、写入文件
                bool isWriteSucces = FileUtils.AppendWriteFile(savePath, templateContent);
                if (isWriteSucces)
                {
                    generateResult.Success = true;
                    generateResult.Message = $"创建 {fileName}代码成功.";
                    generateResult.Content = templateContent;
                }
                else
                {
                    generateResult.Success = false;
                    generateResult.Message = $"创建{fileName} 失败，出错位置：写入文件.";

                }

                #endregion
            }
            catch (Exception ex)
            {
                generateResult.Success = false;
                generateResult.Message = $"创建{entityName} 失败." + ex.ToString();

            }

            return generateResult;

        }

        /// <summary>
        /// 创建其他需要生成的代码
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="saveDir"></param>
        /// <param name="isTree">如果是树形，内部动态替换相关内容</param>
        /// <returns></returns>

        private GenerateResult GenerateOtherCode(Type baseType, string saveDir, bool isTree = false)
        {
            GenerateResult generateResult = new GenerateResult();
            generateResult.Success = true;

            StringBuilder front_mainjsApiInfo = new StringBuilder();//前端main.js 需要的代码
            StringBuilder front_routerInfo = new StringBuilder();//前端router 需要的代码
            StringBuilder front_menuInfo = new StringBuilder();//前端目录代码 Home.vue
            StringBuilder backstage_mappingInfo = new StringBuilder();//后台autoMapper 需要的代码
            StringBuilder permissionInfo = new StringBuilder();//权限配置
            var props = baseType.GetProperties();
            var entityName = baseType.Name;//实体名称

            StringBuilder templateContent = new StringBuilder();//最后汇总输出
            string fileName = $"{entityName}s.txt";//输出的文件名

            string moduleName = "";//显示的模块名
            moduleName = GenerateCodeUtils.GetTableDisplayName(baseType);
            try
            {
                //1、拼接main.js代码
                front_mainjsApiInfo.AppendLine("-------前端main.js 需要添加如下的api请求配置----------------------\r\n");
                front_mainjsApiInfo.AppendLine(VueTemplateCodesnippetConfig.GetMainjsApiInfo(isTree).Replace("{{tableName}}", entityName)
                    .Replace("{{propertyName}}", GenerateCodeUtils.GetHumpStr(entityName)));
                //2、拼接router 需要代码
                front_mainjsApiInfo.AppendLine("----------前端router 目录下的index.js 中home 路由需要添加如下的子路由请求配置-------------\r\n");
                front_routerInfo.AppendLine(VueTemplateCodesnippetConfig.GetRouterInfo.Replace("{{tableName}}", entityName)
                    .Replace("{{propertyName}}", GenerateCodeUtils.GetHumpStr(entityName)));


                //3、拼接菜单目录代码
                front_menuInfo.AppendLine("----------前端Home.vue 中需要添加目录代码-------------\r\n");
                front_menuInfo.AppendLine(VueTemplateCodesnippetConfig.GetMenuInfo.Replace("{{propertyName}}", GenerateCodeUtils.GetHumpStr(entityName)).Replace("{{tableDisplayName}}", moduleName));

                //4、autoMapper 需要的代码
                backstage_mappingInfo.AppendLine("-------后台 applicationService 层 MapConfig 目录下 添加AutoMap 配置-----------\r\n");
                string automappingConfigStr = $"CreateMap<{baseType.Name}, {baseType.Name}AddOrEditDto>((MemberList.None)).ReverseMap();";

                //5、权限配置
                permissionInfo.AppendLine("----------------权限功能 配置-----------------------\r\n");
                permissionInfo.AppendLine(VueTemplateCodesnippetConfig.GetPermissionInfo().Replace("{{moduleName}}", moduleName).Replace("{{propertyName}}", GenerateCodeUtils.GetHumpStr(entityName)).Replace("{{tableName}}", entityName));
                backstage_mappingInfo.AppendLine(automappingConfigStr);
                backstage_mappingInfo.AppendLine($"CreateMap<{baseType.Name}, {baseType.Name}Dto>((MemberList.None)).ReverseMap();");
                templateContent.AppendLine(front_mainjsApiInfo.ToString());
                templateContent.AppendLine(front_routerInfo.ToString());
                templateContent.AppendLine(front_menuInfo.ToString());
                templateContent.AppendLine(backstage_mappingInfo.ToString());
                templateContent.AppendLine(permissionInfo.ToString());

                saveDir = (saveDir.LastIndexOf("/") > 0 ? saveDir : saveDir + "/") + $"//OtherCode//{entityName}//";
                FileUtils.CreateDirectory(saveDir);
                string savePath = saveDir + fileName;

                //3、写入文件
                bool isWriteSucces = FileUtils.AppendWriteFile(savePath, templateContent.ToString());
                if (isWriteSucces)
                {
                    generateResult.Success = true;
                    generateResult.Message = $"创建{fileName}代码成功.";
                    generateResult.Content = templateContent.ToString();
                }
                else
                {
                    generateResult.Success = false;
                    generateResult.Message = $"创建{fileName}失败，出错位置：写入文件.";

                }

            }
            catch (Exception ex)
            {

                generateResult.Success = false;
                generateResult.Message = $"创建{fileName}代码失败." + ex.ToString();
            }

            return generateResult;
        }


        #region GenerateTreeCodeService

        /// <summary>
        /// 创建树形 AppService代码
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="templateFilePath"></param>
        /// <param name="saveDir"></param>
        /// <returns></returns>
        private GenerateResult GenerateTreeServiceCode(Type baseType, string templateFilePath, string saveDir)
        {
            GenerateResult generateResult = new GenerateResult();
            generateResult.Success = true;
            string templateContent = "";

            string idTypeName = "";//Id类型
            string entityName = "";//实体名称
            string repositoryName = "";//创建仓储对象命名
            string moduleName = "";//显示的模块名
            moduleName = GenerateCodeUtils.GetTableDisplayName(baseType);
            try
            {
                #region 业务操作
                //1、校验模板不能为空
                FileUtils.ReadFile(templateFilePath, out templateContent);

                if (string.IsNullOrWhiteSpace(templateContent))
                {
                    generateResult.Success = false;
                    generateResult.Message = "树形 AppService模板文件内容读取为空！";
                    return generateResult;
                }

                var props = baseType.GetProperties();
                entityName = baseType.Name;//实体名称
                repositoryName = entityName.Replace(entityName[0], entityName[0].ToString().ToLower()[0]);//仓储名称是表名首字母小写
                ///字段数据类型映射
                foreach (var p in props)
                {
                    var peropertyTypeName = p.PropertyType.Name;

                    var noppedAttr = p.GetCustomAttributes().Where(x => x.GetType().Name == typeof(System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute).Name);
                    if (noppedAttr.Any())
                    {//如果存在NotMaped 说明不映射直接跳过
                        continue;
                    }
                 
                    if (p.IsDefined(typeof(System.ComponentModel.DataAnnotations.KeyAttribute), false))
                    {
                        idTypeName = peropertyTypeName;//Id类型
                    }
                }
                //2、校验主键不能为空
                if (string.IsNullOrWhiteSpace(idTypeName))
                {
                    generateResult.Success = false;
                    generateResult.Message = "实体主键Key 不能为空！";
                    return generateResult;
                }
                //3、模板替换修改内容
                templateContent = templateContent.Replace("<%=tableName%>", entityName).Replace("<%=tempType%>", idTypeName).Replace("<%=paramName%>", repositoryName)
                    .Replace("<%=tableDisplayName%>", moduleName);

                saveDir = (saveDir.LastIndexOf("/") > 0 ? saveDir : saveDir + "/") + $"//{entityName}AppService//";
                FileUtils.CreateDirectory(saveDir);
                string savePath = saveDir + $"{entityName}AppService.cs";
                //4、写入文件
                bool isWriteSucces = FileUtils.AppendWriteFile(savePath, templateContent);
                if (isWriteSucces)
                {
                    generateResult.Success = true;
                    generateResult.Message = $"创建树形 {entityName}Service代码成功.";
                    generateResult.Content = templateContent;
                }
                else
                {
                    generateResult.Success = false;
                    generateResult.Message = $"创建树形 {entityName}service 失败，出错位置：写入文件.";

                }

                #endregion
            }
            catch (Exception ex)
            {
                generateResult.Success = false;
                generateResult.Message = $"创建树形 {entityName}Service代码失败." + ex.ToString();
            }

            return generateResult;
        }

        /// <summary>
        /// 创建树形 IAppService代码
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="templateFilePath"></param>
        /// <param name="saveDir"></param>
        /// <returns></returns>
        private GenerateResult GenerateTreeIServiceCode(Type baseType, string templateFilePath, string saveDir)
        {
            GenerateResult generateResult = new GenerateResult();
            generateResult.Success = true;
            string templateContent = "";

            string idTypeName = "";//Id类型
            string entityName = "";//实体名称
            string repositoryName = "";//创建接口仓储对象命名
            string moduleName = "";//显示的模块名
            moduleName = GenerateCodeUtils.GetTableDisplayName(baseType);
            try
            {
                #region 业务操作
                //1、校验模板不能为空
                FileUtils.ReadFile(templateFilePath, out templateContent);

                if (string.IsNullOrWhiteSpace(templateContent))
                {
                    generateResult.Success = false;
                    generateResult.Message = "树形 IAppService模板文件内容读取为空！";
                    return generateResult;
                }

                var props = baseType.GetProperties();
                entityName = baseType.Name;//实体名称

                //1、模板替换修改内容
                templateContent = templateContent.Replace("<%=tableName%>", entityName).Replace("<%=tableDisplayName%>", moduleName);

                saveDir = (saveDir.LastIndexOf("/") > 0 ? saveDir : saveDir + "/") + $"//{entityName}AppService//";
                FileUtils.CreateDirectory(saveDir);
                string savePath = saveDir + $"I{entityName}AppService.cs";
                //4、写入文件
                bool isWriteSucces = FileUtils.AppendWriteFile(savePath, templateContent);
                if (isWriteSucces)
                {
                    generateResult.Success = true;
                    generateResult.Message = $"创建树形 I{entityName}Service代码成功.";
                    generateResult.Content = templateContent;
                }
                else
                {
                    generateResult.Success = false;
                    generateResult.Message = $"创建树形 I{entityName}service 失败，出错位置：写入文件.";

                }

                #endregion
            }
            catch (Exception ex)
            {
                generateResult.Success = false;
                generateResult.Message = $"创建树形 {entityName}Service代码失败." + ex.ToString();

            }

            return generateResult;
        }

        /// <summary>
        /// 创建树形 vue 界面,需要默认字段ParentId
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="templateFilePath"></param>
        /// <param name="saveDir"></param>
        /// <returns></returns>
        private GenerateResult GenerateTreeVueCode(Type baseType, string templateFilePath, string saveDir)
        {
            GenerateResult generateResult = new GenerateResult();
            generateResult.Success = true;
            string templateContent = "";

            StringBuilder sbTableColumns = new StringBuilder();//表格列拼接
            StringBuilder sbFormParams = new StringBuilder();//表单参数拼接
            StringBuilder sbFormValidateRules = new StringBuilder();//表单验证拼接

            string idTypeName = "";//Id类型
            string entityName = "";//实体名称


            string serviceObjName = "";//业务实例命名

            entityName = baseType.Name;//实体名称
            serviceObjName = entityName.Replace(entityName[0], entityName[0].ToString().ToLower()[0]);//业务实例命名是表名首字母小写
            string fileName = $"{entityName}s.vue";
            string moduleName = "";//显示的模块名
            var displayAttrbute = baseType.CustomAttributes.Where(x => x.AttributeType.Equals(typeof(DisplayAttribute))).FirstOrDefault();
            //处理展示的名字
            moduleName = GenerateCodeUtils.GetTableDisplayName(baseType);

            try
            {
                #region 业务操作
                //1、校验模板不能为空
                FileUtils.ReadFile(templateFilePath, out templateContent);

                if (string.IsNullOrWhiteSpace(templateContent))
                {
                    generateResult.Success = false;
                    generateResult.Message = "TreeVue模板文件内容读取为空！";
                    return generateResult;
                }

                var props = baseType.GetProperties();



                //排除字段不要生成
                string[] exceptPropeties = new string[] { "LastModificationTime", "LastModifierUserId", "DeletionTime", "DeleterUserId",
                    "CreationTime", "CreatorUserId", "IsActive", "IsDeleted", "TenantId" };
                List<PropertyInfo> mappingPropertyInfoList = new List<PropertyInfo>();//收集需要映射的字段

                int index = 0;
                StringBuilder sbFormRows = new StringBuilder();


                ///字段数据类型映射
                foreach (var p in props)
                {
                    var peropertyTypeName = p.PropertyType.Name;
                    if (peropertyTypeName.ToLower().Contains("Nullable`1".ToLower()))
                    {

                        peropertyTypeName = $"Nullable<{p.PropertyType.GetProperties()[1].PropertyType.Name}>";
                    }
                    var noppedAttr = p.GetCustomAttributes().Where(x => x.GetType().Name == typeof(System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute).Name);
                    if (noppedAttr.Any())
                    {//如果存在NotMaped 说明不映射直接跳过
                        continue;
                    }

                    if (exceptPropeties.Contains(p.Name))
                    {//排除指定的字段映射
                        continue;
                    }
                    if (p.IsDefined(typeof(System.ComponentModel.DataAnnotations.KeyAttribute), false))//主键类型不映射，直接跳过
                    {
                        continue;
                    }

                    string displayName = "";//显示字段名称
                    #region 表格代码拼接
                    string tempColumn = VueTemplateCodesnippetConfig.TreeTableColumn.Replace("{{keyName}}", p.Name.Replace(p.Name[0], p.Name[0].ToString().ToLower()[0]));

                    if (p.IsDefined(typeof(System.ComponentModel.DisplayNameAttribute), false))
                    {
                        var displayNameAttr = p.GetCustomAttribute<System.ComponentModel.DisplayNameAttribute>();
                        displayName = displayNameAttr.DisplayName;
                    }
                    else if (p.IsDefined(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false))
                    {
                        var displayAttr = p.GetCustomAttribute<System.ComponentModel.DataAnnotations.DisplayAttribute>();
                        displayName = displayAttr.Name;
                    }
                    tempColumn = tempColumn.Replace("{{displayName}}", displayName);
                    sbTableColumns.AppendLine(tempColumn);
                    #endregion
                  var propInfo=  GenerateCodeUtils.GetTypePropertyInfo(p);
                    ///声明参数拼接
                    sbFormParams.AppendLine(VueTemplateCodesnippetConfig.AddOrEditFormParam(propInfo.TypeName).Replace("{{propertyName}}", propInfo.InitialsToLowerPropertyName));

                    var propertyInfo = GenerateCodeUtils.GetTypePropertyInfo(p);
                    StringBuilder sbValidate = new StringBuilder();//验证拼接
                    string tempFormPropertyRule = VueTemplateCodesnippetConfig.AddOrEditFormParamRules.Replace("{{propertyName}}", propertyInfo.InitialsToLowerPropertyName);

                    sbValidate.AppendLine(VueTemplateCodesnippetConfig.AddOrEditFormParamRules_Require.Replace("{{displayName}}", propertyInfo.DisPlayName));
                    if (propertyInfo.StringTypeLength > 0)
                    {

                        sbValidate.AppendLine(VueTemplateCodesnippetConfig.AddOrEditFormParamRules_StringMax.Replace("{{propertyLength}}", propertyInfo.StringTypeLength.ToString())
                            .Replace("{{displayName}}", propertyInfo.DisPlayName));

                    }
                    sbFormValidateRules.AppendLine(tempFormPropertyRule.Replace("{{validate}}", sbValidate.ToString()));

                    mappingPropertyInfoList.Add(p);
                }

                //表单拼接
                if (mappingPropertyInfoList.Count > 0)//必须是有字段情况下，才进行表单的映射生成
                {
                    var remainder = mappingPropertyInfoList.Count % 2;//余数
                    var merchant = mappingPropertyInfoList.Count / 2;//商
                    if (remainder == 0)
                    {//可以被2整除，划分两列

                        for (int i = 1; i <= merchant; i++)
                        {
                            var rowIndex = i * 2;
                            var colcumn1 = mappingPropertyInfoList[rowIndex - 1];
                            var colcumn2 = mappingPropertyInfoList[rowIndex - 2];
                            string displayName = "";//显示字段名称
                            string tempColumn = "";

                            var propCol1 = GenerateCodeUtils.GetTypePropertyInfo(colcumn1);
                            var propCol2 = GenerateCodeUtils.GetTypePropertyInfo(colcumn2);

                            StringBuilder sbFormCol = new StringBuilder();
                            sbFormCol.AppendLine(VueTemplateCodesnippetConfig.GetFormRowColumn().Replace("{{columnLable}}", propCol1.DisPlayName).Replace("{{columnProp}}", propCol1.InitialsToLowerPropertyName));
                            sbFormCol.AppendLine(VueTemplateCodesnippetConfig.GetFormRowColumn().Replace("{{columnLable}}", propCol2.DisPlayName).Replace("{{columnProp}}", propCol2.InitialsToLowerPropertyName));
                            sbFormRows.AppendLine(VueTemplateCodesnippetConfig.FormRow.Replace("{{RowData}}", sbFormCol.ToString()));
                        }

                    }
                    else
                    {
                        //不可以被二整除，最后一列划分为1
                        if (merchant >= 1)
                        { //>=1 说明至少存在两列以上
                            for (int i = 1; i <= merchant; i++)
                            {
                                var rowIndex = i * 2;
                                var colcumn1 = mappingPropertyInfoList[rowIndex - 1];
                                var colcumn2 = mappingPropertyInfoList[rowIndex - 2];
                                string displayName = "";//显示字段名称
                                string tempColumn = "";

                                var propCol1 = GenerateCodeUtils.GetTypePropertyInfo(colcumn1);
                                var propCol2 = GenerateCodeUtils.GetTypePropertyInfo(colcumn2);
                                StringBuilder sbFormCol = new StringBuilder();

                                sbFormCol.AppendLine(VueTemplateCodesnippetConfig.GetFormRowColumn().Replace("{{columnLable}}", propCol1.DisPlayName).Replace("{{columnProp}}", propCol1.InitialsToLowerPropertyName));
                                sbFormCol.AppendLine(VueTemplateCodesnippetConfig.GetFormRowColumn().Replace("{{columnLable}}", propCol2.DisPlayName).Replace("{{columnProp}}", propCol2.InitialsToLowerPropertyName));
                                sbFormRows.AppendLine(VueTemplateCodesnippetConfig.FormRow.Replace("{{RowData}}", sbFormCol.ToString()));
                            }
                            StringBuilder sbFormLastCol = new StringBuilder();
                            var lastPropCol = GenerateCodeUtils.GetTypePropertyInfo(mappingPropertyInfoList[mappingPropertyInfoList.Count - 1]);
                            sbFormLastCol.AppendLine(VueTemplateCodesnippetConfig.GetFormRowColumn().Replace("{{columnLable}}", lastPropCol.DisPlayName).Replace("{{columnProp}}", lastPropCol.InitialsToLowerPropertyName));
                            sbFormRows.AppendLine(VueTemplateCodesnippetConfig.FormRow.Replace("{{RowData}}", sbFormLastCol.ToString()));
                        }
                        else
                        { //说明小于两列（前面至少大于0已经过滤，所以这里不会为0）

                            StringBuilder sbFormCol = new StringBuilder();
                            var propCol1 = GenerateCodeUtils.GetTypePropertyInfo(mappingPropertyInfoList[0]);
                            sbFormCol.AppendLine(VueTemplateCodesnippetConfig.GetFormRowColumn().Replace("{{columnLable}}", propCol1.DisPlayName).Replace("{{columnProp}}", propCol1.InitialsToLowerPropertyName));
                            sbFormRows.AppendLine(VueTemplateCodesnippetConfig.FormRow.Replace("{{RowData}}", sbFormCol.ToString()));
                        }

                    }
                }


                //2、模板替换修改内容
                templateContent = templateContent.Replace("<%=tableName%>", entityName)
                     .Replace("<%=moduleName%>", moduleName)
                     .Replace("<%=serviceObjName%>", serviceObjName)
                     .Replace("{{table-Columns}}", sbTableColumns.ToString())
                     .Replace("{{form-Rows}}", sbFormRows.ToString())
                     .Replace("{{form-viewRows}}", sbFormRows.Replace("addOrEditForm", "viewForm").ToString())
                     .Replace("{{addOrEditForm-properties}}", sbFormParams.ToString())
                     .Replace("{{form-rules}}", sbFormValidateRules.ToString());

                saveDir = (saveDir.LastIndexOf("/") > 0 ? saveDir : saveDir + "/") + $"//View//{entityName}//";
                FileUtils.CreateDirectory(saveDir);
                string savePath = saveDir + fileName;
                //3、写入文件
                bool isWriteSucces = FileUtils.AppendWriteFile(savePath, templateContent);
                if (isWriteSucces)
                {
                    generateResult.Success = true;
                    generateResult.Message = $"创建树形 {fileName}代码成功.";
                    generateResult.Content = templateContent;
                }
                else
                {
                    generateResult.Success = false;
                    generateResult.Message = $"创建树形 {fileName} 失败，出错位置：写入文件.";

                }

                #endregion
            }
            catch (Exception ex)
            {
                generateResult.Success = false;
                generateResult.Message = $"创建树形 {fileName}失败." + ex.ToString();

            }

            return generateResult;

        }

        /// <summary>
        /// 创建 实体Dto
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="templateFilePath"></param>
        /// <param name="saveDir"></param>
        /// <returns></returns>
        private GenerateResult GenerateEntityDtoCode(Type baseType, string templateFilePath, string saveDir)
        {
            GenerateResult generateResult = new GenerateResult();
            generateResult.Success = true;
            string templateContent = "";

            string idTypeName = "";//Id类型
            string entityName = "";//实体名称
            string moduleName = "";//显示的模块名
            moduleName = GenerateCodeUtils.GetTableDisplayName(baseType);
            try
            {
                #region 业务操作
                //1、校验模板不能为空
                FileUtils.ReadFile(templateFilePath, out templateContent);

                if (string.IsNullOrWhiteSpace(templateContent))
                {
                    generateResult.Success = false;
                    generateResult.Message = "EntityDTO模板文件内容读取为空！";
                    return generateResult;
                }

                var props = baseType.GetProperties();
                entityName = baseType.Name;//实体名称

                StringBuilder sbModel = new StringBuilder();
                //排除字段不要生成
                string[] exceptPropeties = new string[] { "LastModificationTime", "LastModifierUserId", "DeletionTime", "DeleterUserId",
                    "CreationTime", "CreatorUserId", "IsActive", "IsDeleted", "TenantId" };

                ///字段数据类型映射
                foreach (var p in props)
                {
                    var peropertyTypeName = p.PropertyType.Name;
                    if (peropertyTypeName.ToLower().Contains("Nullable`1".ToLower()))
                    {

                        peropertyTypeName = $"Nullable<{p.PropertyType.GetProperties()[1].PropertyType.Name}>";
                    }
                    var noppedAttr = p.GetCustomAttributes().Where(x => x.GetType().Name == typeof(System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute).Name);
                    if (noppedAttr.Any())
                    {//如果存在NotMaped 说明不映射直接跳过
                        continue;
                    }

                    if (exceptPropeties.Contains(p.Name))
                    {//排除指定的字段映射
                        continue;
                    }

                    if (p.IsDefined(typeof(System.ComponentModel.DisplayNameAttribute), false))
                    {
                        var displayNameAttr = p.GetCustomAttribute<System.ComponentModel.DisplayNameAttribute>();
                        sbModel.AppendLine(" /// <summary>");
                        sbModel.AppendLine("///" + displayNameAttr.DisplayName);
                        sbModel.AppendLine("/// </summary>");

                    }
                    else if (p.IsDefined(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false))
                    {
                        var displayNameAttr = p.GetCustomAttribute<System.ComponentModel.DataAnnotations.DisplayAttribute>();
                        sbModel.AppendLine("/// <summary>");
                        sbModel.AppendLine("///" + displayNameAttr.Name);
                        sbModel.AppendLine("/// </summary>");
                    }
                    if (p.IsDefined(typeof(System.ComponentModel.DataAnnotations.KeyAttribute), false)||p.Name.Equals("ParentId"))//dto 主键默认直接用string
                    {
                        sbModel.AppendLine($"public string {p.Name} {{get;set;}}");

                    }
                    else
                    {
                        sbModel.AppendLine($"public {peropertyTypeName} {p.Name} {{get;set;}}");
                    }



                }

                //2、模板替换修改内容
                templateContent = templateContent.Replace("<%=tableName%>", entityName).Replace("<%=modelInfo%>", sbModel.ToString()).Replace("<%=tableDisplayName%>", moduleName);

                saveDir = (saveDir.LastIndexOf("/") > 0 ? saveDir : saveDir + "/") + $"//{entityName}AppService//" + "Dto//";
                FileUtils.CreateDirectory(saveDir);
                string savePath = saveDir + $"{entityName}Dto.cs";
                //3、写入文件
                bool isWriteSucces = FileUtils.AppendWriteFile(savePath, templateContent);
                if (isWriteSucces)
                {
                    generateResult.Success = true;
                    generateResult.Message = $"创建{entityName}Dto代码成功.";
                    generateResult.Content = templateContent;
                }
                else
                {
                    generateResult.Success = false;
                    generateResult.Message = $"创建{entityName}Dto 失败，出错位置：写入文件.";

                }

                #endregion
            }
            catch (Exception ex)
            {
                generateResult.Success = false;
                generateResult.Message = $"创建{entityName}Dto 代码失败." + ex.ToString();

            }

            return generateResult;

        }

        #endregion
        /// <summary>
        /// 创建 Entity
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="templateFilePath"></param>
        /// <param name="saveDir"></param>
        /// <returns></returns>
        public GenerateResult GenerateEntityCode(HashSet<string> tables, string templateFilePath, string saveDir)
        {
            DatabaseConfig _dbDto = new DbConfig().DatabaseConfig;
            CodeGenerateDBService _service = new CodeGenerateDBService();
            _service.SetInitConnection(_dbDto.DefaultDBConnectionString, _dbDto.GetDbType());
            string _dbName = _dbDto.DefaultDBConnectionString.Split(';').AsEnumerable().Where(x => x.Contains("Database")).FirstOrDefault().Split('=')[1];
            if (tables is null)
            {
                tables = new HashSet<string>();
                IEnumerable<dynamic> _dynamicTables = null;
                using (_service.connection)
                {
                    string _sqlTable = $@"SELECT
                                            TABLE_NAME, -- 表名
	                                        TABLE_TYPE,-- 表类型
	                                        ENGINE,-- 表引擎
	                                        TABLE_COLLATION,-- 排序规则
	                                        TABLE_COMMENT -- 表注释	
                                        FROM
	                                        information_schema.TABLES 
                                        WHERE
	                                        TABLE_SCHEMA = '{_dbName}'";
                    _dynamicTables = _service.connection.Query(_sqlTable);
                }
                foreach (var item in _dynamicTables)
                {
                    tables.Add((item as IDictionary<string, object>)["TABLE_NAME"].ToString());
                }
            }

            GenerateResult _generateResult = new GenerateResult();
            foreach (var item in tables)
            {
                IEnumerable<dynamic> _dynamicColumns = null;
                IEnumerable<dynamic> _dynamicDefaults = null;
                IEnumerable<dynamic> _dynamicTables = null;
                using (_service.connection)
                {
                    string _sql = $@"SELECT
	                            COLUMN_NAME,-- 列名
	                            COLUMN_COMMENT,-- 注释
	                            DATA_TYPE,-- 数据类型
                                CASE DATA_TYPE WHEN 'bigint' THEN 'int' 
                                            WHEN 'tinyint' THEN 'int' 
                                            WHEN 'smallint' THEN 'int' 
                                            WHEN 'varchar' THEN 'string'
                                            WHEN 'varbinary' THEN 'string'
                                            WHEN 'text' THEN 'string'
                                            WHEN 'char' THEN 'string'
                                            WHEN 'datetime' THEN 'DateTime'
                                            WHEN 'bit' THEN 'bool'
                                            WHEN 'decimal' THEN 'decimal'
                                            ELSE DATA_TYPE END AS DATA_TYPEC,
	                            IS_NULLABLE, -- 列为空
	                            CHARACTER_MAXIMUM_LENGTH, -- 最大长度
	                            COLUMN_KEY
                            FROM
	                            information_schema.COLUMNS 
                            WHERE
	                            TABLE_NAME = '{item}' 
	                            AND TABLE_SCHEMA = '{_dbName}'
                            ";
                    _dynamicColumns = _service.connection.Query(_sql);

                    string _sqlDefaults = $"desc `{item}`";
                    _dynamicDefaults = _service.connection.Query(_sqlDefaults);

                    string _sqlTable = $@"SELECT
	                                        TABLE_TYPE,-- 表类型
	                                        ENGINE,-- 表引擎
	                                        TABLE_COLLATION,-- 排序规则
	                                        TABLE_COMMENT -- 表注释	
                                        FROM
	                                        information_schema.TABLES 
                                        WHERE
	                                        TABLE_SCHEMA = '{_dbName}'
                                            AND TABLE_NAME ='{item}'";
                    _dynamicTables = _service.connection.Query(_sqlTable);
                }

                IDictionary<string, object> _dynamicModule = _dynamicTables.FirstOrDefault() as IDictionary<string, object>;
                _generateResult.Success = true;
                string _templateContent = "";
                string _entityName = item;//类名称
                StringBuilder _moduleName = new StringBuilder();//类注释显示的模块名
                _moduleName.AppendLine(" /// <summary>");
                _moduleName.AppendLine($"/// {_entityName} {_dynamicModule["TABLE_COMMENT"]}");
                _moduleName.AppendLine("/// </summary>");
                _moduleName.AppendLine($"[Table(\"{_entityName}\")]");
                _moduleName.AppendLine($"[Display(Name = \"{_dynamicModule["TABLE_COMMENT"]}\")]");
                try
                {
                    #region 业务操作
                    //1、校验模板不能为空
                    FileUtils.ReadFile(templateFilePath, out _templateContent);

                    if (string.IsNullOrWhiteSpace(_templateContent))
                    {
                        _generateResult.Success = false;
                        _generateResult.Message = "Entity模板文件内容读取为空！";
                        return _generateResult;
                    }

                    StringBuilder _sbModel = new StringBuilder();

                    ///字段数据类型映射
                    foreach (var p in _dynamicColumns)
                    {
                        IDictionary<string, object> _dic = p as IDictionary<string, object>;

                        _sbModel.AppendLine(" /// <summary>");
                        _sbModel.AppendLine($"/// {_dic["COLUMN_COMMENT"]}");
                        _sbModel.AppendLine("/// </summary>");
                        //当前字段是不是主键
                        if (_dic["COLUMN_KEY"].Equals("PRI"))
                        {
                            _sbModel.AppendLine("[Key]");
                            _sbModel.AppendLine("[DatabaseGenerated(DatabaseGeneratedOption.Identity)]");
                        }
                        _sbModel.AppendLine($"[DisplayName(\"{_dic["COLUMN_COMMENT"]}\")]");
                        if (_dic["IS_NULLABLE"].Equals("NO"))
                        {
                            _sbModel.AppendLine("[Required]");
                            if (_dic["CHARACTER_MAXIMUM_LENGTH"] != null)
                            {
                                _sbModel.AppendLine($"[StringLength({_dic["CHARACTER_MAXIMUM_LENGTH"]}, ErrorMessage = \"{{0}}不能超过{_dic["CHARACTER_MAXIMUM_LENGTH"]}个字符！\")]");
                            }
                        }
                        _sbModel.AppendLine($"public {_dic["DATA_TYPEC"]} {_dic["COLUMN_NAME"]} {{get;set;}}");
                    }

                    //2、模板替换修改内容
                    _templateContent = _templateContent.Replace("<%=tableName%>", _entityName.ToString()).Replace("<%=modelInfo%>", _sbModel.ToString()).Replace("<%=tableDisplayName%>", _moduleName.ToString());
                    _sbModel.Clear();
                    _moduleName.Clear();
                    string _savePath = Path.Combine(saveDir, "Entity\\"); 
                    FileUtils.CreateDirectory(_savePath);
                    _savePath = _savePath + $"{_entityName}.cs";
                    if (FileUtils.IsExistFile(_savePath))
                    {
                        FileUtils.DeleteFile(_savePath);
                    }
                    string _error = string.Empty;
                    //3、写入文件
                    bool _isWriteSucces = FileUtils.CoverWriteFile(_savePath, _templateContent,out _error);
                    if (_isWriteSucces)
                    {
                        _generateResult.Success = true;
                        _generateResult.Message = $"创建{_entityName}代码成功.";
                        _generateResult.Content = _templateContent;
                    }
                    else
                    {
                        _generateResult.Success = false;
                        _generateResult.Message = $"创建{_entityName}失败，出错位置：写入文件.";
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    _generateResult.Success = false;
                    _generateResult.Message = $"创建{_entityName}代码失败." + ex.ToString();
                }
            }

            return _generateResult;
        }
    }
}
