using System;
using System.Web.Http.Results;
using DataAccessLayer;
using FinalCertWebAPI.Controllers;
using NUnit.Framework;

namespace FinalCert.Tests
{
    [TestFixture]
    public class ParentTaskControllerTests
    {
        [Test]
        public void PostParentTask_ShouldReturnSameParentTask()
        {
            var controller = new ParentTaskController(new TestProjectMangerContext());

            var item = GetDemoParentTask();

            var result =
                controller.PostParent_Task(item) as CreatedAtRouteNegotiatedContentResult<Parent_Task>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.Parent_ID);
            Assert.AreEqual(result.Content.Parent_Task_Name, item.Parent_Task_Name);
        }

        [Test]
        public void PostParentTask_ModelStateError()
        {
            var controller = new ParentTaskController(new TestProjectMangerContext());
            controller.ModelState.AddModelError("fakeError", new Exception());
            var item = GetDemoParentTask();
            var invalidModelState = controller.PostParent_Task(item);
            Assert.AreEqual(invalidModelState.GetType().Name, "InvalidModelStateResult");
        }


        [Test]
        public void GetParentTask_ShouldReturnAllUsers()
        {
            var context = new TestProjectMangerContext();
            context.Parent_Task.Add(new Parent_Task { Parent_ID = 1, Parent_Task_Name = "Parent Task 1"});
            context.Parent_Task.Add(new Parent_Task { Parent_ID = 2, Parent_Task_Name = "Parent Task 2" });

            var controller = new ParentTaskController(context);
            var result = controller.GetParent_Task() as TestParentTaskDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Local.Count);
        }
        Parent_Task GetDemoParentTask()
        {
            return new Parent_Task()
            {
                Parent_ID = 1,
                Parent_Task_Name = "Parent Task 1"
            };
        }
    }
}
