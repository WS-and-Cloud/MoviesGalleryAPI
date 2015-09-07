namespace MoviesGallery.Services.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;

    using MoviesGallery.Data;
    using MoviesGallery.Data.UoW;
    using MoviesGallery.Models;
    using MoviesGallery.Services.Models.UsersModels;

    [RoutePrefix("api/user")]
    public class UserController : BaseApiController
    {
        public UserController()
            : base(new MoviesGalleryData(new MoviesGalleryDbContext()))
        {
        }

        // GET api/user
        [HttpGet]
        public IHttpActionResult GetAllUsers()
        {
            var allUsers =
                this.MoviesGalleryData.ApplicationUsers.All()
                    .OrderByDescending(u => u.DateOfBirth)
                    .ThenBy(u => u.Id)
                    .Select(AllUsersOutputModel.Create);

            return this.Ok(allUsers);
        }

        // GET api/user/{id}
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetUserById(string id)
        {
            var user = this.MoviesGalleryData.ApplicationUsers.Find(id);
            if (user == null)
            {
                return this.NotFound();
            }

            var result =
                this.MoviesGalleryData.ApplicationUsers.All()
                    .Where(u => u.Id == user.Id)
                    .Select(AllUsersOutputModel.Create)
                    .FirstOrDefault();

            return this.Ok(result);
        }

        // GET api/user/gender
        [HttpGet]
        public IHttpActionResult GetUsersByGender(string gender)
        {
            Gender genderAsEnum = (Gender)Enum.Parse(typeof(Gender), gender, true);
            var users =
                this.MoviesGalleryData.ApplicationUsers.All()
                    .OrderBy(u => u.Id)
                    .Where(u => u.Gender == genderAsEnum)
                    .Select(AllUsersOutputModel.Create);
           
            return this.Ok(users);
        }
    }
}