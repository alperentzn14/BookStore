using BookStore.BookOperations;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.BookOperations.Commands.Queries
{
    public class GetBooksDetailQueryValidator : AbstractValidator<GetBooksDetailQuery>
    {
        public GetBooksDetailQueryValidator()
        {
            RuleFor(query=>query.bookId).GreaterThan(0);
        }
    }
}
