namespace MoviesGallery.Services.Models.UsersModels
{
    using System;
    using System.Linq.Expressions;

    using MoviesGallery.Models;

    public class ShortActorDataModel
    {
        public static Expression<Func<Author, ShortActorDataModel>> Create
        {
            get
            {
                return g => new ShortActorDataModel() { Name = g.Name, DateOfBirth = g.DateOfBirth};
            }
        }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}