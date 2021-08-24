using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
// DB operasyonları için kullanılacak dbcontext
namespace WebApi.DBOperations{

    public class BookStoreDBContext : DbContext, IBookStoreDBContext
    {
        public BookStoreDBContext(DbContextOptions<BookStoreDBContext> options) : base(options)
        { }
        public BookStoreDBContext()
        {}

        // BookController Book entity 
        // entity isimleri tekil, dbde oluşturulan isimler çoğul 
        public DbSet<Book> Books {get; set;}
        public DbSet<Genre> Genres {get; set;}
        public DbSet<Author> Authors {get; set;}
        public DbSet<User> Users {get; set;}


        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
        

    }
}