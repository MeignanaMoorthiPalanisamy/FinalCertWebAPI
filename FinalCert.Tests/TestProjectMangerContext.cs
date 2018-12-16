using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace FinalCert.Tests
{
    class TestProjectMangerContext : IProjectManagerContext
    {
        public TestProjectMangerContext()
        {
            this.Users = new TestUserDbSet();
            this.Projects = new TestProjectDbSet();
        }

        public  DbSet<Parent_Task> Parent_Task { get; set; }
        public  DbSet<Project> Projects { get; set; }
        public  DbSet<Task> Tasks { get; set; }
        public  DbSet<User> Users { get; set; }

        public int SaveChanges()
        {
            return 0;
        }

        public void MarkAsModified(dynamic item) { }
        public void Dispose() { }
    }
}
