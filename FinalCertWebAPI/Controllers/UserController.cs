using DataAccessLayer;
using FinalCertWebAPI.Filters;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace FinalCertWebAPI.Controllers
{
    /// <summary>
    /// WebAPI controller to handle all requests related to User 
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [CustomExceptionFilter]
    public class UserController : ApiController
    {
        private IProjectManagerContext db = new FinalFSEEntities();

        public UserController()
        {

        }

        public UserController(IProjectManagerContext context)
        {
            db = context;
        }
        // GET: api/User
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/User/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/User/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.User_ID)
            {
                return BadRequest();
            }

            //db.Entry(user).State = EntityState.Modified;
            db.MarkAsModified(user);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {              
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/User

        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = user.User_ID }, user);
        }

        // DELETE: api/User/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }      
    }
}