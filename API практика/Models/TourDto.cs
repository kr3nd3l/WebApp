using API.Models;
using System.ComponentModel.DataAnnotations;

namespace API_практика.Models
{
    public class TourDto
    {
        [Required(ErrorMessage = "CountryId is required")]
        public int CountryId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
    }
}
