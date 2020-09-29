using BooksAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Data.Repository
{
    public class LibraryRepository : ILibraryRepository
    {
        private List<BookEntity> books = new List<BookEntity>
        {
            new BookEntity(){Id=1, Title="Cien años de soledad", Author="Gabriel Garcia Marquez", rating=8, availability=Availability.available},
            new BookEntity(){Id=2, Title="Controle su destino", Author="Tony RObbins", rating=7, availability=Availability.available},
            new BookEntity(){Id=3, Title="El mejor lider de la historia", Author="Lucas Leys", rating=8, availability=Availability.available},
        };
        public BookEntity CreateBook(BookEntity book)
        {
            int newId;
            if (books.Count == 0)
            {
                newId = 1;
            }
            else
            {
                newId = books.OrderByDescending(a => a.Id).FirstOrDefault().Id + 1;
            }
            book.Id = newId;
            books.Add(book);
            return book;
        }

        public bool DeleteBook(int bookId)
        {
            var bookToDelete = books.FirstOrDefault(a => a.Id == bookId);
            books.Remove(bookToDelete);
            return true;
        }

        public BookEntity GetBook(int bookId)
        {
            return books.FirstOrDefault(a => a.Id == bookId);
        }

        public IEnumerable<BookEntity> GetBooks(string orderBy)
        {
            switch (orderBy)
            {
                case "id":
                    return books.OrderBy(c => c.Id);
                case "title":
                    return books.OrderBy(c => c.Title);
                case "author":
                    return books.OrderBy(c => c.Author);
                case "rating":
                    return books.OrderBy(c => c.rating);
                case "availability":
                    return books.OrderBy(c => c.availability);
                default:
                    return books.OrderBy(c => c.Id);
            }
        }

        public bool UpdateBook(BookEntity book)
        {
            var bookToUpdate = new BookEntity();
            bookToUpdate.Title = book.Title ?? bookToUpdate.Title;
            bookToUpdate.Author = book.Author ?? bookToUpdate.Author;
            bookToUpdate.rating = book.rating ?? bookToUpdate.rating;
            bookToUpdate.availability = book.availability ?? bookToUpdate.availability;
            return true;
        }
    }
}
