using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.CreateAuthor;
using WebApi.Application.BookOperations.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Commands.CreateCommand
{
    public class CreateAuthorCommandTests: IClassFixture<CommonTestFixture>
    {
        
        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;
        
        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;


        }
        [Fact]
        public void WhenAlreadyExistAuthorNameGiven_InvalidOperationException_ShouldBeReturn(){
            // arrange - hazırlama

            var author = new Author(){Name="WhenAlreadyExistAuthorNameGiven_InvalidOperationException_ShouldBeReturn", Surname="", Birthdate=new DateTime(1990,01,10)};
            _context.Authors.Add(author);
            _context.SaveChanges();


            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = new CreateAuthorModel(){Name="WhenAlreadyExistAuthorNameGiven_InvalidOperationException_ShouldBeReturn", Surname=""};



            // act - çalıştırma
            // assert - doğrulama

            FluentActions
                    .Invoking(() => command.Handle())
                    .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar zaten mevcut");

        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            // arrange
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            CreateAuthorModel model = new CreateAuthorModel()
            {
                Name = "Yeni Yazar",
                Surname = "Soyisim",
                Birthdate = new DateTime(1990,01,10)
            };
            command.Model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();


            // assert
            var author = _context.Authors.SingleOrDefault(author => author.Name == model.Name && author.Surname == model.Surname);
            author.Should().NotBeNull();
           
            author.Birthdate.Should().Be(model.Birthdate);
        }
    }
}