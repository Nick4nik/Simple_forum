using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Test_Task_for_GeeksForLess.Models;

namespace Test_Task_for_GeeksForLess.ViewModels.Topic
{
    public class ShowAllTopicViewModel
    {
        public List<Models.Topic> Topics { get; set; }
        public bool Count { get; set; }
        public List<string> UserAuthor { get; set; }
        public List<string> UserUpdate { get; set; }
    }
}
