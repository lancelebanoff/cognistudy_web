using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CogniTutor
{
    [ParseClassName("Question")]
    public class Question : ParseObject
    {
        [ParseFieldName("subject")]
        public string Subject
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
        [ParseFieldName("category")]
        public string Category
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
        [ParseFieldName("inBundle")]
        public bool InBundle
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }
        [ParseFieldName("isActive")]
        public bool IsActive
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }
        [ParseFieldName("questionData")]
        public QuestionData QuestionData
        {
            get { return GetProperty<QuestionData>(); }
            set { SetProperty<QuestionData>(value); }
        }
        [ParseFieldName("questionContents")]
        public QuestionContents QuestionContents
        {
            get { return GetProperty<QuestionContents>(); }
            set { SetProperty<QuestionContents>(value); }
        }
    }
}