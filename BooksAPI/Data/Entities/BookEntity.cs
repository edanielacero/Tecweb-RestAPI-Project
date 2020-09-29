using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Data.Entities
{
    public enum Availability
    {
        rented,
        available
    }
    public class BookEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int? rating { get; set; }
        public Availability? availability { get; set; }
    }
}
