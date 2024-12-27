
using System.ComponentModel.DataAnnotations;
namespace API_практика.Models
{
    public class ClientDto
    {

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }

    }


}

