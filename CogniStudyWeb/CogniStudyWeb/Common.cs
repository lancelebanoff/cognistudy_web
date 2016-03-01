using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Using namespaces 
using System.Configuration;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Data;
using System.Web.SessionState;

namespace CogniTutor
{
    public class Common
    {
        public static T ParseEnum<T>(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return (T)Enum.ToObject(typeof(T), 0);
            }
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static string EscapeHTML(object o)
        {
            string s = o.ToString();
            return s.Replace("\"", "&quot;");
        }
    }
}
