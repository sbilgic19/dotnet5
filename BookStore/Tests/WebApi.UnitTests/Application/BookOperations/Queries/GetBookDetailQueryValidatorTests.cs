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
    public class GetBookDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        
        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;
        
        public GetBookDetailQueryValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

       [Fact]
       public void WhenIDIsInvalidNotGreaterThanZero_InvalidOperationException_ShouldBeReturned()
        {
            GetBookDetailQuery query = new GetBookDetailQuery(null, null);
            query.BookId = -2;

            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();

            var result = validator.Validate(query);
            result.Errors.Count.Should().BeGreaterThan(0);

            
        }
        
    }
}