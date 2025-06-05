using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace CryptoHelp
{
    public class CryptoHelper
    {
        public string Ecrypt(string Line)
        {
            string ToReturn = "";
            try
            {
                string textToEncrypt = Line;

                string publickey = "santhosh";
                string secretkey = "engineer";
                byte[] secretkeyByte;
                secretkeyByte = System.Text.Encoding.UTF8.GetBytes(secretkey);
                byte[] publickeybyte;
                publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
                MemoryStream ms;
                CryptoStream cs;
                byte[] inputbyteArray = System.Text.Encoding.UTF8.GetBytes(textToEncrypt);
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateEncryptor(publickeybyte, secretkeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    ToReturn = Convert.ToBase64String(ms.ToArray());
                }
                ToReturn = ToReturn.Replace("\\", "xxx");
                ToReturn = ToReturn.Replace("&", "yyy");
                ToReturn = ToReturn.Replace("/", "zzz");
                ToReturn = ToReturn.Replace("]", "aaa");
                ToReturn = ToReturn.Replace("G0", "ppp");
                ToReturn = ToReturn.Replace("0x", "lll");

            }
            catch (Exception ex)
            {
            }
            return ToReturn;
        }
        public string Decrypt(string Line)
        {
            string ToReturn = "";
            try
            {

                Line = Line.Replace("xxx", "\\");
                Line = Line.Replace("yyy", "&");
                Line = Line.Replace("zzz", "/");
                Line = Line.Replace("aaa", "]");
                Line = Line.Replace("ppp", "G0");
                Line = Line.Replace("lll", "0x");

                string publickey = "santhosh";
                string privatekey = "engineer";
                byte[] privatekeyByte = System.Text.Encoding.UTF8.GetBytes(privatekey);
                byte[] publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
                MemoryStream ms = null;
                CryptoStream cs = null;
                byte[] inputbyteArray = new byte[Line.Replace(" ", "+").Length];
                inputbyteArray = Convert.FromBase64String(Line.Replace(" ", "+"));
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateDecryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    Encoding encoding = Encoding.UTF8;
                    ToReturn = encoding.GetString(ms.ToArray());
                }
            }
            catch (Exception ex)
            {

            }
            return ToReturn;
        }
    }
}
