using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using System.Data;
using DataAccess.Entity;
using DataAccess.Common;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace IQCare.IPT.Common
{
    public class Patient
    {
        public int PatientId { get; set; }
        public String FirstName { get; set; }
        public String MiddleName { get; set; }
        public String LastName { get; set; }
        public String IQNumber { get; set; }
        public String DOB { get; set; }
        public String Sex { get; set; }
        //public String Status { get; set; }
    }

    public class CodeDeCodeTables
    {
        public int CodeId { get; set; }
        public String CodeName { get; set; }
        public int DeCodeId { get; set; }
        public string DeCodeName { get; set; }
    }

    public class MultiSelectTable
    {
        public int Ptn_pk { get; set; }
        public int Visit_Pk { get; set; }
        public int ValueID { get; set; }
        public string FieldName { get; set; }
        public DateTime? OnSetDate { get; set; }
        public int? NumericField { get; set; }
        public string Notes { get; set; }
        public string VNotes { get; set; }
    }

    public class BIPTDetails : ProcessBase, IIPTDetails
    {
        public static string DBSecurity = "'ttwbvXWpqb5WOLfLrBgisw=='";

        public DataSet GetInitialDetails(int facilityID)
        {
            lock (this)
            {
                ClsObject clsObject = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@facilityId", SqlDbType.Int, facilityID.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)clsObject.ReturnObject(ClsUtility.theParams, "pr_IPT_GetInitialDetails", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetDatabaseNames(string dataSource, string userId, string password)
        {
            lock (this)
            {
                SqlConnection conn = null;
                DataSet dataset = new DataSet();
                try
                {
                    string sql;

                    Cursor.Current = Cursors.WaitCursor;

                    string sqlString = @"Data Source=" + dataSource.ToString() + ";uid=" + userId.ToString() + "; pwd=" + password.ToString() + ";";
                    conn = new SqlConnection(sqlString);
                    conn.Open();

                    sql = "SELECT name FROM SYS.DATABASES where name like '%IQCare%';";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                   
                    using (SqlDataAdapter theAdpt = new SqlDataAdapter(cmd))
                    {
                        theAdpt.Fill(dataset);
                    }

                    cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();
                    Cursor.Current = Cursors.Default;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                    {
                        if (conn.State != ConnectionState.Closed)
                            conn.Close();
                        conn.Dispose();
                    }
                }
                return dataset;
            }
        }

        public DataSet GetFacilityNames(string dataSource, string userId, string password,string initialCatalog)
        {
            lock (this)
            {
                SqlConnection conn = null;
                DataSet dataset = new DataSet();
                try
                {
                    string sql;

                    Cursor.Current = Cursors.WaitCursor;

                    string sqlString = @"Data Source=" + dataSource.ToString() + ";uid=" + userId.ToString() +
                                       "; pwd=" + password.ToString() + ";Initial Catalog=" +
                                       initialCatalog.ToString() + ";";
                    conn = new SqlConnection(sqlString);
                    conn.Open();

                    sql = "select FacilityName,FacilityId,PosId from mst_facility where deleteflag=0;";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    using (SqlDataAdapter theAdpt = new SqlDataAdapter(cmd))
                    {
                        theAdpt.Fill(dataset);
                    }

                    cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();
                    Cursor.Current = Cursors.Default;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                    {
                        if (conn.State != ConnectionState.Closed)
                            conn.Close();
                        conn.Dispose();
                    }
                }
                return dataset;
            }
        }

        public DataSet GetPatientDetails(int facilityID, int moduleId, string identifier, string identifierNumber)
        {
            lock (this)
            {
                ClsObject clsObject = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@EnrollmentType", SqlDbType.Int, identifier.ToString());
                ClsUtility.AddParameters("@enrollmentid", SqlDbType.Int, identifierNumber.ToString());
                ClsUtility.AddParameters("@FacilityId", SqlDbType.Int, facilityID.ToString());
                ClsUtility.AddParameters("@password", SqlDbType.Int, DBSecurity);
                ClsUtility.AddParameters("@moduleId", SqlDbType.Int, moduleId.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)clsObject.ReturnObject(ClsUtility.theParams, "Pr_IPT_GetPatientSearchResults", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetPatientIPTDetails(int facilityID, int ptnpk)
        {
            lock (this)
            {
                ClsObject clsObject = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptnpk.ToString());
                ClsUtility.AddParameters("@facilityId", SqlDbType.Int, facilityID.ToString());
                ClsObject UserManager = new ClsObject();
                return (DataSet)clsObject.ReturnObject(ClsUtility.theParams, "pr_IPT_GetPatientIPTDetails", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataTable SavePatientIPTDetails(int facilityID, int ptnpk, int visitId,
                                                string IPT,
                                                string IPTAdherence,
                                                string IPTContraindication,
                                                string IPTDiscontinued,
                                                DateTime? IPTEndDate,
                                                DateTime? IPTStartDate,
                                                int EligibleForIPT,
                                                string otherReasonDeclinedIPT,
                                                string otherReasonDiscontinuedIPT
                )
        {
            ClsObject clsObj = new ClsObject();
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                clsObj.Connection = this.Connection;
                clsObj.Transaction = this.Transaction;
                string dtVal = DBNull.Value.ToString();

                DataTable ReturnDT = new DataTable();

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptnpk.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, facilityID.ToString());
                ClsUtility.AddParameters("@Visit_Pk", SqlDbType.Int, visitId.ToString());
                ClsUtility.AddParameters("@IPT", SqlDbType.Int, IPT.ToString());
                ClsUtility.AddParameters("@IPTAdherence", SqlDbType.Int, IPTAdherence.ToString());
                ClsUtility.AddParameters("@IPTContraindication", SqlDbType.Int, IPTContraindication.ToString());
                ClsUtility.AddParameters("@IPTDiscontinued", SqlDbType.Int, IPTDiscontinued.ToString());
                ClsUtility.AddParameters("@IPTEndDate", SqlDbType.VarChar, IPTEndDate.HasValue == true ? IPTEndDate.Value.ToString("yyyy-MM-dd hh:mm:ss").ToString() : DBNull.Value.ToString());
                ClsUtility.AddParameters("@IPTStartDate", SqlDbType.VarChar, IPTStartDate.HasValue == true ? IPTStartDate.Value.ToString("yyyy-MM-dd hh:mm:ss").ToString() : DBNull.Value.ToString());
                ClsUtility.AddParameters("@EligibleForIPT", SqlDbType.Int, "1".ToString());
                ClsUtility.AddParameters("@OtherReasonDeclinedIPT", SqlDbType.VarChar, otherReasonDeclinedIPT.ToString());
                ClsUtility.AddParameters("@OtherReasonDiscontinuedIPT", SqlDbType.VarChar, otherReasonDiscontinuedIPT.ToString());
                ClsObject UserManager = new ClsObject();
                ReturnDT = (DataTable)clsObj.ReturnObject(ClsUtility.theParams, "pr_IPT_SavePatientIPTDetails", ClsDBUtility.ObjectEnum.DataTable);

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return ReturnDT;
            }
            catch (Exception ex)
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw ex;
            }
            finally
            {

                clsObj = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
    }
}
