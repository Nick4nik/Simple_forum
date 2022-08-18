using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Test_Task_for_GeeksForLess.Models;
using Test_Task_for_GeeksForLess.Other.Extensions;
using Test_Task_for_GeeksForLess.ViewModels.Post;

namespace Test_Task_for_GeeksForLess.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationContext db;
        private readonly UserManager<User> userManager;

        public PostController(ApplicationContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> CreatePost(int id, string Error = "")
        {
            var result = await db.Topics.FirstAsync(x => x.Id == id);
            if (result == null)
            {
                Error = "This post doesn't exist yet";
                return RedirectToAction("Index", "Home", Error);
            }

            CreatePostViewModel model = new()
            {
                TopicId = id,
                Topic = result
            };

            if (Error != "")
            {
                model.Error = Error;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostViewModel model)
        {
            Post post = new()
            {
                Description = model.Description.RemoveWhiteSpace(),
                Created = DateTime.Now,
                IsUpdated = false,
                User = await userManager.FindByIdAsync(HttpContext.GetUserIdString()),
                TopicId = model.TopicId,
                Topic = model.Topic
            };
            Topic topic = post.Topic;
            topic.LastUpdate = post.Created;
            await db.Posts.AddAsync(post);
            await db.SaveChangesAsync();
            return RedirectToAction("Show", "Topic", model.TopicId);
        }

        [HttpGet]
        public async Task<IActionResult> EditPost(int id, string Error = "")
        {
            var result = await db.Posts.FirstAsync(x => x.Id == id);
            if (result == null)
            {
                Error = "This post doesn't exist yet";
                return RedirectToAction("Index", "Home", Error);
            }

            EditPostViewModel model = new()
            {
                Post = result
            };
            if (Error != "")
            {
                model.Error = Error;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditPost(EditPostViewModel model)
        {
            Post post = model.Post;
            Topic topic = post.Topic;
            post.IsUpdated = true;
            post.Updated = topic.LastUpdate = DateTime.Now;
            db.Posts.Update(post);
            db.Topics.Update(topic);
            await db.SaveChangesAsync();
            return RedirectToAction("Show", "Topic", model.Post.TopicId);
        }
    }
}
