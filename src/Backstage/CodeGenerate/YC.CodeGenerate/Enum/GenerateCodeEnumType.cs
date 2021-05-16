using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.CodeGenerate
{
    public enum GenerateCodeEnumType
    {
        #region 默认生成

        DefaultAppService,
        DefaultIAppService,
        DefaultAddOrEditDto,
        DefaultEntityDto,
        //前端vue 页面
        DefaultVuePage,

        #endregion

        #region 树型代码生成
        TreeAppService,
        TreeIAppService,
        TreeVuePage,
        #endregion

        #region 公共生成
        // 后端 maaping,前端页面 （路由配置、main.js 的api请求地址）
        OtherCode,
        #endregion


    }
}
