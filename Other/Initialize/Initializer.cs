using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Task_for_GeeksForLess.Models;

namespace Test_Task_for_GeeksForLess.Initialize
{
    public static class Initializer
    {
        public static async Task Initialize(UserManager<User> userManager, ApplicationContext db)
        {
            if (db.Users.Any()) return;

            #region Initialize list
            string userEmail = "user@gmail.com";
            string userName = "User";
            string userPassword = "Qwe123";
            string postDescription = "Post description";
            DateTime postCreated = DateTime.Now;
            bool postIsUpdated = false;
            string topicName = "Topic name";
            string topicDescription = "Topic description";
            DateTime topicCreated = postCreated;
            DateTime topicLastUpdate = postCreated;
            Post post = new Post
            {
                Description = postDescription,
                Created = postCreated,
                IsUpdated = postIsUpdated,
                Topic = new Topic()
            };
            Topic topic = new Topic
            {
                Name = topicName,
                Description = topicDescription,
                Created = topicCreated,
                LastUpdate = topicLastUpdate,
                Posts = new List<Post>()
            };
            User user = new User
            {
                Email = userEmail,
                UserName = userEmail,
                Name = userName,
                Posts = new List<Post>(),
                Topics = new List<Topic>()
            };
            post.Topic = topic;
            post.User = user;
            topic.Posts.Add(post);
            topic.User = user;
            user.Posts.Add(post);
            user.Topics.Add(topic);
            #endregion

            if (await userManager.FindByNameAsync(userEmail) == null)
            {
                user.Topics.Add(topic);
                db.SaveChanges();
                await userManager.CreateAsync(user, userPassword);
            }
        }
    }
}
