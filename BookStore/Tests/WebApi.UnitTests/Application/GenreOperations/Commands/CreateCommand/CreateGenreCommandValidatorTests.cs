using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.GenreOperations.Commands.CreateCommand
{
    public class CreateGenreCommandValidatorTest: IClassFixture<CommonTestFixture>
    {
        
        [Theory]
        [InlineData("")]
        [InlineData("             ")]
        [InlineData("Err")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name){
            
            // arrange
           CreateGenreCommand command = new CreateGenreCommand(null);
           command.Model = new CreateGenreModel()
           {
               Name = name
           };

            // act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            
            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
         
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateGenreCommand command = new CreateGenreCommand(null);
           command.Model = new CreateGenreModel()
            {
               Name = "Name boş olmamalı ve 4 karaktere eşit veya daha fazla olmalı"
            };

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var error = validator.Validate(command);

            error.Errors.Count.Should().Equals(0);
        }


        
    }
}