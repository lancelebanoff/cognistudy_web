using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CogniTutor
{
    [ParseClassName("QuestionBundle")]
    public class QuestionBundle : ParseObject
    {
        [ParseFieldName("passageText")]
        public string PassageText
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
        [ParseFieldName("questions")]
        public List<Question> Questions
        {
            get { return GetProperty<List<Question>>(); }
            set { SetProperty<List<Question>>(value); }
        }

        public string ImageUrl { get { return Image == null ? "" : Image.Url.ToString(); } }
    }
}