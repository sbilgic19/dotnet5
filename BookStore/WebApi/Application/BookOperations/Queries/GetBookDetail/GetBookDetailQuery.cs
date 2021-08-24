using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Application.BookOperations.GetBookDetail
{

    public class GetBookDetailQuery
    {
        private readonly IBookStoreDBContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId { get; set; }
        public GetBookDetailQuery(IBookStoreDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle(){
            var book = _dbContext.Books.Include(x => x.Genre).Include(x=>x.Author).Where(book => book.Id == BookId).SingleOrDefault();
            if (book is null){
                throw new InvalidOperationException("Bu idye ait kitap bulunamadı");
            }

            BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book);// new BookDetailViewModel();
            // vm.Title = book.Title;
            // vm.PageCount = book.PageCount;
            // vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
            // vm.Genre = ((GenreEnum)book.GenreId).ToString();
            return vm;
        }
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set;}
        public string Author { get; set;}
        
    }






}
