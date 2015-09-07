namespace MoviesGallery.Services.Models.MoviesModels
{
    using System;
    using System.Linq.Expressions;

    using MoviesGallery.Models;

    public class ShortMovieDataModel
    {
        public static Expression<Func<Movie, ShortMovieDataModel>> Create
        {
            get
            {
                return
                    m =>
                    new ShortMovieDataModel()
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Length = m.Length,
                        Ration = m.Ration,
                        CountryName = m.Country,
                        Genre = m.Genre == null ? "no genre" : m.Genre.Name
                    };
            }
        } 

        public int Id { get; set; }
        
        public string Title { get; set; }

        public int Length { get; set; }

        public int Ration { get; set; }

        public string CountryName { get; set; }

        public string Genre { get; set; }
    }
}