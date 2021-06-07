using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    public static class Tool
    {
        public static string MaHoaMatKhau(string input)
        {
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(input);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);

            string spass = "";

            foreach (byte item in hasData)
            {
                spass += item;
            }

            return spass;
        }
    }
}
