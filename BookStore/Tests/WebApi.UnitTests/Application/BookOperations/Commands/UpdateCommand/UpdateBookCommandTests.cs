using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Commands.UpdateCommand
{
    public class UpdateBookCommandTests: IClassFixture<CommonTestFixture>
    {
        
        private readonly BookStoreDBContext _context;
        
        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;

        }

        [Theory]
        [InlineData("Deneme2",3,2)]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated(string title, int authorId, int genreId)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 3; 

            command.Model = new UpdateBookModel();
            command.Model.Title = title;
            command.Model.GenreId = genreId;
            command.Model.AuthorId = authorId;
            

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var book = _context.Books.SingleOrDefault(book => book.Title == command.Model.Title );
            book.Should().NotBeNull();
            book.GenreId.Should().Be(command.Model.GenreId);
            book.Title.Should().Be(command.Model.Title);
            book.AuthorId.Should().Be(command.Model.AuthorId);

            
        }
       
        
    }
}