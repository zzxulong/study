using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using QQLogin.Models;

namespace QQLogin.Controllers
{
    public class QQUserController : Controller
    {
        // GET: QQUser
        public ActionResult Login ()
        {
            string state = DateTime.Now.ToString("yyyyMMddHHmmssffff");  //client端的状态值。用于第三方应用防止CSRF攻击，成功授权后回调时会原样带回。
            string url = QQLoginHelper.CreateAuthorizeUrl(state);
            RouteValueDictionary routeValue = RouteData.Route.GetRouteData(this.HttpContext).Values;
            Session["QQLoginState"] = state;    //记录client端状态值
            Session["BeforeLoginUrl"] = Request.UrlReferrer;    //记录登陆之前的URL，登陆成功后返回
            return Redirect(url);
        }
        public ActionResult CallBack ()
        {
            string backState = Request.QueryString["state"];
            string code = Request.QueryString["code"];
            if (null != backState && null != code && null != Session["QQLoginState"] && backState == Session["QQLoginState"].ToString())
            {
                Session["QQLoginState"] = null;
                //调获取AccessToken的接口
                string access_token = QQLoginHelper.GetAccessToken(code);
                //调获取OpenID的接口
                string openid =QQLoginHelper.GetOpenId(access_token);
                //调获取用户信息的接口
                QQUserInfo qqUserInfo = QQLoginHelper.GetQQUserInfo(access_token, openid);
                Response.Cookies["is_lost"].Value = qqUserInfo.is_lost.ToString();
                Response.Cookies["province"].Value = qqUserInfo.province;
                Response.Cookies["city"].Value = qqUserInfo.city;
                Response.Cookies["year"].Value = qqUserInfo.year;
                Response.Cookies["constellation"].Value = qqUserInfo.constellation;
                Response.Cookies["figureurl_qq"].Value = qqUserInfo.figureurl_qq;
                Response.Cookies["figureurl_type"].Value = qqUserInfo.figureurl_type;
                Response.Cookies["ret"].Value = qqUserInfo.ret.ToString();
                Response.Cookies["msg"].Value = qqUserInfo.msg;
                Response.Cookies["nickname"].Value = qqUserInfo.nickname;
                Response.Cookies["figureurl"].Value = qqUserInfo.figureurl;
                Response.Cookies["figureurl_1"].Value = qqUserInfo.figureurl_1;
                Response.Cookies["figureurl_2"].Value = qqUserInfo.figureurl_2;
                Response.Cookies["figureurl_qq_1"].Value = qqUserInfo.figureurl_qq_1;
                Response.Cookies["figureurl_qq_2"].Value = qqUserInfo.figureurl_qq_2;
                Response.Cookies["gender"].Value = qqUserInfo.gender;
                Response.Cookies["is_yellow_vip"].Value = qqUserInfo.is_yellow_vip;
                Response.Cookies["vip"].Value = qqUserInfo.vip;
                Response.Cookies["yellow_vip_level"].Value = qqUserInfo.yellow_vip_level;
                Response.Cookies["level"].Value = qqUserInfo.level;
                Response.Cookies["figureurl_qq_1"].Value = qqUserInfo.is_yellow_year_vip;
                //if (qqUserInfo.ret == 0 && string.IsNullOrEmpty(qqUserInfo.msg))
                //{
                //    Response.Cookies["nickname"].Value = qqUserInfo.nickname;  //将值写入到客户端硬盘Cookie
                //    Response.Cookies["nickname"].Expires = DateTime.Now.AddDays(7);//设置Cookie过期时间
                //}
                //else
                //{
                //    //获取用户失败
                //    throw new ApplicationException(qqUserInfo.msg);
                //}
                if (Session["BeforeLoginUrl"] != null)
                {
                    return Redirect(Session["BeforeLoginUrl"].ToString()); //返回登陆之前的URL
                }
                else
                {
                    throw new Exception("登录之前的路由丢失");
                }
            }
            else
            {
                throw new Exception("登录State已丢失或不正确");
            }
        }
        public ActionResult Logout ()
        {
            return View();
        }

    }
}