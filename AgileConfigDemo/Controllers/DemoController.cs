using AgileConfig.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AgileConfigDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigClient _configClient;
        private readonly IOptions<RabbitMqConfig> _dbOptions;
        private readonly IOptionsSnapshot<RabbitMqConfig> _dbOptionsSnapshot;
        public DemoController(IConfiguration configuration, IOptions<RabbitMqConfig> dbOptions, IOptionsSnapshot<RabbitMqConfig> dbOptionsSnapshot, IConfigClient configClient)
        {
            _configuration = configuration;
            _dbOptionsSnapshot = dbOptionsSnapshot;
            _dbOptions = dbOptions;
            _configClient = configClient;
        }
        [HttpGet("config")]
        public IActionResult ByIConfiguration()
        { 
            var test= _configuration.GetValue<RabbitMqConfig>("rabbitmq:rabbitmq");
            string aaa = _configuration["demo_test:Abc"];
            string info = $"Abc.aaa={aaa}"; 
            return Ok(info);
        }
        [HttpGet("configmodel")]
        public IActionResult Model()
        {
            var aaa = _dbOptions.Value;
          
            return Ok(aaa);
        }
         
        [HttpGet("configclient")]
        public IActionResult ByIConfigClient()
        {
            
            var aaa = _configClient["demo_test:Abc"];
            string info = $"Abc.aaa={aaa}"; 
            foreach (var item in _configClient.Data)
            {
                Console.WriteLine($"{item.Key} = {item.Value}");
            }
            return Ok(info);
        }
    }
}
