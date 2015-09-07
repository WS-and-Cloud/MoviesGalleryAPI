namespace MoviesGallery.Services.Models.UsersModels
{
    using System;
    using System.Linq.Expressions;

    using MoviesGallery.Models;

    public class AllUsersOutputModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public DateTime BirthDate { get; set; }

        public string PersonalPage { get; set; }

        public string Gender { get; set; }

        public static Expression<Func<ApplicationUser, AllUsersOutputModel>> Create
        {
            get
            {
                return
                    u =>
                    new AllUsersOutputModel
                    {
                        Id = u.Id,
                        Username = u.UserName,
                        BirthDate = u.DateOfBirth,
                        PersonalPage = u.PersonalPage,
                        Gender = u.Gender.ToString()
                    };
            }
        } 
    }
}