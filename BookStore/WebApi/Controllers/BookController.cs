using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.CreateBook;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.UpdateBook;
using WebApi.BookOperations.DeleteBook;
using AutoMapper;
using FluentValidation.Results;
using FluentValidation;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        // Uygulama içerisinde değiştirilmesi istenmiyor. Constructurda set edilip öyle kalıyor

        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            try{
                query.BookId = id;
                GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
                validator.ValidateAndThrow(query);
                result = query.Handle();
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }

            return Ok(result);
            

            
        }

        // [HttpGet]
        
        // public Book Get([FromQuery]string id)
        // {
        //     var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }


        // POST database'e veri ekleme
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            try{
                command.Model = newBook;

                CreateBookCommandValidator validator = new CreateBookCommandValidator();

                validator.ValidateAndThrow(command);
                command.Handle();



                // ValidationResult result = validator.Validate(command);
                // if(!result.IsValid){
                //     foreach (var item in result.Errors){
                //         Console.WriteLine("Özellik "+ item.PropertyName+"- Error Message: "+item.ErrorMessage); // Hangi fieldda hata alındı ?
                //     }
                // }else{
                //      command.Handle(); // Ekleme işlemi
                // }    
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
            
        }
        // PUT Database veri güncelleme
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
           UpdateBookCommand command = new UpdateBookCommand(_context);
           try {
                command.BookId = id;
                command.Model = updatedBook;
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
           }catch(Exception ex){
               return BadRequest(ex.Message);
           }
           return Ok();
           
        }

       [HttpDelete("{id}")]
       public IActionResult DeleteBook(int id)
       {
           DeleteBookCommand command = new DeleteBookCommand(_context);

           try{
                command.BookId = id;
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
           }catch(Exception ex){
               return BadRequest(ex.Message);
           }
           

           return Ok();
       }



    }
}