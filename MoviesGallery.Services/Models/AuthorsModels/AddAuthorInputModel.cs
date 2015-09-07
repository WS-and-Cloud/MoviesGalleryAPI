namespace MoviesGallery.Services.Models.UsersModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AddAuthorInputModel
    {
        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public string Biography { get; set; }

        public string HomeTown { get; set; } 
    }
}