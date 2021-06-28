using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Transportadora.Data;
using Transportadora.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace Transportadora.Areas.Identity.Pages.Account
{
     [AllowAnonymous]
    //[Authorize(Roles = "funcionario")]
    public class RegisterfModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly ApplicationDbContext _context;

        public RegisterfModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            ApplicationDbContext context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            // [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            //[DataType(DataType.Password)]
            //[Display(Name = "Confirm password")]
            //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            //public string ConfirmPassword { get; set; }

            public Funcionarios Funcionario { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    var claim = new System.Security.Claims.Claim("Nome", Input.Funcionario.Nome);
                    await _userManager.AddClaimAsync(user, claim);
                    await _userManager.AddToRoleAsync(user, "funcionario");
                    _logger.LogInformation("User created a new account with password.");
                    try
                    {
                        Funcionarios novoFuncionario = new Funcionarios
                        {
                            Nome = Input.Funcionario.Nome,
                            UserNameId = user.Id,
                            Morada = Input.Funcionario.Morada,
                            CodPostal = Input.Funcionario.CodPostal,
                            Contacto = Input.Funcionario.Contacto,
                            Cargo = Input.Funcionario.Cargo
                        };

                        _context.Add(novoFuncionario);
                        await _context.SaveChangesAsync();


                    }
                    catch (Exception)
                    {
                        await _userManager.RemoveFromRoleAsync(user, "funcionario");
                        await _userManager.RemoveClaimAsync(user, claim);
                        await _userManager.DeleteAsync(user);

                        // falta aqui uma msg de erro para o utilizador

                        return RedirectToAction("Index", "Home");
                    }

                    // se cheguei aqui é pq o trabalho que devia ser feito foi feito.
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
