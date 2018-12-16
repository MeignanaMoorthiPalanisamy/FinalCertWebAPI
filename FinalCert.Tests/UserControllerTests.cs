using System;
using System.Net;
using System.Web.Http.Results;
using DataAccessLayer;
using FinalCertWebAPI.Controllers;
using NUnit.Framework;

namespace FinalCert.Tests
{
    /// <summary>
    /// User Controller Unit Test cases covers most of the scenarios
    /// </summary>
    [TestFixture]
    public class UserControllerTests
    {
        [Test]
        public void PutUser_ShouldReturnStatusCode()
        {
            var controller = new UserController(new TestProjectMangerContext());

            var item = GetDemoUser();

            var result = controller.PutUser(item.User_ID, item) as StatusCodeResult;
            Assert.IsNotNull(result);
            Assert.AreSame(result.GetType(), typeof(StatusCodeResult));
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Test]
        public void PutUser_ShouldFail_WhenDifferentID()
        {
            var controller = new UserController(new TestProjectMangerContext());

            var badresult = controller.PutUser(999, GetDemoUser());
            Assert.AreSame(badresult.GetType(), typeof(BadRequestResult));
        }

        [Test]
        public void PutUser_ModelStateError()
        {
            var controller = new UserController(new TestProjectMangerContext());
            controller.ModelState.AddModelError("fakeError", new Exception());
            var item = GetDemoUser();
            var invalidModelState = controller.PutUser(item.User_ID, item);
            Assert.AreEqual(invalidModelState.GetType().Name, "InvalidModelStateResult");
        }

        [Test]
        public void PostUser_ShouldReturnSameProduct()
        {
            var controller = new UserController(new TestProjectMangerContext());

            var item = GetDemoUser();

            var result =
                controller.PostUser(item) as CreatedAtRouteNegotiatedContentResult<User>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.User_ID);
            Assert.AreEqual(result.Content.FirstName, item.FirstName);
        }

        [Test]
        public void PostUser_ModelStateError()
        {
            var controller = new UserController(new TestProjectMangerContext());
            controller.ModelState.AddModelError("fakeError", new Exception());
            var item = GetDemoUser();
            var invalidModelState = controller.PostUser(item);
            Assert.AreEqual(invalidModelState.GetType().Name, "InvalidModelStateResult");
        }

        [Test]
        public void GetUser_ShouldReturnUserWithSameID()
        {
            var context = new TestProjectMangerContext();
            context.Users.Add(GetDemoUser());

           var controller = new UserController(context);
            var result = controller.GetUser(1) as OkNegotiatedContentResult<User>;

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Content.User_ID);
        }


        [Test]
        public void GetUser_ShouldReturnNotFound()
        {
            var context = new TestProjectMangerContext();
            var item = GetDemoUser();
            context.Users.Add(item);

            var controller = new UserController(context);
            var result = controller.GetUser(2);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetType(), typeof(NotFoundResult));
        }

        [Test]
        public void GetUsers_ShouldReturnAllUsers()
        {
            var context = new TestProjectMangerContext();
            context.Users.Add(new User { User_ID = 1, FirstName = "Demo1" });
            context.Users.Add(new User { User_ID = 2, FirstName = "Demo1" });
            context.Users.Add(new User { User_ID = 3, FirstName = "Demo1" });

            var controller = new UserController(context);
            var result = controller.GetUsers() as TestUserDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Local.Count);
        }

        [Test]
        public void DeleteUser_ShouldReturnOK()
        {
            var context = new TestProjectMangerContext();
            var item = GetDemoUser();
            context.Users.Add(item);

            var controller = new UserController(context);
            var result = controller.DeleteUser(1) as OkNegotiatedContentResult<User>;

            Assert.IsNotNull(result);
            Assert.AreEqual(item.User_ID, result.Content.User_ID);
        }


        [Test]
        public void DeleteUser_ShouldReturnNotFound()
        {
            var context = new TestProjectMangerContext();
            var item = GetDemoUser();
            context.Users.Add(item);

            var controller = new UserController(context);
            var result = controller.DeleteUser(2);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetType(),typeof(NotFoundResult));
        }
        User GetDemoUser()
        {
            return new User()
            {
                Employee_ID = "407957",
                FirstName = "Meignana Moorthi",
                LastName = "Palanisamy",
                User_ID = 1
            };
        }
    }
}
