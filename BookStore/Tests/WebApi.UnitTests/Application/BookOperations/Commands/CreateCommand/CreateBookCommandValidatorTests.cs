using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Application.BookOperations.CreateBook.CreateBookCommand;

namespace Application.BookOperations.Commands.CreateCommand
{
    public class CreateCommandValidatorTest: IClassFixture<CommonTestFixture>
    {
        
        [Theory]
        [InlineData("Lord Of The Rings", 0, 0)]
        [InlineData("Lord Of The Rings", 0, 1)]
        [InlineData("", 0, 0)]
        [InlineData("", 100, 1)]
        [InlineData("", 0, 1)]
        [InlineData("Lor", 100, 1)]
        //[InlineData("Lord of the Rings", 1100, 1)]
        [InlineData("      ", 120, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId){
            
            // arrange
           CreateBookCommand command = new CreateBookCommand(null, null);
           command.Model = new CreateBookModel()
           {
                Title = title,
                PageCount=pageCount, 
                PublishDate=DateTime.Now.Date.AddYears(-1),
                AuthorId = 2, 
                GenreID=genreId
           };

            // act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            
            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
         
        }


        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Lords of the Ring",
                PageCount =  1000,
                GenreID = 1,
                AuthorId = 2,
                PublishDate = DateTime.Now.Date
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var error = validator.Validate(command);

            error.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Lords of the Ring",
                PageCount =  1000,
                GenreID = 1,
                AuthorId = 2,
                PublishDate = DateTime.Now.Date.AddYears(-2)
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var error = validator.Validate(command);

            error.Errors.Count.Should().Equals(0);
        }

        
    }
}