using BooksAPI.Data.Repository;
using BooksAPI.Exceptions;
using BooksAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Services
{
    public class AuthorsServices : IAuthorsServices
    {
        ILibraryRepository _libraryRepository;
        
        public AuthorsServices(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        private HashSet<string> allowedOrderByParameters = new HashSet<string>()
        {
            "id",
            "name",
            "country",
            "birth-date"
        };
        public IEnumerable<AuthorsModel> GetAuthors(string orderBy)
        {
            if (!allowedOrderByParameters.Contains(orderBy.ToLower()))
            {
                throw new BadRequestOperationException($"the field {orderBy} is not supported, please use one of these {string.Join(",",allowedOrderByParameters)}");
            }
            return _libraryRepository.GetAuthors(orderBy);
        }

        public AuthorsModel GetAuthor(int authorId)
        {
            var author= authors.FirstOrDefault(a => a.Id == authorId);
            if (author == null)
            {
                throw new NotFoundOperationException($"The author with id{authorId} doesnt exist");
            }
            return author;
        }

        public AuthorsModel CreateAuthor(AuthorsModel authorsModel)
        {
            int newId;
            if (authors.Count == 0)
            {
                newId = 1;
            }
            else
            {
            newId = authors.OrderByDescending(a => a.Id).FirstOrDefault().Id + 1;
            }
            authorsModel.Id = newId;
            authors.Add(authorsModel);
            return authorsModel;
        }

        public DeleteModel DeleteAuthor(int authorId)
        {
            var authorToDelete = authors.FirstOrDefault(a => a.Id == authorId);
            if (authorToDelete==null)
            {
                throw new NotFoundOperationException($"The author with id{authorId} doesnt exist");
            }
            var result = authors.Remove(authorToDelete);
            return new DeleteModel()
            {
                IsSuccess = true,
                Message = "The author was deleted"
            };
        }

        public AuthorsModel UpdateAuthor(int authorId, AuthorsModel authorsModel)
        {
            var authorToUpdate = GetAuthor(authorId);
            authorToUpdate.Name = authorsModel.Name ?? authorToUpdate.Name;
            authorToUpdate.Country = authorsModel.Country ?? authorToUpdate.Country;
            authorToUpdate.BirthDate = authorsModel.BirthDate ?? authorToUpdate.BirthDate;
            return authorToUpdate;
        }
    }
}
