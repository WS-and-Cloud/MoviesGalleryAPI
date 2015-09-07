namespace MoviesGallery.Data.UoW
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using MoviesGallery.Data.Repositories;
    using MoviesGallery.Models;

    public class MoviesGalleryData : IMoviesGalleryData
    {
         private readonly DbContext context;
        private readonly IDictionary<Type, object> repositories;

        public MoviesGalleryData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<Movie> Movies
        {
            get
            {
                return this.GetRepository<Movie>();
            }
        }

        public IRepository<Author> Authors
        {
            get
            {
                return this.GetRepository<Author>();
            }
        }

        public IRepository<Review> Reviews
        {
            get
            {
                return this.GetRepository<Review>();
            }
        }

        public IRepository<Genre> Genres
        {
            get
            {
                return this.GetRepository<Genre>();
            }
        }

        public IRepository<ApplicationUser> ApplicationUsers
        {
            get
            {
                return this.GetRepository<ApplicationUser>();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(EFRepository<T>), this.context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        } 
    }
}