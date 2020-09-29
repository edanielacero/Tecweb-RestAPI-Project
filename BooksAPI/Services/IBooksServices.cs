using BooksAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Services
{
    public interface IBooksServices
    {
        IEnumerable<BookModel> GetBooks(string orderBy);
        BookModel GetBook(int bookId);
        BookModel CreateBook(BookModel bookModel);
        DeleteModel DeleteBook(int bookId);
        BookModel UpdateBook(int bookId, BookModel bookModel);
        IEnumerable<BookModel> GetTopRatedBooks(int rate);
        IEnumerable<BookModel> GetBooksFromAuthor(string author);
    }
}
