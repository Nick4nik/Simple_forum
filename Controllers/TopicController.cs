using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Test_Task_for_GeeksForLess.Models;
using Test_Task_for_GeeksForLess.ViewModels.Topic;

namespace Test_Task_for_GeeksForLess.Controllers
{
    public class TopicController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ApplicationContext db;

        public TopicController(UserManager<User> userManager, ApplicationContext db)
        {
            this.userManager = userManager;
            this.db = db;
        }

        //[HttpGet]
        //public async Task<IActionResult> Show(int id)
        //{
        //    if (!db.Users.Any())
        //    {

        //    }
        //}
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTopicViewModel model)
        {
            return RedirectToAction("Index");
        }

    }
}
