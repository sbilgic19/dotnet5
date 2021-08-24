using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.UpdateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.GenreOperations.Commands.UpdateCommand
{
    public class UpdateGenreCommandTests: IClassFixture<CommonTestFixture>
    {
        
        private readonly BookStoreDBContext _context;
        
        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;

        }

        [Theory]
        [InlineData("Yeni name")]
        public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated(string name)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 2; // BookId'nin valid olduğunu varsayıyoruz. Bu testte sadece update inputlar kontrol edilecek.

            command.Model = new UpdateGenreModel(){
                Name = name,
                IsActive = false
            };
           
            

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var genre = _context.Genres.SingleOrDefault(genre => genre.Name == command.Model.Name );
            genre.Should().NotBeNull();
            genre.IsActive.Should().Be(command.Model.IsActive);
            

            
        }
       
        
    }
}