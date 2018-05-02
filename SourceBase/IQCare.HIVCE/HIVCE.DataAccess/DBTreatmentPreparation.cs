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
    public class DBTreatmentPreparation : ProcessBase
    {
        public DBTreatmentPreparation()
        {

        }

        public DataTable GetTreatmentPreparation(int ptn_pk)
        {
            CLogger.WriteLog(ELogLevel.INFO, "DBTreatmentPreparation.GetTreatmentPreparation() Method Called.");
            DataTable dt = new DataTable();
            ClsObject clsObjTP = new ClsObject();
            try
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                dt = (DataTable)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_GetTreatmentPreparation", ClsDBUtility.ObjectEnum.DataTable);
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

        public void SaveUpdateTreatmentPreparation(TreatmentPreparation obj)
        {
            CLogger.WriteLog(ELogLevel.INFO, "DBTreatmentPreparation.SaveUpdateTreatmentPreparation() method called");
            bool flag = true;
            ClsObject clsObjTP = new ClsObject();
            try
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Id", SqlDbType.Int, obj.Id.ToString());
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, obj.Ptn_pk.ToString());
                ClsUtility.AddParameters("@HIVIT", SqlDbType.DateTime, obj.HIVIT.ToString());
                ClsUtility.AddParameters("@HIVR", SqlDbType.DateTime, obj.HIVR.ToString());
                ClsUtility.AddParameters("@ViralLoadIT", SqlDbType.DateTime, obj.ViralLoadIT.ToString());
                ClsUtility.AddParameters("@ViralLoadR", SqlDbType.DateTime, obj.ViralLoadR.ToString());
                ClsUtility.AddParameters("@CD4IT", SqlDbType.DateTime, obj.CD4IT.ToString());
                ClsUtility.AddParameters("@CD4R", SqlDbType.DateTime, obj.CD4R.ToString());
                ClsUtility.AddParameters("@AntiretroviralIT", SqlDbType.DateTime, obj.AntiretroviralIT.ToString());
                ClsUtility.AddParameters("@AntiretroviralR", SqlDbType.DateTime, obj.AntiretroviralR.ToString());
                ClsUtility.AddParameters("@TreatmentFailureIT", SqlDbType.DateTime, obj.TreatmentFailureIT.ToString());
                ClsUtility.AddParameters("@TreatmentFailureR", SqlDbType.DateTime, obj.TreatmentFailureR.ToString());
                ClsUtility.AddParameters("@ARTSEIT", SqlDbType.DateTime, obj.ARTSEIT.ToString());
                ClsUtility.AddParameters("@ARTSER", SqlDbType.DateTime, obj.ARTSER.ToString());
                ClsUtility.AddParameters("@AdherenceIT", SqlDbType.DateTime, obj.AdherenceIT.ToString());
                ClsUtility.AddParameters("@AdherenceR", SqlDbType.DateTime, obj.AdherenceR.ToString());
                ClsUtility.AddParameters("@OtherMedicationsIT", SqlDbType.DateTime, obj.OtherMedicationsIT.ToString());
                ClsUtility.AddParameters("@OtherMedicationsR", SqlDbType.DateTime, obj.OtherMedicationsR.ToString());
                ClsUtility.AddParameters("@NutritionIT", SqlDbType.DateTime, obj.NutritionIT.ToString());
                ClsUtility.AddParameters("@NutritionR", SqlDbType.DateTime, obj.NutritionR.ToString());
                ClsUtility.AddParameters("@FollowUpIT", SqlDbType.DateTime, obj.FollowUpIT.ToString());
                ClsUtility.AddParameters("@FollowUpR", SqlDbType.DateTime, obj.FollowUpR.ToString());
                ClsUtility.AddParameters("@ARTReadinessAssessment", SqlDbType.Int, obj.ARTReadinessAssessment.ToString());
                ClsUtility.AddParameters("@ManagementPlanIT", SqlDbType.DateTime, obj.ManagementPlanIT.ToString());
                ClsUtility.AddParameters("@ManagementPlanR", SqlDbType.DateTime, obj.ManagementPlanR.ToString());
                ClsUtility.AddParameters("@Notes", SqlDbType.VarChar, obj.Notes.ToString());

                DataTable dt = (DataTable)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_UpdateTreatmentPreparation", ClsDBUtility.ObjectEnum.DataTable);
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
