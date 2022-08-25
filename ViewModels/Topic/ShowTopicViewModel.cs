using System;
using System.Collections.Generic;
using Simple_forum.Models;

namespace Simple_forum.ViewModels.Topic
{
    public class ShowTopicViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public List<Models.Post> Posts { get; set; }
    }
}
