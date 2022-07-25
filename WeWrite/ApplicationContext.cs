using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeWrite.Entities;

namespace WeWrite
{
    class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Reader> Readers { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Charter> Charters { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<ReadingList> ReadingLists { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<UserInfo> UsersInfo { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Reader>();
            modelBuilder.Entity<Author>();
            modelBuilder.Entity<Book>().HasKey(u => u.Id);
            modelBuilder.Entity<Category>().HasKey(u => u.Id);
            modelBuilder.Entity<Charter>().HasKey(u => u.Id);
            modelBuilder.Entity<Comment>().HasKey(u => u.Id);
            modelBuilder.Entity<Post>().HasKey(u => u.Id);
            modelBuilder.Entity<ReadingList>().HasKey(u => u.Id);
            modelBuilder.Entity<Tag>().HasKey(u => u.Id);
            modelBuilder.Entity<UserInfo>().HasKey(u => u.Id);


            modelBuilder.Entity<Book>()
            .HasOne(u => u.Author)
            .WithMany(c => c.Books)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Book>()
                .HasMany(c => c.Tags)
                .WithMany(s => s.Books)
                .UsingEntity(j => j.ToTable("BooksTags"));
        }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
