using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeWrite.Entities
{
    internal class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserInfo? Profile { get; set; }
        public List<Post> Posts { get; set; } = new();
        public List<Comment> Comments { get; set; }=new();
    }
}
