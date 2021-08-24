using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Application.BookOperations.CreateBook.CreateBookCommand;

namespace Application.BookOperations.Commands.DeleteCommand
{
    public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        
        [Theory]
        [InlineData(-2)]
        [InlineData(0)]
        [InlineData(-3)]
        public void WhenIDIsInvalidNotGreaterThanZero_InvalidOperationException_ShouldBeReturned(int bookId)
        {
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = bookId;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();

            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);

            
        }

        
    }
}