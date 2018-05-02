using System;
using System.Data;
using System.Collections;


namespace Interface.Administration
{
    public interface Iuser
    {
        DataSet FillDropDowns();
        int SaveNewUser(bool isEmployee, string FName, string LName, string UserName, string Password, string Email, string Phone, int UserId, int Designation, Hashtable UserGroup, Hashtable Store);
        DataSet GetUserList();
        DataSet GetUserRecord(int UserId);
        void UpdateUserRecord(bool isEmployee, string FName, string LName, string UserName, string Password, string Email, string Phone, int UserId, int OperatorId, int Designation, Hashtable UserGroup, Hashtable Store);
        //void DeleteUserRecord(int UserId);
        int DeleteUserRecord(int UserId);
        void SaveUserLock(int UserId, int locationID, string code, string lastURL);
        DataSet GetUserLock(int UserId);
    }

}
