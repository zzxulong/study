using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alipay.Models
{
    public class AlipayConfig
    {
        public static string app_id = "";
        public static string gatewayUrl = "https://openapi.alipay.com/gateway.do";
        // 商户私钥，您的原始格式RSA私钥
        public static string private_key = "MIIEpAIBAAKCAQEAlJVYD01XXeaKJOKgejmoIJ3bC8F/nNY5jrOGRxR0clOH5xDW7YoTyls/xuKSNhNsjJ9OGglYr6cg0cExrCF2MeWrShFRtOIysyhiTV0o2jY8rYpvzNOWwoEzWguaqf7xS5uU/oshxtKEXyik7Nnzg4OTcpofLB7eJYnGqhTqHss04/1W18pAkVAMkGpA0i7jOqKpHvsOMCEkGHfLbasK+vCsAKz4UkxUUNx/7DEw1fEv6QsxtEmnktj8012rAFE/gOsU4mUm19c37uqGL/sydirthV1LubW18r2SikG92OgwI98UXTs4y+BbhIfsjtqUO1+pKINHvOpniv0nftKlkQIDAQABAoIBACIDqlwebYC+lSNqiihQ6K9G0+12B5vE4vpLGNNO2HWPr83y6fEJKgyqJQiHt8cPXUs38O4+84GKthWur4tjQaacmEDlhBnvqoGfHhnt2LtTrUJhLcTrL1kpsLLNc0O1bFHNtScsfLgCW3FZYaAjKP8TcP7MT5SPIHXhe4xWK16O9yayq7nlex9jbJBpt4LLuZqGLcMVPF0ZTK0DPLgEBSNbtgjDvhN3NrSvchhVaQNiwVSwXYfV5oljxNwPPB9I6kkVSmFCAgFBUJ3OkpxFi5rol5ztM3xuO7gefNAyy35OFwpvKXQVbCkF0ZFQ7sUNfHfWFkPtfI+Uh0mvxAbkkMECgYEAxk7E39ujFvKaNastPN9RvPf880FeGLr0lZ0BOFxuyfjiTJk5F2ddUdF5D1jYt43aXCd8u7VTk6dwnVPqTNaX2nLTSKEwSQUjkcovHMTdm4imHAj+HVWO2q9rkf43/M7vnShppH5HFFxDQ21Eoea4t3uCtpQu5OTe9wn8kje4JKkCgYEAv89KNA2fSZ2kn5ii+DwLTHpPtOw4JDjt8DK9tzyzLqQ6VoXqFBY8N9JuxgPaIapPKGY42dADS6SognJCV4z2pGB26uh0M2hoxxLP+6LXbXTTuhmuXV9lW0yzmugVf7O5LaJ4bMubJz5rr5fcLUqJNJ6mBOwSt+cyATJW+rDEIqkCgYBmyVMwONa6wTp/EN+cyp/CU8OPjlUoelbl6YBFZ1uhYlhhnZXC9Leg/fNCqimCUotY8rqrTJwqT0KGuYUbGKuS9atf3PR7FKfQHDz5K201/ckjhOG58Kvf28bk4CDC77uw+M5xDWboPb47h7poNH+P0vv4iKwZxitvXoTEgQ74KQKBgQCGGCwfMfrPI2eor7QXaRrZD5VTveUN5cLqrv6U8Nmv3N5wCNT50AWvXO1Wf/Dqoj8O1lAebl8vbeDkuW6J+KGhj1LzrSk3m2HM8uj2WgTTn945d6S/4GZiqr/RpzkIeyQVtEUOglVo3Sm5q6gg/b/oSpV0UXpQmVALKB9IyXWFaQKBgQDDANHs/gzE/8jst5YhKvv8Dxfk0Aee6ok8Hkj+rvzYdQY7ZWY8NCeJbEK6ZVFAuAB0a/sRB035MGKZc3/ZTFAE9StGgzPhKNtpU+MPlBVvd7TrE26lNJ6jlpSuuNAp3yjSdYkfAwkeGnLIxWUKt6m5w0UUQKXXFt4t23j6ba4TmQ==";

        // 支付宝公钥,查看地址：https://openhome.alipay.com/platform/keyManage.htm 对应APPID下的支付宝公钥。
        public static string alipay_public_key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAlJVYD01XXeaKJOKgejmoIJ3bC8F/nNY5jrOGRxR0clOH5xDW7YoTyls/xuKSNhNsjJ9OGglYr6cg0cExrCF2MeWrShFRtOIysyhiTV0o2jY8rYpvzNOWwoEzWguaqf7xS5uU/oshxtKEXyik7Nnzg4OTcpofLB7eJYnGqhTqHss04/1W18pAkVAMkGpA0i7jOqKpHvsOMCEkGHfLbasK+vCsAKz4UkxUUNx/7DEw1fEv6QsxtEmnktj8012rAFE/gOsU4mUm19c37uqGL/sydirthV1LubW18r2SikG92OgwI98UXTs4y+BbhIfsjtqUO1+pKINHvOpniv0nftKlkQIDAQAB";
        // 签名方式
        public static string sign_type = "RSA2";

        // 编码格式
        public static string charset = "UTF-8";
    }
}