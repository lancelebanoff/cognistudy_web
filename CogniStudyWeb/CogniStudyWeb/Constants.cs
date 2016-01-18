using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CogniTutor
{
    public class Constants
    {
        public static class Database
        {
            public static string TRUE = "True";
            public static string FALSE = "False";
        }

        public static class PurchaseType
        {
            public static string LARGE_COURSE = "large_course";
            public static string SMALL_COURSE = "small_course";
            public static string LESSON = "lesson";
            public static string VA_PAYMENT = "va_payment";
            public static string MONTH_MEMBERSHIP = "month_membership";
            public static string SINGLE_SESSION = "single_session";
            public static string QUICK_STUDY_PACKAGE = "quick_study_package";
        }

        public static class Cost
        {
            //Not sure what these would be, or if they will be used
            //public static string LARGE_COURSE_COST = "";
            //public static string SMALL_COURSE_COST = "";
            //public static string LESSON_COST = "";
            //public static string VA_PAYMENT_COST = "";
            public static string MONTH_MEMBERSHIP_FIRST_COST = "100";
            public static string SECOND_MONTH_DISCOUNT = "10";
            public static string MONTH_MEMBERSHIP_SECOND_COST = (Convert.ToDouble(MONTH_MEMBERSHIP_FIRST_COST) - Convert.ToDouble(SECOND_MONTH_DISCOUNT)).ToString();
            public static string SINGLE_SESSION_COST = "30";
            public static string QUICK_STUDY_PACKAGE_COST = "10";
        }

        public static class ProfileAttributeType
        {
            public static string EDUCATION = "1";
            public static string CERTIFICATION = "2";
            public static string EXPERIENCE = "3";
            public static string AWARD_AFFILIATION = "4";
        }

        public static class SmallGroupStudentSubject
        {
            public static string ENGLISH_WRITING_READING = "1";
            public static string MATH_SCIENCE = "2";
            public static string ALL_SUBJECTS = "3";
            public static String[] SUBJECTS_ARRAY = { "None Selected", "English, Writing and Reading", "Math and Science", "All Subjects" };
        }

        public static class SmallGroupStudentGroupSize
        {
            public static string SMALL = "1";
            public static string MEDIUM = "2";
        }

        public static class CalendarType
        {
            public static string MONTH_MEMBERSHIP = "month_membership";
            public static string SINGLE_SESSION = "single_session";
        }

        public static class Lesson
        {
            public static class Cancelled
            {
                public static string NOT_CANCELLED = "not_cancelled";
                public static string PENDING_APPROVAL = "pending_approval";
                public static string APPROVED = "approved";
                public static string DENIED = "denied";
            }
            public static class Location
            {
                public static string EMPTY = "empty";
                public static string TUTORS_CHOICE = "tutors_choice";
                public static string PUBLIC_LOCATION = "public_location";
                public static string MY_HOME = "my_home";
                public static string ONLINE = "online";
            }
            public static class StudentRelation
            {
                public static string EMPTY = "empty";
                public static string MYSELF = "myself";
                public static string SOMEONE_ELSE = "someone_else";
            }
            public static class Gender
            {
                public static string EMPTY = "empty";
                public static string MALE = "male";
                public static string FEMALE = "female";
            }
        }

        public static class Page
        {
            public static string CHECKOUT = "~/Checkout.aspx";
            public static string RECEIPT = "~/Receipt.aspx";
            public static string ERROR = "~/Error.aspx";
        }
    }
}