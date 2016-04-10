using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CogniTutor
{
    [ParseClassName("Question")]
    [Serializable]
    public class Question : ParseObject
    {
        [ParseFieldName("subject")]
        public string Subject
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
        [ParseFieldName("category")]
        public string Category
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
        [ParseFieldName("bundle")]
        public QuestionBundle Bundle
        {
            get { return GetProperty<QuestionBundle>(); }
            set { SetProperty<QuestionBundle>(value); }
        }
        [ParseFieldName("inBundle")]
        public bool InBundle
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }
        [ParseFieldName("isActive")]
        public bool IsActive
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }
        [ParseFieldName("questionData")]
        public QuestionData QuestionData
        {
            get { return GetProperty<QuestionData>(); }
            set { SetProperty<QuestionData>(value); }
        }
        [ParseFieldName("questionContents")]
        public QuestionContents QuestionContents
        {
            get { return GetProperty<QuestionContents>(); }
            set { SetProperty<QuestionContents>(value); }
        }
        [ParseFieldName("test")]
        public bool Test
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }

        public static List<Question> QueryQuestions(string subject, string category)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "subject", subject ?? "" },
                { "category", category ?? "" }
            };
            Task<IList<ParseObject>> t = ParseCloud.CallFunctionAsync<IList<ParseObject>>("queryAllQuestions", parameters);
            t.Wait();
            List<Question> questions = new List<Question>();
            foreach (ParseObject o in t.Result)
            {
                questions.Add((Question)o);
            }
            return questions;
        }

        public static async Task<Question[]> ChooseTenRandomQuestions()
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>
            {
            };
            IList<Question> questions = await ParseCloud.CallFunctionAsync<IList<Question>>("chooseTenQuestions", parameters);
            return questions.ToArray();
        }

        public static async Task<Question> GetFullQuestionById(string questionId)
        {
            ParseQuery<Question> query = new ParseQuery<Question>().Include("questionData").Include("questionContents").Include("bundle");
            return await query.GetAsync(questionId);
        }
    }
}