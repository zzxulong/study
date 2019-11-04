using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using CustomerPay.Models;
using CustomerPay.Models.Helpers;
using GuangYi.WeiXin.MP.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerPay.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index (string id)
        {
            ViewBag.ID = id;
            return View();
        }

        public ActionResult GetCustomerFile (string id)
        {
            return File(QrCodeHelper.GenQrCode("http://www.xumingzhen.com/Home/Test/"+id, 200).ToArray(), @"image/jpeg");
        }

        public ActionResult Test ()
        {
            var userAgent=HttpContext.Request.UserAgent;
            if (userAgent.Contains("Alipay"))
            {
                IAopClient client = new DefaultAopClient("https://openapi.alipay.com/gateway.do",
            "2019082766434970",
                "MIIEpQIBAAKCAQEAsdm9hrmwgGt3FsiUFDr4wZMWMxLXt5mvJUNBMJacYyxbkCkXu+PNk+1ItN6EFdbIjoqLRlKXT4nGZmcSs7RhbWlDSG4Qd/PaAVZmtjEKiNEwCBIoQ2LyhCGuD5c6bA2K1e/HkgEhXnHPDlCJXf0BrkavPaOHYxG3iCQEXRDxGfDjaK3DSu1vPLEqxsMqPDieTDc81pXGQZeH7cW5iGIo4gX0ac+Bmc9vRz5uUbsfkitTvyx49tO9ROGp5XhlrmqMuslFbYpvx8KNcCr+eVVcaHPOpUXRCHXECBpF+jD0ZvMRg5f43dTaOiXtSdMayfeV2BVSN5vkPYUoNPp8amoVFQIDAQABAoIBAATWrrBWpvWndtL43AZ84D2r+cOj9Jh7Jue1n7Vps5GDKG6Qn6Ot++7VWtAI6cEKWeR+4nEkWBeC4RHyJfEbrDbk0T7MN8h7que0aRD13pqATnU5wTysGXr8y5aC/8TmzWsqndxcCYNfR0nVaRdUhdIu/B+KLmnElqCukivSBS02c9o39nNY2WsxTVabKUaoso7IBmM8dFdEBY7AjlEJQfPBUQldTllm8uRXcKItaHh7/WYQ4MeAf1612eao5LKVR9N+XkDEIz+wys48W58bT/McruvIPjLI3S6MLG6loo34czH7YDbLLtarAUaPGs6YFTRXE2s0+eNrCWHRRrMdmAECgYEA7tmsESQwaEOhu4xPYK4qYqVFVSV8Ve2NP6hvr9fQBlgilshBpVwR0MX41TlFG76qdkBSStky333AoNIf2OzjpZNBA0rAVfnHY3tqJwzeJfsOYUmniY6nepBdorgh94b8DdaeurEre7vwqvFMTBGwmDsR4oIOidY49UiSQ8d+ggECgYEAvp7Tp6ApmyO+PI7yujfE1VsNTeHLiVnf87xYnMW87EOjyl2XqiycspBxiVtcy7PZnQVYaFqcAMf1cYb0ti6BiCNXMOMg0eWFpUknYHllsB7pXu7n8Aheu0QkKRlFzkTBlFcs5yRG7V/vfP0DPL53jI54xj8LHS69FIF9ikazaxUCgYEA7hpogZ4F86qYLBKvfr2g2z3OZBc+X5q0WGVdEXqekfSCZrWzW1jxGaKBcG3rCsGDxOZSIvNq14jQ4oEV5nYgagrloJaALjUWI4IATHVzb4Fa0HlQXryrEG+iKHhOxCb4VgNBsPK1Wl/8hyyM1rg7x0X7ikYEoDvCBCMYTCEvaAECgYEAsaCzPoqi+qxOA4R5b0m0o9RbC24TCoSzih7CvCQPLmLBaDvarnsv8b1j70kxqaiIaienAGce5CNw9j96HaIslj3IbAfad31YEnOeu+mj9oB3mz0o6KYEEKEoyRuBzvqHIMIzV5iYd25kbqYetCYQpOvGY4YvxxvcmhUodQf4/RkCgYEA2SM3mgOz+jaBs96JVtTeLeEFIaKmnceD8u+wuuVfulY2aDDkEwYPfHXBqu4EqZjpcDg2l2EBsLRy7Ot7K2EaGJV0s7H1AP/13kziKCl9WwDoDg4Dk7ZRzj378YFehX4b9T3SQ36WMkgFHB9zOcRP5Rf7HO4inN6UqILHSTuTOY4=",
                "json",
                "1.0",
                "RSA2",
                "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA05z4aIvz0V9DyE/YS72CuCVbb9ncn5EJHGijdQ+qokDXR1e0SNIbZeitNKUL1bfqapjYIDIPON49VHtPFBWz2osSITsv9+lIQUhb/nBvTAhLL2Yebx6/NPC3a+meqm5e53Fu8W6WYvpQGI7zcGMiJaDs19xZHyTGJf0dTVn7+r2/XWA37801W8rADnikIyKm4gu1AiWWtpuVy+bimc6fcFq5dHOnHGlcK+kx+Bm4yRN3aDE8kG2EszLV850KaZwvHlUGdiW3mVixpfbQJV8ei2n0HIwLVGVvt1hjwNGgvrNAuBBc2ACAkk45gg77+pTiHR1XzxyjiXEelGUfFN09wQIDAQAB",
                "GBK", false);
                AlipayTradePrecreateRequest  request= new AlipayTradePrecreateRequest();
                var rand=this.GetRandomMumber(17);
                request.BizContent = "{" +
    "\"out_trade_no\":\"" + (201523423420101001 + long.Parse(rand)) + "\"," +
        "\"seller_id\":\"2088631146114221\"," +
    "\"total_amount\":0.1," +
    "\"subject\":\"Iphone6 16G\"," +
    "\"timeout_express\":\"90m\"," +
    "\"qr_code_timeout_express\":\"90m\"" +
    "  }";
                AlipayTradePrecreateResponse response=client.Execute(request);
                ViewBag.Src = response.QrCode;
                return Redirect(response.QrCode);
            }
            else if(userAgent.Contains("MicroMessenger"))
            {
                return Redirect("/Home/About");
            }
            return Content("1");
        }

        public ActionResult GetFile ()
        {
            IAopClient client = new DefaultAopClient("https://openapi.alipay.com/gateway.do",
            "2019082766434970",
                "MIIEpQIBAAKCAQEAsdm9hrmwgGt3FsiUFDr4wZMWMxLXt5mvJUNBMJacYyxbkCkXu+PNk+1ItN6EFdbIjoqLRlKXT4nGZmcSs7RhbWlDSG4Qd/PaAVZmtjEKiNEwCBIoQ2LyhCGuD5c6bA2K1e/HkgEhXnHPDlCJXf0BrkavPaOHYxG3iCQEXRDxGfDjaK3DSu1vPLEqxsMqPDieTDc81pXGQZeH7cW5iGIo4gX0ac+Bmc9vRz5uUbsfkitTvyx49tO9ROGp5XhlrmqMuslFbYpvx8KNcCr+eVVcaHPOpUXRCHXECBpF+jD0ZvMRg5f43dTaOiXtSdMayfeV2BVSN5vkPYUoNPp8amoVFQIDAQABAoIBAATWrrBWpvWndtL43AZ84D2r+cOj9Jh7Jue1n7Vps5GDKG6Qn6Ot++7VWtAI6cEKWeR+4nEkWBeC4RHyJfEbrDbk0T7MN8h7que0aRD13pqATnU5wTysGXr8y5aC/8TmzWsqndxcCYNfR0nVaRdUhdIu/B+KLmnElqCukivSBS02c9o39nNY2WsxTVabKUaoso7IBmM8dFdEBY7AjlEJQfPBUQldTllm8uRXcKItaHh7/WYQ4MeAf1612eao5LKVR9N+XkDEIz+wys48W58bT/McruvIPjLI3S6MLG6loo34czH7YDbLLtarAUaPGs6YFTRXE2s0+eNrCWHRRrMdmAECgYEA7tmsESQwaEOhu4xPYK4qYqVFVSV8Ve2NP6hvr9fQBlgilshBpVwR0MX41TlFG76qdkBSStky333AoNIf2OzjpZNBA0rAVfnHY3tqJwzeJfsOYUmniY6nepBdorgh94b8DdaeurEre7vwqvFMTBGwmDsR4oIOidY49UiSQ8d+ggECgYEAvp7Tp6ApmyO+PI7yujfE1VsNTeHLiVnf87xYnMW87EOjyl2XqiycspBxiVtcy7PZnQVYaFqcAMf1cYb0ti6BiCNXMOMg0eWFpUknYHllsB7pXu7n8Aheu0QkKRlFzkTBlFcs5yRG7V/vfP0DPL53jI54xj8LHS69FIF9ikazaxUCgYEA7hpogZ4F86qYLBKvfr2g2z3OZBc+X5q0WGVdEXqekfSCZrWzW1jxGaKBcG3rCsGDxOZSIvNq14jQ4oEV5nYgagrloJaALjUWI4IATHVzb4Fa0HlQXryrEG+iKHhOxCb4VgNBsPK1Wl/8hyyM1rg7x0X7ikYEoDvCBCMYTCEvaAECgYEAsaCzPoqi+qxOA4R5b0m0o9RbC24TCoSzih7CvCQPLmLBaDvarnsv8b1j70kxqaiIaienAGce5CNw9j96HaIslj3IbAfad31YEnOeu+mj9oB3mz0o6KYEEKEoyRuBzvqHIMIzV5iYd25kbqYetCYQpOvGY4YvxxvcmhUodQf4/RkCgYEA2SM3mgOz+jaBs96JVtTeLeEFIaKmnceD8u+wuuVfulY2aDDkEwYPfHXBqu4EqZjpcDg2l2EBsLRy7Ot7K2EaGJV0s7H1AP/13kziKCl9WwDoDg4Dk7ZRzj378YFehX4b9T3SQ36WMkgFHB9zOcRP5Rf7HO4inN6UqILHSTuTOY4=", 
                "json", 
                "1.0", 
                "RSA2",
                "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA05z4aIvz0V9DyE/YS72CuCVbb9ncn5EJHGijdQ+qokDXR1e0SNIbZeitNKUL1bfqapjYIDIPON49VHtPFBWz2osSITsv9+lIQUhb/nBvTAhLL2Yebx6/NPC3a+meqm5e53Fu8W6WYvpQGI7zcGMiJaDs19xZHyTGJf0dTVn7+r2/XWA37801W8rADnikIyKm4gu1AiWWtpuVy+bimc6fcFq5dHOnHGlcK+kx+Bm4yRN3aDE8kG2EszLV850KaZwvHlUGdiW3mVixpfbQJV8ei2n0HIwLVGVvt1hjwNGgvrNAuBBc2ACAkk45gg77+pTiHR1XzxyjiXEelGUfFN09wQIDAQAB",
                "GBK", false);
            AlipayTradePrecreateRequest  request= new AlipayTradePrecreateRequest();
            var rand=this.GetRandomMumber(17);
            request.BizContent = "{" +
"\"out_trade_no\":\""+ (201523423420101001+long.Parse(rand)) + "\"," +
"\"seller_id\":\"2088631146114221\"," +
"\"total_amount\":0.1," +
"\"subject\":\"Iphone6 16G\"," +
"\"timeout_express\":\"90m\"," +
"\"qr_code_timeout_express\":\"90m\"" +
"  }";
            AlipayTradePrecreateResponse response=client.Execute(request);
            ViewBag.Src = response.QrCode;
            return File(QrCodeHelper.GenQrCode(response.QrCode, 200).ToArray(), @"image/jpeg");
        }

        public ActionResult GetFileTest ()
        {
            IAopClient client = new DefaultAopClient("https://openapi.alipaydev.com/gateway.do",
                "2016101300673467",
                "MIIEogIBAAKCAQEAgV46jh5KmqbMbOFuFBQxnN6f0OngDpSr+Jkphwd7L6zC7N4yB0vF3EoLj/ahrgCEwTFy6oJz+tRif0hlt41KSMJCgJhiM/axrzyzDL7kGF0/ISwy5fZec8n5TGoS5cwNQ5koPO+ZsqGWd8rB7xIgpZ12APCwghUT1yDvxrAUEj78KBUhZfZTxRPZI5coat/fnVkC0EUR1DNzRjps3mDjleLb4g9fRgAxr/y8aUViOcnDZajZyA9RqZtRPWJOk80+rDGXV2/Z7r0EFokDe448PiV0bOK4Ggmmz+ADAsOvrZoGVUcfqo+1jr0KLOOSKdp9Bs6ft84tO3QMGMgFOqH8vwIDAQABAoIBABWd8DzlNOQosQoUlTdHtI6Y6K2ZFvZ4w0+0ECtfBaeOv7ei3Us4zyQjP1FcDeIsdgJuR9yF1el46ciJCW4lbt8/jqs8S1SRiVlV1DlyAF1N34mkgBDLHTdN7ZlmNCgv85tzETU1eEJQG/uCZuuS5N+nsxsvHN9vtDQDbe2OaJwvqmgxjqHvCLbP9v9U41aGyCveYFmnfEM/+2xufRMt9BzpE867ujmFOKyfqRj8fRHER2dlAGpLz9G2gOL5Eo0ve8KVxfFU3U93HRMCp6Yso0haM7nE0aNgYgl+gWMK+WdPOe/6f5MLVRyn9ePJWt2BIeEq2moZuuosE7B/QDID4PkCgYEA2Hqw4QU6/Z/u4Wl7cwgkdBTtZyGpXwCwjsnrFBqKHlJUowyl9HRqG1o7mqbGhS3iOc2UHA+BhNpRxYy0nl4vQUYzWWfkhq/PvYnuD2TzB3ybGd04iu/mY8hzM9VlsT4ojA3uL3QkO6I1pkLh9OyFglctvIs8KDJo4TP7a5T/6UUCgYEAmPxWCttT7O3vjlZQqjOr854l5Kn1xYKxF8PGaqfzHq79vHkhfNYiY/8cqKcJlJh8F+uG5Q6lRIiihwDuo+45MKmKL9ZHQpgBk9SKztpbflK/BR6UAXH/aloMr7j3IBFoJP7AiBpMtWsW6VTtJOwS9DoehkCN4bEZniDVF9o6tDMCgYBxA6xv7TEc02/8kfr21NtjFsHRw4sOo+eIw/VL64cLyWOSM7o34sa6POM9G7AsOwYNszuNYrL1Tpy+C3UH8Jeu9h08obZ2cjZ79xQnSUZvNCgs2ob4UcHveehauR975r14UJV2hKWd1FM511TrbsdMgoLIGdBwE9EZMPAi/AiWGQKBgHyNE9AK+KzwmhQcGdg38UqJ+vfLl5iR6nQao7O5wKm+dj23sJfeDmmzDTYRqC8eZglja5OY9NxzET8wdxlnnZAMt8Byh9mjv8UHSWtXQx35aCNW3dC/at6/KYz23dtx0IQuE7m7D7Cz2xdkWR2yRUwo9Sj/7YNNmDmxKUweE6UZAoGAPAkO10ddoGpNZ1VsIQPfYx/zn9UpIvax9HDkMYkAxMQdmG3h/yLH3x4c6QkytQE2VcLafQRAKc9il9WlgzUoOrzvO3W95XIck5dq16Re5ZZJCzc4jYnUmJ79AG0gJ3GDRBRWFpp/AxJWlMx7nMyNchj9kGQghgdVehWdV/eHmGM=",
                "json",
                "1.0",
                "RSA2",
                "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA4PO7TRZraDCfHVc+xh6xO3pqthcju3GEd/V0bI+v+GU7BWjdNfQhZmqgCVmGFERf8CQFtX5IQk1+FlH3kwimwyl/TM0R+GwJxl8mIkBn6OSCR+WLMfhiYR9J/vCmUlzur9ByB2VuQk/QWAKFMsVUwhUaWNPWxCEN4nBlmgiss8vDgm+npNBfUJoiT/d/0SW2/jJEzFNWjT0P+MB/BKFCXHlW+2eU12uUZHblZ2yw0pDVnP6jUVd5Y4wrtb4TKrLWxgnCaiki4AsuD9AjTMD4kQ33+Dln9JYCyEyhLRDLWldJs9dw+arL95IkUzvfhAf1CWZo0TYBwlBUom6wmzFbewIDAQAB",
                "GBK", false);
            //"2019082766434970",
            //    "MIIEpQIBAAKCAQEAsdm9hrmwgGt3FsiUFDr4wZMWMxLXt5mvJUNBMJacYyxbkCkXu+PNk+1ItN6EFdbIjoqLRlKXT4nGZmcSs7RhbWlDSG4Qd/PaAVZmtjEKiNEwCBIoQ2LyhCGuD5c6bA2K1e/HkgEhXnHPDlCJXf0BrkavPaOHYxG3iCQEXRDxGfDjaK3DSu1vPLEqxsMqPDieTDc81pXGQZeH7cW5iGIo4gX0ac+Bmc9vRz5uUbsfkitTvyx49tO9ROGp5XhlrmqMuslFbYpvx8KNcCr+eVVcaHPOpUXRCHXECBpF+jD0ZvMRg5f43dTaOiXtSdMayfeV2BVSN5vkPYUoNPp8amoVFQIDAQABAoIBAATWrrBWpvWndtL43AZ84D2r+cOj9Jh7Jue1n7Vps5GDKG6Qn6Ot++7VWtAI6cEKWeR+4nEkWBeC4RHyJfEbrDbk0T7MN8h7que0aRD13pqATnU5wTysGXr8y5aC/8TmzWsqndxcCYNfR0nVaRdUhdIu/B+KLmnElqCukivSBS02c9o39nNY2WsxTVabKUaoso7IBmM8dFdEBY7AjlEJQfPBUQldTllm8uRXcKItaHh7/WYQ4MeAf1612eao5LKVR9N+XkDEIz+wys48W58bT/McruvIPjLI3S6MLG6loo34czH7YDbLLtarAUaPGs6YFTRXE2s0+eNrCWHRRrMdmAECgYEA7tmsESQwaEOhu4xPYK4qYqVFVSV8Ve2NP6hvr9fQBlgilshBpVwR0MX41TlFG76qdkBSStky333AoNIf2OzjpZNBA0rAVfnHY3tqJwzeJfsOYUmniY6nepBdorgh94b8DdaeurEre7vwqvFMTBGwmDsR4oIOidY49UiSQ8d+ggECgYEAvp7Tp6ApmyO+PI7yujfE1VsNTeHLiVnf87xYnMW87EOjyl2XqiycspBxiVtcy7PZnQVYaFqcAMf1cYb0ti6BiCNXMOMg0eWFpUknYHllsB7pXu7n8Aheu0QkKRlFzkTBlFcs5yRG7V/vfP0DPL53jI54xj8LHS69FIF9ikazaxUCgYEA7hpogZ4F86qYLBKvfr2g2z3OZBc+X5q0WGVdEXqekfSCZrWzW1jxGaKBcG3rCsGDxOZSIvNq14jQ4oEV5nYgagrloJaALjUWI4IATHVzb4Fa0HlQXryrEG+iKHhOxCb4VgNBsPK1Wl/8hyyM1rg7x0X7ikYEoDvCBCMYTCEvaAECgYEAsaCzPoqi+qxOA4R5b0m0o9RbC24TCoSzih7CvCQPLmLBaDvarnsv8b1j70kxqaiIaienAGce5CNw9j96HaIslj3IbAfad31YEnOeu+mj9oB3mz0o6KYEEKEoyRuBzvqHIMIzV5iYd25kbqYetCYQpOvGY4YvxxvcmhUodQf4/RkCgYEA2SM3mgOz+jaBs96JVtTeLeEFIaKmnceD8u+wuuVfulY2aDDkEwYPfHXBqu4EqZjpcDg2l2EBsLRy7Ot7K2EaGJV0s7H1AP/13kziKCl9WwDoDg4Dk7ZRzj378YFehX4b9T3SQ36WMkgFHB9zOcRP5Rf7HO4inN6UqILHSTuTOY4=", 
            //    "json", 
            //    "1.0", 2088631146114221
            //    "RSA2",
            //    "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA05z4aIvz0V9DyE/YS72CuCVbb9ncn5EJHGijdQ+qokDXR1e0SNIbZeitNKUL1bfqapjYIDIPON49VHtPFBWz2osSITsv9+lIQUhb/nBvTAhLL2Yebx6/NPC3a+meqm5e53Fu8W6WYvpQGI7zcGMiJaDs19xZHyTGJf0dTVn7+r2/XWA37801W8rADnikIyKm4gu1AiWWtpuVy+bimc6fcFq5dHOnHGlcK+kx+Bm4yRN3aDE8kG2EszLV850KaZwvHlUGdiW3mVixpfbQJV8ei2n0HIwLVGVvt1hjwNGgvrNAuBBc2ACAkk45gg77+pTiHR1XzxyjiXEelGUfFN09wQIDAQAB",
            //    "GBK", false);
            AlipayTradePrecreateRequest  request= new AlipayTradePrecreateRequest();

            request.BizContent = "{" +
"\"out_trade_no\":\"201523423420101001\"," +
"\"seller_id\":\"2088102179325219\"," +
"\"total_amount\":0.1," +
"\"subject\":\"Iphone6 16G\"," +
"\"timeout_express\":\"90m\"," +
"\"qr_code_timeout_express\":\"90m\"" +
"  }";
            AlipayTradePrecreateResponse response=client.Execute(request);
            ViewBag.Src = response.QrCode;
            return File(QrCodeHelper.GenQrCode(response.QrCode, 200).ToArray(), @"image/jpeg");
        }

        public ActionResult About ()
        {
            ViewBag.Message = "Your application description page.";
            var isRedirect = OAuth.LoginByOAuth();
            //如果发生调转，则不输出任何内容（降低服务器压力）
            if (isRedirect) return Content(string.Empty);
            //获取当前用户OpenId
            ViewBag.openId = OAuth.GetOAuthMembership().OpenId;
            return View();
        }

        public ActionResult Contact ()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        private string GetRandomMumber (int intSize)
        {
            string strRtn = "";
            Random Rnd = new Random();
            char[] ocdChars = "0123456789".ToCharArray();
            for (int i = 0; i < intSize; i++)
            {
                strRtn += ocdChars[Rnd.Next(0, ocdChars.Length)].ToString();
            }
            return strRtn;
        }
    }
}