using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YC.CodeGenerate.Dto;

namespace YC.CodeGenerate
{
    public class GenerateCodeUtils
    {

        #region 公共方法

        /// <summary>
        /// 获取表显示的名称
        /// </summary>
        /// <param name="baseType"></param>
        /// <returns></returns>
        public static string GetTableDisplayName(Type baseType)
        {
            string moduleName = "";
            var displayAttrbute = baseType.CustomAttributes.Where(x => x.AttributeType.Equals(typeof(DisplayAttribute))).FirstOrDefault();
            //处理展示的名字
            if (displayAttrbute != null)
            {
                moduleName = ((System.ComponentModel.DataAnnotations.DisplayAttribute)Attribute.GetCustomAttribute(baseType, typeof(System.ComponentModel.DataAnnotations.DisplayAttribute))).Name;
                moduleName = moduleName.LastIndexOf("表") > 0 ? moduleName.Remove(moduleName.Length - 1, 1) : moduleName;
            }
            return moduleName;
        }


        /// <summary>
        /// 获取属性信息
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static PropertyInfoDto GetTypePropertyInfo(PropertyInfo p)
        {

            PropertyInfoDto propertyInfoDto = new PropertyInfoDto();
            if (p.IsDefined(typeof(System.ComponentModel.DataAnnotations.KeyAttribute), false))
            {
                propertyInfoDto.IsKeyWithIdType = p.PropertyType.Name;//Id类型
            }
            propertyInfoDto.DisPlayName = GetDisPlayName(p);
            propertyInfoDto.PropertyName = p.Name;

            propertyInfoDto.TypeName = p.PropertyType.Name;
            if (propertyInfoDto.TypeName.ToLower().Contains("Nullable`1".ToLower()))
            {
                propertyInfoDto.TypeName = $"Nullable<{p.PropertyType.GetProperties()[1].PropertyType.Name}>";
            }
            if (propertyInfoDto.TypeName.ToLower().Equals("string"))
            {
                var stringLengthAttr = p.GetCustomAttribute<System.ComponentModel.DataAnnotations.StringLengthAttribute>();
                if (stringLengthAttr != null)
                {
                    propertyInfoDto.StringTypeLength = stringLengthAttr.MaximumLength;
                }
                else
                {//没有标识 stringLength，默认赋予100
                    propertyInfoDto.StringTypeLength = 100;
                }
            }

            propertyInfoDto.InitialsToLowerPropertyName = p.Name.Replace(p.Name[0], p.Name[0].ToString().ToLower()[0]);

            return propertyInfoDto;
        }

        /// <summary>
        /// 获取对应的字段 display 注释
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string GetDisPlayName(PropertyInfo p)
        {
            string displayName = "";
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
            return displayName;
        }

        /// <summary>
        /// 驼峰法命名的首字母小写
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetHumpStr(string name)
        {
            var humpName = name.Replace(name[0], name[0].ToString().ToLower()[0]);//首字母小写
            return humpName;
        }
        #endregion
    }
}
