using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Simple_forum.Models;
using Simple_forum.Other.Extensions;
using Simple_forum.ViewModels.Topic;

namespace Simple_forum.Controllers
{
    public class TopicController : Controller
    {
        private readonly ApplicationContext db;
        private readonly UserManager<User> userManager;

        public TopicController(ApplicationContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> ShowAllTopic()
        {
            ShowAllTopicViewModel model = new();

            if (!db.Users.Any())
            {
                model.Topics = new List<Topic>();
                model.Count = false;
                return View(model);
            }

            model.Topics = await db.Topics.Include(x => x.User)
                .Include(x => x.Posts).ToListAsync();
            model.Count = true;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ShowTopic(int id)
        {
            ShowTopicViewModel model = new();
            Topic topic = new();
            if (!db.Users.Any())
            {
                string Error = "This topic doesn't exist yet";
                return RedirectToAction("Index", "Home", new { Error = Error });
            }
            topic = await db.Topics.Include(x => x.User).Include(x => x.Posts)
                    .FirstAsync(x => x.Id == id);
            if (topic == null)
            {
                string Error = "This topic doesn't exist yet";
                return RedirectToAction("Index", "Home", new { Error = Error });
            }

            model.Id = id;
            model.Name = topic.Name;
            model.Description = topic.Description;
            model.Created = topic.Created;
            model.UserId = topic.UserId;
            model.Posts = topic.Posts;
            model.User = topic.User;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateTopic()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTopic(CreateTopicViewModel model)
        {
            Topic topic = new()
            {
                Name = model.Name,
                Description = model.Description.RemoveWhiteSpace(),
                Created = DateTime.Now,
                LastUpdate = DateTime.Now,
                User = await userManager.FindByIdAsync(HttpContext.GetUserIdString())
            };

            await db.Topics.AddAsync(topic);
            await db.SaveChangesAsync();
            var existingTopic = await db.Topics.FirstAsync(x => x.Name == topic.Name);
            return RedirectToAction("ShowTopic", new { id = existingTopic.Id });
        }
    }
}
