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
    public class BLARTReadinessAssessment : ProcessBase, IARTReadinessAssessment
    {
        DBARTReadinessAssessment dbLayer;

        public ARTReadinessAssessment GetARTReadinessAssessment(int ptn_pk, int visitPK)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BLARTReadinessAssessment.GetARTReadinessAssessment() method called");
            ARTReadinessAssessment tp = new ARTReadinessAssessment();
            try
            {
                dbLayer = new DBARTReadinessAssessment();
                DataTable dtARA = dbLayer.GetARTReadinessAssessment(ptn_pk, visitPK);
                if (dtARA.Rows.Count > 0)
                {
                    tp = (from dt in dtARA.AsEnumerable()
                          select new ARTReadinessAssessment()
                          {
                              Id = dt.Field<int>("Id"),
                              Ptn_pk = dt.Field<int>("Ptn_pk"),
                              Visit_Id = dt.Field<int>("Visit_Id"),
                              PsychosocialQ1 = dt.Field<int?>("PsychosocialQ1"),
                              PsychosocialQ2 = dt.Field<int?>("PsychosocialQ2"),
                              PsychosocialQ3 = dt.Field<int?>("PsychosocialQ3"),
                              PsychosocialQ4 = dt.Field<int?>("PsychosocialQ4"),
                              PsychosocialQ5 = dt.Field<int?>("PsychosocialQ5"),
                              PsychosocialQ6 = dt.Field<int?>("PsychosocialQ6"),
                              PsychosocialQ7 = dt.Field<int?>("PsychosocialQ7"),
                              PsychosocialQ8 = dt.Field<int?>("PsychosocialQ8"),
                              PsychosocialQ9 = dt.Field<int?>("PsychosocialQ9"),
                              PsychosocialQ10 = dt.Field<int?>("PsychosocialQ10"),
                              SupportQ1 = dt.Field<int?>("SupportQ1"),
                              SupportQ2 = dt.Field<int?>("SupportQ2"),
                              SupportQ3 = dt.Field<int?>("SupportQ3"),
                              SupportQ4 = dt.Field<int?>("SupportQ4"),
                              SupportQ5 = dt.Field<int?>("SupportQ5")
                          }).FirstOrDefault();
                }


            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "BLARTReadinessAssessment.GetARTReadinessAssessment() Method:" + ex.ToString());
            }
            return tp;
        }

        public bool SaveUpdateARTReadinessAssessment(ARTReadinessAssessment obj)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BLARTReadinessAssessment.SaveUpdateARTReadinessAssessment() method called");
            bool flag = true;
            ARTReadinessAssessment tp = new ARTReadinessAssessment();
            try
            {
                if (obj.Ptn_pk != 0)
                {
                    dbLayer = new DBARTReadinessAssessment();
                    dbLayer.SaveUpdateARTReadinessAssessment(obj);
                }
                else
                {
                    throw new Exception("Patient id can not be 0");
                }
            }
            catch (Exception ex)
            {
                flag = false;
                CLogger.WriteLog(ELogLevel.ERROR, "BLARTReadinessAssessment.SaveUpdateARTReadinessAssessment() Method:" + ex.ToString());
                throw ex;
            }
            return flag;
        }
    }
}
