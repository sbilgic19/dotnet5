using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Common;
using AutoMapper;

namespace WebApi.BookOperations.CreateBook
{
    
    public class CreateBookCommand {
        public CreateBookModel Model { get; set; }
        private readonly IMapper _mapper;

        private readonly BookStoreDBContext _dbContext;
        public CreateBookCommand(BookStoreDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (book is not null)
            {
                throw new InvalidOperationException("Kitap zaten mevcut");
            }else{
                // book = new Book();
                // book.Title = Model.Title;
                // book.PublishDate = Model.PublishDate;
                // book.PageCount = Model.PageCount;
                // book.GenreId = Model.GenreID;

                book = _mapper.Map<Book>(Model); // Model ile gelen veriyi book objesine çevir

                _dbContext.Books.Add(book);
                _dbContext.SaveChanges();

        }

    }

    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreID { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
}
