using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel Model {get; set;}

        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommand(BookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Name == Model.Name);
            if (author is not null){
                throw new InvalidOperationException("Yazar zaten mevcut");

            }
            author = new Author();
            author = _mapper.Map<Author>(Model);

            _context.Authors.Add(author);
            _context.SaveChanges();
        }

    }
    public class CreateAuthorModel{
        public string Name {get; set;}
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }
    }
}