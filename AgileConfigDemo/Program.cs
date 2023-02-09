using AgileConfigDemo;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
 
builder.Host.UseAgileConfig();
builder.Services.Configure<RabbitMqConfig>(builder.Configuration.GetSection("rabbitmq:config"));
builder.Services.AddControllersWithViews().AddControllersAsServices();
var config = builder.Configuration.GetSection("rabbitmq:config").Get<RabbitMqConfig>();


var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

});

app.Run();
