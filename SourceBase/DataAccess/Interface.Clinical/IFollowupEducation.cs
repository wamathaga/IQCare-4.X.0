using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace Interface.Clinical
{
    public interface IFollowupEducation
    {
        int SaveFollowupEducation(int Id, int Ptn_pk, int CouncellingTypeId, int CouncellingTopicId, int Visit_pk, int LocationID, string VisitDate, string Comments, string OtherDetail, int UserId, int DeleteFlag);  
        int DeleteFollowupEducation(int Id, int Ptn_pk);
        DataSet GetSearchFollowupEducation(int PatientId);
        DataSet GetCouncellingTopic(int CouncellingTypeId);
        DataSet GetCouncellingType();
        DataSet GetAllFollowupEducationData(int PatientId);
    }
}
