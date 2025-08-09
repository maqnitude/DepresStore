using System.Collections.Immutable;
using System.Security.Claims;
using DepresStore.Modules.Identity.Domain.Entities;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace DepresStore.AuthorizationServer.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IOpenIddictApplicationManager _applicationManager;
        private readonly IOpenIddictScopeManager _scopeManager;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AuthorizationController> _logger;

        public AuthorizationController(
            IOpenIddictApplicationManager applicationManager,
            IOpenIddictScopeManager scopeManager,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            ILogger<AuthorizationController> logger)
        {
            _applicationManager = applicationManager;
            _scopeManager = scopeManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet("~/connect/authorize")]
        [HttpPost("~/connect/authorize")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Authorize()
        {
            _logger.LogInformation("Retrieving OpenID Connect request...");
            var request = HttpContext.GetOpenIddictServerRequest() ??
                throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

            // Retrieve user principal from authentication cookie
            _logger.LogInformation("Retrieving user principal from authentication cookie");
            var result = await HttpContext.AuthenticateAsync();

            // Redirect to login page if user principal cannot be extracted
            if (!result.Succeeded)
            {
                _logger.LogWarning("Could not retrieve user principal. Redirecting to login page.");
                return Challenge(new AuthenticationProperties
                {
                    RedirectUri = Request.PathBase + Request.Path + QueryString.Create(
                        Request.HasFormContentType ? Request.Form.ToList() : Request.Query.ToList())
                });
            }

            // Retrieve profile of the logged in user
            var user = await _userManager.GetUserAsync(result.Principal) ??
                throw new InvalidOperationException("The user details cannot be retrieved.");

            // Retrieve the application details from the database
            var application = await _applicationManager.FindByClientIdAsync(request.ClientId!) ??
                throw new InvalidOperationException("Details concerning the calling application cannot be found.");

            // Create new claims identity used by OpenIddict to generate tokens
            _logger.LogInformation("Creating new claims identity");

            var claimsIdentity = new ClaimsIdentity(
                authenticationType: TokenValidationParameters.DefaultAuthenticationType,
                nameType: OpenIddictConstants.Claims.Name,
                roleType: OpenIddictConstants.Claims.Role);

            claimsIdentity
                .SetClaim(OpenIddictConstants.Claims.Subject, await _userManager.GetUserIdAsync(user))
                .SetClaim(OpenIddictConstants.Claims.Email, await _userManager.GetEmailAsync(user))
                .SetClaim(OpenIddictConstants.Claims.PreferredUsername, await _userManager.GetUserNameAsync(user))
                .SetClaim(OpenIddictConstants.Claims.Name, await _userManager.GetUserNameAsync(user))
                .SetClaims(OpenIddictConstants.Claims.Role, (await _userManager.GetRolesAsync(user)).ToImmutableArray());

            claimsIdentity.SetScopes(request.GetScopes());
            claimsIdentity.SetResources(await _scopeManager.ListResourcesAsync(claimsIdentity.GetScopes()).ToListAsync());

            claimsIdentity.SetDestinations(GetDestinations);

            // Sign in with OpenIddict server authentication scheme to issue authorization code
            _logger.LogInformation("Issuing authorization code");
            return SignIn(new ClaimsPrincipal(claimsIdentity), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        [HttpPost("~/connect/token"), Produces("application/json")]
        public async Task<IActionResult> Exchange()
        {
            var request = HttpContext.GetOpenIddictServerRequest() ??
                throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

            if (request.IsAuthorizationCodeGrantType() || request.IsRefreshTokenGrantType())
            {
                // Retrieve claims principal stored in the authorization code
                var claimsPrincipal = (await HttpContext.AuthenticateAsync(
                    OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)).Principal;

                var user = await _userManager.FindByIdAsync(claimsPrincipal!.GetClaim(OpenIddictConstants.Claims.Subject)!) ??
                    throw new InvalidOperationException("The user details cannot be retrieved.");

                var claimsIdentity = new ClaimsIdentity(claimsPrincipal!.Claims,
                    authenticationType: TokenValidationParameters.DefaultAuthenticationType,
                    nameType: OpenIddictConstants.Claims.Name,
                    roleType: OpenIddictConstants.Claims.Role);

                // Override claims in case they changed since authorization code/refresh token was issued
                claimsIdentity
                    .SetClaim(OpenIddictConstants.Claims.Subject, await _userManager.GetUserIdAsync(user))
                    .SetClaim(OpenIddictConstants.Claims.Email, await _userManager.GetEmailAsync(user))
                    .SetClaim(OpenIddictConstants.Claims.PreferredUsername, await _userManager.GetUserNameAsync(user))
                    .SetClaim(OpenIddictConstants.Claims.Name, await _userManager.GetUserNameAsync(user))
                    .SetClaims(OpenIddictConstants.Claims.Role, (await _userManager.GetRolesAsync(user)).ToImmutableArray());

                claimsIdentity.SetDestinations(GetDestinations);

                // Ask OpenIddict to issue access/identity token
                _logger.LogInformation("Issuing access/identity token");
                return SignIn(claimsPrincipal!, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            throw new InvalidOperationException("The specified grant type is not supported.");
        }

        [HttpGet("~/connect/logout")]
        public IActionResult LogOut()
        {
            return View();
        }

        [HttpPost("~/connect/logout")]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(LogOut))]
        public async Task<IActionResult> LogOutPost()
        {
            // Ask ASP.NET Core Identity to delete the cookies
            await _signInManager.SignOutAsync();

            // Ask OpenIddict to redirect user-agent to post_logout_redirect_uri (specifided by client application)
            // or to RedirectUri like below
            return SignOut(
                authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                properties: new AuthenticationProperties
                {
                    RedirectUri = "/"
                });
        }

        private static IEnumerable<string> GetDestinations(Claim claim)
        {
            switch (claim.Type)
            {
                case OpenIddictConstants.Claims.Name or OpenIddictConstants.Claims.PreferredUsername:
                    yield return OpenIddictConstants.Destinations.AccessToken;

                    if (claim.Subject!.HasScope(OpenIddictConstants.Scopes.Profile))
                    {
                        yield return OpenIddictConstants.Destinations.IdentityToken;
                    }

                    yield break;

                case OpenIddictConstants.Claims.Email:
                    yield return OpenIddictConstants.Destinations.AccessToken;

                    if (claim.Subject!.HasScope(OpenIddictConstants.Scopes.Email))
                    {
                        yield return OpenIddictConstants.Destinations.IdentityToken;
                    }

                    yield break;

                case OpenIddictConstants.Claims.Role:
                    yield return OpenIddictConstants.Destinations.AccessToken;

                    if (claim.Subject!.HasScope(OpenIddictConstants.Scopes.Roles))
                    {
                        yield return OpenIddictConstants.Destinations.IdentityToken;
                    }

                    yield break;

                // Never include the security stamp in the access and identity tokens, as it's a secret value.
                case "AspNet.Identity.SecurityStamp":
                    yield break;

                default:
                    yield return OpenIddictConstants.Destinations.AccessToken;
                    yield break;
            }
        }
    }
}