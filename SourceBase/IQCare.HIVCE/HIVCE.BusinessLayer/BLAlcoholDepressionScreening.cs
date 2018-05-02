using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HIVCE.DataAccess;
using HIVCE.Common.Entities;
using Application.Common;
using HIVCE.Common;
using DataAccess.Base;
using Interface.HIVCE;

namespace HIVCE.BusinessLayer
{
    [Serializable()]
    public class BLAlcoholDepressionScreening : ProcessBase, IAlcoholDepressionScreening
    {
        DBAlcoholDepressionScreening dbLayer;

        public AlcoholDepressionScreeningObj GetAlcoholDepressionScreening(int ptn_pk, int visitPK)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BLARTReadinessAssessment.GetAlcoholDepressionScreening() method called");
            List<AlcoholDepressionScreeningObj> adsList = new List<AlcoholDepressionScreeningObj>();
            try
            {
                dbLayer = new DBAlcoholDepressionScreening();
                DataTable dtARA = dbLayer.GetAlcoholDepressionScreening(ptn_pk, visitPK);
                if (dtARA.Rows.Count > 0)
                {
                    adsList = dtARA.ConvertToList<AlcoholDepressionScreeningObj>();
                }
            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "BLARTReadinessAssessment.GetAlcoholDepressionScreening() Method:" + ex.ToString());
            }
            return adsList.FirstOrDefault();
        }

        public bool SaveUpdateAlcoholDepressionScreening(AlcoholDepressionScreeningObj obj)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BLAlcoholDepressionScreening.SaveUpdateAlcoholDepressionScreening() method called");
            bool flag = true;
            ARTReadinessAssessment tp = new ARTReadinessAssessment();
            try
            {
                if (obj.Ptn_pk != 0)
                {
                    dbLayer = new DBAlcoholDepressionScreening();
                    dbLayer.SaveUpdateAlcoholDepressionScreening(obj);
                }
                else
                {
                    throw new Exception("Patient id can not be 0");
                }
            }
            catch (Exception ex)
            {
                flag = false;
                CLogger.WriteLog(ELogLevel.ERROR, "BLAlcoholDepressionScreening.SaveUpdateAlcoholDepressionScreening() Method:" + ex.ToString());
            }
            return flag;
        }
    }
}
