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
    
    public partial class Coursework
    {
        public int coursework_id { get; set; }
        public int assignment_id { get; set; }
        public int course_id { get; set; }
        public int test_id { get; set; }
        public string due_date { get; set; }
        public string coursework_type { get; set; }
        public bool coursework_status { get; set; }
    
        public virtual Course Course { get; set; }
        public virtual Course Course1 { get; set; }
        public virtual ExamTest ExamTest { get; set; }
    }
}
