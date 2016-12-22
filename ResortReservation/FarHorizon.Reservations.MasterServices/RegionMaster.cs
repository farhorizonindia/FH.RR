using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using FarHorizon.Reservations.Common;

using FarHorizon.Reservations.DataBaseManager;
using FarHorizon.Reservations.Common.DataEntities.Masters;

namespace FarHorizon.Reservations.MasterServices
{
    public class RegionMaster
    {
        #region IMaster Members

        public bool Insert(RegionDTO oRegionData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Ins_RegionMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sRegionName", DbType.String, oRegionData.RegionName);
                oDB.ExecuteNonQuery(oDB.DbCmd);
                //oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oRegionData = null;
                throw exp;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public bool Update(RegionDTO oRegionData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Upd_RegionMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iRegionId", DbType.Int32, oRegionData.RegionId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sRegionName", DbType.String, oRegionData.RegionName);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oRegionData = null;
                GF.LogError("clsRegionMaster.Update", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public bool Delete(RegionDTO oRegionData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Del_RegionMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iRegionId", DbType.Int32, oRegionData.RegionId);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                throw exp;                
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public RegionDTO[] GetData()
        {
            return GetData(0);
        }

        public RegionDTO[] GetData(int RegionId)
        {
            RegionDTO[] oRegionData;
            try
            {
                string query = "select RegionId, RegionName from tblRegionMaster where 1=1";
                if (RegionId != 0)
                    query += " and RegionId=" + RegionId;
                query += " order by RegionName";
                oRegionData = PopulateDataObject(query);
            }
            catch (Exception exp)
            {
                throw exp;
            }            
            return oRegionData;
        }

        private RegionDTO[] PopulateDataObject(string Query)
        {
            RegionDTO[] oRegionData;
            DataSet ds;
            try
            {
                oRegionData = null;
                ds = GetDataFromDB(Query);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    oRegionData = new RegionDTO[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        oRegionData[i] = new RegionDTO();
                        oRegionData[i].RegionId = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                        oRegionData[i].RegionName = Convert.ToString(ds.Tables[0].Rows[i][1]);
                    }
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                ds = null;
            }
            return oRegionData;
        }

        private DataSet GetDataFromDB(string query)
        {
            DatabaseManager oDB = new DatabaseManager();
            DataSet ds = null;
            try
            {
                oDB.DbCmd = oDB.GetSqlStringCommand(query);
                ds = oDB.ExecuteDataSet(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                oDB = null;
            }
            return ds;
        }

        #endregion
    }
}
