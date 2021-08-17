using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Common;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery {
        
        private readonly BookStoreDBContext _dBContext;
        public GetBooksQuery(BookStoreDBContext dBContext){
            _dBContext = dBContext;
        }

        public List<BookViewModel> Handle()
        {
            var bookList = _dBContext.Books.OrderBy(x=> x.Id).ToList<Book>();
            List<BookViewModel> vm = new List<BookViewModel>();
            foreach(var book in bookList)
            {
                vm.Add(new BookViewModel(){
                    Title = book.Title,
                    Genre = ((GenreEnum)book.GenreId).ToString(),
                    PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy"),
                    PageCount = book.PageCount
                });
            }

            return vm;
        }


    }

    public class BookViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set;}
    }









}
