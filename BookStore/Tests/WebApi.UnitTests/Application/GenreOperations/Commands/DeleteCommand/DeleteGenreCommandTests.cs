using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.CreateBook;
using WebApi.Application.BookOperations.DeleteBook;
using WebApi.Application.GenreOperations.DeleteGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.GenreOperations.Commands.DeleteCommand
{
    public class DeleteGenreCommandTests: IClassFixture<CommonTestFixture>
    {
        
        private readonly BookStoreDBContext _context;
        
        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;

        }

        [Theory]
        [InlineData(1)]
        
        public void WhenIdIsValid_Genre_ShouldBeDeleted(int genreId)
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Id == genreId);
            _context.Genres.Remove(genre);
            _context.SaveChanges();

            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = genreId;
            FluentActions
                    .Invoking(() => command.Handle())
                    .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı");


            
        }
        
    }
}