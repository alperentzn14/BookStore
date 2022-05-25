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
        public UpdateBookModel Model { get; set; }
        public int bookId { get; set; }
        private readonly BookStoreDbContext _dbContext;
        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == bookId);
            if (book is null)
            {
                throw new InvalidOperationException("Güncellenecek Kitap Bulunamadı");
            }
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            book.Name = Model.Name != default ? Model.Name : book.Name;
            _dbContext.SaveChanges();

        }

        public class UpdateBookModel
        {
            public string Name { get; set; }
            public string Title { get; set; }
            public int GenreId { get; set; }
        }
    }
}
