using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CogniTutor
{
    [ParseClassName("StudentTotalMonthStats")]
    public class StudentTotalMonthStats : ParseObject
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
    }
}