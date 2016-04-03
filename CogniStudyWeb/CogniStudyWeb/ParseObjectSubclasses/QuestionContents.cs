using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CogniTutor
{
    [ParseClassName("QuestionContents")]
    [Serializable]
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
        public IList<string> Answers
        {
            get { return GetProperty<IList<string>>(); }
            set { SetProperty<IList<string>>(value); }
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

        public string Answer1 { get { return Answers[0]; } }
        public string Answer2 { get { return Answers[1]; } }
        public string Answer3 { get { return Answers[2]; } }
        public string Answer4 { get { return Answers[3]; } }
        public string Answer5 { get { return Answers.Count >= 5 ? Answers[4] : ""; } }
        public string ImageUrl { get { return Image == null ? "" : Image.Url.ToString(); } set { } }
    }
}