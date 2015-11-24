using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TinderChallenge;
using TinderChallenge.Controllers;
using TinderChallenge.Models;

namespace TinderChallenge.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        [TestMethod]
        public void GetUserProfile_Test()
        {
            // Arrange
            UsersController controller = new UsersController();
            
            // Act
            var userId = 1;

            // GET api/users/{userId}
            var result = controller.GetUserProfile(userId);

            // Assert
            Assert.IsNotNull(result);
            var castResult = result as OkNegotiatedContentResult<UserModel>;
            var actualResult = castResult.Content;

            Assert.AreEqual("Test", actualResult.FirstName);
        }

        [TestMethod]
        public void GetUserMatches_Test()
        {
            // Arrange
            UsersController controller = new UsersController();

            // Act
            var userId = 1;

            // GET api/users/{userId}/matches
            var result = controller.GetUserMatches(userId);

            // Assert
            Assert.IsNotNull(result);
            var castResult = result as OkNegotiatedContentResult<List<int>>;
            var actualResult = castResult.Content;

            Assert.AreEqual(2, actualResult.Count);
        }

        [TestMethod]
        public void GetUserLikes_Test()
        {
            // Arrange
            UsersController controller = new UsersController();

            // Act
            var userId = 1;

            // GET api/users/{userId}/likes
            var result = controller.GetUserLikes(userId);

            // Assert
            Assert.IsNotNull(result);
            var castResult = result as OkNegotiatedContentResult<List<int>>;
            var actualResult = castResult.Content;

            Assert.AreEqual(2, actualResult.Count);
        }

        [TestMethod]
        public void PostUserLikesAnotherUser_Test()
        {
            // Arrange
            UsersController controller = new UsersController();

            // Act
            var userId = 1;
            var userModel = new UserModel();
            userModel.UserId = 8;

            // POST api/users/{userId}/likes/
            var result = controller.UserLikesAnotherUser(userId, userModel);

            // Assert
            Assert.IsNotNull(result);
            var castResult = result as OkNegotiatedContentResult<UserModel>;
            var actualResult = castResult.Content;

            Assert.AreEqual(3, actualResult.Likes.Count);
        }

        [TestMethod]
        public void PostUserPassesOnAnotherUser_Test()
        {
            // Arrange
            UsersController controller = new UsersController();

            // Act
            var userId = 1;
            var userModel = new UserModel();
            userModel.UserId = 9;

            // POST api/users/{userId}/passes/
            var result = controller.UserPassesOnAnotherUser(userId, userModel);

            // Assert
            Assert.IsNotNull(result);
            var castResult = result as OkNegotiatedContentResult<UserModel>;
            var actualResult = castResult.Content;

            Assert.AreEqual(2, actualResult.Passes.Count);
        }
    }
}