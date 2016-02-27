using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CogniTutor
{
    [ParseClassName("QuestionData")]
    public class QuestionData : ParseObject
    {
        [ParseFieldName("responses")]
        public ParseRelation<ParseObject> Responses
        {
            get { return GetProperty<ParseRelation<ParseObject>>(); }
            set { SetProperty<ParseRelation<ParseObject>>(value); }
        }
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
        [ParseFieldName("reviews")]
        public ParseRelation<ParseObject> Reviews
        {
            get { return GetProperty<ParseRelation<ParseObject>>(); }
            set { SetProperty<ParseRelation<ParseObject>>(value); }
        }
        [ParseFieldName("newlyApproved")]
        public bool NewlyApproved
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }
        [ParseFieldName("reviewStatus")]
        public string ReviewStatus
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
    }
}