using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApi.DBOperations;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            // DataGenerator'un tetiklenmesi burada gerçekleşiyor. 
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                DataGenerator.Initialize(services); // datayı insert etme
            }
            host.Run();


            /*
            Linq ile CRUD(Create, Read, Update, Delete) İşlemleri
            

            // Find

            BookStoreDBContext _context = new BookStoreDBContext();
            var books = _context.Books.ToList<Book>();

            var book = _context.Books.Where(book => book.Id == 1).FirstOrDefault();
            book = _context.Books.Find(1);
            Console.WriteLine(book.Title);

            // FirstOrDefault, veri bulamazsa null döndürür
            book = _context.Books.Where(book => book.Title == "Harald").FirstOrDefault();
            book = _context.Books.FirstOrDefault(x => x.GenreId == 2);

            // First eleman bulamazsa hata fırlatır
            book = _context.Books.First(x => x.GenreId == 2);


            // SingleOrDefault
            book = _context.Books.SingleOrDefault(book => book.GenreId == 1);

            // ToList

            var bookList = _context.Books.Where(book => book.GenreId == 2).ToList;
            foreach( var i in bookList)
            {
                Console.WriteLine(i);
            }

            // OrderBy
            books = _context.Books.OrderBy(X => X.Id).ToList();


            // OrderByDescending
            books = _context.Books.OrderByDescending(X => X.Id).ToList();
            
            // Anonymous Object Result

            var anonymousObject = _context.Books.Where(x => x.Id == 2).Select(x=> new{
                                                                            Id = x.Id,
                                                                            Full = x.GenreId + " "+x.PageCount
                                                                        });

            foreach( var a in anonymousObject)
            {
                Console.WriteLine(a.Id + " - "+ a.Full);
            }
            */
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
