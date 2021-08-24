using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.CreateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Commands.CreateCommand
{
    public class AuthorCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        
        [Theory]
        [InlineData("Ad", "Soyad")]
        [InlineData("Adımız", "Soy")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname){
            
            // arrange
           CreateAuthorCommand command = new CreateAuthorCommand(null, null);
           command.Model = new CreateAuthorModel()
           {
                Name = name,
                Surname = surname,
           };

            // act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            
            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
         
        }


        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel()
            {
                Name = "Orhan",
                Surname = "Pamuk",
                Birthdate = DateTime.Now.Date
            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var error = validator.Validate(command);

            error.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel()
            {
                Name = "Orhan",
                Surname = "Pamuk",
                Birthdate = DateTime.Now.Date.AddYears(-2)
            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var error = validator.Validate(command);

            error.Errors.Count.Should().Equals(0);
        }

        
    }
}