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
    public class AuthorsServices : IAuthorsServices
    {
        ILibraryRepository _libraryRepository;
        private IMapper _mapper;

        public AuthorsServices(ILibraryRepository libraryRepository, IMapper mapper)
        {
            _libraryRepository = libraryRepository;
            _mapper = mapper;
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
            var entityList= _libraryRepository.GetAuthors(orderBy);
            var modelList = _mapper.Map<IEnumerable<AuthorsModel>>(entityList);
            return modelList;
        }

        public AuthorsModel GetAuthor(int authorId)
        {
            var author = _libraryRepository.GetAuthor(authorId);
            if (author == null)
            {
                throw new NotFoundOperationException($"The author with id{authorId} doesnt exist");
            }
            return _mapper.Map<AuthorsModel>(author);
        }

        public AuthorsModel CreateAuthor(AuthorsModel authorsModel)
        {
            var authorEntity = _mapper.Map<AuthorEntity>(authorsModel);
            var authorToReturn = _libraryRepository.CreateAuthor(authorEntity);
            return _mapper.Map<AuthorsModel>(authorToReturn);
        }

        public DeleteModel DeleteAuthor(int authorId)
        {
            var authorToDelete = GetAuthor(authorId);
            var result =_libraryRepository.DeleteAuthor(authorId);
            if (result)
            {
                return new DeleteModel()
                {
                    IsSuccess = result,
                    Message = "The author was deleted"
                };
            }
            else
            {
                return new DeleteModel()
                {
                    IsSuccess = result,
                    Message = "The author was not deleted"
                };
            }
        }

        public AuthorsModel UpdateAuthor(int authorId, AuthorsModel authorsModel)
        {
            var authorEntity = _mapper.Map<AuthorEntity>(authorsModel);
            _libraryRepository.UpdateAuthor(authorEntity);
            return authorsModel;
        }
    }
}
