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
        [ParseFieldName("blocked")]
        public IList<ParseUser> Blocked
        {
            get { return GetProperty<IList<ParseUser>>(); }
            set { SetProperty<IList<ParseUser>>(value); }
        }

        public async Task AcceptStudentRequest(PublicUserData StudentPublicData, PublicUserData TutorPublicData)
        {
            IList<PublicUserData> requests = (await RequestsFromStudents.FetchAllIfNeededAsync()).ToList();
            requests.Remove(StudentPublicData);
            RequestsFromStudents = requests;

            IList<PublicUserData> students = Students;
            students.Add(StudentPublicData);
            Students = students;

            Student student = await StudentPublicData.Student.FetchIfNeededAsync();
            PrivateStudentData privateStudentData = student.PrivateStudentData;
            IDictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "privateStudentDataId", privateStudentData.ObjectId },
                { "tutorPublicDataId", TutorPublicData.ObjectId }
            };
            await ParseCloud.CallFunctionAsync<string>("addTutor", parameters);

            //this.AddUniqueToList("students", StudentPublicData);
            await this.SaveAsync();
        }

        public async Task SendRequestToStudent(PublicUserData StudentPublicData, PublicUserData TutorPublicData)
        {
            //Student student = await StudentPublicData.Student.FetchIfNeededAsync();
            //PrivateStudentData privateStudentData = student.PrivateStudentData;
            IDictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "publicStudentDataId", StudentPublicData.ObjectId },
                { "publicTutorDataId", TutorPublicData.ObjectId }
            };
            await ParseCloud.CallFunctionAsync<string>("tutorRequestToStudent", parameters);
        }

        public async Task BlockStudent(PublicUserData StudentPublicData)
        {
            IList<ParseUser> blocked = this.Blocked;
            blocked.Add(ParseUser.CreateWithoutData<ParseUser>(StudentPublicData.BaseUserId));
            this.Blocked = blocked;
            await this.SaveAsync();
        }

        public async Task RemoveStudent(PublicUserData StudentPublicData, PublicUserData TutorPublicData)
        {
            Students = Common.RemoveFromList(Students, StudentPublicData);
            await this.SaveAsync();

            Student student = await StudentPublicData.Student.FetchIfNeededAsync();
            PrivateStudentData privateStudentData = student.PrivateStudentData;
            IDictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "privateStudentDataId", privateStudentData.ObjectId },
                { "tutorPublicDataId", TutorPublicData.ObjectId }
            };
            await ParseCloud.CallFunctionAsync<string>("removeTutor", parameters);
        }
        
    }
}