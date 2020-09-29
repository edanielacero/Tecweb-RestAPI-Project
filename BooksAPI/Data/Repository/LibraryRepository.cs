using BooksAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Data.Repository
{
    public class LibraryRepository : ILibraryRepository
    {
        private List<AuthorEntity> authors = new List<AuthorEntity>
        {
            new AuthorEntity(){Id=1, Name="Gabriel Garcia Marquez", BirthDate=new DateTime(1927,3,6), Country="Colombia"},
            new AuthorEntity(){Id=2, Name="Tony Robbins", BirthDate=new DateTime(1960,2,29), Country="US"},
            new AuthorEntity(){Id=3, Name="Lucas Leys", BirthDate=new DateTime(1972,2,10), Country="Argentina"}
        };
        public AuthorEntity CreateAuthor(AuthorEntity authorsModel)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAuthor(int authorId)
        {
            throw new NotImplementedException();
        }

        public AuthorEntity GetAuthor(int authorId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AuthorEntity> GetAuthors(string orderBy)
        {
            switch (orderBy)
            {
                case "id":
                    return authors.OrderBy(c => c.Id);
                case "name":
                    return authors.OrderBy(c => c.Name);
                case "country":
                    return authors.OrderBy(c => c.Country);
                case "birth-date":
                    return authors.OrderBy(c => c.BirthDate);
                default:
                    return authors.OrderBy(c => c.Id);
            }
        }

        public bool UpdateAuthor(AuthorEntity authorsModel)
        {
            throw new NotImplementedException();
        }
    }
}
