using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.GenreOperations.Queries.GetGenresDetail
{
    public class GetGenresDetailQueriesValidator: AbstractValidator<GetGenresDetailQueries>
    {
        public GetGenresDetailQueriesValidator()
        {
            RuleFor(query => query.GenreId).GreaterThan(0);
        }
    }
}
