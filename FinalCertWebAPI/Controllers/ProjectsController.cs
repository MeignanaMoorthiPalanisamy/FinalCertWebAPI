using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DataAccessLayer;
using FinalCertWebAPI.Models;
using AutoMapper;
using System.Web.Http.Cors;
using FinalCertWebAPI.Filters;

namespace FinalCertWebAPI.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [CustomExceptionFilter]
    public class ProjectsController : ApiController
    {
        private IProjectManagerContext db = new FinalFSEEntities();

        public ProjectsController()
        {

        }
        public ProjectsController(IProjectManagerContext context)
        {
            db = context;
        }

        // GET: api/Projects
        public List<ProjectDetailModel> GetProjects()
        {
            //var result = db.Projects.
            //                Join(db.Users, pr => pr.Project_Id, usr => usr.Project_ID
            //                , (pr, usr) => new { pr, usr })
            //                .Select(res => new ProjectDetailModel
            //                {
            //                    Project_Id = res.pr.Project_Id,
            //                    Project_Name = res.pr.Project_Name,
            //                    StartDate = res.pr.StartDate,
            //                    EndDate = res.pr.EndDate,
            //                    Priority = (int)res.pr.Priority,
            //                    User_ID = res.usr.User_ID
            //                }).ToList();
            var result = db.Projects.
                           GroupJoin(db.Users, pr => pr.Project_Id, usr => usr.Project_ID
                           , (pr, usr) => new { proj = pr, user = usr.FirstOrDefault() })
                           .Select(res => new ProjectDetailModel
                           {
                               Project_Id = res.proj.Project_Id,
                               Project_Name = res.proj.Project_Name,
                               StartDate = res.proj.StartDate,
                               EndDate = res.proj.EndDate,
                               Priority = (int)res.proj.Priority,
                               User_ID = res.user.User_ID
                           }).ToList();
            return result;
        }

        // GET: api/Projects/5
        [ResponseType(typeof(Project))]
        public IHttpActionResult GetProject(int id)
        {
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // PUT: api/Projects/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProject(int id, ProjectDetailModel projectModal)
        {           

            if (id != projectModal.Project_Id)
            {
                return BadRequest();
            }

            var project = new Project();
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<ProjectDetailModel, Project>();

            });
            IMapper iMapper = config.CreateMapper();
            project = iMapper.Map<ProjectDetailModel, Project>(projectModal);

            //db.Entry(project).State = EntityState.Modified;
            db.MarkAsModified(project);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {               
                throw;
            }


            var changeUser = db.Users.Find(project.Project_Id);
            changeUser.Project_ID = null;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var user = db.Users.Find(projectModal.User_ID);
            user.Project_ID = project.Project_Id;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Projects
        [ResponseType(typeof(Project))]
        public IHttpActionResult PostProject(ProjectDetailModel projectModal)
        {
            var project = new Project();
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<ProjectDetailModel, Project>();

            });
            IMapper iMapper = config.CreateMapper();
            project = iMapper.Map<ProjectDetailModel, Project>(projectModal);
            db.Projects.Add(project);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {               
                throw;
            }

            var user = db.Users.Find(projectModal.User_ID);
            user.Project_ID = project.Project_Id;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;            
            }
            return CreatedAtRoute("DefaultApi", new { id = project.Project_Id }, project);
        }

        // DELETE: api/Projects/5
        [ResponseType(typeof(Project))]
        public IHttpActionResult DeleteProject(int id)
        {
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }

            db.Projects.Remove(project);
            db.SaveChanges();

            return Ok(project);
        }
    }
}