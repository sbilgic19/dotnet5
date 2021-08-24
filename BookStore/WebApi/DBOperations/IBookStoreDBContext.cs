using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public interface IBookStoreDBContext{
        DbSet<Book> Books { get; set;}
        DbSet<Genre> Genres { get; set;}
        DbSet<Author> Authors { get; set;}

        DbSet<User> Users { get; set;}
        int SaveChanges();
    }
}