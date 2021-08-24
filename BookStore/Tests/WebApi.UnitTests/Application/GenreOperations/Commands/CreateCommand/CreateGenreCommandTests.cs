using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.CreateBook;
using WebApi.Application.GenreOperations.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Application.BookOperations.CreateBook.CreateBookCommand;

namespace Application.GenreOperations.Commands.CreateCommand
{
    public class CreateGenreCommandTests: IClassFixture<CommonTestFixture>
    {
        
        private readonly BookStoreDBContext _context;
        
        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        
        public void WhenAlreadyExistGenreNameGiven_InvalidOperationException_ShouldBeReturn()
        {
            var genre = new Genre(){Name = "Deneme"};
            _context.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = new CreateGenreModel(){Name = "Deneme"};


             FluentActions
                    .Invoking(() => command.Handle())
                    .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü zaten mevcut");

        }
        
        [Fact]
        public  void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            CreateGenreCommand command = new CreateGenreCommand(_context);
            CreateGenreModel model = new CreateGenreModel(){Name = "Deneme2"};
            
            command.Model = model;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var genre = _context.Genres.SingleOrDefault(genre => genre.Name == model.Name );
            genre.Should().NotBeNull();
        }
    }
}