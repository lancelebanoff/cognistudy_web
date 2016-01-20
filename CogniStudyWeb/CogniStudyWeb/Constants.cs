using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CogniTutor
{
    public class Constants
    {
        public static List<string> GetPublicStringProperties(Type type)
        {
            List<String> list = new List<string>();
            var fields = type.GetFields();
            foreach (FieldInfo f in fields)
            {
                list.Add((string)f.GetValue(null));
            }
            return list;
        }

        public static class Subject {
            public static readonly String READING = "Reading";
            public static readonly String MATH = "Math";
            public static readonly String ENGLISH = "English";
            public static readonly String SCIENCE = "Science";
        }

        public static class Category {
            public static readonly String PASSAGE_READING = "Passage Reading";
            public static readonly String SENTENCE_COMPLETION = "Sentence Completion";
            public static readonly String PRE_ALGEBRA = "Pre-Algebra";
            public static readonly String ALGEBRA = "Algebra";
            public static readonly String GEOMETRY = "Geometry";
            public static readonly String TRIGONOMETRY = "Trigonometry";
            public static readonly String DATA_ANALYSIS_STATISTICS_PROBABILITY = "Data Analysis, Statistics, Probability";
            public static readonly String IDENTIFYING_SENTENCE_ERRORS = "Identifying Sentence Errors";
            public static readonly String IMPROVING_SENTENCES = "Improving Sentences";
            public static readonly String IMPROVING_PARAGRAPHS = "Improving Paragraphs";
            public static readonly String DATA_REPRESENTATION = "Data Representation";
            public static readonly String RESEARCH_SUMMARIES = "Research Summaries";
            public static readonly String CONFLICTING_VIEWPOINTS = "Conflicting Viewpoints";
        }

        public static readonly Dictionary<String, String[]> SubjectToCategory;
        static Constants() {
            Dictionary<String, String[]> map = new Dictionary<String, String[]>();
            map[""] = new String[] { "" };
            map[Subject.READING] = new String[]{Category.PASSAGE_READING, Category.SENTENCE_COMPLETION};
            map[Subject.MATH] = new String[] { Category.PRE_ALGEBRA, Category.ALGEBRA, Category.GEOMETRY, Category.TRIGONOMETRY, Category.DATA_ANALYSIS_STATISTICS_PROBABILITY };
            map[Subject.ENGLISH] = new String[] { Category.IDENTIFYING_SENTENCE_ERRORS, Category.IMPROVING_SENTENCES, Category.IMPROVING_PARAGRAPHS };
            map[Subject.SCIENCE] = new String[] { Category.DATA_REPRESENTATION, Category.RESEARCH_SUMMARIES, Category.CONFLICTING_VIEWPOINTS };
            SubjectToCategory = map;
        }
        
        public static class UserType {
            public static readonly String STUDENT = "STUDENT";
            public static readonly String TUTOR = "TUTOR";
            public static readonly String ADMIN = "ADMIN";
        }

        public static class ReviewStatusType
        {
            public static readonly String PENDING = "PENDING";
            public static readonly String TUTOR_APPROVED = "TUTOR_APPROVED";
            public static readonly String APPROVED = "APPROVED";
            public static readonly String DENIED = "DENIED";
        }
    }
}