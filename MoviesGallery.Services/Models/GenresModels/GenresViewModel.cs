namespace MoviesGallery.Services.Models.GenresModels
{
    using System;
    using System.Linq.Expressions;

    using MoviesGallery.Models;

    public class GenresViewModel
    {
        public static Expression<Func<Genre, GenresViewModel>> Create
        {
            get
            {
                return g => new GenresViewModel() { Id = g.Id, Name = g.Name };
            }
        } 

        public int Id { get; set; }

        public string Name { get; set; }
    }
}