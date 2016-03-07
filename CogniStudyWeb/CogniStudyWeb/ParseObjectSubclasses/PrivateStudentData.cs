using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CogniTutor
{
    [ParseClassName("PrivateStudentData")]
    public class PrivateStudentData : ParseObject
    {
        [ParseFieldName("numCoins")]
        public int NumCoins
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }
        [ParseFieldName("friends")]
        public IList<PublicUserData> Friends
        {
            get { return GetProperty<IList<PublicUserData>>(); }
            set { SetProperty<IList<PublicUserData>>(value); }
        }
        [ParseFieldName("tutors")]
        public IList<PublicUserData> Tutors
        {
            get { return GetProperty<IList<PublicUserData>>(); }
            set { SetProperty<IList<PublicUserData>>(value); }
        }
        [ParseFieldName("blocked")]
        public IList<ParseUser> Blocked
        {
            get { return GetProperty<IList<ParseUser>>(); }
            set { SetProperty<IList<ParseUser>>(value); }
        }
        [ParseFieldName("recentChallenges")]
        public IList<ParseObject> RecentChallenges
        {
            get { return GetProperty<IList<ParseObject>>(); }
            set { SetProperty<IList<ParseObject>>(value); }
        }
        [ParseFieldName("requestsFromTutors")]
        public IList<Tutor> RequestsFromTutors
        {
            get { return GetProperty<IList<Tutor>>(); }
            set { SetProperty<IList<Tutor>>(value); }
        }
        [ParseFieldName("baseUserId")]
        public string BaseUserId
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
        [ParseFieldName("responses")]
        public ParseRelation<ParseObject> Responses
        {
            get { return GetProperty<ParseRelation<ParseObject>>(); }
            set { SetProperty<ParseRelation<ParseObject>>(value); }
        }
        [ParseFieldName("assignedQuestions")]
        public IList<Question> AssignedQuestions
        {
            get { return GetProperty<IList<Question>>(); }
            set { SetProperty<IList<Question>>(value); }
        }
    }
}