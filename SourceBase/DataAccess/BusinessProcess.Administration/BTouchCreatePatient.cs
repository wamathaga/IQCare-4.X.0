using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using Interface.Administration;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Application.Common;

namespace BusinessProcess.Administration
{
    class BTouchCreatePatient 
    {
        #region "Constructor"
        public BTouchCreatePatient()
        {
        }
        #endregion

        //public int SaveNewUser(
        //    string FName, string LName, int LocationID, DateTime RegistrationDate, int Sex, DateTime DOB, int UserID, DateTime CreateDate, string IDNo, string D9FolderNo, int Status = 1, int DeleteFlag = 0)
        //{
        //    ClsObject UserManager = new ClsObject();
        //    try
        //    {
        //        this.Connection = DataMgr.GetConnection();
        //        this.Transaction = DataMgr.BeginTransaction(this.Connection);

        //        UserManager.Connection = this.Connection;
        //        UserManager.Transaction = this.Transaction;

        //        Utility theUtil = new Utility();
        //        ClsUtility.Init_Hashtable();
        //        ClsUtility.AddParameters("@fname", SqlDbType.VarChar, FName);
        //        ClsUtility.AddParameters("@lname", SqlDbType.VarChar, LName);
        //        ClsUtility.AddParameters("@username", SqlDbType.VarChar, UserName);
        //        ClsUtility.AddParameters("@EmpId", SqlDbType.Int, EmpId.ToString());
        //        ClsUtility.AddParameters("@password", SqlDbType.VarChar, ApplicationAccess.DBSecurity.Replace("'", ""));
        //        ClsUtility.AddParameters("@userid", SqlDbType.Int, UserId.ToString());

        //        DataRow theDR;
        //        theDR = (DataRow)UserManager.ReturnObject(ClsUtility.theParams, "Pr_Admin_SaveNewUser_Constella", ClsDBUtility.ObjectEnum.DataRow);
        //        if (Convert.ToInt32(theDR[0]) == 0)
        //        {
        //            MsgBuilder theBL = new MsgBuilder();
        //            theBL.DataElements["MessageText"] = "Error in Saving User Record. Try Again..";
        //            AppException.Create("#C1", theBL);
        //            return Convert.ToInt32(theDR[0]);
        //        }
        //    }
        //}
    }
}
