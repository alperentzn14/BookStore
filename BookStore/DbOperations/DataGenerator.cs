using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }
                context.Books.AddRange(
                
                            new Book
            {
                //Id=1,
                Author="alperen",
                Name="BlackList",
                GenreId=1,
                PageCount=200,
                Title="test",
                PublishDate=new DateTime(2001,07,12)
            },
             new Book
            {
                //Id=2,
                Author="ali",
                Name="ProTest",
                GenreId=2,
                PageCount=210,
                Title="test2",
                PublishDate=new DateTime(2002,08,10)
            
                });
                context.SaveChanges();
            }
            
        }
    }
}
