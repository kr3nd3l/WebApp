using System.ComponentModel.DataAnnotations;

namespace API_практика.Models
{
    public class CountryDto
    {
        [Required(ErrorMessage = " Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; }
    }
}
