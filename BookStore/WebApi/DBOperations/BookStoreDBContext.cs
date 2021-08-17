using Microsoft.EntityFrameworkCore;
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

        

    }
}