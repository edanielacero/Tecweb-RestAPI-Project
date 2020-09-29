using BooksAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Data.Repository
{
    public interface ILibraryRepository
    {
        //Authors
        IEnumerable<AuthorEntity> GetAuthors(string orderBy);
        AuthorEntity GetAuthor(int authorId);
        AuthorEntity CreateAuthor(AuthorEntity authorsModel);
        bool DeleteAuthor(int authorId);
        bool UpdateAuthor(AuthorEntity authorsModel);
    }
}
