using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Xml;
using System.Data;
using System.Configuration.Internal;
using FarHorizon.Reservations.DataBaseManager;
using FarHorizon.Reservations.Common;
using FarHorizon.DataSecurity;

namespace FarHorizon.Reservations.UploadManager
{
    public class TouristUploader : ITouristUploader
    {
        #region IUploadedFileProcessor Members
        public bool processUplodedFile(int BookingId, ArrayList RecordList, XmlDocument xmlMappingDoc)
        {
            List<Cells> cellList = new List<Cells>();
            Cells cell = null;

            List<Row> rowList = null;
            Row row = new Row();
            int cellCounter = 0;

            try
            {
                XMLMapper xmlMapper = PrepareXMLMapper(xmlMappingDoc);
                string[] csvCells = null;

                if (RecordList != null && RecordList.Count > 1)
                {
                    rowList = new List<Row>();
                    for (int i = 0; i < RecordList.Count; i++)
                    {
                        if (i >= 4) // xmlMapper.StartingRow - 1
                        {
                            row = new Row();
                            row.RowNum = Convert.ToString(i + 1);

                            cellList = new List<Cells>();
                            cellCounter = 1;

                            csvCells = RecordList[i].ToString().Split(',');

                            for (int j = 0; j < csvCells.Length; j++)
                            {
                                cell = new Cells();
                                cell.ColNum = cellCounter.ToString().Trim();
                                cellCounter++;

                                cell.CellValue = RemoveCSVFormatters(csvCells[j].ToString().Trim());
                                cellList.Add(cell);
                            }
                            row.CellList = cellList;
                            rowList.Add(row);
                            //if (i >= xmlMapper.StartingRow - 1) //Avoiding the Header Row
                            //  prepareQuery(BookingId, cellList, xmlMapper);
                        }
                    }

                }
                prepareQuery(BookingId, rowList, xmlMapper);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return true;
        }

        private string RemoveCSVFormatters(string cellValue)
        {
            string newCellValue = cellValue.Trim();

            string x = '"'.ToString();
            if (newCellValue.Contains(x))
            {
                newCellValue = newCellValue.Replace(x, "");
            }
            if (newCellValue.StartsWith(@"\"))
            {
                newCellValue = newCellValue.Replace(@"\", "");
            }
            if (newCellValue.Contains("'"))
            {
                newCellValue = newCellValue.Replace("'", "''");
            }
            return newCellValue.Trim();
        }

        private XMLMapper PrepareXMLMapper(XmlDocument xmlMappingDoc)
        {
            string xPathTable = @"//table";
            string xPathStartRow = @"//startRow";
            string xPathReadPattern = @"//readPattern";
            string xPathFieldMapper;
            XMLMapper xmlMapper = null;

            List<ReadPattern> readPatternList = new List<ReadPattern>();
            ReadPattern readPattern;

            List<XMLFieldMapper> xmlFieldMapperList = new List<XMLFieldMapper>();
            XMLFieldMapper xmlFieldMapper;

            XmlNodeList nodeList;
            XmlNodeList fieldNodeList;
            string tableName = string.Empty;
            string startingRow = string.Empty;
            try
            {
                #region Getting Table Name
                nodeList = xmlMappingDoc.SelectNodes(xPathTable);
                if (nodeList != null && nodeList.Count > 0)
                {
                    tableName = !String.IsNullOrEmpty(nodeList.Item(0).Attributes["value"].Value) ? nodeList.Item(0).Attributes["value"].Value : String.Empty;
                }
                #endregion

                nodeList = xmlMappingDoc.SelectNodes(xPathStartRow);
                if (nodeList != null && nodeList.Count > 0)
                {
                    startingRow = !String.IsNullOrEmpty(nodeList.Item(0).Attributes["value"].Value) ? nodeList.Item(0).Attributes["value"].Value : String.Empty;
                }

                nodeList = xmlMappingDoc.SelectNodes(xPathReadPattern);
                if (nodeList != null && nodeList.Count > 0)
                {
                    xmlFieldMapperList = new List<XMLFieldMapper>();
                    foreach (XmlNode node in nodeList)
                    {
                        xPathFieldMapper = xPathReadPattern + @"[@value='READPATTERN']//mapper";
                        #region GetReadPattern
                        xmlMapper = new XMLMapper();
                        xmlMapper.TableName = tableName;
                        xmlMapper.StartingRow = Convert.ToInt32(startingRow);

                        readPattern = new ReadPattern();
                        readPattern.ReadPatternValue = node.Attributes["value"].Value;
                        readPattern.StartRow = node.Attributes["startRow"].Value;
                        readPattern.StartCol = node.Attributes["startcol"].Value;
                        readPattern.EndCol = node.Attributes["endcol"].Value;
                        #endregion

                        #region Getting FieldMapper

                        xPathFieldMapper = xPathFieldMapper.Replace("READPATTERN", readPattern.ReadPatternValue);

                        fieldNodeList = xmlMappingDoc.SelectNodes(xPathFieldMapper);
                        if (nodeList != null && fieldNodeList.Count > 0)
                        {
                            xmlFieldMapperList = new List<XMLFieldMapper>();
                            foreach (XmlNode fieldNode in fieldNodeList)
                            {
                                xmlFieldMapper = new XMLFieldMapper();
                                if (fieldNode.Attributes.GetNamedItem("dbField") != null)
                                    xmlFieldMapper.DbField = !String.IsNullOrEmpty(fieldNode.Attributes["dbField"].Value) ? fieldNode.Attributes["dbField"].Value : String.Empty;
                                if (fieldNode.Attributes.GetNamedItem("col") != null)
                                    xmlFieldMapper.ExcelColumn = !String.IsNullOrEmpty(fieldNode.Attributes["col"].Value) ? fieldNode.Attributes["col"].Value : String.Empty;
                                if (fieldNode.Attributes.GetNamedItem("row") != null)
                                    xmlFieldMapper.ExcelRow = !String.IsNullOrEmpty(fieldNode.Attributes["row"].Value) ? fieldNode.Attributes["row"].Value : String.Empty;
                                if (fieldNode.Attributes.GetNamedItem("idField") != null)
                                    xmlFieldMapper.IsIdField = !String.IsNullOrEmpty(fieldNode.Attributes["idField"].Value) ? Boolean.Parse(fieldNode.Attributes["idField"].Value) : false;
                                if (fieldNode.Attributes.GetNamedItem("Alpha") != null)
                                    xmlFieldMapper.IsAlpha = !String.IsNullOrEmpty(fieldNode.Attributes["Alpha"].Value) ? Boolean.Parse(fieldNode.Attributes["Alpha"].Value) : false;
                                if (fieldNode.Attributes.GetNamedItem("MasterLookUpTable") != null)
                                    xmlFieldMapper.MasterLookUpTable = !String.IsNullOrEmpty(fieldNode.Attributes["MasterLookUpTable"].Value) ? fieldNode.Attributes["MasterLookUpTable"].Value : String.Empty;
                                if (fieldNode.Attributes.GetNamedItem("MasterLookUpField") != null)
                                    xmlFieldMapper.MasterLookUpField = !String.IsNullOrEmpty(fieldNode.Attributes["MasterLookUpField"].Value) ? fieldNode.Attributes["MasterLookUpField"].Value : String.Empty;
                                if (fieldNode.Attributes.GetNamedItem("MasterIdField") != null)
                                    xmlFieldMapper.MasterIdField = !String.IsNullOrEmpty(fieldNode.Attributes["MasterIdField"].Value) ? fieldNode.Attributes["MasterIdField"].Value : String.Empty;
                                if (fieldNode.Attributes.GetNamedItem("DateField") != null)
                                    xmlFieldMapper.IsDateField = !String.IsNullOrEmpty(fieldNode.Attributes["DateField"].Value) ? Boolean.Parse(fieldNode.Attributes["DateField"].Value) : false;
                                if (fieldNode.Attributes.GetNamedItem("TimeField") != null)
                                    xmlFieldMapper.TimeField = !String.IsNullOrEmpty(fieldNode.Attributes["TimeField"].Value) ? Boolean.Parse(fieldNode.Attributes["TimeField"].Value) : false;
                                if (fieldNode.Attributes.GetNamedItem("JoinToPrev") != null)
                                    xmlFieldMapper.JoinToPrev = !String.IsNullOrEmpty(fieldNode.Attributes["JoinToPrev"].Value) ? Boolean.Parse(fieldNode.Attributes["JoinToPrev"].Value) : false;
                                if (fieldNode.Attributes.GetNamedItem("BoolField") != null)
                                    xmlFieldMapper.IsBool = !String.IsNullOrEmpty(fieldNode.Attributes["BoolField"].Value) ? Boolean.Parse(fieldNode.Attributes["BoolField"].Value) : false;
                                if (fieldNode.Attributes.GetNamedItem("FetchTouristNo") != null)
                                    xmlFieldMapper.FetchTouristNo = !String.IsNullOrEmpty(fieldNode.Attributes["FetchTouristNo"].Value) ? Boolean.Parse(fieldNode.Attributes["FetchTouristNo"].Value) : false;
                                if (fieldNode.Attributes.GetNamedItem("Encrypt") != null)
                                    xmlFieldMapper.Encrypt = !String.IsNullOrEmpty(fieldNode.Attributes["Encrypt"].Value) ? Boolean.Parse(fieldNode.Attributes["Encrypt"].Value) : false;
                                xmlFieldMapperList.Add(xmlFieldMapper);
                            }
                        }
                        #endregion

                        readPattern.XmlFieldMapperList = xmlFieldMapperList;
                        readPatternList.Add(readPattern);
                        xmlMapper.ReadPatternList = readPatternList;
                    }
                }
            }
            catch (Exception exp)
            {
                GF.LogError("PrepareXMLMapper", exp.Message);
            }
            return xmlMapper;
        }

        private void prepareQuery(int BookingId, List<Row> rowList, XMLMapper xmlMapper)
        {
            StringBuilder insertQuery1 = new StringBuilder();
            StringBuilder insertQuery2 = new StringBuilder();

            StringBuilder updateQuery = new StringBuilder();
            StringBuilder existQuery = new StringBuilder();
            StringBuilder fetchTouristNoQuery = new StringBuilder();
            bool gotSomeCells = false;

            string tablename = xmlMapper.TableName;

            try
            {
                foreach (Row row in rowList)
                {
                    #region Intializing Queries
                    insertQuery1.Remove(0, insertQuery1.Length);
                    insertQuery2.Remove(0, insertQuery2.Length);
                    updateQuery.Remove(0, updateQuery.Length);
                    existQuery.Remove(0, existQuery.Length);
                    fetchTouristNoQuery.Remove(0, fetchTouristNoQuery.Length);

                    insertQuery1 = new StringBuilder();
                    insertQuery2 = new StringBuilder();
                    updateQuery = new StringBuilder();
                    existQuery = new StringBuilder();
                    fetchTouristNoQuery = new StringBuilder();


                    insertQuery1.Append("insert into " + tablename + "(");
                    insertQuery2.Append(" values (");
                    updateQuery.Append("update " + tablename + " set ");
                    existQuery.Append("select * from " + tablename + " where 1=1");
                    #endregion

                    foreach (ReadPattern readPattern in xmlMapper.ReadPatternList)
                    {
                        #region ColumnWise
                        if (readPattern.ReadPatternValue == "columnwise")
                        {
                            foreach (XMLFieldMapper xmlFieldMapper in readPattern.XmlFieldMapperList)
                            {
                                Cells cell = row.CellList.Find(delegate (Cells c) { return c.ColNum == xmlFieldMapper.ExcelColumn; });
                                if (cell != null)
                                {
                                    #region Formatting to Date if it date field
                                    if (xmlFieldMapper.IsDateField)
                                    {
                                        DateTime dt;
                                        DateTime.TryParse(cell.CellValue, out dt);
                                        if (dt != DateTime.MinValue)
                                            cell.CellValue = dt.Year.ToString("0000") + "-" + dt.Month.ToString("00") + "-" + dt.Day.ToString("00");
                                        else
                                            cell.CellValue = "null";
                                    }
                                    #endregion

                                    #region Get Master Id Field
                                    if (xmlFieldMapper.MasterLookUpTable.Trim() != string.Empty)
                                    {
                                        cell.CellValue = getValueFromMaster(xmlFieldMapper.MasterLookUpTable, xmlFieldMapper.MasterIdField, xmlFieldMapper.MasterLookUpField, cell.CellValue);
                                    }
                                    #endregion

                                    #region Preparing the Exist Query
                                    if (xmlFieldMapper.IsIdField)
                                    {
                                        if (cell.CellValue == "null")
                                            existQuery.Append(" and " + xmlFieldMapper.DbField + " = " + cell.CellValue);
                                        else if (xmlFieldMapper.IsAlpha)
                                        {
                                            if (xmlFieldMapper.IsDateField)
                                            {
                                                string newVal = "cast('" + cell.CellValue + "' as DateTime)";
                                                existQuery.Append(" and " + xmlFieldMapper.DbField + " = " + newVal);
                                            }
                                            else
                                                existQuery.Append(" and " + xmlFieldMapper.DbField + " = '" + cell.CellValue + "'");
                                        }
                                        else
                                            existQuery.Append(" and " + xmlFieldMapper.DbField + " = " + cell.CellValue);
                                    }
                                    #endregion

                                    #region Preparing the Fetch Tourist No. Query
                                    if (xmlFieldMapper.FetchTouristNo)
                                    {
                                        if (cell.CellValue == "null")
                                            fetchTouristNoQuery.Append(" and " + xmlFieldMapper.DbField + " = " + cell.CellValue);
                                        else if (xmlFieldMapper.IsAlpha)
                                            fetchTouristNoQuery.Append(" and " + xmlFieldMapper.DbField + " = '" + cell.CellValue + "'");
                                        else
                                            fetchTouristNoQuery.Append(" and " + xmlFieldMapper.DbField + " = " + cell.CellValue);
                                    }
                                    #endregion

                                    insertQuery1.Append(xmlFieldMapper.DbField + ", ");

                                    #region Adding '', if field is Alpha
                                    if (xmlFieldMapper.IsAlpha)
                                    {
                                        if (cell.CellValue == "null")
                                        {
                                            insertQuery2.Append(cell.CellValue + ", ");
                                            updateQuery.Append(xmlFieldMapper.DbField + " = " + cell.CellValue + ", ");
                                        }
                                        else
                                        {
                                            string val = xmlFieldMapper.Encrypt ? DataSecurityManager.Encrypt(cell.CellValue) : cell.CellValue;
                                            insertQuery2.Append("'" + val + "', ");
                                            updateQuery.Append(xmlFieldMapper.DbField + " = '" + val + "', ");
                                        }
                                    }
                                    else
                                    {
                                        insertQuery2.Append(cell.CellValue + ", ");
                                        updateQuery.Append(xmlFieldMapper.DbField + " = " + cell.CellValue + ", ");
                                    }
                                    #endregion

                                    gotSomeCells = true;
                                }
                            }
                        }
                        #endregion

                        #region RowWise
                        if (readPattern.ReadPatternValue == "rowwise")
                        {
                            string prevValue = string.Empty;
                            foreach (Row columnarRow in rowList)
                            {
                                XMLFieldMapper xmlFieldMapper = readPattern.XmlFieldMapperList.Find(delegate (XMLFieldMapper fm) { return fm.ExcelRow == columnarRow.RowNum; });
                                if (xmlFieldMapper != null)
                                {
                                    Cells cell = columnarRow.CellList.Find(delegate (Cells c) { return c.ColNum == xmlFieldMapper.ExcelColumn; });
                                    if (cell != null)
                                    {
                                        #region Formatting to Date if it is date field
                                        if (xmlFieldMapper.IsDateField)
                                        {
                                            DateTime dt;
                                            DateTime.TryParse(cell.CellValue, out dt);
                                            if (dt != DateTime.MinValue)
                                                cell.CellValue = dt.Year.ToString("0000") + "-" + dt.Month.ToString("00") + "-" + dt.Day.ToString("00");
                                            else
                                                cell.CellValue = "null";
                                        }
                                        #endregion

                                        #region Formatting to Date if it is Time field
                                        if (xmlFieldMapper.TimeField)
                                        {
                                            cell.CellValue = cell.CellValue.Replace("HRS", "").Trim();

                                            DateTime dt;
                                            DateTime.TryParse(cell.CellValue, out dt);
                                            cell.CellValue = dt.ToShortTimeString();

                                            //if (cell.CellValue.Length == 4 && cell.CellValue != "null")
                                            //{
                                            //    string hh = cell.CellValue.Substring(0, 2);
                                            //    string mm = cell.CellValue.Substring(2);
                                            //    cell.CellValue = hh + ":" + mm;
                                            //}
                                            //else
                                            //    cell.CellValue = "null";
                                        }
                                        #endregion

                                        #region Formatting to Bool if it is Boolean field
                                        if (xmlFieldMapper.IsBool)
                                        {
                                            if (cell.CellValue.StartsWith("Y"))
                                            {
                                                cell.CellValue = "1";
                                            }
                                            else if (cell.CellValue.StartsWith("N"))
                                            {
                                                cell.CellValue = "0";
                                            }
                                        }
                                        #endregion

                                        #region Get Master Id Field
                                        if (xmlFieldMapper.MasterLookUpTable.Trim() != string.Empty)
                                        {
                                            cell.CellValue = getValueFromMaster(xmlFieldMapper.MasterLookUpTable, xmlFieldMapper.MasterIdField, xmlFieldMapper.MasterLookUpField, cell.CellValue);
                                        }
                                        #endregion

                                        #region Preparing the Exist Query
                                        if (xmlFieldMapper.IsIdField)
                                        {
                                            if (cell.CellValue == "null")
                                                existQuery.Append(" and " + xmlFieldMapper.DbField + " = " + cell.CellValue);
                                            else if (xmlFieldMapper.IsAlpha)
                                                existQuery.Append(" and " + xmlFieldMapper.DbField + " = '" + cell.CellValue + "'");
                                            else
                                                existQuery.Append(" and " + xmlFieldMapper.DbField + " = " + cell.CellValue);
                                        }
                                        #endregion

                                        if (!xmlFieldMapper.JoinToPrev)
                                        {
                                            insertQuery1.Append(xmlFieldMapper.DbField + ", ");
                                            #region Adding '', if field is Alpha
                                            if (xmlFieldMapper.IsAlpha)
                                            {
                                                if (cell.CellValue == "null")
                                                {
                                                    insertQuery2.Append(cell.CellValue + ", ");
                                                    updateQuery.Append(xmlFieldMapper.DbField + " = " + cell.CellValue + ", ");
                                                }
                                                else
                                                {
                                                    insertQuery2.Append("'" + cell.CellValue + "', ");
                                                    updateQuery.Append(xmlFieldMapper.DbField + " = '" + cell.CellValue + "', ");
                                                }
                                            }
                                            else
                                            {
                                                insertQuery2.Append(cell.CellValue + ", ");
                                                updateQuery.Append(xmlFieldMapper.DbField + " = " + cell.CellValue + ", ");
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            if (prevValue != "null" && cell.CellValue != "null")
                                            {
                                                int lastindex = insertQuery2.ToString().LastIndexOf("'");
                                                insertQuery2.Insert(lastindex, " " + cell.CellValue);

                                                lastindex = updateQuery.ToString().LastIndexOf("'");
                                                updateQuery.Insert(lastindex, " " + cell.CellValue);
                                            }
                                        }

                                        prevValue = cell.CellValue;
                                        gotSomeCells = true;
                                    }
                                }
                            }
                        }
                        #endregion
                    }

                    #region DB OPerations
                    if (gotSomeCells)
                    {
                        #region Get Tourist No.
                        fetchTouristNoQuery.Append(" and bookingId = " + BookingId.ToString());
                        int TouristNo = getTouristNo(fetchTouristNoQuery.ToString());

                        existQuery.Append(" and bookingId = " + BookingId.ToString());

                        insertQuery1.Append("TouristNo, ");
                        insertQuery2.Append(TouristNo.ToString() + ",");
                        updateQuery.Append("TouristNo = " + TouristNo.ToString());

                        #endregion

                        #region Adding Booking Id
                        insertQuery1.Append("BookingId) ");
                        insertQuery2.Append(BookingId.ToString() + ")");

                        if (updateQuery.ToString().EndsWith(","))
                            updateQuery = updateQuery.Remove(updateQuery.ToString().Trim().LastIndexOf(','), 1);

                        //updateQuery = updateQuery.Remove(updateQuery.Length - 1, 1);
                        updateQuery.Append(" where BookingId = " + BookingId.ToString());
                        updateQuery.Append(" and TouristNo = " + TouristNo.ToString());
                        #endregion

                        insertQuery1.Append(insertQuery2);

                        if (IsExists(existQuery.ToString()))
                        {
                            UpdateRecord(updateQuery.ToString());
                        }
                        else
                        {
                            InsertRecord(insertQuery1.ToString());

                        }
                    }
                    #endregion
                }

                RemoveExtraInsertedRecords(BookingId);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        private void RemoveExtraInsertedRecords(int BookingId)
        {
            DataSet ds;
            StringBuilder Query = new StringBuilder();
            Query.Append("SELECT BookingId, TouristNo, COUNT(tblBookingTouristDetails.TouristNo) AS 'ExtraRecords' ");
            Query.Append(" FROM tblBookingTouristDetails GROUP BY BookingId, tblBookingTouristDetails.TouristNo ");
            Query.Append(" HAVING (COUNT(tblBookingTouristDetails.TouristNo) > 1)");

            StringBuilder deleteQuery = new StringBuilder();
            deleteQuery.Append("delete Top([count]) from tblBookingTouristDetails where 1=1 ");

            DatabaseManager oDB = null;
            oDB = new DatabaseManager();
            ds = oDB.ExecuteDataSet(Query.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        int duplicateRecs = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray.GetValue(2));
                        duplicateRecs = duplicateRecs == 0 ? 0 : duplicateRecs - 1;
                        deleteQuery.Replace("[count]", duplicateRecs.ToString());
                        deleteQuery.Append(" AND  BookingId = " + Convert.ToString(ds.Tables[0].Rows[i].ItemArray.GetValue(0)));
                        deleteQuery.Append(" AND  TouristNo = " + Convert.ToString(ds.Tables[0].Rows[i].ItemArray.GetValue(1)));
                        oDB.ExecuteNonQuery(deleteQuery.ToString());
                    }
                }
            }
        }

        private int getTouristNo(String WhereClause)
        {
            string sProcName;
            DatabaseManager oDB;
            int TouristNo = 0;

            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Get_TouristNo";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@WhereClause", DbType.String, WhereClause); //Max. 2000              
                TouristNo = Convert.ToInt32(oDB.ExecuteScalar(oDB.DbCmd));
            }
            catch (Exception exp)
            {
                oDB = null;
                GF.LogError("clsTouristUploadFileProcessor.getTouristNo", exp.Message);
            }
            return TouristNo;
        }

        private void UpdateRecord(String updateQuery)
        {
            try
            {
                processRecord(updateQuery);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        private void InsertRecord(String insertQuery)
        {
            try
            {
                processRecord(insertQuery);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        private static void processRecord(string Query)
        {
            DatabaseManager oDB = null;
            try
            {
                if (oDB == null)
                    oDB = new DatabaseManager();
                oDB.ExecuteNonQuery(Query);
            }
            catch (Exception exp)
            {
                //GF.LogError("clsTouristUploadFileProcessor.processRecord", exp.Message + " - " + Query);
                throw exp;
            }
        }

        private bool IsExists(String existQuery)
        {
            DataSet ds = GF.GetDataFromDB(existQuery);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0].ItemArray.GetValue(0) != DBNull.Value)
                return true;
            else
                return false;
        }

        private string getValueFromMaster(string MasterLookUpTable, string MasterIdField, string MasterLookUpField, string FieldValue)
        {
            DataSet ds;
            string Query;
            Query = "select " + MasterIdField + " from " + MasterLookUpTable + " where " + MasterLookUpField + " = '" + FieldValue + "'";

            ds = GF.GetDataFromDB(Query);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0].ItemArray.GetValue(0) != null)
                    return Convert.ToString(ds.Tables[0].Rows[0].ItemArray.GetValue(0));
                else
                    return "null";
            }
            return "null";
        }

        #endregion
    }

    class Row
    {
        string _rowNum;
        List<Cells> _cellList;

        public string RowNum
        {
            get { return _rowNum; }
            set { _rowNum = value; }
        }

        public List<Cells> CellList
        {
            get { return _cellList; }
            set { _cellList = value; }
        }
    }

    class Cells
    {
        string _colNum;
        string _CellValue;

        public string ColNum
        {
            get { return _colNum; }
            set { _colNum = value; }
        }

        public string CellValue
        {
            get { return _CellValue; }
            set { _CellValue = value; }
        }
    }

    class XMLMapper
    {
        string _TableName;
        int _StartingRow;
        List<ReadPattern> _readPatternList;

        public XMLMapper()
        {
            _TableName = string.Empty;
            _StartingRow = 0;
            _readPatternList = null;
        }

        public string TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }

        public int StartingRow
        {
            get { return _StartingRow; }
            set { _StartingRow = value; }
        }

        public List<ReadPattern> ReadPatternList
        {
            get { return _readPatternList; }
            set { _readPatternList = value; }
        }
    }

    class ReadPattern
    {
        string _ReadPatternValue;
        string _StartRow;
        string _StartCol;
        string _EndCol;
        List<XMLFieldMapper> _xmlFieldMapper;

        public ReadPattern()
        {
            _ReadPatternValue = string.Empty;
            _StartRow = string.Empty;
            _StartCol = string.Empty;
            _EndCol = string.Empty;
            _xmlFieldMapper = null;
        }

        public string ReadPatternValue
        {
            get { return _ReadPatternValue; }
            set { _ReadPatternValue = value; }
        }

        public string StartRow
        {
            get { return _StartRow; }
            set { _StartRow = value; }
        }

        public string StartCol
        {
            get { return _StartCol; }
            set { _StartCol = value; }
        }

        public string EndCol
        {
            get { return _EndCol; }
            set { _EndCol = value; }
        }

        public List<XMLFieldMapper> XmlFieldMapperList
        {
            get { return _xmlFieldMapper; }
            set { _xmlFieldMapper = value; }
        }
    }

    class XMLFieldMapper
    {
        string _DbField;
        string _ExcelColumn;
        string _ExcelRow;
        bool _IsIdField;
        bool _IsAlpha;
        string _MasterLookUpTable;
        string _MasterLookUpField;
        string _MasterIdField;
        bool _IsDateField;
        bool _TimeField;
        bool _JoinToPrev;
        bool _IsBool;
        bool _FetchTouristNo;
        bool _Encrypt;

        public XMLFieldMapper()
        {
            _DbField = string.Empty;
            _ExcelColumn = string.Empty;
            _ExcelRow = string.Empty;
            _IsIdField = false;
            _IsAlpha = false;
            _MasterLookUpTable = string.Empty;
            _MasterLookUpField = string.Empty;
            _IsDateField = false;
            _JoinToPrev = false;
        }

        public string MasterIdField
        {
            get { return _MasterIdField; }
            set { _MasterIdField = value; }
        }

        public bool IsDateField
        {
            get { return _IsDateField; }
            set { _IsDateField = value; }
        }

        public string MasterLookUpField
        {
            get { return _MasterLookUpField; }
            set { _MasterLookUpField = value; }
        }

        public bool IsIdField
        {
            get { return _IsIdField; }
            set { _IsIdField = value; }
        }

        public bool IsAlpha
        {
            get { return _IsAlpha; }
            set { _IsAlpha = value; }
        }

        public string MasterLookUpTable
        {
            get { return _MasterLookUpTable; }
            set { _MasterLookUpTable = value; }
        }

        public string DbField
        {
            get { return _DbField; }
            set { _DbField = value; }
        }

        public string ExcelColumn
        {
            get { return _ExcelColumn; }
            set { _ExcelColumn = value; }
        }

        public string ExcelRow
        {
            get { return _ExcelRow; }
            set { _ExcelRow = value; }
        }

        public bool JoinToPrev
        {
            get { return _JoinToPrev; }
            set { _JoinToPrev = value; }
        }

        public bool TimeField
        {
            get { return _TimeField; }
            set { _TimeField = value; }
        }

        public bool IsBool
        {
            get { return _IsBool; }
            set { _IsBool = value; }
        }

        public bool FetchTouristNo
        {
            get { return _FetchTouristNo; }
            set { _FetchTouristNo = value; }
        }

        public bool Encrypt
        {
            get { return _Encrypt; }
            set { _Encrypt = value; }
        }
    }
}
