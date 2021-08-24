using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.GetBookDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Queries
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        
        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;
        
        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenBookIdIsValid_ParticularBook_ShouldBeReturned()
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = 2;
            
            BookDetailViewModel book = new BookDetailViewModel()
            {
                Title = "Herland",
                PageCount = 250,
                Genre = "SciFi",
                Author = "İsim Test 2 Soyisim Test 2"
            };
            

            FluentActions.Invoking(() => query.Handle()).Invoke();
            var model = query.Handle();
            model.Should().NotBeNull();
            model.PageCount.Should().Be(book.PageCount);
            model.Genre.Should().Be(book.Genre);
            book.Title.Should().Be(model.Title);
            model.Author.Should().Be(book.Author);
            
        }
        
    }
}