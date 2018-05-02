using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Interface.FormBuilder
{
    public interface  IFormBuilder
    {
        DataTable SystemDate();
        int RetrieveMaxId(String strSearchIn);
        int SaveFormDetail(DataSet dsSaveFormData, DataTable dtFieldDetails, DataTable dtbusinessRules, DataSet DSFormVer);
        int UpdateFormDetailSeq(DataTable dtFieldDetails);
        bool CheckDuplicate(string strSearchTable, string strSearchColumn, string strSearchValue, String iDeleteFlagCheck, int iModuleId);
        bool CheckDuplicate(string strSearchTable, string strSearchColumn1, string strSearchValue1, string strSearchColumn2, string strSearchValue2, String iDeleteFlagCheck, int iModuleId);
        DataSet GetFormDetail(Int32 iFeatureId);
        int SaveCustomRegistrationFormDetail(DataSet dsSaveFormData, DataTable dtFieldDetails);
        
    }
}
