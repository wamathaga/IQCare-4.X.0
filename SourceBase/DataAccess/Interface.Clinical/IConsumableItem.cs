
using System;
namespace Interface.Clinical
{
    public interface IConsumableItem
    {
        /// <summary>
        /// Gets or sets the item identifier.
        /// </summary>
        /// <value>
        /// The item identifier.
        /// </value>
        int ItemID { get; set; }
        /// <summary>
        /// Gets or sets the item type identifier.
        /// </summary>
        /// <value>
        /// The item type identifier.
        /// </value>
        int ItemTypeID { get; set; }
        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        /// <value>
        /// The name of the item.
        /// </value>
        string ItemName { get; set; }
        /// <summary>
        /// Gets or sets the name of the item type.
        /// </summary>
        /// <value>
        /// The name of the item type.
        /// </value>
        string ItemTypeName { get; set; }
        /// <summary>
        /// Gets or sets the selling price.
        /// </summary>
        /// <value>
        /// The selling price.
        /// </value>
        float SellingPrice { get; set; }
        /// <summary>
        /// Calculates the discount.
        /// </summary>
        /// <param name="itemID">The item identifier.</param>
        /// <param name="patientID">The patient identifier.</param>
        /// <param name="issueDate">The issue date.</param>
        /// <param name="moduleID">The module identifier.</param>
        /// <returns></returns>
        float CalculateDiscount(int patientID, DateTime issueDate, int moduleID);
    }
}
