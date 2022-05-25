using BookStore.BookOperations;
using BookStore.DbOperations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BookStore.BookOperations.CreateBookCommand;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController:ControllerBase
    {


        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
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
        public Book GetById(int id)
        {
            var book = _context.Books.Where(book => book.Id == id).SingleOrDefault();

            return book;
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
   
     
            return Ok();
            
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] UpdateBookCommand updateBook)
        {
            UpdateBookCommand updateCommand = new UpdateBookCommand(_context);

            /*var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if(book is null)
            {
                return BadRequest();
            }
            book.GenreId = updateBook.GenreId != default ? updateBook.GenreId : book.GenreId;
            book.PageCount = updateBook.PageCount != default ? updateBook.PageCount : book.PageCount;
            book.PublishDate = updateBook.PublishDate != default ? updateBook.PublishDate : book.PublishDate;
            book.Title = updateBook.Title != default ? updateBook.Title : book.Title;
            book.Name = updateBook.Name != default ? updateBook.Name : book.Name;
            _context.SaveChanges();
            return Ok();*/
                
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
            {
                return BadRequest();
            }
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();

        }
    }
}
