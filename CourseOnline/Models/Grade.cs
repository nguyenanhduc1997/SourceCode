//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CourseOnline.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Grade
    {
        public int grade_id { get; set; }
        public int registration_id { get; set; }
        public int course_id { get; set; }
        public int user_id { get; set; }
        public int coursework_id { get; set; }
        public string grade_comment { get; set; }
        public double grade_user { get; set; }
    
        public virtual Course Course { get; set; }
        public virtual Coursework Coursework { get; set; }
        public virtual Registration Registration { get; set; }
        public virtual User User { get; set; }
    }
}
