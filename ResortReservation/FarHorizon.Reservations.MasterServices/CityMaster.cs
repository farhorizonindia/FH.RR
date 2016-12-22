using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using FarHorizon.Reservations.DataBaseManager;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Masters;

namespace FarHorizon.Reservations.MasterServices
{
    public class CityMaster 
    {
        #region IMaster Members

        public bool Insert(CityDTO oCityData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Ins_CityMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sCityCode", DbType.String, oCityData.CityCode);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sCityName", DbType.String, oCityData.CityName);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oCityData = null;
                GF.LogError("clsCityMaster.Insert", exp.Message);
                return false;
            }
            finally
            {
                oCityData = null;
                oDB = null;
            }
            return true;
        }

        public bool Update(CityDTO oCityData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Upd_CityMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@CityId", DbType.Int32, oCityData.CityId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sCityCode", DbType.String, oCityData.CityCode);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sCityName", DbType.String, oCityData.CityName);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oCityData = null;
                GF.LogError("clsCityMaster.Update", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
                oCityData = null;
            }
            return true;
        }

        public bool Delete(CityDTO oCityData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Del_CityMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@CityId", DbType.Int32, oCityData.CityId);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oCityData = null;
                GF.LogError("clsCityMaster.Delete", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
                oCityData = null;
            }
            return true;
        }

        public CityDTO[] GetData()
        {
            CityDTO[] oCityData;
            oCityData = GetData(0);
            return oCityData;
        }

        public CityDTO[] GetData(int CityId)
        {
            CityDTO[] oCityData;
            oCityData = null;
            DataSet ds;
            string query = "select CityId, CityCode, CityName from tblCityMaster where 1=1";
            if (CityId != 0)
            {
                query += " and CityId=" + CityId;
                query += " order by CityCode";
            }
            ds = GetDataFromDB(query);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                oCityData = new CityDTO[ds.Tables[0].Rows.Count];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    oCityData[i] = new CityDTO();
                    oCityData[i].CityId = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                    oCityData[i].CityCode = Convert.ToString(ds.Tables[0].Rows[i][1]);
                    oCityData[i].CityName = Convert.ToString(ds.Tables[0].Rows[i][2]);
                }
            }
            return oCityData;
        }

        private DataSet GetDataFromDB(string Query)
        {
            DatabaseManager oDB = new DatabaseManager();
            DataSet ds = null;
            try
            {

                oDB.DbCmd = oDB.GetSqlStringCommand(Query);
                //DataSet ds = oDB.FetchRecords("tblBookingDetails", Query);
                ds = oDB.ExecuteDataSet(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                ds = null;
                GF.LogError("clsCityMaster.GetDataFromDB", exp.Message);
            }
            return ds;
        }

        public string GetCityName(int cityId)
        {
            string cityName = string.Empty;
            CityDTO[] cityList = GetData(cityId);
            if (cityList != null && cityList.Length > 0)
            {
                cityName = cityList[0].CityName;
            }
            return cityName;
        }
        #endregion        
    }
}
