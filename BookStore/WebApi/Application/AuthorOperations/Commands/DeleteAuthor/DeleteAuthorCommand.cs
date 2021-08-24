using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }
        private readonly IBookStoreDBContext _context;

        public DeleteAuthorCommand(IBookStoreDBContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if(author is null){
                throw new InvalidOperationException("Yazar bulunamadı");
            }
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}
