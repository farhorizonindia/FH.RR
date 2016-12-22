using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.DataBaseManager;
using FarHorizon.Reservations.Common.DataEntities.Masters;

namespace FarHorizon.Reservations.MasterServices
{
    public class TransportMaster 
    {
        #region IMaster Members

        public bool Insert(TransportDTO oTransportData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {                
                oDB = new DatabaseManager();
                sProcName = "up_Ins_TransportMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@TransportName", DbType.String, oTransportData.TransportName);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oTransportData = null;
                GF.LogError("clsTransportMaster.Insert", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
                oTransportData = null;
            }
            return true;
        }

        public bool Update(TransportDTO oTransportData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Upd_TransportMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iTransportID", DbType.Int32, oTransportData.TransportId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Transport", DbType.String, oTransportData.TransportName);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oTransportData = null;
                GF.LogError("clsTransportMaster.Update", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
                oTransportData = null;
            }
            return true;
        }

        public bool Delete(TransportDTO oTransportData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Del_TransportMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@TransportId", DbType.Int32, oTransportData.TransportId);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oTransportData = null;
                GF.LogError("clsTransportMaster.Delete", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
                oTransportData = null;
            }
            return true;
        }

        public TransportDTO[] GetData()
        {
            TransportDTO[] oTransportData;
            oTransportData = GetData(0);
            return oTransportData;
        }

        public TransportDTO[] GetData(int TransportId)
        {
            TransportDTO[] oTransportData;
            oTransportData = null;

            string query = "select TransportID, TransportName from tblTransportMaster where 1=1";
            if (TransportId != 0)
                query += " and TransportID=" + TransportId;
            query += " order by [TransportName]";
            oTransportData = PopulateDataObject(query);
            return oTransportData;
        }

        private TransportDTO[] PopulateDataObject(string Query)
        {
            TransportDTO[] oTransportData;
            DataSet ds;
            oTransportData = null;
            ds = GetDataFromDB(Query);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                oTransportData = new TransportDTO[ds.Tables[0].Rows.Count];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    oTransportData[i] = new TransportDTO();
                    if (ds.Tables[0].Rows[i][0] != DBNull.Value)
                        oTransportData[i].TransportId = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                    if (ds.Tables[0].Rows[i][1] != DBNull.Value)
                        oTransportData[i].TransportName = Convert.ToString(ds.Tables[0].Rows[i][1]);
                }
            }
            return oTransportData;
        }

        private DataSet GetDataFromDB(string query)
        {
            DatabaseManager oDB = new DatabaseManager();
            DataSet ds = null;
            try
            {
                oDB.DbCmd = oDB.GetSqlStringCommand(query);
                //DataSet ds = oDB.FetchRecords("tblBookingDetails", Query);
                ds = oDB.ExecuteDataSet(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                GF.LogError("clsTransportData.GetDataFromDB", exp.Message);
                oDB = null;
                ds = null;
            }
            return ds;
        }
        #endregion

        public string GetTransportName(int transportId)
        {
            string transportName = string.Empty;
            TransportDTO[] transportDto = GetData(transportId);
            if (transportDto != null && transportDto.Length > 0)
            {
                transportName = transportDto[0].TransportName;
            }
            return transportName;
        }

        #region IMaster Members       

        #endregion

        #region IMaster Members      

        #endregion
    }
}
