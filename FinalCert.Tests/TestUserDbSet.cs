using System;
using System.Linq;
using DataAccessLayer;

namespace FinalCert.Tests
{
    class TestUserDbSet : TestDbSet<User>
    {
        public override User Find(params object[] keyValues)
        {
            return this.SingleOrDefault(user => user.User_ID == (int)keyValues.Single());
        }
    }

    class TestProjectDbSet : TestDbSet<Project>
    {
        public override Project Find(params object[] keyValues)
        {
            return this.SingleOrDefault(proj => proj.Project_Id == (int)keyValues.Single());
        }
    }


    class TestTaskDbSet : TestDbSet<Task>
    {
        public override Task Find(params object[] keyValues)
        {
            return this.SingleOrDefault(task => task.Task_ID == (int)keyValues.Single());
        }
    }

    class TestParentTaskDbSet : TestDbSet<Parent_Task>
    {
        public override Parent_Task Find(params object[] keyValues)
        {
            return this.SingleOrDefault(ptask => ptask.Parent_ID == (int)keyValues.Single());
        }
    }
}