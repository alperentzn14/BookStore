using BookStore.BookOperations;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(query => query.Model.GenreId).GreaterThan(0);
            RuleFor(query => query.bookId).GreaterThan(0);
            RuleFor(query => query.Model.Title).NotEmpty().MinimumLength(4);
            RuleFor(query => query.Model.Name).NotEmpty().MinimumLength(4);
        }
    }
}
