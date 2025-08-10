using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Client.AspNetCore;

namespace DepresStore.Storefront.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpGet("~/login")]
        public IActionResult LogIn(string? returnUrl = null)
        {
            var properties = new AuthenticationProperties
            {
                // Only allow local return URLs to prevent redirect attacks
                RedirectUri = Url.IsLocalUrl(returnUrl) ? returnUrl : "/"
            };

            // Ask OpenIddict client middleware to redirect user-agent to identity provider (authorization server)
            return Challenge(properties, OpenIddictClientAspNetCoreDefaults.AuthenticationScheme);
        }

        [HttpPost("~/logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut(string? returnUrl = null)
        {
            // Retrieve the identity stored in the authentication cookie
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // User is logged out locally or not logged in
            if (!result.Succeeded)
            {
                // Only allow local return URLs to prevent redirect attacks
                return Redirect(Url.IsLocalUrl(returnUrl) ? returnUrl : "/");
            }

            // Remove the local authentication cookie before triggerring a redirection
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.IsLocalUrl(returnUrl) ? returnUrl : "/"
            };

            // Ask OpenIddict client middleware to redirect user-agent to identity provider (authorization server)
            return SignOut(properties, OpenIddictClientAspNetCoreDefaults.AuthenticationScheme);
        }

        [HttpGet("/callback/login/{provider}")]
        [HttpPost("/callback/login/{provider}")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> LogInCallback()
        {
            // Retrieve the authorization data validated by OpenIddict as part of the callback handling
            var result = await HttpContext.AuthenticateAsync(OpenIddictClientAspNetCoreDefaults.AuthenticationScheme);

            if (!result.Succeeded || !result.Principal.Identity!.IsAuthenticated)
            {
                throw new InvalidOperationException("The external authorization data cannot be used for authentication.");
            }

            // Build a claims identity based on external claims that will be used to create the authentication cookie
            var claimsIdentity = new ClaimsIdentity(
                authenticationType: "ExternalLogin",
                nameType: ClaimTypes.Name,
                roleType: ClaimTypes.Role);

            claimsIdentity
                .SetClaim(ClaimTypes.NameIdentifier, result.Principal.GetClaim(ClaimTypes.NameIdentifier))
                .SetClaim(ClaimTypes.Email, result.Principal.GetClaim(ClaimTypes.Email))
                .SetClaim(ClaimTypes.Name, result.Principal.GetClaim(ClaimTypes.Name));

            // Preserve registration details to be able to resolve them later
            claimsIdentity
                .SetClaim(OpenIddictConstants.Claims.Private.RegistrationId,
                    result.Principal.GetClaim(OpenIddictConstants.Claims.Private.RegistrationId))
                .SetClaim(OpenIddictConstants.Claims.Private.ProviderName,
                    result.Principal.GetClaim(OpenIddictConstants.Claims.Private.ProviderName));

            // Build the authentication properties based on the properties that were added when the challange was triggered
            var properties = new AuthenticationProperties
            {
                RedirectUri = result.Properties.RedirectUri ?? "/",

                // Set to null to have the same lifetime as the identity token from the authorization server
                IssuedUtc = null,
                ExpiresUtc = null,

                // Treat as session cookie
                IsPersistent = false
            };

            // Filter tokens to make cooke less heavy
            properties.StoreTokens(result.Properties.GetTokens().Where(token => token.Name is
                OpenIddictClientAspNetCoreConstants.Tokens.BackchannelAccessToken or
                OpenIddictClientAspNetCoreConstants.Tokens.BackchannelIdentityToken or
                OpenIddictClientAspNetCoreConstants.Tokens.RefreshToken));

            return SignIn(new ClaimsPrincipal(claimsIdentity), properties);
        }

        [HttpGet("~/callback/logout/{provider}")]
        [HttpPost("~/callback/logout/{provider}")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> LogOutCallback()
        {
            // Retrieve the data stored by OpenIddict in the state token created when logout was triggered
            var result = await HttpContext.AuthenticateAsync(OpenIddictClientAspNetCoreDefaults.AuthenticationScheme);

            return Redirect(result.Properties?.RedirectUri ?? "/");
        }
    }
}