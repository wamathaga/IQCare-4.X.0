using System;
using System.Text;
using Sand.Security.Cryptography;
using System.Web;


namespace Application.Common
{
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

            byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);
            string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }

        public void SetSession()
        {
            HttpContext.Current.Session["PatientVisitId"] = 0;
            HttpContext.Current.Session["ServiceLocationId"] = 0;
            HttpContext.Current.Session["LabId"] = 0;
        }

    }
    /// <summary>
    /// class for handling Byte Order Mark problems when generating reports using xsl transformation for xml
    /// </summary>
    public class XmlEncodingBOM
    {
        public string EncDescription;
        public Encoding TextEncoding;
        public int BOMLength;
        public byte[] ByteSequnce;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlEncodingBOM" /> struct.
        /// </summary>
        /// <param name="ED">The ed.</param>
        /// <param name="TE">The te.</param>
        /// <param name="BL">The bl.</param>
        /// <param name="BS">The bs.</param>
        private XmlEncodingBOM(string ED, Encoding TE, int BL, byte[] BS)
        {
            this.EncDescription = ED;
            this.TextEncoding = TE;
            this.BOMLength = BL;
            this.ByteSequnce = BS;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlEncodingBOM"/> class.
        /// </summary>
        XmlEncodingBOM()
        {
            DefaultXmlEncodings = new XmlEncodingBOM[10]{
			    new XmlEncodingBOM("UTF-8", Encoding.UTF8, 3, new byte[3]{0xEF, 0xBB, 0xBF}),
			    new XmlEncodingBOM("UTF-16", Encoding.BigEndianUnicode, 2, new byte[2]{0xFE, 0xFF}),
			    new XmlEncodingBOM("UTF-16", Encoding.Unicode, 2, new byte[2]{0xFF, 0xFE}),
			    new XmlEncodingBOM("UTF-32", Encoding.UTF32, 4, new byte[4]{0x00, 0x00, 0xFE, 0xFF}),
			    new XmlEncodingBOM("UTF-32", Encoding.UTF32, 4, new byte[4]{0xFF, 0xFE, 0x00, 0x00}),
			    new XmlEncodingBOM("UTF-7", Encoding.UTF7, 4, new byte[4]{0x2B, 0x2F, 0x76, 0x38}),
			    new XmlEncodingBOM("UTF-7", Encoding.UTF7, 4, new byte[4]{0x2B, 0x2F, 0x76, 0x39}),
			    new XmlEncodingBOM("UTF-7", Encoding.UTF7, 4, new byte[4]{0x2B, 0x2F, 0x76, 0x2B}),
			    new XmlEncodingBOM("UTF-7", Encoding.UTF7, 4, new byte[4]{0x2B, 0x2F, 0x76, 0x2F}),
			    new XmlEncodingBOM("ASCII", Encoding.ASCII, 0, null)
            };

        }
        static XmlEncodingBOM[] DefaultXmlEncodings = new XmlEncodingBOM[10]{
			    new XmlEncodingBOM("UTF-8", Encoding.UTF8, 3, new byte[3]{0xEF, 0xBB, 0xBF}),
			    new XmlEncodingBOM("UTF-16", Encoding.BigEndianUnicode, 2, new byte[2]{0xFE, 0xFF}),
			    new XmlEncodingBOM("UTF-16", Encoding.Unicode, 2, new byte[2]{0xFF, 0xFE}),
			    new XmlEncodingBOM("UTF-32", Encoding.UTF32, 4, new byte[4]{0x00, 0x00, 0xFE, 0xFF}),
			    new XmlEncodingBOM("UTF-32", Encoding.UTF32, 4, new byte[4]{0xFF, 0xFE, 0x00, 0x00}),
			    new XmlEncodingBOM("UTF-7", Encoding.UTF7, 4, new byte[4]{0x2B, 0x2F, 0x76, 0x38}),
			    new XmlEncodingBOM("UTF-7", Encoding.UTF7, 4, new byte[4]{0x2B, 0x2F, 0x76, 0x39}),
			    new XmlEncodingBOM("UTF-7", Encoding.UTF7, 4, new byte[4]{0x2B, 0x2F, 0x76, 0x2B}),
			    new XmlEncodingBOM("UTF-7", Encoding.UTF7, 4, new byte[4]{0x2B, 0x2F, 0x76, 0x2F}),
			    new XmlEncodingBOM("ASCII", Encoding.ASCII, 0, null)
            };
        /// <summary>
        /// Gets the encoding bom.
        /// </summary>
        /// <param name="ByteArray">The byte array.</param>
        /// <returns></returns>
        public static XmlEncodingBOM GetEncodingBOM(byte[] ByteArray)
        {
            int bomIndex = -1;
            int bomLength = 0;
            for (int i = 0; i < DefaultXmlEncodings.Length; i++)
            {
                if (ByteArray.Length < DefaultXmlEncodings[i].BOMLength) break;
                bool isTheOne = true;
                for (int x = 0; x < DefaultXmlEncodings[i].BOMLength; x++)
                {
                    if (DefaultXmlEncodings[i].ByteSequnce[x] != ByteArray[x]) isTheOne = false;
                }
                if (isTheOne)
                {
                    if (DefaultXmlEncodings[i].BOMLength >= bomLength)
                    {
                        bomLength = DefaultXmlEncodings[i].BOMLength;
                        bomIndex = i;
                    }
                }
            }

            if (bomIndex < 0)
            {
                return (new XmlEncodingBOM("UTF-8", Encoding.UTF8, 0, null));
            }
            else
            {
                return (DefaultXmlEncodings[bomIndex]);
            }
        }
        /// <summary>
        /// Gets the bom string.
        /// </summary>
        /// <param name="ByteArray">The byte array.</param>
        /// <returns></returns>
        public static string GetBOMString(byte[] ByteArray)
        {
            byte[] fileData;
            string strOut;
            XmlEncodingBOM encBOM = XmlEncodingBOM.GetEncodingBOM(ByteArray);
            int FileSize = ByteArray.Length;
            if (encBOM.BOMLength > 0)
            {
                fileData = new byte[FileSize - encBOM.BOMLength];
                Array.Copy(ByteArray, encBOM.BOMLength, fileData, 0, FileSize - encBOM.BOMLength);
            }
            else
            {
                fileData = ByteArray;
            }
            strOut = encBOM.TextEncoding.GetString(fileData);
            return strOut;
        }
    }
}
