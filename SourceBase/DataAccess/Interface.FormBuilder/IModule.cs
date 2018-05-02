using System;
using System.Collections;
using System.Data;

namespace Interface.FormBuilder
{
    public interface IModule
    {
        DataSet GetModuleDetail();
        DataSet GetModuleIdentifier(Int32 ModuleId);
        int SaveUpdateModuleDetail(Hashtable ht, DataTable dt, DataTable dtbusinessrules);
        int StatusUpdate(Hashtable ht);
        void DeleteModule(int ModuleId);

    

    }
}
