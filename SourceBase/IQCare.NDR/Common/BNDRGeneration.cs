using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Interface.NDR;
using DataAccess.Base;
using DataAccess.Entity;
using DataAccess.Common;

namespace BusinessProcess.NDR
{
    public class BNDRGeneration : ProcessBase, INDRGeneration
    {
        public static string DBSecurity = "'ttwbvXWpqb5WOLfLrBgisw=='";
        public BNDRGeneration()
        {

        }

        public DataSet GetPatientDetails(int facilityID)
        {
            lock (this)
            {
                ClsObject clsObject = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@PosId", SqlDbType.Int, facilityID.ToString());
                ClsUtility.AddParameters("@DBKey", SqlDbType.VarChar, DBSecurity);
                ClsObject UserManager = new ClsObject();
                return (DataSet)clsObject.ReturnObject(ClsUtility.theParams, "pr_NDR_GetPatientDetails", ClsDBUtility.ObjectEnum.DataSet);
            }
        }

        public void SavePatientDetails(int patientId)
        {
            lock (this)
            {
                ClsObject clsObject = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_Pk", SqlDbType.Int, patientId.ToString());
                ClsObject UserManager = new ClsObject();
                int RowsAffected = (Int32)clsObject.ReturnObject(ClsUtility.theParams, "pr_NDR_SavePatient", ClsDBUtility.ObjectEnum.ExecuteNonQuery);
            }
        }
    }
}
