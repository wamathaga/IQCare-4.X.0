using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Interface.Service
{
    public interface ICommonData
    {
        DataTable getUserList();
        DataTable getMSTDecode(Int32 CodeID);
        DataTable getAllMSTDecodeIn(string Codes);
        DataTable getAllMSTCode();
        DataTable getAllEmployees();
        DataTable getAllDesignation();
        DataTable getAllLPTF();
        DataTable getAllHIVDiseases();
        DataTable getAllSymptoms();
        DataTable getAllReasons();
        DataTable getAllWards();
        DataTable getAllVillages();
        DataTable getAllCouncellingType();
        DataTable getAllCouncellingTopic();
        DataTable getAllDistrict();
        DataTable getAllProvince();
        DataTable getAllCountries();
        DataTable getAllEducation();
        DataTable getAllDivision();
        DataTable getAllARTSponsor();
        DataTable getAllHivDisclosure();
    }
}
