using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.CreateBook;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Commands.UpdateCommand
{
    public class UpdateCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        
       [Theory]
       [InlineData("L",0,0)]
       [InlineData("L",1,1)]
       [InlineData("Deneme",1,0)]
       [InlineData("ValidTitle",0,1)]
       public void WhenUpdateBookParametersInvalid_InvalidOperationException_ShouldBeReturned(string title, int authorId, int genreId)
       {
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model = new UpdateBookModel();
            command.Model.Title = title;
            command.Model.GenreId = genreId;
            command.Model.AuthorId = authorId;


            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var error = validator.Validate(command);

            error.Errors.Count.Should().BeGreaterThan(0);

       }

       [Theory]
       [InlineData("Deneme",1,2)]
       [InlineData("ValidTitle",3,1)]
       public void WhenUpdateBookParametersValid_BookShouldBeUpdated(string title, int authorId, int genreId)
       {
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model = new UpdateBookModel();
            command.Model.Title = title;
            command.Model.GenreId = genreId;
            command.Model.AuthorId = authorId;


            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var error = validator.Validate(command);

            error.Errors.Count.Should().Equals(0);

       }

        
    }
}