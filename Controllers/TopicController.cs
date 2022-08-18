using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Task_for_GeeksForLess.Models;
using Test_Task_for_GeeksForLess.Other.Extensions;
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

        [HttpGet]
        public async Task<IActionResult> Show()
        {
            ShowAllTopicViewModel model = new();

            if (!db.Users.Any())
            {
                model.Topics = new List<Topic>();
                model.Count = false;
                return View(model);
            }

            model.Topics = await db.Topics.ToListAsync();
            model.Count = true;
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Show(int id = 0, string name = "")
        {
            
            ShowTopicViewModel model = new();
            Topic topic = new();
            if (!db.Users.Any())
            {
                string Error = "This topic doesn't exist yet";
                return RedirectToAction("Index", "Home", new { Error });
            }
            if (id != 0)
            {
                topic = await db.Topics.FirstAsync(x => x.Id == id);
                if (topic == null)
                {

                    string Error = "This topic doesn't exist yet";
                    return RedirectToAction("Index", "Home", new { Error });
                }

                model.Id = id;
                model.Name = topic.Name;
                model.Description = topic.Description;
                model.Created = topic.Created;
                model.UserId = topic.UserId;
                model.Posts = topic.Posts;

                return View(model);
            }

            topic = await db.Topics.FirstAsync(x => x.Name == name);
            if (topic == null)
            {

                string Error = "This topic doesn't exist yet";
                return RedirectToAction("Index", "Home", new { Error });
            }

            model.Id = id;
            model.Name = topic.Name;
            model.Description = topic.Description;
            model.Created = topic.Created;
            model.UserId = topic.UserId;
            model.Posts = topic.Posts;

            return View(model);

        }
        [HttpGet]
        public async Task<IActionResult> Create(string? Error)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTopicViewModel model)
        {
            if (!ModelState.IsValid)
            {
                string Error = "Ooops... Something went wrong.Please retype.";
                return RedirectToAction("Create", new { Error });
            }

            Topic topic = new()
            {
                Name = model.Name,
                Description = model.Description,
                Created = DateTime.Now,
                UserId = HttpContext.GetUserIdString()
            };

            await db.Topics.AddAsync(topic);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                string Error = ex.Message;
                return RedirectToAction("Create", new { Error });
            }
            return RedirectToAction("Show", new { model.Name });
        }
    }
}
