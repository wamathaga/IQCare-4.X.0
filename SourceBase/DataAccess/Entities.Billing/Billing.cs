using System;
using System.Collections.Generic;

namespace Entities.Billing
{
    /// <summary>
    /// Bill Statuses
    /// </summary>
    public enum BillStatus
    {
        /// <summary>
        /// All
        /// </summary>
        All = 0,
        /// <summary>
        /// The open bills. Not fully settle
        /// </summary>
        Open = 1,
        /// <summary>
        /// The closed. Fully settled
        /// </summary>
        Closed = 2,
        /// <summary>
        /// The voided bills
        /// </summary>
        Voided = 3
    }
    /// <summary>
    /// 
    /// </summary>
    public enum BillPaymentOptions
    {
        /// <summary>
        /// The full bill
        /// </summary>
        FullBill = 1,
        /// <summary>
        /// The select item
        /// </summary>
        SelectItem = 2
    }
    [Serializable]
    public class BillItem
    {
        /// <summary>
        /// The bill item identifier
        /// </summary>
        public int BillItemID;
        /// <summary>
        /// The item identifier
        /// </summary>
        public int ItemID;

        /// <summary>
        /// The amount
        /// </summary>
        public Decimal Amount;
        /// <summary>
        /// The item name
        /// </summary>
        public string ItemName;


    }
    [Serializable]
    public class SaleItem
    {
        /// <summary>
        /// The item identifier
        /// </summary>
        public int ItemID { get; set; }
        /// <summary>
        /// The item type identifier
        /// </summary>
        public int ItemTypeID { get; set; }
        /// <summary>
        /// The selling price
        /// </summary>
        public decimal? SellingPrice { get; set; }
        /// <summary>
        /// The item name
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// The item type name
        /// </summary>
        public string ItemTypeName { get; set; }
        /// <summary>
        /// The price date
        /// </summary>
        public DateTime? PriceDate { get; set; }
        /// <summary>
        /// The priced per item
        /// </summary>
        /// <value>
        /// The priced per item.
        /// </value>
        public bool? PricedPerItem { get; set; }

        /// <summary>
        /// Gets or sets the version stamp.
        /// </summary>
        /// <value>
        /// The version stamp.
        /// </value>
        public UInt64? VersionStamp { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SaleItem"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Active { get; set; }
    }
    [Serializable]
    public class Pager
    {
        /// <summary>
        /// Gets or sets the page count.
        /// </summary>
        /// <value>
        /// The page count.
        /// </value>
        public int PageCount { get; set; }
        /// <summary>
        /// Gets or sets the index of the page.
        /// </summary>
        /// <value>
        /// The index of the page.
        /// </value>
        public int PageIndex { get; set; }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class ResultSet<T>
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public List<T> Items { get; set; }
        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The total numbe of pages based on the pager setting.
        /// </value>
        public int Count { get; set; }
    }
    /// <summary>
    /// Complete bill payment information. Contains all the details necessary to execute payment for a given bill id
    /// </summary>
    /// 
    [Serializable]
    public class BillPaymentInfo
    {
        /// <summary>
        /// The bill identifier
        /// </summary>
        public int BillID;
        /// <summary>
        /// The location identifier
        /// </summary>
        public int LocationID;
        /// <summary>
        /// The payment mode
        /// </summary>
        public PaymentMethod PaymentMode;
        /// <summary>
        /// The reference number
        /// </summary>
        public string ReferenceNumber;
        /// <summary>
        /// The amount
        /// </summary>
        public Decimal Amount;
        /// <summary>
        /// The amount payable
        /// </summary>
        public Decimal AmountPayable;
        /// <summary>
        /// The tendered amount
        /// </summary>
        public Decimal TenderedAmount;
        /// <summary>
        /// The deposit
        /// </summary>
        public Boolean Deposit;
        /// <summary>
        /// The print receipt
        /// </summary>
        public Boolean PrintReceipt = true;
        /// <summary>
        /// The items to pay
        /// </summary>
        public List<BillItem> ItemsToPay;

    }
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class PaymentMethod
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public int? ID { get; set; }
        /// <summary>
        /// The name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The active
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// The locked
        /// </summary>
        public bool Locked { get; set; }
        /// <summary>
        /// The method description
        /// </summary>
        public string MethodDescription { get; set; }
        /// <summary>
        /// The handler
        /// </summary>
        public string Handler { get; set; }
        /// <summary>
        /// The handler description
        /// </summary>
        public string HandlerDescription { get; set; }
        /// <summary>
        /// The control name
        /// </summary>
        public string ControlName { get; set; }
    }
    /// <summary>
    /// Types of receipts
    /// </summary>
    public enum ReceiptType
    {
        /// <summary>
        /// The bill payment in whatever mode
        /// </summary>
        BillPayment = 0,
        /// <summary>
        /// The deposit transaction either refund or new deposit
        /// </summary>
        DepositTransaction = 1,
        /// <summary>
        /// The reversal transaction
        /// </summary>
        ReversalTransaction = 2
    }
    /// <summary>
    /// Transaction Types allowable on deposits  only
    /// </summary>
    public enum DepositTransactionType
    {
        /// <summary>
        /// Making a deposit
        /// </summary>
        MakeDeposit = 1,
        /// <summary>
        /// The settlement. Using the deposit to clear bills
        /// </summary>
        Settlement = 2,
        /// <summary>
        /// Return available deposit to the client
        /// </summary>
        ReturnDeposit = 3
    }

}
