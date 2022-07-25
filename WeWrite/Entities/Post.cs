using System.ComponentModel.DataAnnotations.Schema;

namespace WeWrite.Entities
{
    internal class Post
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User? User { get; set; }

        public string Content { get; set; }
        public int Likes { get; set; }=0;
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
