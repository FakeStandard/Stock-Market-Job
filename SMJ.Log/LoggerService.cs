using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMJ.Log
{
    /// <summary>
    /// NLog Service
    /// 外部參考必須安裝 NLog.Schema & NLog.Config 這兩個 NuGet 套件
    /// </summary>
    public class LoggerService
    {
        private static Logger CurrentLogger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 寫入基本資訊
        /// </summary>
        public static void WriteInfo(string info)
        {
            CurrentLogger.Info($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}\t{info} 寫入成功.");
        }

        /// <summary>
        /// 寫入錯誤訊息
        /// </summary>
        /// <param name="error"></param>
        public static void WriteError(string error)
        {
            CurrentLogger.Error($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}\t{error}");
        }
    }
}
