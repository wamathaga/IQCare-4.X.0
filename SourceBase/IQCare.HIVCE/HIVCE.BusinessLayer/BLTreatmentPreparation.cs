using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HIVCE.DataAccess;
using HIVCE.Common.Entities;
using Application.Common;
using DataAccess.Base;
using Interface.HIVCE;

namespace HIVCE.BusinessLayer
{
    [Serializable()]
    public class BLTreatmentPreparation : ProcessBase, ITreatmentPreparation
    {
        DBTreatmentPreparation dbTP;
        public TreatmentPreparation GetTreatmentPreparation(int ptn_pk)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BLTreatmentPreparation.GetTreatmentPreparation() method called");
            TreatmentPreparation tp = new TreatmentPreparation();
            try
            {
                dbTP = new DBTreatmentPreparation();
                DataTable dtTP = dbTP.GetTreatmentPreparation(ptn_pk);
                if (dtTP.Rows.Count > 0)
                {
                    tp = (from dt in dtTP.AsEnumerable()
                          select new TreatmentPreparation()
                          {
                              Id = dt.Field<int>("Id"),
                              Ptn_pk = dt.Field<int>("Ptn_pk"),
                              HIVIT = dt.Field<DateTime?>("HIVIT"),
                              HIVR = dt.Field<DateTime?>("HIVR"),
                              ViralLoadIT = dt.Field<DateTime?>("ViralLoadIT"),
                              ViralLoadR = dt.Field<DateTime?>("ViralLoadR"),
                              CD4IT = dt.Field<DateTime?>("CD4IT"),
                              CD4R = dt.Field<DateTime?>("CD4R"),
                              AntiretroviralIT = dt.Field<DateTime?>("AntiretroviralIT"),
                              AntiretroviralR = dt.Field<DateTime?>("AntiretroviralR"),
                              TreatmentFailureIT = dt.Field<DateTime?>("TreatmentFailureIT"),
                              TreatmentFailureR = dt.Field<DateTime?>("TreatmentFailureR"),
                              ARTSEIT = dt.Field<DateTime?>("ARTSEIT"),
                              ARTSER = dt.Field<DateTime?>("ARTSER"),
                              AdherenceIT = dt.Field<DateTime?>("AdherenceIT"),
                              AdherenceR = dt.Field<DateTime?>("AdherenceR"),
                              OtherMedicationsIT = dt.Field<DateTime?>("OtherMedicationsIT"),
                              OtherMedicationsR = dt.Field<DateTime?>("OtherMedicationsR"),
                              NutritionIT = dt.Field<DateTime?>("NutritionIT"),
                              NutritionR = dt.Field<DateTime?>("NutritionR"),
                              FollowUpIT = dt.Field<DateTime?>("FollowUpIT"),
                              FollowUpR = dt.Field<DateTime?>("FollowUpR"),
                              ARTReadinessAssessment = dt.Field<int?>("ARTReadinessAssessment"),
                              ManagementPlanIT = dt.Field<DateTime?>("ManagementPlanIT"),
                              ManagementPlanR = dt.Field<DateTime?>("ManagementPlanR"),
                              Notes = dt.Field<string>("Notes")
                          }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "BLTreatmentPreparation.GetTreatmentPreparation() Method:" + ex.ToString());
            }
            return tp;
        }

        public bool SaveUpdateTreatmentPreparation(TreatmentPreparation obj)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BLTreatmentPreparation.SaveUpdateTreatmentPreparation() method called");
            bool flag = true;
            TreatmentPreparation tp = new TreatmentPreparation();
            try
            {
                dbTP = new DBTreatmentPreparation();
                dbTP.SaveUpdateTreatmentPreparation(obj);
            }
            catch (Exception ex)
            {
                flag = false;
                CLogger.WriteLog(ELogLevel.ERROR, "BLTreatmentPreparation.SaveUpdateTreatmentPreparation() Method:" + ex.ToString());
            }
            return flag;
        }

    }
}
