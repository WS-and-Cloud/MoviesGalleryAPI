namespace MoviesGallery.Services.Models.UsersModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AddReviewInputModel
    {
        public int MovieId { get; set; }

        public string UserId { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime DateOfCreation { get; set; } 
    }
}