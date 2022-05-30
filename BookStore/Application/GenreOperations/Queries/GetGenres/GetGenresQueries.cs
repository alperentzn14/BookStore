using AutoMapper;
using BookStore.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQueries
    {
        private readonly IBookStoreDbContext _dbContext;

        private readonly IMapper _mapper;
        public GetGenresQueries(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<GenreViewModel> Handle()
        {
            var genresList = _dbContext.Genres.Where(x => x.IsActive).OrderBy(x => x.Id);
            List<GenreViewModel> vm = _mapper.Map<List<GenreViewModel>>(genresList);

            return vm;
        }
    }
    public class GenreViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

