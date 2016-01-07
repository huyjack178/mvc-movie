using Fanex.Data.Configuration;
using MvcMovie.Models;
using MvcMovie.Models.DataHandler;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace MvcMovie
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode,
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DbSettingProviderManager.Current.Start();
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        string userName = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                        string roles = string.Empty;

                        UserDataHandler userDataHandler = new UserDataHandler();

                        ClientUser user = (ClientUser)userDataHandler.Get(userName);
                        roles = user.Role.ToString();

                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(
                          new System.Security.Principal.GenericIdentity(userName, "Forms"), roles.Split(';'));
                    }
                    catch (Exception)
                    {
                        ////somehting went wrong
                    }
                }
            }
        }
    }
}