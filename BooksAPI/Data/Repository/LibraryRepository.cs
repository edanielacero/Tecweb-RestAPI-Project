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
            new BookEntity(){Id=2, Title="Controle su destino", Author="Tony Robbins", rating=7, availability=Availability.available},
            new BookEntity(){Id=3, Title="El mejor lider de la historia", Author="Lucas Leys", rating=6, availability=Availability.available},
            new BookEntity(){Id=4, Title="Poder Sin Limites", Author="Tony Robbins", rating=9, availability=Availability.available},
            new BookEntity(){Id=5, Title="El amor en los tiempos de colera", Author="Gabriel Garcia Marquez", rating=8, availability=Availability.available},
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

        public BookEntity UpdateBook(int bookId, BookEntity book)
        {
            var bookToUpdate = GetBook(bookId);
            bookToUpdate.Title = book.Title ?? bookToUpdate.Title;
            bookToUpdate.Author = book.Author ?? bookToUpdate.Author;
            if (book.rating != 0)
            {
                bookToUpdate.rating = book.rating;
            }
            //bookToUpdate.rating = book.rating ?? bookToUpdate.rating;
            //bookToUpdate.availability = book.availability ?? bookToUpdate.availability;
            if (book.availability != 0)
            {
                bookToUpdate.availability = book.availability;
            }
            return bookToUpdate;
        }
        public IEnumerable<BookEntity> GetTopBooks(int rate)
        {
            var top5books = new List<BookEntity>();
            var newlist=books.OrderByDescending(c => c.rating).ToList();
            for (int i = 0; i < rate; i++)
            {
                top5books.Add(newlist[i]);
            }
            return top5books;
        }
        public IEnumerable<BookEntity> GetBooksFromAuthor(string author)
        {
            var booksFromAuthor = from book in books where book.Author == author select book;
            return booksFromAuthor;
            /*var booksFromAuthor = new List<BookEntity>();
            var newlist = books.OrderByDescending(c => c.rating).ToList();
            for (int i = 0; i < rate; i++)
            {
                top5books.Add(newlist[i]);
            }
            return booksFromAuthor;*/
        }
    }
}
