using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Entities.Billing;

namespace Interface.SCM
{
  
    /// <summary>
    /// 
    /// </summary>
    public interface IBilling
    {
             
        ///// <summary>    
        /// Gets the open bilss.
        /// </summary>
        /// <param name="LocationID">The location identifier.</param>
        /// <param name="DateFrom">The date from.</param>
        /// <param name="DateTo">The date to.</param>
        /// <returns></returns>
        DataTable GetPatientWithUnpaidItems(int LocationID, DateTime? DateFrom, DateTime? DateTo);
        /// <summary>
        /// Gets all billable items.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="ItemTypeID">The item type identifier.</param>
        /// <param name="ExcludeItemTypeID">The exclude item type identifier.</param>
        /// <param name="PriceDate">The price date.</param>
        /// <param name="WithPriceOnly">if set to <c>true</c> [with price only].</param>
        /// <returns></returns>
       // DataTable GetAllBillableItems(String filter, int? ItemTypeID = null, int? ExcludeItemTypeID = null, DateTime? PriceDate=null, bool? WithPriceOnly =true);      
        /// <summary>
        /// Gets the price list.
        /// </summary>
        /// <param name="itemType">Type of the item.</param>
        /// <returns></returns>
        DataTable GetPriceList(int itemType);
        /// <summary>
        /// Gets the price list.
        /// </summary>
        /// <param name="ItemTypeID">The item type identifier.</param>
        /// <param name="ItemName">Name of the item.</param>
        /// <param name="WithPriceOnly">if set to <c>true</c> [with price only].</param>
        /// <returns></returns>
        ResultSet<SaleItem> GetPriceList(int? ItemTypeID, DateTime? PriceDate = null,string ItemName = "", bool WithPriceOnly = false, Pager page = null);
        /// <summary>
        /// Gets the price list XML.
        /// </summary>
        /// <param name="facilityName">Name of the facility.</param>
        /// <param name="UserName">Name of the user.</param>
        /// <returns></returns>
        String GetPriceListXML(string facilityName, string UserName,DateTime? PriceDate = null);
        /// <summary>
        /// Saves the price list.
        /// </summary>
        /// <param name="dtPriceList">The dt price list.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        int SavePriceList(DataTable dtPriceList, int UserId);
        /// <summary>
        /// Saves the price list.
        /// </summary>
        /// <param name="itemList">The item list.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        int SavePriceList(List<SaleItem> itemList, int UserId);
        /// <summary>
        /// Saves the patient bill payments.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <param name="paymentInfo">The payment information.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns></returns>
        DataTable SavePatientBillPayments(int patientID, BillPaymentInfo paymentInfo,  int UserID);

        #region Reports
        /// <summary>
        /// Gets the reciept.
        /// </summary>
        /// <param name="TransactionID">The transaction identifier.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <param name="receiptType">Type of the receipt.</param>
        /// <returns></returns>
        DataSet GetReceipt(int TransactionID, int locationID, ReceiptType receiptType = ReceiptType.BillPayment);
        // int getCurrentBillID(int PatientID);
        /// <summary>
        /// Gets the daily collection summary report.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <returns></returns>
        DataSet getDailyCollectionSummaryReport(DateTime fromDate, DateTime toDate, int locationID);
        /// <summary>
        /// Gets the daily cashiers summary report.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <returns></returns>
        DataSet getDailyCashiersSummaryReport(DateTime fromDate, DateTime toDate, int locationID);
        /// <summary>
        /// Gets the daily dept collections report.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <returns></returns>
        DataSet getDailyDeptCollectionsReport(DateTime fromDate, DateTime toDate, int locationID);
        
        #endregion

        #region PaidItems
        /// <summary>
        /// Determines whether [is visit type paid] [the specified visittype identifier].
        /// </summary>
        /// <param name="visittypeID">The visittype identifier.</param>
        /// <param name="patientID">The patient identifier.</param>
        /// <returns></returns>
        DataTable isVisitTypePaid(int visittypeID, int patientID);
        /// <summary>
        /// Gets the paid labs.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <returns></returns>
        DataTable GetPaidLabs(int patientID);
        /// <summary>
        /// Gets the paid drugs.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <returns></returns>
        DataTable GetPaidDrugs(int patientID);  
        #endregion    
      
        #region Reversal
        /// <summary>
        /// Requests the bill reversal.
        /// </summary>
        /// <param name="TransactionID">The transaction identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <param name="ReversalReason">The reversal reason.</param>
        /// <param name="RequestDate">The request date.</param>
        /// <param name="ItemToReverse">The item to reverse.</param>
        void RequestTransactionReversal(int TransactionID, int UserID, string ReversalReason, DateTime RequestDate, List<int> ItemToReverse = null);
        /// <summary>
        /// Approves the bill reversal.
        /// </summary>
        /// <param name="ReversalID">The reversal identifier.</param>
        /// <param name="TransactionID">The transaction identifier.</param>
        /// <param name="Approved">if set to <c>true</c> [approved].</param>
        /// <param name="ApproverID">The approver identifier.</param>
        /// <param name="ApprovalReason">The approval reason.</param>
        /// <param name="ApprovalDate">The approval date.</param>
        /// <param name="RefundCash">if set to <c>true</c> [refund cash].</param>
        void ApproveRejectTransactionReversal(int ReversalID, int TransactionID, bool Approved, int ApproverID, string ApprovalReason, DateTime ApprovalDate, bool RefundCash=false);
        /// <summary>
        /// Itemses to be reversed.
        /// </summary>
        /// <param name="ReversalID">The reversal identifier.</param>
        /// <returns></returns>
        DataTable ItemsToBeReversed(int ReversalID);
        /// <summary>
        /// Gets the reversal requests.
        /// </summary>
        /// <param name="LocationID">The location identifier.</param>
        /// <param name="ReversalReference">The reversal reference.</param>
        /// <param name="FilterOption">The filter option.</param>
        /// <param name="PatientID">The patient identifier.</param>
        /// <returns></returns>
        DataTable GetReversalRequests(int LocationID, string ReversalReference = "", string FilterOption = "ALL", int? PatientID=null);

        /// <summary>
        /// Refunds the cash.
        /// </summary>
        /// <param name="ReversalId">The reversal identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        void RefundCash(int ReversalId, int UserID);
        #endregion

        /// <summary>
        /// Saves the patient payable items.
        /// </summary>
        /// <param name="billitems">The billitems.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns></returns>
        int SavePatientPayableItems(DataTable billitems, int UserID);
        /// <summary>
        /// Gets the patient un billed items.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <param name="LocationID">The location identifier.</param>
        /// <returns></returns>
        DataTable GetPatientUnBilledItems(int patientID, int LocationID);
        /// <summary>
        /// Generates the bill.
        /// </summary>
        /// <param name="billItems">The bill items.</param>
        /// <param name="PatientID">The patient identifier.</param>
        /// <param name="LocationID">The location identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns></returns>
        string GenerateBill(DataTable billItems, int PatientID,int LocationID, int UserID);
        /// <summary>
        /// Gets the patient bill by status.
        /// </summary>
        /// <param name="PatientID">The patient identifier.</param>
        /// <param name="LocationID">The location identifier.</param>
        /// <param name="billStatus">The bill status.</param>
        /// <returns></returns>
        DataTable GetPatientBillByStatus(int PatientID, int LocationID, BillStatus billStatus);
        /// <summary>
        /// Gets the bill items.
        /// </summary>
        /// <param name="BillID">The bill identifier.</param>
        /// <returns></returns>
        DataTable GetBillItems(int BillID);
        /// <summary>
        /// Gets the bill transactions.
        /// </summary>
        /// <param name="BillID">The bill identifier.</param>
        /// <returns></returns>
        DataTable GetBillTransactions(int BillID);
        /// <summary>
        /// Cancels the bill.
        /// </summary>
        /// <param name="BillID">The bill identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        void CancelBill(int BillID, int UserID);
        /// <summary>
        /// Gets the bill details.
        /// </summary>
        /// <param name="BillID">The bill identifier.</param>
        /// <param name="PatientID">The patient identifier.</param>
        /// <returns></returns>
        DataTable GetBillDetails(int BillID);
        /// <summary>
        /// Removes the item from bill.
        /// </summary>
        /// <param name="billItemID">The bill item identifier.</param>
        /// <param name="BillID">The bill identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        void RemoveItemFromBill(int billItemID, int BillID, int UserID);
      /// <summary>
        /// Gets the invoice.
        /// </summary>
        /// <param name="BillID">The bill identifier.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <param name="patientID">The patient identifier.</param>
        /// <returns></returns>
        DataSet GetInvoice(int BillID, int locationID, int patientID);
        /// <summary>
        /// Gets the item price.
        /// </summary>
        /// <param name="ItemID">The item identifier.</param>
        /// <param name="ItemTypeID">The item type identifier.</param>
        /// <param name="BillingDate">The billing date.</param>
        /// <returns></returns>
        decimal GetItemPrice(int ItemID, int ItemTypeID, DateTime BillingDate);

        #region Deposits
        /// <summary>
        /// Gets the patient deposit.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <returns></returns>
        DataTable GetPatientDeposit(int patientID, int locationID);

        /// <summary>
        /// Gets the patient deposit transactions.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <returns></returns>
        DataTable GetPatientDepositTransactions(int patientID, int locationID);

        /// <summary>
        /// Executes the deposit transaction.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="transactionType">Type of the transaction.</param>
        /// <param name="clsObject">The CLS object.</param>
        /// <returns></returns>
        DataTable ExecuteDepositTransaction(int patientID, int locationID, int userID, decimal amount, DepositTransactionType transactionType, object clsObject=null);
        #endregion
        DataSet getSalesSummary(DateTime fromDate, DateTime toDate, int locationID);
        /// <summary>
        /// Gets the payment methods.
        /// </summary>
        /// <param name="filterName">Name of the filter.</param>
        /// <returns></returns>
        List<PaymentMethod> GetPaymentMethods(string filterName = "");
        /// <summary>
        /// Saves the payment method.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="UserID">The user identifier.</param>
        void SavePaymentMethod(PaymentMethod p, int UserID);
        /// <summary>
        /// Gets or sets the print price list XSL.
        /// </summary>
        /// <value>
        /// The print price list XSL.
        /// </value>
       // string PrintPriceListXSL { get;  }
        
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IPayment
    {
        /// <summary>
        /// Gets or sets the amount due.
        /// </summary>
        /// <value>
        /// The amount due.
        /// </value>
        Decimal AmountDue { get; set; }
        /// <summary>
        /// Gets or sets the bill amount.
        /// </summary>
        /// <value>
        /// The bill amount.
        /// </value>
        Decimal BillAmount { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [allow partial payment].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow partial payment]; otherwise, <c>false</c>.
        /// </value>
        Decimal AmountToPay { get; set; }
        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        int PatientID { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        int UserID { get; set; }
        /// <summary>
        /// Gets or sets the bill identifier.
        /// </summary>
        /// <value>
        /// The bill identifier.
        /// </value>
        int BillID { get; set; }
        /// <summary>
        /// Gets or sets the bill location identifier.
        /// </summary>
        /// <value>
        /// The bill location identifier.
        /// </value>
        int BillLocationID { get; set; }
        /// <summary>
        /// Gets or sets the item for pay.
        /// </summary>
        /// <value>
        /// The item for pay.
        /// </value>
        Delegate ItemForPay { get; set; }
        /// <summary>
        /// Gets or sets the payment mode.
        /// </summary>
        /// <value>
        /// The payment mode.
        /// </value>
        PaymentMethod PaymentMode { get; set; }
        /// <summary>
        /// Gets or sets the payment option.
        /// </summary>
        /// <value>
        /// The payment option.
        /// </value>
        BillPaymentOptions BillPayOption { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has transaction.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has transaction; otherwise, <c>false</c>.
        /// </value>
        bool HasTransaction { get; set; }
        /// <summary>
        /// Rebinds this instance.
        /// </summary>
        void Rebind();
        /// <summary>
        /// Clears this instance.
        /// </summary>
        void Clear();
        /// <summary>
        /// Computes this instance.
        /// </summary>
        void Compute();
        /// <summary>
        /// Validates the specified bill payment information.
        /// </summary>
        /// <param name="billPaymentInfo">The bill payment information.</param>
        /// <returns></returns>
        void Validate();        
        /// <summary>
        /// Occurs when [cancel compute].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when compute balance is canceled.")]
        [System.ComponentModel.Bindable(true)]
        event CommandEventHandler CancelCompute;
        /// <summary>
        /// Occurs when [pay complete].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when finish payment events completes.")]
        [System.ComponentModel.Bindable(true)]
        event CommandEventHandler PayComplete;
        /// <summary>
        /// Occurs when [notify command].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when a notifcation need to be sent.")]
        [System.ComponentModel.Bindable(true)]
        event CommandEventHandler NotifyCommand;
        /// <summary>
        /// Occurs when [error occurred].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when an error occurs.")]
        [System.ComponentModel.Bindable(true)]
        event CommandEventHandler ErrorOccurred;
        /// <summary>
        /// Occurs when [execute payment].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised to execute payment.")]
        [System.ComponentModel.Bindable(true)]
        event CommandEventHandler ExecutePayment;
    }
   
   
}
