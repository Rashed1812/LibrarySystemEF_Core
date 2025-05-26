using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.DTOs;
using LibrarySystem.IServicesContract;
using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Services
{
    public class BookService(LibraryDbContext _dbContext) : IBookService
    {
        public async Task<bool> InsertBookAsync(BookDto book)
        {
            var entity = new Book
            {
                Title = book.Title,
                PublishedYear = book.PublishedYear,
                AuthorId = book.AuthorId,
            };
            _dbContext.Add(entity);
            var result = await _dbContext.SaveChangesAsync();
            if (result < 0)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> InsertBooksAsync(List<BookDto> books)
        {
            var entities = books.Select(book => new Book
            {
                Title = book.Title,
                PublishedYear = book.PublishedYear,
                AuthorId = book.AuthorId,
            }).ToList();
            _dbContext.AddRange(entities);
            var result = await _dbContext.SaveChangesAsync();
            if (result < 0)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> UpdateBookAsync(int id, BookDto book)
        {
            var entity = await _dbContext.Books.FindAsync(id);
            if (entity is not null)
            {
                entity.Title = book.Title;
                entity.PublishedYear = book.PublishedYear;
                entity.AuthorId = book.AuthorId;
                _dbContext.Update(entity);
                var result = await _dbContext.SaveChangesAsync();
                if (result < 0)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteBooksByAuthorAsync(int authorId)
        {
            var booksToDelete = await _dbContext.Books
                            .Where(b => b.AuthorId == authorId)
                            .ToListAsync();

            if (booksToDelete.Any())
            {
                _dbContext.Books.RemoveRange(booksToDelete);
                var result = await _dbContext.SaveChangesAsync();
                if (result < 0)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> InsertBookWithAuthorAsync(BookDto book, int authorId)
        {
            var author = await _dbContext.Authors.FindAsync(authorId);
            if (author is not null)
            {
                var entity = new Book
                {
                    Title = book.Title,
                    PublishedYear = book.PublishedYear,
                    Author = author
                };
                await _dbContext.AddAsync(entity);
                var result = await _dbContext.SaveChangesAsync();
                if(result < 0)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
        public async Task<bool> SetEntityStateAsync(int bookId, string state)
        {
            var entity = await _dbContext.Books.FindAsync(bookId);
            if (entity == null)
                return false;

            if (!Enum.TryParse<EntityState>(state, true, out var entityState))
            {
                throw new ArgumentException("Invalid entity state value.", nameof(state));
            }

            _dbContext.Entry(entity).State = entityState;

            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
        public async Task<IEnumerable<Book>> GetAllBooksAsyncUsingEager()
        {
            var booksWithAuthors = await _dbContext.Books
                                       .Include(b => b.Author)
                                       .ToListAsync();
            return booksWithAuthors;
        }
        public async Task<IEnumerable<Book>> GetAllBooksAsyncUsingLazy()
        {
            var books = await _dbContext.Books.ToListAsync();
            return books;
        }
    }
}
