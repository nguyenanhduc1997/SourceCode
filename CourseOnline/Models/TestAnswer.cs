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
    
    public partial class TestAnswer
    {
        public int test_answer_id { get; set; }
        public int test_user_id { get; set; }
        public int user_id { get; set; }
        public int question_id { get; set; }
        public string user_answer { get; set; }
        public int test_id { get; set; }
    
        public virtual ExamTest ExamTest { get; set; }
        public virtual Question Question { get; set; }
        public virtual TestResult TestResult { get; set; }
    }
}
