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

    public partial class Parent_Task
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Parent_ID { get; set; }
        public string Parent_Task_Name { get; set; }
    }
}
