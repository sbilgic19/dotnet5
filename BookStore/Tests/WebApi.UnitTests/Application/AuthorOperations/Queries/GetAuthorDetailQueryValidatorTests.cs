using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.AuthorOperations.Queries
{
    public class GetAuthorDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        
        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;
        
        public GetAuthorDetailQueryValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

       [Fact]
       public void WhenIDIsInvalidNotGreaterThanZero_InvalidOperationException_ShouldBeReturned()
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(null, null);
            query.AuthorId = -2;

            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();

            var result = validator.Validate(query);
            result.Errors.Count.Should().BeGreaterThan(0);

            
        }
        
    }
}