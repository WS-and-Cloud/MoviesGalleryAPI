namespace MoviesGallery.Services.Models.GenresModels
{
    using System.ComponentModel.DataAnnotations;

    public class AddGenreInputModel
    {
        [Required]
        public string Name { get; set; } 
    }
}