﻿using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CogniTutor
{
    [ParseClassName("PublicUserData")]
    public class PublicUserData : ParseObject
    {
        [ParseFieldName("facebookId")]
        public string FacebookId
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
        [ParseFieldName("profilePic")]
        public ParseFile ProfilePic
        {
            get { return GetProperty<ParseFile>(); }
            set { SetProperty<ParseFile>(value); }
        }
        [ParseFieldName("userType")]
        public string UserType
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
        [ParseFieldName("searchableDisplayName")]
        public string SearchableDisplayName
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
        [ParseFieldName("profilePicData")]
        public byte[] ProfilePicData
        {
            get { return GetProperty<byte[]>(); }
            set { SetProperty<byte[]>(value); }
        }
        [ParseFieldName("displayName")]
        public string DisplayName
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
        [ParseFieldName("lastSeen")]
        public DateTime LastSeen
        {
            get { return GetProperty<DateTime>(); }
            set { SetProperty<DateTime>(value); }
        }
        [ParseFieldName("fbLinked")]
        public bool FbLinked
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }
        [ParseFieldName("tutor")]
        public Tutor Tutor
        {
            get { return GetProperty<Tutor>(); }
            set { SetProperty<Tutor>(value); }
        }
        [ParseFieldName("baseUserId")]
        public string BaseUserId
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
        [ParseFieldName("student")]
        public Student Student
        {
            get { return GetProperty<Student>(); }
            set { SetProperty<Student>(value); }
        }

        public static async Task<PublicUserData> GetById(string objectId)
        {
            var query = new ParseQuery<PublicUserData>();
            return await query.GetAsync(objectId);
        }

        public static async Task<PublicUserData> GetAllTutorDataById(string objectId)
        {
            var query = new ParseQuery<PublicUserData>().Include("tutor.privateTutorData");
            return await query.GetAsync(objectId);
        }

        public static async Task<IEnumerable<PublicUserData>> Search(string searchText)
        {
            System.Diagnostics.Debug.WriteLine("search");
            var query = from data in new ParseQuery<PublicUserData>()
                        where data.SearchableDisplayName.StartsWith(searchText.ToLower())
                        select data;
            System.Diagnostics.Debug.WriteLine("searchabout done");
            return await query.FindAsync();
        }

        public static async Task<IEnumerable<PublicUserData>> AllTutors()
        {
            string[] array = new string[] {Constants.UserType.TUTOR, Constants.UserType.MODERATOR};
            var query = from tutor in new ParseQuery<PublicUserData>().Include("tutor.privateTutorData")
                        where array.Contains(tutor.UserType)
                        select tutor;
            return await query.FindAsync();
        }
    }
}