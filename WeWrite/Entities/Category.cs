namespace WeWrite.Entities
{
    internal class Category
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books  { get; set; } = new();
    }
}
