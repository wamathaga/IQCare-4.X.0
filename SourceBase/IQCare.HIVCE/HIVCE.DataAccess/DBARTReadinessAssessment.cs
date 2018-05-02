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
    public class DBARTReadinessAssessment : ProcessBase
    {
        public DBARTReadinessAssessment()
        { }

        public DataTable GetARTReadinessAssessment(int ptn_pk, int visitPK)
        {
            CLogger.WriteLog(ELogLevel.INFO, "DBARTReadinessAssessment.GetTreatmentPreparation() Method Called.");
            DataTable dt = new DataTable();
            ClsObject clsObjTP = new ClsObject();
            try
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Id", SqlDbType.Int, visitPK.ToString());
                dt = (DataTable)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_GetARTReadinessAssessment", ClsDBUtility.ObjectEnum.DataTable);
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

        public void SaveUpdateARTReadinessAssessment(ARTReadinessAssessment obj)
        {
            CLogger.WriteLog(ELogLevel.INFO, "DBARTReadinessAssessment.SaveUpdateARTReadinessAssessment() method called");
            bool flag = true;
            ClsObject clsObjTP = new ClsObject();
            try
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Id", SqlDbType.Int, obj.Id.ToString());
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, obj.Ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Id", SqlDbType.Int, obj.Visit_Id.ToString());
                ClsUtility.AddParameters("@PsychosocialQ1", SqlDbType.Int, obj.PsychosocialQ1.ToString());
                ClsUtility.AddParameters("@PsychosocialQ2", SqlDbType.Int, obj.PsychosocialQ2.ToString());
                ClsUtility.AddParameters("@PsychosocialQ3", SqlDbType.Int, obj.PsychosocialQ3.ToString());
                ClsUtility.AddParameters("@PsychosocialQ4", SqlDbType.Int, obj.PsychosocialQ4.ToString());
                ClsUtility.AddParameters("@PsychosocialQ5", SqlDbType.Int, obj.PsychosocialQ5.ToString());
                ClsUtility.AddParameters("@PsychosocialQ6", SqlDbType.Int, obj.PsychosocialQ6.ToString());
                ClsUtility.AddParameters("@PsychosocialQ7", SqlDbType.Int, obj.PsychosocialQ7.ToString());
                ClsUtility.AddParameters("@PsychosocialQ8", SqlDbType.Int, obj.PsychosocialQ8.ToString());
                ClsUtility.AddParameters("@PsychosocialQ9", SqlDbType.Int, obj.PsychosocialQ9.ToString());
                ClsUtility.AddParameters("@PsychosocialQ10", SqlDbType.Int, obj.PsychosocialQ10.ToString());
                ClsUtility.AddParameters("@SupportQ1", SqlDbType.Int, obj.SupportQ1.ToString());
                ClsUtility.AddParameters("@SupportQ2", SqlDbType.Int, obj.SupportQ2.ToString());
                ClsUtility.AddParameters("@SupportQ3", SqlDbType.Int, obj.SupportQ3.ToString());
                ClsUtility.AddParameters("@SupportQ4", SqlDbType.Int, obj.SupportQ4.ToString());
                ClsUtility.AddParameters("@SupportQ5", SqlDbType.Int, obj.SupportQ5.ToString());
                
                DataTable dt = (DataTable)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_UpdateARTReadinessAssessment", ClsDBUtility.ObjectEnum.DataTable);
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
