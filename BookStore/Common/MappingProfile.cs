using AutoMapper;
using BookStore.BookOperations;
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
            CreateMap<Book, BookDetailViewModel>().ForMember(dest=>dest.Genre,opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()));
            CreateMap<Book,BookViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
        }
    }
}
