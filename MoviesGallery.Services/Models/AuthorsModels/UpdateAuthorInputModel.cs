namespace MoviesGallery.Services.Models.UsersModels
{
    using System;

    public class UpdateAuthorInputModel
    {
        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Biography { get; set; }

        public string HomeTown { get; set; }
    }
}