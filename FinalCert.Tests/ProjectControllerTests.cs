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
    public class ProjectControllerTests
    {
        [Test]
        public void PutProject_ShouldReturnStatusCode()
        {
            var context = new TestProjectMangerContext();
            context.Users.Add(GetDemoUser());
            var controller = new ProjectsController(context);
            
            var item = GetDemoProjectDetail();

            var result = controller.PutProject(item.Project_Id, item) as StatusCodeResult;
            Assert.IsNotNull(result);
            Assert.AreSame(result.GetType(), typeof(StatusCodeResult));
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Test]
        public void PutProject_ShouldFail_WhenDifferentID()
        {
            var controller = new ProjectsController(new TestProjectMangerContext());

            var badresult = controller.PutProject(999, GetDemoProjectDetail());
            Assert.AreSame(badresult.GetType(), typeof(BadRequestResult));
        }

        [Test]
        public void PostProject_ShouldReturnSameProduct()
        {
            var context = new TestProjectMangerContext();
            context.Users.Add(GetDemoUser());
            var controller = new ProjectsController(context);

            var item = GetDemoProjectDetail();

            var result =
                controller.PostProject(item) as CreatedAtRouteNegotiatedContentResult<Project>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.Project_Id);
            Assert.AreEqual(result.Content.Project_Name, item.Project_Name);
        }

        [Test]
        public void GetProject_ShouldReturnProjectWithSameID()
        {
            var context = new TestProjectMangerContext();
            context.Projects.Add(GetDemoProject());

            var controller = new ProjectsController(context);
            var result = controller.GetProject(1) as OkNegotiatedContentResult<Project>;

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Content.Project_Id);
        }


        [Test]
        public void GetProject_ShouldReturnNotFound()
        {
            var context = new TestProjectMangerContext();
            context.Projects.Add(GetDemoProject());

            var controller = new ProjectsController(context);
            var result = controller.GetProject(2);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetType(), typeof(NotFoundResult));
        }

        [Test]
        public void GetProjects_ShouldReturnAllProjects()
        {
            var context = new TestProjectMangerContext();

            context.Projects.Add(new Project { Project_Id = 1, Project_Name = "Project1", EndDate = DateTime.Now, StartDate = DateTime.Now, Priority = 15 });
            context.Users.Add(GetDemoUser());
            var controller = new ProjectsController(context);
            var result = controller.GetProjects();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void DeleteProject_ShouldReturnOK()
        {
            var context = new TestProjectMangerContext();
            var item = GetDemoProject();
            context.Projects.Add(item);

            var controller = new ProjectsController(context);
            var result = controller.DeleteProject(1) as OkNegotiatedContentResult<Project>;

            Assert.IsNotNull(result);
            Assert.AreEqual(item.Project_Id, result.Content.Project_Id);
        }


        [Test]
        public void DeleteProject_ShouldReturnNotFound()
        {
            var context = new TestProjectMangerContext();
            var item = GetDemoProject();
            context.Projects.Add(item);

            var controller = new ProjectsController(context);
            var result = controller.DeleteProject(2);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetType(), typeof(NotFoundResult));
        }
        ProjectDetailModel GetDemoProjectDetail()
        {
            return new ProjectDetailModel()
            {
                Project_Id = 1,
                Project_Name = "Project1",
                Priority = 10,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                User_ID = 1
            };
        }

        Project GetDemoProject()
        {
            return new Project()
            {
                Project_Id = 1,
                Project_Name = "Project1",
                Priority = 10,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
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
                Project_ID = 1
            };
        }
    }
}
