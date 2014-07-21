using System;
using System.Web.Http;
using Tt.Rest.App_Start;

namespace Tt.Rest
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Tt.Framework"));

        }
    }
}
