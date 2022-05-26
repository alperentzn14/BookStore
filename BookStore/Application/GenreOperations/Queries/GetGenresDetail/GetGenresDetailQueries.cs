using AutoMapper;
using BookStore.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.GenreOperations.Queries.GetGenresDetail
{
    public class GetGenresDetailQueries
    {
        private readonly BookStoreDbContext _dbContext;

        private readonly IMapper _mapper;

        public int GenreId { get; set; }
        public GetGenresDetailQueries(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public GenresDetailViewModel Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.IsActive && x.Id==GenreId);
            if (genre is null)
                throw new InvalidOperationException("Kitap Türü Bulunamadı");
            return _mapper.Map<GenresDetailViewModel>(genre);

           
        }
    }
    public class GenresDetailViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

