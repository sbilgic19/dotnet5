using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Common;

namespace WebApi.Application.BookOperations.DeleteBook
{

    public class DeleteBookCommand
    {
        private readonly IBookStoreDBContext _dbContext;
        public int BookId { get; set; }
        public DeleteBookCommand(IBookStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle(){
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
            {
                throw new InvalidOperationException("Silinecek Kitap Bulunamad─▒.");
            }else{
                _dbContext.Books.Remove(book);
                _dbContext.SaveChanges();
            }
        }
    }





}
