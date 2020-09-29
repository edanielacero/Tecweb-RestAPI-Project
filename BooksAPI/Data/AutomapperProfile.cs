using AutoMapper;
using BooksAPI.Data.Entities;
using BooksAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Data
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {
            this.CreateMap<BookEntity, BookModel>().ReverseMap();
        }
    }
}
