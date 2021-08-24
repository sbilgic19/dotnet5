using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.GetBookDetail;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.GenreOperations.Queries
{
    public class GetGenreDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        
        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;
        
        public GetGenreDetailQueryValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

       [Fact]
       public void WhenIDIsInvalidNotGreaterThanZero_InvalidOperationException_ShouldBeReturned()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
            query.GenreId = 0;

            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();

            var result = validator.Validate(query);
            result.Errors.Count.Should().BeGreaterThan(0);

            
        }
        
    }
}