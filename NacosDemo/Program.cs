using Nacos.AspNetCore.V2;
using Nacos.OpenApi;
using Nacos.V2.DependencyInjection;
using NacosDemo;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddNacosAspNet(builder.Configuration);
builder.Host.UseNacosConfig(section: "NacosConfig", parser: null, logAction: null);

//builder.Services.AddNacosV2Config(x =>
//{
//    x.ServerAddresses = new System.Collections.Generic.List<string> { "http://nacos.xumingzhen.com/" };
//    x.EndPoint = "";
//    x.Namespace = "public";
//    x.UserName = "nacos";
//    x.Password = "HhM50VunTbBWkj7F";

//    // this sample will add the filter to encrypt the config with AES.
//    x.ConfigFilterAssemblies = new System.Collections.Generic.List<string> { "App3" };

//    // swich to use http or rpc
//    x.ConfigUseRpc = false;
//});

//builder.Services.AddNacosV2Naming(x =>
//{
//    x.ServerAddresses = new System.Collections.Generic.List<string> { "http://nacos.xumingzhen.com/" };
//    x.EndPoint = "";
//    x.Namespace = "cs";

//    // swich to use http or rpc
//    x.NamingUseRpc = false;
//});

//builder.Services.AddNacosOpenApi(x =>
//{
//    x.ServerAddresses = new System.Collections.Generic.List<string> { "http://nacos.xumingzhen.com/" };
//    x.EndPoint = "";
//    x.Namespace = "public";
//});

//builder.Services.AddNacosV2Configuration(builder.Configuration.GetSection("NacosConfig"));

//builder.AddNacosV2Configuration(builder.GetSection("NacosConfig"));

builder.Services.AddNacosAspNet(builder.Configuration);
builder.Services.AddControllersWithViews().AddControllersAsServices();
var config = builder.Configuration.Get<RabbitMqConfig>();



var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

});
app.Run();
