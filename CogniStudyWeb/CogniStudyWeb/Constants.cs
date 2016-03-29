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
            public static readonly String SOCIAL_STUDIES_SCIENCE = "Social Studies/Science";
            public static readonly String ARTS_LITERATURE = "Arts/Literature";
            public static readonly String PRE_ALGEBRA = "Pre-Algebra";
            //public static readonly String ELEMENTARY_ALGEBRA = "Elementary Algebra";
            //public static readonly String INTERMEDIATE_ALGEBRA = "Intermediate Algebra";
            public static readonly String GEOMETRY = "Geometry";
            public static readonly String TRIGONOMETRY = "Trigonometry";
            //public static readonly String DATA_ANALYSIS = "Data Analysis";
            public static readonly String USAGE_AND_MECHANICS = "Usage and Mechanics";
            public static readonly String RHETORICAL_SKILLS = "Rhetorical Skills";
            public static readonly String DATA_REPRESENTATION = "Data Representation";
            public static readonly String RESEARCH_SUMMARIES = "Research Summaries";
            public static readonly String CONFLICTING_VIEWPOINTS = "Conflicting Viewpoints";
        }

        public static class Test {
            public static readonly String SAT = "SAT";
            public static readonly String ACT = "ACT";
            public static readonly String BOTH = "Both";

            public static String[] getTests() {
                return new String[] { SAT, ACT, BOTH} ;
            }
        }

        public static readonly Dictionary<String, String[]> SubjectToCategory;
        static Constants()
        {
            Dictionary<String, String[]> map = new Dictionary<String, String[]>();
            map[""] = new String[] { "" };
            map[Subject.READING] = new String[]{Category.SOCIAL_STUDIES_SCIENCE, Category.ARTS_LITERATURE};
            //map[Subject.MATH] = new String[] { Category.PRE_ALGEBRA, Category.ELEMENTARY_ALGEBRA, Category.GEOMETRY, Category.TRIGONOMETRY, Category.INTERMEDIATE_ALGEBRA, Category.DATA_ANALYSIS };
            map[Subject.MATH] = new String[] { Category.PRE_ALGEBRA, Category.GEOMETRY, Category.TRIGONOMETRY };
            map[Subject.ENGLISH] = new String[]{Category.USAGE_AND_MECHANICS, Category.RHETORICAL_SKILLS};
            map[Subject.SCIENCE] = new String[]{Category.DATA_REPRESENTATION, Category.RESEARCH_SUMMARIES, Category.CONFLICTING_VIEWPOINTS};
            SubjectToCategory = map;
        }
        
        public static class UserType {
            public static readonly String STUDENT = "STUDENT";
            public static readonly String TUTOR = "TUTOR";
            public static readonly String MODERATOR = "MODERATOR";
            public static readonly String ADMIN = "ADMIN";
            public static bool IsQualifiedToReviewQuestions(string myUserType) {
                return myUserType == MODERATOR || myUserType == ADMIN;
            }
            public static bool IsTutor(string myUserType)
            {
                return myUserType == TUTOR || myUserType == MODERATOR || myUserType == ADMIN;
            }
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