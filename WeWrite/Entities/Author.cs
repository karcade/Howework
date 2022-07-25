using System.ComponentModel.DataAnnotations.Schema;

namespace WeWrite.Entities
{
    [Table("Authors")]
    internal class Author:User
    {
        public decimal Coins { get; set; }=Decimal.Zero;
        public List<Book> Books { get; set; } = new();
    }
}
