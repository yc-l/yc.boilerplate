using System;
using System.Collections.Generic;
using System.Text;

namespace YC.DapperFrameWork
{
    public class DefaultUnitOfWorkManager : IDisposable
    {
      public  IUnitOfWork _unitOfWork = null;

        
        public void Dispose()
        {
            if(_unitOfWork!=null)
            _unitOfWork.Dispose();
        }
    }
}
