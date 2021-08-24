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
    public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        
        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;
        
        public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenBookIdIsValid_ParticularBook_ShouldBeReturned()
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = 2;
            
            AuthorDetailViewModel author = new AuthorDetailViewModel()
            {
               Name = "İsim Test 2",
               Surname = "Soyisim Test 2",
               Birthdate = new DateTime(2002,06,12).ToString()
            };
            

            FluentActions.Invoking(() => query.Handle()).Invoke();
            var model = query.Handle();
            model.Should().NotBeNull();
            model.Name.Should().Be(author.Name);
            model.Surname.Should().Be(author.Surname);
            
        }
        
    }
}