using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.DTOs;
using LibrarySystem.Models;

namespace LibrarySystem.IServicesContract
{
    public interface IBookService
    {
        //Insert Book Object in DB
        public Task<bool> InsertBookAsync(BookDto book);
        //Update Book Object in DB
        public Task<bool> UpdateBookAsync(int id, BookDto book);
        //insert for many objects of book
        public Task<bool> InsertBooksAsync(List<BookDto> books);
        //Insert using navigation properties 
        public Task<bool> InsertBookWithAuthorAsync(BookDto book, int authorId);
        //Delete List of Books
        public Task<bool> DeleteBooksByAuthorAsync(int authorId);
        //use EntityState to manipulate object states
        public Task<bool> SetEntityStateAsync(int bookId, string state);
        //Get All Books With Author Using Eager Loading
        public Task<IEnumerable<Book>> GetAllBooksAsyncUsingEager();
        //Get All Books With Author Using Lazy Loading
        #region Bouns

        public Task<IEnumerable<Book>> GetAllBooksAsyncUsingLazy();
        //Get all books (Read-Only)
        Task<IEnumerable<Book>> GetAllBooksReadOnlyAsync();
        //Update book title using Attach (no need to fetch first)
        Task<bool> UpdateBookTitleWithAttachAsync(int bookId, string newTitle);
        //Get books by AuthorId using stored procedure
        Task<IEnumerable<Book>> GetBooksByAuthorIdUsingSPAsync(int authorId); 

        #endregion

    }
}
