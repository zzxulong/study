using CustomerPay.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerPay.Models
{
    public class AlipayConfig
    {
        #region 单例
        private static AlipayConfig instance = new AlipayConfig();
        public static AlipayConfig Instance { get { return instance; } }
        #endregion        

        #region  设置支付宝支付的配置信息
        /// <summary>
        /// 设置支付宝支付的配置信息
        /// </summary>
        public AlipayConfig ()
        {
            //合作身份者ID，以2088开头由16位纯数字组成的字符串
            Partner = ConfigHelper.GetAppSetting("AlipayPartner");
            //支付宝MD5私钥，如果签名方式设置为“MD5”时，请设置该参数
            Key = ConfigHelper.GetAppSetting("AlipayKey");
            //收款账号
            Seller_email = ConfigHelper.GetAppSetting("AlipaySeller_email");
            //商户的私钥
            Private_key = ConfigHelper.GetAppSetting("AlipayPrivate_key");
            //支付宝的公钥，无需修改该值
            Public_key = ConfigHelper.GetAppSetting("AlipayPublic_key");
            //字符编码格式 目前支持 gbk 或 utf-8
            Input_charset = ConfigHelper.GetAppSetting("AlipayInput_charset");
            //签名方式，选择项：RSA、DSA、MD5
            Sign_type = ConfigHelper.GetAppSetting("AlipaySign_type");
            //异步通知地址
            Notify_url = ConfigHelper.GetAppSetting("AlipayNotifyUrl");
            //同步返回地址
            Return_url = ConfigHelper.GetAppSetting("AlipayReturnUrl");
            //支付取消跳转地址
            Show_url = ConfigHelper.GetAppSetting("AlipayShowUrl");
            //退款异步通知地址
            RefundNotify_url = ConfigHelper.GetAppSetting("AlipayRefundNotifyUrl");
        }

        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置合作者身份ID
        /// </summary>
        public string Partner { get; set; }
        /// <summary>
        /// 收款账户
        /// </summary>
        public string Seller_email { get; set; }
        /// <summary>
        /// 支付宝MD5私钥
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 获取或设置商户的私钥
        /// </summary>
        public string Private_key { get; set; }
        /// <summary>
        /// 获取或设置支付宝的公钥
        /// </summary>
        public string Public_key { get; set; }
        /// <summary>
        /// 获取字符编码格式
        /// </summary>
        public string Input_charset { get; set; }
        /// <summary>
        /// 获取签名方式
        /// </summary>
        public string Sign_type { get; set; }
        /// <summary>
        /// 异步回调地址
        /// </summary>
        public string Notify_url { get; set; }
        /// <summary>
        /// 同步返回地址
        /// </summary>
        public string Return_url { get; set; }
        /// <summary>
        /// 支付中断(取消)
        /// </summary>
        public string Show_url { get; set; }
        /// <summary>
        /// 退款异步通知地址
        /// </summary>
        public string RefundNotify_url { get; set; }
        #endregion
    }
}