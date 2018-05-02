using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Entities.Common
{
    [Serializable]
    [DataContract(Name = "ANC")]
    public class ANC
    {
        [DataMember(Name = "Ptn_Pk", Order = 1)]
        public int Ptn_pk { get; set; }

        [DataMember(Name = "LId", Order = 2)]
        public int LocationId { get; set; }

        [DataMember(Name = "VId", Order = 3)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "CC", Order = 4)]
        public List<CodeDeCodeTables> ChronicCondition = new List<CodeDeCodeTables>();

        [DataMember(Name = "Severity", Order = 5)]
        public List<MultiTableList> Severity = new List<MultiTableList>();

        [DataMember(Name = "AT", Order = 6)]
        public List<MultiTableList> AllergyType = new List<MultiTableList>();

        [DataMember(Name = "Allergen", Order = 7)]
        public List<MultiTableList> Allergen = new List<MultiTableList>();

        [DataMember(Name = "ENVA", Order = 8)]
        public List<MultiTableList> environmentalAllergen = new List<MultiTableList>();

        [DataMember(Name = "FOODA", Order = 9)]
        public List<MultiTableList> foodAllergen = new List<MultiTableList>();

        [DataMember(Name = "Collection", Order = 11)]
        public List<MultiTableList> Collections = new List<MultiTableList>();

        [DataMember(Name = "OV", Order = 12)]
        public OrdVisit OrdVisit = new OrdVisit();

        [DataMember(Name = "OBSGYN", Order = 13)]
        public OBSnGYNHistory OBSnGYNHistory = new OBSnGYNHistory();

        [DataMember(Name = "AllergiesD", Order = 14)]
        public List<AllergiesDetail> AllergiesDetail = new List<AllergiesDetail>();

        [DataMember(Name = "FHOF", Order = 15)]
        public List<CodeDeCodeTables> FamilyHistoryOf = new List<CodeDeCodeTables>();

        [DataMember(Name = "CDT", Order = 16)]
        public List<MultiSelectTable> CDT = new List<MultiSelectTable>();

        [DataMember(Name = "Pregnancies", Order = 17)]
        public List<PregnancyDetail> PregnancyDetails = new List<PregnancyDetail>();

        [DataMember(Name = "SH", Order = 18)]
        public string SurgicalHistory { get; set; }

        [DataMember(Name = "HBT", Order = 19)]
        public int? HistoryBloodTransfusion { get; set; }

        [DataMember(Name = "GestAge", Order = 20)]
        public string GestAge { get; set; }

        [DataMember(Name = "SFHOF", Order = 21)]
        public List<DBMultiSelectSelcted> SelectedFamilyHistoryOf = new List<DBMultiSelectSelcted>();

        [DataMember(Name = "AllerHF")]
        public int AllerHF { get; set; }

        [DataMember(Name = "AllerH")]
        public List<AllerH> AllerH = new List<AllerH>();

    }

    [Serializable]
    [DataContract(Name = "ANCST")]
    public class ANCScreeningTests
    {
        [DataMember(Name = "Ptn_Pk", Order = 1)]
        public int Ptn_pk { get; set; }

        [DataMember(Name = "LId", Order = 2)]
        public int LocationId { get; set; }

        [DataMember(Name = "VId", Order = 3)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "LRS", Order = 4)]
        public List<LabResult> LabResults = new List<LabResult>();

        [DataMember(Name = "Collection", Order = 5)]
        public List<MultiTableList> Collections = new List<MultiTableList>();

        [DataMember(Name = "HIVTesting", Order = 6)]
        public HIVTesting HIVTesting = new HIVTesting();

    }

    [Serializable]
    [DataContract(Name = "ANCVD")]
    public class ANCVisitDetails
    {
        [DataMember(Name = "Ptn_Pk", Order = 1)]
        public int Ptn_pk { get; set; }

        [DataMember(Name = "LId", Order = 2)]
        public int LocationId { get; set; }

        [DataMember(Name = "VId", Order = 3)]
        public int Visit_Id { get; set; }

        [DataMember(Name = "VD", Order = 4)]
        public VisitDetails VisitDetails = new VisitDetails();

        [DataMember(Name = "VA", Order = 5)]
        public List<ANCVisitAnthropometric> VisitAnthropometric = new List<ANCVisitAnthropometric>();

        [DataMember(Name = "PREG", Order = 6)]
        public List<ANCPresentPregnancy> PresentPregnancy = new List<ANCPresentPregnancy>();

        [DataMember(Name = "Collection", Order = 7)]
        public List<MultiTableList> Collections = new List<MultiTableList>();

        [DataMember(Name = "Appointments", Order = 8)]
        public List<Appointment> Appointments = new List<Appointment>();

        [DataMember(Name = "PUR", Order = 9)]
        public List<CodeDeCodeTables> Purpose = new List<CodeDeCodeTables>();

    }

    [Serializable]
    [DataContract]
    public class CodeDeCodeTables
    {
        [DataMember(Name = "CId", Order = 1)]
        public int CodeId { get; set; }
        [DataMember(Name = "CN", Order = 2)]
        public String CodeName { get; set; }
        [DataMember(Name = "DId", Order = 3)]
        public int DeCodeId { get; set; }
        [DataMember(Name = "DN", Order = 4)]
        public string DeCodeName { get; set; }
    }

    [Serializable]
    [DataContract(Name = "MST")]
    public class MultiSelectTable
    {
        [DataMember(Name = "Ptn_pk", Order = 1)]
        public int Ptn_pk { get; set; }
        [DataMember(Name = "VId", Order = 2)]
        public int Visit_Pk { get; set; }
        [DataMember(Name = "ValId", Order = 3)]
        public int ValueID { get; set; }
        [DataMember(Name = "FN", Order = 4)]
        public string FieldName { get; set; }
        [DataMember(Name = "OSD", Order = 5)]
        public DateTime? OnSetDate { get; set; }
        [DataMember(Name = "NF", Order = 6)]
        public int? NumericField { get; set; }
        [DataMember(Name = "NT", Order = 7)]
        public string Notes { get; set; }
        [DataMember(Name = "VNT", Order = 8)]
        public string VNotes { get; set; }
        [DataMember(Name = "ValN", Order = 9)]
        public string ValueName { get; set; }
    }

    [Serializable]
    [DataContract(Name = "MTL")]
    public class MultiTableList
    {
        [DataMember(Name = "Id", Order = 1)]
        public int Id { get; set; }
        [DataMember(Name = "NM", Order = 2)]
        public string Name { get; set; }
        [DataMember(Name = "CNM", Order = 3)]
        public string CName { get; set; }
    }

    [Serializable]
    [DataContract]
    public class DBMultiSelectSelcted
    {

        [DataMember(Name = "id")]
        public int id { get; set; }
    }

    [Serializable]
    [DataContract]
    public class AllerH
    {

        [DataMember(Name = "ATID")]
        public string ATID { get; set; }

        [DataMember(Name = "ATD")]
        public string ATD { get; set; }

        [DataMember(Name = "Allergen")]
        public string Allergen { get; set; }

        [DataMember(Name = "AllerDesc")]
        public string AllerDesc { get; set; }

        [DataMember(Name = "AllerTID")]
        public string AllerTID { get; set; }

        [DataMember(Name = "OAller")]
        public string OAller { get; set; }

        [DataMember(Name = "STID")]
        public string STID { get; set; }

        [DataMember(Name = "SDesc")]
        public string SDesc { get; set; }

        [DataMember(Name = "DAller")]
        public DateTime DAller { get; set; }

        [DataMember(Name = "displayDAller")]
        public string displayDAller { get; set; }

        [DataMember(Name = "TR")]
        public string TR { get; set; }

        [DataMember(Name = "Comment")]
        public string Comment { get; set; }
    }
}
