using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Interface.Clinical
{
    public interface IFreeText
    {
        DataSet GetFreeText(int patientID, int VisitID);
        int Save_Update_FreeText(int Ptn_pk, int Visit_Id, string FreeTextXml, int location_id, int userID);
    }
}
