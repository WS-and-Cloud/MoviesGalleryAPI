namespace MoviesGallery.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Author
    {
        public Author()
        {
            this.Movies = new HashSet<Movie>();
            this.Fans = new HashSet<ApplicationUser>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Biography { get; set; }

        public string HomeTown { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }

        public virtual ICollection<ApplicationUser> Fans { get; set; } 
    }
}