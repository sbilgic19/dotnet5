using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
// DB operasyonları için kullanılacak dbcontext
namespace WebApi.DBOperations{

    public class BookStoreDBContext : DbContext
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


        

    }
}