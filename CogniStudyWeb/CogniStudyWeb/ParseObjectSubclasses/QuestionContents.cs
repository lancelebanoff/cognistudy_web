using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CogniTutor
{
    [ParseClassName("QuestionContents")]
    public class QuestionContents : ParseObject
    {
        [ParseFieldName("bundle")]
        public QuestionBundle Bundle
        {
            get { return GetProperty<QuestionBundle>(); }
            set { SetProperty<QuestionBundle>(value); }
        }
        [ParseFieldName("questionText")]
        public string QuestionText
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
        [ParseFieldName("image")]
        public ParseFile Image
        {
            get { return GetProperty<ParseFile>(); }
            set { SetProperty<ParseFile>(value); }
        }
        [ParseFieldName("author")]
        public PublicUserData Author
        {
            get { return GetProperty<PublicUserData>(); }
            set { SetProperty<PublicUserData>(value); }
        }
        [ParseFieldName("answers")]
        public List<string> Answers
        {
            get { return GetProperty<List<string>>(); }
            set { SetProperty<List<string>>(value); }
        }
        [ParseFieldName("correctAnswer")]
        public int CorrectAnswer
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }
        [ParseFieldName("explanation")]
        public string Explanation
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
    }
}