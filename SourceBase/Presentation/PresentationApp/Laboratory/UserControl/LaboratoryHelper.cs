using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;

namespace PresentationApp.Laboratory
{
    public class FillSpecimen
    {
        int _specimentypeId, _specimensourceId, _specimennumbers, _specimenRecvbyId;
        string _specimentype, _specimensource, _specCustomNumber, _specimenRecvdby, _specimenOtherSource;
        DateTime _specimenDate;

        public int SpecimentypeId
        {
            get { return _specimentypeId; }
            set { _specimentypeId = value; }
        }
        public int SpecimensourceId
        {
            get { return _specimensourceId; }
            set { _specimensourceId = value; }
        }
        public int Specimennumbers
        {
            get { return _specimennumbers; }
            set { _specimennumbers = value; }
        }
        public int SpecimenRecvbyId
        {
            get { return _specimenRecvbyId; }
            set { _specimenRecvbyId = value; }
        }

        public string Specimentype
        {
            get { return _specimentype; }
            set { _specimentype = value; }
        }
        public string Specimensource
        {
            get { return _specimensource; }
            set { _specimensource = value; }
        }
        public string SpecCustomNumber
        {
            get { return _specCustomNumber; }
            set { _specCustomNumber = value; }
        }
        public string SpecimenRecvdby
        {
            get { return _specimenRecvdby; }
            set { _specimenRecvdby = value; }
        }
        public DateTime SpecimenDate
        {
            get { return _specimenDate; }
            set { _specimenDate = value; }
        }
        public string SpecimenOtherSource
        {
            get { return _specimenOtherSource; }
            set { _specimenOtherSource = value; }
        }
        public FillSpecimen(int specimentypeId, int specimensourceId, int specimennumbers, int specimenRecvbyId, string specimentype, string specimensource, string specCustomNumber, string specimenRecvdby, DateTime specimenDate, string specimenOtherSource)
        {
            _specimentypeId = specimentypeId;
            _specimensourceId = specimensourceId;
            _specimennumbers = specimennumbers;
            _specimenRecvbyId = specimenRecvbyId;
            _specimentype = specimentype;
            _specimensource = specimensource;
            _specCustomNumber = specCustomNumber;
            _specimenRecvdby = specimenRecvdby;
            _specimenDate = specimenDate;
            _specimenOtherSource = specimenOtherSource;
        }

        
    }
    public class FillTestInit
    {
        int _specimentypeId, _specimensourceId, _specimennumbers, _specimenRecvbyId;
        string _specimentype, _specimensource, _specCustomNumber, _specimenRecvdby;
        DateTime _specimenDate;

        public int SpecimentypeId
        {
            get { return _specimentypeId; }
            set { _specimentypeId = value; }
        }
        public int SpecimensourceId
        {
            get { return _specimensourceId; }
            set { _specimensourceId = value; }
        }
        public int Specimennumbers
        {
            get { return _specimennumbers; }
            set { _specimennumbers = value; }
        }
        public int SpecimenRecvbyId
        {
            get { return _specimenRecvbyId; }
            set { _specimenRecvbyId = value; }
        }

        public string Specimentype
        {
            get { return _specimentype; }
            set { _specimentype = value; }
        }
        public string Specimensource
        {
            get { return _specimensource; }
            set { _specimensource = value; }
        }
        public string SpecCustomNumber
        {
            get { return _specCustomNumber; }
            set { _specCustomNumber = value; }
        }
        public string SpecimenRecvdby
        {
            get { return _specimenRecvdby; }
            set { _specimenRecvdby = value; }
        }
        public DateTime SpecimenDate
        {
            get { return _specimenDate; }
            set { _specimenDate = value; }
        }

        public FillTestInit(int specimentypeId, int specimensourceId, int specimennumbers, int specimenRecvbyId, string specimentype, string specimensource, string specCustomNumber, string specimenRecvdby, DateTime specimenDate)
        {
            _specimentypeId = specimentypeId;
            _specimensourceId = specimensourceId;
            _specimennumbers = specimennumbers;
            _specimenRecvbyId = specimenRecvbyId;
            _specimentype = specimentype;
            _specimensource = specimensource;
            _specCustomNumber = specCustomNumber;
            _specimenRecvdby = specimenRecvdby;
            _specimenDate = specimenDate;

        }
    }
    public static class Helper
    {
        /// <summary>
        /// Converts a DataTable to a list with generic objects
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List with generic objects</returns>
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }
    }
}