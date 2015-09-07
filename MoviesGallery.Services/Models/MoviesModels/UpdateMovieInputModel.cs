namespace MoviesGallery.Services.Models.MoviesModels
{
    using System.ComponentModel.DataAnnotations;


    public class UpdateMovieInputModel
    {
        [Required]
        public string Title { get; set; }

        public int Length { get; set; }

        [Range(1, 10)]
        public int Ration { get; set; }

        [Required]
        public string CountryName { get; set; }

        public int GenreId { get; set; } 
    }
}