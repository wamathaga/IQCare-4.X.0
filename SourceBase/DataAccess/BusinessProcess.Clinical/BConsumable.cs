using System;
using System.Data;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.Clinical;

namespace BusinessProcess.Clinical
{
    public class BConsumable : ProcessBase, IConsumable
    {

        /// <summary>
        /// Gets the name of the consumable by.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <returns></returns>
        public DataTable GetConsumableByName(string searchText)
        {
            int itemTypeID = GetConsumableTypeID();
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject consumable = new ClsObject();
                ClsUtility.AddParameters("@SearchText", SqlDbType.VarChar, searchText.Trim().ToString());
                ClsUtility.AddParameters("@ItemTypeID", SqlDbType.Int, itemTypeID.ToString());
                return (DataTable)consumable.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_FindItemByName", ClsDBUtility.ObjectEnum.DataTable);
            }
        }
        /// <summary>
        /// Gets the patient consumable by date.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <param name="issueDate">The issue date.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public DataTable GetPatientConsumableByDate(int patientID, DateTime issueDate)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject consumable = new ClsObject();
                ClsUtility.AddParameters("@Ptn_PK", SqlDbType.Int, patientID.ToString());
                ClsUtility.AddParameters("@IssueDate", SqlDbType.VarChar, issueDate.ToString("dd-MMM-yyyy"));
                return (DataTable)consumable.ReturnObject(ClsUtility.theParams, "dbo.pr_Clinical_PatientConsumablesByDate", ClsDBUtility.ObjectEnum.DataTable);
            }
        }

        /// <summary>
        /// Issues the consumable.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <param name="issueDate">The issue date.</param>
        /// <param name="userID">The user identifier.</param>
        /// <param name="item">The item.</param>
        /// <param name="quantity">The quantity.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void IssueConsumable(int itemID, int itemTypeID,string itemName,float sellingPrice,int patientID,int LocationID, DateTime issueDate, int userID, int quantity, int moduleID,bool itemConsumed=true)
        {
            lock (this)
            {
                IConsumableItem item = (IConsumableItem)this;
                item.ItemID = itemID;
                item.ItemName = itemName;
                item.ItemTypeID = itemTypeID;
                item.SellingPrice = sellingPrice;
                float _discount = item.CalculateDiscount(patientID, issueDate, moduleID);
                ClsObject objItemList = new ClsObject();
                ClsUtility.Init_Hashtable();

                
                ClsUtility.AddExtendedParameters("@PatientID", SqlDbType.Int, patientID);
                ClsUtility.AddExtendedParameters("@LocationID", SqlDbType.Int, LocationID);
                ClsUtility.AddExtendedParameters("@ModuleID", SqlDbType.Int, moduleID);
                ClsUtility.AddParameters("@DateIssued", SqlDbType.DateTime, issueDate.ToString("dd-MMM-yyyy"));
                ClsUtility.AddParameters("@ItemId", SqlDbType.Int, itemID.ToString());            
                ClsUtility.AddParameters("@ItemTypeID", SqlDbType.Int, itemTypeID.ToString());
                ClsUtility.AddParameters("@Quantity", SqlDbType.Int, quantity.ToString());
                ClsUtility.AddParameters("@SellingPrice", SqlDbType.Int, sellingPrice.ToString());
                ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                DataRow row =    (DataRow)objItemList.ReturnObject(ClsUtility.theParams, "dbo.pr_Clinical_IssueItemToPatient", ClsDBUtility.ObjectEnum.DataRow);


                if (item.SellingPrice > 0 && (item.SellingPrice - _discount) > 0 && row !=null)
                {
                    objItemList = new ClsObject();
                    ClsUtility.Init_Hashtable();
                   // ClsUtility.AddExtendedParameters("@BillID", SqlDbType.Int, DBNull.Value);
                    ClsUtility.AddExtendedParameters("@PatientID", SqlDbType.Int, patientID);
                    ClsUtility.AddExtendedParameters("@ModuleID", SqlDbType.Int, moduleID);
                    ClsUtility.AddExtendedParameters("@LocationID", SqlDbType.Int, LocationID);
                    //ClsUtility.AddExtendedParameters("@billItemID", SqlDbType.Int, DBNull.Value);
                    ClsUtility.AddExtendedParameters("@ItemSourceReferenceID", SqlDbType.Int, row["PatientItemID"].ToString());
                    ClsUtility.AddParameters("@BillItemDate", SqlDbType.DateTime, issueDate.ToString("dd-MMM-yyyy"));
                    //ClsUtility.AddExtendedParameters("@PaymentType", SqlDbType.Int, DBNull.Value);
                    ClsUtility.AddParameters("@ItemId", SqlDbType.Int, itemID.ToString());
                    ClsUtility.AddParameters("@ItemName", SqlDbType.VarChar, itemName);
                    ClsUtility.AddParameters("@ItemType", SqlDbType.Int, itemTypeID.ToString());
                    ClsUtility.AddParameters("@Quantity", SqlDbType.Int, quantity.ToString());
                    ClsUtility.AddParameters("@SellingPrice", SqlDbType.Int, item.SellingPrice.ToString());
                    ClsUtility.AddParameters("@PaymentStatus ", SqlDbType.Int, "0");
                    ClsUtility.AddParameters("@Discount", SqlDbType.Int, _discount.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, userID.ToString());
                    ClsUtility.AddParameters("@ServiceStatus", SqlDbType.Int, itemConsumed ? "1" : "0");
                    objItemList.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_SaveBillItem", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
              
            }
        }

        /// <summary>
        /// Removes the consumable.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="itemIssueID">The item issue identifier.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void RemoveConsumable(int userID, int itemIssueID)
        {
            lock (this)
            {
                ClsObject objItemList = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ItemIssueID", SqlDbType.Int, itemIssueID.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                objItemList.ReturnObject(ClsUtility.theParams, "dbo.pr_Clinical_DeleteItemIssuedToPatient", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
            }

        }
        int _itemID;
        /// <summary>
        /// Gets or sets the item identifier.
        /// </summary>
        /// <value>
        /// The item identifier.
        /// </value>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public int ItemID
        {
            get
            {
                return this._itemID;
            }
            set
            {
                this._itemID = value;
            }
        }
        int _itemTypeID;
        /// <summary>
        /// Gets or sets the item type identifier.
        /// </summary>
        /// <value>
        /// The item type identifier.
        /// </value>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        /// 
        int GetConsumableTypeID()
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsObject consumable = new ClsObject();
                ClsUtility.AddParameters("@ItemName", SqlDbType.VarChar, ItemTypeName);
                DataRow row =
                    (DataRow)consumable.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_GetItemTypeIDByName", ClsDBUtility.ObjectEnum.DataRow);
                return int.Parse(row[0].ToString());
             
            }
        }
        public int ItemTypeID
        {
            get
            {
                //
                return this._itemTypeID; 
            }
            set
            {
                this._itemTypeID = value;

            }
        }
        float _sellingPrice;

        /// <summary>
        /// Gets or sets the selling price.
        /// </summary>
        /// <value>
        /// The selling price.
        /// </value>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public float SellingPrice
        {
            get
            {
                return this._sellingPrice;
            }
            set
            {
                this._sellingPrice = value;
            }
        }

        /// <summary>
        /// Calculates the discount.
        /// </summary>
        /// <param name="itemID">The item identifier.</param>
        /// <param name="patientID">The patient identifier.</param>
        /// <param name="issueDate">The issue date.</param>
        /// <param name="moduleID">The module identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        float IConsumableItem.CalculateDiscount(int patientID, DateTime issueDate, int moduleID)
        {
            return 0.00F;
        }
        /// <summary>
        /// The _item name
        /// </summary>
        string _itemName;
        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        /// <value>
        /// The name of the item.
        /// </value>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public string ItemName
        {
            get
            {
                return this._itemName;
            }
            set
            {
                this._itemName=value;
            }
        }
        /// <summary>
        /// The _item type name
        /// </summary>
        string _itemTypeName;
        /// <summary>
        /// Gets or sets the name of the item type.
        /// </summary>
        /// <value>
        /// The name of the item type.
        /// </value>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public string ItemTypeName
        {
            get
            {
                return "Consumables";
            }
            set
            {
                this._itemTypeName = value;
            }
        }
    }
}
