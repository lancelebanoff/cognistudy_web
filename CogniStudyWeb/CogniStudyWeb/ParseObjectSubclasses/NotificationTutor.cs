using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CogniTutor
{
    [ParseClassName("NotificationTutor")]
    public class NotificationTutor : ParseObject
    {
        [ParseFieldName("tutorFor")]
        public PublicUserData TutorFor
        {
            get { return GetProperty<PublicUserData>(); }
            set { SetProperty<PublicUserData>(value); }
        }
        [ParseFieldName("userFrom")]
        public PublicUserData UserFrom
        {
            get { return GetProperty<PublicUserData>(); }
            set { SetProperty<PublicUserData>(value); }
        }
        [ParseFieldName("type")]
        public string Type
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
        [ParseFieldName("firstSeenAt")]
        public DateTime? FirstSeenAt
        {
            get { return GetProperty<DateTime?>(); }
            set { SetProperty<DateTime?>(value); }
        }

        public string RedirectLink
        {
            get
            {
                if (Type == Constants.NotificationType.REQUEST_FROM_STUDENT || Type == Constants.NotificationType.ACCEPT_FROM_STUDENT)
                    return "StudentProfile?StudentId=" + UserFrom.ObjectId;
                else if (Type == Constants.NotificationType.MESSAGE)
                    return "Messages";
                else
                    return "";
            }
        }

        public string NotificationMessage
        {
            get
            {
                if (Type == Constants.NotificationType.REQUEST_FROM_STUDENT)
                    return UserFrom.DisplayName + " sent you a request to be their tutor.";
                else if (Type == Constants.NotificationType.ACCEPT_FROM_STUDENT)
                    return UserFrom.DisplayName + " accepted your request to be their tutor.";
                else if (Type == Constants.NotificationType.MESSAGE)
                    return UserFrom.DisplayName + " sent you a message.";
                else
                    return "";
            }
        }
        
    }
}