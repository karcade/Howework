using System.ComponentModel.DataAnnotations.Schema;

namespace WeWrite.Entities
{
    internal class Charter
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }

        [ForeignKey("BookId")]
        public int BookId { get; set; }
        public Book? Book { get; set; }

        public string? Title { get; set; }
        public int? PageAmount { get; set; }
        public int? Likes { get; set; }
        public List<Comment> Comments { get; set; } = new();
    }
}
