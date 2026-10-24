using CRM.IdentityServer.Entities;
using CRM.IdentityServer.Model;
using CRM.IdentityServer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.IdentityServer.Controllers {
    [Route("api/Account")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRefreshTokenRepository _repository;

        public AccountController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IRefreshTokenRepository repository) {
            _userManager = userManager;
            _signInManager = signInManager;
            _repository = repository;
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterModel model) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new ApplicationUser {
                UserName = model.Email,
                Email = model.Email,
                Ident = model.Ident,
                Code = model.Code,
                Origin = model.Origin,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return Ok(result.Errors);

            return Ok("Success");
        }

        [HttpGet]
        [Route("LogOff/{userId}/{clientId}")]
        public async Task<IActionResult> LogOff(string userId, string clientId) {
            var result = _repository.RemoveRefreshToken(Guid.Parse(userId), clientId);

            await _signInManager.SignOutAsync();

            return Ok(result);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("ConfirmEmail/{userId}/{code}")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code) {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(code))
                return BadRequest("Userid and Code is required!");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest("User not found!");

            var result = await _userManager.ConfirmEmailAsync(user, code);

            return Ok(result.Succeeded ? "Success" : "Error");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                return BadRequest("Error while confirming!");

            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
            // Send an email with this link
            //var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            //var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
            //await _emailSender.SendEmailAsync(model.Email, "Reset Password",
            //   "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");
            //return View("ForgotPasswordConfirmation");

            return Ok("Success!");
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("User not found!");

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Success!");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("SendCode/{returnUrl}/{rememberMe}")]
        public async Task<ActionResult> SendCode(string returnUrl = null, bool rememberMe = false) {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null) 
                return BadRequest("User not found!");
            
            var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(user);
            var factorOptions = userFactors.Select(purpose => new SelectListItem{ Text = purpose, Value = purpose })
                .ToList();

            return Ok(new SendCodeModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("SendCode")]
        public async Task<IActionResult> SendCode(SendCodeModel model) {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null) 
                return BadRequest("User not found!");
            

            if (model.SelectedProvider == "Authenticator") 
                return RedirectToAction(nameof(VerifyAuthenticatorCodeModel), new { ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
            

            // Generate the token and send it
            var code = await _userManager.GenerateTwoFactorTokenAsync(user, model.SelectedProvider);
            if (string.IsNullOrWhiteSpace(code)) 
                return BadRequest("Error");
            

            //var message = "Your security code is: " + code;
            //if (model.SelectedProvider == "Email") {
            //    await _emailSender.SendEmailAsync(await _userManager.GetEmailAsync(user), "Security Code", message);
            //}
            //else if (model.SelectedProvider == "Phone") {
            //    await _smsSender.SendSmsAsync(await _userManager.GetPhoneNumberAsync(user), message);
            //}

            return RedirectToAction(nameof(VerifyCodeModel), new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("VerifyAuthenticatorCode")]
        public async Task<IActionResult> VerifyAuthenticatorCode(VerifyAuthenticatorCodeModel model) {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);            

            // The following code protects for brute force attacks against the two factor codes.
            // If a user enters incorrect codes for a specified amount of time then the user account
            // will be locked out for a specified amount of time.
            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(model.Code, model.RememberMe, model.RememberBrowser);
            if (result.Succeeded) 
                return BadRequest("Success!");
            
            if (result.IsLockedOut) 
                return BadRequest("Lockout");
            else 
                return BadRequest("Invalid code!");            
        }
    }
}
