using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Microsoft.AspNet.Identity;
using WebUI.Infrastructure;

[assembly: OwinStartup(typeof(WebUI.App_Start.Startup))]

namespace WebUI.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });

            //app.UseOAuthBearerTokens(new OAuthAuthorizationServerOptions
            //{
            //    Provider = new StoreAuthProvider(),
            //    AllowInsecureHttp = true,
            //    TokenEndpointPath = new PathString("/Authenticate")
            //});
        }
    }
}