using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Security.Claims;

namespace Transportadora.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel, IEquatable<LoginModel>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public LoginModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<IdentityUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", (ReturnUrl: returnUrl, Input.RememberMe));
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        public override bool Equals(object obj)
        {
            return obj is LoginModel model &&
                   EqualityComparer<UserManager<IdentityUser>>.Default.Equals(_userManager, model._userManager);
        }

        public bool Equals(LoginModel other)
        {
            return other != null &&
                   EqualityComparer<HttpContext>.Default.Equals(HttpContext, other.HttpContext) &&
                   EqualityComparer<IModelMetadataProvider>.Default.Equals(MetadataProvider, other.MetadataProvider) &&
                   EqualityComparer<ModelStateDictionary>.Default.Equals(ModelState, other.ModelState) &&
                   EqualityComparer<PageContext>.Default.Equals(PageContext, other.PageContext) &&
                   EqualityComparer<HttpRequest>.Default.Equals(Request, other.Request) &&
                   EqualityComparer<HttpResponse>.Default.Equals(Response, other.Response) &&
                   EqualityComparer<RouteData>.Default.Equals(RouteData, other.RouteData) &&
                   EqualityComparer<ITempDataDictionary>.Default.Equals(TempData, other.TempData) &&
                   EqualityComparer<IUrlHelper>.Default.Equals(Url, other.Url) &&
                   EqualityComparer<ClaimsPrincipal>.Default.Equals(User, other.User) &&
                   EqualityComparer<ViewDataDictionary>.Default.Equals(ViewData, other.ViewData) &&
                   EqualityComparer<UserManager<IdentityUser>>.Default.Equals(_userManager, other._userManager) &&
                   EqualityComparer<SignInManager<IdentityUser>>.Default.Equals(_signInManager, other._signInManager) &&
                   EqualityComparer<ILogger<LoginModel>>.Default.Equals(_logger, other._logger) &&
                   EqualityComparer<InputModel>.Default.Equals(Input, other.Input) &&
                   EqualityComparer<IList<AuthenticationScheme>>.Default.Equals(ExternalLogins, other.ExternalLogins) &&
                   ReturnUrl == other.ReturnUrl &&
                   ErrorMessage == other.ErrorMessage;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(HttpContext);
            hash.Add(MetadataProvider);
            hash.Add(ModelState);
            hash.Add(PageContext);
            hash.Add(Request);
            hash.Add(Response);
            hash.Add(RouteData);
            hash.Add(TempData);
            hash.Add(Url);
            hash.Add(User);
            hash.Add(ViewData);
            hash.Add(_userManager);
            hash.Add(_signInManager);
            hash.Add(_logger);
            hash.Add(Input);
            hash.Add(ExternalLogins);
            hash.Add(ReturnUrl);
            hash.Add(ErrorMessage);
            return hash.ToHashCode();
        }

        public static bool operator ==(LoginModel left, LoginModel right)
        {
            return EqualityComparer<LoginModel>.Default.Equals(left, right);
        }

        public static bool operator !=(LoginModel left, LoginModel right)
        {
            return !(left == right);
        }
    }
}