namespace MoviesGallery.Services.Models.MoviesModels
{
    using MoviesGallery.Models;

    public class AddMovieInputModel
    {
        public string Title { get; set; }

        public int Length { get; set; }

        public int Ration { get; set; }

        public string Country { get; set; }

        public int GenreId { get; set; } 
    }
}