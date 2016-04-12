using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using log4net.Appender;
using log4net.Repository.Hierarchy;

namespace Ises.Core.Common
{
    public class Utils
    {
        public static string ToQueryString(NameValueCollection nvc)
        {
            var array = (from key in nvc.AllKeys
                         from value in nvc.GetValues(key)
                         select String.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value)))
                .ToArray();
            return "?" + String.Join("&", array);
        }

        public static async Task RunSafe(Func<Task> func)
        {
            try
            {
                await func();
            }
            catch (AggregateException ex)
            {
                ApplicationContext.Logger.ErrorFormat("There was an error executing Action '{0}'. Message: {1}", func.Method.Name, ex.Message);
            }
            catch (Exception ex)
            {
                ApplicationContext.Logger.ErrorFormat("There was an error executing Action '{0}'. Message: {1}", func.Method.Name, ex.Message);
            }
        }

        public static void RunSafe(Action func)
        {
            try
            {
                func();
            }
            catch (AggregateException ex)
            {
                ApplicationContext.Logger.ErrorFormat("There was an error executing Action '{0}'. Message: {1}", func.Method.Name, ex.Message);
            }
            catch (Exception ex)
            {
                ApplicationContext.Logger.ErrorFormat("There was an error executing Action '{0}'. Message: {1}", func.Method.Name, ex.Message);
            }
        }

        public static string GetHostVersion()
        {
            return FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
        }

        public static void LogError(string errorMessage)
        {
            ApplicationContext.Logger.ErrorFormat(errorMessage);
        }

        public static void LogError(Exception ex)
        {
            ApplicationContext.Logger.ErrorFormat("{0}, Stack Trace:{1}", ex.GetExceptionMessages(), ex.StackTrace);
        }

        public static async Task<string> GetLoggerContent()
        {
            var content = String.Empty;
            var logger = ApplicationContext.Logger.Logger as Logger;

            return await GetLogContent(logger, content);
        }

        static async Task<string> GetLogContent(Logger logger, string content)
        {
            if (logger == null) return content;

            var appender = logger.Appenders.OfType<FileAppender>().FirstOrDefault() ??
                           logger.Hierarchy.Root.Appenders.OfType<FileAppender>().FirstOrDefault();

            content = await ReadLogFile(appender);
            return content;
        }

        static async Task<string> ReadLogFile(FileAppender appender)
        {
            var content = String.Empty;
            if (appender == null) return content;

            using (var stream = File.Open(appender.File, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var sr = new StreamReader(stream))
                {
                    content = await sr.ReadToEndAsync();
                }
            }
            return content;
        }
    }
}
