using WeWrite;
using WeWrite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory());
builder.AddJsonFile("appsettings.json");
var config = builder.Build();
string connectionString = config.GetConnectionString("DefaultConnection");

var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
var options = optionsBuilder.UseSqlServer(connectionString).Options;

using (ApplicationContext db = new ApplicationContext(options))
{
    User user = new User { Username = "Zheka", Password = "zheka" };
    UserInfo userinfo = new UserInfo { Name = "Evgeniusz", User = user };
    db.Users.Add(user);
    db.UsersInfo.Add(userinfo);

    Post post1 = new Post { Id = 1, User = user, Content = "COSMOS HORROR IS SO COOOL" };
    Post post2 = new Post { Id = 1, User = user, Content = "Love cats <3" };
    db.Posts.AddRange(post1, post2);

    Category horror = new Category { Name = "Horror" };
    Category fiction = new Category { Name = "Fiction" };
    db.Categories.AddRange(horror, fiction);

    Tag tag1 = new Tag { Name = "scary" };
    Tag tag2 = new Tag { Name = "crazy" };
    Tag tag3 = new Tag { Name = "genius" };
    db.Tags.AddRange(tag1, tag2, tag3);

    Author author1 = new Author { Username = "Lovecraft", Password = "123" };
    Author author2 = new Author { Username = "King", Password = "321" };
    db.Authors.AddRange(author1, author2);

    Book book1 = new Book { Title = "Cthulhu", Catagory = horror, Author = author1, Tags = new List<Tag> { tag1, tag3 }, Status = "completed", Likes = 100 };
    Book book2 = new Book { Title = "11/22/63", Catagory = fiction, Author = author2, Tags = new List<Tag> { tag2 }, Status = "completed", Likes = 100 };
    db.Books.AddRange(book1, book2);

    Charter charter1 = new Charter { Book = book2, Title = "Wormhole" };
    Charter charter2 = new Charter { Book = book2, Title = "New town" };
    db.Charters.AddRange(charter1, charter2);   

    Reader reader = new Reader { Username = "Lexy", Password = "bigpigisrabbit"};
    db.Readers.Add(reader);
    ReadingList list = new ReadingList { Name = "Best Works", Reader = reader };
    db.ReadingLists.Add(list);

    Comment com1 = new Comment { User = reader, Charter = charter1, Content = "OMG!" };
    db.Comments.Add(com1);

    db.SaveChanges();
}

using (ApplicationContext db = new ApplicationContext(options))
{
    var books = db.Books.Include(u => u.Author).Include(u => u.Catagory).ToList();

    foreach (var book in books)
    {
        Console.WriteLine($"{book.Title} - {book.Author?.Username} - {book.Catagory?.Name}");
    }
}
