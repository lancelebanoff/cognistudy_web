using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CogniTutor
{
    [ParseClassName("Tutor")]
    public class Tutor : ParseObject
    {
        [ParseFieldName("numQuestionsCreated")]
        public int NumQuestionsCreated
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }
        [ParseFieldName("numQuestionsReviewed")]
        public int NumQuestionsReviewed
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }
        [ParseFieldName("biography")]
        public string Biography
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
        [ParseFieldName("baseUserId")]
        public string BaseUserId
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
        [ParseFieldName("privateTutorData")]
        public PrivateTutorData PrivateTutorData
        {
            get { return GetProperty<PrivateTutorData>(); }
            set { SetProperty<PrivateTutorData>(value); }
        }
    }
}