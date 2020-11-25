using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.SignalR;

namespace BookStore_API.Data
{
    [Table("Authors")]
    public partial class Author
    {

        public int Id{ get; set; }
        public string FirstName{ get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public virtual IList<Book> Books { get; set; }

    }
}