using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Xml;
using System.Xml.Linq;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using DataAccess.Report;
using Interface.Reports;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using Application.Common;
using System.Collections.Specialized;
namespace BusinessProcess.Reports
{
     
    [Serializable()]
    public class IQToolsReport : ProcessBase, IReportIQTools
    {

        /// <summary>
        /// The report
        /// </summary>
        private Report report;
        /// <summary>
        /// The query parameters
        /// </summary>
        QueryParameters queryParameters;
        /// <summary>
        /// The report query
        /// </summary>
    //    IReportQuery reportQuery;
        /// <summary>
        /// The report iq tools
        /// </summary>
        IReportIQTools reportIQTools;
        /// <summary>
        /// Prevents a default instance of the <see cref="IQToolsReport"/> class from being created.
        /// </summary>
        public IQToolsReport()
        {
            reportIQTools = (IReportIQTools)this;
           // reportQuery = (IReportQuery)this;
        }
        /// <summary>
        /// The XML document
        /// </summary>
        /// <value>
        /// The report XML document.
        /// </value>
        /// 
       // [Serializable]
        XmlDocument reportXML = null;

        /// <summary>
        /// The report XSL
        /// </summary>
        //[Serializable]
        XmlDocument reportXSL = new XmlDocument();
        private static List<Facility> _facilities;
        private static EventLog theLog = new EventLog();
        private Utility clsUtil;
        SqlConnection cnBKTest;
        public string constr;
        /// <summary>
        /// Initializes a new instance of the <see cref="IQToolsReport"/> class.
        /// </summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        public IQToolsReport(DateTime dateFrom, DateTime dateTo)
        {
            queryParameters = new QueryParameters() { DateFrom = dateFrom, DateTo = dateTo };
            //XmlDeclaration newChild = this.reportXML.CreateXmlDeclaration("1.0", "UTF-8", null);
            //this.reportXML.AppendChild(newChild);
            //XmlElement element = this.reportXML.CreateElement("Root");
            //this.reportXML.AppendChild(element);
        }
        /// <summary>
        /// Initialises the report document.
        /// </summary>
        void InitialiseReportDocument()
        {
                   this.reportXML = new XmlDocument();
            XmlDeclaration newChild = this.reportXML.CreateXmlDeclaration("1.0", "UTF-8", null);
            this.reportXML.AppendChild(newChild);
            XmlElement element = this.reportXML.CreateElement("Root");
            this.reportXML.AppendChild(element);
        }
        /// <summary>
        /// 
        /// </summary>
        private struct XmlEncodingBOM
        {
            public string EncDescription;
            public Encoding TextEncoding;
            public int BOMLength;
            public byte[] ByteSequnce;

            /// <summary>
            /// Initializes a new instance of the <see cref="XmlEncodingBOM"/> struct.
            /// </summary>
            /// <param name="ED">The ed.</param>
            /// <param name="TE">The te.</param>
            /// <param name="BL">The bl.</param>
            /// <param name="BS">The bs.</param>
            public XmlEncodingBOM(string ED, Encoding TE, int BL, byte[] BS)
            {
                this.EncDescription = ED;
                this.TextEncoding = TE;
                this.BOMLength = BL;
                this.ByteSequnce = BS;
            }
        }
        /// <summary>
        /// Sets the report parameters.
        /// </summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <param name="cd4CutOff">The CD4 cut off.</param>
        /// <returns></returns>
        public IReportIQTools SetReportParameters(DateTime dateFrom, DateTime dateTo, int cd4CutOff = 350)
        {
            this.queryParameters = new QueryParameters()
            {
                DateFrom = dateFrom,
                DateTo = dateTo,
                CD4Cutoff =cd4CutOff
            };
            return (IReportIQTools)this;
        }
        /// <summary>
        /// Gets the name of the report.
        /// </summary>
        /// <value>
        /// The name of the report.
        /// </value>
        public string ReportName
        {
            get
            {
                return this.report.ReportName;
            }
        }
        /// <summary>
        /// Gets the report XSD schema.
        /// </summary>
        /// <value>
        /// The report XSD schema.
        /// </value>
        public string ReportXSDSchema
        {
            get
            {
                return @"<?xml version=""1.0"" encoding=""utf-8""?>
<xs:schema attributeFormDefault=""unqualified"" elementFormDefault=""qualified"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:element name=""Root"">
    <xs:complexType>
      <xs:sequence>
        <xs:element name=""Report"">
          <xs:complexType>
            <xs:sequence>
              <xs:element name=""reportid"" type=""xs:unsignedByte"" />
              <xs:element name=""reportname"" type=""xs:string"" />
              <xs:element name=""reportfrom"" type=""xs:dateTime"" />
              <xs:element name=""reportto"" type=""xs:dateTime"" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
        <xs:element name=""Data"">
          <xs:complexType>
            <xs:sequence>
              <xs:element maxOccurs=""unbounded"" name=""Results"">
                <xs:complexType>
                  <xs:sequence>
                    <xs:element name=""Query"">
                      <xs:complexType>
                        <xs:sequence>
                          <xs:element name=""QueryDescription"" type=""xs:string"" />
                          <xs:element name=""QueryCategory"" type=""xs:string"" />
                          <xs:element name=""QueryCategoryID"" type=""xs:unsignedShort"" />
                        </xs:sequence>
                      </xs:complexType>
                    </xs:element>
                    <xs:element name=""Mapping"">
                      <xs:complexType>
                        <xs:sequence>
                          <xs:element maxOccurs=""unbounded"" name=""Map"">
                            <xs:complexType>
                              <xs:sequence>
                                <xs:element name=""Title"" type=""xs:string"" />
                                <xs:element name=""Cell"" type=""xs:string"" />
                              </xs:sequence>
                            </xs:complexType>
                          </xs:element>
                        </xs:sequence>
                      </xs:complexType>
                    </xs:element>
                    <xs:element minOccurs=""0"" name=""Error"">
                      <xs:complexType>
                        <xs:sequence>
                          <xs:element name=""QueryID"" type=""xs:unsignedShort"" />
                          <xs:element name=""QueryName"" type=""xs:string"" />
                          <xs:element name=""QueryDescription"" type=""xs:string"" />
                          <xs:element name=""QueryCategory"" type=""xs:string"" />
                          <xs:element name=""QueryCategoryID"" type=""xs:unsignedShort"" />
                          <xs:element name=""ErrrorMessage"" type=""xs:string"" />
                        </xs:sequence>
                        <xs:attribute name=""QueryID"" type=""xs:unsignedShort"" use=""required"" />
                        <xs:attribute name=""subcategoryid"" type=""xs:unsignedShort"" use=""required"" />
                      </xs:complexType>
                    </xs:element>
                    <xs:element minOccurs=""0"" name=""QueryResults"">
                      <xs:complexType>
                        <xs:sequence minOccurs=""0"">
                          <xs:element maxOccurs=""unbounded"" name=""Row"">
                            <xs:complexType>
                              <xs:sequence>
                                <xs:element name=""Count"" type=""xs:unsignedByte"" />
                                <xs:element maxOccurs=""unbounded"" name=""Indicators"">
                                  <xs:complexType>
                                    <xs:sequence>
                                      <xs:element name=""Name"" type=""xs:string"" />
                                      <xs:element name=""Result"" type=""xs:string"" />
                                    </xs:sequence>
                                  </xs:complexType>
                                </xs:element>
                              </xs:sequence>
                            </xs:complexType>
                          </xs:element>
                        </xs:sequence>
                      </xs:complexType>
                    </xs:element>
                  </xs:sequence>
                  <xs:attribute name=""QUERY_NAME"" type=""xs:string"" use=""required"" />
                  <xs:attribute name=""QUERY_ID"" type=""xs:unsignedShort"" use=""required"" />
                </xs:complexType>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>";
            }
        }
               
        /// <summary>
        /// Imports the reports from iq tools.
        /// </summary>
        /// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void ImportReportsFromIQTools(bool overwrite = false)
        {
            lock (this)
            {
                ClsObject ReportManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                string queryStatement;
                DataMgr dataManager = new DataMgr();
                //  string sourceDB = dataManager.Reports_DatabaseName;
                // string destinationDB = dataManager.EMR_DatabaseName;

                //if (overwrite)
                //{
                //    queryStatement = "Truncate table dbo.IQToolsExcelReports";

                //    ReportManager.ReturnObject(ClsUtility.theParams, queryStatement, ClsUtility.ObjectEnum.ExecuteNonQuery);
                //    ReportManager.Connection = null;
                //}
                queryStatement = @"SELECT catID,Category FROM aa_Category WHERE  excel=1;";

                DataTable dt = (DataTable)ReportManager.ReturnIQToolsObject(ClsUtility.theParams, queryStatement, ClsDBUtility.ObjectEnum.DataTable, ConnectionMode.REPORT);

                if (dt != null)
                {
                    ReportManager.Connection = null;
                    dt.TableName = "reports";
                    //dt.Namespace = "fromiqtools";

                    System.IO.StringWriter writer = new System.IO.StringWriter();
                    dt.WriteXml(writer, true);
                    queryStatement = "pr_IQTools_ImportReports";
                    //                      @"Insert Into dbo.IQToolsExcelReports(IQToolsCaTID,ReportName,ReportStylesheet)
                    //                                        Select 
                    //                                                a.c.value('catID[1]','int'), 
                    //                                                a.c.Category('Category[1]','varchar(200)')
                    //                                        From @x.nodes(//reports) a(c);";
                    ClsUtility.Init_Hashtable();
                    if (overwrite)
                        ClsUtility.AddParameters("@overwrite", SqlDbType.Bit, (overwrite) ? "1" : "0");
                    ClsUtility.AddParameters("@data", SqlDbType.Xml, writer.ToString());
                    ReportManager.ReturnObject(ClsUtility.theParams, queryStatement, ClsDBUtility.ObjectEnum.ExecuteNonQuery);

                }


                // return (DataSet)ReportManager.ReturnObject(ClsUtility.theParams, "pr_Reports_Patient_Constella", ClsUtility.ObjectEnum.DataSet);
            }

        }

        /// <summary>
        /// Updates the report XSL.
        /// </summary>
        /// <param name="reportId">The report identifier.</param>
        /// <param name="buffer">The buffer.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool UpdateReportXsl(int reportId, byte[] buffer, string filename = "", string fileExt = "", string contentType = "text/xsl", int fileLength = 0)
        {
            lock (this)
            {
                ClsObject ReportManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                string queryStatement = "pr_IQTools_UpdateReport";
                DataMgr dataManager = new DataMgr();
                ClsUtility.AddParameters("@reportID", SqlDbType.Int, reportId.ToString());
                ClsUtility.AddExtendedParameters("@template", SqlDbType.VarBinary, (object)(buffer));
                ClsUtility.AddParameters("@templateFileName", SqlDbType.VarChar, filename);
                ClsUtility.AddParameters("@templateFileExt", SqlDbType.VarChar, fileExt);
                ClsUtility.AddParameters("@templateContentType", SqlDbType.VarChar, contentType);
                ClsUtility.AddParameters("@fileLength", SqlDbType.Int, fileLength.ToString());
                ReportManager.ReturnObject(ClsUtility.theParams, queryStatement, ClsDBUtility.ObjectEnum.ExecuteNonQuery,true);
                return true;
            }
        }
        /// <summary>
        /// Gets the report XSL.
        /// </summary>
        /// <param name="reportId">The report identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public string ReportXslTemplate
        {
            get
            {
                if (this.report != null && this.report.ReportStyleSheet != "")
                {
                    return (report.ReportStyleSheet);

                }
                return "";
            }
        }
        /// <summary>
        /// Gets a value indicating whether [report has template].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [report has template]; otherwise, <c>false</c>.
        /// </value>
        public bool ReportHasTemplate
        {
            get
            {
                if (this.report == null) return false;

                return report.HasReportStyleSheet;
            }
        }
        /// <summary>
        /// Gets the XSL template.
        /// </summary>
        /// <returns></returns>
        public bool GetXSLTemplate(Page pwPage)
        {
            HttpResponse Response = pwPage.Response;
            Response.Clear();
            int FileSize = report.ReportTemplateSize;
            string contentType = "text/xml";
            string fileName = report.ReportTemplateName;
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);

            Response.OutputStream.Write(this.report.ReportTemplateBinary, 0, FileSize);

            Response.End();

            return (true);

        }
        /// <summary>
        /// Gets the report queries.
        /// </summary>
        /// <param name="reportId">The report identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
       QueryCollection IReportIQTools.GetReportQueries(int reportId)
        {
            try
            {
                QueryCollection qCollection = new QueryCollection();
                ClsObject ReportManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                string queryStatement;
                DataMgr dataManager = new DataMgr();
                ClsUtility.AddParameters("@ReportID", SqlDbType.Int, reportId.ToString());
                queryStatement = @"Select Distinct	Q.QryID QueryID,
				                SB.sbCatID SubCategoryID,
				                SB.sbCategory SubCategory,
				                Q.qryName QueryName,
				                Q.qryDefinition Defination,
				                Q.qryDescription Description
                From dbo.aa_sbCategory SB
                Inner Join aa_Category C
	                On SB.CatID = C.CatID
                Inner Join dbo.aa_Queries Q On Q.qryID = SB.QryID
                Where (SB.DeleteFlag Is Null Or SB.DeleteFlag = 0) 
                And SB.QryID Is Not Null 
                And C.CatID=@ReportID;";
                DataTable dt = (DataTable)ReportManager.ReturnIQToolsObject(ClsUtility.theParams, queryStatement, ClsDBUtility.ObjectEnum.DataTable, ConnectionMode.REPORT);
                if (dt == null) return null;
                foreach (DataRow row in dt.Rows)
                {
                    //Query q = new 
                    qCollection.Add(new Query()
                    {
                        QueryID = Convert.ToInt16(row["QueryID"]),
                        Name = row["QueryName"].ToString(),
                        SubCategoryID = Convert.ToInt16(row["SubCategoryID"]),
                        SubCategory = row["SubCategory"].ToString(),
                        Defination = row["Defination"].ToString(),
                        Description = row["Description"].ToString()
                    });
                };

                return qCollection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Gets the report.
        /// </summary>
        /// <returns></returns>
        public string GetReportData()
        {
            if (this.reportXML !=null)
            {
                return this.reportXML.OuterXml;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// Runs the report.
        /// </summary>
        /// <param name="reportId">The report identifier.</param>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public void RunReport(int reportId, DateTime dateFrom, DateTime dateTo, int cd4CutOff = 350)
        {
            //throw new NotImplementedException();
            this.queryParameters = new QueryParameters() { DateFrom = dateFrom, DateTo = dateTo, CD4Cutoff= cd4CutOff };
             this.GetReportbyID(reportId);
            report.PeriodFrom = dateFrom;
            report.PeriodTo = dateTo;
            SetReportParameters(dateFrom, dateTo);
            QueryCollection qCollection = this.reportIQTools.GetReportQueries(reportId);

            this.InitialiseReportDocument();

            XmlElement rootElement = this.reportXML.DocumentElement;
            XmlNode reportNode = this.reportXML.CreateNode(XmlNodeType.Element, "Report", string.Empty);

            reportNode.InnerXml =
                new XElement("reportid", report.ReportID).ToString() +
                new XElement("reportname", report.ReportName).ToString() +
                new XElement("reportfrom", report.PeriodFrom.Value.ToString("o")).ToString() +
                new XElement("reportto", report.PeriodTo.Value.ToString("o")).ToString();
            rootElement.AppendChild(reportNode);
            //report.PeriodTo = dateTo };
            XmlNode mainNode = reportXML.CreateNode(XmlNodeType.Element, "Data", string.Empty);
            foreach (string key in qCollection)
            {
                XmlNode resultNode = reportXML.CreateNode(XmlNodeType.Element, "Results", string.Empty);
               
                Query query = qCollection[key];
                 XmlAttribute att1 = reportXML.CreateAttribute("QUERY_NAME");
                XmlAttribute att2 = reportXML.CreateAttribute("QUERY_ID");
                att1.InnerText = query.Name;
                att2.InnerText = query.QueryID.ToString();
                resultNode.Attributes.Append(att1);
                resultNode.Attributes.Append(att2);
                XElement c = new XElement("Query",
                    // new XAttribute("ID", query.QueryID),
                   // new XAttribute("subcategoryid", query.SubCategoryID),
                   // new XElement("QueryID", query.QueryID),
                   // new XElement("QueryName", query.Name),
                    new XElement("QueryDescription", query.Description),
                    new XElement("QueryCategory", query.SubCategory),
                    new XElement("QueryCategoryID", query.SubCategoryID));

                XmlDocumentFragment queryNode = reportXML.CreateDocumentFragment();
                queryNode.InnerXml = c.ToString();

                resultNode.AppendChild(queryNode);


                List<QueryMapping> queryMaps = reportIQTools.GetQueryMaps(query.QueryID);

                XElement map = new XElement("Mapping",
                        from qMap in queryMaps
                        select new XElement("Map",
                            new XElement("Title", qMap.Title),
                            new XElement("Cell", qMap.Cell)
                        )
                            );


                XmlDocumentFragment mapNode = reportXML.CreateDocumentFragment();
                mapNode.InnerXml = map.ToString();
                resultNode.AppendChild(mapNode);

                string queryResult = reportIQTools.ExecQuery(query);
               // XmlNode queryNode = reportXML.CreateNode(XmlNodeType.Element, "Query", string.Empty);
                XmlDocumentFragment fragment = reportXML.CreateDocumentFragment();
                fragment.InnerXml = queryResult;
                resultNode.AppendChild(fragment);
                mainNode.AppendChild(resultNode);
                //queryNode.InnerXml = queryResult;                
                // resultNode.InnerXml += queryResult; ;
            }
            rootElement.AppendChild(mainNode);
           // return reportXML;
        }

        /// <summary>
        /// Converts the data table to XML.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        /// <param name="q">The q.</param>
        /// <returns></returns>
        private String GetTransposedQueryResultAsXML(DataTable dataTable, ref Query q)
        {
            try
            {
                int queryId = q.QueryID;
                //List<QueryMapping> queryMaps = reportIQTools.GetQueryMaps(q.QueryID);

                var columnNames = (from dc in dataTable.Columns.Cast<DataColumn>()
                                   select dc.ColumnName);
                //var qc = from column in columnNames
                //            join qmap in queryMaps on column.ToLower() equals qmap.Title.ToLower() into gj
                //            from fgroup in gj.DefaultIfEmpty()
                //         select new { column, fgroup.Title, Cell = (fgroup.Cell == null ? String.Empty : fgroup.Cell) };

                XElement xmlDT = new XElement("QueryResults",
                    //new XAttribute("ID", q.QueryID),
                    //new XAttribute("subcategoryid", q.SubCategoryID),
                    //new XElement("QueryID", q.QueryID),
                    //new XElement("QueryName", q.Name),
                    //new XElement("QueryDescription", q.Description),
                    //new XElement("QueryCategory", q.SubCategory),
                    //new XElement("QueryCategoryID", q.SubCategoryID),
                    //new XElement("Mapping",
                    //    from  qMap in queryMaps
                    //        select new XElement("Map",
                    //            new XElement("Title",qMap.Title),
                    //            new XElement("Cell",qMap.Cell)
                    //        )
                    //        ),
                        (from row in dataTable.AsEnumerable()
                         select new XElement("Row",
                                new XElement("Count", dataTable.Rows.IndexOf(row)),
                                    (from columns in columnNames
                                     select new XElement("Indicators",
                                         new XElement("Name", columns),
                                         new XElement("Result", row[columns])//,
                                         //new XElement("Cell", (queryMaps.Find(x=> x.Title.ToLower()== columns.ToLower()).Cell)
                                     )
                                    )
                             //)
                             //( from columns in qc
                             //  select new XElement("Indicators",
                             //      new XElement("Name", columns.column),
                             //      new XElement("Result",row[columns.column]),
                             //      new XElement("Cell",columns.Cell)

                                    //)
                             //)
                                )
                        )
                        .Distinct()
                      );

                return xmlDT.ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Runs the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        string IReportIQTools.ExecQuery(Query query)
        {
            try
            {
                ClsObject ReportManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                string queryStatement;
                query.queryParameters = this.queryParameters;
                ClsUtility.AddParameters("@QueryID", SqlDbType.Int, query.QueryID.ToString());
                List<QueryMapping> queryMaps = new List<QueryMapping>();
                queryStatement = query.Defination;
                ClsUtility.AddExtendedParameters("@FromDate", SqlDbType.DateTime, query.queryParameters.DateFrom);
                ClsUtility.AddExtendedParameters("@ToDate", SqlDbType.DateTime, query.queryParameters.DateTo);
                ClsUtility.AddExtendedParameters("@CD4Cutoff", SqlDbType.Int, query.queryParameters.CD4Cutoff);
                DataTable dt = (DataTable)ReportManager.ReturnIQToolsObject(ClsUtility.theParams, queryStatement, ClsDBUtility.ObjectEnum.DataTable, ConnectionMode.REPORT);
                return GetTransposedQueryResultAsXML(dt, ref query);
            }
            catch (Exception ex)
            {
                return new XElement("Error",
                    new XAttribute("QueryID", query.QueryID),
                    new XAttribute("subcategoryid", query.SubCategoryID),
                    new XElement("QueryID", query.QueryID),
                    new XElement("QueryName", query.Name),
                    new XElement("QueryDescription", query.Description),
                    new XElement("QueryCategory", query.SubCategory),
                    new XElement("QueryCategoryID", query.SubCategoryID),
                    new XElement("ErrrorMessage", ex.Message)).ToString();
            }
        }
        /// <summary>
        /// Gets the size of the report template.
        /// </summary>
        /// <value>
        /// The size of the report template.
        /// </value>
        public int ReportTemplateSize
        {
            get
            {
               return this.report.ReportTemplateSize;
            }
        }
        /// <summary>
        /// Gets the name of the report template file.
        /// </summary>
        /// <value>
        /// The name of the report template file.
        /// </value>
        public string ReportTemplateFileName
        {
            get
            {
                return this.report.ReportTemplateName;
            }
        }
        /// <summary>
        /// Gets the type of the report template content.
        /// </summary>
        /// <value>
        /// The type of the report template content.
        /// </value>
        public string ReportTemplateContentType {
            get
            {
                return this.report.ReportTemplateContentType;
            }
        }
        /// <summary>
        /// Gets the reportby identifier.
        /// </summary>
        /// <param name="reportID">The report identifier.</param>
        /// <returns></returns>
        public void GetReportbyID(int reportID)
        {
            try
            {
                //this.report = null;
                ClsObject ReportManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                string queryStatement;

                ClsUtility.AddParameters("@ReportID", SqlDbType.Int, reportID.ToString());
                queryStatement = @"Select  IQToolsCaTID ReportID,ReportName , 
                           ReportStylesheet,
                        FileName+'.'+FileExt FullFileName,
                        ContentType,
                        Isnull(FileLength,0)FileLength
                From dbo.IQToolsExcelReports Where IQToolsCaTID=@ReportID;";
                DataRow dr = (DataRow)ReportManager.ReturnObject(ClsUtility.theParams, queryStatement, ClsDBUtility.ObjectEnum.DataRow);
                ReportManager = null;
                if (dr == null) return;

                this.report = new Report();
                if (this.queryParameters != null)
                {
                    report.PeriodFrom = this.queryParameters.DateFrom;
                    report.PeriodTo = this.queryParameters.DateTo;
                }
                report.ReportName = dr["ReportName"].ToString();
                report.SetReportID(reportID);
                report.HasReportStyleSheet = false;
                if (dr["ReportStylesheet"] != DBNull.Value)
                {
                    int FileSize = Convert.ToInt32(dr["FileLength"]);
                    byte[] xslStyle;
                    byte[] xslData = new byte[FileSize];
                    string strOut_XSL;
                    ((byte[])dr["ReportStylesheet"]).CopyTo(xslData, 0);


                    XmlEncodingBOM encBOM = GetEncodingBOM(xslData);

                    if (encBOM.BOMLength > 0)
                    {
                        xslStyle = new byte[FileSize - encBOM.BOMLength];
                        Array.Copy(xslData, encBOM.BOMLength, xslStyle, 0, FileSize - encBOM.BOMLength);
                    }
                    else
                    {
                        xslStyle = xslData;
                    }

                    //ignore EncodingByteOrderMark assume utf8
                    // xslStyle = xslData;
                    strOut_XSL = encBOM.TextEncoding.GetString(xslStyle);


                    report.ReportStyleSheet = strOut_XSL;

                    report.HasReportStyleSheet = true;
                    report.ReportTemplateSize = FileSize;
                    report.ReportTemplateName = dr["FullFileName"].ToString();
                    report.ReportTemplateContentType = dr["ContentType"].ToString();
                    report.ReportTemplateBinary = xslData;
                }
            }
            
            catch (Exception e)
            {
                throw e;
            }
            //return this.report;
        }
        /// <summary>
        /// Gets the encoding bom.
        /// </summary>
        /// <param name="ByteArray">The byte array.</param>
        /// <returns></returns>
        private XmlEncodingBOM GetEncodingBOM(byte[] ByteArray)
        {

            int bomIndex = -1;
            int bomLength = 0;

            XmlEncodingBOM[] XmlEncodings = new XmlEncodingBOM[10]{
			new XmlEncodingBOM("UTF-8", Encoding.UTF8, 3, new byte[3]{0xEF, 0xBB, 0xBF}),
			new XmlEncodingBOM("UTF-16", Encoding.BigEndianUnicode, 2, new byte[2]{0xFE, 0xFF}),
			new XmlEncodingBOM("UTF-16", Encoding.Unicode, 2, new byte[2]{0xFF, 0xFE}),
			new XmlEncodingBOM("UTF-32", Encoding.UTF32, 4, new byte[4]{0x00, 0x00, 0xFE, 0xFF}),
			new XmlEncodingBOM("UTF-32", Encoding.UTF32, 4, new byte[4]{0xFF, 0xFE, 0x00, 0x00}),
			new XmlEncodingBOM("UTF-7", Encoding.UTF7, 4, new byte[4]{0x2B, 0x2F, 0x76, 0x38}),
			new XmlEncodingBOM("UTF-7", Encoding.UTF7, 4, new byte[4]{0x2B, 0x2F, 0x76, 0x39}),
			new XmlEncodingBOM("UTF-7", Encoding.UTF7, 4, new byte[4]{0x2B, 0x2F, 0x76, 0x2B}),
			new XmlEncodingBOM("UTF-7", Encoding.UTF7, 4, new byte[4]{0x2B, 0x2F, 0x76, 0x2F}),
			new XmlEncodingBOM("ASCII", Encoding.ASCII, 0, null)
		};


            for (int i = 0; i < XmlEncodings.Length; i++)
            {
                if (ByteArray.Length < XmlEncodings[i].BOMLength) break;
                bool isTheOne = true;
                for (int x = 0; x < XmlEncodings[i].BOMLength; x++)
                {
                    if (XmlEncodings[i].ByteSequnce[x] != ByteArray[x]) isTheOne = false;
                }
                if (isTheOne)
                {
                    if (XmlEncodings[i].BOMLength >= bomLength)
                    {
                        bomLength = XmlEncodings[i].BOMLength;
                        bomIndex = i;
                    }
                }
            }

            if (bomIndex < 0)
            {
                return (new XmlEncodingBOM("UTF-8", Encoding.UTF8, 0, null));
            }
            else
            {
                return (XmlEncodings[bomIndex]);
            }
        }

        /// <summary>
        /// Gets the reports.
        /// </summary>
        /// <returns></returns>
        public DataTable GetReports()
        {
            try
            {
                ClsObject ReportManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                string queryStatement;
                DataMgr dataManager = new DataMgr();
                queryStatement = @"Select  IQToolsCaTID ReportID,ReportName , 
                            Case  When ReportStylesheet Is Null Then 'No' Else 'Yes' End As HasTemplate,
                        FileName+'.'+FileExt FullFileName,
                        ContentType
                From dbo.IQToolsExcelReports;";
                DataTable dt = (DataTable)ReportManager.ReturnObject(ClsUtility.theParams, queryStatement, ClsDBUtility.ObjectEnum.DataTable);

                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Gets the query maps.
        /// </summary>
        /// <param name="queryId"></param>
        /// <returns></returns>
       List<QueryMapping> IReportIQTools.GetQueryMaps(int queryId)
        {
            try
            {
                ClsObject ReportManager = new ClsObject();
                ClsUtility.Init_Hashtable();
                string queryStatement;
                ClsUtility.AddParameters("@QueryID", SqlDbType.Int, queryId.ToString());
                List<QueryMapping> queryMaps = new List<QueryMapping>();
                queryStatement = @"SELECT xlsCell Cell, xlsTitle Title,QryID QueryID FROM aa_xlMaps WHERE QryID=@QueryID;";
                DataTable dt = (DataTable)ReportManager.ReturnIQToolsObject(ClsUtility.theParams, queryStatement, ClsDBUtility.ObjectEnum.DataTable, ConnectionMode.REPORT);

                if (dt == null) return null;
                foreach (DataRow row in dt.Rows)
                {
                    queryMaps.Add(new QueryMapping()
                    {
                        Cell = row["Cell"].ToString(),
                        Title = row["Title"].ToString(),
                        QueryID = Convert.ToInt16(row["QueryID"])
                    });
                };
                return queryMaps;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
       
       private List<Facility> FacilityList
       {
           get
           {
               if (_facilities == null)
               {
                   FacilityDetails();
               }
               return _facilities;
           }
       }
       class Facility
       {
           /// <summary>
           /// Gets or sets the identifier.
           /// </summary>
           /// <value>
           /// The identifier.
           /// </value>
           public int ID { get; internal set; }
           /// <summary>
           /// Gets or sets the name.
           /// </summary>
           /// <value>
           /// The name.
           /// </value>
           public string Name { get; internal set; }
       }
       private void FacilityDetails()
       {
           try
           {
               if (_facilities == null)
               {
                   SqlCommand cmdTest;
                   constr = clsUtil.Decrypt(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["ConnectionString"]);
                   cnBKTest = new SqlConnection(this.constr);
                   cmdTest = new SqlCommand(@"Select F.FacilityID, F.FacilityName From dbo.mst_Facility F Where F.DeleteFlag = 0 Or F.DeleteFlag Is Null Order By 1", cnBKTest);
                   cnBKTest.Open();
                   IDataReader dr = cmdTest.ExecuteReader(CommandBehavior.Default);
                   DataTable dt = new DataTable();
                   dt.Load(dr);
                   dr.Close();
                   _facilities = new List<Facility>();
                   foreach (DataRow row in dt.Rows)
                   {
                       _facilities.Add(new Facility { ID = int.Parse(row["FacilityID"].ToString()), Name = row["FacilityName"].ToString() });
                   }

               }
           }
           catch (Exception e)
           {
               throw e;
           }
           // return _facilities;
       }


       void IReportIQTools.IQToolsRefresh()
       {
           SqlConnection connectionIQtools = null;
           try
           {
               string strIQToolsConnection = "";
               string strIQToolsInit = "";
               
               DateTime dateNextRefreshDate = DateTime.Now;
               clsUtil = new Utility();
               if (ConfigurationManager.AppSettings["IQToolsConnectionString"] != null)
               {
                   strIQToolsConnection = clsUtil.Decrypt(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["IQToolsConnectionString"]);
                   //strIQToolsConnection = clsUtil.Decrypt(ConfigurationManager.AppSettings.Get("IQToolsConnectionString"));
               }
               if (ConfigurationManager.AppSettings["IQToolsInitializationProcedures"] != null)
               {
                   strIQToolsInit = clsUtil.Decrypt(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["IQToolsInitializationProcedures"]);
                   //strIQToolsInit = clsUtil.Decrypt(ConfigurationManager.AppSettings["IQToolsInitializationProcedures"]);
               }
               if (ConfigurationManager.AppSettings["IQToolsNextRefreshDateTime"] != null)
               {
                   try
                   {
                       //XmlConvert.ToDateTime()
                       dateNextRefreshDate = Convert.ToDateTime(ConfigurationManager.AppSettings["IQToolsNextRefreshDateTime"]);
                   }
                   catch (Exception e1)
                   {
                       theLog.WriteEntry(e1.Message + e1.StackTrace);
                   }
               }
               if (dateNextRefreshDate <= DateTime.Now && strIQToolsConnection != "")
               {
                   //set next update time
                   //XmlNode nodeIQToolsRefreshTime = doc.SelectSingleNode("//appSettings/add[@key='IQToolsNextRefreshDateTime']");
                   //if (nodeIQToolsRefreshTime != null)
                   //{
                   int refreshInterval = 30;
                   //if(refreshIntervalText.v)
                   // XmlNode nodeIQToolsRefreshInterval = doc.SelectSingleNode("//appSettings/add[@key='IQToolsRefreshInterval']");

                   int.TryParse(ConfigurationManager.AppSettings["IQToolsRefreshInterval"], out refreshInterval);


                   string strIQToolsRefreshTime = (DateTime.Now.AddMinutes(refreshInterval)).ToString("yyyy-MM-dd HH:mm:ss");
                   Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                   config.AppSettings.Settings["IQToolsNextRefreshDateTime"].Value = strIQToolsRefreshTime;
                   config.Save(ConfigurationSaveMode.Modified);
                   ConfigurationManager.RefreshSection("appSettings");

                   connectionIQtools = new SqlConnection(strIQToolsConnection);
                   connectionIQtools.Open();
                   SqlCommand command;

                   string[] procedures = strIQToolsInit.Split(';');
                   string facilityName = FacilityList[0].Name;
                   foreach (string procedure in procedures)
                   {
                       try
                       {
                           command = new SqlCommand(procedure, connectionIQtools);
                           command.CommandType = CommandType.StoredProcedure;
                           command.Parameters.Add("@EMR", SqlDbType.VarChar, 10).Value = "IQCare";
                           command.Parameters.Add("@FacilityName", SqlDbType.VarChar, 50).Value = facilityName;
                           command.ExecuteNonQuery();
                       }
                       catch (Exception e2)
                       {
                           theLog.WriteEntry(e2.Message + e2.StackTrace + procedure);
                       }
                       finally
                       {
                           connectionIQtools.Dispose();
                       }
                   }

                   connectionIQtools.Close();
                   connectionIQtools.Dispose();
               }
           }
           catch (Exception e0)
           {
               //theLog.WriteEntry(e0.Message + e0.StackTrace);
               CLogger.WriteLog("Namespace: DataAccess.Entity, Class: ClsObject, Method: IQToolsReferesh - Call started.", "","", e0.Message.ToString());
               throw new ApplicationException("There was an error occured while connecting IQTools databae. Please verify the connection string and try again.", e0);
           }
           finally
           {
               connectionIQtools.Close();
               connectionIQtools.Dispose();
           }

       }
    }
}
