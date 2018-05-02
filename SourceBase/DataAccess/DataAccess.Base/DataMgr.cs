using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;
using MySql.Data.MySqlClient;
using System.Configuration;
using Application.Common;

namespace DataAccess.Base
{
    public enum ConnectionMode
    {
        EMR = 1,
        REPORT = 2
    };
    public class DataMgr 
    {
        protected string emrdatabase;
        protected string reportsdatabase;
        #region "Constructor"
        public DataMgr()
        {
        }
        #endregion

        #region "Custom Properties"
        public static object GetConnection()
        {
            Utility objUtil = new Utility();
            string constr = objUtil.Decrypt(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["ConnectionString"]);
            constr += ";connect timeout=" + ((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["SessionTimeOut"].ToString();
            constr += ";packet size=4128;Min Pool Size=3;Max Pool Size=200;";
            SqlConnection connection = new SqlConnection(constr);
            connection.Open();
            return connection;
        }

        public static object GetMySQLConnection()
        {
            try
            {
            Utility objUtil = new Utility();
            string constr = objUtil.Decrypt(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["MySQLConnectionString"]);
            //constr += "connect timeout=" + ((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["SessionTimeOut"].ToString();
            //constr += ";packet size=4128;Min Pool Size=3;Max Pool Size=200;";
            MySqlConnection connection = new MySqlConnection(constr);
            connection.Open();
            return connection;
            }
            catch (MySqlException ex)
            {
                //When handling errors, your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.");
                        break;
                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                    case 1042:
                        Console.WriteLine("Unable to Connect to MySQL, please check MySQL configuration");
                        break;
                }
                return false;
            }
        }

        public static object GetConnection_Master()
        {
            Utility objUtil = new Utility();
            string constr = objUtil.Decrypt(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["ConnectionStringMaster"]);
           // constr = constr.Substring(0, constr.Length - 6);
           // constr += "master";
            //constr = constr.Substring(0, constr.IndexOf("g=") + 2).ToString() + "Master";
            constr += ";connect timeout=" + ((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["SessionTimeOut"].ToString();
            constr += ";packet size=4128;Min Pool Size=3;Max Pool Size=200;";
            //set nocount off;set arithabort on;set concat_null_yields_null on;set ansi_nulls on;";
            //constr += "set cursor_close_on_commit off;set ansi_null_dflt_on on;set implicit_transactions off;set ansi_padding on;set ansi_warnings on;";
            //constr += "set quoted_identifier on;";
            SqlConnection connection = new SqlConnection(constr);
            connection.Open();
            return connection;
            
        }

        public static int CommandTimeOut()
        {
            int TimeOut = Convert.ToInt32(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["CommandTimeOut"]);
            return TimeOut;
        }

        public static void ReleaseConnection(object Connection)
        {
            SqlConnection cnn = (SqlConnection)Connection;
            if (cnn != null)
            {
                if (cnn.State != ConnectionState.Closed)
                    cnn.Close();
                cnn.Dispose();
            }
        }

        public static void ReleaseMySQLConnection(object Connection)
        {
            MySqlConnection cnn = (MySqlConnection)Connection;
            if (cnn != null)
            {
                if (cnn.State != ConnectionState.Closed)
                    cnn.Close();
                cnn.Dispose();
            }
        }
        public static object BeginTransaction(object Connection)
        {
            return ((SqlConnection)Connection).BeginTransaction();
        }

        public static void CommitTransaction(object Transation)
        {
            ((SqlTransaction)Transation).Commit();
        }

        public static void RollBackTransation(object Transaction)
        {
            ((SqlTransaction)Transaction).Rollback();
        }

        #endregion

        #region "IQTools"
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <param name="connectionMode">The connection mode.</param>
        /// <returns></returns>
        public static object GetConnection(ConnectionMode connectionMode)
        {
            try
            {
                Utility objUtil = new Utility();
                string key = "ConnectionString";
                if (connectionMode == ConnectionMode.REPORT)
                {
                    key = "IQToolsConnectionString";
                }
                //if (Mode == "Report")
                //{
                //    key = "IQToolsConnectionString";
                //}
                string constr = objUtil.Decrypt(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))[key]);
                //string constr = objUtil.Decrypt(ConfigurationManager.AppSettings.Get(key));
                //string constr = objUtil.Decrypt(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["ConnectionString"]);
                constr += ";connect timeout=" + CommandTimeOut().ToString();
                // constr += ";connect timeout=" + ConfigurationManager.AppSettings.Get("SessionTimeOut");//((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["SessionTimeOut"].ToString();
                constr += ";packet size=4128;Min Pool Size=3;Max Pool Size=200;";
                SqlConnection connection = new SqlConnection(constr);
                connection.Open();
                return connection;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="connectionMode">The connection mode.</param>
        /// <returns></returns>
        public static string GetConnectionString(ConnectionMode connectionMode)
        {
            Utility objUtil = new Utility();
            string key = "ConnectionString";
            if (connectionMode == ConnectionMode.REPORT)
            {
                key = "IQToolsConnectionString";
            }
            string constr = objUtil.Decrypt(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))[key]);
            //string constr = objUtil.Decrypt(ConfigurationManager.AppSettings.Get(key));
            return constr;
        }
        /// <summary>
        /// /// Tests the connection.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="enncrypted">if set to <c>true</c> [enncrypted].</param>
        /// <returns></returns>
        public bool TestConnection(string connectionString, bool enncrypted = true)
        {
            bool success = false;
            Utility objUtil = new Utility();
            string constr = "";
            if (enncrypted)
                constr = objUtil.Decrypt(connectionString);
            else
            {
                constr = connectionString;
            }
            try
            {
                SqlConnection connection = new SqlConnection(constr);
                connection.Open();
                success = true;
            }
            catch { }

            return success;
        }
        /// <summary>
        #endregion 
    }
}
