using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CogniTutor
{
    [ParseClassName("StudentCategoryStats")]
    public class StudentCategoryStats : ParseObject
    {
        [ParseFieldName("totalResponses")]
        public int TotalResponses
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }
        [ParseFieldName("correctResponses")]
        public int CorrectResponses
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
        [ParseFieldName("category")]
        public string Category
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
    }
}