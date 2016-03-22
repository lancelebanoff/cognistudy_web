using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using System.IO;
using Parse;

namespace CogniTutor
{
    public partial class Profile : CogniPage
    {
        public PublicUserData theirPublicUserData { get { return (PublicUserData)Session["theirPublicUserData"]; } set { Session["theirPublicUserData"] = value; } }
        public Tutor theirTutor { get { return (Tutor)Session["theirTutor"]; } set { Session["theirTutor"] = value; } }
        public PrivateTutorData theirPrivateTutorData { get { return (PrivateTutorData)Session["theirPrivateTutorData"]; } set { Session["theirPrivateTutorData"] = value; } }
        public bool IsMyProfile { get { return theirPublicUserData.BaseUserId == UserID; } }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override async Task OnStart()
        {
            string tutorId = Request.QueryString["tutorId"];
            theirPublicUserData = await PublicUserData.GetAllTutorDataById(tutorId);
            theirTutor = theirPublicUserData.Tutor;
            theirPrivateTutorData = theirTutor.PrivateTutorData;
            Image1.ImageUrl = PublicUserData.ProfilePic != null ? PublicUserData.ProfilePic.Url.ToString() : "Images/default_prof_pic.png";
        }

        protected void aboutMeSaveChangesBtn_Click(object sender, EventArgs e)
        {
            Tutor.Biography = tbEditAboutMe.Text;
            //RegisterAsyncTask(new PageAsyncTask(() => Tutor.SaveAsync()));
            //ExecuteRegisteredAsyncTasks();
            Task t = Tutor.SaveAsync();
            t.Wait();
            //UpdateUserColumn("About", tbEditAboutMe.Text);
            SetAboutMeVisibilities(sender, e);
        }

        protected void SetAboutMeVisibilities(object sender, EventArgs e)
        {
            bool setToEdit = divAboutMeStatic.Visible; //if static div is visible, we wish to set to edit mode

            //if (sender.Equals(EditAboutMeBtn) && !setToEdit)
            //    return;

            divAboutMeStatic.Visible = !setToEdit;
            divAboutMeDynamic.Visible = setToEdit;
            aboutMeCancelBtn.Visible = setToEdit;
            aboutMeSaveChangesBtn.Visible = setToEdit;

            if (setToEdit)
                tbEditAboutMe.Text = Tutor.Biography;
        }

        protected void Upload(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string uploadFileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileInfo Finfo = new FileInfo(FileUpload1.PostedFile.FileName);
                string extension = Finfo.Extension.ToLower();
                byte[] data = FileUpload1.FileBytes;
                ParseFile file = new ParseFile("profile_picture" + extension, data);
                Task t = file.SaveAsync();
                t.Wait();

                PublicUserData.ProfilePic = file;
                //RegisterAsyncTask(new PageAsyncTask(() => PublicUserData.SaveAsync()));
                Task t2 = PublicUserData.SaveAsync();
                t2.Wait();
                //string uploadFileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                //string downloadFileName = "user" + Request.QueryString["TutorID"].ToString();
                //Directory.CreateDirectory("~/Images/profile pictures/");
                //FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Images/profile pictures/") + downloadFileName + ".png");
                //Common.ExecuteCommand("update users set PictureFileName='" + downloadFileName
                //    + "' where UserID=" + Request.QueryString["TutorID"]);
                //ReloadPage();
            }
        }
    }
}