﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseOnline.Models
{
    public class QuestionModel
    {
        public int questionID { get; set; }
        public string questiontext { get; set; }
        public string subjectname { get; set; }
        public int subjectid { get; set; }
        public string useranswer { get; set; }
        public string correctanswer { get; set; }
        public ICollection<AnswerModel> answers { get; set; }

    }
    public class AnswerModel
    {
        public int answerID { get; set; }
        public string answertext { get; set; }
        public bool isCorrect { get; set; }
    }
    public class QuizResultModel
    {
        public int questionID { get; set; }
        public string questiontext { get; set; }
        public string answertext { get; set; }
        public bool isCorrect { get; set; }
        public string answercorrect { get; set; }
        public int timeduration { get; set; }
    }

    public class LessonQuizModel : Lesson
    {
        public int test_id { get; set; }
        public string test_name { get; set; }
        public string due_date { get; set; }
    }

    public class ConfigModel
    {
        public int subject_id { get; set; }
        public string exam_level { get; set; }
        public int lesson_id { get; set; }
        public int lesson_size { get; set; }
        public int domain_id { get; set; }
        public int domain_size { get; set; }
        public double exam_duration { get; set; }
        public string due_date { get; set; }
        public string test_code { get; set; }
        public int test_id { get; set; }
        public int coursework_id { get; set; }
        public int exam_id { get; set; }
        public double pass_rate { get; set; }
    }
}