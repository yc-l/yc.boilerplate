using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.CodeGenerate
{
    public class VueTemplateCodesnippetConfig
    {
        /// <summary>
        /// 表格 列
        /// </summary>
        public static string TableColumn = "<el-table-column prop='{{keyName}}' label='{{displayName}}'></el-table-column>";
        /// <summary>
        /// 树形表格 列
        /// </summary>
        public static string TreeTableColumn = " {label: '{{displayName}}',minWidth: '100px',prop: '{{keyName}}'},";

        /// <summary>
        /// 表单两列
        /// </summary>         
        public static string FormRow
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<el-row :gutter='20'>");
                sb.AppendLine("{{RowData}}");
                sb.AppendLine("</el-row>");
                return sb.ToString();
            }
        }

        /// <summary>
        /// 添加或编辑表单参数
        /// </summary>

        public static string AddOrEditFormParam(string typeName)
        {
            string temp = "";
            if (typeName.ToLower().Contains("int") || typeName.ToLower().Contains("long"))
            {
                temp = "{{propertyName}}: 0,";
            }
            else if (typeName.ToLower().Contains("bool"))
            {
                temp = "{{propertyName}}: true,";
            }
            else
            {
                temp = "{{propertyName}}: '',";
            }
            return temp;
        }


        #region 表单验证规则
        public static string AddOrEditFormParamRules => "{{propertyName}}:[{{validate}}],";
        public static string AddOrEditFormParamRules_Require => "{ required: false, message: '请输入{{displayName}}', trigger: 'blur' },";
        public static string AddOrEditFormParamRules_StringMax => " { max: {{propertyLength}},message: '{{displayName}}的长度在{{propertyLength}}字符之内',trigger: 'blur'},";
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowColCount">是否是两列模式，不是就是一行，一个单元</param>
        /// <returns></returns>
        public static string GetFormRowColumn(bool twoColType = true)
        {
            StringBuilder sb = new StringBuilder();

            if (twoColType)//两列模式，
            {
                sb.AppendLine("<el-col :span='12'>");
            }
            else
            {
                sb.AppendLine("<el-col :span='24'>");
            }
            sb.AppendLine("<el-form-item label='{{columnLable}}：' prop='{{columnProp}}'>");
            sb.AppendLine("<el-input v-model='addOrEditForm.{{columnProp}}'></el-input>");
            sb.AppendLine("</el-form-item>");
            sb.AppendLine("</el-col>");

            return sb.ToString();
        }

        /// <summary>
        /// main.js 的api请求接口
        /// </summary>
        /// <returns></returns>
        public static string GetMainjsApiInfo(bool isTree = false)
        {

            StringBuilder sb = new StringBuilder();
            if (isTree)
            {
                sb.AppendLine("Vue.prototype.${{propertyName}}ManagerUrl='{{tableName}}/Get{{tableName}}List'");
                sb.AppendLine("Vue.prototype.${{propertyName}}Manager_Get{{tableName}}FilterByPidUrl='{{tableName}}/Get{{tableName}}FilterByPid'");
            }
            else
            {
                sb.AppendLine("Vue.prototype.${{propertyName}}ManagerUrl='{{tableName}}/GetPage{{tableName}}List'");
            }

            sb.AppendLine("Vue.prototype.${{propertyName}}Manager_Get{{tableName}}Url='{{tableName}}/Get'");
            sb.AppendLine("Vue.prototype.${{propertyName}}Manager_Create{{tableName}}Url='{{tableName}}/Create{{tableName}}'");
            sb.AppendLine("Vue.prototype.${{propertyName}}Manager_Edit{{tableName}}Url ='{{tableName}}/Update{{tableName}}'");
            sb.AppendLine("Vue.prototype.${{propertyName}}Manager_Delete{{tableName}}Url='{{tableName}}/Delete{{tableName}}ById'");
            return sb.ToString();
        }

        /// <summary>
        /// 权限配置
        /// </summary>
        /// <returns></returns>
        public static string GetPermissionInfo() {

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("查看{{moduleName}}列表：" + "{{propertyName}}Manager.view{{tableName}},  "+ "对应需要配置的权限api：{{tableName}}/GetPage{{tableName}}List,{{tableName}}/Get");
            sb.AppendLine("新增{{moduleName}}：" + "{{propertyName}}Manager.create{{tableName}}," + "对应需要配置的权限api：{{tableName}}/Create{{tableName}}");
            sb.AppendLine("编辑{{moduleName}}：" + "{{propertyName}}Manager.edit{{tableName}}," + "对应需要配置的权限api：{{tableName}}/Update{{tableName}}");
            sb.AppendLine("删除{{moduleName}}：" + "{{propertyName}}Manager.delete{{tableName}}," + "对应需要配置的权限api：{{tableName}}/Delete{{tableName}}ById");
            return sb.ToString();
        }


        /// <summary>
        /// 路由配置，需要配置在home 的children 下面
        /// </summary>
        public static string GetRouterInfo
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("import {{tableName}}s from '../view/{{tableName}}/{{tableName}}s'");
                sb.AppendLine("{ path: '/{{propertyName}}s', component: {{tableName}}s }");
                return sb.ToString();
            }
        }

        /// <summary>
        /// 前端 Home.vue 的菜单目录
        /// </summary>

        public static string GetMenuInfo
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<el-menu-item index='/{{propertyName}}s'>");
                sb.AppendLine("<i class='el-icon-s-operation'></i>");
                sb.AppendLine("<span slot='title'>{{tableDisplayName}}</span>");
                sb.AppendLine("</el-menu-item>");
              
                return sb.ToString();
            }
        }
    }
}
