using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using DataAccessLayer;
using FinalCertWebAPI.Filters;
using FinalCertWebAPI.Models;

namespace FinalCertWebAPI.Controllers
{

    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [CustomExceptionFilter]
    public class TasksController : ApiController
    {
        private IProjectManagerContext db = new FinalFSEEntities();

        public TasksController()
        {

        }

        public TasksController(IProjectManagerContext context)
        {
            db = context;
        }

        [Route("api/TaskList/{projectId}")]
        // GET: api/Tasks
        public List<TaskDetail> GetTasks(int projectId)
        {
            var result = db.Tasks.Where(x => x.Project_ID == projectId).
                            GroupJoin(db.Parent_Task, t => t.Parent_ID, pta => pta.Parent_ID
                            , (t, pta) => new { tas = t, pat = pta.FirstOrDefault() })
                            .Select(res => new TaskDetail
                            {
                                Parent_ID = res.pat.Parent_ID,
                                Parent_Task_Name = res.pat.Parent_Task_Name,
                                Project_ID = projectId,
                                Task_ID = res.tas.Task_ID,
                                Task_Name = res.tas.Task_Name,
                                Start_Date = res.tas.Start_Date,
                                End_Date = res.tas.End_Date,
                                Priority = res.tas.Priority,
                                Status = res.tas.Status
                            }).ToList();
            return result;
        }

        // GET: api/Tasks/5
        [ResponseType(typeof(Task))]
        public List<TaskDetail> GetTask(int id)
        {
            List<TaskDetail> finalResult = new List<TaskDetail>();
            var result = new List<Task>();
            Task task = db.Tasks.Find(id);
            if (task != null)
            {
                result.Add(task);
                 finalResult = result.
                                GroupJoin(db.Users, t => t.Task_ID, pta => pta.Task_ID
                                , (t, pta) => new { tas = t, pat = pta.FirstOrDefault() })
                                .Select(res => new TaskDetail
                                {
                                    Parent_ID = res.tas.Parent_ID,
                                    Project_ID = res.tas.Project_ID,
                                    Task_ID = res.tas.Task_ID,
                                    Task_Name = res.tas.Task_Name,
                                    Start_Date = res.tas.Start_Date,
                                    End_Date = res.tas.End_Date,
                                    Priority = res.tas.Priority,
                                    Status = res.tas.Status,
                                    User_ID = res.pat.User_ID
                                }).ToList();
            }
            return finalResult;
        }

        // PUT: api/Tasks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTask(int id, Task task)
        {
            db.MarkAsModified(task);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }


            var changeUser = db.Users.Find(task.Task_ID);
            changeUser.Task_ID = null;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var user = db.Users.Find(task.User_ID);
            user.Task_ID = task.Task_ID;

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

        // POST: api/Tasks
        [ResponseType(typeof(Task))]
        public IHttpActionResult PostTask(Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tasks.Add(task);
            db.SaveChanges();

            var user = db.Users.Find(task.User_ID);
            user.Task_ID = task.Task_ID;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CreatedAtRoute("DefaultApi", new { id = task.Task_ID }, task);
        }

        // DELETE: api/Tasks/5
        [ResponseType(typeof(Task))]
        public IHttpActionResult DeleteTask(int id)
        {
            Task tasks = db.Tasks.Find(id);
            if (tasks != null)
                tasks.Status = "Completed";
            else
                return NotFound();
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok(tasks);
        }
    }
}