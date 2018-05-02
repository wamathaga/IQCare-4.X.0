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
    public class BPurchase : ProcessBase, IPurchase
    {
       #region "Constructor"

        public BPurchase()
        {
        }

        #endregion

        public int SavePurchaseOrder(DataTable DtMasterPO, DataTable dtPOItems, bool isUpdate)
        {

            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject PODetail = new ClsObject();
                PODetail.Connection = this.Connection;
                PODetail.Transaction = this.Transaction;
                int theRowAffected = 0;
                int POID = 0;
                DataRow theDR;

                ClsUtility.Init_Hashtable();

                ClsUtility.AddParameters("@LocationID", SqlDbType.VarChar, DtMasterPO.Rows[0]["LocationID"].ToString());
                ClsUtility.AddParameters("@SupplierID", SqlDbType.Int, DtMasterPO.Rows[0]["SupplierID"].ToString());
                ClsUtility.AddParameters("@OrderDate", SqlDbType.VarChar, DtMasterPO.Rows[0]["OrderDate"].ToString());
                ClsUtility.AddParameters("@PreparedBy", SqlDbType.VarChar, DtMasterPO.Rows[0]["PreparedBy"].ToString());
                ClsUtility.AddParameters("@SourceStoreID", SqlDbType.Int, DtMasterPO.Rows[0]["SrcStore"].ToString());
                ClsUtility.AddParameters("@DestinStoreID", SqlDbType.Int, DtMasterPO.Rows[0]["DestStore"].ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, DtMasterPO.Rows[0]["UserID"].ToString());
                ClsUtility.AddParameters("@AuthorizedBy", SqlDbType.Int, DtMasterPO.Rows[0]["AthorizedBy"].ToString());
                if (isUpdate)
                {
                    ClsUtility.AddParameters("@Poid", SqlDbType.Int, DtMasterPO.Rows[0]["POID"].ToString());
                    ClsUtility.AddParameters("@IsUpdate", SqlDbType.Bit, isUpdate.ToString());

                    if (Convert.ToString(DtMasterPO.Rows[0]["IsRejectedStatus"]) == "1")
                    {
                        ClsUtility.AddParameters("@Status", SqlDbType.Int, "5");
                    }
                    else
                    {
                        if (Convert.ToString(DtMasterPO.Rows[0]["AthorizedBy"]) == "0")
                        {
                            ClsUtility.AddParameters("@Status", SqlDbType.Int, "1");
                        }
                        else
                        {
                            ClsUtility.AddParameters("@Status", SqlDbType.Int, "2");
                        }
                    }
                }
                else
                {
                    if (Convert.ToString(DtMasterPO.Rows[0]["AthorizedBy"]) == "0")
                    {
                        ClsUtility.AddParameters("@Status", SqlDbType.Int, "1");
                    }
                    else
                    {
                        ClsUtility.AddParameters("@Status", SqlDbType.Int, "2");
                    }
                }

                theDR =
                    (DataRow)
                    PODetail.ReturnObjectNewImpl(ClsUtility.theParams, "pr_SCM_SavePurchaseOrderMaster_Futures",
                                          ClsDBUtility.ObjectEnum.DataRow);
                POID = System.Convert.ToInt32(theDR[0].ToString());

                if (isUpdate)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@POId", SqlDbType.Int, POID.ToString());
                    theRowAffected =
                        (int)
                        PODetail.ReturnObject(ClsUtility.theParams, "pr_SCM_DeletePurchaseOrderItem_Futures",
                                              ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }

                for (int i = 0; i < dtPOItems.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@POId", SqlDbType.Int, POID.ToString());
                    ClsUtility.AddParameters("@ItemId", SqlDbType.VarChar, dtPOItems.Rows[i]["ItemId"].ToString());
                    ClsUtility.AddParameters("@Quantity", SqlDbType.Int, dtPOItems.Rows[i]["Quantity"].ToString());
                    ClsUtility.AddParameters("@PurchasePrice", SqlDbType.Decimal,
                                             dtPOItems.Rows[i]["priceperunit"].ToString());
                    //  ClsUtility.AddParameters("@Unit", SqlDbType.Int,dtPOItems.Rows[i]["Units"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, DtMasterPO.Rows[0]["UserID"].ToString());

                    ClsUtility.AddParameters("@BatchID", SqlDbType.Int, dtPOItems.Rows[i]["BatchID"].ToString());
                    ClsUtility.AddParameters("@AvaliableQty", SqlDbType.Int, dtPOItems.Rows[i]["AvaliableQty"].ToString());
                    ClsUtility.AddParameters("@ExpiryDate", SqlDbType.VarChar, dtPOItems.Rows[i]["ExpiryDate"].ToString());
                    ClsUtility.AddParameters("@UnitQuantity", SqlDbType.Int, dtPOItems.Rows[i]["UnitQuantity"].ToString());
                    theRowAffected =
                        (int)
                        PODetail.ReturnObject(ClsUtility.theParams, "pr_SCM_SavePurchaseOrderItem_Futures",
                                              ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return POID;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }
        }
        public DataSet GetPurcaseOrderItem(int isPO, int UserID, int StoreID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject GetPurcahseItem = new ClsObject();
                ClsUtility.AddParameters("@isPO", SqlDbType.Int, isPO.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreID.ToString());

                return
                    (DataSet)
                    GetPurcahseItem.ReturnObject(ClsUtility.theParams, "Pr_SCM_GetPurcaseOrderItem",
                                                 ClsDBUtility.ObjectEnum.DataSet);
            }
        }
        public DataSet GetPurchaseOrderDetailsByPoid(Int32 POId)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    ClsUtility.AddParameters("@Poid", SqlDbType.Int, POId.ToString());
                    return
                        (DataSet)
                        objPOdetails.ReturnObject(ClsUtility.theParams, "pr_SCM_GetPurchaseOrderDetailsByPoid_Futures",
                                                  ClsDBUtility.ObjectEnum.DataSet);
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
        public DataTable GetPurchaseOrderDetails(Int32 UserID, Int32 DestinStoreID, Int32 locationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    ClsUtility.AddParameters("@DestinStoreID", SqlDbType.Int, DestinStoreID.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, locationID.ToString());

                    return
                        (DataTable)
                        objPOdetails.ReturnObject(ClsUtility.theParams, "Pr_SCM_GetPurchaseDetails_Futures",
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
        public DataTable GetPurchaseOrderDetailsForGRN(Int32 UserID, Int32 StoreID, Int32 locationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    ClsUtility.AddParameters("@StoreID", SqlDbType.Int, StoreID.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, locationID.ToString());

                    return
                        (DataTable)
                        objPOdetails.ReturnObject(ClsUtility.theParams, "Pr_SCM_GetPurchaseDetailsForGRN_Futures",
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
        public DataSet GetPurchaseOrderDetailsByPoidGRN(Int32 POId)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    ClsUtility.AddParameters("@Poid", SqlDbType.Int, POId.ToString());
                    return
                        (DataSet)
                        objPOdetails.ReturnObject(ClsUtility.theParams, "pr_SCM_GetPurchaseOrderGRNByPoid_Futures",
                                                  ClsDBUtility.ObjectEnum.DataSet);
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
        public DataSet GetOpenStock()
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject OpeningStock = new ClsObject();
                    return
                        (DataSet)
                        OpeningStock.ReturnObject(ClsUtility.theParams, "pr_SCM_GetOpeningStock_Futures",
                                                  ClsDBUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    DataMgr.RollBackTransation(this.Transaction);
                    throw;
                }
                finally
                {
                    if (this.Connection != null)
                        DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }
        public DataTable GetDuplicateBatchOpenStock(string batchname, DateTime ExpiryDate)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject OpeningStock = new ClsObject();
                    //ClsUtility.AddParameters("@ItemID", SqlDbType.Int, ItemId.ToString());
                    ClsUtility.AddParameters("@BatchName", SqlDbType.VarChar, batchname.ToString());
                    ClsUtility.AddParameters("@ExpiryDate", SqlDbType.VarChar, ExpiryDate.ToString("dd-MMM-yyyy"));
                    return
                        (DataTable)
                        OpeningStock.ReturnObject(ClsUtility.theParams, "pr_SCM_GetDuplicateBatchOpenStock_Futures",
                                                  ClsDBUtility.ObjectEnum.DataTable);
                }
                catch
                {
                    DataMgr.RollBackTransation(this.Transaction);
                    throw;
                }
                finally
                {
                    if (this.Connection != null)
                        DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }
        public int SaveUpdateOpeningStock(DataTable theDTOPStock, Int32 UserID, DateTime TransactionDate)
        {
            lock (this)
            {
                ClsObject StoreUserLnk = new ClsObject();
                int theRowAffected = 0;
                for (int i = 0; i < theDTOPStock.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ItemId", SqlDbType.Int, theDTOPStock.Rows[i]["ItemId"].ToString());
                    ClsUtility.AddParameters("@BatchId", SqlDbType.Int, theDTOPStock.Rows[i]["BatchId"].ToString());
                    ClsUtility.AddParameters("@StoreId", SqlDbType.Int, theDTOPStock.Rows[i]["StoreId"].ToString());
                    ClsUtility.AddParameters("@Quantity", SqlDbType.Int, theDTOPStock.Rows[i]["Quantity"].ToString());
                    ClsUtility.AddParameters("@ExpiryDate ", SqlDbType.VarChar,
                                             theDTOPStock.Rows[i]["ExpiryDate"].ToString());
                    ClsUtility.AddParameters("@UserId ", SqlDbType.Int, UserID.ToString());
                    ClsUtility.AddParameters("@TransactionDate", SqlDbType.VarChar, TransactionDate.ToString("dd-MMM-yyyy"));
                    theRowAffected =
                        (int)
                        StoreUserLnk.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveOpeningStock_Futures",
                                                  ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }

        }
        public int SaveUpdateStockAdjustment(DataTable theDTAdjustStock, int LocationId, int StoreId,
                                            string AdjustmentDate, int AdjustmentPreparedBy, int AdjustmentAuthorisedBy,
                                            int Updatestock, int UserID)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject ObjStoreAdjust = new ClsObject();
                int theRowAffected = 0;
                ClsUtility.Init_Hashtable();

                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                ClsUtility.AddParameters("@AdjustmentDate", SqlDbType.VarChar, AdjustmentDate.ToString());
                ClsUtility.AddParameters("@AdjustmentPreparedBy", SqlDbType.Int, AdjustmentPreparedBy.ToString());
                ClsUtility.AddParameters("@AdjustmentAuthorisedBy", SqlDbType.Int, AdjustmentAuthorisedBy.ToString());
                DataRow theDR = (DataRow)ObjStoreAdjust.ReturnObject(ClsUtility.theParams, "Pr_SCM_SaveStockOrdAdjust_Futures", ClsDBUtility.ObjectEnum.DataRow);

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                theRowAffected = (int)ObjStoreAdjust.ReturnObject(ClsUtility.theParams, "Pr_SCM_DeleteStockforAdjustment_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                for (int i = 0; i < theDTAdjustStock.Rows.Count; i++)
                 {
                     if (Convert.ToInt32(theDTAdjustStock.Rows[i]["AdjQty"]) > 0 || Convert.ToInt32(theDTAdjustStock.Rows[i]["AdjQty"]) < 0)
                     {
                         if (Updatestock == 1)
                         {
                             ClsUtility.Init_Hashtable();
                             ClsUtility.AddParameters("@Updatestock", SqlDbType.Int, Updatestock.ToString());
                             ClsUtility.AddParameters("@AjustmentId", SqlDbType.Int, theDR["AdjustId"].ToString());
                             ClsUtility.AddParameters("@ItemId", SqlDbType.Int,
                                                      theDTAdjustStock.Rows[i]["ItemId"].ToString());
                             ClsUtility.AddParameters("@BatchId", SqlDbType.Int,
                                                      theDTAdjustStock.Rows[i]["BatchId"].ToString());
                             ClsUtility.AddParameters("@ExpiryDate", SqlDbType.DateTime,
                                                      theDTAdjustStock.Rows[i]["ExpiryDate"].ToString());
                             ClsUtility.AddParameters("@StoreId", SqlDbType.Int,
                                                      theDTAdjustStock.Rows[i]["StoreId"].ToString());
                             ClsUtility.AddParameters("@AdjustmentQuantity", SqlDbType.Int,
                                                      theDTAdjustStock.Rows[i]["AdjQty"].ToString());
                             ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                             theRowAffected =
                                 (int)
                                 ObjStoreAdjust.ReturnObject(ClsUtility.theParams, "Pr_SCM_SaveStockTransAdjust_Futures",
                                                             ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                         }
                     }
                 }

                for (int i = 0; i < theDTAdjustStock.Rows.Count; i++)
                {
                    if (Convert.ToInt32(theDTAdjustStock.Rows[i]["AdjQty"]) > 0 || Convert.ToInt32(theDTAdjustStock.Rows[i]["AdjQty"]) < 0)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@UpdateStock", SqlDbType.Int, Updatestock.ToString());
                        ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                        ClsUtility.AddParameters("@AdjustmentPreparedBy", SqlDbType.Int, AdjustmentPreparedBy.ToString());
                        ClsUtility.AddParameters("@AdjustmentAuthorisedBy", SqlDbType.Int, AdjustmentAuthorisedBy.ToString());
                        ClsUtility.AddParameters("@AdjustmentDate", SqlDbType.DateTime, AdjustmentDate.ToString());
                        ClsUtility.AddParameters("@AdjustmentId", SqlDbType.Int, theDR["AdjustId"].ToString());
                        ClsUtility.AddParameters("@ItemId", SqlDbType.Int, theDTAdjustStock.Rows[i]["ItemId"].ToString());
                        ClsUtility.AddParameters("@BatchId", SqlDbType.Int,
                                                 theDTAdjustStock.Rows[i]["BatchId"].ToString());
                        ClsUtility.AddParameters("@ExpiryDate", SqlDbType.DateTime,
                                                 theDTAdjustStock.Rows[i]["ExpiryDate"].ToString());
                        ClsUtility.AddParameters("@StoreId", SqlDbType.Int,
                                                 theDTAdjustStock.Rows[i]["StoreId"].ToString());
                        ClsUtility.AddParameters("@PurchaseUnit", SqlDbType.Int,
                                                 theDTAdjustStock.Rows[i]["UnitId"].ToString());
                        ClsUtility.AddParameters("@AdjustReasonId ", SqlDbType.VarChar,
                                                 theDTAdjustStock.Rows[i]["AdjustReasonId"].ToString());
                        ClsUtility.AddParameters("@AdjustmentQuantity", SqlDbType.Int,
                                                 theDTAdjustStock.Rows[i]["AdjQty"].ToString());
                        ClsUtility.AddParameters("@UserId ", SqlDbType.Int, UserID.ToString());
                        theRowAffected =
                            (int)
                            ObjStoreAdjust.ReturnObject(ClsUtility.theParams, "Pr_SCM_SaveStockOrdAdjust_Futures",
                                                        ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                return theRowAffected;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public int SaveGoodreceivedNotes(DataTable DtMasterGRN, DataTable dtGRNItems, int IsPOorIST)
        {

            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject PODetail = new ClsObject();
                PODetail.Connection = this.Connection;
                PODetail.Transaction = this.Transaction;
                int theRowAffected = 0;
                int GrnId = 0;
                DataRow theDR;

                if (DtMasterGRN.Rows.Count > 0)
                {
                    if (!String.IsNullOrEmpty(Convert.ToString(DtMasterGRN.Rows[0]["GRNId"])))
                    {
                        if (Convert.ToInt32(DtMasterGRN.Rows[0]["GRNId"]) == 0)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@POId", SqlDbType.VarChar, DtMasterGRN.Rows[0]["POId"].ToString());
                            ClsUtility.AddParameters("@LocationID", SqlDbType.Int,
                                                     DtMasterGRN.Rows[0]["LocationID"].ToString());
                            ClsUtility.AddParameters("@RecievedStoreID", SqlDbType.Int,
                                                     DtMasterGRN.Rows[0]["DestinStoreID"].ToString());
                            ClsUtility.AddParameters("@Freight", SqlDbType.VarChar,
                                                     DtMasterGRN.Rows[0]["Freight"].ToString());
                            ClsUtility.AddParameters("@Tax", SqlDbType.Int, DtMasterGRN.Rows[0]["Tax"].ToString());
                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, DtMasterGRN.Rows[0]["UserID"].ToString());
                            theDR =
                                (DataRow)
                                PODetail.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveGRNMaster_Futures",
                                                      ClsDBUtility.ObjectEnum.DataRow);
                            GrnId = System.Convert.ToInt32(theDR[0].ToString());
                        }
                    }
                }

                if (GrnId == 0)
                {
                    GrnId = Convert.ToInt32(DtMasterGRN.Rows[0]["GRNId"]);
                }
                for (int i = 0; i < dtGRNItems.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    if (!String.IsNullOrEmpty(Convert.ToString(dtGRNItems.Rows[i]["GRNId"].ToString())))
                    {                        

                            ClsUtility.AddParameters("@GRNId", SqlDbType.Int, GrnId.ToString());
                            ClsUtility.AddParameters("@ItemId", SqlDbType.VarChar, dtGRNItems.Rows[i]["ItemId"].ToString());
                            //ClsUtility.AddParameters("@BatchID", SqlDbType.Int, dtGRNItems.Rows[i]["BatchID"].ToString());
                            ClsUtility.AddParameters("@batchName", SqlDbType.VarChar,
                                                     dtGRNItems.Rows[i]["batchName"].ToString());
                            ClsUtility.AddParameters("@RecievedQuantity", SqlDbType.Int,
                                                     dtGRNItems.Rows[i]["RecievedQuantity"].ToString());

                            ClsUtility.AddParameters("@FreeRecievedQuantity", SqlDbType.Int,
                                (Convert.ToString(dtGRNItems.Rows[i]["FreeRecievedQuantity"]) == "") ? "0" : dtGRNItems.Rows[i]["FreeRecievedQuantity"].ToString());

                            ClsUtility.AddParameters("@PurchasePrice", SqlDbType.Int,
                                                (Convert.ToString(dtGRNItems.Rows[i]["ItemPurchasePrice"]) == "") ? "0" : dtGRNItems.Rows[i]["ItemPurchasePrice"].ToString());
                            ClsUtility.AddParameters("@TotPurchasePrice", SqlDbType.Int,
                                                    (Convert.ToString(dtGRNItems.Rows[i]["TotPurchasePrice"]) == "") ? "0" : dtGRNItems.Rows[i]["TotPurchasePrice"].ToString());
                            ClsUtility.AddParameters("@SellingPrice", SqlDbType.Decimal,
                                                    (Convert.ToString(dtGRNItems.Rows[i]["SellingPrice"]) == "") ? "0" : dtGRNItems.Rows[i]["SellingPrice"].ToString());
                            ClsUtility.AddParameters("@SellingPricePerDispense", SqlDbType.Decimal,
                                                     (Convert.ToString(dtGRNItems.Rows[i]["SellingPricePerDispense"]) == "") ? "0" : dtGRNItems.Rows[i]["SellingPricePerDispense"].ToString());
                            ClsUtility.AddParameters("@ExpiryDate", SqlDbType.VarChar,
                                                     dtGRNItems.Rows[i]["ExpiryDate"].ToString());

                            ClsUtility.AddParameters("@UserID", SqlDbType.Int,
                                 (Convert.ToString(dtGRNItems.Rows[i]["UserID"]) == "") ? DtMasterGRN.Rows[0]["UserID"].ToString() : dtGRNItems.Rows[i]["UserID"].ToString());
                            ClsUtility.AddParameters("@IsPOorIST", SqlDbType.Int, IsPOorIST.ToString());
                            ClsUtility.AddParameters("@POId", SqlDbType.VarChar, dtGRNItems.Rows[i]["POId"].ToString());
                            ClsUtility.AddParameters("@Margin", SqlDbType.Decimal, dtGRNItems.Rows[i]["Margin"].ToString());
                            ClsUtility.AddParameters("@destinationStoreID", SqlDbType.Int,
                                                     dtGRNItems.Rows[i]["DestinStoreID"].ToString());
                            ClsUtility.AddParameters("@SourceStoreID", SqlDbType.Int,
                                                     dtGRNItems.Rows[i]["SourceStoreID"].ToString());
                            //ClsUtility.AddParameters("@InKindFlag", SqlDbType.Int,
                            //                         dtGRNItems.Rows[i]["InKindFlag"].ToString());
                            ClsUtility.AddParameters("@CustomPurchasePrice", SqlDbType.Decimal,
                                                    (Convert.ToString(dtGRNItems.Rows[i]["CustomPurchasePrice"]) == "") ? "0" : dtGRNItems.Rows[i]["CustomPurchasePrice"].ToString());
                            ClsUtility.AddParameters("@CustomMargin", SqlDbType.Decimal,
                                                    (Convert.ToString(dtGRNItems.Rows[i]["CustomMargin"]) == "") ? "0" : dtGRNItems.Rows[i]["CustomMargin"].ToString());
                            ClsUtility.AddParameters("@CustomSellingPrice", SqlDbType.Decimal,
                                                     (Convert.ToString(dtGRNItems.Rows[i]["CustomSellingPrice"]) == "") ? "0" : dtGRNItems.Rows[i]["CustomSellingPrice"].ToString());
                            if (Convert.ToInt32(dtGRNItems.Rows[i]["GRNId"].ToString()) == 0)
                            {
                            theRowAffected =
                                (int)
                                PODetail.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveGRNItems_Futures",
                                                      ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                            }
                        else
                            {
                                theRowAffected =
                                (int)
                                PODetail.ReturnObject(ClsUtility.theParams, "pr_SCM_UpdateGRNItems",
                                                      ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                            }
                    }
                }
            

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return GrnId;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }
        }

        public DataSet GetDisposeStock(int StoreId, DateTime AsofDate)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                    ClsUtility.AddParameters("@AsofDate", SqlDbType.VarChar, AsofDate.ToString("dd-MMM-yyyy"));

                    return
                        (DataSet)
                        objPOdetails.ReturnObject(ClsUtility.theParams, "pr_SCM_GetDisposeStock_Futures",
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
        public DataTable GetStoreSourceDestination(int StoreId)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject objPOdetails = new ClsObject();
                    ClsUtility.AddParameters("@SupplierId", SqlDbType.Int, StoreId.ToString());                    
                    return(DataTable)objPOdetails.ReturnObject(ClsUtility.theParams, "pr_SCM_GetStoreSourceDestination",
                                                  ClsDBUtility.ObjectEnum.DataTable);
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
        public int SaveDisposeItems(int StoreId, int LocationId, DateTime AsofDate, int UserId, DataTable theDT)
        {

            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject ObjStoreDispose = new ClsObject();
                int theRowAffected = 0;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                ClsUtility.AddParameters("@DisposeDate", SqlDbType.VarChar, AsofDate.ToString("dd-MMM-yyyy"));
                ClsUtility.AddParameters("@DisposePreparedBy", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@DisposeAuthorisedBy", SqlDbType.Int, UserId.ToString());
                DataRow theDR = (DataRow)ObjStoreDispose.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveDisposeItems_Futures", ClsDBUtility.ObjectEnum.DataRow);
                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    if (Convert.ToInt32(!DBNull.Value.Equals(theDT.Rows[i]["Dispose"])) == 1)
                    {
                        ClsUtility.AddParameters("@DisposeId", SqlDbType.Int, theDR["DisposeId"].ToString());
                        ClsUtility.AddParameters("@ItemId", SqlDbType.Int, theDT.Rows[i]["ItemId"].ToString());
                        ClsUtility.AddParameters("@BatchId", SqlDbType.Int, theDT.Rows[i]["BatchId"].ToString());
                        ClsUtility.AddParameters("@ExpiryDate", SqlDbType.VarChar,
                                                 theDT.Rows[i]["ExpiryDate"].ToString());
                        ClsUtility.AddParameters("@StoreId", SqlDbType.Int, theDT.Rows[i]["StoreId"].ToString());
                        ClsUtility.AddParameters("@Quantity", SqlDbType.Int, "-" + theDT.Rows[i]["Quantity"].ToString());
                        ClsUtility.AddParameters("@UserId ", SqlDbType.Int, UserId.ToString());
                        theRowAffected =
                            (int)
                            ObjStoreDispose.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveDisposeItems_Futures",
                                                         ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                }
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return theRowAffected;
            }


            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }

        }

        public DataSet GetStockforAdjustment(int StoreId, string AdjustmentDate)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject GetStockItem = new ClsObject();
                    ClsUtility.AddParameters("@StoreID", SqlDbType.Int, StoreId.ToString());
                    ClsUtility.AddParameters("@AdjustmentDate", SqlDbType.VarChar, AdjustmentDate.ToString());
                    return
                        (DataSet)
                        GetStockItem.ReturnObject(ClsUtility.theParams, "Pr_SCM_GetStockforAdjustment_Futures",
                                                  ClsDBUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    DataMgr.RollBackTransation(this.Transaction);
                    throw;
                }
                finally
                {
                    if (this.Connection != null)
                        DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }

        public DataSet GetOpenStockWeb()
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject OpeningStock = new ClsObject();
                    return
                        (DataSet)
                        OpeningStock.ReturnObject(ClsUtility.theParams, "pr_SCM_GetOpeningStock_Web",
                                                  ClsDBUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    DataMgr.RollBackTransation(this.Transaction);
                    throw;
                }
                finally
                {
                    if (this.Connection != null)
                        DataMgr.ReleaseConnection(this.Connection);
                }
            }
        }

        public int SaveGoodreceivedNotes_Web(DataTable DtMasterGRN, DataTable dtGRNItems, int IsPOorIST)
        {

            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject PODetail = new ClsObject();
                PODetail.Connection = this.Connection;
                PODetail.Transaction = this.Transaction;
                int theRowAffected = 0;
                int GrnId = 0;
                DataRow theDR;

                if (DtMasterGRN.Rows.Count > 0)
                {
                    //if (!String.IsNullOrEmpty(Convert.ToString(DtMasterGRN.Rows[0]["GRNId"])))
                    //{
                    if (Convert.ToInt32(DtMasterGRN.Rows[0]["GRNId"]) == 0)
                    {
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddParameters("@POId", SqlDbType.VarChar, DtMasterGRN.Rows[0]["POId"].ToString());
                        ClsUtility.AddParameters("@LocationID", SqlDbType.Int, DtMasterGRN.Rows[0]["LocationID"].ToString());
                        ClsUtility.AddParameters("@RecievedStoreID", SqlDbType.Int, DtMasterGRN.Rows[0]["DestinStoreID"].ToString());
                        ClsUtility.AddParameters("@Freight", SqlDbType.VarChar, DtMasterGRN.Rows[0]["Freight"].ToString());
                        ClsUtility.AddParameters("@Tax", SqlDbType.Int, DtMasterGRN.Rows[0]["Tax"].ToString());
                        ClsUtility.AddParameters("@UserID", SqlDbType.Int, DtMasterGRN.Rows[0]["UserID"].ToString());
                        theDR = (DataRow)PODetail.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveGRNMaster_Web", ClsDBUtility.ObjectEnum.DataRow);
                        GrnId = System.Convert.ToInt32(theDR[0].ToString());
                    }
                    //}
                }

                if (GrnId == 0)
                {
                    GrnId = Convert.ToInt32(DtMasterGRN.Rows[0]["GRNId"]);
                }
                for (int i = 0; i < dtGRNItems.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    //if (!String.IsNullOrEmpty(Convert.ToString(dtGRNItems.Rows[i]["GRNId"].ToString())))
                    //{
                    //if (Convert.ToInt32(dtGRNItems.Rows[i]["GRNId"].ToString()) == 0)
                    //{

                    ClsUtility.AddParameters("@GRNId", SqlDbType.Int, GrnId.ToString());
                    ClsUtility.AddParameters("@ItemId", SqlDbType.VarChar, dtGRNItems.Rows[i]["ItemId"].ToString());
                    ClsUtility.AddParameters("@BatchID", SqlDbType.Int, dtGRNItems.Rows[i]["BatchID"].ToString());
                    ClsUtility.AddParameters("@batchName", SqlDbType.VarChar, dtGRNItems.Rows[i]["batchName"].ToString());
                    ClsUtility.AddParameters("@RecievedQuantity", SqlDbType.Int, dtGRNItems.Rows[i]["RecievedQuantity"].ToString());

                    ClsUtility.AddParameters("@FreeRecievedQuantity", SqlDbType.Int, (Convert.ToString(dtGRNItems.Rows[i]["FreeRecievedQuantity"]) == "") ? "0" : dtGRNItems.Rows[i]["FreeRecievedQuantity"].ToString());

                    ClsUtility.AddParameters("@PurchasePrice", SqlDbType.Int,
                                        (Convert.ToString(dtGRNItems.Rows[i]["ItemPurchasePrice"]) == "") ? "0" : dtGRNItems.Rows[i]["ItemPurchasePrice"].ToString());
                    ClsUtility.AddParameters("@TotPurchasePrice", SqlDbType.Int,
                                            (Convert.ToString(dtGRNItems.Rows[i]["TotPurchasePrice"]) == "") ? "0" : dtGRNItems.Rows[i]["TotPurchasePrice"].ToString());
                    ClsUtility.AddParameters("@SellingPrice", SqlDbType.Decimal,
                                            (Convert.ToString(dtGRNItems.Rows[i]["SellingPrice"]) == "") ? "0" : dtGRNItems.Rows[i]["SellingPrice"].ToString());
                    ClsUtility.AddParameters("@SellingPricePerDispense", SqlDbType.Decimal,
                                             (Convert.ToString(dtGRNItems.Rows[i]["SellingPricePerDispense"]) == "") ? "0" : dtGRNItems.Rows[i]["SellingPricePerDispense"].ToString());
                    ClsUtility.AddParameters("@ExpiryDate", SqlDbType.DateTime, dtGRNItems.Rows[i]["ExpiryDate"].ToString());

                    ClsUtility.AddParameters("@UserID", SqlDbType.Int,
                         (Convert.ToString(dtGRNItems.Rows[i]["UserID"]) == "") ? DtMasterGRN.Rows[0]["UserID"].ToString() : dtGRNItems.Rows[i]["UserID"].ToString());
                    ClsUtility.AddParameters("@IsPOorIST", SqlDbType.Int, IsPOorIST.ToString());
                    ClsUtility.AddParameters("@POId", SqlDbType.VarChar, dtGRNItems.Rows[i]["POId"].ToString());
                    //oUtility.AddParameters("@Margin", SqlDbType.Decimal, dtGRNItems.Rows[i]["Margin"].ToString());
                    ClsUtility.AddParameters("@destinationStoreID", SqlDbType.Int, dtGRNItems.Rows[i]["DestinStoreID"].ToString());
                    ClsUtility.AddParameters("@SourceStoreID", SqlDbType.Int, dtGRNItems.Rows[i]["SourceStoreID"].ToString());
                    ClsUtility.AddParameters("@comments", SqlDbType.Int, dtGRNItems.Rows[i]["Comments"].ToString());
                    //oUtility.AddParameters("@InKindFlag", SqlDbType.Int,
                    //                         dtGRNItems.Rows[i]["InKindFlag"].ToString());


                    theRowAffected =
                        (int)
                        PODetail.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveGRNItems_Web",
                                              ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    //}
                    //}
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return GrnId;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }
        }
        public int SavePurchaseOrderWeb(DataTable DtMasterPO, DataTable dtPOItems, bool isUpdate)
        {

            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);
                ClsObject PODetail = new ClsObject();
                PODetail.Connection = this.Connection;
                PODetail.Transaction = this.Transaction;
                int theRowAffected = 0;
                int POID = 0;
                DataRow theDR;

                ClsUtility.Init_Hashtable();

                ClsUtility.AddParameters("@LocationID", SqlDbType.VarChar, DtMasterPO.Rows[0]["LocationID"].ToString());
                ClsUtility.AddParameters("@SupplierID", SqlDbType.Int, DtMasterPO.Rows[0]["SupplierID"].ToString());
                ClsUtility.AddParameters("@OrderDate", SqlDbType.Int, DtMasterPO.Rows[0]["OrderDate"].ToString());
                ClsUtility.AddParameters("@PreparedBy", SqlDbType.VarChar, DtMasterPO.Rows[0]["PreparedBy"].ToString());
                ClsUtility.AddParameters("@SourceStoreID", SqlDbType.Int, DtMasterPO.Rows[0]["SrcStore"].ToString());
                ClsUtility.AddParameters("@DestinStoreID", SqlDbType.Int, DtMasterPO.Rows[0]["DestStore"].ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, DtMasterPO.Rows[0]["UserID"].ToString());
                ClsUtility.AddParameters("@AuthorizedBy", SqlDbType.Int, DtMasterPO.Rows[0]["AthorizedBy"].ToString());
                if (isUpdate)
                {
                    ClsUtility.AddParameters("@Poid", SqlDbType.Int, DtMasterPO.Rows[0]["POID"].ToString());
                    ClsUtility.AddParameters("@IsUpdate", SqlDbType.Bit, isUpdate.ToString());

                    if (Convert.ToString(DtMasterPO.Rows[0]["IsRejectedStatus"]) == "1")
                    {
                        ClsUtility.AddParameters("@Status", SqlDbType.Int, "5");
                    }
                    else
                    {
                        if (Convert.ToString(DtMasterPO.Rows[0]["AthorizedBy"]) == "0")
                        {
                            ClsUtility.AddParameters("@Status", SqlDbType.Int, "1");
                        }
                        else
                        {
                            ClsUtility.AddParameters("@Status", SqlDbType.Int, "2");
                        }
                    }
                }
                else
                {
                    if (Convert.ToString(DtMasterPO.Rows[0]["AthorizedBy"]) == "0")
                    {
                        ClsUtility.AddParameters("@Status", SqlDbType.Int, "1");
                    }
                    else
                    {
                        ClsUtility.AddParameters("@Status", SqlDbType.Int, "2");
                    }
                }

                theDR =
                    (DataRow)
                    PODetail.ReturnObject(ClsUtility.theParams, "pr_SCM_SavePurchaseOrderMaster_Web",
                                          ClsDBUtility.ObjectEnum.DataRow);
                POID = System.Convert.ToInt32(theDR[0].ToString());

                if (isUpdate)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@POId", SqlDbType.Int, POID.ToString());
                    theRowAffected =
                        (int)
                        PODetail.ReturnObject(ClsUtility.theParams, "pr_SCM_DeletePurchaseOrderItem_Futures",
                                              ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }

                for (int i = 0; i < dtPOItems.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@POId", SqlDbType.Int, POID.ToString());
                    ClsUtility.AddParameters("@ItemId", SqlDbType.VarChar, dtPOItems.Rows[i]["ItemId"].ToString());
                    ClsUtility.AddParameters("@Quantity", SqlDbType.Int, dtPOItems.Rows[i]["Quantity"].ToString());
                    ClsUtility.AddParameters("@PurchasePrice", SqlDbType.Decimal,
                                             dtPOItems.Rows[i]["priceperunit"].ToString());
                    //  oUtility.AddParameters("@Unit", SqlDbType.Int,dtPOItems.Rows[i]["Units"].ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, DtMasterPO.Rows[0]["UserID"].ToString());

                    ClsUtility.AddParameters("@BatchID", SqlDbType.Int, dtPOItems.Rows[i]["BatchID"].ToString());
                    ClsUtility.AddParameters("@AvaliableQty", SqlDbType.Int, dtPOItems.Rows[i]["AvaliableQty"].ToString());
                    ClsUtility.AddParameters("@ExpiryDate", SqlDbType.Int, dtPOItems.Rows[i]["ExpiryDate"].ToString());
                    ClsUtility.AddParameters("@UnitQuantity", SqlDbType.Int, dtPOItems.Rows[i]["UnitQuantity"].ToString());
                    theRowAffected =
                        (int)
                        PODetail.ReturnObject(ClsUtility.theParams, "pr_SCM_SavePurchaseOrderItem_Web",
                                              ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }

                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);
                return POID;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);

            }
        }

        public int SaveUpdateStockAdjustmentWeb(DataTable theDTAdjustStock, int LocationId, int StoreId,
                                            string AdjustmentDate, int AdjustmentPreparedBy, int AdjustmentAuthorisedBy,
                                            int Updatestock, int UserID)
        {
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                ClsObject ObjStoreAdjust = new ClsObject();
                int theRowAffected = 0;
                ClsUtility.Init_Hashtable();

                ClsUtility.AddParameters("@AdjustmentId", SqlDbType.Int, "0");
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
                ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                ClsUtility.AddParameters("@AdjustmentDate", SqlDbType.VarChar, AdjustmentDate.ToString());
                ClsUtility.AddParameters("@AdjustmentPreparedBy", SqlDbType.Int, AdjustmentPreparedBy.ToString());
                ClsUtility.AddParameters("@AdjustmentAuthorisedBy", SqlDbType.Int, AdjustmentAuthorisedBy.ToString());
                DataRow theDR = (DataRow)ObjStoreAdjust.ReturnObject(ClsUtility.theParams, "Pr_SCM_SaveStockOrdAdjust_Web", ClsDBUtility.ObjectEnum.DataRow);

                for (int i = 0; i < theDTAdjustStock.Rows.Count; i++)
                {
                    if (Convert.ToInt32(theDTAdjustStock.Rows[i]["AdjQty"]) > 0 || Convert.ToInt32(theDTAdjustStock.Rows[i]["AdjQty"]) < 0)
                    {
                        if (Updatestock == 1)
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@Updatestock", SqlDbType.Int, Updatestock.ToString());
                            ClsUtility.AddParameters("@AdjustmentId", SqlDbType.Int, theDR["AdjustId"].ToString());
                            ClsUtility.AddParameters("@ItemId", SqlDbType.Int, theDTAdjustStock.Rows[i]["ItemId"].ToString());
                            ClsUtility.AddParameters("@BatchId", SqlDbType.Int, theDTAdjustStock.Rows[i]["BatchId"].ToString());
                            ClsUtility.AddParameters("@ExpiryDate", SqlDbType.DateTime, theDTAdjustStock.Rows[i]["ExpiryDate"].ToString());
                            ClsUtility.AddParameters("@StoreId", SqlDbType.Int, theDTAdjustStock.Rows[i]["StoreId"].ToString());
                            ClsUtility.AddParameters("@AdjustmentQuantity", SqlDbType.Int, theDTAdjustStock.Rows[i]["AdjQty"].ToString());
                            ClsUtility.AddParameters("@comments", SqlDbType.Int, theDTAdjustStock.Rows[i]["Comments"].ToString());
                            ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                            theRowAffected = (int)ObjStoreAdjust.ReturnObject(ClsUtility.theParams, "Pr_SCM_SaveStockforAdjustment_Web", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                        }
                    }
                }

                
                return theRowAffected;
            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
        }

        public int SaveUpdateOpeningStockWeb(DataTable theDTOPStock, Int32 UserID, string TransactionDate)
        {
            lock (this)
            {
                ClsObject StoreUserLnk = new ClsObject();
                int theRowAffected = 0;
                for (int i = 0; i < theDTOPStock.Rows.Count; i++)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ItemId", SqlDbType.Int, theDTOPStock.Rows[i]["ItemId"].ToString());
                    ClsUtility.AddParameters("@BatchNo", SqlDbType.VarChar, theDTOPStock.Rows[i]["BatchNo"].ToString());
                    ClsUtility.AddParameters("@StoreId", SqlDbType.Int, theDTOPStock.Rows[i]["StoreId"].ToString());
                    ClsUtility.AddParameters("@Quantity", SqlDbType.Int, theDTOPStock.Rows[i]["Quantity"].ToString());
                    ClsUtility.AddParameters("@ExpiryDate ", SqlDbType.VarChar, theDTOPStock.Rows[i]["ExpiryDate"].ToString());
                    ClsUtility.AddParameters("@UserId ", SqlDbType.Int, UserID.ToString());
                    ClsUtility.AddParameters("@TransactionDate", SqlDbType.VarChar, TransactionDate.ToString());
                    theRowAffected = (int)StoreUserLnk.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveOpeningStock_Web", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }

        }



    }

}
