using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Threading.Tasks;
using Test_Task_for_GeeksForLess.Models;
using Test_Task_for_GeeksForLess.Other.Extensions;
using Test_Task_for_GeeksForLess.ViewModels.Post;

namespace Test_Task_for_GeeksForLess.Controllers
{
    public class PostController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ApplicationContext db;

        public PostController(UserManager<User> userManager, ApplicationContext db)
        {
            this.userManager = userManager;
            this.db = db;
        }

        [HttpGet]
        public async Task<IActionResult> CreatePost(int id, string Error = "")
        {
            var result = await db.Topics.FirstAsync(x => x.Id == id);
            if (result == null)
            {
                Error = "This post doesn't exist yet";
                return RedirectToAction("Index", "Home", new { Error });
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
            if (model.Description == null)
            {
                string Error = "Ooops... Something went wrong.Please retype.";
                return RedirectToAction("CreatePost", new { id = model.TopicId, Error = Error });
            }

            Post post = new()
            {
                Description = model.Description,
                Created = DateTime.Now,
                IsUpdated = false,
                UserId = HttpContext.GetUserIdString(),
                TopicId = model.TopicId,
                Topic = model.Topic
            };
            await db.Posts.AddAsync(post);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                string Error = ex.Message;
                return RedirectToAction("CreatePost", new { model.TopicId, Error });
            }
            return RedirectToAction("Show", "Topic", new { model.TopicId });
        }

        [HttpGet]
        public async Task<IActionResult> EditPost(int id, string Error = "")
        {
            var result = await db.Posts.FirstAsync(x => x.Id == id);
            if (result == null)
            {
                Error = "This post doesn't exist yet";
                return RedirectToAction("Index", "Home", new { Error });
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
            if (string.IsNullOrEmpty(model.Post.Description))
            {
                string Error = "Description cannot be empty";
                return RedirectToAction("EditPost", new { model.Post.TopicId, Error });
            }

            Post post = model.Post;
            db.Posts.Update(post);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                string Error = ex.Message;
                return RedirectToAction("EditPost", new { model.Post.TopicId, Error });
            }
            return RedirectToAction("Show", "Topic", new { model.Post.TopicId });
        }
    }
}
