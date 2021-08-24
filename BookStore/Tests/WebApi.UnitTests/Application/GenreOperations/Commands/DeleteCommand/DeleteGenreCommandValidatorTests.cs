using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.DeleteBook;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.Application.GenreOperations.DeleteGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;


namespace Application.GenreOperations.Commands.DeleteCommand
{
    public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        
        [Theory]
        [InlineData(-2)]
        [InlineData(0)]
        [InlineData(-3)]
        public void WhenIDIsInvalidNotGreaterThanZero_InvalidOperationException_ShouldBeReturned(int genreId)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = genreId;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();

            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);

            
        }

        
    }
}