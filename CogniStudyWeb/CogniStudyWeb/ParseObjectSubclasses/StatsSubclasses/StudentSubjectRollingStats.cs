using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CogniTutor
{
    [ParseClassName("StudentSubjectRollingStats")]
    public class StudentSubjectRollingStats : ParseObject
    {
        [ParseFieldName("correctAllTime")]
        public int CorrectAllTime
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }
        [ParseFieldName("correctPastMonth")]
        public int CorrectPastMonth
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }
        [ParseFieldName("totalAllTime")]
        public int TotalAllTime
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }
        [ParseFieldName("totalPastWeek")]
        public int TotalPastWeek
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }
        [ParseFieldName("totalPastMonth")]
        public int TotalPastMonth
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }
        [ParseFieldName("correctPastWeek")]
        public int CorrectPastWeek
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }
        [ParseFieldName("baseUserId")]
        public string BaseUserId
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
        [ParseFieldName("subject")]
        public string Subject
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
    }
}