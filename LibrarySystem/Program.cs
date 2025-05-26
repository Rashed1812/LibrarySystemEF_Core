using System.Threading.Tasks;
using LibrarySystem.DTOs;
using LibrarySystem.IServicesContract;
using LibrarySystem.Models;
using LibrarySystem.Services;

namespace LibrarySystem
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            #region DI
            //Create Object from DBContext Manullay
            using var dbContext = new LibraryDbContext();
            var bookService = new BookService(dbContext);
            var memberService = new MemberService(dbContext);
            #endregion

            #region Insert Book Object

            var bookDto = new BookDto
            {
                Title = "UI/UX",
                PublishedYear = 2024,
                AuthorId = 1
            };
            var insertResult = await bookService.InsertBookAsync(bookDto);
            Console.WriteLine($"Insert Single Book: {(insertResult ? "Success" : "Failed")}");

            #endregion


            #region Insert Multiple Books Objects

            var books = new List<BookDto>
            {
                new BookDto { Title = "ASP.NET Core", PublishedYear = 2023, AuthorId = 1 },
                new BookDto { Title = "EF Core", PublishedYear = 2022, AuthorId = 1 },
            };
            var insertManyResult = await bookService.InsertBooksAsync(books);
            Console.WriteLine($"Insert Multiple Books: {(insertManyResult ? "Success" : "Failed")}");

            #endregion


            #region Update Book

            var updateDto = new BookDto { Title = "Updated Title", PublishedYear = 2025, AuthorId = 1 };
            var updateResult = await bookService.UpdateBookAsync(1, updateDto);
            Console.WriteLine($"Update Book: {(updateResult ? "Success" : "Failed")}");

            #endregion


            #region Property Navigation

            var navInsertResult = await bookService.InsertBookWithAuthorAsync(bookDto, 1);
            Console.WriteLine($"Insert with Navigation: {(navInsertResult ? "Success" : "Failed")}");

            #endregion


            #region Set Entity State

            var stateResult = await bookService.SetEntityStateAsync(1, "Modified");
            Console.WriteLine($"Set Entity State: {(stateResult ? "Success" : "Failed")}");

            #endregion


            #region Delete Books by AuthorId
            var deleteResult = await bookService.DeleteBooksByAuthorAsync(1);
            Console.WriteLine($"Delete Books by Author: {(deleteResult ? "Success" : "Failed")}");
            #endregion


            #region Get All Books using Eager Loading
            var eagerBooks = await bookService.GetAllBooksAsyncUsingEager();
            Console.WriteLine("Books with Eager Loading:");
            foreach (var book in eagerBooks)
                Console.WriteLine($"{book.Title} - {book.Author?.Name}");
            #endregion


            #region Get All Books using Lazy Loading

            var lazyBooks = await bookService.GetAllBooksAsyncUsingLazy();
            Console.WriteLine("Books with Lazy Loading:");
            foreach (var book in lazyBooks)
                Console.WriteLine($"{book.Title} - {book.Author?.Name}");

            #endregion

            #region Bouns

            Console.WriteLine("Read-only Books:");
            var BookNoTracking = await bookService.GetAllBooksReadOnlyAsync();
            foreach (var book in BookNoTracking)
            {
                Console.WriteLine($"{book.Title}");
            }


            Console.WriteLine("Updating title using Attach");
            bool updated = await bookService.UpdateBookTitleWithAttachAsync(1, "Updated Title");
            Console.WriteLine(updated ? "Update successful." : "Update failed.");


            Console.WriteLine("Books by AuthorId (stored procedure):");
            var authorBooks = await bookService.GetBooksByAuthorIdUsingSPAsync(1);
            foreach (var book in authorBooks)
            {
                Console.WriteLine($"{book.Title}");
            }

            #endregion

        }
    }
}
