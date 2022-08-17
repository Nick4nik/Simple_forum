using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Test_Task_for_GeeksForLess.Models;

namespace Test_Task_for_GeeksForLess.Controllers
{
    public class PostController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public PostController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
    }
}
