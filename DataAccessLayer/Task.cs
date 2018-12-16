//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Task
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Task_ID { get; set; }
        public Nullable<int> Parent_ID { get; set; }
        public Nullable<int> Project_ID { get; set; }
        public string Task_Name { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        public Nullable<int> Priority { get; set; }
        public string Status { get; set; }
    
        public virtual Parent_Task Parent_Task { get; set; }
        public virtual Project Project { get; set; }
    }
}