namespace BookStore_UI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string ISBN { get; set; }
        public decimal? Price { get; set; }
        public int AuthorId { get; set; }
        
    }
}