namespace MoviesGallery.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.AccessControl;

    public class Movie
    {
        public Movie()
        {
            this.Authors = new HashSet<Author>();
            this.Reviews = new HashSet<Review>();
            this.Users = new HashSet<ApplicationUser>();
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int Length { get; set; }

        [Range(1, 10)]
        public int Ration { get; set; }

        [Required]
        public string Country { get; set; }

        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual ICollection<Author> Authors { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}