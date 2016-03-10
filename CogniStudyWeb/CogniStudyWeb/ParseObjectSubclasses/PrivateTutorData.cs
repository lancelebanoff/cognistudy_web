using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CogniTutor
{
    [ParseClassName("PrivateTutorData")]
    public class PrivateTutorData : ParseObject
    {
        [ParseFieldName("questionsCreated")]
        public ParseRelation<Question> QuestionsCreated
        {
            get { return GetProperty<ParseRelation<Question>>(); }
            set { SetProperty<ParseRelation<Question>>(value); }
        }
        [ParseFieldName("baseUserId")]
        public string BaseUserId
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
        [ParseFieldName("students")]
        public IList<PublicUserData> Students
        {
            get { return GetProperty<IList<PublicUserData>>(); }
            set { SetProperty<IList<PublicUserData>>(value); }
        }
        [ParseFieldName("requestsFromStudents")]
        public IList<PublicUserData> RequestsFromStudents
        {
            get { return GetProperty<IList<PublicUserData>>(); }
            set { SetProperty<IList<PublicUserData>>(value); }
        }

        public async Task AddStudent(PublicUserData StudentPublicData)
        {
            this.AddUniqueToList("students", StudentPublicData);
            await this.SaveAsync();
        }
    }
}