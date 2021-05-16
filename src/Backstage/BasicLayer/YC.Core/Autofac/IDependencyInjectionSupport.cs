using System;
using System.Collections.Generic;
using System.Text;

namespace YC.Core.Autofac
{
    /// <summary>
    /// 标示需要注入的就需要继承这个接口，用于反射时候过滤注入
    /// </summary>
    public interface IDependencyInjectionSupport
    {
    }
}
