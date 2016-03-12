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
using Parse;

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

        public static IList<T> RemoveFromList<T>(IEnumerable<T> list, T obj) where T: ParseObject
        {
            list = list.Where(item => item.ObjectId != obj.ObjectId);
            return list.ToList();
        }

        public static bool ParseContains(IEnumerable<ParseObject> list, ParseObject obj)
        {
            //IList<ParseObject> l = (IList<ParseObject>)list;
            //ParseObject o = (ParseObject)obj;
            return list.Contains(obj, new Common.ParseObjectComparer());
        }

        public class ParseObjectComparer : IEqualityComparer<ParseObject>
        {
            public bool Equals(ParseObject a, ParseObject b)
            {
                return a.ObjectId == b.ObjectId;
            }

            public int GetHashCode(ParseObject a)
            {
                return a.ObjectId.GetHashCode();
            }
        }
    }
}
