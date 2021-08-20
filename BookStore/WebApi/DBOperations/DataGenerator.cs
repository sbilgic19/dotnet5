using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;
// Initial data için DataGenerator yazılır
namespace WebApi.DBOperations
{
    // Başlangıçta hazır datalarla başlamak için oluşturulur.
    public class DataGenerator
    {
        // IServiceProvider -> Program.cs içerisindeki yapı aracılığıyla her çalıştığında bunu çalıştıracak.
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDBContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDBContext>>()))
            {
                // Dbde data varsa çalıştırma
                if(context.Books.Any())
                {
                    return;
                // dbde data yoksa default datayla başlat
                }else{
                    context.Authors.AddRange(
                        new Author{
                            Name = "İsim Test 1",
                            Surname = "Soyisim Test 1",
                            Birthdate = new DateTime(2001, 06,12)
                        },
                        new Author{
                            Name = "İsim Test 2",
                            Surname = "Soyisim Test 2",
                            Birthdate = new DateTime(2002, 06,12)
                        },
                        new Author{
                            Name = "İsim Test 3",
                            Surname = "Soyisim Test 3",
                            Birthdate = new DateTime(2003, 06,12)
                        }

                    );
                    context.Genres.AddRange(
                        new Genre{
                            Name = "PersonalGrowth",

                        },
                        new Genre{
                            Name = "SciFi",
                            
                        },
                        new Genre{
                            Name = "Novel",
                            
                        }
                    );
                    // Book'a liste eklemek için AddRange
                    context.Books.AddRange(
                        new Book{
                            //Id = 1, autogenerator yapıldı
                            Title = "Lean StartUp",
                            AuthorId = 1,
                            GenreId = 1, // Personal Development
                            PageCount = 200,
                            PublishDate = new DateTime(2001, 06,12)
                        },
                        new Book{
                            //Id = 2,
                            Title = "Herland",
                            GenreId = 2, // Sci-fi
                            AuthorId = 2,
                            PageCount = 250,
                            PublishDate = new DateTime(2010, 05,23)
                        },
                        new Book{
                            // Id = 3,
                            Title = "Dune",
                            GenreId = 2, // Sci-fi
                            AuthorId = 3,
                            PageCount = 540,
                            PublishDate = new DateTime(2001, 12,21)
                        }
                    );
                }
                context.SaveChanges(); // dbye kaydolmasını sağlıyor.
            }
        } 
    }
}