using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace YC.Model.DbEntity
{
   public class BaseEntity<T> 
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("主键Id")]
        public T Id { get; set; }
        [DisplayName("是否启用标识")]
        public bool? IsActive { get; set; }
        [DisplayName(" 是否删除标识")]
        public bool? IsDeleted { get; set; }

        [DisplayName("租户名")]
        public virtual int? TenantId { get; set; }

        #region 拓展
        static readonly string DefaultKeyName = "Id";
        private static Dictionary<string, Dictionary<string, List<PropertyInfo>>> g_TableSpecialFields = new Dictionary<string, Dictionary<string, List<PropertyInfo>>>();
        private static Dictionary<string, string> g_Tables = new Dictionary<string, string>();
        private static Object fieldsLock = new Object();
        private static Object tableLock = new Object();
        [Key]
       

        [NotMapped, JsonIgnore]
        public string Table
        {
            get
            {
                return GetTableName(this.GetType());
            }
        }

        [NotMapped, JsonIgnore]
        public PropertyInfo KeyField
        {
            get
            {
                var specialFields = GetTableSpecialFields(this.GetType(), typeof(KeyAttribute));
                return specialFields.Count > 0 ? specialFields.First() : this.GetType().GetProperty(DefaultKeyName);
            }
        }



        public string DisplayName(string field)
        {
            MetadataTypeAttribute metadataTypeAttr = this.GetType().GetCustomAttribute(typeof(MetadataTypeAttribute)) as MetadataTypeAttribute;
            if (metadataTypeAttr != null)
            {
                PropertyInfo metadataTypeFieldProperty = metadataTypeAttr.MetadataClassType.GetProperty(field);
                if (metadataTypeFieldProperty != null)
                {
                    DisplayAttribute da = metadataTypeFieldProperty.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
                    if (da != null)
                    {
                        return da.Name;
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <param name="attrType">Attribute类型</param>
        /// <param name="fromMetadata">是否MetaData</param>
        /// <param name="entityProperty">返回是否Metadata Property</param>
        /// <returns></returns>
        private static List<PropertyInfo> GetTableSpecialFields(Type entityType, Type attrType, bool findFromMetadata = true, bool entityProperty = true)
        {
            lock (fieldsLock)
            {
                string table = GetTableName(entityType);
                if (!g_TableSpecialFields.ContainsKey(table))
                {
                    g_TableSpecialFields.Add(table, new Dictionary<string, List<PropertyInfo>>());
                }
                var specialAttributes = g_TableSpecialFields[table];
                if (!specialAttributes.ContainsKey(attrType.Name))
                {
                    specialAttributes.Add(attrType.Name, new List<PropertyInfo>());
                    //Metadata特性，目前除了外键，其他都是在Metadata定义
                    if (findFromMetadata)
                    {
                        MetadataTypeAttribute metadataTypeAttr = entityType.GetCustomAttribute(typeof(MetadataTypeAttribute)) as MetadataTypeAttribute;
                        if (metadataTypeAttr != null)
                        {
                            PropertyInfo[] properties = metadataTypeAttr.MetadataClassType.GetProperties();
                            foreach (PropertyInfo pi in properties)
                            {
                                Attribute attr = pi.GetCustomAttribute(attrType);
                                if (attr != null)//Model找不到，就查找Metadata,返回的还是Model的property
                                {
                                    PropertyInfo piReturn = entityProperty ? entityType.GetProperty(pi.Name) : pi;
                                    specialAttributes[attrType.Name].Add(piReturn);
                                }
                            }
                        }
                    }
                    else
                    {
                        PropertyInfo[] properties = entityType.GetProperties();
                        foreach (PropertyInfo pi in properties)
                        {
                            Attribute attr = pi.GetCustomAttribute(attrType);
                            if (attr != null)//Model找不到，就查找Metadata
                            {
                                specialAttributes[attrType.Name].Add(pi);
                            }
                        }
                    }
                }
                return specialAttributes[attrType.Name];
            }
        }

        public static string GetTableName(Type entityType)
        {
            lock (tableLock)
            {
                string typeName = entityType.Name;
                if (!g_Tables.ContainsKey(typeName))
                {
                    Attribute attr = entityType.GetCustomAttribute(typeof(TableAttribute));
                    if (attr != null)
                    {
                        g_Tables.Add(typeName, (attr as TableAttribute).Name);
                    }
                    else
                    {
                        g_Tables.Add(typeName, typeName);
                    }
                }
                return g_Tables[typeName];
            }
        }

        public static string GetKeyName(Type entityType)
        {
            var specialFields = GetTableSpecialFields(entityType, typeof(KeyAttribute));
            return specialFields.Count > 0 ? specialFields.First().Name : DefaultKeyName;
        }


        public string GetEnumName(Enum value)
        {
            if (value != null)
            {
                object[] attr = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DisplayAttribute), false);
                if (attr.Length > 0)
                {
                    return (attr[0] as DisplayAttribute).Name;
                }
            }
            return string.Empty;
        }
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]  --计算字段

       

        #endregion

    }
}
