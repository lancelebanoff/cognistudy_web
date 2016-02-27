using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CogniTutor
{
    [ParseClassName("Student")]
    public class Student : ParseObject
    {
        [ParseFieldName("achievements")]
        public List<ParseObject> Achievements
        {
            get { return GetProperty<List<ParseObject>>(); }
            set { SetProperty<List<ParseObject>>(value); }
        }
        [ParseFieldName("shopItemsBought")]
        public List<string> ShopItemsBought
        {
            get { return GetProperty<List<string>>(); }
            set { SetProperty<List<string>>(value); }
        }
        [ParseFieldName("skinSelections")]
        public List<ParseObject> SkinSelections
        {
            get { return GetProperty<List<ParseObject>>(); }
            set { SetProperty<List<ParseObject>>(value); }
        }
        [ParseFieldName("randomEnabled")]
        public bool RandomEnabled
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }
        [ParseFieldName("publicAnalytics")]
        public bool PublicAnalytics
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }
        [ParseFieldName("baseUserId")]
        public string BaseUserId
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
        [ParseFieldName("studentSubjectRollingStats")]
        public List<StudentSubjectRollingStats> StudentSubjectRollingStats
        {
            get { return GetProperty<List<StudentSubjectRollingStats>>(); }
            set { SetProperty<List<StudentSubjectRollingStats>>(value); }
        }
        [ParseFieldName("studentCategoryRollingStats")]
        public List<StudentCategoryRollingStats> StudentCategoryRollingStats
        {
            get { return GetProperty<List<StudentCategoryRollingStats>>(); }
            set { SetProperty<List<StudentCategoryRollingStats>>(value); }
        }
        [ParseFieldName("studentTotalRollingStats")]
        public StudentTotalRollingStats StudentTotalRollingStats
        {
            get { return GetProperty<StudentTotalRollingStats>(); }
            set { SetProperty<StudentTotalRollingStats>(value); }
        }
        [ParseFieldName("studentCategoryDayStats")]
        public ParseRelation<ParseObject> StudentCategoryDayStats
        {
            get { return GetProperty<ParseRelation<ParseObject>>(); }
            set { SetProperty<ParseRelation<ParseObject>>(value); }
        }
        [ParseFieldName("studentCategoryTridayStats")]
        public ParseRelation<ParseObject> StudentCategoryTridayStats
        {
            get { return GetProperty<ParseRelation<ParseObject>>(); }
            set { SetProperty<ParseRelation<ParseObject>>(value); }
        }
        [ParseFieldName("studentCategoryMonthStats")]
        public ParseRelation<ParseObject> StudentCategoryMonthStats
        {
            get { return GetProperty<ParseRelation<ParseObject>>(); }
            set { SetProperty<ParseRelation<ParseObject>>(value); }
        }
        [ParseFieldName("studentCategoryStats")]
        public ParseRelation<ParseObject> StudentCategoryStats
        {
            get { return GetProperty<ParseRelation<ParseObject>>(); }
            set { SetProperty<ParseRelation<ParseObject>>(value); }
        }
        [ParseFieldName("studentSubjectDayStats")]
        public ParseRelation<ParseObject> StudentSubjectDayStats
        {
            get { return GetProperty<ParseRelation<ParseObject>>(); }
            set { SetProperty<ParseRelation<ParseObject>>(value); }
        }
        [ParseFieldName("studentSubjectTridayStats")]
        public ParseRelation<ParseObject> StudentSubjectTridayStats
        {
            get { return GetProperty<ParseRelation<ParseObject>>(); }
            set { SetProperty<ParseRelation<ParseObject>>(value); }
        }
        [ParseFieldName("studentSubjectMonthStats")]
        public ParseRelation<ParseObject> StudentSubjectMonthStats
        {
            get { return GetProperty<ParseRelation<ParseObject>>(); }
            set { SetProperty<ParseRelation<ParseObject>>(value); }
        }
        [ParseFieldName("studentSubjectStats")]
        public ParseRelation<ParseObject> StudentSubjectStats
        {
            get { return GetProperty<ParseRelation<ParseObject>>(); }
            set { SetProperty<ParseRelation<ParseObject>>(value); }
        }
        [ParseFieldName("privateStudentData")]
        public PrivateStudentData PrivateStudentData
        {
            get { return GetProperty<PrivateStudentData>(); }
            set { SetProperty<PrivateStudentData>(value); }
        }
    }
}