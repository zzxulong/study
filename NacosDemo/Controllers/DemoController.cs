using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Nacos.V2;

namespace NacosDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly INacosNamingService _svc;
        private readonly IOptionsMonitor<RabbitMqConfig> _optionsMonitor;

        public DemoController(IConfiguration configuration, IOptionsMonitor<RabbitMqConfig> optionsMonitor, INacosNamingService svc)//
        {
            _configuration = configuration;
            _optionsMonitor = optionsMonitor;
           _svc = svc;
        }

        [HttpGet]
        public object Get()
        {
            //var res = _svc.GetConfig("rabbitmq", "DEFAULT_GROUP", 3000).Result;
            
            var rabbitmq= _configuration.Get<RabbitMqConfig>();
            var conn = _configuration.Get<ConnectionStrings>();
            _svc.RegisterInstance("BaseApi", new Nacos.V2.Naming.Dtos.Instance()
            {
                ServiceName = "BaseApi",
                ClusterName = "DEFAULT",
                Ip = "127.0.0.1",
                Port = 5259,
                Enabled = true,
                Weight = 100,
                Metadata = null
            });
            var all = _svc.GetAllInstances("BaseApi", false);

            var instance = _svc.SelectOneHealthyInstance("BaseApi", "DEFAULT_GROUP").Result;
            var host = $"{instance.Ip}:{instance.Port}";

            var baseUrl = instance.Metadata.TryGetValue("secure", out _)
                ? $"https://{host}"
                : $"http://{host}";

            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                return "empty";
            }

            var url = $"{baseUrl}/api/values";
             
            return new {rabbitmq,conn, _optionsMonitor,url };
        }
        /// <summary>
        /// 通过nacos的服务发现来调用接口
        /// </summary>
        /// <returns></returns>
        [HttpGet("test")]
        public async Task<string> Test()
        {
            //// 通过分组与服务名获取
            //var instance = await _svc.SelectOneHealthyInstance("BaseApi", "DEFAULT_GROUP");
            //var host = $"{instance.Ip}:{instance.Port}";
            //var baseUrl = instance.Metadata.TryGetValue("secure", out _)
            //    ? $"https://{host}"
            //    : $"http://{host}";
            //if (string.IsNullOrWhiteSpace(baseUrl))
            //{
            //    return "empty";
            //}
            ////测试调用一下上面那个接口
            //var url = $"{baseUrl}/api/WeatherForecast";
            //using (HttpClient client = new HttpClient())
            //{
            //    var result = await client.GetAsync(url);
            //    return await result.Content.ReadAsStringAsync();
            //}
            return "";
        }
    }
}
