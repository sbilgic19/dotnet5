using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.UpdateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Commands.UpdateCommand
{
    public class UpdateAuthorCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        
       [Theory]
       [InlineData("Yazar İsim 1","So")]
       [InlineData("Yaz","Yazar Soyisim 2")]
       public void WhenUpdateAuthorParametersAreInvalid_InvalidOperationException_ShouldBeReturned(string name, string surname)
       {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = new UpdateAuthorModel()
            {
                Name = name,
                Surname = surname
            };
        


            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var error = validator.Validate(command);

            error.Errors.Count.Should().BeGreaterThan(0);

       }

       [Theory]
       [InlineData("Deneme İsim 1","Deneme Soyisim 1")]
       [InlineData("Valid İsim","Valid Soyisim")]
       public void WhenUpdateAuthorParametersValid_AuthorShouldBeUpdated(string name, string surname)
       {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = new UpdateAuthorModel()
            {
                Name = name,
                Surname = surname
            };
         


            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var error = validator.Validate(command);

            error.Errors.Count.Should().Equals(0);

       }


        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = new UpdateAuthorModel()
            {
                Name = "Geçerli İsim",
                Surname = "Geçerli Soyisim",
                Birthdate = DateTime.Now.Date
            };

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var error = validator.Validate(command);

            error.Errors.Count.Should().BeGreaterThan(0);
        }
       

        
    }
}