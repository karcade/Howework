using System.ComponentModel.DataAnnotations.Schema;

namespace WeWrite.Entities
{
    internal class Comment
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public int IdUser { get; set; }
        public User? User { get; set; }

        [ForeignKey("CharterId")]
        public int IdCharter { get; set; }
        public Charter Charter { get; set; }

        public string Content { get; set; }
    }
}
