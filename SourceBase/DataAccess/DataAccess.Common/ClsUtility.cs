using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class ClsDBUtility
    {
        private int Pkey;
        public Hashtable theParams = new Hashtable();

        public enum ObjectEnum
        {
            DataSet,DataTable,DataRow,ExecuteNonQuery
        }  

        public void Init_Hashtable()
        {
            //theParams.Clear();
            theParams = new Hashtable();
            Pkey = 0;
        }
        public void AddParameters(string FieldName, SqlDbType FieldType, string FieldValue)
        {
            Pkey = Pkey + 1;
            theParams.Add(Pkey, FieldName);
            Pkey = Pkey + 1;
            theParams.Add(Pkey, FieldType);
                  Pkey = Pkey + 1;
            if (FieldType == SqlDbType.DateTime)//conversion of string to date time...using ISO standard for datetime defination always
            {
                DateTime dateValue;
                if (DateTime.TryParse(FieldValue, out dateValue))
                    FieldValue = dateValue.ToString("yyyyMMdd hh:mm:ss tt");
            }

            theParams.Add(Pkey, FieldValue);
        }
        /// <summary>
        /// Update: C.Low 27-Jan-2014 : Overload not available for some reason, so adding added additional method to add Paramater Direction
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="FieldType"></param>
        /// <param name="FieldValue"></param>
        /// <param name="ParamDirection">nullable value determining direction of output or input</param>
        public void AddDirectionParameter(string FieldName, SqlDbType FieldType, ParameterDirection? ParamDirection)
        {

            Pkey += 1;
            theParams.Add(Pkey, FieldName);
            Pkey += 1;
            theParams.Add(Pkey, FieldType);
            Pkey += 1;
            theParams.Add(Pkey, ParamDirection);

        }
        //IQTool extented function 
        public void AddExtendedParameters(string FieldName, SqlDbType FieldType, object FieldValue)
        {
            Pkey = Pkey + 1;
            theParams.Add(Pkey, FieldName);
            Pkey = Pkey + 1;
            theParams.Add(Pkey, FieldType);
            Pkey = Pkey + 1;
            theParams.Add(Pkey, FieldValue);
        }
        
        
    }
}
