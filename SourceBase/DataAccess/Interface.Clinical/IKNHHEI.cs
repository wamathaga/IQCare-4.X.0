using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interface.Clinical
{
    public interface IKNHHEI
    {
        DataSet GetKNHPMTCTHEI(int patientID, int VisitID);
        int Save_Update_KNHHEI(int patientID, int VisitID, int LocationID, Hashtable ht, DataSet theDSchklist, int userID, int DataQualityFlag);
    }
}
