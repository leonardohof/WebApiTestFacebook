using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebApiTest.Controllers
{
    public class AccountController : ApiController
    {
        private IAuthenticationManager Authentication
        {
            get { return HttpContext.Current.GetOwinContext().Authentication; }
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("FacebookLogin")]
        //public async Task<IHttpActionResult> ExternalLogin(string provider, string token, string error = null)
        //{
        //    if (provider != "Facebook")
        //        return BadRequest("Invalid provider.");

        //    if (string.IsNullOrEmpty(token))
        //        return BadRequest("Invalid OAuth access token");

        //    var tokenExpirationTimeSpan = TimeSpan.FromDays(14);
        //    ApplicationUser user = null;
        //    // Get the fb access token and make a graph call to the /me endpoint
        //    var fbUser = await VerifyFacebookAccessToken(token);
        //    if (fbUser == null)
        //    {
        //        return BadRequest("Invalid OAuth access token");
        //    }
        //    // Check if the user is already registered
        //    user = await UserManager.FindByNameAsync(fbUser.Username);
        //    // If not, register it
        //    if (user == null)
        //    {
        //        var randomPassword = System.Web.Security.Membership.GeneratePassword(10, 5);
        //        user = await RegisterUserAsync(fbUser.Username, randomPassword, fbUser.ID);
        //        var customer = await RegisterCustomerAsync(fbUser.FirstName, fbUser.LastName, fbUser.Email, user);
        //    }
        //    // Sign-in the user using the OWIN flow
        //    var identity = new ClaimsIdentity(Startup.OAuthBearerOptions.AuthenticationType);
        //    identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName, null, "Facebook"));
        //    // This is very important as it will be used to populate the current user id 
        //    // that is retrieved with the User.Identity.GetUserId() method inside an API Controller
        //    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id, null, "LOCAL_AUTHORITY"));
        //    AuthenticationTicket ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
        //    var currentUtc = new Microsoft.Owin.Infrastructure.SystemClock().UtcNow;
        //    ticket.Properties.IssuedUtc = currentUtc;
        //    ticket.Properties.ExpiresUtc = currentUtc.Add(tokenExpirationTimeSpan);
        //    var accesstoken = Startup.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);
        //    Request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accesstoken);
        //    Authentication.SignIn(identity);

        //    // Create the response building a JSON object that mimics exactly the one issued by the default /Token endpoint
        //    JObject blob = new JObject(
        //        new JProperty("userName", user.UserName),
        //        new JProperty("access_token", accesstoken),
        //        new JProperty("token_type", "bearer"),
        //        new JProperty("expires_in", tokenExpirationTimeSpan.TotalSeconds.ToString()),
        //        new JProperty(".issued", ticket.Properties.IssuedUtc.ToString()),
        //        new JProperty(".expires", ticket.Properties.ExpiresUtc.ToString())
        //    );
        //    // Return OK
        //    return Ok(blob);
        //}

    }
}
