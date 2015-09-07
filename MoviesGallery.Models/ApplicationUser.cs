namespace MoviesGallery.Models
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.FavouriteAuthors = new HashSet<Author>();
            this.FavouriteMovies = new HashSet<Movie>();
            this.Reviews = new HashSet<Review>();
        }

        public string PersonalPage { get; set; }

        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }
        
        public virtual ICollection<Movie> FavouriteMovies { get; set; }

        public virtual ICollection<Author> FavouriteAuthors { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }
    }
}