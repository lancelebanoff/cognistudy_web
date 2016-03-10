using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;


using System.Data.SqlClient;
using System.Collections.Generic;
using System.Diagnostics;
using Parse;
using System.Threading.Tasks;

namespace CogniTutor
{
    public abstract partial class CogniPage : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            //if (!StartPage())
            //{
            //    //Response.Redirect("Error.aspx");
            //}
            base.OnInit(e);
        }
        protected override void OnLoad(EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(StartPage));
            //AsyncHelpers.RunSync(StartPage);
            base.OnLoad(e);
        }
        protected async Task StartPage()
        {
            RegisterParseSubclasses();
            ParseClient.Initialize("iT8NyJO0dChjLyfVsHUTM8UZQLSBBJLxd43AX9IY", "SvmmmluPjmLblmNrgqnUmylInkyiXzoWBk9ZxeZH");
            if (IsTestMode)
            {
                Session["Email"] = "loganlebanoff@yahoo.com";
                Session["Password"] = "poi";
            }
            if (LoggedIn)
            {
                await ParseUser.LogInAsync(Session["Email"].ToString(), Session["Password"].ToString());
                PublicUserData = ParseUser.CurrentUser.Get<PublicUserData>("publicUserData");
                await PublicUserData.FetchAsync();
                Tutor = PublicUserData.Tutor;
                await Tutor.FetchAsync();
                PrivateTutorData = Tutor.PrivateTutorData;
                await PrivateTutorData.FetchAsync();
            }
            await OnStart();
        }

        private void RegisterParseSubclasses()
        {
            ParseObject.RegisterSubclass<PrivateStudentData>();
            ParseObject.RegisterSubclass<PrivateTutorData>();
            ParseObject.RegisterSubclass<PublicUserData>();
            ParseObject.RegisterSubclass<Question>();
            ParseObject.RegisterSubclass<QuestionBundle>();
            ParseObject.RegisterSubclass<QuestionContents>();
            ParseObject.RegisterSubclass<QuestionData>();
            ParseObject.RegisterSubclass<Student>();
            ParseObject.RegisterSubclass<Tutor>();

            ParseObject.RegisterSubclass<StudentCategoryDayStats>();
            ParseObject.RegisterSubclass<StudentCategoryMonthStats>();
            ParseObject.RegisterSubclass<StudentCategoryRollingStats>();
            ParseObject.RegisterSubclass<StudentCategoryStats>();
            ParseObject.RegisterSubclass<StudentCategoryTridayStats>();
            ParseObject.RegisterSubclass<StudentSubjectDayStats>();
            ParseObject.RegisterSubclass<StudentSubjectMonthStats>();
            ParseObject.RegisterSubclass<StudentSubjectRollingStats>();
            ParseObject.RegisterSubclass<StudentSubjectStats>();
            ParseObject.RegisterSubclass<StudentSubjectTridayStats>();
            ParseObject.RegisterSubclass<StudentTotalRollingStats>();
            ParseObject.RegisterSubclass<StudentTotalDayStats>();
            ParseObject.RegisterSubclass<StudentTotalTridayStats>();
            ParseObject.RegisterSubclass<StudentTotalMonthStats>();
        }
        protected abstract Task OnStart();
        protected override void OnUnload(EventArgs e)
        {
            EndPage();

            base.OnUnload(e);
        }
        //protected void Page_Error(object sender, EventArgs e)
        //{
        //    //ErrorToUserLog();
        //}

        protected bool isExpired()
        {
            return false;
        }

        protected void EndPage()
        {
        }

        private void ErrorToUserLog()
        {
            try
            {
                Exception exc = Server.GetLastError();
                string source = Request.QueryString["handler"];
                bool Error = true;
                string ErrorMessage = "";
                if (exc.InnerException != null)
                {
                    ErrorMessage += "Inner Exception Type: ";
                    ErrorMessage += exc.InnerException.GetType().ToString();
                    ErrorMessage += "\n\nInner Exception: ";
                    ErrorMessage += exc.InnerException.Message;
                    ErrorMessage += "\n\nInner Source: ";
                    ErrorMessage += exc.InnerException.Source;
                    if (exc.InnerException.StackTrace != null)
                    {
                        ErrorMessage += "\nInner Stack Trace: ";
                        ErrorMessage += exc.InnerException.StackTrace;
                    }
                }
                var frame = new StackTrace(exc, true).GetFrame(0);
                var sourceFile = frame.GetFileName();
                var lineNumber = frame.GetFileLineNumber();
                ErrorMessage += "\n\nSource File: " + sourceFile + "\nLine Number: " + lineNumber;
                ErrorMessage += "\n\nException Type: ";
                ErrorMessage += exc.GetType().ToString();
                ErrorMessage += "\n\nException: " + exc.Message;
                ErrorMessage += "\n\nSource: " + source;
                ErrorMessage += "\n\nStack Trace: ";
                if (exc.StackTrace != null)
                {
                    ErrorMessage += exc.StackTrace;
                }
                ErrorMessage += "\n\n\n" + exc.ToString();
                ParseObject error = new ParseObject("Error");
                error["errorMessage"] = ErrorMessage;
                if (LoggedIn)
                {
                    error["user"] = ParseUser.CurrentUser;
                }
                error.SaveAsync();
            }
            catch
            { }
        }
        public static string GetConfiguration(string conf)
        {
            System.Configuration.AppSettingsReader configurationAppSettings;
            configurationAppSettings = new System.Configuration.AppSettingsReader();
            return (configurationAppSettings.GetValue(conf, typeof(string)) as string);
        }
        public string UserID
        {
            get
            {
                return ParseUser.CurrentUser.ObjectId;
            }
        }
        public UserTypes UserType
        {
            get
            {
                string userType = PublicUserData.Get<string>("userType");
                return Common.ParseEnum<UserTypes>(userType);
            }
        }
        public PublicUserData PublicUserData
        {
            get
            {
                return (PublicUserData)Session["PublicUserData"];
            }
            set
            {
                Session["PublicUserData"] = value;
            }
        }
        public Tutor Tutor
        {
            get
            {
                return (Tutor)Session["Tutor"];
            }
            set
            {
                Session["Tutor"] = value;
            }
        }
        public PrivateTutorData PrivateTutorData
        {
            get
            {
                return (PrivateTutorData)Session["PrivateTutorData"];
            }
            set
            {
                Session["PrivateTutorData"] = value;
            }
        }
        public bool LoggedIn
        {
            get
            {
                return (Session["Email"] != null);
            }
        }
        public string Name
        {
            get
            {
                return PublicUserData.Get<string>("displayName");
            }
        }
        public static bool IsTestMode
        {
            get
            {
                return Convert.ToBoolean(GetConfiguration("IsTestMode"));
            }
        }
        protected string GetHttpPath()
        {
            return ConfigurationManager.AppSettings["HttpPath"];
        }
        public void Redirect(string link)
        {
            EndPage();
            this.Response.Redirect(link);
        }
        public enum UserTypes
        {
            NotLoggedIn = 0,
            Student = 1,
            Tutor = 2,
            Moderator = 3,
            Admin = 4
        }

        public static IList<T> GetAllControlsRecusrvive<T>(Control control) where T : Control
        {
            var rtn = new List<T>();
            foreach (Control item in control.Controls)
            {
                var ctr = item as T;
                if (ctr != null)
                {
                    rtn.Add(ctr);
                }
                else
                {
                    rtn.AddRange(GetAllControlsRecusrvive<T>(item));
                }
            }
            return rtn;
        }
    }
}
