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
    public class BLTransition : ProcessBase, ITransition
    {
        DBTransition dbLayer;

        public Transition GetTransition(int ptn_pk, int visitPK)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BLTransition.GetTransition() method called");
            Transition tp = new Transition();
            try
            {
                dbLayer = new DBTransition();
                DataTable dtARA = dbLayer.GetTransition(ptn_pk, visitPK);
                if (dtARA.Rows.Count > 0)
                {
                    tp = (from dt in dtARA.AsEnumerable()
                          select new Transition()
                          {
                              Id = dt.Field<int>("Id"),
                              Ptn_pk = dt.Field<int>("Ptn_pk"),
                              Visit_Id = dt.Field<int>("Visit_Id"),
                              G1Q1 = dt.Field<int?>("G1Q1"),
                              G1Q2 = dt.Field<int?>("G1Q2"),
                              G1Q3 = dt.Field<int?>("G1Q3"),
                              G1Q4 = dt.Field<int?>("G1Q4"),
                              G2Q1 = dt.Field<int?>("G2Q1"),
                              G2Q1Date = dt.Field<DateTime?>("G2Q1Date"),
                              G2Q2 = dt.Field<int?>("G2Q2"),
                              G2Q2Date = dt.Field<DateTime?>("G2Q2Date"),
                              G2Q3 = dt.Field<int?>("G2Q3"),
                              G2Q3Date = dt.Field<DateTime?>("G2Q3Date"),
                              G3Q1 = dt.Field<int?>("G3Q1"),
                              G3Q1Date = dt.Field<DateTime?>("G3Q1Date"),
                              G3Q2 = dt.Field<int?>("G3Q2"),
                              G3Q2Date = dt.Field<DateTime?>("G3Q2Date"),
                              G4Q1 = dt.Field<int?>("G4Q1"),
                              G4Q1Date = dt.Field<DateTime?>("G4Q1Date"),
                              G4Q2 = dt.Field<int?>("G4Q2"),
                              G4Q2Date = dt.Field<DateTime?>("G4Q2Date"),
                              G4Q3 = dt.Field<int?>("G4Q3"),
                              G4Q3Date = dt.Field<DateTime?>("G4Q3Date"),
                              G4Q4 = dt.Field<int?>("G4Q4"),
                              G4Q4Date = dt.Field<DateTime?>("G4Q4Date")
                          }).FirstOrDefault();
                }


            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "BLTransition.GetTransition() Method:" + ex.ToString());
            }
            return tp;
        }

        public bool SaveUpdateTransition(Transition obj)
        {
            CLogger.WriteLog(ELogLevel.INFO, "BLTransition.SaveUpdateTransition() method called");
            bool flag = true;
            Transition tp = new Transition();
            try
            {
                if (obj.Ptn_pk != 0)
                {
                    dbLayer = new DBTransition();
                    dbLayer.SaveUpdateTransition(obj);
                }
                else
                {
                    throw new Exception("Patient id can not be 0");
                }
            }
            catch (Exception ex)
            {
                flag = false;
                CLogger.WriteLog(ELogLevel.ERROR, "BLTransition.SaveUpdateTransition() Method:" + ex.ToString());
                throw ex;
            }
            return flag;
        }
    }
}
