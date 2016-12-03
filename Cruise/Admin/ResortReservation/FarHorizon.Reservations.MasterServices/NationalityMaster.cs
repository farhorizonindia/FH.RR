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
    public class NationalityMaster 
    {
        #region IMaster Members

        public bool Insert(NationalityDTO oNationalityData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Ins_NationalityMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sNationality", DbType.String, oNationalityData.Nationality);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oNationalityData = null;
                GF.LogError("clsNationalityMaster.Insert",exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public bool Update(NationalityDTO oNationalityData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Upd_NationalityMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iNationalityID", DbType.Int32, oNationalityData.NationalityId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sNationality", DbType.String, oNationalityData.Nationality);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oNationalityData = null;
                GF.LogError("clsNationalityMaster.Update", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public bool Delete(NationalityDTO oNationalityData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Del_NationalityMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iNationalityId", DbType.Int32, oNationalityData.NationalityId);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oNationalityData = null;
                GF.LogError("clsNationalityMaster.Delete", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public NationalityDTO[] GetData()
        {
            return GetData(0);
        }

        public String GetNationalityName(int NationalityId)
        {
            NationalityDTO[] oNationalityData;
            oNationalityData = null;

            oNationalityData = GetData(NationalityId);
            if (oNationalityData != null && oNationalityData.Length > 0)
            {
                return oNationalityData[0].Nationality;
            }
            else
            {
                return null;
            }
        }

        public NationalityDTO[] GetData(int NationalityId)
        {
            NationalityDTO[] oNationalityData;
            oNationalityData = null;

            string query = "select NationalityID, Nationality from tblNationalityMaster where 1=1";
            if (NationalityId != 0)
                query += " and NationalityID=" + NationalityId;
            query += " order by Nationality";
            oNationalityData = PopulateDataObject(query);
            return oNationalityData;
        }

        private NationalityDTO[] PopulateDataObject(string Query)
        {
            NationalityDTO[] oNationalityData;
            DataSet ds;
            oNationalityData = null;
            ds = GetDataFromDB(Query);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                oNationalityData = new NationalityDTO[ds.Tables[0].Rows.Count];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    oNationalityData[i] = new NationalityDTO();
                    oNationalityData[i].NationalityId = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                    oNationalityData[i].Nationality = Convert.ToString(ds.Tables[0].Rows[i][1]);
                }
            }
            return oNationalityData;
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
                GF.LogError("clsNationalityMaster.GetDataFromDB", exp.Message);
                ds = null;
            }
            return ds;
        }

        #endregion
    }
}
