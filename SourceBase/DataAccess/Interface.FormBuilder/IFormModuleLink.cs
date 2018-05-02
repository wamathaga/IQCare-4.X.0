using System;
using System.Collections;
using System.Data;

namespace Interface.FormBuilder
{
     public interface IFormModuleLink
    {
         DataSet GetFormModuleLinkDetail(Int32 moduleID);
         void SaveUpdateFormModuleLinkDetail(int intModuleID, ArrayList list,int userId);
         DataSet FormModuleLinking(int ModuleId, int CountryID);
    }
}
