using BooksAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Services
{
    public interface IAuthorsServices
    {
        IEnumerable<AuthorsModel> GetAuthors(string orderBy);
        AuthorsModel GetAuthor(int authorId);
        AuthorsModel CreateAuthor(AuthorsModel authorsModel);
        DeleteModel DeleteAuthor(int authorId);
        AuthorsModel UpdateAuthor(int authorId, AuthorsModel authorsModel);
    }
}
