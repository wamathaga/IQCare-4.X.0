using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Xml;
using System.Web.Script.Serialization;
using System.Data;
using System.Reflection;
using System.Collections;

namespace HIVCE.Common
{
    public static class SerializerUtil
    {
        public static string Serialize(object obj)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            using (StreamReader reader = new StreamReader(memoryStream))
            {
                DataContractSerializer serializer = new DataContractSerializer(obj.GetType());
                serializer.WriteObject(memoryStream, obj);
                memoryStream.Position = 0;
                return reader.ReadToEnd();
            }
        }

        public static object Deserialize(string xml, Type toType)
        {
            using (Stream stream = new MemoryStream())
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(xml);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                DataContractSerializer deserializer = new DataContractSerializer(toType);
                return deserializer.ReadObject(stream);
            }
        }

        public static string ConverToJson<T>(object obj)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));

                    serializer.WriteObject(ms, obj);
                    byte[] json = ms.ToArray();
                    ms.Close();

                    return Encoding.UTF8.GetString(json, 0, json.Length);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<T> JsonToObject<T>(string jsonData)
        {
            using (MemoryStream ms2 = new MemoryStream(Encoding.UTF8.GetBytes(jsonData)))
            {
                try
                {
                    // Deserialize into generic List 
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<T>));
                    List<T> jsonList = ser.ReadObject(ms2) as List<T>;
                    // Serialize JSON MemoryStream to WCS XML wire format 
                    XmlDictionaryReader xdrJson = JsonReaderWriterFactory.CreateJsonReader(ms2, XmlDictionaryReaderQuotas.Max);
                    xdrJson.Read();
                    string xml = xdrJson.ReadOuterXml();
                    xdrJson.Close();
                    ms2.Close();

                    return jsonList;
                }
                catch { throw; }
            }
        }

        public static List<T> JsonToObject<T>(string jsonData, string jsonKey)
        {
            try
            {

                object objChannelInfo = new JavaScriptSerializer().DeserializeObject(jsonData);
                Dictionary<string, object> list = (Dictionary<string, object>)objChannelInfo;
                object val = null;
                list.TryGetValue(jsonKey, out val);
                using (MemoryStream ms2 = new MemoryStream(Encoding.UTF8.GetBytes(val.ToString())))
                {
                    //Deserialize into generic List 
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<T>));
                    List<T> jsonList = ser.ReadObject(ms2) as List<T>;
                    // Serialize JSON MemoryStream to WCS XML wire format 
                    XmlDictionaryReader xdrJson = JsonReaderWriterFactory.CreateJsonReader(ms2, XmlDictionaryReaderQuotas.Max);
                    xdrJson.Read();
                    string xml = xdrJson.ReadOuterXml();
                    xdrJson.Close();
                    ms2.Close();

                    return jsonList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T ConverToObject<T>(string json)// where T: new ()
        {
            T returnValue = default(T);//new T();
            if (!string.IsNullOrEmpty(json))
            {
                using (var memoryStream = new MemoryStream())
                {
                    byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
                    memoryStream.Write(jsonBytes, 0, jsonBytes.Length);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    using (var jsonReader = JsonReaderWriterFactory.CreateJsonReader(memoryStream, Encoding.UTF8, XmlDictionaryReaderQuotas.Max, null))
                    {
                        if (!object.Equals(jsonBytes, null))
                        {
                            var serializer = new DataContractJsonSerializer(typeof(T));
                            returnValue = (T)serializer.ReadObject(jsonReader);
                        }
                    }
                }
            }
            return returnValue;
        }

        public static T ConvertToEntity<T>(this DataRow tableRow) where T : new()
        {
            // Create a new type of the entity I want
            Type t = typeof(T);
            T returnObject = new T();

            foreach (DataColumn col in tableRow.Table.Columns)
            {
                string colName = col.ColumnName;

                // Look for the object's property with the columns name, ignore case
                PropertyInfo pInfo = t.GetProperty(colName.ToLower(),
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                // did we find the property ?
                if (pInfo != null)
                {
                    object val = tableRow[colName];

                    // is this a Nullable<> type
                    bool IsNullable = (Nullable.GetUnderlyingType(pInfo.PropertyType) != null);
                    if (IsNullable)
                    {
                        if (val is System.DBNull)
                        {
                            val = null;
                        }
                        else
                        {
                            // Convert the db type into the T we have in our Nullable<T> type
                            val = Convert.ChangeType
                    (val, Nullable.GetUnderlyingType(pInfo.PropertyType));
                        }
                    }
                    else
                    {
                        // Convert the db type into the type of the property in our entity
                        val = Convert.ChangeType(val, pInfo.PropertyType);
                    }
                    // Set the value of the property with the value from the db
                    pInfo.SetValue(returnObject, val, null);
                }
            }

            // return the entity object with values
            return returnObject;
        }

        public static List<T> ConvertToList<T>(this DataTable table) where T : new()
        {
            Type t = typeof(T);

            // Create a list of the entities we want to return
            List<T> returnObject = new List<T>();

            // Iterate through the DataTable's rows
            foreach (DataRow dr in table.Rows)
            {
                // Convert each row into an entity object and add to the list
                T newRow = dr.ConvertToEntity<T>();
                returnObject.Add(newRow);
            }

            // Return the finished list
            return returnObject;
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

    }
}