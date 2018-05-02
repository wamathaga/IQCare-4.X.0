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
    public class DBTransition : ProcessBase
    {
        public DBTransition()
        { }

        public DataTable GetTransition(int ptn_pk, int visitPK)
        {
            CLogger.WriteLog(ELogLevel.INFO, "DBTransition.GetTransition() Method Called.");
            DataTable dt = new DataTable();
            ClsObject clsObjTP = new ClsObject();
            try
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Id", SqlDbType.Int, visitPK.ToString());
                dt = (DataTable)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_GetPatientTransition", ClsDBUtility.ObjectEnum.DataTable);
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

        public void SaveUpdateTransition(Transition obj)
        {
            CLogger.WriteLog(ELogLevel.INFO, "DBTransition.SaveUpdateTransition() method called");
            bool flag = true;
            ClsObject clsObjTP = new ClsObject();
            try
            {
                ClsUtility.Init_Hashtable();
                ClsUtility.AddParameters("@Id", SqlDbType.Int, obj.Id.ToString());
                ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, obj.Ptn_pk.ToString());
                ClsUtility.AddParameters("@Visit_Id", SqlDbType.Int, obj.Visit_Id.ToString());
                ClsUtility.AddParameters("@G1Q1", SqlDbType.Int, obj.G1Q1.ToString());
                ClsUtility.AddParameters("@G1Q2", SqlDbType.Int, obj.G1Q2.ToString());
                ClsUtility.AddParameters("@G1Q3", SqlDbType.Int, obj.G1Q3.ToString());
                ClsUtility.AddParameters("@G1Q4", SqlDbType.Int, obj.G1Q4.ToString());

                ClsUtility.AddParameters("@G2Q1", SqlDbType.Int, obj.G2Q1.ToString());
                ClsUtility.AddParameters("@G2Q1Date", SqlDbType.DateTime, obj.G2Q1Date.ToString());
                ClsUtility.AddParameters("@G2Q2", SqlDbType.Int, obj.G2Q2.ToString());
                ClsUtility.AddParameters("@G2Q2Date", SqlDbType.DateTime, obj.G2Q2Date.ToString());
                ClsUtility.AddParameters("@G2Q3", SqlDbType.Int, obj.G2Q3.ToString());
                ClsUtility.AddParameters("@G2Q3Date", SqlDbType.DateTime, obj.G2Q3Date.ToString());
                ClsUtility.AddParameters("@G3Q1", SqlDbType.Int, obj.G3Q1.ToString());

                ClsUtility.AddParameters("@G3Q1Date", SqlDbType.DateTime, obj.G3Q1Date.ToString());
                ClsUtility.AddParameters("@G3Q2", SqlDbType.Int, obj.G3Q2.ToString());
                ClsUtility.AddParameters("@G3Q2Date", SqlDbType.DateTime, obj.G3Q2Date.ToString());
                ClsUtility.AddParameters("@G4Q1", SqlDbType.Int, obj.G4Q1.ToString());
                ClsUtility.AddParameters("@G4Q1Date", SqlDbType.DateTime, obj.G4Q1Date.ToString());
                ClsUtility.AddParameters("@G4Q2", SqlDbType.Int, obj.G4Q2.ToString());
                ClsUtility.AddParameters("@G4Q2Date", SqlDbType.DateTime, obj.G4Q2Date.ToString());
                ClsUtility.AddParameters("@G4Q3", SqlDbType.Int, obj.G4Q3.ToString());
                ClsUtility.AddParameters("@G4Q3Date", SqlDbType.DateTime, obj.G4Q3Date.ToString());
                ClsUtility.AddParameters("@G4Q4", SqlDbType.Int, obj.G4Q4.ToString());
                ClsUtility.AddParameters("@G4Q4Date", SqlDbType.DateTime, obj.G4Q4Date.ToString());

                

                DataTable dt = (DataTable)clsObjTP.ReturnObject(ClsUtility.theParams, "Pr_HIVCE_UpdateTransition", ClsDBUtility.ObjectEnum.DataTable);
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
