using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IQCare.IPT.Common
{
    public interface IIPTDetails
    {
        DataSet GetInitialDetails(int facilityID);
        DataSet GetDatabaseNames(string dataSource, string userId, string password);
        DataSet GetFacilityNames(string dataSource, string userId, string password, string initialCatalog);
        DataSet GetPatientDetails(int facilityID, int moduleId, string identifier, string identifierNumber);

        DataSet GetPatientIPTDetails(int facilityID, int ptnpk);
        DataTable SavePatientIPTDetails(int facilityID, int ptnpk, int visitId,
                                                string IPT,
                                                string IPTAdherence,
                                                string IPTContraindication,
                                                string IPTDiscontinued,
                                                DateTime? IPTEndDate,
                                                DateTime? IPTStartDate,
                                                int EligibleForIPT,
                                                string otherReasonDeclinedIPT,
                                                string otherReasonDiscontinuedIPT
                );
    }
}
