using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Common;
using AutoMapper;
using WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Application.BookOperations.GetBooks
{
    public class GetBooksQuery {
        
        private readonly BookStoreDBContext _dBContext;
        private readonly IMapper _mapper;

        public GetBooksQuery(BookStoreDBContext dBContext, IMapper mapper)
        {
            _dBContext = dBContext;
            _mapper = mapper;
        }

        public List<BookViewModel> Handle()
        {
            var bookList = _dBContext.Books.Include(x=>x.Genre).Include(x=>x.Author).OrderBy(x=> x.Id).ToList<Book>();
            List<BookViewModel> vm = _mapper.Map<List<BookViewModel>>(bookList);  // new List<BookViewModel>();
            // foreach(var book in bookList)
            // {
            //     vm.Add(new BookViewModel(){
            //         Title = book.Title,
            //         Genre = ((GenreEnum)book.GenreId).ToString(),
            //         PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy"),
            //         PageCount = book.PageCount
            //     });
            // }

            return vm;
        }


    }

    public class BookViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set;}
        public string Author { get; set;}
    }









}
