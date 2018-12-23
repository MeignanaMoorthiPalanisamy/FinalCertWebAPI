using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalCertWebAPI.Models
{
    public class TaskDetail
    {
        public int Task_ID { get; set; }
        public Nullable<int> Parent_ID { get; set; }
        public Nullable<int> Project_ID { get; set; }
        public string Task_Name { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        public Nullable<int> Priority { get; set; }
        public string Status { get; set; }

        public string Parent_Task_Name { get; set; }

        public int User_ID { set; get; }
        
    }
}