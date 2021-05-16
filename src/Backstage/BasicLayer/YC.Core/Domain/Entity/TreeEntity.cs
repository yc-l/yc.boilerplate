using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.Core.Domain.Entity
{
    /// <summary>
    /// 树形的不要使用这边泛型，内部逻辑处理没有问题，但是到了json 映射可能造成死循环，应该是json组件等冲突，无法解析泛型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TreeEntity<T>
    {

        public T TObject { get; set; }

        public List<TreeEntity<T>> Childrens { get; set; }
    }
}
