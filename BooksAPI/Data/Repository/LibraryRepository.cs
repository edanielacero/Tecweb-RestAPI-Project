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
        public AuthorEntity CreateAuthor(AuthorEntity author)
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
            author.Id = newId;
            authors.Add(author);
            return author;
        }

        public bool DeleteAuthor(int authorId)
        {
            var authorToDelete = authors.FirstOrDefault(a => a.Id == authorId);
            authors.Remove(authorToDelete);
            return true;
        }

        public AuthorEntity GetAuthor(int authorId)
        {
            return authors.FirstOrDefault(a => a.Id == authorId);
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
            var authorToUpdate = GetAuthor(authorsModel.Id);
            authorToUpdate.Name = authorsModel.Name ?? authorToUpdate.Name;
            authorToUpdate.Country = authorsModel.Country ?? authorToUpdate.Country;
            authorToUpdate.BirthDate = authorsModel.BirthDate ?? authorToUpdate.BirthDate;
            return true;
        }
    }
}
