﻿using System.ComponentModel.DataAnnotations;

namespace BlazorApp2.Models
{
    public class ClientDto
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
    }
}
