using BookStore.Common;
using BookStore.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.BookOperations
{
    public class GetBooksDetailQuery
    {

        private readonly BookStoreDbContext _dbContext;

        public int bookId { get; set; }
        public GetBooksDetailQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Where(book => book.Id == bookId).SingleOrDefault();
            if(book is null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı");
            }
            BookDetailViewModel vm = new BookDetailViewModel();
            vm.Title = book.Title;
            vm.Name = book.Name;
            vm.PageCount = book.PageCount;
            vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy");
            vm.Genre = ((GenreEnum)book.GenreId).ToString();



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
