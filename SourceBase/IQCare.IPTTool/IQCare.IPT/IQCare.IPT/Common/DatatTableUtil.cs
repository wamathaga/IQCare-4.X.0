using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using System.Collections;
using Application.Common;
using System.Configuration;
using System.Collections.Specialized;
using System.Data.SqlClient;

namespace Application.Presentation
{
    public static class DatatTableUtil
    {
        public static List<T> ToList<T>(this DataTable table) where T : new()
        {
            List<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            List<T> result = new List<T>();

            foreach (var row in table.Rows)
            {
                var item = CreateItemFromRow<T>((DataRow)row, properties);
                result.Add(item);
            }

            return result;
        }

        private static T CreateItemFromRow<T>(DataRow row, List<PropertyInfo> properties) where T : new()
        {
            T item = new T();
            foreach (var property in properties)
            {
                if (row.Table.Columns.Contains(property.Name))
                {
                    if (row[property.Name] != DBNull.Value)
                        property.SetValue(item, row[property.Name], null);
                }
            }
            return item;
        }

        public static void BindCombo(ComboBox theDropDown, DataTable theDT, string theTextField, string theValueField)
        {
            BindCombo(theDT, theTextField, theValueField, theDropDown);
        }

        private static void BindCombo(DataTable theDT, string theTextField, string theValueField, ComboBox theDropDown = null)
        {
            theDropDown.Items.Clear();
            //DataRow[] DR = theDT.Select("" + theValueField + " = 0");
            //if (DR.Length < 1)
            //{
            //    DataRow theDR = theDT.NewRow();
            //    theDR["" + theTextField + ""] = "Select";
            //    theDR["" + theValueField + ""] = 0;
            //    theDT.Rows.InsertAt(theDR, 0);
            //}
            if (theDropDown != null)
            {

                theDropDown.DisplayMember = theTextField;
                theDropDown.ValueMember = theValueField;
                theDropDown.DataSource = theDT;
            }
        }

        public static DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
        {
            DataTable dt = new DataTable();


            PropertyInfo[] columns = null;

            if (Linqlist == null) return dt;

            foreach (T Record in Linqlist)
            {

                if (columns == null)
                {
                    columns = ((Type)Record.GetType()).GetProperties();
                    foreach (PropertyInfo GetProperty in columns)
                    {
                        Type colType = GetProperty.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dt.Columns.Add(new DataColumn(GetProperty.Name, colType));
                    }
                }

                DataRow dr = dt.NewRow();

                foreach (PropertyInfo pinfo in columns)
                {
                    dr[pinfo.Name] = pinfo.GetValue(Record, null) == null ? DBNull.Value : pinfo.GetValue
                    (Record, null);
                }

                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static DataTable CreateTableFromDataView(DataView obDataView)
        {
            DataTable obNewDt = null;
            if (obDataView.Table != null)
            {
                obNewDt = obDataView.Table.Clone();
                int idx = 0;
                string[] strColNames = new string[obNewDt.Columns.Count];
                foreach (DataColumn col in obNewDt.Columns)
                {
                    strColNames[idx++] = col.ColumnName;
                }

                IEnumerator viewEnumerator = obDataView.GetEnumerator();
                while (viewEnumerator.MoveNext())
                {
                    DataRowView drv = (DataRowView)viewEnumerator.Current;
                    DataRow dr = obNewDt.NewRow();
                    foreach (string strName in strColNames)
                    {
                        dr[strName] = drv[strName];
                    }
                    obNewDt.Rows.Add(dr);
                }
                return obNewDt;
            }
            return obNewDt;
        }

        public static object CheckConnection()
        {
            SqlConnection connection;
            try
            {
                Utility objUtil = new Utility();
                string constr = objUtil.Decrypt(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["ConnectionString"]);
                constr += ";connect timeout=" + ((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["SessionTimeOut"].ToString();
                constr += ";packet size=4128;Min Pool Size=3;Max Pool Size=200;";
                connection = new SqlConnection(constr);
                //if (connection.State == ConnectionState.Open)
                connection.Open();
            }
            catch (SqlException sqlException)
            {
                throw sqlException;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return connection;
        }
    }
}
