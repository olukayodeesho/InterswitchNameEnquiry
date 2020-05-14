using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;

namespace InterswitchNameEnquiry.Logic
{
    public class Utils
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static void LogError(Exception e, string message)
        {
            logger.Error( e, message);
        }

        public static void LogInfo(string message)
        {
            logger.Info(message);
        }
    }
}