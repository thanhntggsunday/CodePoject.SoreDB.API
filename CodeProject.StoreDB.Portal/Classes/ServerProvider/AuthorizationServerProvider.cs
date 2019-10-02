namespace CodeProject.StoreDB.Portal.Classes.ServerProvider
{
    using CodeProject.StoreDB.Model.Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="AuthorizationServerProvider" />
    /// </summary>
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// The ValidateClientAuthentication
        /// </summary>
        /// <param name="context">The context<see cref="OAuthValidateClientAuthenticationContext"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            await Task.FromResult<object>(null);
        }

        /// <summary>
        /// The GrantResourceOwnerCredentials
        /// </summary>
        /// <param name="context">The context<see cref="OAuthGrantResourceOwnerCredentialsContext"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            UserManager<ApplicationUser> userManager = context.OwinContext.GetUserManager<UserManager<ApplicationUser>>();
            ApplicationUser user;

            try
            {
                user = await userManager.FindAsync(context.UserName, context.Password);
            }
            catch
            {
                // Could not retrieve the user due to error.
                context.SetError("server_error");
                context.Rejected();
                return;
            }
            if (user != null)
            {
                ClaimsIdentity identity = await userManager.CreateIdentityAsync(user,
                                                       DefaultAuthenticationTypes.ExternalBearer);

                string email = string.IsNullOrEmpty(user.Email) ? "" : user.Email;

                identity.AddClaim(new Claim("email", email));

                var props = new AuthenticationProperties(new Dictionary<string, string>
                            {
                                {"email", email}
                            });
                context.Validated(new AuthenticationTicket(identity, props));

                // context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "Tài khoản hoặc mật khẩu không đúng.'");
                context.Rejected();
            }


        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }

    }
}