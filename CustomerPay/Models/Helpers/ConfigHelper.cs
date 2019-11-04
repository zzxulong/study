using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerPay.Models.Helpers
{
    public class ConfigHelper
    {
        /// <summary>
        /// 获取AppSetting值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static string GetAppSetting (string key)
        {
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            if (value == null)
            {
                return string.Empty;
            }
            return value;
        }

        public static string GetAppSetting (string key, string defaultValue)
        {
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            if (value == null)
            {
                return defaultValue;
            }
            return value;
        }

        /// <summary>
        /// 获取AppSetting值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static int GetAppSettingInt (string key)
        {
            return int.Parse(GetAppSetting(key));
        }

        /// <summary>
        /// 获取AppSetting值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static int GetAppSettingInt (string key, int defaultValue)
        {
            int result = -1;
            if (!int.TryParse(GetAppSetting(key), out result))
            {
                result = defaultValue;
            }
            return result;
        }

        /// <summary>
        /// 获取AppSetting值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static bool GetAppSettingBoolean (string key, bool defaultVal)
        {
            var str = GetAppSetting(key);
            if (string.IsNullOrWhiteSpace(str))
            {
                return defaultVal;
            }

            bool result = defaultVal;
            bool.TryParse(str, out result);
            return result;
        }
        /// <summary>
        /// 获取AppSetting值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static decimal GetAppSettingDecimal (string key, decimal defaultValue)
        {
            decimal result = 0;
            if (!decimal.TryParse(GetAppSetting(key), out result))
            {
                result = defaultValue;
            }
            return result;
        }


        // <summary>
        /// 获取AppSetting值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static long GetAppSettingLong (string key)
        {
            return GetAppSettingLong(key, 0);
        }

        /// <summary>
        /// 获取AppSetting值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static long GetAppSettingLong (string key, long defaultValue)
        {
            long result = 0;
            if (!long.TryParse(GetAppSetting(key), out result))
            {
                result = defaultValue;
            }
            return result;
        }
    }
}