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

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        // Uygulama içerisinde değiştirilmesi istenmiyor. Constructurda set edilip öyle kalıyor

        private readonly BookStoreDBContext _context;
        public BookController(BookStoreDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            GetBookDetailQuery query = new GetBookDetailQuery(_context);
            try{
                query.BookId = id;
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
            CreateBookCommand command = new CreateBookCommand(_context);
            try{
                command.Model = newBook;
                command.Handle();   
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
                command.Handle();
           }catch(Exception ex){
               return BadRequest(ex.Message);
           }
           

           return Ok();
       }



    }
}