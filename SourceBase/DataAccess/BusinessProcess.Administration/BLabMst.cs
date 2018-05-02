using System;
using System.Data;
using System.Data.SqlClient;
using Interface.Administration;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;

namespace BusinessProcess.Administration
{
    public class BLabMst : ProcessBase, ILabMst
    {
        #region "Constructor"
        public BLabMst()
        {
        }
        #endregion

        public DataSet GetLabs()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject LabManager = new ClsObject();
                return (DataSet)LabManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectLabTest_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetLabTestList()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject LabManager = new ClsObject();
                return (DataSet)LabManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetLabTestList_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetDepartments()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject LabManager = new ClsObject();
                return (DataSet)LabManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectLabDepartment_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetDropDowns()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject LabManager = new ClsObject();
                return (DataSet)LabManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectLabDD_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        //public DataSet DeleteLab(int labid)
        //{
        //    ClsUtility.Init_Hashtable();
        //    ClsObject LabManager = new ClsObject();
        //    ClsUtility.AddParameters("@Original_LabTestID", SqlDbType.Int, labid.ToString());
        //    return (DataSet)LabManager.ReturnObject(ClsUtility.theParams, "pr_Admin_DeleteLabTest_Constella", ClsDBUtility.ObjectEnum.DataSet);
        //}
        public DataSet DeleteLabtype(int labtypeid)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject LabManager = new ClsObject();
                ClsUtility.AddParameters("@Original_LabTypeID", SqlDbType.Int, labtypeid.ToString());
                return (DataSet)LabManager.ReturnObject(ClsUtility.theParams, "pr_Admin_DeleteLabtypeCategory_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetLabType()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject LabManager = new ClsObject();
                return (DataSet)LabManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectLabType_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetLabTypeByID(int labtypeid)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject LabManager = new ClsObject();
                ClsUtility.AddParameters("@origlabtypeid", SqlDbType.Int, labtypeid.ToString());

                return (DataSet)LabManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectReasonCategoryByID_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetLabByID(int labid)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject LabManager = new ClsObject();
                ClsUtility.AddParameters("@OrigLabTestID", SqlDbType.Int, labid.ToString());

                return (DataSet)LabManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SelectLabTestByID_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetSubTestDetails(int SubTestID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject LabManager = new ClsObject();
                ClsUtility.AddParameters("@SubTestID", SqlDbType.Int, SubTestID.ToString());
                return (DataSet)LabManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetSubTestDetails_Constella", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataTable SaveNewLab(string LabName, int DepartmentID, int LabTypeID, int UserID, string DataType, decimal MaxBoundary, decimal MinBoundary, int Sequence, int UnitId)
        {
            try
            {
                DataTable theAffectedDT;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject LabManager = new ClsObject();
                LabManager.Connection = this.Connection;
                LabManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@LabName", SqlDbType.VarChar, LabName);
                ClsUtility.AddParameters("@LabDepartmentID", SqlDbType.Int, DepartmentID.ToString());
                ClsUtility.AddParameters("@LabTypeID", SqlDbType.Int, LabTypeID.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
             //   ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, DeleteFlag.ToString());
                ClsUtility.AddParameters("@DataType", SqlDbType.NVarChar, DataType.ToString());
                ClsUtility.AddParameters("@MaxBoundary", SqlDbType.Decimal, MaxBoundary.ToString());
                ClsUtility.AddParameters("@MinBoundary", SqlDbType.Decimal, MinBoundary.ToString());

                ClsUtility.AddParameters("@Sequence", SqlDbType.Int, Sequence.ToString());
                ClsUtility.AddParameters("@UnitId", SqlDbType.Int, UnitId.ToString());

                DataRow theDR;
                theAffectedDT = (DataTable)LabManager.ReturnObject(ClsUtility.theParams, "pr_Admin_AddLabTest_Constella", ClsDBUtility.ObjectEnum.DataTable);
                if (theAffectedDT.Rows[0][0].ToString() == "-1")
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Lab record. Try Again..";
                    AppException.Create("#C1", theBL);
                }
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theAffectedDT;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public DataTable SaveLabUnitLinks(int ID, int SubTestID, decimal MinBoundaryValue, decimal MaxBoundaryValue, int UnitID, int DefaultUnit, int Undetectable)
        {
            try
            {
                DataTable theAffectedDT;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject LabManager = new ClsObject();
                LabManager.Connection = this.Connection;
                LabManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ID", SqlDbType.Int, ID.ToString());
                ClsUtility.AddParameters("@SubTestID", SqlDbType.Int, SubTestID.ToString());
                ClsUtility.AddParameters("@MinBoundaryValue", SqlDbType.Decimal, MinBoundaryValue.ToString());
                ClsUtility.AddParameters("@MaxBoundaryValue", SqlDbType.Decimal, MaxBoundaryValue.ToString());
                ClsUtility.AddParameters("@UnitID", SqlDbType.Int, UnitID.ToString());
                ClsUtility.AddParameters("@DefaultUnit", SqlDbType.Int, DefaultUnit.ToString());
                ClsUtility.AddParameters("@Undetectable", SqlDbType.Int, Undetectable.ToString());
                DataRow theDR;
                theAffectedDT = (DataTable)LabManager.ReturnObject(ClsUtility.theParams, "pr_Admin_SaveLabUnitLinks_Constella", ClsDBUtility.ObjectEnum.DataTable);
                if (theAffectedDT.Rows[0][0].ToString() == "-1")
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Lab record. Try Again..";
                    AppException.Create("#C1", theBL);
                }
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theAffectedDT;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
        public DataTable ChangeDefaultUnit(int ID)
        {
            try
            {
                DataTable theAffectedDT;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject LabManager = new ClsObject();
                LabManager.Connection = this.Connection;
                LabManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ID", SqlDbType.Int, ID.ToString());
                DataRow theDR;
                theAffectedDT = (DataTable)LabManager.ReturnObject(ClsUtility.theParams, "pr_Admin_ChangeDefaultUnit_Constella", ClsDBUtility.ObjectEnum.DataTable);
                if (theAffectedDT.Rows[0][0].ToString() == "-1")
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Lab record. Try Again..";
                    AppException.Create("#C1", theBL);
                }
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theAffectedDT;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
        public DataTable CheckDefaultUnit(int ID)
        {
            try
            {
                ClsUtility.Init_Hashtable();
                ClsObject LabManager = new ClsObject();
                ClsUtility.AddParameters("@ID", SqlDbType.Int, ID.ToString());
                return (DataTable)LabManager.ReturnObject(ClsUtility.theParams, "pr_Admin_CheckDefaultUnit_Constella", ClsDBUtility.ObjectEnum.DataTable);

            }
            catch
            {
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }



        //public int SaveNewLabType(string LabTypeName, int UserID)
        //{
        //    try
        //    {
        //        this.Connection = DataMgr.GetConnection();
        //        this.Transaction = DataMgr.BeginTransaction(this.Connection);

        //        ClsObject LabManager = new ClsObject();
        //        LabManager.Connection = this.Connection;
        //        LabManager.Transaction = this.Transaction;

        //        ClsUtility.Init_Hashtable();
        //        ClsUtility.AddParameters("@LabTypeName", SqlDbType.VarChar, LabTypeName);
        //        ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());

        //        DataRow theDR;
        //        int RowsAffected = (Int32)LabManager.ReturnObject(ClsUtility.theParams, "pr_Admin_AddLabtype_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
        //        if (RowsAffected == 0)
        //        {
        //            MsgBuilder theBL = new MsgBuilder();
        //            theBL.DataElements["MessageText"] = "Error in Saving Lab type record. Try Again..";
        //            AppException.Create("#C1", theBL);
        //        }


        //        DataMgr.CommitTransaction(this.Transaction);
        //        DataMgr.ReleaseConnection(this.Connection);
        //        return Convert.ToInt32(RowsAffected);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);
        //    }
        //}
        //public int UpdateLabType(int LabTypeID, string LabTypeName, int UserID)
        //{
        //    try
        //    {
        //        this.Connection = DataMgr.GetConnection();
        //        this.Transaction = DataMgr.BeginTransaction(this.Connection);

        //        ClsObject LabManager = new ClsObject();
        //        LabManager.Connection = this.Connection;
        //        LabManager.Transaction = this.Transaction;

        //        ClsUtility.Init_Hashtable();
        //        ClsUtility.AddParameters("@LabTypeName", SqlDbType.VarChar, LabTypeName);
        //        ClsUtility.AddParameters("@LabTypeID", SqlDbType.Int, LabTypeID.ToString());
        //        ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());

        //        DataRow theDR;
        //        int RowsAffected = (Int32)LabManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_UpdateLabType_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
        //        if (RowsAffected == 0)
        //        {
        //            MsgBuilder theBL = new MsgBuilder();
        //            theBL.DataElements["MessageText"] = "Error in Saving Lab type record. Try Again..";
        //            AppException.Create("#C1", theBL);
        //        }


        //        DataMgr.CommitTransaction(this.Transaction);
        //        DataMgr.ReleaseConnection(this.Connection);
        //        return Convert.ToInt32(RowsAffected);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);
        //    }
        //}


        public DataTable UpdateLab(int LabID, string LabName, int LabDepartmentID, int LabTypeID, int UserID, int DeleteFlag, string DataType, decimal MaxBoundary, decimal MinBoundary, int LabValueId, int Sequence, int UnitId)
        {
            try
            {
                DataTable theAffectedDT;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject LabManager = new ClsObject();
                LabManager.Connection = this.Connection;
                LabManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@LabName", SqlDbType.VarChar, LabName);
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@LabDepartmentId", SqlDbType.Int, LabDepartmentID.ToString());
                ClsUtility.AddParameters("@LabTypeId", SqlDbType.Int, LabTypeID.ToString());
                ClsUtility.AddParameters("@LabTestId", SqlDbType.Int, LabID.ToString());
                ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, DeleteFlag.ToString());
                ClsUtility.AddParameters("@DataType", SqlDbType.NVarChar, DataType.ToString());
                ClsUtility.AddParameters("@MaxBoundary", SqlDbType.Decimal , MaxBoundary.ToString());
                ClsUtility.AddParameters("@MinBoundary", SqlDbType.Decimal, MinBoundary.ToString());
                ClsUtility.AddParameters("@LabValueId", SqlDbType.Int, LabValueId.ToString());

                ClsUtility.AddParameters("@Sequence", SqlDbType.Int, Sequence.ToString());
                ClsUtility.AddParameters("@UnitId", SqlDbType.Int, UnitId.ToString());
                //DataRow theDR;
                theAffectedDT = (DataTable)LabManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_UpdateLabTest_Constella", ClsDBUtility.ObjectEnum.DataTable);
                if (theAffectedDT.Rows[0][0].ToString() == "-1")
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in Saving Lab test record. Try Again..";
                    AppException.Create("#C1", theBL);
                }


                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theAffectedDT;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }
        public int SaveNewLabselectList(int testid, DataTable theDTselectList, int UserID)
        {
            try
            {
               
                int theRowAffected = 0;
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject LabManager = new ClsObject();
                LabManager.Connection = this.Connection;
                LabManager.Transaction = this.Transaction;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@testID", SqlDbType.Int, testid.ToString());
               
                theRowAffected = (Int32)LabManager.ReturnObject(ClsUtility.theParams, "pr_Admin_deleteLabTestselectList_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                if (theRowAffected == 0)
                {
                    MsgBuilder theBL = new MsgBuilder();
                    theBL.DataElements["MessageText"] = "Error in delete Lab Result record. Try Again..";
                    AppException.Create("#C1", theBL);
                }


                for (int i = 0; i < theDTselectList.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@LabName", SqlDbType.VarChar, theDTselectList.Rows[i]["selectlist"].ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    ClsUtility.AddParameters("@testID", SqlDbType.Int, testid.ToString());

                    theRowAffected = (Int32)LabManager.ReturnObject(ClsUtility.theParams, "pr_Admin_AddLabTestselectList_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    if (theRowAffected == 0)
                    {
                        MsgBuilder theBL = new MsgBuilder();
                        theBL.DataElements["MessageText"] = "Error in Saving Lab record. Try Again..";
                        AppException.Create("#C1", theBL);
                    }
                }
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theRowAffected;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        /// <summary>
        /// Saves the lab group items.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="itemList">The item list.</param>
        /// <param name="labGroupID">The lab group identifier.</param>
        public void SaveLabGroupItems(int userID, DataTable itemList, int labGroupID, string LabName, int DepartmentID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                ClsUtility.AddParameters("@labGroupID", SqlDbType.Int, labGroupID.ToString());
                ClsUtility.AddParameters("@LabName", SqlDbType.VarChar, LabName);
                ClsUtility.AddParameters("@LabDepartmentID", SqlDbType.Int, DepartmentID.ToString());

                System.Text.StringBuilder sbItems = new System.Text.StringBuilder("<root>");
                foreach (DataRow row in itemList.Rows)
                {

                    sbItems.Append("<row>");
                    sbItems.Append("<LabgroupID>" + row["LabgroupID"].ToString() + "</LabgroupID>");
                    sbItems.Append("<LabTestID>" + row["LabTestID"].ToString() + "</LabTestID>");

                    sbItems.Append("</row>");
                }
                sbItems.Append("</root>");
                ClsUtility.AddExtendedParameters("@ItemList", SqlDbType.Xml, sbItems.ToString());
                ClsObject LabManager = new ClsObject();
                LabManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Admin_SaveLabGroupTests", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

            }
        }
        /// <summary>
        /// Gets the lab group tests.
        /// </summary>
        /// <param name="labGroupID">The lab group identifier.</param>
        /// <returns></returns>
        public DataSet GetLabGroupTests(int labGroupID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@LabGroupTestID", SqlDbType.Int, labGroupID.ToString());
                ClsObject LabManager = new ClsObject();
                return (DataSet)LabManager.ReturnObject(ClsUtility.theParams, "pr_Admin_GetLabGroupTests", ClsDBUtility.ObjectEnum.DataSet);
            }
        }
    }
}
