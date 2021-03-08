using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

[assembly:log4net.Config.XmlConfigurator(Watch=true) ]
namespace LMS.Common
{
    public class LogHelper
    {
        public static void WriteLog(Type type, Exception ex) {
            log4net.ILog  log= log4net.LogManager.GetLogger(type);
            log.Error("Error",ex);  
        }

        public static void WriteLog(Type type, string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(type);
            log.Error(msg);
        }

    }
}
