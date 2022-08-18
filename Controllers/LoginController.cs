using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Test_Task_for_GeeksForLess.Models;
using Test_Task_for_GeeksForLess.ViewModels.Login;

namespace Test_Task_for_GeeksForLess.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Login(bool error = false)
        {
            LoginRegisterViewModel model = new LoginRegisterViewModel();
            if (error)
            {
                model.Error = true;
                return View(model);
            }
            model.Error = false;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRegisterViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.LoginEmail);
            var result = await signInManager.PasswordSignInAsync(
                user.UserName, model.LoginPassword, model.LoginRememberMe, false);
            if (!result.Succeeded)
            {
                model.Error = true;
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(LoginRegisterViewModel model)
        {
            try
            {
                User user = new User
                {
                    Email = model.RegisterEmail,
                    UserName = model.RegisterEmail,
                    Name = model.RegisterName
                };
                var result = await userManager.CreateAsync(user, model.RegisterPassword);

                if (!result.Succeeded)
                {
                    model.Error = true;
                    return View("Login", model);
                }


                await signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                model.Error = true;
                return View("Login", model);
            }
            
        }
    }
}
