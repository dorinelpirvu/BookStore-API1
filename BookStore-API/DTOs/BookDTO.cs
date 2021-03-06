﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_API.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string ISBN { get; set; }
        public decimal? Price { get; set; }
        public int AuthorId { get; set; }
        public virtual AuthorDTO Author { get; set; }
    }
    public class BookCreateDTO
    {
       
        public string Title { get; set; }
        public string Year { get; set; }
        public string ISBN { get; set; }
        public decimal? Price { get; set; }
        public int AuthorId { get; set; }
        
    }
    public class BookUpdateDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string ISBN { get; set; }
        public decimal? Price { get; set; }
        public int AuthorId { get; set; }

    }
}
