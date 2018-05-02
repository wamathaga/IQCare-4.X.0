using System;
using System.Xml;
using System.Configuration;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;


    namespace Application.Common
    {
    public class MsgRepository
    {
        #region "Constructor"
        public MsgRepository()
        {
        }
        #endregion

        private static XmlDocument doc = null;

        public static RawMessage GetMessage(string MsgId)
        {
            if (doc == null)
            {
                doc = new XmlDocument();
                doc.Load(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["MessageRepository"]);
            }

            XmlElement Root = doc.DocumentElement;
            XmlNode Node = Root.SelectSingleNode("Message[@Id = '" + MsgId.Trim() + "']");
            if (Node != null)
            {
                string Id = "", Text = "", Type = "", Buttons = "";
                Id = Node.Attributes["Id"].Value;
                Text = Node.Attributes["Text"].Value;
                Type = Node.Attributes["Type"].Value;
                Buttons = Node.Attributes["Buttons"].Value;
                RawMessage theRawMsg = new RawMessage(Id, Type, Text, Buttons);
                return theRawMsg;
            }
            else
            {
                return new RawMessage("", "", "", "");
            }
        }
    }

    #region "User Struct"
    public struct RawMessage
    {
        public string Text, Id, Type, Buttons;
        public RawMessage(string MessageId, string MessageType, string MessageText, string MessageButtons)
        {
            this.Id = MessageId;
            this.Type = MessageType;
            this.Text = MessageText;
            this.Buttons = MessageButtons;
        }

        public override string ToString()
        {
            return this.Text;
        }
    }
    #endregion

    }
