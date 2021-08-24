using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.UpdateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Commands.UpdateCommand
{
    public class UpdateAuthorCommandTests: IClassFixture<CommonTestFixture>
    {
        
        private readonly BookStoreDBContext _context;
        
        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;

        }

        [Theory]
        [InlineData("Yeni Yazar İsim 1","Yeni Yazar Soyisim 1")]
        [InlineData("Yeni Yazar İsim 2","Yeni Yazar Soyisim 2")]
        public void WhenValidInputsAreGiven_Author_ShouldBeUpdated(string name, string surname)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = 3;

            command.Model = new UpdateAuthorModel()
            {
                Name = name,
                Surname = surname,
                Birthdate = DateTime.Now.Date.AddYears(-4)
            };
           
            

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var author = _context.Authors.SingleOrDefault(author => author.Name == command.Model.Name && author.Surname == command.Model.Surname );
            author.Should().NotBeNull();
            author.Birthdate.Should().Be(command.Model.Birthdate);

            
        }
        
       
        
    }
}