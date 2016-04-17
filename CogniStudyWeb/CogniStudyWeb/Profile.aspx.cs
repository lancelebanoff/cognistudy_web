using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using System.IO;
using Parse;
using CodeCarvings.Piczard;

namespace CogniTutor
{
    public partial class Profile : CogniPage
    {
        public PublicUserData theirPublicUserData { get { return (PublicUserData)Session["theirPublicUserData"]; } set { Session["theirPublicUserData"] = value; } }
        public Tutor theirTutor { get { return (Tutor)Session["theirTutor"]; } set { Session["theirTutor"] = value; } }
        public bool IsMyProfile { get { return theirPublicUserData.BaseUserId == UserID; } }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override async Task OnStart()
        {
            string tutorId = Request.QueryString["tutorId"];
            theirPublicUserData = await PublicUserData.GetPublicTutorDataById(tutorId);
            theirTutor = theirPublicUserData.Tutor;
            Image1.ImageUrl = theirPublicUserData.ProfilePic != null ? theirPublicUserData.ProfilePic.Url.ToString() : "Images/default_prof_pic.png";
            edit.Visible = IsMyProfile;
            EditAboutMeBtn.Visible = IsMyProfile;
            btnSendMessage.Visible = !IsMyProfile;
            //theirPublicUserData.ProfilePic = null;
            //await theirPublicUserData.SaveAsync();
        }

        protected void aboutMeSaveChangesBtn_Click(object sender, EventArgs e)
        {
            theirTutor.Biography = tbEditAboutMe.Text;
            AsyncHelpers.RunSync(theirTutor.SaveAsync);
            //Task t = Tutor.SaveAsync();
            //t.Wait();
            SetAboutMeVisibilities(sender, e);
        }

        protected void SetAboutMeVisibilities(object sender, EventArgs e)
        {
            bool setToEdit = divAboutMeStatic.Visible; //if static div is visible, we wish to set to edit mode

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
                byte[] square;

                var sourceImage = CodeCarvings.Piczard.ImageArchiver.LoadImage(data);
                if (sourceImage.Size.Width == sourceImage.Size.Height)
                {
                    square = data;
                }
                else
                {
                    // Calculate the square size
                    int imageSize = sourceImage.Size.Width < sourceImage.Size.Height ? sourceImage.Size.Width : sourceImage.Size.Height;

                    // Get a fixed resize filter (square)
                    //var filter = new CodeCarvings.Piczard.FixedResizeConstraint(imageSize, imageSize);
                    var filter = new CodeCarvings.Piczard.FixedCropConstraint(imageSize, imageSize);
                    // Force white background
                    filter.CanvasColor = BackgroundColor.GetStatic(System.Drawing.Color.White);
                    square = filter.SaveProcessedImageToByteArray(sourceImage, new CodeCarvings.Piczard.JpegFormatEncoderParams(82));
                }

                ParseFile file = new ParseFile("profile_picture" + extension, square);
                Task t = file.SaveAsync();
                t.Wait();

                theirPublicUserData.ProfilePic = file;
                Task t2 = theirPublicUserData.SaveAsync();
                t2.Wait();

                Image1.ImageUrl = theirPublicUserData.ProfilePic != null ? theirPublicUserData.ProfilePic.Url.ToString() : "Images/default_prof_pic.png";
            }
        }

        protected void btnSendMessage_Click(object sender, EventArgs e)
        {
            Session["ConversationUserId"] = theirPublicUserData.ObjectId;
            Response.Redirect("Messages");
        }
    }
}