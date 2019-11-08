using Castle.DynamicProxy;
using CastleCoreTest.Core;
using ConsoleTest.Core;
using CsharpHttpHelper;
using CsharpHttpHelper.Enum;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CastleCoreTest
{
    class Program
    {

        public static HttpHelper httpHelper=new HttpHelper();
		private static int _result;
        static void Main (string[] args)
        {
            #region email
            //"song","20191104","mail.ldddns.com",110,false
            //var popHelper=new PopHelper();
            //popHelper.CfgIMAP(1);
            //var t=popHelper.GetEmailByFolder("INBOX");
            //foreach (var email in t.Where(e => e.Address == "support-zh@support.gandi.net"))
            //{
            //    if (!email.IsRead)
            //    {
            //        var emailContent=popHelper.GetEmailByUniqueId(email.UniqueId);

            //        var regex=Regex.Match(emailContent.BodyText,@"https://account.gandi.net/zh-hans/validate-email([\s\S]+?)[\s]");
            //        if (regex.Success)
            //        {
            //            var vaildUrl=regex.Value.TrimEnd('r').TrimEnd('\\');
            //            var vaildItem=new HttpItem()
            //            {
            //                Expect100Continue=false,
            //                URL=vaildUrl,
            //            };
            //            var tcontent = httpHelper.GetHtml(vaildItem);
            //        }
            //    }
                
            //}
            #endregion
            #region register
            //var url="https://account.gandi.net/zh-hans/create_account";
            //var item=new HttpItem(){
            //    Expect100Continue=false,
            //    URL=url,
            //};

            //var content=httpHelper.GetHtml(item);
            //var doc = new HtmlAgilityPack.HtmlDocument();
            //doc.LoadHtml(content.Html);
            //var header=new WebHeaderCollection();
            //header.Add("Cache-Control", "max-age=0");
            //header.Add("Origin", "https://account.gandi.net");
            //header.Add("Upgrade-Insecure-Requests", "1");
            //header.Add("DNT", "1");
            //header.Add("Accept-Encoding", "gzip, deflate, br");
            //header.Add("Sec-Fetch-Mode", "navigate");
            //header.Add("Sec-Fetch-User", "?1");
            //header.Add("Sec-Fetch-Site", "same-origin");
            //header.Add("Accept-Language", "zh-CN,zh;q=0.9");
            //var token=doc.DocumentNode.SelectSingleNode("//input[@name='csrf_token']").Attributes["value"].Value;
            //item = new HttpItem() {
            //    URL = url,
            //    Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3",
            //    Method = "POST",
            //    Encoding = Encoding.UTF8,
            //    PostEncoding = Encoding.UTF8,
            //    KeepAlive = true,
            //    ContentType = "application/x-www-form-urlencoded",
            //    UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.120 Safari/537.36",
            //    Cookie = content.Cookie,
            //    Referer = url,
            //    Header = header,
            //    Host = "account.gandi.net",
            //    Postdata = $"user.email=hfllvjjf@ldddns.com&user.username=hfllvjjfsong&user.password=pnbxlhdzAaaaaaA&contracts=g2&redirect=&csrf_token={token}&form.submitted=True"
            //};
            //content=httpHelper.GetHtml(item);
            #endregion

           

            #region login
            
            var header = new WebHeaderCollection();
            header.Add("Upgrade-Insecure-Requests", "1");
            header.Add("DNT", "1");
            header.Add("Sec-Fetch-Mode", "navigate");
            header.Add("Sec-Fetch-User", "?1");
            header.Add("Sec-Fetch-Site", "same-origin");
            var item = new HttpItem()
            {
                KeepAlive=true,
                URL = "https://id.gandi.net/zh-hans/login",
                ProxyIp= "127.0.0.1:8888",
            };
            var content = httpHelper.GetHtml(item);
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(content.Html);
            var token = doc.DocumentNode.SelectSingleNode("//input[@name='csrf_token']").Attributes["value"].Value;
            var redirect = doc.DocumentNode.SelectSingleNode("//input[@name='redirect']").Attributes["value"].Value;
            header = new WebHeaderCollection();
            header.Add("Upgrade-Insecure-Requests", "1");
            header.Add("Accept-Language", "zh-CN");
            header.Add("Cache-Control", "max-age=0");
            header.Add("Accept-Encoding", "gzip, deflate, br");
            item = new HttpItem()
            {
                URL = "https://id.gandi.net/zh-hans/login",
                Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8",
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36 Edge/18.18362",
                KeepAlive = true,
                Method = "POST",
                Allowautoredirect = true,
                AutoRedirectCookie=true,
                PostEncoding = Encoding.UTF8,
                CookieCollection = content.CookieCollection,
                ContentType = "application/x-www-form-urlencoded",
                Cookie = content.Cookie,
                Header = header,
                ProxyIp = "127.0.0.1:8888",
                Referer = content.ResponseUri,
                PostDataType = PostDataType.Dictionary,
                PostdataDic =
                {
                    { "auth.username","hfllvjjfsong"},
                    { "auth.password","pnbxlhdzAaaaaaA"},
                    { "auth.remember_me","1"},
                    { "redirect",redirect},
                    { "csrf_token",token},
                    { "form.submitted",""},
                },
            };
            content = httpHelper.GetHtml(item);
            item.URL = "https://admin.gandi.net";
            content = httpHelper.GetHtml(item);

            #endregion

            #region 拦截器
            //Interceptor();
            #endregion

            #region interlocked
            //Interlocked();
            #endregion

            Console.WriteLine("Hello World!");

		}

		#region 拦截器
		public static void Interceptor()
		{
			var proxyGenerate = new ProxyGenerator();//实例化【代理类生成器】  
													 //TestIntercept t = new TestIntercept();//实例化【拦截器】 
			var t = new Interceptor();
			//使用【代理类生成器】创建Person对象，而不是使用new关键字来实例化  
			var pg = proxyGenerate.CreateClassProxy<MyClass>(t);
			pg.MyMethod();
		}
		#endregion


		#region Interlocked
		private static void Interlocked()
		{
			//运行后按住Enter键数秒，对比使用Interlocked.Increment(ref _result);与 _result++;的不同
			while(true)
			{
				Task[] _tasks = new Task[100];
				int i = 0;

				for(i = 0;i < _tasks.Length;i++)
				{
					_tasks[i] = Task.Factory.StartNew((num) =>
					{
						var taskid = (int)num;
						Work(taskid);
					},i);
				}

				Task.WaitAll(_tasks);
				Console.WriteLine(_result);

				Console.ReadKey();
			}

		}

		//线程调用方法
		private static void Work(int TaskID)
		{
			for(int i = 0;i < 10;i++)
			{
				_result++;

				//Interlocked.Increment(ref _result);
			}
		}
		#endregion

	}
}
