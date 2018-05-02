using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using Interface.Service;
using System.Data;
using DataAccess.Entity;
using DataAccess.Common;

namespace BusinessProcess.Service
{
    public class BCommonData : ProcessBase, ICommonData 
    {
        public BCommonData() { }

        public DataTable getUserList()
        { 
            ClsUtility.Init_Hashtable();
            ClsObject oUserList = new ClsObject();
            //return (DataTable)oUserList.ReturnObject(ClsUtility.theParams, "VW_UserDesignationTransaction", ClsDBUtility.ObjectEnum.DataTable);
            return (DataTable)oUserList.ReturnObject(ClsUtility.theParams, "select * from VW_UserDesignationTransaction", ClsDBUtility.ObjectEnum.DataTable);// Change by Rahmat(14.02.17)
        }

        public DataTable getMSTDecode(Int32 CodeID)
        { 
            ClsUtility.Init_Hashtable();
            ClsObject oMstCodeDecode = new ClsObject();    
            ClsUtility.AddParameters("@CodeID", SqlDbType.Int, CodeID.ToString());
            
            return (DataTable)oMstCodeDecode.ReturnObject(ClsUtility.theParams, "sp_GetMSTDecode", ClsDBUtility.ObjectEnum.DataTable);
        }

        public DataTable getAllMSTDecodeIn(string Codes)
        {
            ClsUtility.Init_Hashtable();
            ClsObject oMstCodeDecode = new ClsObject();
            ClsUtility.AddParameters("@Codes", SqlDbType.NVarChar, Codes.ToString());

            return (DataTable)oMstCodeDecode.ReturnObject(ClsUtility.theParams, "sp_GetAllMSTDecode", ClsDBUtility.ObjectEnum.DataTable);
        }

        public DataTable getAllMSTCode()
        {
            ClsUtility.Init_Hashtable();
            ClsObject oMstAllCode = new ClsObject();

            return (DataTable)oMstAllCode.ReturnObject(ClsUtility.theParams, "sp_GetAllMSTCode", ClsDBUtility.ObjectEnum.DataTable);
        }

        public DataTable getAllEmployees()
        {
            ClsUtility.Init_Hashtable();
            ClsObject oMstAllEmployees = new ClsObject();

            return (DataTable)oMstAllEmployees.ReturnObject(ClsUtility.theParams, "sp_GetAllEmployees", ClsDBUtility.ObjectEnum.DataTable);
        }

        public DataTable getAllDesignation()
        {
            ClsUtility.Init_Hashtable();
            ClsObject oMstAllDesignation = new ClsObject();

            return (DataTable)oMstAllDesignation.ReturnObject(ClsUtility.theParams, "sp_GetAllDesignation", ClsDBUtility.ObjectEnum.DataTable);
        }


        public DataTable getAllLPTF()
        {
            ClsUtility.Init_Hashtable();
            ClsObject oMstGetAllLPTF = new ClsObject();

            return (DataTable)oMstGetAllLPTF.ReturnObject(ClsUtility.theParams, "sp_GetAllLPTF", ClsDBUtility.ObjectEnum.DataTable);
        }

        public DataTable getAllHIVDiseases()
        {
            ClsUtility.Init_Hashtable();
            ClsObject oMstAllHIVDiseases = new ClsObject();

            return (DataTable)oMstAllHIVDiseases.ReturnObject(ClsUtility.theParams, "sp_GetAllHIVDiseases", ClsDBUtility.ObjectEnum.DataTable);
        }

        public DataTable getAllSymptoms()
        {
            ClsUtility.Init_Hashtable();
            ClsObject oMstAllSymptoms = new ClsObject();

            return (DataTable)oMstAllSymptoms.ReturnObject(ClsUtility.theParams, "sp_GetAllMSTSymptoms", ClsDBUtility.ObjectEnum.DataTable);
        }

        public DataTable getAllReasons()
        {
            ClsUtility.Init_Hashtable();
            ClsObject oMstAllReasons = new ClsObject();

            return (DataTable)oMstAllReasons.ReturnObject(ClsUtility.theParams, "sp_GetAllReasons", ClsDBUtility.ObjectEnum.DataTable);
        }

        public DataTable getAllWards()
        {
            ClsUtility.Init_Hashtable();
            ClsObject oMstAllWards = new ClsObject();

            return (DataTable)oMstAllWards.ReturnObject(ClsUtility.theParams, "sp_GetAllWards", ClsDBUtility.ObjectEnum.DataTable);
        }

        public DataTable getAllVillages()
        {
            ClsUtility.Init_Hashtable();
            ClsObject oMstAllVillages = new ClsObject();

            return (DataTable)oMstAllVillages.ReturnObject(ClsUtility.theParams, "sp_GetAllVillages", ClsDBUtility.ObjectEnum.DataTable);
        }

        public DataTable getAllCouncellingType()
        {
            ClsUtility.Init_Hashtable();
            ClsObject oMstAllCounsellingType = new ClsObject();

            return (DataTable)oMstAllCounsellingType.ReturnObject(ClsUtility.theParams, "sp_GetAllCounsellingType", ClsDBUtility.ObjectEnum.DataTable);
        }

        public DataTable getAllCouncellingTopic()
        {
            ClsUtility.Init_Hashtable();
            ClsObject oMstAllCounsellingTopic = new ClsObject();

            return (DataTable)oMstAllCounsellingTopic.ReturnObject(ClsUtility.theParams, "sp_GetAllCouncellingTopic", ClsDBUtility.ObjectEnum.DataTable);
        }

        public DataTable getAllDistrict()
        {
            ClsUtility.Init_Hashtable();
            ClsObject oMstAllDistrict = new ClsObject();

            return (DataTable)oMstAllDistrict.ReturnObject(ClsUtility.theParams, "sp_GetAllDistrict", ClsDBUtility.ObjectEnum.DataTable);
        }

        public DataTable getAllProvince()
        {
            ClsUtility.Init_Hashtable();
            ClsObject oMstAllProvince = new ClsObject();

            return (DataTable)oMstAllProvince.ReturnObject(ClsUtility.theParams, "sp_GetAllProvince", ClsDBUtility.ObjectEnum.DataTable);
        }

        public DataTable getAllCountries()
        {
            ClsUtility.Init_Hashtable();
            ClsObject oMstAllCountries = new ClsObject();

            return (DataTable)oMstAllCountries.ReturnObject(ClsUtility.theParams, "sp_GetAllCountries", ClsDBUtility.ObjectEnum.DataTable);
        }

        public DataTable getAllEducation()
        {
            ClsUtility.Init_Hashtable();
            ClsObject oMstAllEducation = new ClsObject();

            return (DataTable)oMstAllEducation.ReturnObject(ClsUtility.theParams, "sp_GetAllEducation", ClsDBUtility.ObjectEnum.DataTable);
        }

        public DataTable getAllARTSponsor()
        {
            ClsUtility.Init_Hashtable();
            ClsObject oMstARTSponsor = new ClsObject();

            return (DataTable)oMstARTSponsor.ReturnObject(ClsUtility.theParams, "sp_GetAllARTSponsor", ClsDBUtility.ObjectEnum.DataTable);
        }

        public DataTable getAllHivDisclosure()
        {
            ClsUtility.Init_Hashtable();
            ClsObject oMstHivDisclosure = new ClsObject();

            return (DataTable)oMstHivDisclosure.ReturnObject(ClsUtility.theParams, "sp_GetAllHivDisclosure", ClsDBUtility.ObjectEnum.DataTable);
        }

        public DataTable getAllDivision()
        {
            ClsUtility.Init_Hashtable();
            ClsObject oMstDivision = new ClsObject();

            return (DataTable)oMstDivision.ReturnObject(ClsUtility.theParams, "sp_GetAllDivision", ClsDBUtility.ObjectEnum.DataTable);
        }
        
        public DataTable getAllProvinceByCountry(int CountryID)
        {
            ClsUtility.Init_Hashtable();
            ClsObject oMstAllProvince = new ClsObject();
            ClsUtility.AddParameters("@CountryID", SqlDbType.NVarChar, CountryID.ToString());


            return (DataTable)oMstAllProvince.ReturnObject(ClsUtility.theParams, "sp_GetAllProvince_Country", ClsDBUtility.ObjectEnum.DataTable);
        }
        
    }
    
}
