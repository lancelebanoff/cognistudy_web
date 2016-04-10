using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CogniTutor
{
    [ParseClassName("SuggestedQuestion")]
    public class SuggestedQuestion : ParseObject
    {
        [ParseFieldName("response")]
        public ParseObject Response
        {
            get { return GetProperty<ParseObject>(); }
            set { SetProperty<ParseObject>(value); }
        }
        [ParseFieldName("question")]
        public Question Question
        {
            get { return GetProperty<Question>(); }
            set { SetProperty<Question>(value); }
        }
        [ParseFieldName("studentBaseUserId")]
        public string StudentBaseUserId
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
        [ParseFieldName("tutor")]
        public PublicUserData Tutor
        {
            get { return GetProperty<PublicUserData>(); }
            set { SetProperty<PublicUserData>(value); }
        }
        [ParseFieldName("answered")]
        public bool Answered
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }

        public static async Task<IList<SuggestedQuestion>> GetAssignedQuestionsBetweenStudentTutor(PublicUserData studentPublicData, PublicUserData tutorPublicData)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "studentPublicDataId", studentPublicData.ObjectId },
                { "tutorPublicDataId", tutorPublicData.ObjectId }
            };
            IList<SuggestedQuestion> assignedQuestions = await ParseCloud.CallFunctionAsync<IList<SuggestedQuestion>>("assignedQuestionsToStudent", parameters);
            return assignedQuestions;
        }
    }
}