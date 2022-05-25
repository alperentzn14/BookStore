using BookStore.DbOperations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.BookOperations
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle(int id, [FromBody] Book updateBook)
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
            {
                throw new InvalidOperationException("Kitap Güncellenemedi");
            }
            book.GenreId = updateBook.GenreId != default ? updateBook.GenreId : book.GenreId;
            book.PageCount = updateBook.PageCount != default ? updateBook.PageCount : book.PageCount;
            book.PublishDate = updateBook.PublishDate != default ? updateBook.PublishDate : book.PublishDate;
            book.Title = updateBook.Title != default ? updateBook.Title : book.Title;
            book.Name = updateBook.Name != default ? updateBook.Name : book.Name;
            _dbContext.SaveChanges();

        }

        public class UpdateBookModel
        {
            public string Name { get; set; }
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
        }
    }
}
