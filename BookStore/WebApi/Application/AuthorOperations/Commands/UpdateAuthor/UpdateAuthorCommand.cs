using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public int AuthorId { get; set; }

        public UpdateAuthorModel Model { get; set; }
        private readonly BookStoreDBContext _context;

        public UpdateAuthorCommand(BookStoreDBContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var Author = _context.Authors.SingleOrDefault( x=> x.Id == AuthorId);

            if(Author is null){
                throw new InvalidOperationException("Yazar bulunamadı");
            }

            if(_context.Authors.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Surname.ToLower() == Model.Surname.ToLower()&& x.Id != AuthorId)){
                throw new InvalidOperationException("Aynı isimli bir yazar zaten mevcut");
            }
            // Eğer sadece aktifliği değiştirmek isterse buna izin veriyoruz. default string nullsa kendi namiyle isActive i update ediyoruz
            Author.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? Author.Name : Model.Name;
            Author.Surname = string.IsNullOrEmpty(Model.Surname.Trim()) ? Author.Surname : Model.Surname;
            Author.Birthdate = Model.Birthdate == null ? Author.Birthdate : Model.Birthdate;

            
            _context.SaveChanges();

        }

    }

    public class UpdateAuthorModel{
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }
    }

}