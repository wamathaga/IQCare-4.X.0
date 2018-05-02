using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Interface.Pharmacy;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;
using System.Collections.Generic;

namespace BusinessProcess.Pharmacy
{

    public class BIQTouchImmunisation : ProcessBase, IBIQTouchImmunisation
    {
        public BIQTouchImmunisation()
        {
        }

        public int SaveUpdateImmunisationDetail(List<BIQTouchmmunisationFields> immnisationFields)
        {

            ClsObject immunisation = new ClsObject();
            int theRowAffected = 0;
            try
            {
                this.Connection = DataMgr.GetConnection();
                this.Transaction = DataMgr.BeginTransaction(this.Connection);

                immunisation.Connection = this.Connection;
                immunisation.Transaction = this.Transaction;
                DataRow theDR;

                foreach (var Value in immnisationFields)
                {
                    ClsUtility.Init_Hashtable();
                    ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Value.Ptnpk.ToString());
                    ClsUtility.AddParameters("@LocationId", SqlDbType.Int, Value.LocationId.ToString());
                    ClsUtility.AddParameters("@Immunisation_code", SqlDbType.VarChar, Value.ImmunisationCode.ToString());

                    if (Value.ImmunisationDate.Year.ToString() != "1900")
                    {
                        ClsUtility.AddParameters("@ImmunisationDate", SqlDbType.VarChar, String.Format("{0:dd-MMM-yyyy}", Value.ImmunisationDate));
                    }


                    ClsUtility.AddParameters("@ImmunisationCU", SqlDbType.Int, Value.ImmunisationCU.ToString());
                    ClsUtility.AddParameters("@UserId", SqlDbType.Int, Value.UserId.ToString());
                    ClsUtility.AddParameters("@CardAvailabe", SqlDbType.Int, Value.CardAvailable.ToString());
                    ClsUtility.AddParameters("@ImmunisationOther", SqlDbType.VarChar, Value.ImmunisationOther);
                    ClsUtility.AddParameters("@Flag", SqlDbType.Int, Value.Flag.ToString());
                    theRowAffected = (int)immunisation.ReturnObject(ClsUtility.theParams, "Pr_IQTouch_Pharmacy_AddUpdateImmunisation", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
                    if (theRowAffected == 0)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["MessageText"] = "Error in Saving Immunisation Details. Try Again..";
                        AppException.Create("#C1", theMsg);

                    }
                }
                DataMgr.CommitTransaction(this.Transaction);
                DataMgr.ReleaseConnection(this.Connection);


            }
            catch
            {
                DataMgr.RollBackTransation(this.Transaction);
                throw;
            }
            finally
            {
                immunisation = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }

            return theRowAffected;

        }
        public DataSet GetImmunisationDetails(BIQTouchmmunisationFields immnisationFields)
        {
            lock (this)
            {
                ClsObject ImmunisationManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, immnisationFields.Ptnpk.ToString());
                ClsUtility.AddParameters("@LocationId", SqlDbType.Int, immnisationFields.LocationId.ToString());
                ClsUtility.AddParameters("@Flag", SqlDbType.Int, immnisationFields.Flag.ToString());
                return (DataSet)ImmunisationManager.ReturnObject(ClsUtility.theParams, "Pr_IQTouch_Pharmacy_GetImmunisationDetails", ClsDBUtility.ObjectEnum.DataSet);
            }
            throw new NotImplementedException();
        }
    }

}
