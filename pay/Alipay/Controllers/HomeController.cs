using Alipay.Models;
using Aop.Api;
using Aop.Api.Domain;
using Aop.Api.Request;
using Aop.Api.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alipay.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DefaultAopClient client = new DefaultAopClient(AlipayConfig.gatewayUrl,
                AlipayConfig.app_id,
                AlipayConfig.private_key,
                "json", "1.0",
                AlipayConfig.sign_type,
                AlipayConfig.alipay_public_key,
                AlipayConfig.charset, false);
            // 外部订单号，商户网站订单系统中唯一的订单号
            string out_trade_no ="10000";

            // 订单名称
            string subject = "支付测试";

            // 付款金额
            string total_amout = "0.01";

            // 商品描述
            string body = "测试商品";

            // 组装业务参数model
            AlipayTradePagePayModel model = new AlipayTradePagePayModel();
            model.Body = body;
            model.Subject = subject;
            model.TotalAmount = total_amout;
            model.OutTradeNo = out_trade_no;
            model.ProductCode = "FAST_INSTANT_TRADE_PAY";

            AlipayTradePagePayRequest request = new AlipayTradePagePayRequest();
            // 设置同步回调地址
            request.SetReturnUrl("htttp://www.zzzz.com/pay/alipayreturn");
            // 设置异步通知接收地址
            request.SetNotifyUrl("htttp://www.zzzz.com/pay/alipaynotify");
            // 将业务model载入到request
            request.SetBizModel(model);
            AlipayTradePagePayResponse response = null;
            try
            {
                response = client.pageExecute(request, null, "post");
                Response.Write(response.Body);
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}