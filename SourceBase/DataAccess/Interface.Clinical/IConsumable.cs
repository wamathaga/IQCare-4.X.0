using System;
using System.Data;
namespace Interface.Clinical
{
    public interface IConsumable : IConsumableItem
    {
        /// <summary>
        /// Gets the name of the consumable by.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <returns></returns>
        DataTable GetConsumableByName(string searchText);
        /// <summary>
        /// Gets the patient consumable by date.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <param name="issueDate">The issue date.</param>
        /// <returns></returns>
        DataTable GetPatientConsumableByDate(int patientID, DateTime issueDate);
        /// <summary>
        /// Issues the consumable.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <param name="LocationID">The location identifier.</param>
        /// <param name="issueDate">The issue date.</param>
        /// <param name="userID">The user identifier.</param>
        /// <param name="quantity">The quantity.</param>
        /// <param name="moduleID">The module identifier.</param>
        /// <param name="itemConsumed">if set to <c>true</c> [item consumed]. not implemented</param>
        void IssueConsumable(int itemID, int itemTypeID,string itemName,float sellingPrice,int patientID, int LocationID,DateTime issueDate, int userID, int quantity, int moduleID, bool itemConsumed = true);
        /// <summary>
        /// Removes the consumable.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="itemIssueID">The item issue identifier.</param>
        void RemoveConsumable(int userID, int itemIssueID);
      
    }
}
