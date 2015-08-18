using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using System;
using WebApiTest.Providers;

namespace WebApiTest
{
    public partial class Startup
    {
        public const string FacebookAppId = "132414960151040";
        public const string FacebookAppSecret = "644b791f140d413865779debcd7e9faf";

        public void ConfigureAuth(IAppBuilder app)
        {
            var OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true,
            });
        }
    }
}
