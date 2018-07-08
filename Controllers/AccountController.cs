using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using crmApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace crmApi.Controllers
{
    [Route("api/account")]
    //[ApiController]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // POST: api/Account
        [HttpPost]
        //[Route("api/Account/Register")]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                    //    "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    //_logger.LogInformation(3, "User created a new account with password.");

                    //return RedirectToAction(nameof(HomeController.Index), "Home");

                    //log off the user 
                    //AuthenticationManager.SignOut();
                    //advise him to confirm email
                    //return RedirectToAction("AdviseConfirmEmail", "Account", new { portalGuid = model.PortalGuid });
                    return new ObjectResult(result);
                }
                AddErrors(result);
                return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));
            }
            return BadRequest("Invalid content");
        }

        //// POST: api/Account/Login
        //[HttpPost("login")]
        //[AllowAnonymous]
        ////[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("Invalid content");
        //    }

        //    //// This doesn't count login failures towards account lockout
        //    //// To enable password failures to trigger account lockout, change to shouldLockout: true
        //    //var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
        //    ////var user = await UserManager.FindByNameAsync(model.UserName);
        //    ////await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //    ////var x = User.Identity.GetUserId();
        //    //if (result.Succeeded)
        //    //{
        //    //    return new ObjectResult(model);
        //    //}
        //    ////case Microsoft.AspNetCore.Identity.SignInResult.LockedOut:
        //    ////    return View("Lockout");
        //    ////case Microsoft.AspNetCore.Identity.SignInResult.RequiresVerification:
        //    ////    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
        //    ////case Microsoft.AspNetCore.Identity.SignInResult.Failure:
        //    //ModelState.AddModelError("", "Invalid login attempt.");
        //    //return BadRequest("Login failed");

        //    var identity = await GetClaimsIdentity(model.UserName, model.Password);
        //    if (identity == null)
        //    {
        //        return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
        //    }

        //    var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, model.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
        //    return new OkObjectResult(jwt);
        //}

        //private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        //{
        //    if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
        //        return await Task.FromResult<ClaimsIdentity>(null);

        //    // get the user to verifty
        //    var userToVerify = await _userManager.FindByNameAsync(userName);

        //    if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

        //    // check the credentials
        //    if (await _userManager.CheckPasswordAsync(userToVerify, password))
        //    {
        //        return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
        //    }

        //    // Credentials are invalid, or account doesn't exist
        //    return await Task.FromResult<ClaimsIdentity>(null);
        //}

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }

}