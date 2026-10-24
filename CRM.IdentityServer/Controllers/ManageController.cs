using CRM.IdentityServer.Entities;
using CRM.IdentityServer.Model;
using CRM.IdentityServer.Repositories;
using CRM.IdentityServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.IdentityServer.Controllers {
    [Route("api/Manage")]
    [ApiController]
    [Authorize]
    public class ManageController : ControllerBase {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRefreshTokenRepository _repository;
        private readonly ISmsSender _smsSender;

        public ManageController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IRefreshTokenRepository repository,
            ISmsSender smsSender) {
            _userManager = userManager;
            _signInManager = signInManager;
            _repository = repository;
            _smsSender = smsSender;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllUsersAsync() {
            var result = _userManager.Users.Where(c => c.Email != null).ToList();

            return Ok(result);
        }

        [HttpGet]
        [Route("Index/{userId}")]
        public async Task<IActionResult> Index(string userId) {
            if (string.IsNullOrEmpty(userId))
                return BadRequest("User id is required!");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest("User not found!");

            var model = new IndexModel {
                HasPassword = await _userManager.HasPasswordAsync(user),
                PhoneNumber = await _userManager.GetPhoneNumberAsync(user),
                TwoFactor = await _userManager.GetTwoFactorEnabledAsync(user),
                Logins = await _userManager.GetLoginsAsync(user),
                BrowserRemembered = await _signInManager.IsTwoFactorClientRememberedAsync(user),
                AuthenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user)
            };

            return Ok(model);
        }

        [HttpPost]
        [Route("RemoveUser")]
        public async Task<IActionResult> RemoveUserAsync(RemoveUserModel model) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return BadRequest("User not found!");

            foreach (var login in user.Logins.ToList())
                await _userManager.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey);

            await _userManager.DeleteAsync(user);

            return Ok("Success");
        }

        [HttpPost]
        [Route("RemoveLogin")]
        public async Task<IActionResult> RemoveLogin(RemoveLoginModel model) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return BadRequest("User not found!");

            var result = await _userManager.RemoveLoginAsync(user, model.LoginProvider, model.ProviderKey);
            if (!result.Succeeded)
                return BadRequest("Error!");

            await _signInManager.SignInAsync(user, isPersistent: false);
            return Ok("Success");
        }

        [HttpPost]
        [Route("AddPhoneNumber")]
        public async Task<IActionResult> AddPhoneNumber(AddPhoneNumberModel model) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return BadRequest("User not found!");

            var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, model.PhoneNumber);
            await _smsSender.SendSmsAsync(model.PhoneNumber, "Your security code is " + code);

            return Ok("Success!");
        }

        [HttpGet]
        [Route("ResetAuthenticatorKey/{userId}")]
        public async Task<IActionResult> ResetAuthenticatorKey(string userId) {
            if (string.IsNullOrEmpty(userId))
                return BadRequest("User id is required!");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest("User not found!");

            await _userManager.ResetAuthenticatorKeyAsync(user);
            return Ok("Success!");
        }

        [HttpGet]
        [Route("GenerateRecoveryCode/{userId}")]
        public async Task<IActionResult> GenerateRecoveryCode(string userId) {
            if (string.IsNullOrEmpty(userId))
                return BadRequest("User id is required!");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest("User not found!");

            var code = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 5);
            return Ok(code);
        }

        [HttpGet]
        [Route("EnableTwoFactorAuthentication/{userId}")]
        public async Task<IActionResult> EnableTwoFactorAuthentication(string userId) {
            if (string.IsNullOrEmpty(userId))
                return BadRequest("User id is required!");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest("User not found!");

            await _userManager.SetTwoFactorEnabledAsync(user, true);
            await _signInManager.SignInAsync(user, isPersistent: false);

            return Ok("Success!");
        }

        [HttpGet]
        [Route("DisableTwoFactorAuthentication/{userId}")]
        public async Task<IActionResult> DisableTwoFactorAuthentication(string userId) {
            if (string.IsNullOrEmpty(userId))
                return BadRequest("User id is required!");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest("User not found!");

            await _userManager.SetTwoFactorEnabledAsync(user, false);
            await _signInManager.SignInAsync(user, isPersistent: false);

            return Ok("Success!");
        }

        [HttpPost]
        [Route("VerifyPhoneNumber")]
        public async Task<IActionResult> VerifyPhoneNumber(VerifyPhoneNumberModel model) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return BadRequest("User not found!");

            var result = await _userManager.ChangePhoneNumberAsync(user, model.PhoneNumber, model.Code);
            if (!result.Succeeded)
                return BadRequest("Failed to verify phone number!");

            await _signInManager.SignInAsync(user, isPersistent: false);
            return Ok("Success!");
        }

        [HttpGet]
        [Route("RemovePhoneNumber/{userId}")]
        public async Task<IActionResult> RemovePhoneNumber(string userId) {
            if (string.IsNullOrEmpty(userId))
                return BadRequest("User id is required!");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest("User not found!");

            var result = await _userManager.SetPhoneNumberAsync(user, null);
            if (!result.Succeeded)
                return BadRequest("Failed to remove phone number!");

            await _signInManager.SignInAsync(user, isPersistent: false);
            return Ok("Success!");
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return BadRequest("User not found!");

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!result.Succeeded)
                return BadRequest("Failed to change password!");

            await _signInManager.SignInAsync(user, isPersistent: false);
            return Ok("Success!");
        }

        [HttpPost]
        [Route("SetPassword")]
        public async Task<IActionResult> SetPassword(SetPasswordModel model) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return BadRequest("User not found!");

            var result = await _userManager.AddPasswordAsync(user, model.NewPassword);
            if (!result.Succeeded)
                return BadRequest("Failed to change password!");

            await _signInManager.SignInAsync(user, isPersistent: false);
            return Ok("Success!");
        }

    }
}
