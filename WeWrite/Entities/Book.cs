using System.ComponentModel.DataAnnotations.Schema;

namespace WeWrite.Entities
{
    internal class Book
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        public string Title { get; set; } 
        public DateTime Created { get; set; }=DateTime.Now;

        [ForeignKey("AuthorId")]
        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        [ForeignKey("CategoryId")]
        public int? CategoryId { get; set; }
        public Category? Catagory { get; set; }

        public List<Tag>? Tags { get; set; } = new();
        public List<Charter> Charters { get; set; } = new();
        public string? Status { get; set; }
        public int? Rating { get; set; }
        public int Views { get; set; } = 0;
        public int Likes { get; set; } = 0;
    }
}
