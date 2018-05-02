using System;
using System.Runtime.Serialization;

    namespace Application.Common
    {
    [Serializable]
    public class AppException : ApplicationException 
    {
        #region "Property"
        protected string _TranslationCode;
        public string TranslationCode
        {
            get { return _TranslationCode;}
            set { _TranslationCode = value; }
        }
        #endregion

        #region "Constructor"
        public AppException()
        {
            _TranslationCode = "";
        }

        public AppException(string theMessage)
        {

        }
        #endregion

        public static AppException Create(string MsgCode)
        {
            return Create(MsgCode,"");
        }
        
        public static AppException Create(string MsgCode, MsgBuilder builder)
        {
            RawMessage themsg = MsgRepository.GetMessage(MsgCode);
            builder.MsgRepository[MsgCode] = themsg.ToString();
            string thedynamicmsg = builder.BuildMessage(MsgCode);
            return Create(MsgCode, thedynamicmsg);
        }

        public static AppException Create(string MsgCode, string theMsg)
        {
            string theException = MsgCode;
            return new AppException(string.Format("AppException/{0}",theException));
        }
    }
    }
