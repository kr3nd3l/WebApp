namespace BlazorApp.Models
{
    public class Tour
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
    }
}
