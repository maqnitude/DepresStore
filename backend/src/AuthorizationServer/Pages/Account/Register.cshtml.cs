using System.ComponentModel.DataAnnotations;
using DepresStore.Modules.Identity.Domain.Entities;
using DepresStore.Shared.Kernel.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DepresStore.AuthorizationServer.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            ILogger<RegisterModel> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public required RegisterInputModel Input { get; set; }

        public class RegisterInputModel
        {
            [Required]
            [StringLength(100)]
            public required string FirstName { get; set; }

            [StringLength(100)]
            public string? LastName { get; set; }

            [Required, EmailAddress]
            [StringLength(50)]
            public required string Email { get; set; }

            [Required, DataType(DataType.Password)]
            public required string Password { get; set; }

            [Required, DataType(DataType.Password)]
            [Compare(nameof(Password), ErrorMessage = "Password and Confirm Password do not match.")]
            public required string ConfirmPassword { get; set; }
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
                return Page();
            }

            var user = new User
            {
                FirstName = Input.FirstName,
                LastName = Input.LastName,
                UserName = Input.Email,
                Email = Input.Email
            };

            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, RoleConstants.Customer);
                await _signInManager.SignInAsync(user, isPersistent: false);

                return LocalRedirect(ReturnUrl);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}