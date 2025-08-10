using System.ComponentModel.DataAnnotations;
using DepresStore.Modules.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DepresStore.AuthorizationServer.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public required LoginInputModel Input { get; set; }

        public class LoginInputModel
        {
            [Required, EmailAddress]
            public required string Email { get; set; }

            [Required, DataType(DataType.Password)]
            public required string Password { get; set; }

            public bool RememberMe { get; set; }
        }

        [BindProperty(SupportsGet = true)]
        public string? ReturnUrl { get; set; }

        public IActionResult OnGet(string? returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!Url.IsLocalUrl(ReturnUrl))
            {
                ReturnUrl = Url.Content("~/");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Validation failed.");
                return Page();
            }

            _logger.LogInformation("Retrieving user.");

            var user = await _userManager.FindByEmailAsync(Input.Email);

            if (user == null)
            {
                _logger.LogWarning("Failed to retrieve user.");
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return Page();
            }

            _logger.LogInformation("Signing in user using password.");

            var result = await _signInManager.PasswordSignInAsync(
                userName: user.Email ?? user.UserName ?? string.Empty,
                password: Input.Password,
                isPersistent: Input.RememberMe,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                _logger.LogInformation("Successfully signed in user, redirecting to: {ReturnUrl}.", ReturnUrl);
                return LocalRedirect(ReturnUrl);
            }

            _logger.LogWarning("Failed to sign in user.");

            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return Page();
        }
    }
}