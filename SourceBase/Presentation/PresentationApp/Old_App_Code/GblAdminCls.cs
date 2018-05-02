using System;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for GblAdminCls
/// </summary>

namespace IQCare.SharedClass
{
    public class GblAdminCls
    {
        #region "Constructor"
        public GblAdminCls()
        {
        }
        #endregion

        #region "Private Variables"
        int _UserId = 0;
        DataSet _PharmacyMaster = new DataSet();
        #endregion

        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public DataSet PharmacyMasters
        {
            get { return _PharmacyMaster; }
            set { _PharmacyMaster = value; }
        }
    }
}