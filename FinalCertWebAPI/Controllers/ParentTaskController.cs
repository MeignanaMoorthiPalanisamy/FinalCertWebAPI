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

namespace FinalCertWebAPI.Controllers
{

    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [CustomExceptionFilter]
    public class ParentTaskController : ApiController
    {
        private IProjectManagerContext db = new FinalFSEEntities();


        public ParentTaskController()
        {

        }

        public ParentTaskController(IProjectManagerContext context)
        {
            db = context;
        }
        // GET: api/ParentTask
        public IQueryable<Parent_Task> GetParent_Task()
        {
            return db.Parent_Task;
        }
               
        // POST: api/ParentTask
        [ResponseType(typeof(Parent_Task))]
        public IHttpActionResult PostParent_Task(Parent_Task parent_Task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Parent_Task.Add(parent_Task);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = parent_Task.Parent_ID }, parent_Task);
        }
    }
}