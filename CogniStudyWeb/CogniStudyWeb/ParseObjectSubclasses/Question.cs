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

        public static List<Question> QueryQuestions(string subject, string category)
        {
            Task<IEnumerable<Question>> t;
            if (subject == null)
            {
                var query = from ques in new ParseQuery<Question>().Include("questionContents").Include("questionData").Include("bundle")
                            where ques.Get<bool>("isActive")
                            select ques;
                t = query.FindAsync();
            }
            else if (category == null)
            {
                var query = from ques in new ParseQuery<Question>().Include("questionContents").Include("questionData").Include("bundle")
                            where ques.Get<bool>("isActive")
                            where ques.Subject == subject
                            select ques;
                t = query.FindAsync();
            }
            else
            {
                var query = from ques in new ParseQuery<Question>().Include("questionContents").Include("questionData").Include("bundle")
                            where ques.Get<bool>("isActive")
                            where ques.Subject == subject
                            where ques.Category == category
                            select ques;
                t = query.FindAsync();
            }
            t.Wait();
            return t.Result.ToList();
        }

        public static async Task<Question[]> ChooseTenRandomQuestions()
        {
            //IDictionary<string, object> parameters = new Dictionary<string, object>
            //{
            //    { "categories", Constants.GetPublicStringProperties(typeof(Constants.Category)) }
            //    //{ "answeredQuestionIds", new string[] {} },
            //    //{ "skipBundles", true}
            //};
            //IList<ParseObject> questions = await ParseCloud.CallFunctionAsync<IList<ParseObject>>("chooseTenQuestions", parameters);
            //return questions.ToArray();

            var questionQuery = from question in new ParseQuery<Question>()
                                where question.Get<bool>("isActive")
                                where !question.Get<bool>("inBundle")
                                select question;
            questionQuery = questionQuery.Limit(10);
            IEnumerable<Question> questions = await questionQuery.FindAsync();
            return questions.ToArray();
        }
    }
}