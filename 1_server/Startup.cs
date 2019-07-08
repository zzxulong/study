using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace _1_server
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services
			 .AddIdentityServer()
			 //设置临时签名凭据。Keyset is missing
			 .AddDeveloperSigningCredential()
			  .AddInMemoryIdentityResources(Config.GetIdentityResources())
			   .AddInMemoryApiResources(Config.GetApis())
				.AddInMemoryClients(Config.GetClients());

			
			//
			//添加api资源（也就是这些api是受认证中心管理，就像QQ中的QQ相册API）
			//.AddInMemoryApiResources(new List<ApiResource>{
			//	new ApiResource("api1", "My API")
			// })
			////添加客户信息（也就是我们申请QQ第三方登录时，QQ给我们的APP ID、APP Key）
			////QQ中的APP ID对应我们这里的ClientId，APP Key对应我们这里的Secret
			//.AddInMemoryClients(new List<Client>{
			//new Client
			//{
			//	ClientId = "client",
			//	AllowedGrantTypes = GrantTypes.ClientCredentials,
			//	ClientSecrets =
			//	{
			//		new Secret("secret".Sha256())
			//	},
			//	AllowedScopes = { "api1" }
			//}
			//});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app,IHostingEnvironment env)
		{
			if(env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
			}

			app.UseIdentityServer();
		}
	}
}
