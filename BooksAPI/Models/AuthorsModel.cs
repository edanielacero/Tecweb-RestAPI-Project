using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Models
{
    public class AuthorsModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [StringLength(30, MinimumLength =2)]
        public string Country { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
    }
}
