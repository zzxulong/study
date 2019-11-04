using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace CustomerPay.Models.Helpers
{
    public class HttpHelper
    {
        #region 变量

        public static readonly string DefaultUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36";
        public static readonly string NotSupportMessage = "This request method is not supported by API!";

        #endregion

        #region WebClient  Http 请求

        /// <summary>
        /// HttpGet请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <returns></returns>
        public static string HttpGetRequest (string url)
        {
            return HttpGetRequest(url, null);
        }

        /// <summary>
        /// HttpGet请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="requestParams">参数</param>
        /// <returns></returns>
        public static string HttpGetRequest (string url, IDictionary<string, string> requestParams)
        {
            return HttpGetRequest(url, requestParams, null);
        }

        /// <summary>
        /// HttpGet请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="requestParams">参数</param>
        /// <param name="headerParams">Header参数</param>
        /// <returns></returns>
        public static string HttpGetRequest (string url, IDictionary<string, string> requestParams, IDictionary<string, string> headerParams)
        {
            if (requestParams != null && requestParams.Count > 0) url = url + (url.EndsWith("?") ? "&" : "?") + GenerateParameterString(requestParams);
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("user-agent", DefaultUserAgent);
                if (headerParams != null)
                {
                    foreach (KeyValuePair<string, string> item in headerParams)
                    {
                        client.Headers.Add(item.Key, item.Value);
                    }
                }
                Stream data = client.OpenRead(url);
                StreamReader reader = new StreamReader(data, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// HttpPost请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="postParams">参数(键值对类型)</param>
        /// <returns></returns>
        public static string HttpPostRequest (string url, IDictionary<string, string> requestParams)
        {
            return HttpPostRequest(url, requestParams, null);
        }

        /// <summary>
        /// HttpPost请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="requestData">参数（字符串类型）</param>
        /// <returns></returns>
        public static string HttpPostRequest (string url, string requestData)
        {
            return HttpPostRequest(url, requestData, null);
        }

        /// <summary>
        /// 发送Http post请求
        /// </summary>
        /// <param name="url">请求url</param>
        /// <param name="bodyParams">post数据</param>
        /// <param name="headerParams">头信息</param>
        /// <returns>请求结果</returns>
        public static string HttpPostRequest (string url, IDictionary<string, string> postParams, IDictionary<string, string> headerParams)
        {
            string postData = GenerateParameterString(postParams);
            return HttpPostRequest(url, postData, headerParams);
        }

        public static string HttpPostRequest (string url, string postData, IDictionary<string, string> headerParams)
        {
            byte[] data = Encoding.UTF8.GetBytes(postData);
            return Encoding.UTF8.GetString(HttpPostRequest(url, data, headerParams));
        }

        /// <summary>
        /// 发送Http post请求
        /// </summary>
        /// <param name="url">请求url</param>
        /// <param name="headerParams">头信息</param>
        /// <param name="data">post数据</param>
        /// <returns>请求结果</returns>
        public static byte[] HttpPostRequest (string url, byte[] data, IDictionary<string, string> headerParams)
        {
            byte[] postData = data;
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                if (headerParams != null)
                {
                    foreach (KeyValuePair<string, string> item in headerParams)
                    {
                        client.Headers.Add(item.Key, item.Value);
                    }
                }
                return client.UploadData(url, "post", postData);
            }
        }

        #region 过时方法

        /// <summary>
        /// 发送http Get请求,获取HttpResponse
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="timeout">超时</param>
        /// <param name="userAgent">代理</param>
        /// <param name="cookies">cookie</param>
        /// <returns>响应</returns>
        public static HttpWebResponse CreateGetHttpResponse (string url, int? timeout, string userAgent, CookieContainer cookies)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = DefaultUserAgent;
            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }
            if (cookies == null)
                cookies = new CookieContainer();
            request.CookieContainer = cookies;

            return request.GetResponse() as HttpWebResponse;
        }

        /// <summary>
        /// 发送http Post请求,获取HttpResponse
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parameters">请求参数</param>
        /// <param name="timeout">超时</param>
        /// <param name="userAgent">代理</param>
        /// <param name="requestEncoding">请求编码</param>
        /// <param name="cookies">cookie</param>
        /// <returns>响应</returns>
        protected HttpWebResponse CreatePostHttpResponse (string url, IDictionary<string, string> parameters, int? timeout, string userAgent, Encoding requestEncoding, CookieContainer cookies)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (requestEncoding == null)
            {
                throw new ArgumentNullException("requestEncoding");
            }
            HttpWebRequest request = null;
            //如果是发送HTTPS请求
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            else
            {
                request.UserAgent = DefaultUserAgent;
            }

            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }
            if (cookies != null) cookies = new CookieContainer();
            request.CookieContainer = cookies;
            //如果需要POST数据
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    }
                    i++;
                }
                byte[] data = requestEncoding.GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            return request.GetResponse() as HttpWebResponse;
        }
        #endregion

        #endregion

        #region HttpWebRequest Http请求

        public static string GetHttpResponse (string url)
        {
            return GetHttpResponse(url, null);
        }

        public static string GetHttpResponse (string url, IDictionary<string, string> requestParams)
        {
            return GetHttpResponse(url, requestParams, Encoding.UTF8);
        }

        public static string GetHttpResponse (string url, IDictionary<string, string> requestParams, Encoding encode)
        {
            if (requestParams != null && requestParams.Count > 0) url = url + (url.EndsWith("?") ? "&" : "?") + GenerateParameterString(requestParams);
            return GetHttpResponse(url, encode);
        }

        [Obsolete("此方法已经过时，只是为了兼容以前的方法")]
        public static string GetHttpResponse (string url, int? timeout, string userAgent, Encoding encode, CookieContainer cookies)
        {
            return GetHttpResponse(url, encode, null, cookies, null, true, userAgent, timeout);
        }

        /// <summary>
        /// 获取指定URL地址页面输出内容（GET）
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="userAgent">浏览器代理</param>
        /// <param name="encode">编码</param>
        /// <param name="cookies">cookie</param>
        /// <returns></returns>
        public static string GetHttpResponse (string url,
            Encoding encode = null, IDictionary<string, string> headerParams = null, CookieContainer cookies = null, string refererUrl = null, bool allowAutoRedirect = true, string userAgent = null, int? timeout = 5000)
        {

            string strReturn = string.Empty;
            try
            {
                //读取响应页面内容
                using (var response = CreateGetHttpResponse(url, ref encode, headerParams, cookies, refererUrl, allowAutoRedirect, userAgent, timeout))
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader streamRead = new StreamReader(responseStream, encode))
                        {
                            strReturn = streamRead.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception)
            {
                //throw new HttpException(e.Message);
            }
            return strReturn;
        }


        public static HttpWebResponse CreateGetHttpResponse (string url,
            ref Encoding encode, IDictionary<string, string> headerParams = null, CookieContainer cookies = null, string refererUrl = null, bool allowAutoRedirect = true, string userAgent = null, int? timeout = 5000)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException("url");
            if (encode == null) encode = Encoding.UTF8;

            HttpWebRequest orqRequest = WebRequest.Create(url) as HttpWebRequest;
            orqRequest.Method = "GET";
            orqRequest.AllowAutoRedirect = allowAutoRedirect;
            orqRequest.KeepAlive = true;
            //设置UrlReferer
            if (!string.IsNullOrEmpty(refererUrl)) orqRequest.Referer = refererUrl;
            //设置浏览器代理
            if (!string.IsNullOrEmpty(userAgent)) orqRequest.UserAgent = userAgent;
            else orqRequest.UserAgent = DefaultUserAgent;
            //设置超时时间
            if (timeout.HasValue) orqRequest.Timeout = timeout.Value;

            //设置Head
            if (headerParams != null)
            {
                foreach (KeyValuePair<string, string> item in headerParams)
                {
                    orqRequest.Headers.Add(item.Key, item.Value);
                }
            }

            //设置Cookie
            if (cookies == null) cookies = new CookieContainer();
            orqRequest.CookieContainer = cookies;

            //返回HttpResponse
            return orqRequest.GetResponse() as HttpWebResponse;
        }

        public static string PostHttpResponse (string url, IDictionary<string, string> requestParams)
        {
            return PostHttpResponse(url, requestParams, Encoding.UTF8);
        }

        public static string PostHttpResponse (string url, IDictionary<string, string> requestParams, Encoding encode, List<StreamFileInfo> files = null)
        {
            return PostHttpResponse(url, requestParams, files, encode);
        }

        public static string PostHttpResponse (string url, string data)
        {
            return PostHttpResponse(url, data, Encoding.UTF8);
        }
        public static string PostHttpResponse (string url, string data, Encoding encode, List<StreamFileInfo> files = null)
        {
            return PostHttpResponse(url, data, files, encode);
        }

        /// <summary>
        /// 获取指定URL地址页面输出内容（POST）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">请求地址</param>
        /// <param name="formData">post参数</param>
        /// <param name="files">上传文件</param>
        /// <param name="encode">编码格式</param>
        /// <param name="refererUrl">来源页面</param>
        /// <param name="cookies">请求cookie</param>
        /// <param name="allowAutoRedirect">是否自动跳转</param>
        /// <param name="userAgent">代理</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        public static string PostHttpResponse<T> (string url, T formData, List<StreamFileInfo> files = null,
            Encoding encode = null, IDictionary<string, string> headerParams = null, CookieContainer cookies = null, string refererUrl = null, bool allowAutoRedirect = true, string userAgent = null, int? timeout = 5000)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException("url");
            if (encode == null) encode = Encoding.UTF8;
            string strReturn = string.Empty;
            try
            {
                //读取响应页面内容
                using (var response = CreatePostHttpResponse(url, formData, files, encode, headerParams, cookies, refererUrl, allowAutoRedirect, userAgent, timeout))
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader streamRead = new StreamReader(responseStream, encode))
                        {
                            strReturn = streamRead.ReadToEnd();

                        }
                    }
                }
            }
            catch (Exception)
            {
                //throw new HttpException(e.Message);
            }
            return strReturn;
        }

        public static HttpWebResponse CreatePostHttpResponse<T> (string url, T formData, List<StreamFileInfo> files = null,
            Encoding encode = null, IDictionary<string, string> headerParams = null, CookieContainer cookies = null, string refererUrl = null, bool allowAutoRedirect = true, string userAgent = null, int? timeout = 5000)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException("url");
            if (encode == null) encode = Encoding.UTF8;

            using (var postStream = new MemoryStream())
            {
                HttpWebRequest orqRequest = WebRequest.Create(url) as HttpWebRequest;

                //判断是否包括文件上传
                var formUploadFile = files != null && files.Count > 0;
                if (formUploadFile)
                {
                    string boundary = string.Format("----{0}", DateTime.Now.Ticks.ToString("x")),
                        beginBoundary = string.Format("--{0}\r\n", boundary),
                        endBoundary = string.Format("\r\n--{0}--\r\n", boundary);

                    byte[] beginBoundaryBytes = encode.GetBytes(beginBoundary),
                            endBoundaryBytes = encode.GetBytes(endBoundary);

                    string parameterHeaderTemplate = beginBoundary + "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n";
                    string fileHeaderTemplate = beginBoundary + "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n";

                    if (typeof(IDictionary<string, string>).IsAssignableFrom(typeof(T)))
                    {
                        var dicFormData = (IDictionary<string, string>)formData;
                        if (dicFormData != null && dicFormData.Count > 0)
                        {
                            foreach (var item in dicFormData)
                            {
                                //写入参数
                                var formdataBytes = encode.GetBytes(string.Format(parameterHeaderTemplate, item.Key, item.Value));
                                postStream.Write(formdataBytes, 0, formdataBytes.Length);
                            }
                        }
                    }

                    foreach (var file in files)
                    {
                        if (file.File != null)
                        {
                            //写入文件头
                            var formdataBytes = encode.GetBytes(string.Format(fileHeaderTemplate, file.Name, Path.GetFileName(file.FileName)));
                            postStream.Write(formdataBytes, 0, formdataBytes.Length);
                            //写入文件
                            file.File.Seek(0, SeekOrigin.Begin);//设置指针读取位置
                            byte[] buffer = new byte[1024];
                            int bytesRead = 0;
                            while ((bytesRead = file.File.Read(buffer, 0, buffer.Length)) != 0)
                            {
                                postStream.Write(buffer, 0, bytesRead);
                            }
                        }
                    }

                    //写入结尾                  
                    postStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
                    orqRequest.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
                }
                else
                {
                    var stringFormData = string.Empty;
                    if (typeof(T) == typeof(string)) stringFormData = formData.ToString();
                    else if (typeof(IDictionary<string, string>).IsAssignableFrom(typeof(T))) stringFormData = GenerateParameterString((IDictionary<string, string>)formData);
                    if (!string.IsNullOrEmpty(stringFormData))
                    {
                        byte[] obtformData = encode.GetBytes(stringFormData);
                        postStream.Write(obtformData, 0, obtformData.Length);
                    }
                    orqRequest.ContentType = "application/x-www-form-urlencoded";
                }

                orqRequest.Method = "POST";
                //orqRequest.Referer = RefererUrl;
                orqRequest.AllowAutoRedirect = allowAutoRedirect;
                orqRequest.ContentLength = postStream != null ? postStream.Length : 0;
                orqRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                orqRequest.KeepAlive = true;

                //设置UrlReferer
                if (!string.IsNullOrEmpty(refererUrl)) orqRequest.Referer = refererUrl;
                //设置浏览器代理
                orqRequest.UserAgent = string.IsNullOrEmpty(userAgent) ? DefaultUserAgent : userAgent;

                //设置超时时间
                if (timeout.HasValue) orqRequest.Timeout = timeout.Value;

                //设置Head
                if (headerParams != null)
                {
                    foreach (KeyValuePair<string, string> item in headerParams)
                    {
                        orqRequest.Headers.Add(item.Key, item.Value);
                    }
                }

                //设置Cookie
                if (cookies == null) cookies = new CookieContainer();
                orqRequest.CookieContainer = cookies;

                if (postStream != null)
                {
                    postStream.Seek(0, SeekOrigin.Begin);//设置指针读取位置
                    //写入post数据
                    using (Stream stream = orqRequest.GetRequestStream())
                    {
                        byte[] buffer = new byte[1024];
                        int bytesRead = 0;
                        while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            stream.Write(buffer, 0, bytesRead);
                        }
                    }
                }
                //返回
                return orqRequest.GetResponse() as HttpWebResponse;
            }

        }
        #endregion

        #region 私有方法

        /// <summary>
        /// 根据字典生成URL参数
        /// </summary>
        /// <param name="requestParams">请求参数</param>
        /// <returns>URL参数</returns>
        private static string GenerateParameterString (IDictionary<string, string> requestParams)
        {
            string paramstring = "";
            if (requestParams != null)
            {
                foreach (KeyValuePair<string, string> pair in requestParams)
                {
                    paramstring += pair.Key + "=" + pair.Value + "&";
                }
                paramstring = paramstring.Substring(0, paramstring.Length - 1);
            }
            return paramstring;
        }

        private static string GenerateParameterEncodeString (IDictionary<string, string> requestParams)
        {
            string paramstring = "";
            if (requestParams != null)
            {
                foreach (KeyValuePair<string, string> pair in requestParams)
                {
                    paramstring += pair.Key + "=" + HttpUtility.UrlEncode(pair.Value) + "&";
                }
                paramstring = paramstring.Substring(0, paramstring.Length - 1);
            }
            return paramstring;
        }

        private static bool CheckValidationResult (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受
        }

        #endregion
    }
    public class StreamFileInfo
    {

        public string Name { get; set; }
        public string FileName { get; set; }
        public Stream File { get; set; }
    }
}