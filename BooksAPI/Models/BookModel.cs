using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Models
{
    public enum Availability
    {
        rented,
        available
    }
    public class BookModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [StringLength(30, MinimumLength =2)]
        public string Author { get; set; }
        public int rating { get; set; }
        public Availability availability { get; set; }
    }
}
