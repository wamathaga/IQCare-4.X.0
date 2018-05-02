using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Entities.Common
{
    [Serializable]
    [DataContract]
    public class Appointment
    {

        [DataMember(Name = "Id", Order = 1)]
        public int Id { get; set; }

        [DataMember(Name = "Ptn_Pk", Order = 2)]
        public int Ptn_pk { get; set; }

        [DataMember(Name = "LId", Order = 3)]
        public int LocationId { get; set; }

        [DataMember(Name = "VId", Order = 4)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "ANAD", Order = 5)]
        public DateTime? AppointmentDate { get; set; }

        [DataMember(Name = "APurpose", Order = 6)]
        public int? Purpose { get; set; }

        [DataMember(Name = "PurposeN", Order = 7)]
        public string PurposeName { get; set; }

        [DataMember(Name = "AT", Order = 8)]
        public int? AppointmentType { get; set; }

        [DataMember(Name = "SA", Order = 9)]
        public int? ServiceArea { get; set; }

        [DataMember(Name = "SAN", Order = 10)]
        public string ServiceAreaName { get; set; }

        [DataMember(Name = "Desc", Order = 11)]
        public string Description { get; set; }

        [DataMember(Name = "Status", Order = 12)]
        public string Status { get; set; }

    }
}
