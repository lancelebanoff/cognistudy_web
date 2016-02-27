using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CogniTutor
{
    [ParseClassName("StudentSubjectMonthStats")]
    public class StudentSubjectMonthStats : ParseObject
    {
        [ParseFieldName("total")]
        public int Total
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }
        [ParseFieldName("correct")]
        public int Correct
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }
        [ParseFieldName("baseUserId")]
        public int BaseUserId
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }
        [ParseFieldName("blockNum")]
        public int BlockNum
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }
        [ParseFieldName("subject")]
        public string Subject
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
    }
}