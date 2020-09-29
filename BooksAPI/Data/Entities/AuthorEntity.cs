using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Data.Entities
{
    public class AuthorEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
