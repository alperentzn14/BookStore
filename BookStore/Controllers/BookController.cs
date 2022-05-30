using AutoMapper;
using BookStore.Application.BookOperations.Commands.Queries;
using BookStore.Application.BookOperations.Commands.UpdateBook;
using BookStore.BookOperations;
using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.DeleteBook;
using BookStore.DbOperations;
using FluentValidation;
using FluentValidation.Results;
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


        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController(IBookStoreDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
        
                GetBooksDetailQuery query = new GetBooksDetailQuery(_context,_mapper);
                query.bookId = id;
                GetBooksDetailQueryValidator validator = new GetBooksDetailQueryValidator();
                validator.ValidateAndThrow(query);
                result =query.Handle();
      

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
         
                command.Model = newBook;

                CreateBookCommandValidator validator = new CreateBookCommandValidator();
        
                validator.ValidateAndThrow(command);
                command.Handle();
     
            return Ok();
            
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updateBook)
        {
      
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.bookId = id;
                command.Model = updateBook;
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
      
                DeleteBookCommand command = new DeleteBookCommand(_context);

                command.bookId = id;

                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();

            return Ok();
     

        }
    }
}
