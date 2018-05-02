using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.SCM;
using Application.Common;
using System.Collections;

namespace BusinessProcess.SCM
{
    public class BSCMReport : ProcessBase, ISCMReport
    {
        #region "Constructor"

        public BSCMReport()
        {
        }

        #endregion
        public DataTable GetExperyReport(int StoreId, DateTime TransDate, DateTime ExpiryDate)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    ClsUtility.AddParameters("@Storeid", SqlDbType.Int, StoreId.ToString());
                    ClsUtility.AddParameters("@TransDate", SqlDbType.VarChar, TransDate.ToString("dd-MMM-yyyy"));
                    ClsUtility.AddParameters("@ExpiryDate", SqlDbType.VarChar, ExpiryDate.ToString("dd-MMM-yyyy"));
                    return
                        (DataTable)
                        objPOdetails.ReturnObject(ClsUtility.theParams, "pr_SCM_GetExperyReport_Futures",
                                                  ClsDBUtility.ObjectEnum.DataTable);
                }
                catch
                {
                    //DataMgr.RollBackTransation(this.Transaction);
                    throw;
                }
                finally
                {
                    if (this.Connection != null)
                        DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }
        public DataSet GetStockSummary(int StoreId, int ItemId, DateTime FromDate, DateTime ToDate)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                    ClsUtility.AddParameters("@ItemId", SqlDbType.Int, ItemId.ToString());
                    ClsUtility.AddParameters("@FromDate", SqlDbType.VarChar, FromDate.ToString("dd-MMM-yyyy"));
                    ClsUtility.AddParameters("@ToDate", SqlDbType.VarChar, ToDate.ToString("dd-MMM-yyyy"));
                    return
                        (DataSet)
                        objPOdetails.ReturnObject(ClsUtility.theParams, "pr_SCM_GetStockSummary_Futures",
                                                  ClsDBUtility.ObjectEnum.DataSet);
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
        }
        public DataSet GetBatchSummary(int StoreId, int ItemId, DateTime FromDate, DateTime ToDate)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                    ClsUtility.AddParameters("@ItemId", SqlDbType.Int, ItemId.ToString());
                    ClsUtility.AddParameters("@FromDate", SqlDbType.VarChar, FromDate.ToString("dd-MMM-yyyy"));
                    ClsUtility.AddParameters("@ToDate", SqlDbType.VarChar, ToDate.ToString("dd-MMM-yyyy"));
                    return
                        (DataSet)
                        objPOdetails.ReturnObject(ClsUtility.theParams, "pr_SCM_GetBatchSummary_Futures",
                                                  ClsDBUtility.ObjectEnum.DataSet);
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
        }
        public DataSet GetStockLedgerData(int StoreId, DateTime FromDate, DateTime ToDate)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                    ClsUtility.AddParameters("@StartDate", SqlDbType.VarChar, FromDate.ToString("dd-MMM-yyyy"));
                    ClsUtility.AddParameters("@EndDate", SqlDbType.VarChar, ToDate.ToString("dd-MMM-yyyy"));
                    ClsUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                    return
                        (DataSet)
                        objPOdetails.ReturnObject(ClsUtility.theParams, "Pr_SCM_GetStockLedger_Futures",
                                                  ClsDBUtility.ObjectEnum.DataSet);
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

        }

        public DataSet GetBINCard(int StoreId, int ItemId, DateTime FromDate, DateTime ToDate, int LocationId)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                    ClsUtility.AddParameters("@ItemId", SqlDbType.Int, ItemId.ToString());
                    ClsUtility.AddParameters("@FromDate", SqlDbType.VarChar, FromDate.ToString("dd-MMM-yyyy"));
                    ClsUtility.AddParameters("@ToDate", SqlDbType.VarChar, ToDate.ToString("dd-MMM-yyyy"));
                    ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                    return (DataSet)objPOdetails.ReturnObject(ClsUtility.theParams, "pr_SCM_BinCard_Futures", ClsDBUtility.ObjectEnum.DataSet);
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
        }

        public DataSet PharmacyDashBoard(int StoreId)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                    return (DataSet)objPOdetails.ReturnObject(ClsUtility.theParams, "pr_SCM_GetDashBoardDetails", ClsDBUtility.ObjectEnum.DataSet);
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
        }

        public DataTable GetStocksPerStore(int StoreId, int SupplierFlag)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                    ClsUtility.AddParameters("@SupplierFlag", SqlDbType.Int, SupplierFlag.ToString());
                    return (DataTable)objPOdetails.ReturnObject(ClsUtility.theParams, "pr_SCM_GetStocksPerStore", ClsDBUtility.ObjectEnum.DataTable);
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
        }
    }
}
