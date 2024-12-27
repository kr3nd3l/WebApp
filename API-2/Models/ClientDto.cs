using System.ComponentModel.DataAnnotations;

namespace API_2.Models
{
    public class ClientDto
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; } = "";
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; } = "";
        [Required, EmailAddress]
        public string Email { get; set; } = "";
        [Phone] 
        public string Phone { get; set; } = "";
    }
}
