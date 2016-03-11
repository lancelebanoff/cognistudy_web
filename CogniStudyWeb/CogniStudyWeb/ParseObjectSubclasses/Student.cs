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
        public IList<ParseObject> Achievements
        {
            get { return GetProperty<IList<ParseObject>>(); }
            set { SetProperty<IList<ParseObject>>(value); }
        }
        [ParseFieldName("shopItemsBought")]
        public IList<string> ShopItemsBought
        {
            get { return GetProperty<IList<string>>(); }
            set { SetProperty<IList<string>>(value); }
        }
        [ParseFieldName("skinSelections")]
        public IList<ParseObject> SkinSelections
        {
            get { return GetProperty<IList<ParseObject>>(); }
            set { SetProperty<IList<ParseObject>>(value); }
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
        public IList<StudentSubjectRollingStats> StudentSubjectRollingStats
        {
            get { return GetProperty<IList<StudentSubjectRollingStats>>(); }
            set { SetProperty<IList<StudentSubjectRollingStats>>(value); }
        }
        [ParseFieldName("studentCategoryRollingStats")]
        public IList<StudentCategoryRollingStats> StudentCategoryRollingStats
        {
            get { return GetProperty<IList<StudentCategoryRollingStats>>(); }
            set { SetProperty<IList<StudentCategoryRollingStats>>(value); }
        }
        [ParseFieldName("studentTotalRollingStats")]
        public StudentTotalRollingStats StudentTotalRollingStats
        {
            get { return GetProperty<StudentTotalRollingStats>(); }
            set { SetProperty<StudentTotalRollingStats>(value); }
        }
        [ParseFieldName("studentCategoryDayStats")]
        public ParseRelation<StudentCategoryDayStats> StudentCategoryDayStats
        {
            get { return GetProperty<ParseRelation<StudentCategoryDayStats>>(); }
            set { SetProperty<ParseRelation<StudentCategoryDayStats>>(value); }
        }
        [ParseFieldName("studentCategoryTridayStats")]
        public ParseRelation<StudentCategoryTridayStats> StudentCategoryTridayStats
        {
            get { return GetProperty<ParseRelation<StudentCategoryTridayStats>>(); }
            set { SetProperty<ParseRelation<StudentCategoryTridayStats>>(value); }
        }
        [ParseFieldName("studentCategoryMonthStats")]
        public ParseRelation<StudentCategoryMonthStats> StudentCategoryMonthStats
        {
            get { return GetProperty<ParseRelation<StudentCategoryMonthStats>>(); }
            set { SetProperty<ParseRelation<StudentCategoryMonthStats>>(value); }
        }
        [ParseFieldName("studentCategoryStats")]
        public ParseRelation<StudentCategoryStats> StudentCategoryStats
        {
            get { return GetProperty<ParseRelation<StudentCategoryStats>>(); }
            set { SetProperty<ParseRelation<StudentCategoryStats>>(value); }
        }
        [ParseFieldName("studentSubjectDayStats")]
        public ParseRelation<StudentSubjectDayStats> StudentSubjectDayStats
        {
            get { return GetProperty<ParseRelation<StudentSubjectDayStats>>(); }
            set { SetProperty<ParseRelation<StudentSubjectDayStats>>(value); }
        }
        [ParseFieldName("studentSubjectTridayStats")]
        public ParseRelation<StudentSubjectTridayStats> StudentSubjectTridayStats
        {
            get { return GetProperty<ParseRelation<StudentSubjectTridayStats>>(); }
            set { SetProperty<ParseRelation<StudentSubjectTridayStats>>(value); }
        }
        [ParseFieldName("studentSubjectMonthStats")]
        public ParseRelation<StudentSubjectMonthStats> StudentSubjectMonthStats
        {
            get { return GetProperty<ParseRelation<StudentSubjectMonthStats>>(); }
            set { SetProperty<ParseRelation<StudentSubjectMonthStats>>(value); }
        }
        [ParseFieldName("studentTotalDayStats")]
        public ParseRelation<StudentTotalDayStats> StudentTotalDayStats
        {
            get { return GetProperty<ParseRelation<StudentTotalDayStats>>(); }
            set { SetProperty<ParseRelation<StudentTotalDayStats>>(value); }
        }
        [ParseFieldName("studentTotalTridayStats")]
        public ParseRelation<StudentTotalTridayStats> StudentTotalTridayStats
        {
            get { return GetProperty<ParseRelation<StudentTotalTridayStats>>(); }
            set { SetProperty<ParseRelation<StudentTotalTridayStats>>(value); }
        }
        [ParseFieldName("studentTotalMonthStats")]
        public ParseRelation<StudentTotalMonthStats> StudentTotalMonthStats
        {
            get { return GetProperty<ParseRelation<StudentTotalMonthStats>>(); }
            set { SetProperty<ParseRelation<StudentTotalMonthStats>>(value); }
        }
        [ParseFieldName("studentSubjectStats")]
        public ParseRelation<StudentSubjectStats> StudentSubjectStats
        {
            get { return GetProperty<ParseRelation<StudentSubjectStats>>(); }
            set { SetProperty<ParseRelation<StudentSubjectStats>>(value); }
        }
        [ParseFieldName("privateStudentData")]
        public PrivateStudentData PrivateStudentData
        {
            get { return GetProperty<PrivateStudentData>(); }
            set { SetProperty<PrivateStudentData>(value); }
        }
    }
}