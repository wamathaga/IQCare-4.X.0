using System;
using System.Text;
using Sand.Security.Cryptography;
using System.Security.Cryptography;
using System.Web;
using System.Collections;
    public class Utility
    {
        #region "Constructor"
        public Utility()
        {
        }
        #endregion

        public string Encrypt(string Parameter)
        {
            Encryptor Encry = new Encryptor(EncryptionAlgorithm.TripleDes);
            Encry.IV = Encoding.ASCII.GetBytes("t3ilc0m3");
            return Encry.Encrypt(Parameter, "3wmotherwdrtybnio12ewq23");
        }
        
        public string Decrypt(string Parameter)
        {
            Decryptor Decry = new Decryptor(EncryptionAlgorithm.TripleDes);
            Decry.IV = Encoding.ASCII.GetBytes("t3ilc0m3");
            return Decry.Decrypt(Parameter, "3wmotherwdrtybnio12ewq23");
        }

        public string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        public string DecodeFrom64(string encodedData)
        {

            byte[] encodedDataAsBytes=System.Convert.FromBase64String(encodedData);
            string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }

        public string EncryptSHA1(string pasword)
        {

            byte[] arrbyte = new byte[pasword.Length];
            SHA1 hash = new SHA1CryptoServiceProvider();
            arrbyte = hash.ComputeHash(Encoding.UTF8.GetBytes(pasword));
            return Convert.ToBase64String(arrbyte);

        }

    }
