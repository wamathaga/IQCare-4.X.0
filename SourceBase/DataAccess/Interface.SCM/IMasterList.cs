using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Interface.SCM
{

    public interface IMasterList
    {
        DataSet GetProgramList();
        int SaveProgramList(DataTable dtProgramList,int UserID);
        DataSet GetSupplierList();
        int SaveSupplierList(DataTable dtSupplierList, int UserID);
        DataTable  GetItemType();
        DataSet GetDrugType(int itemTypeId);
        DataTable GetSubItemType();
       // DataTable GetItemList(int itemTypeId, int Subtypeid);
        DataSet GetItemList(int itemTypeId, int Subtypeid, int programId);
        DataSet GetItemList(int Subtypeid);
        DataTable GetCommonItemList(String CategoryId, String TableName);
        int SaveSubItemList(ArrayList tdrugType, int ItemID, int userid);
        int SaveItemList(DataTable dtItemList, int itematypeID, int UserID, int ProgramID);
        int SaveSupplierItemList(DataTable dtItemList, int itematypeID, int UserID, int supplierID);
        int SaveUpdateItemList(DataTable dtItemList, string CategoryID, string TableName, int UserID);
        DataSet GetDonorList();
        int SaveDonorList(DataTable dtDonorList, int UserID);
        DataSet GetProgramDonorLnk();
        DataSet GetItemMasterListing();
        int SaveUpdateStore(DataTable dtItemList, string CategoryID, string TableName, int UserID);
        int SaveProgramDonorLnk(DataTable dtProgramDonorLnk, int UserID);
        //DataTable GetItemListSupplier(int itemTypeId, int Subtypeid);
        DataSet GetItemListSupplier(int itemTypeId, int Subtypeid, int SupplierId);
        DataSet GetStoreDetail();
        DataSet GetItemDetails(int theItemId);
        int SaveUpdateStoreLinking(DataTable dtStoreList, string TableName, int UserID);
        int SaveUpdateItemMaster(Hashtable theHash);
        DataSet GetStoreUserLink(int StoreId);
        int SaveUpdateStoreUserLinking(DataTable dtStoreList);
        DataTable GetStoreByUser(int UserId);
        DataSet GetItemListStore(int StoreId);
        int SaveStoreItemList(DataTable dtItemList, int UserID, int StoreID);
        //DataSet GetPurcaseOrderItem(int isPO, int UserID, int StoreID);
        //DataSet GetStockforAdjustment(int StoreId, string AdjustmentDate);
        //int SavePurchaseOrder(DataTable DtMasterPO, DataTable dtPOItems, bool isUpdate);
        //DataSet GetOpenStock();
        //DataTable GetPurchaseOrderDetails(Int32 UserID, Int32 DestinStoreID, Int32 locationID);
        //DataTable GetPurchaseOrderDetailsForGRN(Int32 UserID, Int32 StoreID, Int32 locationID);
        //int SaveUpdateOpeningStock(DataTable theDTOPStock, Int32 UserID, DateTime TransactionDate);
        //DataSet GetPurchaseOrderDetailsByPoid(Int32 POId);
        //int SaveUpdateStockAdjustment(DataTable theDTAdjustStock, int LocationId, int StoreId, string AdjustmentDate, int AdjustmentPreparedBy, int AdjustmentAuthorisedBy, int Updatestock, int UserID);
        DataSet SaveBatchName(string BatchName, int UserId, string itemID, string expiryDatetime);
        //DataSet GetPurchaseOrderDetailsByPoidGRN(Int32 POId);
        //int SaveGoodreceivedNotes(DataTable DtMasterGRN, DataTable dtGRNItems,int IsPOorIST);
        //DataTable GetExperyReport(int StoreId, DateTime TransDate,DateTime ExpiryDate);
        //DataSet GetStockSummary(int StoreId,int ItemId, DateTime FromDate, DateTime ToDate);
        //DataSet GetDisposeStock(int StoreId, DateTime AsofDate);
        //int SaveDisposeItems(int StoreId, int LocationId, DateTime AsofDate, int UserId, DataTable theDT);
        //DataSet GetBatchSummary(int StoreId, int ItemId, DateTime FromDate, DateTime ToDate);
        //DataSet GetStockLedgerData(int StoreId, DateTime FromDate, DateTime ToDate);
        DataSet GetItemListStore_Filtered(int itemTypeId, int Subtypeid, int StoreId);
        int SaveStoreItemList_Filtered(DataTable dtItemList, int UserID, int StoreID, int itemtypeID);
        DataTable GetBillables();
        DataTable GetBillingGroups();
        DataTable GetBillingGroupItems(int billableID);
        DataTable GetBillablesItems(int DecodeID);
        DataTable GetFilteredSubItemType(int ItemTypeId);
        DataTable GetItemTypeList();
        DataSet GetDrugTypeIQMGT(int itemTypeId);
    }
}
