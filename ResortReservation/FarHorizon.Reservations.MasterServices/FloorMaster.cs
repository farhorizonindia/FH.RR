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
    public class FloorMaster 
    {
        #region IMaster Members

        public bool Insert(FloorDTO oFloorData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Ins_FloorMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iFloor", DbType.Int32, oFloorData.Floor);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oFloorData = null;
                GF.LogError("clsFloorMaster.Insert", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
                oFloorData = null;  
            }
            return true;
        }

        public bool Update(FloorDTO oFloorData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Upd_FloorMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iFloorID", DbType.Int32, oFloorData.FloorId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iFloor", DbType.Int32, oFloorData.Floor);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oFloorData = null;
                GF.LogError("clsFloorMaster.Update", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
                oFloorData = null;
            }
            return true;
        }

        public bool Delete(FloorDTO oFloorData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Del_FloorMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iFloorId", DbType.Int32, oFloorData.FloorId);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oFloorData = null;
                GF.LogError("clsFloorMaster.Delete", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
                oFloorData = null;
            }
            return true;
        }

        public FloorDTO[] GetData()
        {
            FloorDTO[] oFloorData;
            oFloorData = GetData(0);
            return oFloorData;
        }

        public FloorDTO[] GetData(int FloorId)
        {
            FloorDTO[] oFloorData;
            oFloorData = null;

            string query = "select FloorID, Floor from tblFloorMaster where 1=1";
            if (FloorId != 0)
                query += " and FloorID=" + FloorId;
            query += " order by [Floor]";
            oFloorData = PopulateDataObject(query);
            return oFloorData;
        }

        private FloorDTO[] PopulateDataObject(string Query)
        {
            FloorDTO[] oFloorData;
            DataSet ds;
            oFloorData = null;
            ds = GetDataFromDB(Query);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                oFloorData = new FloorDTO[ds.Tables[0].Rows.Count];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    oFloorData[i] = new FloorDTO();
                    if(ds.Tables[0].Rows[i][0] !=DBNull.Value)
                    oFloorData[i].FloorId = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                    if(ds.Tables[0].Rows[i][1] != DBNull.Value)
                        oFloorData[i].Floor = Convert.ToInt32(ds.Tables[0].Rows[i][1]);
                }
            }
            return oFloorData;
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
                GF.LogError("clsFloorData.GetDataFromDB", exp.Message);
                oDB = null;
                ds = null;
            }
            return ds;
        }

        #endregion
    }
}
