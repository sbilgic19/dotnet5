using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.CreateBook;
using WebApi.Application.BookOperations.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Application.BookOperations.CreateBook.CreateBookCommand;

namespace Application.BookOperations.Commands.DeleteCommand
{
    public class DeleteBookCommandTests: IClassFixture<CommonTestFixture>
    {
        
        private readonly BookStoreDBContext _context;
        
        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            


        }

        [Theory]
        [InlineData(1)]
        
        public void WhenIdIsValid_Book_ShouldBeDeleted(int bookId)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == bookId);
            _context.Books.Remove(book);
            _context.SaveChanges();

            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = bookId;
            FluentActions
                    .Invoking(() => command.Handle())
                    .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek Kitap Bulunamadı.");


            
        }
        
    }
}