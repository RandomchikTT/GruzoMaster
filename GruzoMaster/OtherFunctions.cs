using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster
{
    public static class OtherFunctions
    {
        public static String GetSha256(String strData)
        {
            try
            {
                Byte[] message = Encoding.ASCII.GetBytes(strData);
                SHA256Managed hashString = new SHA256Managed();
                String hex = "";
                Byte[] hashValue = hashString.ComputeHash(message);
                foreach (Byte x in hashValue)
                    hex += String.Format("{0:x2}", x);
                return hex;
            }
            catch (Exception e) { MessageBox.Show("GetSha256: " + e.ToString()); return null; }
        }
    }
}
