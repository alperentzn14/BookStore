using AutoMapper;
using BookStore.Application.GenreOperations.Queries.GetGenres;
using BookStore.Application.GenreOperations.Queries.GetGenresDetail;
using BookStore.BookOperations;
using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BookStore.BookOperations.CreateBookCommand;

namespace BookStore.Common
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel,Book>();
            CreateMap<Book, BookDetailViewModel>().ForMember(dest=>dest.Genre,opt=>opt.MapFrom(src=>src.Genre.Name));
            CreateMap<Book,BookViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Genre, GenreViewModel>();
            CreateMap<Genre, GenresDetailViewModel>();
        }
    }
}
