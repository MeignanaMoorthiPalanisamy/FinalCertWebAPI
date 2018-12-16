using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IProjectManagerContext : IDisposable
    {

       DbSet<Parent_Task> Parent_Task { get; set; }
       DbSet<Project> Projects { get; set; }
       DbSet<Task> Tasks { get; set; }
       DbSet<User> Users { get; set; }
        int SaveChanges();
        void MarkAsModified(dynamic item);
    }
}
