using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.DeleteAuthor;
using WebApi.Application.BookOperations.CreateBook;
using WebApi.Application.BookOperations.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Commands.DeleteCommand
{
    public class DeleteAuthorCommandTests: IClassFixture<CommonTestFixture>
    {
        
        private readonly BookStoreDBContext _context;
        
        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            


        }

        [Theory]
        [InlineData(1)]
        
        public void WhenIdIsValid_Author_ShouldBeDeleted(int authorId)
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == authorId);
            _context.Authors.Remove(author);
            _context.SaveChanges();

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = authorId;
            FluentActions
                    .Invoking(() => command.Handle())
                    .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar bulunamadı");


            
        }
        
    }
}