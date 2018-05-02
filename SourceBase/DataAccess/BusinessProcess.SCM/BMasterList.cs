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

    public class BMasterList : ProcessBase, IMasterList
    {
        #region "Constructor"

        public BMasterList()
        {
        }

        #endregion

        public DataSet GetProgramList()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject Programlist = new ClsObject();
                return
                    (DataSet)
                    Programlist.ReturnObject(ClsUtility.theParams, "pr_SCM_GetProgramList_Futures",
                                             ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet SaveBatchName(string BatchName, int UserId, string itemID, string expiryDatetime)
        {
            lock (this)
            {
                ClsObject BatchNameMgr = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@BatchName", SqlDbType.VarChar, BatchName.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                ClsUtility.AddParameters("@ItemID", SqlDbType.VarChar, itemID.ToString());
                ClsUtility.AddParameters("@ExpiryDatetime", SqlDbType.VarChar, expiryDatetime.ToString());

                return
                    (DataSet)
                    BatchNameMgr.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveBatchFromOpnStock_Futures",
                                              ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveProgramList(DataTable dtProgramList, int UserID)
        {
            lock (this)
            {
                ClsObject ProgramList = new ClsObject();
                int Rec = 0;

                int theRowAffected = 0;

                for (int i = 0; i <= dtProgramList.Rows.Count - 1; i++)
                {
                    Rec = Rec + 1;
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Id", SqlDbType.Int, dtProgramList.Rows[i]["id"].ToString());
                    ClsUtility.AddParameters("@ProgramId", SqlDbType.VarChar, dtProgramList.Rows[i]["ProgramId"].ToString());
                    ClsUtility.AddParameters("@ProgramName", SqlDbType.VarChar,
                                             dtProgramList.Rows[i]["ProgramName"].ToString());
                    ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dtProgramList.Rows[i]["Status"].ToString());
                    ClsUtility.AddParameters("@FiscalYearMonth", SqlDbType.Int,
                                             dtProgramList.Rows[i]["FiscalYearMonth"].ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    theRowAffected =
                        (int)
                        ProgramList.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveProgramMaster_Futures",
                                                 ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        public int SaveUpdateItemList(DataTable dtItemList, string CategoryID, string TableName, int UserID)
        {
            lock (this)
            {
                ClsObject ItemList = new ClsObject();
                int theRowAffected = 0;

                foreach (DataRow theDR in dtItemList.Rows)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["id"].ToString());
                    ClsUtility.AddParameters("@Name", SqlDbType.VarChar, theDR["Name"].ToString());
                    ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, theDR["Status"].ToString());
                    ClsUtility.AddParameters("@SRNo", SqlDbType.Int, theDR["SRNo"].ToString());
                    ClsUtility.AddParameters("@CategoryID", SqlDbType.Int, CategoryID.ToString());
                    ClsUtility.AddParameters("@TableName", SqlDbType.Int, TableName.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    theRowAffected =
                        (int)
                        ItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveUpdateItemMasterList_Futures",
                                              ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        public DataSet GetSupplierList()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject SupplierList = new ClsObject();
                return
                    (DataSet)
                    SupplierList.ReturnObject(ClsUtility.theParams, "pr_SCM_GetSupplierList_Futures",
                                              ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveSupplierList(DataTable dtSupplierList, int UserID)
        {
            lock (this)
            {
                ClsObject SupplierList = new ClsObject();
                int Rec = 0;

                int theRowAffected = 0;

                for (int i = 0; i <= dtSupplierList.Rows.Count - 1; i++)
                {
                    Rec = Rec + 1;
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Id", SqlDbType.Int, dtSupplierList.Rows[i]["id"].ToString());
                    ClsUtility.AddParameters("@SupplierId", SqlDbType.VarChar,
                                             dtSupplierList.Rows[i]["SupplierId"].ToString());
                    ClsUtility.AddParameters("@SupplierName", SqlDbType.VarChar,
                                             dtSupplierList.Rows[i]["SupplierName"].ToString());
                    ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dtSupplierList.Rows[i]["Status"].ToString());
                    ClsUtility.AddParameters("@Address", SqlDbType.VarChar, dtSupplierList.Rows[i]["Address"].ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    theRowAffected =
                        (int)
                        SupplierList.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveSupplierMaster_Futures",
                                                  ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        public DataTable GetItemType()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ItemType = new ClsObject();
                return
                    (DataTable)
                    ItemType.ReturnObject(ClsUtility.theParams, "[pr_SCM_GetItemType_Futures]",
                                          ClsDBUtility.ObjectEnum.DataTable);
            }
        }

        public DataTable GetSubItemType()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ItemType = new ClsObject();
                return
                    (DataTable)
                    ItemType.ReturnObject(ClsUtility.theParams, "[pr_SCM_GetSubItemType_Futures]",
                                          ClsDBUtility.ObjectEnum.DataTable);
            }
        }

        public DataTable GetFilteredSubItemType(int ItemTypeId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ItemTypeId", SqlDbType.Int, ItemTypeId.ToString());
                ClsObject ItemType = new ClsObject();
                return
                    (DataTable)
                    ItemType.ReturnObject(ClsUtility.theParams, "[pr_SCM_GetSubItemTypefromItemTpye]",
                                          ClsDBUtility.ObjectEnum.DataTable);
            }
        }

        public DataSet GetDrugType(int itemTypeId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itemTypeId.ToString());
                ClsObject DrugList = new ClsObject();
                return
                    (DataSet)
                    DrugList.ReturnObject(ClsUtility.theParams, "[pr_SCM_GetDrugType_Futures]",
                                          ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetDrugTypeIQMGT(int itemTypeId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itemTypeId.ToString());
                ClsObject DrugList = new ClsObject();
                return
                    (DataSet)
                    DrugList.ReturnObject(ClsUtility.theParams, "[pr_IQMgtGetSubItemTypes]",
                                          ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetItemList(int itemTypeId, int Subtypeid, int programId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ItemList = new ClsObject();
                ClsUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itemTypeId.ToString());
                ClsUtility.AddParameters("@SubitemId", SqlDbType.Int, Subtypeid.ToString());
                ClsUtility.AddParameters("@programId", SqlDbType.Int, programId.ToString());
                return
                    (DataSet)
                    ItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_GetItemList_Futures", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetItemList(int Subtypeid)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ItemList = new ClsObject();
                ClsUtility.AddParameters("@SubitemId", SqlDbType.Int, Subtypeid.ToString());
                return
                    (DataSet)
                    ItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_GetDrugList_Futures", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetItemListSupplier(int itemTypeId, int Subtypeid, int SupplierId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ItemList = new ClsObject();
                ClsUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itemTypeId.ToString());
                ClsUtility.AddParameters("@SubitemId", SqlDbType.Int, Subtypeid.ToString());
                ClsUtility.AddParameters("@SupplierId", SqlDbType.Int, SupplierId.ToString());
                return
                    (DataSet)
                    ItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_GetItemListSupplier_Futures",
                                          ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetItemListStore_Filtered(int itemTypeId, int Subtypeid, int StoreId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ItemList = new ClsObject();
                ClsUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itemTypeId.ToString());
                ClsUtility.AddParameters("@SubitemId", SqlDbType.Int, Subtypeid.ToString());
                ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                return
                    (DataSet)
                    ItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_GetItemListStoreFiltered_Futures",
                                          ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetItemListStore(int StoreId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ItemList = new ClsObject();
                ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                return
                    (DataSet)
                    ItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_GetItemListStore_Futures",
                                          ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataTable GetCommonItemList(String CategoryId, String TableName)
        {
            lock (this)
            {
                ClsObject ObjCommonItemList = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@CategoryId", SqlDbType.VarChar, CategoryId.ToString());
                ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName.ToString());
                return
                    (DataTable)
                    ObjCommonItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_GetCommonItemList_Futures",
                                                   ClsDBUtility.ObjectEnum.DataTable);
            }
        }
        public DataTable GetItemTypeList()
        {
            lock (this)
            {
                ClsObject ObjCommonItemList = new ClsObject();
                ClsUtility.Init_Hashtable();                
                return
                    (DataTable)
                    ObjCommonItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_GetItemTypeList",
                                                   ClsDBUtility.ObjectEnum.DataTable);
            }
        }

        public int SaveSubItemList(ArrayList dtSubitemList, int itemID, int UserID)
        {
            lock (this)
            {
                ClsObject subItemList = new ClsObject();
                int Rec = 0;
                int theRowAffected = 0;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itemID.ToString());
                theRowAffected =
                    (int)
                    subItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_DeleteItemdrugType_Futures",
                                             ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                for (int i = 0; i <= dtSubitemList.Count - 1; i++)
                {
                    
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@DrugTypeId", SqlDbType.Int, dtSubitemList[i].ToString());
                    ClsUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itemID.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    theRowAffected =
                        (int)
                        subItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveItemdrugType_Futures",
                                                 ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    Rec = Rec + theRowAffected;
                }
                return Rec;
            }
        }

        public int SaveItemList(DataTable dtItemList, int itematypeID, int UserID, int ProgramID)
        {
            lock (this)
            {
                ClsObject objItemList = new ClsObject();
                int Rec = 0;
                int theRowAffected = 0;
                //ClsUtility.Init_Hashtable();
                //ClsUtility.AddParameters("@ProgramId", SqlDbType.Int, ProgramID.ToString());
                //ClsUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itematypeID.ToString());
                //theRowAffected =
                //    (int)
                //    objItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_DeleteItemList_Futures",
                //                             ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                for (int i = 0; i < dtItemList.Rows.Count; i++)
                {
                    Rec = Rec + 1;
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ItemId", SqlDbType.Int, dtItemList.Rows[i]["ItemID"].ToString());
                    ClsUtility.AddParameters("@ProgramId", SqlDbType.Int, ProgramID.ToString());
                    ClsUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itematypeID.ToString());
                    //  ClsUtility.AddParameters("@DrugGeneric", SqlDbType.Int, dtItemList.Rows[i]["DrugGeneric"].ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    ClsUtility.AddParameters("@Checked", SqlDbType.Int, dtItemList.Rows[i]["Checked"].ToString());
                    theRowAffected =
                        (int)
                        objItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveItemList_Futures",
                                                 ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                return Rec;
            }
        }

        public int SaveSupplierItemList(DataTable dtItemList, int itematypeID, int UserID, int supplierID)
        {
            lock (this)
            {
                ClsObject objItemList = new ClsObject();
                int Rec = 0;
                int theRowAffected = 0;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@SupplierId", SqlDbType.Int, supplierID.ToString());
                ClsUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itematypeID.ToString());
                theRowAffected =
                    (int)
                    objItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_DeletesupplierItemList_Futures",
                                             ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                for (int i = 0; i <= dtItemList.Rows.Count - 1; i++)
                {
                    Rec = Rec + 1;
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ItemId", SqlDbType.Int, dtItemList.Rows[i]["ItemID"].ToString());
                    ClsUtility.AddParameters("@SupplierId", SqlDbType.Int, supplierID.ToString());
                    ClsUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itematypeID.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    theRowAffected =
                        (int)
                        objItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveSupplierItemList_Futures",
                                                 ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        public int SaveStoreItemList_Filtered(DataTable dtItemList, int UserID, int StoreID, int itemtypeID)
        {
            lock (this)
            {
                ClsObject objItemList = new ClsObject();
                int Rec = 0;
                int theRowAffected = 0;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@storeId", SqlDbType.Int, StoreID.ToString());
                ClsUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itemtypeID.ToString());
                theRowAffected =
                    (int)
                    objItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_DeleteStoreItemList_Futures",
                                             ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                for (int i = 0; i <= dtItemList.Rows.Count - 1; i++)
                {
                    Rec = Rec + 1;
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ItemId", SqlDbType.Int, dtItemList.Rows[i]["ItemID"].ToString());
                    ClsUtility.AddParameters("@storeId", SqlDbType.Int, StoreID.ToString());
                    ClsUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itemtypeID.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    theRowAffected =
                        (int)
                        objItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveItemListStore_Futures",
                                                 ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        public int SaveStoreItemList(DataTable dtItemList, int UserID, int StoreID)
        {
            lock (this)
            {
                ClsObject objItemList = new ClsObject();
                int Rec = 0;
                int theRowAffected = 0;
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@storeId", SqlDbType.Int, StoreID.ToString());
                theRowAffected =
                    (int)
                    objItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_DeleteStoreItemList_Futures",
                                             ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                for (int i = 0; i <= dtItemList.Rows.Count - 1; i++)
                {
                    Rec = Rec + 1;
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ItemId", SqlDbType.Int, dtItemList.Rows[i]["ItemID"].ToString());
                    ClsUtility.AddParameters("@storeId", SqlDbType.Int, StoreID.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    theRowAffected =
                        (int)
                        objItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveItemListStore_Futures",
                                                 ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        public DataSet GetDonorList()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject DonorList = new ClsObject();
                return
                    (DataSet)
                    DonorList.ReturnObject(ClsUtility.theParams, "pr_SCM_GetDonorList_Futures",
                                           ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveDonorList(DataTable dtDonorList, int UserID)
        {
            lock (this)
            {
                ClsObject DonorList = new ClsObject();
                int Rec = 0;

                int theRowAffected = 0;

                for (int i = 0; i <= dtDonorList.Rows.Count - 1; i++)
                {
                    Rec = Rec + 1;
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Id", SqlDbType.Int, dtDonorList.Rows[i]["id"].ToString());
                    ClsUtility.AddParameters("@DonorId", SqlDbType.VarChar, dtDonorList.Rows[i]["DonorId"].ToString());
                    ClsUtility.AddParameters("@DonorName", SqlDbType.VarChar, dtDonorList.Rows[i]["DonorName"].ToString());
                    ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dtDonorList.Rows[i]["Status"].ToString());
                    ClsUtility.AddParameters("@Donorshortname", SqlDbType.VarChar,
                                             dtDonorList.Rows[i]["Donorshortname"].ToString());
                    ClsUtility.AddParameters("@Srno", SqlDbType.Int, dtDonorList.Rows[i]["Srno"].ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    theRowAffected =
                        (int)
                        DonorList.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveDonorMaster_Futures",
                                               ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        public DataSet GetProgramDonorLnk()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ProgramDonorLnk = new ClsObject();
                return
                    (DataSet)
                    ProgramDonorLnk.ReturnObject(ClsUtility.theParams, "pr_SCM_GetDonorProgramLinking_Futures",
                                                 ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetItemMasterListing()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ItemMasterManager = new ClsObject();
                return
                    (DataSet)
                    ItemMasterManager.ReturnObject(ClsUtility.theParams, "Pr_SCM_GetItemListing_Futures",
                                                   ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveUpdateStore(DataTable dtItemList, string CategoryID, string TableName, int UserID)
        {
            lock (this)
            {
                ClsObject ItemList = new ClsObject();
                int theRowAffected = 0;
                foreach (DataRow theDR in dtItemList.Rows)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Id", SqlDbType.Int, theDR["id"].ToString());
                    ClsUtility.AddParameters("@StoreId", SqlDbType.VarChar, theDR["StoreId"].ToString());
                    ClsUtility.AddParameters("@Name", SqlDbType.VarChar, theDR["Name"].ToString());
                    ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, theDR["Status"].ToString());
                    ClsUtility.AddParameters("@CentralStore", SqlDbType.Int, theDR["CentralStore"].ToString());
                    ClsUtility.AddParameters("@DispensingStore", SqlDbType.VarChar, theDR["DispensingStore"].ToString());
                    ClsUtility.AddParameters("@SRNo", SqlDbType.Int, theDR["SRNo"].ToString());
                    ClsUtility.AddParameters("@CategoryID", SqlDbType.Int, CategoryID.ToString());
                    ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    theRowAffected =
                        (int)
                        ItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveUpdateItemMasterList_Futures",
                                              ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        public int SaveProgramDonorLnk(DataTable dtProgramDonorLnk, int UserID)
        {
            lock (this)
            {
                ClsObject ProgramDonorLnk = new ClsObject();
                int Rec = 0;

                int theRowAffected = 0;

                for (int i = 0; i <= dtProgramDonorLnk.Rows.Count - 1; i++)
                {
                    Rec = Rec + 1;
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@DonorId", SqlDbType.Int, dtProgramDonorLnk.Rows[i]["DonorId"].ToString());
                    ClsUtility.AddParameters("@ProgramId", SqlDbType.Int, dtProgramDonorLnk.Rows[i]["ProgramId"].ToString());
                    ClsUtility.AddParameters("@FundingStartDate", SqlDbType.VarChar, dtProgramDonorLnk.Rows[i]["FundingStartDate"].ToString());
                    ClsUtility.AddParameters("@FundingEndDate", SqlDbType.VarChar, dtProgramDonorLnk.Rows[i]["FundingEndDate"].ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    if (Rec == 1)
                        ClsUtility.AddParameters("@Delete", SqlDbType.Int, "1");
                    theRowAffected = (int)ProgramDonorLnk.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveProgramDonorlnk_Futures", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        public DataSet GetStoreDetail()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject StoreMasterManager = new ClsObject();
                return
                    (DataSet)
                    StoreMasterManager.ReturnObject(ClsUtility.theParams, "Pr_SCM_GetStoreDetails_Futures",
                                                    ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetItemDetails(int theItemId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ItemId", SqlDbType.Int, theItemId.ToString());
                ClsObject ItemManager = new ClsObject();
                return
                    (DataSet)
                    ItemManager.ReturnObject(ClsUtility.theParams, "pr_SCM_GetItemDetails_Futures",
                                             ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        

        public int SaveUpdateStoreLinking(DataTable dtStoreList, string TableName, int UserID)
        {
            lock (this)
            {
                ClsObject ItemList = new ClsObject();
                int theRowAffected = 0;
                ClsUtility.Init_Hashtable();
                StringBuilder theSB = new StringBuilder();
                theSB.Append("Delete from " + TableName + " ");
                foreach (DataRow theDR in dtStoreList.Rows)
                {
                    theSB.Append("Insert into " + TableName +
                                 "(SourceStore, DestinationStore, UserId, CreateDate, UpdateDate,SupplierFlag) ");
                    theSB.Append("values (" + theDR["SourceStore"].ToString() + "," + theDR["DestinationStore"].ToString() +
                                 "," + UserID + ", getdate(), getdate()," + theDR["SupplierFlag"].ToString() + ")");
                }
                ClsUtility.AddParameters("@Str", SqlDbType.VarChar, theSB.ToString());
                ClsUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName.ToString());
                theRowAffected =
                    (int)
                    ItemList.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveUpdateItemMasterList_Futures",
                                          ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                return theRowAffected;
            }
        }

        public int SaveUpdateItemMaster(Hashtable theHash)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Drug_Pk", SqlDbType.Int, theHash["Drug_Pk"].ToString());
                ClsUtility.AddParameters("@ItemCode", SqlDbType.VarChar, theHash["ItemCode"].ToString());
                ClsUtility.AddParameters("@FDACode", SqlDbType.VarChar, theHash["FDACode"].ToString());
                ClsUtility.AddParameters("@DispensingUnit", SqlDbType.Int, theHash["DispensingUnit"].ToString());
                ClsUtility.AddParameters("@PurchaseUnit", SqlDbType.Int, theHash["PurchaseUnit"].ToString());
                ClsUtility.AddParameters("@PurchaseUnitQty", SqlDbType.Int, theHash["PurchaseUnitQty"].ToString());
                ClsUtility.AddParameters("@PurchaseUnitPrice", SqlDbType.Decimal, theHash["PurchaseUnitPrice"].ToString());
                ClsUtility.AddParameters("@Manufacturer", SqlDbType.Int, theHash["Manufacturer"].ToString());
                ClsUtility.AddParameters("@DispensingUnitPrice", SqlDbType.Decimal, theHash["DispensingUnitPrice"].ToString());
                ClsUtility.AddParameters("@DispensingMargin", SqlDbType.Decimal, theHash["DispensingMargin"].ToString());
                ClsUtility.AddParameters("@SellingPrice", SqlDbType.Decimal, theHash["SellingPrice"].ToString());
                ClsUtility.AddParameters("@EffectiveDate", SqlDbType.VarChar, theHash["EffectiveDate"].ToString());
                ClsUtility.AddParameters("@Status", SqlDbType.Int, theHash["Status"].ToString());
                ClsUtility.AddParameters("@MinStock", SqlDbType.Int, theHash["MinQty"].ToString());
                ClsUtility.AddParameters("@MaxStock", SqlDbType.Int, theHash["MaxQty"].ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, theHash["UserId"].ToString());
                ClsUtility.AddParameters("@ItemInstructions", SqlDbType.VarChar, theHash["ItemInstructions"].ToString());

                ClsUtility.AddParameters("@DispenseUnitQty", SqlDbType.Int, theHash["DispenseUnitQty"].ToString());
                ClsUtility.AddParameters("@VolumeUnit", SqlDbType.Int, theHash["VolumeUnit"].ToString());
                ClsUtility.AddParameters("@MedicationAmt", SqlDbType.Int, theHash["MedicationAmt"].ToString());
                ClsUtility.AddParameters("@PerlblVolUnits", SqlDbType.Decimal, string.IsNullOrEmpty(theHash["PerlblVolUnits"].ToString()) == false ? theHash["PerlblVolUnits"].ToString() : "0");
                ClsUtility.AddParameters("@DispesingUnit", SqlDbType.Int, theHash["DispesingUnit"].ToString());


                ClsObject theItemManager = new ClsObject();
                return
                    (Int32)
                    theItemManager.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveUpdateItemMaster_Futures",
                                                ClsDBUtility.ObjectEnum.ExecuteNonQuery);
            }
        }

        public DataSet GetStoreUserLink(int StoreId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject UserList = new ClsObject();
                ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                return
                    (DataSet)
                    UserList.ReturnObject(ClsUtility.theParams, "pr_SCM_GetStoreUserLinking_Futures",
                                          ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public DataTable GetStoreByUser(int UserId)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject UserList = new ClsObject();
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                return
                    (DataTable)
                    UserList.ReturnObject(ClsUtility.theParams, "Pr_SCM_GetStoreNameByUserID_Futures",
                                          ClsDBUtility.ObjectEnum.DataTable);
            }
        }

        public int SaveUpdateStoreUserLinking(DataTable dtStoreUserList)
        {
            lock (this)
            {
                ClsObject StoreUserLnk = new ClsObject();
                int Rec = 0;

                int theRowAffected = 0;

                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@StoreId", SqlDbType.Int, dtStoreUserList.Rows[0]["StoreID"].ToString());
                theRowAffected =
                    (int)
                    StoreUserLnk.ReturnObject(ClsUtility.theParams, "pr_SCM_DeleteStoreUserlnk_Futures",
                                              ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                for (int i = 0; i <= dtStoreUserList.Rows.Count - 1; i++)
                {
                    Rec = Rec + 1;
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@StoreId", SqlDbType.Int, dtStoreUserList.Rows[i]["StoreID"].ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, dtStoreUserList.Rows[i]["USerId"].ToString());
                    theRowAffected =
                        (int)
                        StoreUserLnk.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveStoreUserlnk_Futures",
                                                  ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        //public DataSet GetPurcaseOrderItem(int isPO, int UserID, int StoreID)
        //{
        //    ClsUtility.Init_Hashtable();
        //    ClsObject GetPurcahseItem = new ClsObject();
        //    ClsUtility.AddParameters("@isPO", SqlDbType.Int, isPO.ToString());
        //    ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
        //    ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreID.ToString());

        //    return
        //        (DataSet)
        //        GetPurcahseItem.ReturnObject(ClsUtility.theParams, "Pr_SCM_GetPurcaseOrderItem",
        //                                     ClsDBUtility.ObjectEnum.DataSet);
        //}

        //public DataSet GetStockforAdjustment(int StoreId, string AdjustmentDate)
        //{
        //    try
        //    {
        //        ClsUtility.Init_Hashtable();
        //        ClsObject GetStockItem = new ClsObject();
        //        ClsUtility.AddParameters("@StoreID", SqlDbType.Int, StoreId.ToString());
        //        ClsUtility.AddParameters("@AdjustmentDate", SqlDbType.VarChar, AdjustmentDate.ToString());
        //        return
        //            (DataSet)
        //            GetStockItem.ReturnObject(ClsUtility.theParams, "Pr_SCM_GetStockforAdjustment_Futures",
        //                                      ClsDBUtility.ObjectEnum.DataSet);
        //    }
        //    catch
        //    {
        //        DataMgr.RollBackTransation(this.Transaction);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);
        //    }
        //}


        //public int SavePurchaseOrder(DataTable DtMasterPO, DataTable dtPOItems, bool isUpdate)
        //{

        //    try
        //    {
        //        this.Connection = DataMgr.GetConnection();
        //        this.Transaction = DataMgr.BeginTransaction(this.Connection);
        //        ClsObject PODetail = new ClsObject();
        //        PODetail.Connection = this.Connection;
        //        PODetail.Transaction = this.Transaction;
        //        int theRowAffected = 0;
        //        int POID = 0;
        //        DataRow theDR;

        //        ClsUtility.Init_Hashtable();

        //        ClsUtility.AddParameters("@LocationID", SqlDbType.VarChar, DtMasterPO.Rows[0]["LocationID"].ToString());
        //        ClsUtility.AddParameters("@SupplierID", SqlDbType.Int, DtMasterPO.Rows[0]["SupplierID"].ToString());
        //        ClsUtility.AddParameters("@OrderDate", SqlDbType.Int, DtMasterPO.Rows[0]["OrderDate"].ToString());
        //        ClsUtility.AddParameters("@PreparedBy", SqlDbType.VarChar, DtMasterPO.Rows[0]["PreparedBy"].ToString());
        //        ClsUtility.AddParameters("@SourceStoreID", SqlDbType.Int, DtMasterPO.Rows[0]["SrcStore"].ToString());
        //        ClsUtility.AddParameters("@DestinStoreID", SqlDbType.Int, DtMasterPO.Rows[0]["DestStore"].ToString());
        //        ClsUtility.AddParameters("@UserID", SqlDbType.Int, DtMasterPO.Rows[0]["UserID"].ToString());
        //        ClsUtility.AddParameters("@AuthorizedBy", SqlDbType.Int, DtMasterPO.Rows[0]["AthorizedBy"].ToString());
        //        if (isUpdate)
        //        {
        //            ClsUtility.AddParameters("@Poid", SqlDbType.Int, DtMasterPO.Rows[0]["POID"].ToString());
        //            ClsUtility.AddParameters("@IsUpdate", SqlDbType.Bit, isUpdate.ToString());

        //            if (Convert.ToString(DtMasterPO.Rows[0]["IsRejectedStatus"]) == "1")
        //            {
        //                ClsUtility.AddParameters("@Status", SqlDbType.Int, "5");
        //            }
        //            else
        //            {
        //                if (Convert.ToString(DtMasterPO.Rows[0]["AthorizedBy"]) == "0")
        //                {
        //                    ClsUtility.AddParameters("@Status", SqlDbType.Int, "1");
        //                }
        //                else
        //                {
        //                    ClsUtility.AddParameters("@Status", SqlDbType.Int, "2");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (Convert.ToString(DtMasterPO.Rows[0]["AthorizedBy"]) == "0")
        //            {
        //                ClsUtility.AddParameters("@Status", SqlDbType.Int, "1");
        //            }
        //            else
        //            {
        //                ClsUtility.AddParameters("@Status", SqlDbType.Int, "2");
        //            }
        //        }

        //        theDR =
        //            (DataRow)
        //            PODetail.ReturnObject(ClsUtility.theParams, "pr_SCM_SavePurchaseOrderMaster_Futures",
        //                                  ClsDBUtility.ObjectEnum.DataRow);
        //        POID = System.Convert.ToInt32(theDR[0].ToString());

        //        if (isUpdate)
        //        {
        //            ClsUtility.Init_Hashtable();
        //            ClsUtility.AddParameters("@POId", SqlDbType.Int, POID.ToString());
        //            theRowAffected =
        //                (int)
        //                PODetail.ReturnObject(ClsUtility.theParams, "pr_SCM_DeletePurchaseOrderItem_Futures",
        //                                      ClsDBUtility.ObjectEnum.ExecuteNonQuery);
        //        }

        //        for (int i = 0; i < dtPOItems.Rows.Count; i++)
        //        {
        //            ClsUtility.Init_Hashtable();
        //            ClsUtility.AddParameters("@POId", SqlDbType.Int, POID.ToString());
        //            ClsUtility.AddParameters("@ItemId", SqlDbType.VarChar, dtPOItems.Rows[i]["ItemId"].ToString());
        //            ClsUtility.AddParameters("@Quantity", SqlDbType.Int, dtPOItems.Rows[i]["Quantity"].ToString());
        //            ClsUtility.AddParameters("@PurchasePrice", SqlDbType.Decimal,
        //                                     dtPOItems.Rows[i]["priceperunit"].ToString());
        //            //  ClsUtility.AddParameters("@Unit", SqlDbType.Int,dtPOItems.Rows[i]["Units"].ToString());
        //            ClsUtility.AddParameters("@UserID", SqlDbType.Int, DtMasterPO.Rows[0]["UserID"].ToString());

        //            ClsUtility.AddParameters("@BatchID", SqlDbType.Int, dtPOItems.Rows[i]["BatchID"].ToString());
        //            ClsUtility.AddParameters("@AvaliableQty", SqlDbType.Int, dtPOItems.Rows[i]["AvaliableQty"].ToString());
        //            ClsUtility.AddParameters("@ExpiryDate", SqlDbType.Int, dtPOItems.Rows[i]["ExpiryDate"].ToString());

        //            theRowAffected =
        //                (int)
        //                PODetail.ReturnObject(ClsUtility.theParams, "pr_SCM_SavePurchaseOrderItem_Futures",
        //                                      ClsDBUtility.ObjectEnum.ExecuteNonQuery);
        //        }

        //        DataMgr.CommitTransaction(this.Transaction);
        //        DataMgr.ReleaseConnection(this.Connection);
        //        return POID;
        //    }
        //    catch
        //    {
        //        DataMgr.RollBackTransation(this.Transaction);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);

        //    }
        //}


        //public DataSet GetOpenStock()
        //{
        //    try
        //    {
        //        ClsUtility.Init_Hashtable();
        //        ClsObject OpeningStock = new ClsObject();
        //        return
        //            (DataSet)
        //            OpeningStock.ReturnObject(ClsUtility.theParams, "pr_SCM_GetOpeningStock_Futures",
        //                                      ClsDBUtility.ObjectEnum.DataSet);
        //    }
        //    catch
        //    {
        //        DataMgr.RollBackTransation(this.Transaction);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);
        //    }
        //}

        //public DataTable GetPurchaseOrderDetails(Int32 UserID, Int32 DestinStoreID, Int32 locationID)
        //{
        //    try
        //    {
        //        ClsUtility.Init_Hashtable();
        //        ClsObject objPOdetails = new ClsObject();
        //        ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
        //        ClsUtility.AddParameters("@DestinStoreID", SqlDbType.Int, DestinStoreID.ToString());
        //        ClsUtility.AddParameters("@LocationID", SqlDbType.Int, locationID.ToString());

        //        return
        //            (DataTable)
        //            objPOdetails.ReturnObject(ClsUtility.theParams, "Pr_SCM_GetPurchaseDetails_Futures",
        //                                      ClsDBUtility.ObjectEnum.DataTable);
        //    }
        //    catch
        //    {
        //        //DataMgr.RollBackTransation(this.Transaction);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);
        //    }
        //}

        //public DataTable GetPurchaseOrderDetailsForGRN(Int32 UserID, Int32 StoreID, Int32 locationID)
        //{
        //    try
        //    {
        //        ClsUtility.Init_Hashtable();
        //        ClsObject objPOdetails = new ClsObject();
        //        ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
        //        ClsUtility.AddParameters("@StoreID", SqlDbType.Int, StoreID.ToString());
        //        ClsUtility.AddParameters("@LocationID", SqlDbType.Int, locationID.ToString());

        //        return
        //            (DataTable)
        //            objPOdetails.ReturnObject(ClsUtility.theParams, "Pr_SCM_GetPurchaseDetailsForGRN_Futures",
        //                                      ClsDBUtility.ObjectEnum.DataTable);
        //    }
        //    catch
        //    {
        //        //DataMgr.RollBackTransation(this.Transaction);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);
        //    }
        //}

        //public int SaveUpdateOpeningStock(DataTable theDTOPStock, Int32 UserID, DateTime TransactionDate)
        //{
        //    ClsObject StoreUserLnk = new ClsObject();
        //    int theRowAffected = 0;
        //    for (int i = 0; i < theDTOPStock.Rows.Count; i++)
        //    {
        //        ClsUtility.Init_Hashtable();
        //        ClsUtility.AddParameters("@ItemId", SqlDbType.Int, theDTOPStock.Rows[i]["ItemId"].ToString());
        //        ClsUtility.AddParameters("@BatchId", SqlDbType.Int, theDTOPStock.Rows[i]["BatchId"].ToString());
        //        ClsUtility.AddParameters("@StoreId", SqlDbType.Int, theDTOPStock.Rows[i]["StoreId"].ToString());
        //        ClsUtility.AddParameters("@Quantity", SqlDbType.Int, theDTOPStock.Rows[i]["Quantity"].ToString());
        //        ClsUtility.AddParameters("@ExpiryDate ", SqlDbType.VarChar,
        //                                 theDTOPStock.Rows[i]["ExpiryDate"].ToString());
        //        ClsUtility.AddParameters("@UserId ", SqlDbType.Int, UserID.ToString());
        //        ClsUtility.AddParameters("@TransactionDate", SqlDbType.DateTime, TransactionDate.ToString());
        //        theRowAffected =
        //            (int)
        //            StoreUserLnk.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveOpeningStock_Futures",
        //                                      ClsDBUtility.ObjectEnum.ExecuteNonQuery);
        //    }
        //    return theRowAffected;
        //}

        //public DataSet GetPurchaseOrderDetailsByPoid(Int32 POId)
        //{
        //    try
        //    {
        //        ClsUtility.Init_Hashtable();
        //        ClsObject objPOdetails = new ClsObject();
        //        ClsUtility.AddParameters("@Poid", SqlDbType.Int, POId.ToString());
        //        return
        //            (DataSet)
        //            objPOdetails.ReturnObject(ClsUtility.theParams, "pr_SCM_GetPurchaseOrderDetailsByPoid_Futures",
        //                                      ClsDBUtility.ObjectEnum.DataSet);
        //    }
        //    catch
        //    {
        //        //DataMgr.RollBackTransation(this.Transaction);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);
        //    }
        //}


        //public int SaveUpdateStockAdjustment(DataTable theDTAdjustStock, int LocationId, int StoreId,
        //                                     string AdjustmentDate, int AdjustmentPreparedBy, int AdjustmentAuthorisedBy,
        //                                     int Updatestock, int UserID)
        //{
        //    try
        //    {
        //        this.Connection = DataMgr.GetConnection();
        //        this.Transaction = DataMgr.BeginTransaction(this.Connection);

        //        ClsObject ObjStoreAdjust = new ClsObject();
        //        int theRowAffected = 0;
        //        ClsUtility.Init_Hashtable();

        //        ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
        //        ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
        //        ClsUtility.AddParameters("@AdjustmentDate", SqlDbType.VarChar, AdjustmentDate.ToString());
        //        ClsUtility.AddParameters("@AdjustmentPreparedBy", SqlDbType.Int, AdjustmentPreparedBy.ToString());
        //        ClsUtility.AddParameters("@AdjustmentAuthorisedBy", SqlDbType.Int, AdjustmentAuthorisedBy.ToString());
        //        DataRow theDR = (DataRow)ObjStoreAdjust.ReturnObject(ClsUtility.theParams, "Pr_SCM_SaveStockOrdAdjust_Futures", ClsDBUtility.ObjectEnum.DataRow);

        //        ClsUtility.Init_Hashtable();
        //        ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
        //        theRowAffected = (int) ObjStoreAdjust.ReturnObject(ClsUtility.theParams, "Pr_SCM_DeleteStockforAdjustment_Futures",ClsDBUtility.ObjectEnum.ExecuteNonQuery);

        //        for (int i = 0; i < theDTAdjustStock.Rows.Count; i++)
        //        {
        //            if (Convert.ToInt32(theDTAdjustStock.Rows[i]["AdjQty"]) > 0 || Convert.ToInt32(theDTAdjustStock.Rows[i]["AdjQty"]) < 0)
        //            {
        //                if (Updatestock == 1)
        //                {
        //                    ClsUtility.Init_Hashtable();
        //                    ClsUtility.AddParameters("@Updatestock", SqlDbType.Int, Updatestock.ToString());
        //                    ClsUtility.AddParameters("@ItemId", SqlDbType.Int,
        //                                             theDTAdjustStock.Rows[i]["ItemId"].ToString());
        //                    ClsUtility.AddParameters("@BatchId", SqlDbType.Int,
        //                                             theDTAdjustStock.Rows[i]["BatchId"].ToString());
        //                    ClsUtility.AddParameters("@ExpiryDate", SqlDbType.DateTime,
        //                                             theDTAdjustStock.Rows[i]["ExpiryDate"].ToString());
        //                    ClsUtility.AddParameters("@StoreId", SqlDbType.Int,
        //                                             theDTAdjustStock.Rows[i]["StoreId"].ToString());
        //                    ClsUtility.AddParameters("@AdjustmentQuantity", SqlDbType.Int,
        //                                             theDTAdjustStock.Rows[i]["AdjQty"].ToString());
        //                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
        //                    theRowAffected =
        //                        (int)
        //                        ObjStoreAdjust.ReturnObject(ClsUtility.theParams, "Pr_SCM_SaveStockTransAdjust_Futures",
        //                                                    ClsDBUtility.ObjectEnum.ExecuteNonQuery);
        //                }
        //            }
        //        }

        //        for (int i = 0; i < theDTAdjustStock.Rows.Count; i++)
        //        {
        //            if (Convert.ToInt32(theDTAdjustStock.Rows[i]["AdjQty"]) > 0 || Convert.ToInt32(theDTAdjustStock.Rows[i]["AdjQty"]) < 0)
        //            {
        //                ClsUtility.Init_Hashtable();
        //                ClsUtility.AddParameters("@UpdateStock", SqlDbType.Int, Updatestock.ToString());
        //                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
        //                ClsUtility.AddParameters("@AdjustmentPreparedBy", SqlDbType.Int, AdjustmentPreparedBy.ToString());
        //                ClsUtility.AddParameters("@AdjustmentAuthorisedBy", SqlDbType.Int, AdjustmentAuthorisedBy.ToString());
        //                ClsUtility.AddParameters("@AdjustmentDate", SqlDbType.DateTime, AdjustmentDate.ToString());
        //                ClsUtility.AddParameters("@AdjustmentId", SqlDbType.Int, theDR["AdjustId"].ToString());
        //                ClsUtility.AddParameters("@ItemId", SqlDbType.Int, theDTAdjustStock.Rows[i]["ItemId"].ToString());
        //                ClsUtility.AddParameters("@BatchId", SqlDbType.Int,
        //                                         theDTAdjustStock.Rows[i]["BatchId"].ToString());
        //                ClsUtility.AddParameters("@ExpiryDate", SqlDbType.DateTime,
        //                                         theDTAdjustStock.Rows[i]["ExpiryDate"].ToString());
        //                ClsUtility.AddParameters("@StoreId", SqlDbType.Int,
        //                                         theDTAdjustStock.Rows[i]["StoreId"].ToString());
        //                ClsUtility.AddParameters("@PurchaseUnit", SqlDbType.Int,
        //                                         theDTAdjustStock.Rows[i]["UnitId"].ToString());
        //                ClsUtility.AddParameters("@AdjustReasonId ", SqlDbType.VarChar,
        //                                         theDTAdjustStock.Rows[i]["AdjustReasonId"].ToString());
        //                ClsUtility.AddParameters("@AdjustmentQuantity", SqlDbType.Int,
        //                                         theDTAdjustStock.Rows[i]["AdjQty"].ToString());
        //                ClsUtility.AddParameters("@UserId ", SqlDbType.Int, UserID.ToString());
        //                theRowAffected =
        //                    (int)
        //                    ObjStoreAdjust.ReturnObject(ClsUtility.theParams, "Pr_SCM_SaveStockOrdAdjust_Futures",
        //                                                ClsDBUtility.ObjectEnum.ExecuteNonQuery);
        //            }
        //        }
        //        return theRowAffected;
        //    }
        //    catch
        //    {
        //        DataMgr.RollBackTransation(this.Transaction);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);
        //    }
        //}

        //public DataSet GetPurchaseOrderDetailsByPoidGRN(Int32 POId)
        //{
        //    try
        //    {
        //        ClsUtility.Init_Hashtable();
        //        ClsObject objPOdetails = new ClsObject();
        //        ClsUtility.AddParameters("@Poid", SqlDbType.Int, POId.ToString());
        //        return
        //            (DataSet)
        //            objPOdetails.ReturnObject(ClsUtility.theParams, "pr_SCM_GetPurchaseOrderGRNByPoid_Futures",
        //                                      ClsDBUtility.ObjectEnum.DataSet);
        //    }
        //    catch
        //    {
        //        //DataMgr.RollBackTransation(this.Transaction);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);
        //    }
        //}

        //public int SaveGoodreceivedNotes(DataTable DtMasterGRN, DataTable dtGRNItems, int IsPOorIST)
        //{

        //    try
        //    {
        //        this.Connection = DataMgr.GetConnection();
        //        this.Transaction = DataMgr.BeginTransaction(this.Connection);
        //        ClsObject PODetail = new ClsObject();
        //        PODetail.Connection = this.Connection;
        //        PODetail.Transaction = this.Transaction;
        //        int theRowAffected = 0;
        //        int GrnId = 0;
        //        DataRow theDR;

        //        if (DtMasterGRN.Rows.Count > 0)
        //        {
        //            if (!String.IsNullOrEmpty(Convert.ToString(DtMasterGRN.Rows[0]["GRNId"])))
        //            {
        //                if (Convert.ToInt32(DtMasterGRN.Rows[0]["GRNId"]) == 0)
        //                {
        //                    ClsUtility.Init_Hashtable();
        //                    ClsUtility.AddParameters("@POId", SqlDbType.VarChar, DtMasterGRN.Rows[0]["POId"].ToString());
        //                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int,
        //                                             DtMasterGRN.Rows[0]["LocationID"].ToString());
        //                    ClsUtility.AddParameters("@RecievedStoreID", SqlDbType.Int,
        //                                             DtMasterGRN.Rows[0]["DestinStoreID"].ToString());
        //                    ClsUtility.AddParameters("@Freight", SqlDbType.VarChar,
        //                                             DtMasterGRN.Rows[0]["Freight"].ToString());
        //                    ClsUtility.AddParameters("@Tax", SqlDbType.Int, DtMasterGRN.Rows[0]["Tax"].ToString());
        //                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, DtMasterGRN.Rows[0]["UserID"].ToString());
        //                    theDR =
        //                        (DataRow)
        //                        PODetail.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveGRNMaster_Futures",
        //                                              ClsDBUtility.ObjectEnum.DataRow);
        //                    GrnId = System.Convert.ToInt32(theDR[0].ToString());
        //                }
        //            }
        //        }

        //        if (GrnId == 0)
        //        {
        //            GrnId = Convert.ToInt32(DtMasterGRN.Rows[0]["GRNId"]);
        //        }
        //        for (int i = 0; i < dtGRNItems.Rows.Count; i++)
        //        {
        //            ClsUtility.Init_Hashtable();
        //            if (!String.IsNullOrEmpty(Convert.ToString(dtGRNItems.Rows[i]["GRNId"].ToString())))
        //            {
        //                if (Convert.ToInt32(dtGRNItems.Rows[i]["GRNId"].ToString()) == 0)
        //                {

        //                    ClsUtility.AddParameters("@GRNId", SqlDbType.Int, GrnId.ToString());
        //                    ClsUtility.AddParameters("@ItemId", SqlDbType.VarChar, dtGRNItems.Rows[i]["ItemId"].ToString());
        //                    //ClsUtility.AddParameters("@BatchID", SqlDbType.Int, dtGRNItems.Rows[i]["BatchID"].ToString());
        //                    ClsUtility.AddParameters("@batchName", SqlDbType.VarChar,
        //                                             dtGRNItems.Rows[i]["batchName"].ToString());
        //                    ClsUtility.AddParameters("@RecievedQuantity", SqlDbType.Int,
        //                                             dtGRNItems.Rows[i]["RecievedQuantity"].ToString());

        //                    ClsUtility.AddParameters("@FreeRecievedQuantity", SqlDbType.Int,
        //                        (Convert.ToString(dtGRNItems.Rows[i]["FreeRecievedQuantity"]) == "") ? "0" : dtGRNItems.Rows[i]["FreeRecievedQuantity"].ToString());

        //                    ClsUtility.AddParameters("@PurchasePrice", SqlDbType.Int,
        //                                        (Convert.ToString(dtGRNItems.Rows[i]["ItemPurchasePrice"]) == "") ? "0" : dtGRNItems.Rows[i]["ItemPurchasePrice"].ToString());
        //                    ClsUtility.AddParameters("@TotPurchasePrice", SqlDbType.Int,
        //                                            (Convert.ToString(dtGRNItems.Rows[i]["TotPurchasePrice"]) == "") ? "0" : dtGRNItems.Rows[i]["TotPurchasePrice"].ToString());
        //                    ClsUtility.AddParameters("@SellingPrice", SqlDbType.Decimal,
        //                                            (Convert.ToString(dtGRNItems.Rows[i]["SellingPrice"]) == "") ? "0" : dtGRNItems.Rows[i]["SellingPrice"].ToString());
        //                    ClsUtility.AddParameters("@SellingPricePerDispense", SqlDbType.Decimal,
        //                                             (Convert.ToString(dtGRNItems.Rows[i]["SellingPricePerDispense"]) == "") ? "0" : dtGRNItems.Rows[i]["SellingPricePerDispense"].ToString());
        //                    ClsUtility.AddParameters("@ExpiryDate", SqlDbType.DateTime,
        //                                             dtGRNItems.Rows[i]["ExpiryDate"].ToString());

        //                    ClsUtility.AddParameters("@UserID", SqlDbType.Int,
        //                         (Convert.ToString(dtGRNItems.Rows[i]["UserID"]) == "") ? DtMasterGRN.Rows[0]["UserID"].ToString() : dtGRNItems.Rows[i]["UserID"].ToString());
        //                    ClsUtility.AddParameters("@IsPOorIST", SqlDbType.Int, IsPOorIST.ToString());
        //                    ClsUtility.AddParameters("@POId", SqlDbType.VarChar, dtGRNItems.Rows[i]["POId"].ToString());
        //                    ClsUtility.AddParameters("@Margin", SqlDbType.Decimal, dtGRNItems.Rows[i]["Margin"].ToString());
        //                    ClsUtility.AddParameters("@destinationStoreID", SqlDbType.Int,
        //                                             dtGRNItems.Rows[i]["DestinStoreID"].ToString());
        //                    ClsUtility.AddParameters("@SourceStoreID", SqlDbType.Int,
        //                                             dtGRNItems.Rows[i]["SourceStoreID"].ToString());


        //                    theRowAffected =
        //                        (int)
        //                        PODetail.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveGRNItems_Futures",
        //                                              ClsDBUtility.ObjectEnum.ExecuteNonQuery);
        //                }
        //            }
        //        }

        //        DataMgr.CommitTransaction(this.Transaction);
        //        DataMgr.ReleaseConnection(this.Connection);
        //        return GrnId;
        //    }
        //    catch
        //    {
        //        DataMgr.RollBackTransation(this.Transaction);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);

        //    }
        //}


        //public DataTable GetExperyReport(int StoreId, DateTime TransDate, DateTime ExpiryDate)
        //{
        //    try
        //    {
        //        ClsUtility.Init_Hashtable();
        //        ClsObject objPOdetails = new ClsObject();
        //        ClsUtility.AddParameters("@Storeid", SqlDbType.Int, StoreId.ToString());
        //        ClsUtility.AddParameters("@TransDate", SqlDbType.Date, TransDate.ToString());
        //        ClsUtility.AddParameters("@ExpiryDate", SqlDbType.Date, ExpiryDate.ToString());
        //        return
        //            (DataTable)
        //            objPOdetails.ReturnObject(ClsUtility.theParams, "pr_SCM_GetExperyReport_Futures",
        //                                      ClsDBUtility.ObjectEnum.DataTable);
        //    }
        //    catch
        //    {
        //        //DataMgr.RollBackTransation(this.Transaction);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);
        //    }
        //}


        //public DataSet GetStockSummary(int StoreId, int ItemId, DateTime FromDate, DateTime ToDate)
        //{
        //    try
        //    {
        //        ClsUtility.Init_Hashtable();
        //        ClsObject objPOdetails = new ClsObject();
        //        ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
        //        ClsUtility.AddParameters("@ItemId", SqlDbType.Int, ItemId.ToString());
        //        ClsUtility.AddParameters("@FromDate", SqlDbType.DateTime, FromDate.ToString());
        //        ClsUtility.AddParameters("@ToDate", SqlDbType.DateTime, ToDate.ToString());
        //        return
        //            (DataSet)
        //            objPOdetails.ReturnObject(ClsUtility.theParams, "pr_SCM_GetStockSummary_Futures",
        //                                      ClsDBUtility.ObjectEnum.DataSet);
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


        //public DataSet GetDisposeStock(int StoreId, DateTime AsofDate)
        //{
        //    try
        //    {
        //        ClsUtility.Init_Hashtable();
        //        ClsObject objPOdetails = new ClsObject();
        //        ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
        //        ClsUtility.AddParameters("@AsofDate", SqlDbType.DateTime, AsofDate.ToString());

        //        return
        //            (DataSet)
        //            objPOdetails.ReturnObject(ClsUtility.theParams, "pr_SCM_GetDisposeStock_Futures",
        //                                      ClsDBUtility.ObjectEnum.DataSet);
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

        //public int SaveDisposeItems(int StoreId, int LocationId, DateTime AsofDate, int UserId, DataTable theDT)
        //{

        //    try
        //    {
        //        this.Connection = DataMgr.GetConnection();
        //        this.Transaction = DataMgr.BeginTransaction(this.Connection);

        //        ClsObject ObjStoreDispose = new ClsObject();
        //        int theRowAffected = 0;
        //        ClsUtility.Init_Hashtable();
        //        ClsUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
        //        ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
        //        ClsUtility.AddParameters("@DisposeDate", SqlDbType.VarChar, AsofDate.ToString());
        //        ClsUtility.AddParameters("@DisposePreparedBy", SqlDbType.Int, UserId.ToString());
        //        ClsUtility.AddParameters("@DisposeAuthorisedBy", SqlDbType.Int, UserId.ToString());
        //        DataRow theDR = (DataRow)ObjStoreDispose.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveDisposeItems_Futures", ClsDBUtility.ObjectEnum.DataRow);
        //        for (int i = 0; i < theDT.Rows.Count; i++)
        //        {
        //            ClsUtility.Init_Hashtable();
        //            if (Convert.ToInt32(! DBNull.Value.Equals(theDT.Rows[i]["Dispose"])) == 1)
        //            {
        //                ClsUtility.AddParameters("@DisposeId", SqlDbType.Int, theDR["DisposeId"].ToString());
        //                ClsUtility.AddParameters("@ItemId", SqlDbType.Int, theDT.Rows[i]["ItemId"].ToString());
        //                ClsUtility.AddParameters("@BatchId", SqlDbType.Int, theDT.Rows[i]["BatchId"].ToString());
        //                ClsUtility.AddParameters("@ExpiryDate", SqlDbType.DateTime,
        //                                         theDT.Rows[i]["ExpiryDate"].ToString());
        //                ClsUtility.AddParameters("@StoreId", SqlDbType.Int, theDT.Rows[i]["StoreId"].ToString());
        //                ClsUtility.AddParameters("@Quantity", SqlDbType.Int, "-" + theDT.Rows[i]["Quantity"].ToString());
        //                ClsUtility.AddParameters("@UserId ", SqlDbType.Int, UserId.ToString());
        //                theRowAffected =
        //                    (int)
        //                    ObjStoreDispose.ReturnObject(ClsUtility.theParams, "pr_SCM_SaveDisposeItems_Futures",
        //                                                 ClsDBUtility.ObjectEnum.ExecuteNonQuery);
        //            }
        //        }
        //        DataMgr.CommitTransaction(this.Transaction);
        //        DataMgr.ReleaseConnection(this.Connection);
        //        return theRowAffected;
        //    }


        //    catch
        //    {
        //        DataMgr.RollBackTransation(this.Transaction);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);
        //    }

        //}

        //public DataSet GetBatchSummary(int StoreId, int ItemId, DateTime FromDate, DateTime ToDate)
        //{
        //    try
        //    {
        //        ClsUtility.Init_Hashtable();
        //        ClsObject objPOdetails = new ClsObject();
        //        ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
        //        ClsUtility.AddParameters("@ItemId", SqlDbType.Int, ItemId.ToString());
        //        ClsUtility.AddParameters("@FromDate", SqlDbType.DateTime, FromDate.ToString());
        //        ClsUtility.AddParameters("@ToDate", SqlDbType.DateTime, ToDate.ToString());
        //        return
        //            (DataSet)
        //            objPOdetails.ReturnObject(ClsUtility.theParams, "pr_SCM_GetBatchSummary_Futures",
        //                                      ClsDBUtility.ObjectEnum.DataSet);
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

        //public DataSet GetStockLedgerData(int StoreId, DateTime FromDate, DateTime ToDate)
        //{

        //    try
        //    {
        //        ClsUtility.Init_Hashtable();
        //        ClsObject objPOdetails = new ClsObject();
        //        ClsUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
        //        ClsUtility.AddParameters("@StartDate", SqlDbType.DateTime, FromDate.ToString());
        //        ClsUtility.AddParameters("@EndDate", SqlDbType.DateTime, ToDate.ToString());
        //        ClsUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
        //        return
        //            (DataSet)
        //            objPOdetails.ReturnObject(ClsUtility.theParams, "Pr_SCM_GetStockLedger_Futures",
        //                                      ClsDBUtility.ObjectEnum.DataSet);
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
        public DataTable GetBillables()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ItemType = new ClsObject();
                return
                    (DataTable)
                    ItemType.ReturnObject(ClsUtility.theParams, "[pr_SCM_GetBillables]",
                                          ClsDBUtility.ObjectEnum.DataTable);
            }
        }
        public DataTable GetBillingGroups()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject ItemType = new ClsObject();
                return
                    (DataTable)
                    ItemType.ReturnObject(ClsUtility.theParams, "[pr_SCM_GetBillingGroups]",
                                          ClsDBUtility.ObjectEnum.DataTable);
            }
        }

        public DataTable GetBillingGroupItems(int billableID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@BillingTypeID", SqlDbType.Int, billableID.ToString());
                ClsObject ItemType = new ClsObject();

                return
                    (DataTable)
                    ItemType.ReturnObject(ClsUtility.theParams, "[pr_SCM_GetBillingGroupItems]",
                                          ClsDBUtility.ObjectEnum.DataTable);
            }
        }
        public DataTable GetBillablesItems(int DecodeID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@DecodeID", SqlDbType.Int, DecodeID.ToString());
                ClsObject ItemType = new ClsObject();

                return
                    (DataTable)
                    ItemType.ReturnObject(ClsUtility.theParams, "[pr_SCM_GetBillablesItems]",
                                          ClsDBUtility.ObjectEnum.DataTable);
            }
        }


    }
}


