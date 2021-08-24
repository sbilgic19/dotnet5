using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.Application.AuthorOperations.DeleteAuthor;
using WebApi.Application.BookOperations.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Commands.DeleteCommand
{
    public class DeleteAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        
        [Theory]
        [InlineData(-2)]
        [InlineData(0)]
        [InlineData(-3)]
        public void WhenIDIsInvalidNotGreaterThanZero_InvalidOperationException_ShouldBeReturned(int authorId)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(null);
            command.AuthorId = authorId;

            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();

            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);

            
        }

        
    }
}