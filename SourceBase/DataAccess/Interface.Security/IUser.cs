using System;
using System.Data;
using System.Collections;

namespace Interface.Security
{
    public interface IUser
    {
        //DataTable GetFacilitySystemId();  
        DataTable GetFacilityList();
        DataSet GetFacilitySettings(int SystemId);
        DataSet GetUserCredentials(string UserName, int LocationId, int SystemId);
        DataTable GetEmployeeDetails();
        int UpdateAppointmentStatus(string Currentdat, int locationid);
        DataSet GetAuditData();
    }
}
