using AutoMapper;
using BookStore.Application.GenreOperations.Commands.CreateGenre;
using BookStore.Application.GenreOperations.Commands.DeleteGenre;
using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using BookStore.Application.GenreOperations.Queries.GetGenres;
using BookStore.Application.GenreOperations.Queries.GetGenresDetail;
using BookStore.DbOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class GenreController:ControllerBase
    {
        private readonly BookStoreDbContext _context;

        private readonly IMapper _mapper;

        public GenreController(BookStoreDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetGenres()
        {
            GetGenresQueries queries = new GetGenresQueries(_context, _mapper);
            var obj = queries.Handle();
            return Ok(obj);
        }

        [HttpGet("id")]
        public ActionResult GetGenresDetail(int id)
        {
            GetGenresDetailQueries queries = new GetGenresDetailQueries(_context, _mapper);
            queries.GenreId = id;
            GetGenresDetailQueriesValidator validator = new GetGenresDetailQueriesValidator();
            validator.ValidateAndThrow(queries);
            var obj = queries.Handle();
            return Ok(obj);
        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = newGenre;

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpPut("id")]
        public IActionResult updateGenre(int id,[FromBody] UpdateGenreModel updateGenre)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = id;
            command.Model = updateGenre;
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = id;
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

    }
}
