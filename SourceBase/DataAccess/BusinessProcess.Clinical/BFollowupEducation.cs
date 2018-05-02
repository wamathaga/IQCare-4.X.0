using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;
using Interface.Clinical;

namespace BusinessProcess.Clinical
{
   public class BFollowupEducation : ProcessBase, IFollowupEducation
    {
       public int SaveFollowupEducation(int Id, int Ptn_pk, int CouncellingTypeId, int CouncellingTopicId, int Visit_pk, int LocationID, string VisitDate, string Comments, string OtherDetail,int UserId, int DeleteFlag)  
       {
           ClsObject FollowupEducation = new ClsObject();
           int retval = 0;
           try
           {
               this.Connection = DataMgr.GetConnection();
               this.Transaction = DataMgr.BeginTransaction(this.Connection);

               FollowupEducation.Connection = this.Connection;
               FollowupEducation.Transaction = this.Transaction;

               ClsUtility.Init_Hashtable();
               ClsUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());
               ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Ptn_pk.ToString());
               ClsUtility.AddParameters("@VisitPk ", SqlDbType.Int, Visit_pk.ToString());
               ClsUtility.AddParameters("@LocationID", SqlDbType.Int, LocationID.ToString());
               ClsUtility.AddParameters("@CouncellingTypeId", SqlDbType.Int, CouncellingTypeId.ToString());
               ClsUtility.AddParameters("@CouncellingTopicId", SqlDbType.Int, CouncellingTopicId.ToString());
               ClsUtility.AddParameters("@VisitDate", SqlDbType.VarChar, VisitDate.ToString());
               ClsUtility.AddParameters("@Comments", SqlDbType.VarChar, Comments.ToString());
               ClsUtility.AddParameters("@OtherDetail", SqlDbType.VarChar, OtherDetail.ToString());               
               ClsUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
               ClsUtility.AddParameters("@DeleteFlag", SqlDbType.Int, DeleteFlag.ToString());

               retval = (int)FollowupEducation.ReturnObject(ClsUtility.theParams, "Pr_Clinical_SaveFollowupEducation_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);

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
               FollowupEducation = null;
               if (this.Connection != null)
                   DataMgr.ReleaseConnection(this.Connection);

           }
           return retval;
       }
       public int DeleteFollowupEducation(int Id, int Ptn_pk)
       {
           try
           {
               int theAffectedRows = 0;
               this.Connection = DataMgr.GetConnection();
               this.Transaction = DataMgr.BeginTransaction(this.Connection);

               ClsObject DeleteFollowupEducation = new ClsObject();
               DeleteFollowupEducation.Connection = this.Connection;

               DeleteFollowupEducation.Transaction = this.Transaction;

               ClsUtility.Init_Hashtable();

               ClsUtility.AddParameters("@Id", SqlDbType.Int, Id.ToString());
               ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, Ptn_pk.ToString());
               

               theAffectedRows = (int)DeleteFollowupEducation.ReturnObject(ClsUtility.theParams, "Pr_Clinical_DeleteFollowupEducation_Constella", ClsDBUtility.ObjectEnum.ExecuteNonQuery);


               DataMgr.CommitTransaction(this.Transaction);
               DataMgr.ReleaseConnection(this.Connection);
               return theAffectedRows;
           }
           catch
           {
               DataMgr.RollBackTransation(this.Transaction);
               throw;
           }
           finally
           {
               if (this.Connection != null)
                   DataMgr.ReleaseConnection(this.Connection);
           }
       }
       public DataSet GetSearchFollowupEducation(int PatientId)
       {
           lock (this)
           {
               ClsUtility.Init_Hashtable();
               ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientId.ToString());
               ClsObject FollowupEducation = new ClsObject();
               return (DataSet)FollowupEducation.ReturnObject(ClsUtility.theParams, "Pr_Clinical_GetFollowupEducation_Constella", ClsDBUtility.ObjectEnum.DataSet);
           }
       }
       public DataSet GetAllFollowupEducationData(int PatientId)
       {
           lock (this)
           {
               ClsUtility.Init_Hashtable();
               ClsUtility.AddParameters("@Ptn_pk", SqlDbType.Int, PatientId.ToString());
               ClsObject FollowupEducation = new ClsObject();
               return (DataSet)FollowupEducation.ReturnObject(ClsUtility.theParams, "pr_Clinical_GetAllFollowupEducationData_Constella", ClsDBUtility.ObjectEnum.DataSet);
           }
       }
       public DataSet GetCouncellingTopic(int CouncellingTypeId)
       {
           lock (this)
           {
               ClsUtility.Init_Hashtable();
               ClsUtility.AddParameters("@Id", SqlDbType.Int, CouncellingTypeId.ToString());
               ClsObject FollowupEducation = new ClsObject();
               return (DataSet)FollowupEducation.ReturnObject(ClsUtility.theParams, "pr_clinical_GetCouncellingTypeTopic_Constella", ClsDBUtility.ObjectEnum.DataSet);
           }

       }
       public DataSet GetCouncellingType()
       {
           lock (this)
           {
               ClsUtility.Init_Hashtable();
               ClsObject FollowupEducation = new ClsObject();
               return (DataSet)FollowupEducation.ReturnObject(ClsUtility.theParams, "pr_clinical_GetCouncellingType_Constella", ClsDBUtility.ObjectEnum.DataSet);
           }
       }

    }
}
