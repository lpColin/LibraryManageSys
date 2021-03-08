using LibraryManageSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace LibraryManageSys.DAL
{
    /// <summary>
    /// 简单工厂生产DbConext子类
    /// <remarks>创建：2014.8.27</remarks>
    /// </summary>
    public class ContextFactory
    {
        public static LMSEntitys GetCurrentContext() {
            LMSEntitys _dbContext = CallContext.GetData("LibraryManageSys") as LMSEntitys;
            if (_dbContext == null) {
                _dbContext = new LMSEntitys();
                CallContext.SetData("LibraryManageSys", _dbContext);
            }
            return _dbContext;        
        }
    }
}