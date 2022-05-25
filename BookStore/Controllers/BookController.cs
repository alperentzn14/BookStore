using BookStore.BookOperations;
using BookStore.DbOperations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BookStore.BookOperations.CreateBookCommand;
using static BookStore.BookOperations.UpdateBookCommand;

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
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            try
            {
                GetBooksDetailQuery query = new GetBooksDetailQuery(_context);
                query.bookId = id;
                result=query.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
          

            return Ok(result);
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
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updateBook)
        {
          
            try
            {

                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.bookId = id;
                command.Model = updateBook;
                command.Handle();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {

                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.bookId = id;
                command.Handle();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


            return Ok();
     

        }
    }
}
