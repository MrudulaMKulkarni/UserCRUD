using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace User_TestProject
{
    public class UnitTest1
    {
        UserCRUD.Controllers.UserController userController;
        UserCRUD.Services.UserService userService;
        public UnitTest1()
        {
            UserCRUD.Models.UserDatabaseSettings userDatabaseSettings = new UserCRUD.Models.UserDatabaseSettings();
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            IConfigurationSection section = config.GetSection("UserDatabaseSettings");

            userDatabaseSettings.UsersCollectionName = section["UsersCollectionName"];
            userDatabaseSettings.DatabaseName = section["DatabaseName"];
            userDatabaseSettings.ConnectionString = section["ConnectionString"];

            if (userService == null)
                userService = new UserCRUD.Services.UserService(userDatabaseSettings);

            userController = new UserCRUD.Controllers.UserController(userService);
        }
        [Fact]
        public void GetAllTest()
        {
            //Arrange
            //Act
            var result = userController.Get();
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var list = result.Result as OkObjectResult;
            Assert.IsType<List<UserCRUD.Models.User>>(list.Value);
            var listUsers = list.Value as List<UserCRUD.Models.User>;
            Assert.Equal(10, listUsers.Count);
        }
        [Theory]
        [InlineData("626282f74fe955eda11ecfae", "626282f74fe955eda11ecfa1")]
        public void GetUserByIdTest(string id1, string id2)
        {
            //Arrange
            var validGuid = id1;
            var invalidGuid = id2;

            //Act
            var notFoundResult = userController.Get(invalidGuid);
            var okResult = userController.Get(validGuid);

            //Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
            Assert.IsType<OkObjectResult>(okResult.Result);


            //Now we need to check the value of the result for the ok object result.
            var item = okResult.Result as OkObjectResult;

            //We Expect to return a single User
            Assert.IsType<UserCRUD.Models.User>(item.Value);

            //Now, let us check the value itself.
            var usr = item.Value as UserCRUD.Models.User;
            Assert.Equal(validGuid, usr._id);
            Assert.Equal("Bret", usr.username);
        }
        [Fact]
        public void AddUserTest()
        {
            //OK RESULT TEST START

            //Arrange
            var completeUser = new UserCRUD.Models.User()
            {
                id = 12,
                name = "Test Graham",
                username = "Gret",
                email = "Sincere@june.biz",
                address = new UserCRUD.Models.address() { street = "Test Street", suite = "Test Suite", city = "Test City", zipcode = "92998-3874", geo = new UserCRUD.Models.geo() { lat = "-37.3159", lng = "81.1496" } },
                phone = "1-770-736-8031 56442",
                website = "hildegard.org",
                company = new UserCRUD.Models.company() { name = "Test Company", catchPhrase = "Test Catch Phrase", bs = "Test BS" }
            };

            //Act
            var createdResponse = userController.Post(completeUser);

            //Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);

            //value of the result
            var item = createdResponse as CreatedAtActionResult;
            Assert.IsType<UserCRUD.Models.User>(item.Value);

            //check value of this User
            var UserItem = item.Value as UserCRUD.Models.User;
            Assert.Equal(completeUser.id, UserItem.id);
            Assert.Equal(completeUser.name, UserItem.name);
            Assert.Equal(completeUser.username, UserItem.username);
            Assert.Equal(completeUser.email, UserItem.email);
            Assert.Equal(completeUser.address.street, UserItem.address.street);
            Assert.Equal(completeUser.address.suite, UserItem.address.suite);
            Assert.Equal(completeUser.address.city, UserItem.address.city);
            Assert.Equal(completeUser.address.zipcode, UserItem.address.zipcode);
            Assert.Equal(completeUser.address.geo.lat, UserItem.address.geo.lat);
            Assert.Equal(completeUser.address.geo.lng, UserItem.address.geo.lng);
            Assert.Equal(completeUser.phone, UserItem.phone);
            Assert.Equal(completeUser.website, UserItem.website);
            Assert.Equal(completeUser.company.name, UserItem.company.name);
            Assert.Equal(completeUser.company.catchPhrase, UserItem.company.catchPhrase);
            Assert.Equal(completeUser.company.bs, UserItem.company.bs);

            //OK RESULT TEST END
        }
        [Theory]
        [InlineData("626783f212104a228d74c5c1", "626a94fa356a71de2722fbb2")]
        public void RemoveUserByIdTest(string id1, string id2)
        {
            //Arrange
            var validGuid = id1;
            var invalidGuid = id2;

            //Act
            var notFoundResult = userController.Delete(invalidGuid);

            //Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
            Assert.Equal(10, userService.Get().Count());

            //Act
            var okResult = userController.Delete(validGuid);

            //Assert
            Assert.IsType<OkResult>(okResult);
            Assert.Equal(9, userService.Get().Count());
        }
        [Fact]
        public void Task_Update_ValidData_Return_OkResult()
        {
            //Arrange  
            var postId = "626783f212104a228d74c5c1";
            var completeUser = new UserCRUD.Models.User()
            {
                name = "Test Test updated",
                address = new UserCRUD.Models.address() { street = "", suite = "", city = "", zipcode = "", geo = new UserCRUD.Models.geo() { lat = "", lng = "" } },
                company = new UserCRUD.Models.company() { name = "", catchPhrase = "", bs = "" }
            };

            //Act  
            var existingPost = userController.Get(postId);
            Assert.IsType<OkObjectResult>(existingPost.Result);
            //Now we need to check the value of the result for the ok object result.
            var item = existingPost.Result as OkObjectResult;
            //We Expect to return a single User
            Assert.IsType<UserCRUD.Models.User>(item.Value);
            //Now, let us check the value itself.
            var usr = item.Value as UserCRUD.Models.User;
            completeUser.id = usr.id;
            var updatedData = userController.Put(postId, completeUser);

            //Assert  
            Assert.IsType<OkResult>(updatedData);
        }

        [Fact]
        public void Task_Update_InvalidData_Return_NotFound()
        {
            //Arrange  
            var postId = "626783f212104a228d74c5c2";
            var completeUser = new UserCRUD.Models.User()
            {
                name = "Test Test updated",
                address = new UserCRUD.Models.address() { street = "", suite = "", city = "", zipcode = "", geo = new UserCRUD.Models.geo() { lat = "", lng = "" } },
                company = new UserCRUD.Models.company() { name = "", catchPhrase = "", bs = "" }
            };

            //Act  
            var existingPost = userController.Get(postId);

            //Assert  
            Assert.IsType<NotFoundResult>(existingPost.Result);
        }
    }
}
