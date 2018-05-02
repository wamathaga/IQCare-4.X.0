using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using System.Data;
using DataAccess.Entity;
using DataAccess.Common;
using HIVCE.Common.Entities;
using Application.Common;

namespace HIVCE.DataAccess
{
    public class DBAlcoholDepressionScreening : ProcessBase
    {
        public DBAlcoholDepressionScreening()
        { }

        public DataTable GetAlcoholDepressionScreening(int ptn_pk, int visitPK)
        {
            CLogger.WriteLog(ELogLevel.INFO, "DBAlcoholDepressionScreening.GetAlcoholDepressionScreening() Method Called.");
            DataTable dt = new DataTable();
            ClsObject clsObjTP = new ClsObject();
            try
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Id", SqlDbType.Int, visitPK.ToString());
                dt = (DataTable)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_GetAlcoholDepressionScreening", ClsDBUtility.ObjectEnum.DataTable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                clsObjTP = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }
            return dt;
        }

        public void SaveUpdateAlcoholDepressionScreening(AlcoholDepressionScreeningObj obj)
        {
            CLogger.WriteLog(ELogLevel.INFO, "DBAlcoholDepressionScreening.SaveUpdateAlcoholDepressionScreening() method called");
            bool flag = true;
            ClsObject clsObjTP = new ClsObject();
            try
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@w_Id", SqlDbType.Int, obj.Id.ToString());
                ClsUtility.AddParameters("@w_Ptn_pk", SqlDbType.Int, obj.Ptn_pk.ToString());
                ClsUtility.AddParameters("@w_Visit_Id", SqlDbType.Int, obj.Visit_Id.ToString());
                ClsUtility.AddParameters("@p_IsFeelingDown", SqlDbType.Int, obj.IsFeelingDown.ToString());
                ClsUtility.AddParameters("@p_IsLittleInterest", SqlDbType.Int, obj.IsLittleInterest.ToString());
                ClsUtility.AddParameters("@p_PHQLittleInterest", SqlDbType.Int, obj.PHQLittleInterest.ToString());
                ClsUtility.AddParameters("@p_PHQFeelingDown", SqlDbType.Int, obj.PHQFeelingDown.ToString());
                ClsUtility.AddParameters("@p_PHQTroubleSleep", SqlDbType.Int, obj.PHQTroubleSleep.ToString());
                ClsUtility.AddParameters("@p_PHQTiredLittleEnergy", SqlDbType.Int, obj.PHQTiredLittleEnergy.ToString());
                ClsUtility.AddParameters("@p_PHQAppetite", SqlDbType.Int, obj.PHQAppetite.ToString());
                ClsUtility.AddParameters("@p_PHQYourselfDown", SqlDbType.Int, obj.PHQYourselfDown.ToString());
                ClsUtility.AddParameters("@p_PHQTroubleConcentrating", SqlDbType.Int, obj.PHQTroubleConcentrating.ToString());
                ClsUtility.AddParameters("@p_PHQMovingSlowly", SqlDbType.Int, obj.PHQMovingSlowly.ToString());
                ClsUtility.AddParameters("@p_PHQFidgetyRestless", SqlDbType.Int, obj.PHQFidgetyRestless.ToString());
                ClsUtility.AddParameters("@p_PHQHurtingYourself", SqlDbType.Int, obj.PHQHurtingYourself.ToString());
                ClsUtility.AddParameters("@p_PHQTotal", SqlDbType.Int, obj.PHQTotal.ToString());
                ClsUtility.AddParameters("@p_PHQDepressionSeverity", SqlDbType.VarChar, obj.PHQDepressionSeverity.ToString());
                ClsUtility.AddParameters("@p_PHQRecommended", SqlDbType.VarChar, obj.PHQRecommended.ToString());
                ClsUtility.AddParameters("@p_SGBQ1", SqlDbType.Int, obj.SGBQ1.ToString());
                ClsUtility.AddParameters("@p_SGBQ2", SqlDbType.Int, obj.SGBQ2.ToString());
                ClsUtility.AddParameters("@p_SGBQ3", SqlDbType.Int, obj.SGBQ3.ToString());
                ClsUtility.AddParameters("@p_SGBQ4", SqlDbType.Int, obj.SGBQ4.ToString());
                ClsUtility.AddParameters("@p_SGBQ5", SqlDbType.Int, obj.SGBQ5.ToString());
                ClsUtility.AddParameters("@p_DisclosureHIVStatus", SqlDbType.Int, obj.DisclosureHIVStatus.ToString());
                ClsUtility.AddParameters("@p_DisclosureStatus", SqlDbType.Int, obj.DisclosureStatus.ToString());
                ClsUtility.AddParameters("@p_DisclosureTo", SqlDbType.Int, obj.DisclosureTo.ToString());
                ClsUtility.AddParameters("@p_CrafftAlcohol", SqlDbType.Int, obj.CrafftAlcohol.ToString());
                ClsUtility.AddParameters("@p_CrafftSmoke", SqlDbType.Int, obj.CrafftSmoke.ToString());
                ClsUtility.AddParameters("@p_CrafftAnythingHigh", SqlDbType.Int, obj.CrafftAnythingHigh.ToString());
                ClsUtility.AddParameters("@p_CrafftC", SqlDbType.Int, obj.CrafftC.ToString());
                ClsUtility.AddParameters("@p_CrafftR", SqlDbType.Int, obj.CrafftR.ToString());
                ClsUtility.AddParameters("@p_CrafftA", SqlDbType.Int, obj.CrafftA.ToString());
                ClsUtility.AddParameters("@p_CrafftF1", SqlDbType.Int, obj.CrafftF1.ToString());
                ClsUtility.AddParameters("@p_CrafftF2", SqlDbType.Int, obj.CrafftF2.ToString());
                ClsUtility.AddParameters("@p_CrafftT", SqlDbType.Int, obj.CrafftT.ToString());
                ClsUtility.AddParameters("@p_CrafftScore", SqlDbType.Int, obj.CrafftScore.ToString());
                ClsUtility.AddParameters("@p_CrafftRisk", SqlDbType.VarChar, obj.CrafftRisk.ToString());
                ClsUtility.AddParameters("@p_CageAIDAlcohol", SqlDbType.Int, obj.CageAIDAlcohol.ToString());
                ClsUtility.AddParameters("@p_CageAIDDrugs", SqlDbType.Int, obj.CageAIDDrugs.ToString());
                ClsUtility.AddParameters("@p_CageAIDSmoke", SqlDbType.Int, obj.CageAIDSmoke.ToString());
                ClsUtility.AddParameters("@p_CageAIDQ1", SqlDbType.Int, obj.CageAIDQ1.ToString());
                ClsUtility.AddParameters("@p_CageAIDQ2", SqlDbType.Int, obj.CageAIDQ2.ToString());
                ClsUtility.AddParameters("@p_CageAIDQ3", SqlDbType.Int, obj.CageAIDQ3.ToString());
                ClsUtility.AddParameters("@p_CageAIDQ4", SqlDbType.Int, obj.CageAIDQ4.ToString());
                ClsUtility.AddParameters("@p_CageAIDScore", SqlDbType.Int, obj.CageAIDScore.ToString());
                ClsUtility.AddParameters("@p_CageAIDRisk", SqlDbType.VarChar, obj.CageAIDRisk.ToString());
                ClsUtility.AddParameters("@p_CageAIDStopSmoking", SqlDbType.Int, obj.CageAIDStopSmoking.ToString());
                ClsUtility.AddParameters("@Notes", SqlDbType.VarChar, obj.Notes.ToString());


                DataTable dt = (DataTable)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_UpdateAlcoholDepressionScreening", ClsDBUtility.ObjectEnum.DataTable);
            }
            catch (Exception ex)
            {
                flag = false;
                throw ex;
            }
            finally
            {
                clsObjTP = null;
                if (this.Connection != null)
                    DataMgr.ReleaseConnection(this.Connection);
            }

        }
    }
}
