using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CogniTutor
{
    [ParseClassName("Question")]
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

        public static async Task<IEnumerable<Question>> QueryQuestions2(string subject, string category)
        {
            if (subject == null)
            {
                var query = from ques in new ParseQuery<Question>()
                            where ques.ObjectId != ""
                            select ques;
                return await query.FindAsync();
            }
            else if (category == null)
            {
                var query = from ques in new ParseQuery<Question>()
                            where ques.IsActive
                            where ques.Subject == subject
                            select ques;
                return await query.FindAsync();
            }
            else
            {
                var query = from ques in new ParseQuery<Question>()
                            where ques.IsActive
                            where ques.Subject == subject
                            where ques.Category == category
                            select ques;
                return await query.FindAsync();
            }
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
    }
}