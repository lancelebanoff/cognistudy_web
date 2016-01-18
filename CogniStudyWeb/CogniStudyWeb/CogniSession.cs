using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Data.SqlClient;
using System.Configuration;

namespace CogniTutor
{
    public class SessionClass
    {
        //public static SqlConnection SqlConnection1 = new SqlConnection(CogniPage.GetConnectionString());
        public static string SessionID = String.Empty;
        public static string SessionPW = String.Empty;

        public static bool getSessionData(HttpSessionState Session, string SessionID, string SessionPW)
        {
            bool result;
            //SqlDataAdapter da = new SqlDataAdapter("select * from sessions where (SessionID=" + SessionID
            //    + ") and ( SessionPW=" + SessionPW + ") ", SqlConnection1);
            //DataSet ds = new DataSet();
            //SqlConnection1.Open();
            //da.Fill(ds, "Sessions");
            //if ((ds.Tables["Sessions"].Rows.Count == 0))
            //{
            //    Session["UserID"] = "0";
            //    Session["SessionID"] = "1";
            //    Session["SessionPW"] = "0";
            //    Session["valid"] = "false";
            //    Session["Active"] = "False";
            //    if (SessionID == "0")
            //    {
            //        Session.Timeout = 11;
            //    }
            //    // 500000;
            //    result = false;
            //    // session.
            //}
            //else
            //{
            //    Session["UserID"] = ds.Tables["Sessions"].Rows[0]["UserID"].ToString();
            //    //Session["Active"] = true;
            //    da.SelectCommand.CommandText = "select * from Users where UserID=" + Session["UserID"].ToString();
            //    da.Fill(ds, "Users");
            //    // SesionInfo
            //    Session["SessionID"] = ds.Tables["Sessions"].Rows[0]["SessionID"].ToString();
            //    Session["SessionPW"] = ds.Tables["Sessions"].Rows[0]["SessionPW"].ToString();
            //    // User Info
            //    foreach (DataColumn col in ds.Tables["Users"].Columns)
            //    {
            //        string name = col.ColumnName;
            //        Session[name] = ds.Tables["Users"].Rows[0][name];
            //    }
            //    //Session["Admin"] = ds.Tables["Users"].Rows[0]["Admin"];
            //    //Session["ExecutiveLogon"] = ds.Tables["Users"].Rows[0]["ExecutiveLogon"];
            //    //Session["ThisUserEmail"] = ds.Tables["Users"].Rows[0]["EMail"];
            //    //Session["ThisUserFirstName"] = ds.Tables["Users"].Rows[0]["FirstName"];
            //    //Session["ThisUserLastName"] = ds.Tables["Users"].Rows[0]["LastName"];
            //    //Session["FreeTrialSignUpTimeStamp"] = ds.Tables["Companies"].Rows[0]["FreeTrialSignUpTimeStamp"];

            //    Session["xml"] = ds.GetXml();
            //    Session["valid"] = "true";
            //    result = true;
            //}
            //SqlConnection1.Close();
            result = true;
            return result;
        }

        public static bool startSession(HttpSessionState Session)
        {
            if (Session["SessionID"] == null || Session["SessionPW"] == null)
            {
                    return false;
            }
            else
            {
                SessionID = Session["SessionID"].ToString();
                SessionPW = Session["SessionPW"].ToString();
            }

            return getSessionData(Session, SessionID, SessionPW);
        }


    }

}

