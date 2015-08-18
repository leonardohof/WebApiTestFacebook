using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTest.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext c)
        {
            c.Validated();

            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext c)
        {
            if (c.UserName == "leonardo" && c.Password == "123123")
            {
                Claim claim1 = new Claim(ClaimTypes.Name, c.UserName);
                Claim[] claims = new Claim[] { claim1 };
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(
                       claims, OAuthDefaults.AuthenticationType);
                c.Validated(claimsIdentity);
            }

            return Task.FromResult<object>(null);
        }

        public override Task GrantCustomExtension(OAuthGrantCustomExtensionContext c)
        {
            if (c.GrantType == "facebook_token")
            {
                var token = c.Parameters.Get("token");
                if (!string.IsNullOrEmpty(token))
                {
                    if (ValidateFacebookToken(token).Result)
                    {
                        var fbInfo = GetFacebookInfo(token).Result;
                        if (fbInfo != null)
                        {
                            string id = fbInfo["id"].Value;
                            //string email = fbInfo["email"].Value;
                            string name = fbInfo["name"].Value;

                            // Verificar se o usuário existe no banco de dados
                            // Se não existir e tiver todos os dados, pode cadastrar já
                            bool userHasRegistered = true;
                            if (userHasRegistered)
                            {
                                Claim claim1 = new Claim(ClaimTypes.Name, name);
                                Claim[] claims = new Claim[] { claim1 };
                                ClaimsIdentity claimsIdentity =
                                    new ClaimsIdentity(
                                       claims, OAuthDefaults.AuthenticationType);
                                c.Validated(claimsIdentity);
                            }
                        }
                    }
                }
            }

            return Task.FromResult<object>(null);
        }

        #region Helpers

        private async Task<bool> ValidateFacebookToken(string token)
        {
            var verifyTokenEndPoint = string.Format("https://graph.facebook.com/app?access_token={0}", token);
            var client = new HttpClient();
            var uri = new Uri(verifyTokenEndPoint);
            var response = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                dynamic jObj = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                var app_id = jObj["id"].Value;

                if (string.Equals(Startup.FacebookAppId, app_id, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        private async Task<dynamic> GetFacebookInfo(string token)
        {
            var verifyTokenEndPoint = string.Format("https://graph.facebook.com/me?access_token={0}", token);
            var client = new HttpClient();
            var uri = new Uri(verifyTokenEndPoint);
            var response = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                dynamic jObj = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                return jObj;
            }

            return null;
        }

        #endregion
    }
}
