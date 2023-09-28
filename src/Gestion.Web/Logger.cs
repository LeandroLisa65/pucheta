using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;

namespace Gestion.Web
{
    public static class Logger
    {
        private static void SetLog4NetConfiguration()
        {
            var repo = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(repo, new FileInfo("log4net.config"));
        }
        public static void Debug(object message)
        {
            SetLog4NetConfiguration();

            object fMensaje =  message;

            var _logger = LogManager.GetLogger(typeof(Program));
            _logger.Debug(fMensaje);
        }

        public static void Error(object message)
        {
            SetLog4NetConfiguration();

            object fMensaje = message;

            var _logger = LogManager.GetLogger(typeof(Program));
            _logger.Error(fMensaje);
        }


    }
}
