using System.Data;

namespace ClientSide.Models
{
    public class datavalid
    {
        public bool Check_DataTable(DataTable param)
        {
            if (param is null && param.Rows.Count < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }


        public bool checkparamstring(string param)
        {
            if (param is null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }



        public class Receiver
        {
            public string Personal_Token { get; set; }
            public string Username { get; set; }

        }

    }
}