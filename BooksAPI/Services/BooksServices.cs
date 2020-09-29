using AutoMapper;
using BooksAPI.Data.Entities;
using BooksAPI.Data.Repository;
using BooksAPI.Exceptions;
using BooksAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Services
{
    public class BooksServices : IBooksServices
    {
        ILibraryRepository _libraryRepository;
        private IMapper _mapper;

        public BooksServices(ILibraryRepository libraryRepository, IMapper mapper)
        {
            _libraryRepository = libraryRepository;
            _mapper = mapper;
        }

        private HashSet<string> allowedOrderByParameters = new HashSet<string>()
        {
            "id",
            "title",
            "author",
            "rating",
            "availability"
        };
        public IEnumerable<BookModel> GetBooks(string orderBy)
        {
            if (!allowedOrderByParameters.Contains(orderBy.ToLower()))
            {
                throw new BadRequestOperationException($"the field {orderBy} is not supported, please use one of these {string.Join(",",allowedOrderByParameters)}");
            }
            var entityList= _libraryRepository.GetBooks(orderBy);
            var modelList = _mapper.Map<IEnumerable<BookModel>>(entityList);
            return modelList;
        }

        public BookModel GetBook(int bookId)
        {
            var book = _libraryRepository.GetBook(bookId);
            if (book == null)
            {
                throw new NotFoundOperationException($"The book with id {bookId} doesnt exist");
            }
            return _mapper.Map<BookModel>(book);
        }

        public BookModel CreateBook(BookModel booksModel)
        {
            var bookEntity = _mapper.Map<BookEntity>(booksModel);
            var bookToReturn = _libraryRepository.CreateBook(bookEntity);
            return _mapper.Map<BookModel>(bookToReturn);
        }

        public DeleteModel DeleteBook(int bookId)
        {
            var bookToDelete = GetBook(bookId);
            var result =_libraryRepository.DeleteBook(bookId);
            if (result)
            {
                return new DeleteModel()
                {
                    IsSuccess = result,
                    Message = "The book was deleted"
                };
            }
            else
            {
                return new DeleteModel()
                {
                    IsSuccess = result,
                    Message = "The book was not deleted"
                };
            }
        }

        public BookModel UpdateBook(int bookId, BookModel booksModel)
        {
            var bookEntity = _mapper.Map<BookEntity>(booksModel);
            var bookUpdated = _libraryRepository.UpdateBook(bookId, bookEntity);
            return _mapper.Map<BookModel>(bookUpdated);
        }
        public IEnumerable<BookModel> GetTopRatedBooks(int rate)
        {
            var entityList = _libraryRepository.GetTopBooks(rate);
            var modelList = _mapper.Map<IEnumerable<BookModel>>(entityList);
            return modelList;
        }
        public IEnumerable<BookModel> GetBooksFromAuthor(string author)
        {
            var entityList = _libraryRepository.GetBooksFromAuthor(author);
            var modelList = _mapper.Map<IEnumerable<BookModel>>(entityList);
            return modelList;
        }

    }
}
