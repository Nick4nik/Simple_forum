using Microsoft.AspNetCore.Identity;
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
            Post post = new Post
            {
                Description = postDescription,
                Created = postCreated,
                IsUpdated = postIsUpdated
            };
            Topic topic = new Topic
            {
                Name = topicName,
                Description = topicDescription,
                Created = topicCreated,
                Posts = new List<Post>() { post}
            };
            User user = new User
            {
                Email = userEmail,
                UserName = userName,
                PostCount = 1,
                TopicCount = 1,
                Topics = new List<Topic>() { topic}
            };
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
