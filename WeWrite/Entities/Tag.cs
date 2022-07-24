namespace WeWrite.Entities
{
    internal class Tag
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; } = new();
    }
}
