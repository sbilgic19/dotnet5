using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Common;

namespace WebApi.BookOperations.UpdateBook
{
    
    public class UpdateBookCommand
    {
        private readonly BookStoreDBContext _dbContext;
        public int BookId { get; set; }
        public UpdateBookModel Model { get; set; }

        public UpdateBookCommand(BookStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault( x=> x.Id == BookId);
            if(book is null)
            {
                throw new InvalidOperationException("Güncellenecek kitap bulunamadı");
            }else{
                book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
                book.Title = Model.Title != default ? Model.Title : book.Title;

                _dbContext.SaveChanges();
            }

        }
    }

    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }

    }












}