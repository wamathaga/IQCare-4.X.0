using System;
using System.Collections;
using System.Configuration;
using System.Data;


namespace Application.Presentation
{
    /// <summary>
    /// This class is applicable only with windows application of IQCare.
    /// </summary>
    public abstract class GblIQCare
    {
        
        public static int iFormMode;

        public int iUserId;
        public static string strAppVersion;
        public static DateTime dtmAppVersion;

        #region "Application Paramete>rs"
        public static string AppVersion = "4.1.0";
        public static string ReleaseDate = "15-Mar-2018";
        #endregion

        #region "Public Variables"
        /// <summary>
        /// This class is applicable for login form
        /// Meetu Rahul
        /// </summary>
        public static int AppUserId;
        public static string AppUserName;
        public static string AppUName;
        public static int EnrollFlag;
        public static int IdentifierFlag;
        public static DataTable dtUserRight;
        public static int SystemId;
        public static string AppCountryId;
        public static int AppLocationId;
        public static string AppLocation;
        public static string AppLocTelNo;
        public static string AppPosID;
        public static string AppSatelliteId;
        public static string AppGracePeriod;
        public static string AppDateFormat;
        public static string BackupDrive;
        public static string pwd;
        public static string CurrentDate;
        public static DataTable dtModules;
        public static DataSet dsTreeView;
        public static string PatientInstructions;
        public static DataTable dtFacility;

        /// <summary>
        /// This class is applicable for Businessrule Screen
        /// </summary>
        public static string strDisplayType ;
        public static string strFieldName ;
        public static string strTempName = "";
        public static string strCount = "";
        public static DataTable dtBusinessValues = new DataTable();
        public static DataSet dsBusinessRuleVal = new DataSet();
        public static DataTable dtTempValue = new DataTable();
        ///
        /// This variable is used in service area business rules form
        /// 
        public static string strserviceareaname = "";
        public static DataTable dtServiceBusinessValues = new DataTable();

        ///
        /// This variable is used in form lavel business rules form
        /// 
        public static Boolean blnSingleVisit = false;
        public static Boolean blnMultivisit = false;
        public static Boolean blnSignatureOneachpage = false;
        public static Boolean blncontrolunabledesable = true;
        public static DataTable dtformBusinessValues = new DataTable();
        /// <summary>
        /// This class is applicable for Select List Screen
        /// </summary>

        public static string strSelectFieldName = "";
        public static Hashtable hashTblSelectList = new Hashtable();
        public static string strSelectListValue = "";
        public static DataTable dtICD10Code = new DataTable();
        /// <summary>
        /// This class is applicable for Field Details Screen
        /// </summary>
        public static Hashtable objHashtbl = new Hashtable();
        public static Boolean blnFieldDetailsChange = false;
        public static Boolean blhselectlistChange=false;
        public static Boolean blnBusinessRuleChange = false;
        public static int intFieldDetaisChange = 0;
        public static string strRetainSelectList = "";
        public static string strRetainSelectField = "";
        public static string strgblPredefined = "";
        public static Hashtable objhashSelectList = new Hashtable();
        public static int gblRowIndex = 0;
        public static Hashtable objhashBusinessRule = new Hashtable();
        public static string strICD10 = "";
        public static Boolean blnICD10multiselect = false;
        public static Boolean blnOtherICD10 = false;
        /// <summary>
        /// This class is applicable for Form Details Screen
        /// </summary>
        public static int iFormBuilderId;
        public static int ResetSection;
        public static int RefillDdlFields;
        public static int ModuleId;
        public static int iHomePageId;
        public static string ModuleName="";
        public static int UpdateFlag = 0;
        public static int Identifier = 0;
        public static int PharmacyFlag = 0;
        public static string Query = "";
        public static int Scheduler = 0;
        public static int iManageCareEnded;
        public static int iCareEndedId=0;
        public static bool blnIsPatientHomePage = false;
        public static bool blnCustomPharmacyPage = false;
        public static string iFormBuilderType="";

        /// <summary>
        /// This class is applicable for Associated Field
        /// </summary>
        public static int iFieldId=0;
        public static int iModuleId = 0;
        public static string strMainGrdFldName = "";
        public static DataTable dtConditionalFields = new DataTable();
        public static string strPredefinevalue = "";
        public static int iConditionalbtn = 0;
        public static DataSet DsFieldDetailsCon = new DataSet();
        
        /// <summary>
        /// This class is applicable for CareEndConditional Field
        /// </summary>
        public static DataTable dtCareEndConditional = new DataTable();
        public static DataTable dtPharmacyBusRule = new DataTable();

        public static string strSelectList = "";
        public static string strSelectListstr = "";
        public static string strCareEndFeatureName = "";
        public static int CareEndconFlag = 0;
        public static string strYesNo = "";
        public static string strCareEndcon = "";
        public static string strfoll = "0";

        /// <summary>
        /// This class is applicable for Lab Configuration Field
        /// </summary>
        public static int LabTestId = 0;
        
        //Item List Common Variable
        public static string ItemLabel = "";
        public static string ItemCategoryId = "";
        public static string ItemTableName = "";
        public static int ItemFeatureId = 0;

        /// <summary>
        /// Variables releated to ItemMaster
        /// </summary>
        public static string theItemId = "";

        /// <summary>
        /// Variables for UserStore Selection
        /// </summary>
        public static string theArea = "";
        public static Int32 intStoreId = 0;
        public static Int32 PurchaseOrderID = 0;
        /// <summary>
        /// if ModePurchaseOrder = 0 then No interstore transfer and no Purchase Order  Selected
        /// if ModePurchaseOrder = 1 then  Purchase Order  Selected
        /// if ModePurchaseOrder = 2 then  Interstore transfer  Selected
        /// </summary>
        public static Int32 ModePurchaseOrder = 0;

        /// <summary>
        /// Variables releated to PatientRegistration
        /// </summary>
        public static string strPatientRegistrationInsert = "I";
        public static int patientID;

        /// <summary>
        /// Variables releated to Label Print
        /// </summary>
        /// 
        public static DataTable dtPrintLabel = new DataTable();
        public static string PatientName;
        public static string IQNumber;
        public static string StoreName;
        /// <summary>
        /// Variables releated to Form Versioning
        /// </summary>
        /// 
        public static int FormVersionId;
        public static decimal FormVersionNo;
        public static DateTime FormVersionDate;
        public static decimal NewFormVersionNo;
        public static DateTime NewFormVersionDate;
        
        public static DataTable dtchangedTabs;
        public static DataTable dtChangedSections;
        public static DataTable dtChangedFields;
        public static DataTable dtChangedConditionalFields;
        public static DataSet dsFormversionFieldDetails;
        public static int FormId = 0;
        #endregion

        public static Boolean HasFeatureRight(int FeatureId, DataTable theDT)
        {
            DataView theDV = new DataView(theDT);
            theDV.RowFilter = "FeatureId = " + FeatureId.ToString();
            if (theDV.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static Boolean HasFunctionRight(int FeatureId, int FunctionId, DataTable theDT)
        {
            DataView theDV = new DataView(theDT);
            theDV.RowFilter = "FeatureId = " + FeatureId.ToString() + " and FunctionId = " + FunctionId.ToString();
            if (theDV.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// This function is used to get the full path of image folder
        /// </summary>
        ///<returns></returns>
        public static string GetPath()
        {
            return (ConfigurationManager.AppSettings["ImagePath"]);
        }
        public static string weburl()
        {
            return (ConfigurationManager.AppSettings["webfindaddpatientUrl"]);
        }
        public static string IQCareHelpweburl()
        {
            return (ConfigurationManager.AppSettings["IQCareHelpPath"]);
        }
        public static string PresentationImagePath()
        {
            return (ConfigurationManager.AppSettings["PresentationImagePath"]);
        }
        /// <summary>
        /// This function is used to get the full path of image folder
        /// </summary>
        ///<returns></returns>
        public static string GetXMLPath()
        {
            return (ConfigurationManager.AppSettings["XMLFilesPath"]);
        }

        public static string GetExcelPath()
        {
            return (ConfigurationManager.AppSettings["ExcelFilesPath"]);
        }
        public static string GetReportPath()
        {
            return (ConfigurationManager.AppSettings["ReportsPath"]);
        }
        public static string GetFieldvalidationReportPath()
        {
            return (ConfigurationManager.AppSettings["Rptfieldvalidation"]);
        }

    }
 }
