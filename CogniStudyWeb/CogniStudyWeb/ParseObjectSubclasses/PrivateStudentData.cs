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
        public List<PublicUserData> Friends
        {
            get { return GetProperty<List<PublicUserData>>(); }
            set { SetProperty<List<PublicUserData>>(value); }
        }
        [ParseFieldName("tutors")]
        public List<PublicUserData> Tutors
        {
            get { return GetProperty<List<PublicUserData>>(); }
            set { SetProperty<List<PublicUserData>>(value); }
        }
        [ParseFieldName("blocked")]
        public List<ParseUser> Blocked
        {
            get { return GetProperty<List<ParseUser>>(); }
            set { SetProperty<List<ParseUser>>(value); }
        }
        [ParseFieldName("recentChallenges")]
        public List<ParseObject> RecentChallenges
        {
            get { return GetProperty<List<ParseObject>>(); }
            set { SetProperty<List<ParseObject>>(value); }
        }
        [ParseFieldName("requestsFromTutors")]
        public List<Tutor> RequestsFromTutors
        {
            get { return GetProperty<List<Tutor>>(); }
            set { SetProperty<List<Tutor>>(value); }
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
    }
}