using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalCertWebAPI.Models
{
    public class ProjectDetailModel
    {
        public int Project_Id { get; set; }
        public string Project_Name { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public int Priority { get; set; }
        public int? User_ID { set; get; }
    }
}