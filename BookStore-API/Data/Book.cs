using System.ComponentModel;
using System.Security.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_API.Data
{
    public partial class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string ISBN { get; set; }
        public decimal? Price { get; set; }
        public int? AuthorId { get; set; }
        public virtual Author Author { get; set; }


    }
}