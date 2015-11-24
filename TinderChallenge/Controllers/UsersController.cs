using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TinderChallenge.Models;

namespace TinderChallenge.Controllers
{
    // No need to authorize in a code challenge API
    //[Authorize]
    public class UsersController : ApiController
    {
        // Private (fake -- for simulation) data context
        private List<UserModel> DataContext = new List<UserModel>()
        {
            new UserModel()
            {
                UserId = 1,
                FirstName = "Test",
                LastName = "User1",
                Gender = Gender.Male,
                Age = 21,
                InterestedInFinding = LookingFor.Women,
                ProfilePicture = "TestUser1.jpg",
                Likes = new List<int> { 2, 4 },
                Passes = new List<int> { 7 }
            },
            new UserModel()
            {
                UserId = 2,
                FirstName = "Test",
                LastName = "User2",
                Gender = Gender.Female,
                Age = 20,
                InterestedInFinding = LookingFor.Men,
                ProfilePicture = "TestUser2.jpg",
                Likes = new List<int> { 1, 4 },
                Passes = new List<int> { 6 }
            },
            new UserModel()
            {
                UserId = 3,
                FirstName = "Test",
                LastName = "User3",
                Gender = Gender.Male,
                Age = 26,
                InterestedInFinding = LookingFor.Men,
                ProfilePicture = "TestUser3.jpg",
                Likes = new List<int> { 5 },
                Passes = new List<int> { 6 }
            },
            new UserModel()
            {
                UserId = 4,
                FirstName = "Test",
                LastName = "User4",
                Gender = Gender.Female,
                Age = 25,
                InterestedInFinding = LookingFor.Both,
                ProfilePicture = "TestUser4.jpg",
                Likes = new List<int> { 1, 5 },
                Passes = new List<int> { 7 }
            },
            new UserModel()
            {
                UserId = 5,
                FirstName = "Test",
                LastName = "User5",
                Gender = Gender.Male,
                Age = 30,
                InterestedInFinding = LookingFor.Both,
                ProfilePicture = "TestUser5.jpg",
                Likes = new List<int> { 3 },
                Passes = new List<int> { 6 }
            },
            new UserModel()
            {
                UserId = 6,
                FirstName = "Test",
                LastName = "User6",
                Gender = Gender.Male,
                Age = 30,
                InterestedInFinding = LookingFor.Both,
                ProfilePicture = "TestUser6.jpg",
                Likes = new List<int> {  },
                Passes = new List<int> {  }
            },
            new UserModel()
            {
                UserId = 7,
                FirstName = "Test",
                LastName = "User7",
                Gender = Gender.Female,
                Age = 29,
                InterestedInFinding = LookingFor.Both,
                ProfilePicture = "TestUser7.jpg",
                Likes = new List<int> { 6 },
                Passes = new List<int> { 1, 3 }
            }
        };


        // No creating users from the API, and no listing all users,  either.

        // GET api/users/{userId}
        [Route("api/users/{userId}")]
        public IHttpActionResult GetUserProfile(int userId)
        {
            var user = DataContext.FirstOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/users/matches
        [Route("api/users/{userId}/matches")]
        public IHttpActionResult GetUserMatches(int userId)
        {
            List<int> matches = new List<int>();

            var user = DataContext.FirstOrDefault(u => u.UserId == userId);
            if(user != null)
            {
                // Check each of the likes for our current user to see if they also like the user for a match
                foreach(var like in user.Likes)
                {
                    var prospect = DataContext.FirstOrDefault(p => p.UserId == like);
                    if(prospect != null)
                    {
                        if(prospect.Likes.Contains(userId))
                        {
                            matches.Add(prospect.UserId);
                        }
                    }
                }
            }

            // Retrieve the user's matches.
            return Ok(matches);
        }

        // GET api/users/{userId}/likes
        [Route("api/users/{userId}/likes")]
        public IHttpActionResult GetUserLikes(int userId)
        {
            List<int> likes = new List<int>();

            var user = DataContext.FirstOrDefault(u => u.UserId == userId);

            if (user != null)
            {
                return Ok(user.Likes);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/users/{userId}/likes/
        [Route("api/users/{userId}/likes/")]
        public IHttpActionResult UserLikesAnotherUser(int userId, [FromBody] UserModel user)
        {
            var currentUser = DataContext.FirstOrDefault(u => u.UserId == userId);

            if (currentUser != null)
            {
                currentUser.Likes.Add(user.UserId);
                return Ok(currentUser);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/users/{userId}/passes/
        [Route("api/users/{userId}/passes/")]
        public IHttpActionResult UserPassesOnAnotherUser(int userId, [FromBody] UserModel user)
        {
            var currentUser = DataContext.FirstOrDefault(u => u.UserId == userId);

            if (currentUser != null)
            {
                if (currentUser.Likes.Contains(user.UserId))
                {
                    currentUser.Likes.Remove(user.UserId);
                    currentUser.Passes.Add(user.UserId);
                }
                else
                {
                    currentUser.Passes.Add(user.UserId);
                }
                
                return Ok(currentUser);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
