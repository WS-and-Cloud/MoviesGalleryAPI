namespace MoviesGallery.Services.Models.GenresModels
{
    using System.ComponentModel.DataAnnotations;

    public class UpdateGenreInputModel
    {
        [Required]
        public string Name { get; set; } 
    }
}