using AutoMapper;
using BookStore.Common;
using BookStore.DbOperations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.BookOperations
{
    public class GetBooksDetailQuery
    {

        private readonly BookStoreDbContext _dbContext;

        private readonly IMapper _mapper;

        public int bookId { get; set; }
        public GetBooksDetailQuery(BookStoreDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Include(x=>x.Genre).Where(book => book.Id == bookId).SingleOrDefault();
            if(book is null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı");
            }
            BookDetailViewModel vm = _mapper.Map<Book, BookDetailViewModel>(book);
          



            return vm;
        }
    }
    public class BookDetailViewModel
        {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }

        }
}
