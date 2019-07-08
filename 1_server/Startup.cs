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
			 //������ʱǩ��ƾ�ݡ�Keyset is missing
			 .AddDeveloperSigningCredential()
			  .AddInMemoryIdentityResources(Config.GetIdentityResources())
			   .AddInMemoryApiResources(Config.GetApis())
				.AddInMemoryClients(Config.GetClients());

			
			//
			//���api��Դ��Ҳ������Щapi������֤���Ĺ�������QQ�е�QQ���API��
			//.AddInMemoryApiResources(new List<ApiResource>{
			//	new ApiResource("api1", "My API")
			// })
			////��ӿͻ���Ϣ��Ҳ������������QQ��������¼ʱ��QQ�����ǵ�APP ID��APP Key��
			////QQ�е�APP ID��Ӧ���������ClientId��APP Key��Ӧ���������Secret
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
