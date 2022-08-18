using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test_Task_for_GeeksForLess.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public List<Topic> Topics { get; set; }
        public List<Post> Posts { get; set; }
    }
}