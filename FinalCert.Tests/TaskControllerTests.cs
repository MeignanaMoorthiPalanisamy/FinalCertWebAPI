using DataAccessLayer;
using FinalCertWebAPI.Controllers;
using FinalCertWebAPI.Models;
using NUnit.Framework;
using System;
using System.Net;
using System.Web.Http.Results;

namespace FinalCert.Tests
{
    [TestFixture]
    public class TaskControllerTests
    {
        [Test]
        public void PutTask_ShouldReturnStatusCode()
        {
            var context = new TestProjectMangerContext();
            var item = GetDemoTask();
            context.Users.Add(GetDemoUser());
            var controller = new TasksController(context);
            var result = controller.PutTask(item.Task_ID, item) as StatusCodeResult;
            Assert.IsNotNull(result);
            Assert.AreSame(result.GetType(), typeof(StatusCodeResult));
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Test]
        public void PostTask_ShouldReturnSameProduct()
        {
            var context = new TestProjectMangerContext();
            var controller = new TasksController(context);
            context.Users.Add(GetDemoUser());

            var item = GetDemoTask();

            var result =
                controller.PostTask(item) as CreatedAtRouteNegotiatedContentResult<Task>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.Task_ID);
            Assert.AreEqual(result.Content.Task_Name, item.Task_Name);
        }

        [Test]
        public void PostTask_ModelStateError()
        {
            var controller = new TasksController(new TestProjectMangerContext());
            controller.ModelState.AddModelError("fakeError", new Exception());
            var item = GetDemoTask();
            var invalidModelState = controller.PostTask(item);
            Assert.AreEqual(invalidModelState.GetType().Name, "InvalidModelStateResult");
        }
        [Test]
        public void GetTask_ShouldReturnProjectWithSameID()
        {
            var context = new TestProjectMangerContext();
            context.Tasks.Add(GetDemoTask());
            context.Users.Add(GetDemoUser());
            var controller = new TasksController(context);
            var result = controller.GetTask(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }


        [Test]
        public void GetTask_ShouldReturnNotFound()
        {
            var context = new TestProjectMangerContext();
            context.Tasks.Add(GetDemoTask());
            context.Users.Add(GetDemoUser());

            var controller = new TasksController(context);
            var result = controller.GetTask(2);

            Assert.AreEqual(0,result.Count);
        }

        [Test]
        public void GetTasks_ShouldReturnAllProjects()
        {
            var context = new TestProjectMangerContext();

            context.Parent_Task.Add(GetDemoParentTask());
            context.Tasks.Add(GetDemoTask());
            var controller = new TasksController(context);
            var result = controller.GetTasks(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void DeleteTask_ShouldReturnOK()
        {
            var context = new TestProjectMangerContext();
            var item = GetDemoTask();
            context.Tasks.Add(item);

            var controller = new TasksController(context);
            var result = controller.DeleteTask(1) as OkNegotiatedContentResult<Task>;

            Assert.IsNotNull(result);
            Assert.AreEqual(item.Task_ID, result.Content.Task_ID);
        }


        [Test]
        public void DeleteTask_ShouldReturnNotFound()
        {
            var context = new TestProjectMangerContext();
            var item = GetDemoTask();
            context.Tasks.Add(item);

            var controller = new TasksController(context);
            var result = controller.DeleteTask(2);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetType(), typeof(NotFoundResult));
        }
        Parent_Task GetDemoParentTask()
        {
            return new Parent_Task()
            {
                Parent_ID = 1,
                Parent_Task_Name = "Parent Task 1"
            };
        }

        Task GetDemoTask()
        {
            return new Task()
            {
                Task_ID = 1,
                Task_Name = "Task 1",
                End_Date = DateTime.Now,
                Start_Date = DateTime.Now,
                Priority = 10,
                Project_ID = 1,
                User_ID = 1,
                Parent_ID = 1,
                Status = "Not Started"

            };
        }

        User GetDemoUser()
        {
            return new User()
            {
                Employee_ID = "407957",
                FirstName = "Meignana Moorthi",
                LastName = "Palanisamy",
                User_ID = 1,
                Project_ID = 1,
                Task_ID = 1
            };
        }
    }
}
