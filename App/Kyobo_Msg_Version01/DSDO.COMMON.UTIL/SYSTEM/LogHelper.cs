using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSDO.COMMON.UTIL.SYSTEM
{
    public sealed class LogHelper
    {
        public static bool isUsePrintLog = true;
        public static void log(string log)
        {
            if (!isUsePrintLog)
                return;
            string header = "Debug";
            string logMessage = String.Format("[{0}] log: {1}", header, log);
#if (UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID)
            Debug.Log(logMessage);
#else
            Console.WriteLine(logMessage);            
#endif
        }
        public static void log(string header, string log)
        {
            if (!isUsePrintLog)
                return;
            string logMessage = String.Format("[{0}] log: {1}", header, log);
#if (UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID)
            Debug.Log(logMessage);
#else
            Console.WriteLine(logMessage);
#endif
        }

        public static void PrintBytesLog(string header, byte[] bytes)
        {
            if (!isUsePrintLog)
                return;
            string logMessage = BytesToLogString(header, bytes);
#if (UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID)
            Debug.Log(logMessage);
#else
            Console.WriteLine(logMessage);
#endif
        }

        private static string BytesToLogString(string header, byte[] bytes)
        {
            var sb = new StringBuilder(String.Format("[{0}] byte: ", header));
            for (var i = 0; i < bytes.Length; i++)
            {
                var b = bytes[i];
                sb.Append(b);
                if (i < bytes.Length - 1)
                {
                    sb.Append(", ");
                }
            }
            return sb.ToString();
        }
    }
}
