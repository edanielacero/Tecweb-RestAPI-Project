using BooksAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Data.Repository
{
    public interface ILibraryRepository
    {
        //Books
        IEnumerable<BookEntity> GetBooks(string orderBy);
        BookEntity GetBook(int bookId);
        BookEntity CreateBook(BookEntity booksModel);
        bool DeleteBook(int bookId);
        BookEntity UpdateBook(int bookId, BookEntity booksModel);
        IEnumerable<BookEntity> GetTopBooks(int rate);
        IEnumerable<BookEntity> GetBooksFromAuthor(string author);
        
    }
}
