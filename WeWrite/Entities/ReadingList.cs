namespace WeWrite.Entities
{
    internal class ReadingList
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> BookList { get; set; } = new();

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ReaderId")]
        public int ReaderId { get; set; }
        public Reader? Reader { get; set; } = new();

    }
}
