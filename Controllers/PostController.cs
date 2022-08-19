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
        public async Task<IActionResult> CreatePost(int id)
        {
            var result = await db.Topics.FirstAsync(x => x.Id == id);
            if (result == null)
            {
                string Error = "This topic doesn't exist yet";
                return RedirectToAction("Index", "Home", Error);
            }

            CreatePostViewModel model = new()
            {
                TopicId = id,
                Topic = result
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostViewModel model)
        {
            var now = DateTime.Now;
            Post post = new()
            {
                Description = model.Description.RemoveWhiteSpace(),
                Created = now,
                IsUpdated = false,
                User = await userManager.FindByIdAsync(HttpContext.GetUserIdString()),
            };
            Topic topic = await db.Topics.FirstAsync(x => x.Id == model.TopicId);
            post.Topic = topic;
            post.TopicId = topic.Id;
            topic.LastUpdate = now;

            db.Topics.Update(topic);
            await db.Posts.AddAsync(post);
            await db.SaveChangesAsync();
            return RedirectToAction("Show", "Topic", new { model.TopicId });
        }

        [HttpGet]
        public async Task<IActionResult> EditPost(int id)
        {
            var result = await db.Posts.FirstAsync(x => x.Id == id);
            if (result == null)
            {
                string Error = "This post doesn't exist yet";
                return RedirectToAction("Index", "Home", Error);
            }

            EditPostViewModel model = new()
            {
                Post = result
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditPost(EditPostViewModel model)
        {
            var now = DateTime.Now;
            Post post = await db.Posts.FirstAsync(x => x.Id == model.Post.Id);
            post.Description = model.Post.Description;
            post.IsUpdated = true;
            post.Updated = now;
            Topic topic = await db.Topics.FirstAsync(x => x.Id == model.Post.TopicId);
            topic.LastUpdate = now;

            db.Posts.Update(post);
            db.Topics.Update(topic);
            await db.SaveChangesAsync();
            return RedirectToAction("ShowTopic", "Topic", new { topic.Id });
        }
    }
}
