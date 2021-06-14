using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using itraTsk4secondTry.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace itraTsk4secondTry.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;
        public HomeController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignIn(string returnUrl = null)
        {
            return View(new UserLog { ReturnUrl = returnUrl });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Block(string[] selectedItems)
        {
            if (await LogoutUser()) return RedirectToAction("SignIn");
            foreach (var i in selectedItems)
            {
                User user = await _userManager.FindByIdAsync(i);
                user.Status = true;
                await _userManager.UpdateAsync(user);
            }
            await LogoutUser();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UnBlock(string[] selectedItems)
        {
            if (await LogoutUser()) return RedirectToAction("SignIn");
            foreach (var i in selectedItems)
            {
                User user = await _userManager.FindByIdAsync(i);
                user.Status = false;
                await _userManager.UpdateAsync(user);
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string[] selectedItems)
        {
            if (await LogoutUser()) return RedirectToAction("SignIn");
            foreach (var i in selectedItems)
            {
                User user = await _userManager.FindByIdAsync(i);
                await _userManager.DeleteAsync(user);
            }
            await LogoutUser();
            return Ok();
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Account()
        {
            if (await LogoutUser()) return RedirectToAction("SignIn");
            return View(await _userManager.Users.ToListAsync());
        }

       
       

        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn");
        }

        public async Task<bool> LogoutUser()
        {
            User user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            if (user == null) await _signInManager.SignOutAsync();
            else if (user.Status) await _signInManager.SignOutAsync();
            return (user == null || user.Status);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(UserReg userReg)
        {
            string msg = await Task.Run(()=> Checker.checkRegistrarion(_userManager, userReg));
            if (msg==string.Empty)
            {
                User user = new User(userReg);
                var result = await _userManager.CreateAsync(user, userReg.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    TempData["Sucess"] = "Successful registration!";
                    return RedirectToAction("SignIn");
                }
                else
                    foreach (var i in result.Errors)
                        msg += i.Description;
            }
            TempData["Danger"] = msg;
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(UserLog userLog)
        {
            var user = await _userManager.FindByEmailAsync(userLog.Email);
            string msg = await Task.Run(() => Checker.checkLogin(user));
            if (msg==string.Empty)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, userLog.Password, false, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(userLog.ReturnUrl) && Url.IsLocalUrl(userLog.ReturnUrl))   return Redirect(userLog.ReturnUrl);
                    else
                    {
                        user.AuthDate = userLog.GetDate();
                        await _userManager.UpdateAsync(user);
                        return RedirectToAction("Account");
                    }
                }
                else  msg = "Invalid password";
            }
            TempData["Danger"] = msg;
            return View(userLog);
        }
    }
}