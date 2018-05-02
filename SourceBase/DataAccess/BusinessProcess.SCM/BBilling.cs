using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface.SCM;
using System.Data;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;
using DataAccess.Base;
using Entities.Billing;
using System.Xml.Linq;

namespace BusinessProcess.SCM
{
    public class BBilling : ProcessBase, IBilling
    {
        #region Price

        /// <summary>
        /// Gets the item price.
        /// </summary>
        /// <param name="ItemID">The item identifier.</param>
        /// <param name="ItemTypeID">The item type identifier.</param>
        /// <param name="BillingDate">The billing date.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public decimal GetItemPrice(int ItemID, int ItemTypeID, DateTime BillingDate)
        {

            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddExtendedParameters("@ItemID", SqlDbType.Int, ItemID);

                    if (BillingDate.TimeOfDay.TotalSeconds == 0)
                    {
                        BillingDate = BillingDate.AddDays(1).AddMinutes(-1);
                    }
                    ClsUtility.AddExtendedParameters("@BillingDate", SqlDbType.VarChar, BillingDate.ToString("dd-MMM-yyyy")); // Bug ID 158....
                    ClsUtility.AddExtendedParameters("@ItemTypeID", SqlDbType.Int, ItemTypeID);
                    ClsObject BillManager = new ClsObject();
                    DataTable theDt = (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_GetItemPriceOnDate",ClsDBUtility.ObjectEnum.DataTable);
                    if (theDt.Rows.Count > 0)
                    {
                        decimal itemPrice = 0.0M;
                        decimal.TryParse(theDt.Rows[0]["SellingPrice"].ToString(), out itemPrice);
                        return itemPrice;
                    }
                    return 0.0M;
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Gets the price list.
        /// </summary>
        /// <param name="itemType">Type of the item.</param>
        /// <returns></returns>
        public DataTable GetPriceList(int itemType)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@ItemTypeID", SqlDbType.Int, itemType.ToString());
                    ClsObject BillManager = new ClsObject();
                    return (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_GetPriceList",ClsDBUtility.ObjectEnum.DataTable);
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Gets the price list.
        /// </summary>
        /// <param name="ItemTypeID">The item type identifier.</param>
        /// <param name="ItemName">Name of the item.</param>
        /// <param name="WithPriceOnly">if set to <c>true</c> [with price only].</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public ResultSet<SaleItem> GetPriceList(int? ItemTypeID,DateTime? PriceDate = null, string ItemName = "", bool WithPriceOnly = false, Pager page = null)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    // Dictionary<int, List<SaleItem>> resultSets = new Dictionary<int, List<SaleItem>>();
                    if (ItemTypeID.HasValue)
                        ClsUtility.AddExtendedParameters("@ItemTypeID", SqlDbType.Int, ItemTypeID.Value);
                    if(PriceDate.HasValue)
                        ClsUtility.AddExtendedParameters("@PriceDate", SqlDbType.DateTime, PriceDate.Value.ToString("yyyy-MM-dd hh:mm:ss").ToString());
                            //ClsUtility.AddExtendedParameters("@PriceDate", SqlDbType.DateTime, PriceDate.Value);
                    if (ItemName.Trim() != "")
                        ClsUtility.AddParameters("@ItemName", SqlDbType.VarChar, ItemName);
                    ClsUtility.AddExtendedParameters("@WithPriceOnly", SqlDbType.Bit, WithPriceOnly);
                    if (page != null)
                    {
                        ClsUtility.AddExtendedParameters("@Paged", SqlDbType.Bit, true);
                        ClsUtility.AddExtendedParameters("@PageIndex", SqlDbType.Int, page.PageIndex);
                        ClsUtility.AddExtendedParameters("@PageCount", SqlDbType.Int, page.PageCount);
                    }
                    else
                    {
                        ClsUtility.AddExtendedParameters("@Paged", SqlDbType.Bit, false);
                    }
                    ClsObject BillManager = new ClsObject();
                    DataSet ds = (DataSet)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_GetPriceList",ClsDBUtility.ObjectEnum.DataSet);

                    DateTime? emptyDate = null;
                    UInt64? emptyU64Int = null;
                    decimal? emptyDecimal = null;
                    bool? emptyBool = false;

                    DataTable dt = ds.Tables[0];
                    int pages = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
                    var _priceList = (from row in dt.AsEnumerable()
                                      select new SaleItem
                                      {
                                          ItemID = Convert.ToInt32(row["ItemID"]),
                                          ItemName = row["ItemName"].ToString(),
                                          ItemTypeID = Convert.ToInt32(row["ItemTypeID"]),
                                          ItemTypeName = row["ItemTypeName"].ToString(),
                                          PriceDate = row["PriceDate"] != DBNull.Value ? Convert.ToDateTime(row["PriceDate"]) : emptyDate,
                                          PricedPerItem = row["PharmacyPriceType"] != DBNull.Value ? !Convert.ToBoolean(row["PharmacyPriceType"]) : emptyBool,
                                          SellingPrice = row["SellingPrice"] != DBNull.Value ? Convert.ToDecimal(row["SellingPrice"]) : emptyDecimal,
                                          SellingPriceIns = row["SellingPriceIns"] != DBNull.Value ? Convert.ToDecimal(row["SellingPriceIns"]) : emptyDecimal,
                                          Active = Convert.ToBoolean(row["Active"]),
                                          VersionStamp = row["VersionStamp"] != DBNull.Value ? Convert.ToUInt64(row["VersionStamp"]) : emptyU64Int

                                      }
                                       ).ToList<SaleItem>();
                    ResultSet<SaleItem> resultSet = new ResultSet<SaleItem>() { Count = pages, Items = _priceList };
                    //  resultSets.Add(pages, _priceList);
                    return resultSet;
                }
                catch
                {
                    throw;
                }
            }
        }

        
        /// <summary>
        /// Saves the price list.
        /// </summary>
        /// <param name="dtPriceList">The dt price list.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        public int SavePriceList(DataTable dtPriceList, int UserId)
        {
            lock (this)
            {
                try
                {
                    ClsObject ItemList = new ClsObject();
                    int theRowAffected = 0;
                    foreach (DataRow theDR in dtPriceList.Rows)
                    {
                        if (theDR["Item Selling Price"].ToString() == "") continue;
                        ClsUtility.Init_Hashtable();
                        ClsUtility.AddExtendedParameters("@itemID", SqlDbType.Int, int.Parse(theDR["ID"].ToString()));
                        ClsUtility.AddExtendedParameters("@itemType", SqlDbType.Int, int.Parse(theDR["BillingTypeID"].ToString()));
                        ClsUtility.AddExtendedParameters("@itemSellingPrice", SqlDbType.Decimal, Convert.ToDecimal(theDR["Item Selling Price"]));
                        ClsUtility.AddParameters("@effectiveDate", SqlDbType.Int, theDR["Effective Date"].ToString());
                        ClsUtility.AddParameters("@PharmacyPriceType", SqlDbType.Int, theDR["PharmacyPriceType"].ToString());
                        ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, int.Parse(UserId.ToString()));
                        theRowAffected = (int)ItemList.ReturnObject(ClsUtility.theParams, "pr_Billing_SavePriceList",ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    }
                    return theRowAffected;
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Saves the price list.
        /// </summary>
        /// <param name="itemList">The item list.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        public int SavePriceList(List<SaleItem> itemList, int UserId)
        {
            lock (this)
            {
                try
                {
                    ClsObject obj = new ClsObject();
                    int theRowAffected = 0;
                   // int itemsCount = itemList.Count;
                    foreach (SaleItem saleItem in itemList)
                    {
                        if (saleItem.SellingPrice.HasValue)
                        {

                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddExtendedParameters("@itemID", SqlDbType.Int, saleItem.ItemID);
                            ClsUtility.AddExtendedParameters("@itemType", SqlDbType.Int, saleItem.ItemTypeID);
                            ClsUtility.AddExtendedParameters("@itemSellingPrice", SqlDbType.Decimal, saleItem.SellingPrice);
                            if (!saleItem.PriceDate.HasValue) saleItem.PriceDate = DateTime.Now;
                            ClsUtility.AddParameters("@effectiveDate", SqlDbType.VarChar, saleItem.PriceDate.Value.ToString("dd-MMM-yyyy"));
                            ClsUtility.AddExtendedParameters("@PharmacyPriceType", SqlDbType.Bit, !saleItem.PricedPerItem);
                            if (saleItem.VersionStamp.HasValue)
                            {
                                ClsUtility.AddExtendedParameters("@VersionStamp", SqlDbType.BigInt, Convert.ToInt64(saleItem.VersionStamp));
                            }
                            ClsUtility.AddExtendedParameters("@Active", SqlDbType.Bit, saleItem.Active);
                            ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, int.Parse(UserId.ToString()));
                            ClsUtility.AddExtendedParameters("@itemSellingPriceIns", SqlDbType.Decimal, saleItem.SellingPriceIns);

                           int x = Convert.ToInt32(((DataRow)obj.ReturnObject(
                                ClsUtility.theParams, 
                                "pr_Billing_SavePriceList",
                               ClsDBUtility.ObjectEnum.DataRow))[0]);
                           theRowAffected += x;

                        }
                    }
                    return theRowAffected;
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Gets the price list XML.
        /// </summary>
        /// <param name="facilityName">Name of the facility.</param>
        /// <param name="UserName">Name of the user.</param>
        /// <returns></returns>
        public String GetPriceListXML(string facilityName, string UserName,DateTime? PriceDate = null)
        {

            try
            {
                ResultSet<SaleItem> resultSet = this.GetPriceList(null, PriceDate, "", true, null);
                XDocument docX = new XDocument(
                       new XDeclaration("1.0", "UTF-8", "yes"),
                       new XElement("Report",
                           new XElement("Summary",
                               new XElement("Facility_Name", facilityName),
                               new XElement("User_Details", UserName),
                               new XElement("Report_Date", DateTime.Now.ToString("yyyy-MM-dd HH:mm")),
                               new XElement("Price_Date", PriceDate.HasValue? PriceDate.Value.ToString("yyyy-MM-dd"): DateTime.Now.ToString("yyyy-MM-dd"))
                               ),
                           new XElement("Data",
                               new XElement("ItemTypes",
                                   (from it in resultSet.Items
                                    group it by new
                                    {
                                        it.ItemTypeID,
                                        it.ItemTypeName
                                    } into gcs
                                    select new XElement("ItemType",
                                        new XElement("ItemTypeID", gcs.Key.ItemTypeID),
                                        new XElement("ItemTypeName", gcs.Key.ItemTypeName),
                                        new XElement("ItemCount", gcs.Count())
                                     )
                                   )
                             ),
                             new XElement("PriceList",
                                 (
                                   from item in resultSet.Items
                                   select new XElement("Item",
                                      new XElement("ItemID", item.ItemID),
                                      new XElement("ItemName", item.ItemName),
                                      new XElement("ItemTypeID", item.ItemTypeID),
                                      new XElement("ItemTypeName", item.ItemTypeName),
                                      new XElement("SellingPrice", item.SellingPrice),
                                      new XElement("PriceDate", item.PriceDate),
                                      new XElement("PricedPerItem", item.PricedPerItem)
                                   )
                                   )
                                )
                           )
                         )
                   );
                return docX.ToString();
            }
            catch
            {

                throw;
            }
        }


        #endregion

        /// <summary>
        /// Saves the patient bill payments.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <param name="paymentInfo">The payment information.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public DataTable SavePatientBillPayments(int patientID, BillPaymentInfo paymentInfo, int UserID)
        {
            lock (this)
            {
                ClsObject objItemList = null;

                List<BillItem> itemsPaid = paymentInfo.ItemsToPay;
                System.Text.StringBuilder sbItems = new System.Text.StringBuilder("<root>");
                if (itemsPaid != null && itemsPaid.Count > 0)
                {
                    foreach (BillItem item in itemsPaid)
                    {

                        sbItems.Append("<parameter>");
                        sbItems.Append("<billitemid>" + item.BillItemID.ToString() + "</billitemid>");
                        sbItems.Append("</parameter>");
                    }
                }
                sbItems.Append("</root>");
                // int theTransactionID = 0;
                // DataRow billItemRow = paymentInfo.Rows[0];
                DataTable theDR = null;
                try
                {
                    this.Connection = DataMgr.GetConnection();
                    this.Transaction = DataMgr.BeginTransaction(this.Connection);
                    objItemList = new ClsObject();
                    objItemList.Connection = this.Connection;
                    objItemList.Transaction = this.Transaction;

                    if (paymentInfo.Deposit)
                    {
                        int locationID = paymentInfo.LocationID;
                        this.ExecuteDepositTransaction(
                            patientID,
                            locationID,
                            UserID,
                            paymentInfo.Amount,
                            DepositTransactionType.Settlement,
                            objItemList
                          );
                    }
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddExtendedParameters("@BillID", SqlDbType.Int, paymentInfo.BillID);
                    ClsUtility.AddExtendedParameters("@TransactionType", SqlDbType.Int, paymentInfo.PaymentMode.ID);
                    ClsUtility.AddExtendedParameters("@RefNumber", SqlDbType.VarChar, paymentInfo.ReferenceNumber);
                    ClsUtility.AddExtendedParameters("@Amount", SqlDbType.Decimal, paymentInfo.Amount);
                    ClsUtility.AddExtendedParameters("@AmountPayable", SqlDbType.Decimal, paymentInfo.AmountPayable);
                    ClsUtility.AddExtendedParameters("@TenderedAmount", SqlDbType.Decimal, paymentInfo.TenderedAmount);
                    ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, UserID);
                    ClsUtility.AddExtendedParameters("@PatientID", SqlDbType.Int, patientID);
                    ClsUtility.AddParameters("@BillStatus", SqlDbType.VarChar, "Paid");
                    if (itemsPaid != null && itemsPaid.Count > 0)
                        ClsUtility.AddExtendedParameters("@ItemsList", SqlDbType.Xml, sbItems.ToString());
                    theDR = (DataTable)objItemList.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_SaveBillPayment",ClsDBUtility.ObjectEnum.DataTable);

                    DataMgr.CommitTransaction(this.Transaction);
                    DataMgr.ReleaseConnection(this.Connection);

                    return theDR;
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

        #region Reports:
        /// <summary>
        /// Gets the receipt XSD schema.
        /// </summary>
        /// <value>
        /// The receipt XSD schema.
        /// </value>
        string ReceiptXSDSchema
        {
            get
            {
                string xsd =
                    @"<?xml version=""1.0"" standalone=""yes""?>
<xs:schema id=""RECEIPTDATA"" xmlns="""" xmlns:xs=""http://www.w3.org/2001/XMLSchema"" xmlns:msdata=""urn:schemas-microsoft-com:xml-msdata""> 	
	<xs:element name=""Receipt"" msdata:IsDataSet=""true"" msdata:UseCurrentLocale=""true"">
		<xs:complexType> 
			<xs:choice minOccurs=""0"" maxOccurs=""unbounded"">
				<xs:element name=""Items"" maxOccurs=""unbounded"" minOccurs=""1"">
					 <xs:complexType>
						<xs:sequence>
							<xs:element name=""ID""                 type=""xs:string""  />							
							<xs:element name=""BillItemDate""       type=""xs:string""  /> 
							<xs:element name=""Item""               type=""xs:string""  /> 
                            <xs:element name=""Quantity""           type=""xs:int""     /> 
                            <xs:element name=""SellingPrice""       type=""xs:float""   /> 
                            <xs:element name=""Amount""             type=""xs:float""   />                         
                            <xs:element name=""UserFirstName""      type=""xs:string""  /> 
                            <xs:element name=""UserLastName""       type=""xs:string""  /> 
                            <xs:element name=""UserID""             type=""xs:int""     /> 
						</xs:sequence>
					</xs:complexType>
				</xs:element>
                <xs:element name=""Transaction"" maxOccurs=""1"" minOccurs=""1"">
		            <xs:complexType>
		                <xs:sequence>
                            <xs:element name=""PatientID""           type=""xs:string"" />
                            <xs:element name=""LastName""            type=""xs:string"" />	
                            <xs:element name=""FirstName""           type=""xs:string"" />	
                            <xs:element name=""Sex""                 type=""xs:string"" />	
                            <xs:element name=""FacilityName""        type=""xs:string"" />	
                            <xs:element name=""FacilityTel""         type=""xs:string""  minOccurs=""0""/>	
                            <xs:element name=""FacilityCell""        type=""xs:string"" minOccurs=""0"" />
			                <xs:element name=""FacilityFax""         type=""xs:string"" minOccurs=""0"" />
                            <xs:element name=""FacilityAddress""     type=""xs:string"" minOccurs=""0""/>	
                            <xs:element name=""FacilityEmail""       type=""xs:string"" minOccurs=""0""/>	
                            <xs:element name=""FacilityFooter""      type=""xs:string"" minOccurs=""0"" />	
                            <xs:element name=""FacilityURL""         type=""xs:string"" minOccurs=""0""/>	
                            <xs:element name=""FacilityLogo""        type=""xs:string"" minOccurs=""0"" />	
                            <xs:element name=""Currency""            type=""xs:string"" minOccurs=""0"" />	
                            <xs:element name=""BillNumber""          type=""xs:string"" />	
                            <xs:element name=""BillDate""            type=""xs:dateTime"" />	
                            <xs:element name=""BillAmount""          type=""xs:string"" />					             
			                <xs:element name=""TransactionID""       type=""xs:string"" />			               
                            <xs:element name=""Amount""              type=""xs:float""  /> 
                            <xs:element name=""TransactionDate""     type=""xs:dateTime"" /> 
                            <xs:element name=""TransactionType""     type=""xs:string""  /> 
                            <xs:element name=""RefNumber""           type=""xs:string"" minOccurs=""0""/> 
                            <xs:element name=""UserFirstName""       type=""xs:string"" /> 
                            <xs:element name=""UserLastName""        type=""xs:string"" /> 
                            <xs:element name=""UserID""              type=""xs:int""   /> 
		                </xs:sequence>
	                </xs:complexType>
                </xs:element>							                                   
			</xs:choice>
		</xs:complexType>
	</xs:element>
 </xs:schema>";
                return (xsd);
            }
        }
        /// <summary>
        /// Gets the receipt.
        /// </summary>
        /// <param name="TransactionID">The transaction identifier.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <param name="receiptType">Type of the receipt.</param>
        /// <returns></returns>
        public DataSet GetReceipt(int TransactionID, int locationID, ReceiptType receiptType = ReceiptType.BillPayment)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@TransactionID", SqlDbType.Int, TransactionID.ToString());
                    ClsUtility.AddParameters("@locationID", SqlDbType.Int, locationID.ToString());
                    ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);

                    ClsObject BillManager = new ClsObject();
                    string commandText = "pr_Billing_GetReciept";

                    if (receiptType == ReceiptType.DepositTransaction)
                        commandText = "pr_Billing_Report_GetDepositTransaction";
                    else if (receiptType == ReceiptType.ReversalTransaction)
                        commandText = "pr_Billing_Report_GetReversalReciept";

                    DataSet dsReceipt = (DataSet)BillManager.ReturnObject(ClsUtility.theParams, commandText,ClsDBUtility.ObjectEnum.DataSet);

                    DataSet receiptData = new DataSet("Receipt");
                    using (System.IO.TextReader txR = new System.IO.StringReader(this.ReceiptXSDSchema))
                    {

                        receiptData.ReadXmlSchema(txR);
                        txR.Close();
                    }
                    DataTable dtItems = dsReceipt.Tables[0];
                    DataTable dtTransaction = dsReceipt.Tables[1];
                    DataTableReader reader = new DataTableReader(dtItems);
                    receiptData.Tables["Items"].Load(reader);
                    receiptData.AcceptChanges();

                    reader = new DataTableReader(dtTransaction);
                    receiptData.Tables["Transaction"].Load(reader);
                    receiptData.AcceptChanges();

                    return receiptData;
                }
                catch
                {
                    throw;
                }

            }
        }
        /// <summary>
        /// Gets the daily collection summary report.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <returns></returns>
        public DataSet getDailyCollectionSummaryReport(DateTime fromDate, DateTime toDate, int locationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@fromDate", SqlDbType.Date, fromDate.ToString("yyyyMMdd"));
                    ClsUtility.AddParameters("@toDate", SqlDbType.Date, toDate.ToString("yyyyMMdd"));
                    ClsUtility.AddParameters("@locationID", SqlDbType.Int, locationID.ToString());
                    ClsObject BillManager = new ClsObject();
                    return (DataSet)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_DaillyCollectionSummaryReport",ClsDBUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Gets the daily cashiers summary report.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <returns></returns>
        public DataSet getDailyCashiersSummaryReport(DateTime fromDate, DateTime toDate, int locationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@fromDate", SqlDbType.Date, fromDate.ToString("yyyyMMdd"));
                    ClsUtility.AddParameters("@toDate", SqlDbType.Date, toDate.ToString("yyyyMMdd"));
                    ClsUtility.AddParameters("@locationID", SqlDbType.Int, locationID.ToString());
                    ClsObject BillManager = new ClsObject();
                    return (DataSet)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_DailyCashiersSummaryReport",ClsDBUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Gets the daily dept collections report.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <returns></returns>
        public DataSet getDailyDeptCollectionsReport(DateTime fromDate, DateTime toDate, int locationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@fromDate", SqlDbType.Date, fromDate.ToString("yyyyMMdd"));
                    ClsUtility.AddParameters("@toDate", SqlDbType.Date, toDate.ToString("yyyyMMdd"));
                    ClsUtility.AddParameters("@locationID", SqlDbType.Int, locationID.ToString());
                    ClsObject BillManager = new ClsObject();
                    return (DataSet)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_DailyCollectionsReport",ClsDBUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Gets the daily cashiers summary report.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <returns></returns>
        public DataSet getSalesSummary(DateTime fromDate, DateTime toDate, int locationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@fromDate", SqlDbType.Date, fromDate.ToString("yyyyMMdd"));
                    ClsUtility.AddParameters("@toDate", SqlDbType.Date, toDate.ToString("yyyyMMdd"));
                    ClsUtility.AddParameters("@locationID", SqlDbType.Int, locationID.ToString());
                    ClsObject BillManager = new ClsObject();
                    return (DataSet)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_SalesSummaryReport",ClsDBUtility.ObjectEnum.DataSet);
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Gets the invoice.
        /// </summary>
        /// <param name="BillID">The bill identifier.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <param name="patientID">The patient identifier.</param>
        /// <returns></returns>
        public DataSet GetInvoice(int BillID, int locationID, int patientID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@BillID", SqlDbType.Int, BillID.ToString());
                    ClsUtility.AddParameters("@locationID", SqlDbType.Int, locationID.ToString());
                    ClsUtility.AddParameters("@patientID", SqlDbType.Int, patientID.ToString());
                    ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                    ClsObject BillManager = new ClsObject();
                    DataSet theDS = (DataSet)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_GetInvoice",ClsDBUtility.ObjectEnum.DataSet);
                    return theDS;

                }
                catch
                {
                    throw;
                }
            }
        }

        #endregion

        #region PaidItems
        /// <summary>
        /// Determines whether [is visit type paid] [the specified visittype identifier].
        /// </summary>
        /// <param name="visittypeID">The visittype identifier.</param>
        /// <param name="patientID">The patient identifier.</param>
        /// <returns></returns>
        public DataTable isVisitTypePaid(int visittypeID, int patientID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsUtility.AddParameters("@patientID", SqlDbType.Int, patientID.ToString());
                    ClsUtility.AddParameters("@itemID", SqlDbType.Int, visittypeID.ToString());
                    ClsUtility.AddParameters("@itemTypeName", SqlDbType.VarChar, "VisitType");
                    ClsObject BillManager = new ClsObject();
                    DataTable theDT = (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_GetItemPayStatus",ClsDBUtility.ObjectEnum.DataTable);
                    /*  if (theDR[0].ToString() == "1")
                          return true;
                      else
                          return false;*/
                    return theDT;
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Gets the paid labs.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <returns></returns>
        public DataTable GetPaidLabs(int patientID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientID", SqlDbType.Int, patientID.ToString());

                ClsObject BillManager = new ClsObject();
                return (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_GetPaidLabs",ClsDBUtility.ObjectEnum.DataTable);
            }
        }
        /// <summary>
        /// Gets the paid drugs.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <returns></returns>
        public DataTable GetPaidDrugs(int patientID)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@patientID", SqlDbType.Int, patientID.ToString());

                ClsObject BillManager = new ClsObject();
                return (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_GetPaidDrugs",ClsDBUtility.ObjectEnum.DataTable);
            }
        }
        #endregion

        #region Reversal
        /// <summary>
        /// Approves the reject bill reversal.
        /// </summary>
        /// <param name="BillRervesalID">The bill rervesal identifier.</param>
        /// <param name="TransactionID">The transaction identifier.</param>
        /// <param name="Approved">if set to <c>true</c> [approved].</param>
        /// <param name="ApproverID">The approver identifier.</param>
        /// <param name="ApprovalReason">The approval reason.</param>
        /// <param name="ApprovalDate">The approval date.</param>
        public void ApproveRejectTransactionReversal(int BillRervesalID, int TransactionID, bool Approved, int ApproverID, string ApprovalReason, DateTime ApprovalDate, bool RefundCash = false)
        {
            try
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@ReversalID", SqlDbType.Int, BillRervesalID.ToString());
                ClsUtility.AddParameters("@TransactionID", SqlDbType.Int, TransactionID.ToString());
                ClsUtility.AddParameters("@ApprovedBy", SqlDbType.Int, ApproverID.ToString());
                ClsUtility.AddParameters("@ApprovedStatus", SqlDbType.VarChar, (Approved ? "APPROVED" : "REJECTED"));
                ClsUtility.AddParameters("@ApprovalReason", SqlDbType.VarChar, ApprovalReason);
                ClsUtility.AddParameters("@ApprovalDate", SqlDbType.DateTime, ApprovalDate.ToString("yyyy-MM-dd"));
                ClsUtility.AddExtendedParameters("@RefundCash", SqlDbType.Bit, RefundCash);
                ClsObject BillManager = new ClsObject();
                BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_ApproveRejectReversal",ClsDBUtility.ObjectEnum.ExecuteNonQuery);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Gets the reversal requests.
        /// </summary>
        /// <param name="LocationID">The location identifier.</param>
        /// <param name="ReversalReference">The reversal reference.</param>
        /// <param name="FilterOption">The filter option.</param>
        /// <param name="PatientID">The patient identifier.</param>
        /// <returns></returns>
        public DataTable GetReversalRequests(int LocationID, string ReversalReference = "", string FilterOption = "PENDING", int? PatientID = null)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                if (string.IsNullOrEmpty(ReversalReference))
                    ClsUtility.AddParameters("@Reference", SqlDbType.VarChar, ReversalReference);
                if (FilterOption == "APPROVED")
                    ClsUtility.AddParameters("@ApprovalStatus", SqlDbType.Int, "1");
                else if (FilterOption == "PENDING")
                    ClsUtility.AddParameters("@ApprovalStatus", SqlDbType.Int, "0");
                else if (FilterOption == "REJECTED")
                    ClsUtility.AddParameters("@ApprovalStatus", SqlDbType.Int, "2");
                if (PatientID.HasValue)
                    ClsUtility.AddExtendedParameters("@PatientID", SqlDbType.Int, PatientID.Value);
                ClsUtility.AddExtendedParameters("@LocationID", SqlDbType.Int, LocationID);
                ClsObject BillManager = new ClsObject();
                return (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_GetReversals",ClsDBUtility.ObjectEnum.DataTable);

            }
        }
        /// <summary>
        /// Itemses to be reversed.
        /// </summary>
        /// <param name="BillRervesalID">The bill rervesal identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public DataTable ItemsToBeReversed(int BillRervesalID)
        {
            //lock (this)
            //{
            //    ClsUtility.Init_Hashtable();
            //    ClsUtility.AddParameters("@ReversalID", SqlDbType.Int, BillRervesalID.ToString());
            //    ClsObject BillManager = new ClsObject();
            //    return (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_ItemsForReversal",ClsDBUtility.ObjectEnum.DataTable);

            //}
            return new DataTable();
        }

        /// <summary>
        /// Requests the bill reversal.
        /// </summary>
        /// <param name="TransactionID">The transaction identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="ReversalReason">The reversal reason.</param>
        /// <param name="RequestDate">The request date.</param>
        /// <param name="ItemToReverse">The item to reverse.</param>
        public void RequestTransactionReversal(int TransactionID, int UserID, string ReversalReason, DateTime RequestDate, List<int> ItemToReverse = null)
        {
            lock (this)
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@TransactionID", SqlDbType.Int, TransactionID.ToString());
                ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                ClsUtility.AddParameters("@ReversalReason", SqlDbType.VarChar, ReversalReason);
                ClsUtility.AddParameters("@RequestDate", SqlDbType.DateTime, RequestDate.ToString("yyyy-MM-dd"));
                System.Text.StringBuilder sbItems = new System.Text.StringBuilder("<root>");
                foreach (int item in ItemToReverse)
                {

                    sbItems.Append("<parameter>");
                    sbItems.Append("<billitemid>" + item.ToString() + "</billitemid>");
                    sbItems.Append("</parameter>");
                }
                sbItems.Append("</root>");
                ClsUtility.AddExtendedParameters("@ItemsList", SqlDbType.Xml, sbItems.ToString());
                ClsObject BillManager = new ClsObject();
                BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_RequestForReversal",ClsDBUtility.ObjectEnum.ExecuteNonQuery);

            }
        }
        /// <summary>
        /// Refunds the cash.
        /// </summary>
        /// <param name="ReversalId">The reversal identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        public void RefundCash(int ReversalId, int UserID)
        {
            ClsUtility.Init_Hashtable();
            ClsUtility.AddParameters("@ReversalId", SqlDbType.Int, ReversalId.ToString());
            ClsUtility.AddParameters("@RefundedBy", SqlDbType.Int, UserID.ToString());
            ClsObject BillManager = new ClsObject();
            BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_RefundPayment",ClsDBUtility.ObjectEnum.ExecuteNonQuery);
            BillManager = null;
        }
        #endregion

        /// <summary>
        /// Gets the open bills.
        /// </summary>
        /// <param name="LocationID">The location identifier.</param>
        /// <param name="PatientID">The patient identifier.</param>
        /// <param name="DateFrom">The date from.</param>
        /// <param name="DateTo">The date to.</param>
        /// <param name="PaymentStatus">The payment status.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public DataTable GetPatientWithUnpaidItems(int LocationID, DateTime? DateFrom, DateTime? DateTo)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                    ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
                    //if(PatientID.HasValue)
                    //    ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID.Value.ToString());
                    if (DateFrom.HasValue)
                        ClsUtility.AddParameters("@DateFrom", SqlDbType.DateTime, DateFrom.Value.ToString("yyyy-MM-dd"));
                    if (DateTo.HasValue)
                        ClsUtility.AddParameters("@DateTo", SqlDbType.DateTime, DateTo.Value.ToString("yyyy-MM-dd"));
                    // ClsUtility.AddParameters("@PaymentStatus", SqlDbType.Int, "0");
                    ClsObject BillManager = new ClsObject();
                    return (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_getOpenBills",ClsDBUtility.ObjectEnum.DataTable);
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets the patient un billed items.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public DataTable GetPatientUnBilledItems(int patientID, int LocationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddExtendedParameters("@LocationID", SqlDbType.Int, LocationID);
                    ClsUtility.AddParameters("@PatientID", SqlDbType.Int, patientID.ToString());
                    // ClsUtility.AddExtendedParameters("@BillID", SqlDbType.Int, DBNull.Value);
                    ClsObject BillManager = new ClsObject();
                    return (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_GetPatientsBilItems",ClsDBUtility.ObjectEnum.DataTable);
                }
                catch
                {
                    throw;
                }
            }
        }

        #region MakeBill
        /// <summary>
        /// Saves the patient payable items.
        /// </summary>
        /// <param name="billitems">The billitems.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public int SavePatientPayableItems(DataTable billitems, int UserID)
        {
            lock (this)
            {
                try
                {
                    ClsObject objItemList = new ClsObject();

                    int theRowAffected = 0;

                    DataView view = billitems.DefaultView;
                    view.RowFilter = "RowStatus In ('Deleted','Added','Updated')";
                    DataTable newTable = view.ToTable();

                    foreach (DataRow billItemRow in newTable.Rows)
                    {
                        if (billItemRow["RowStatus"].ToString() == "Deleted")
                        {
                            ClsUtility.Init_Hashtable();
                            ClsUtility.AddParameters("@billItemID", SqlDbType.Int, billItemRow["billItemID"].ToString());
                            ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                            theRowAffected += (int)objItemList.ReturnObject(ClsUtility.theParams, "pr_Billing_DeleteBillItem",ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                        }
                        else
                        {
                            Decimal result = 0;
                            ClsUtility.Init_Hashtable();
                            if (billItemRow["BillID"] != DBNull.Value || billItemRow["BillID"].ToString() != "")
                                ClsUtility.AddExtendedParameters("@BillID", SqlDbType.Int, int.Parse(billItemRow["BillID"].ToString()));
                            ClsUtility.AddExtendedParameters("@PatientID", SqlDbType.Int, int.Parse(billItemRow["PatientID"].ToString()));
                            if (billItemRow["ModuleID"].ToString() != "")
                                ClsUtility.AddExtendedParameters("@ModuleID", SqlDbType.Int, int.Parse(billItemRow["ModuleID"].ToString()));
                            if (billItemRow["LocationID"] != DBNull.Value)
                                ClsUtility.AddExtendedParameters("@LocationID", SqlDbType.Int, int.Parse(billItemRow["LocationID"].ToString()));
                            if (billItemRow["billItemID"] != DBNull.Value || billItemRow["billItemID"].ToString() != "")
                                ClsUtility.AddExtendedParameters("@billItemID", SqlDbType.Int, int.Parse(billItemRow["billItemID"].ToString()));
                            ClsUtility.AddParameters("@billItemDate", SqlDbType.DateTime, Convert.ToDateTime(billItemRow["billItemDate"]).ToString("yyyy-MM-dd HH:mm:ss"));
                            // ClsUtility.AddParameters("@PaymentType", SqlDbType.Int, billItemRow["PaymentType"].ToString());
                            if (billItemRow["PaymentStatus"] != DBNull.Value || billItemRow["PaymentStatus"].ToString() != "")
                                ClsUtility.AddExtendedParameters("@PaymentStatus", SqlDbType.Int, int.Parse(billItemRow["PaymentStatus"].ToString()));
                            ClsUtility.AddExtendedParameters("@ItemId", SqlDbType.Int, int.Parse(billItemRow["itemId"].ToString()));
                            ClsUtility.AddParameters("@ItemName", SqlDbType.VarChar, billItemRow["itemName"].ToString());
                            ClsUtility.AddExtendedParameters("@ItemType", SqlDbType.Int, int.Parse(billItemRow["itemType"].ToString()));
                            ClsUtility.AddExtendedParameters("@Quantity", SqlDbType.Int, int.Parse(billItemRow["Quantity"].ToString()));
                            ClsUtility.AddExtendedParameters("@SellingPrice", SqlDbType.Decimal, decimal.Parse(billItemRow["SellingPrice"].ToString()));
                            Decimal.TryParse(billItemRow["Discount"].ToString(), out result);
                            ClsUtility.AddExtendedParameters("@Discount", SqlDbType.Decimal, result);
                            ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, int.Parse(UserID.ToString()));
                            if (billItemRow["serviceStatus"] != DBNull.Value || billItemRow["serviceStatus"].ToString() != "")
                                ClsUtility.AddExtendedParameters("@ServiceStatus", SqlDbType.Int, int.Parse(billItemRow["serviceStatus"].ToString()));
                            theRowAffected +=
                                  (int)
                                  objItemList.ReturnObject(ClsUtility.theParams, "pr_Billing_SaveBillItem",ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                        }
                    }

                    return theRowAffected;
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Generates the bill.
        /// </summary>
        /// <param name="billItems">The bill items.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="PatientID">The patient identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public string GenerateBill(DataTable billItems, int PatientID, int LocationID, int UserID)
        {
            try
            {
                decimal billAmount = Convert.ToDecimal(billItems.Compute("Sum(amount)", ""));
                System.Text.StringBuilder sbItems = new System.Text.StringBuilder("<root>");
                foreach (DataRow item in billItems.Rows)
                {

                    sbItems.Append("<parameter>");
                    sbItems.Append("<billitemid>" + item["billitemid"].ToString() + "</billitemid>");
                    sbItems.Append("</parameter>");
                }
                sbItems.Append("</root>");
                lock (this)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddExtendedParameters("@ItemsList", SqlDbType.Xml, sbItems.ToString());
                    ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                    ClsUtility.AddExtendedParameters("@BillAmount", SqlDbType.Decimal, billAmount);
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    ClsObject BillManager = new ClsObject();
                    DataRow row = (DataRow)BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_GenerateBill",ClsDBUtility.ObjectEnum.DataRow);
                    return row[0].ToString();

                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Removes the item from bill.
        /// </summary>
        /// <param name="billItemID">The bill item identifier.</param>
        /// <param name="BillID">The bill identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void RemoveItemFromBill(int billItemID, int BillID, int UserID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@BillItemID", SqlDbType.Int, billItemID.ToString());
                    ClsUtility.AddParameters("@BillID", SqlDbType.Int, BillID.ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    ClsObject BillManager = new ClsObject();
                    BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_RemoveItemFromBill",ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                catch
                {
                    throw;
                }

            }
        }
        /// <summary>
        /// Cancels the bill.
        /// </summary>
        /// <param name="BillID">The bill identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void CancelBill(int BillID, int UserID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@BillID", SqlDbType.Int, BillID.ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
                    ClsObject BillManager = new ClsObject();
                    BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_CancelBill",ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                }
                catch
                {
                    throw;
                }
            }
        }

        #endregion

        /// <summary>
        /// Gets the patient bill by status.
        /// </summary>
        /// <param name="PatientID">The patient identifier.</param>
        /// <param name="LocationID">The location identifier.</param>
        /// <param name="billStatus">The bill status.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public DataTable GetPatientBillByStatus(int PatientID, int LocationID, BillStatus billStatus)
        {

            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    if (billStatus == BillStatus.All)
                    {
                        //ClsUtility.AddExtendedParameters("@BillStatus", SqlDbType.Int, DBNull.Value);
                    }
                    else ClsUtility.AddExtendedParameters("@BillStatus", SqlDbType.Int, (int)billStatus);
                    ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID.ToString());
                    ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
                    ClsObject BillManager = new ClsObject();
                    DataTable patientBills = (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_GetBillDetails",ClsDBUtility.ObjectEnum.DataTable);
                    return patientBills;
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets the items in a bill.
        /// </summary>
        /// <param name="BillID">The bill identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public DataTable GetBillItems(int BillID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    //ClsUtility.AddExtendedParameters("@LocationID", SqlDbType.Int, DBNull.Value);
                    ClsUtility.AddParameters("@BillID", SqlDbType.Int, BillID.ToString());
                   // ClsUtility.AddExtendedParameters("@PatientID", SqlDbType.Int, DBNull.Value);
                    ClsObject BillManager = new ClsObject();
                    DataTable billItems = (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_GetPatientsBilItems",ClsDBUtility.ObjectEnum.DataTable);
                    return billItems;
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Gets the bill transactions.
        /// </summary>
        /// <param name="BillID">The bill identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public DataTable GetBillTransactions(int BillID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@BillID", SqlDbType.Int, BillID.ToString());
                    ClsObject BillManager = new ClsObject();
                    DataTable billTran = (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_GetBillTransactions",ClsDBUtility.ObjectEnum.DataTable);
                    return billTran;
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Gets the bill details.
        /// </summary>
        /// <param name="BillID">The bill identifier.</param>
        /// <param name="PatientID">The patient identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public DataTable GetBillDetails(int BillID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    //ClsUtility.AddParameters("@PatientID", SqlDbType.Int, PatientID.ToString());
                   // ClsUtility.AddExtendedParameters("@LocationID", SqlDbType.Int, DBNull.Value);
                    ClsUtility.AddParameters("@BillID", SqlDbType.Int, BillID.ToString());
                    ClsObject BillManager = new ClsObject();
                    DataTable patientBills = (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_GetBillDetails",ClsDBUtility.ObjectEnum.DataTable);
                    return patientBills;
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Saves the billables items.
        /// </summary>
        /// <param name="BillableID">The billable identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <param name="itemList">The item list.</param>
        public void SaveBillablesItems(int BillableID, int userID, DataTable itemList)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@BillableID", SqlDbType.Int, BillableID.ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());

                    System.Text.StringBuilder sbItems = new System.Text.StringBuilder("<root>");
                    foreach (DataRow row in itemList.Rows)
                    {

                        sbItems.Append("<row>");
                        sbItems.Append("<BillingTypeID>" + row["BillTypeID"].ToString() + "</BillingTypeID>");
                        sbItems.Append("<ItemID>" + row["ID"].ToString() + "</ItemID>");

                        sbItems.Append("</row>");
                    }
                    sbItems.Append("</root>");
                    ClsUtility.AddExtendedParameters("@ItemList", SqlDbType.Xml, sbItems.ToString());
                    ClsObject BillablesManager = new ClsObject();
                    BillablesManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_SaveBillablesItems",ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                catch
                {
                    throw;
                }
            }
        }


        #region Deposits

        /// <summary>
        /// Executes the deposit transaction.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="transationType">Type of the transation.</param>
        public DataTable ExecuteDepositTransaction(int patientID, int locationID, int userID, decimal amount, DepositTransactionType transactionType, object clsObject = null)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@locationID", SqlDbType.Int, locationID.ToString());
                    ClsUtility.AddParameters("@patientID", SqlDbType.Int, patientID.ToString());
                    ClsUtility.AddParameters("@UserID", SqlDbType.Int, userID.ToString());
                    ClsUtility.AddExtendedParameters("@Amount", SqlDbType.Decimal, amount);
                    ClsUtility.AddExtendedParameters("@TransactionType", SqlDbType.Int, (int)transactionType);
                    ClsObject BillManager = null;
                    if (clsObject == null)
                        BillManager = new ClsObject();
                    else BillManager = (ClsObject)clsObject;

                    DataTable theDR = (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_ExecDepositTransaction",ClsDBUtility.ObjectEnum.DataTable);
                    return theDR;
                }
                catch
                {
                    throw;
                }

            }
        }

        /// <summary>
        /// Gets the patient deposit. The summary. The recent deposit and the available amount
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <returns></returns>
        public DataTable GetPatientDeposit(int patientID, int locationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@locationID", SqlDbType.Int, locationID.ToString());
                    ClsUtility.AddParameters("@patientID", SqlDbType.Int, patientID.ToString());
                    ClsObject BillManager = new ClsObject();
                    DataTable theDS = (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_GetPatientDepositSummary",ClsDBUtility.ObjectEnum.DataTable);
                    return theDS;
                }
                catch
                {
                    throw;
                }

            }
        }

        /// <summary>
        /// Gets all the deposits transactions for a patient.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <returns></returns>
        public DataTable GetPatientDepositTransactions(int patientID, int locationID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@locationID", SqlDbType.Int, locationID.ToString());
                    ClsUtility.AddParameters("@patientID", SqlDbType.Int, patientID.ToString());
                    ClsObject BillManager = new ClsObject();
                    DataTable theDS = (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "pr_Billing_GetPatientDepositTransactions",ClsDBUtility.ObjectEnum.DataTable);
                    return theDS;
                }
                catch
                {
                    throw;
                }
            }
        }

        #endregion

        #region PaymentMethods
        /// <summary>
        /// Saves the payment method.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="UserID">The user identifier.</param>
        public void SavePaymentMethod(PaymentMethod p, int UserID)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    if (p.ID.HasValue)
                    {
                        ClsUtility.AddExtendedParameters("@MethodID", SqlDbType.Int, p.ID);
                        ClsUtility.AddParameters("@Action", SqlDbType.VarChar, "UPDATE");
                    }
                    else
                    {
                        ClsUtility.AddParameters("@Action", SqlDbType.VarChar, "NEW");
                    }
                    ClsUtility.AddExtendedParameters("@UserID", SqlDbType.Int, UserID);
                    ClsUtility.AddParameters("@MethodName", SqlDbType.VarChar, p.Name);
                    ClsUtility.AddExtendedParameters("@Active", SqlDbType.Bit, p.Active);
                    ClsObject BillablesManager = new ClsObject();
                    BillablesManager.ReturnObject(ClsUtility.theParams, "dbo.Pr_Billing_ManagePaymentMethods", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Gets the payment methods.
        /// </summary>
        /// <param name="filterName">Name of the filter.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public List<PaymentMethod> GetPaymentMethods(string filterName = "")
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();
                    ClsObject BillManager = new ClsObject();
                    if (filterName != "")
                    {
                        ClsUtility.AddParameters("@MethodName", SqlDbType.VarChar, filterName);
                    }
                    DataTable theDT = (DataTable)BillManager.ReturnObject(ClsUtility.theParams, "Pr_Billing_GetPaymentMethods",
                        ClsDBUtility.ObjectEnum.DataTable);
                    //DataView theDV = new DataView(theDS);
                    //if (filterName != "")
                    //{
                    //    theDV.RowFilter = "PaymentName ='" + filterName + "'";
                    //}
                    //DataTable theDT = theDV.ToTable();
                    var _pm = (from row in theDT.AsEnumerable()
                               select new PaymentMethod()
                               {
                                   ID = Convert.ToInt32(row["ID"]),
                                   Name = row["PaymentName"].ToString(),
                                   Active = Convert.ToBoolean(row["Active"]),
                                   Locked = Convert.ToBoolean(row["Locked"])
                               }
                     ).ToList<PaymentMethod>();
                    return _pm;
                }
                catch
                {
                    throw;
                }
            }
        }

        public string GetPatientPaymentMode(int PatientId, int BillId)
        {
            lock (this)
            {
                try
                {
                    ClsUtility.Init_Hashtable();

                    ClsUtility.AddExtendedParameters("@PatientId", SqlDbType.Int, PatientId);
                    ClsUtility.AddExtendedParameters("@BillId", SqlDbType.Int, BillId);

                    ClsObject BillablesManager = new ClsObject();
                    DataTable dt = (DataTable)BillablesManager.ReturnObject(ClsUtility.theParams, "dbo.pr_Billing_GetPatientPaymentMode", ClsDBUtility.ObjectEnum.DataTable);
                    return dt.Rows[0]["PaymentMode"].ToString();
                }
                catch
                {
                    throw;
                }
            }
        }
        #endregion
   
    }
}
