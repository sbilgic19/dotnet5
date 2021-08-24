using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        public int AuthorId { get; set; }
        public readonly IBookStoreDBContext _context;
        public readonly IMapper _mapper;
        public GetAuthorDetailQuery(IBookStoreDBContext context, IMapper mapper )
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if(author is null){
                throw new InvalidOperationException("Yazar bulunamadı");
            }
            return _mapper.Map<AuthorDetailViewModel>(author);;
        }

        
    }

    public class AuthorDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Birthdate { get; set; }
    }
}