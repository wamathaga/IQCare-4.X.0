using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace Application.Presentation.NDR
{
    public enum ValueSet
    {
        [Description("ADDRESS_TYPE")]
        ADDRESS_TYPE = 1,
        [Description("ADHERANCE")]
        ADHERANCE = 2,
        [Description("ADHERANCE_POORFAIR_REASON")]
        ADHERANCE_POORFAIR_REASON = 3,
        [Description("ADVERSE_REACTIONS")]
        ADVERSE_REACTIONS = 4,
        [Description("ART_STATUS")]
        ART_STATUS = 5,
        [Description("ARV_REGIMEN")]
        ARV_REGIMEN = 6,
        [Description("CARE_ENTRY_POINT")]
        CARE_ENTRY_POINT = 7,
        [Description("CONDITION_CODE")]
        CONDITION_CODE = 8,
        [Description("COUNTRY")]
        COUNTRY = 9,
        [Description("EDD_PMTCT_LINK")]
        EDD_PMTCT_LINK = 10,
        [Description("EDUCATIONAL_LEVEL")]
        EDUCATIONAL_LEVEL = 11,
        [Description("FACILITY_TYPE")]
        FACILITY_TYPE = 12,
        [Description("FAMILY_PLANNING_METHOD")]
        FAMILY_PLANNING_METHOD = 13,
        [Description("FAMILY_PLANNING_STATUS")]
        FAMILY_PLANNING_STATUS = 14,
        [Description("FUNCTIONAL_STATUS")]
        FUNCTIONAL_STATUS = 15,
        [Description("HIV_TEST_TYPE")]
        HIV_TEST_TYPE = 16,
        [Description("IDENTIFIER_TYPE")]
        IDENTIFIER_TYPE = 17,
        [Description("INTERRUPT")]
        INTERRUPT = 18,
        [Description("INTERRUPTION_REASON")]
        INTERRUPTION_REASON = 19,
        [Description("LAB_RESULTED_TEST")]
        LAB_RESULTED_TEST = 20,
        [Description("LANGUAGE")]
        LANGUAGE = 21,
        [Description("LGA")]
        LGA = 22,
        [Description("MARITAL_STATUS")]
        MARITAL_STATUS = 23,
        [Description("MEASURE_UNITS")]
        MEASURE_UNITS = 24,
        [Description("MESSAGE_STATUS")]
        MESSAGE_STATUS = 25,
        [Description("OCCUPATION_STATUS")]
        OCCUPATION_STATUS = 26,
        [Description("OI_OTHER")]
        OI_OTHER = 27,
        [Description("OI_REGIMEN")]
        OI_REGIMEN = 28,
        [Description("PREGNANCY_STATUS")]
        PREGNANCY_STATUS = 29,
        [Description("PRIOR_ART")]
        PRIOR_ART = 30,
        [Description("PROGRAM_AREA")]
        PROGRAM_AREA = 31,
        [Description("REGIMEN_LINE")]
        REGIMEN_LINE = 32,
        [Description("REGIMEN_STOP")]
        REGIMEN_STOP = 33,
        [Description("REGIMEN_SUB_SWITCH_REASON")]
        REGIMEN_SUB_SWITCH_REASON = 34,
        [Description("REGIMEN_TYPE")]
        REGIMEN_TYPE = 35,
        [Description("RELATIONSHIP")]
        RELATIONSHIP = 36,
        [Description("SEX")]
        SEX = 37,
        [Description("STATES")]
        STATES = 38,
        [Description("TB_REGIMEN")]
        TB_REGIMEN = 39,
        [Description("TB_STATUS")]
        TB_STATUS = 40,
        [Description("TESTING_STATUS")]
        TESTING_STATUS = 41,
        [Description("VACCINE_ADMINISTER")]
        VACCINE_ADMINISTER = 42,
        [Description("VACCINE_SITE")]
        VACCINE_SITE = 43,
        [Description("VACCINE_TYPE")]
        VACCINE_TYPE = 44,
        [Description("VALUE_TYPE")]
        VALUE_TYPE = 45,
        [Description("WHO_STAGE")]
        WHO_STAGE = 46,
        [Description("WHY_ELIGIBLE")]
        WHY_ELIGIBLE = 47,
        [Description("YNU")]
        YNU = 48,
    }
}
