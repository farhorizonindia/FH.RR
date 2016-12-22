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
    public class DepartmentMaster 
    {
        #region IMaster Members

        public bool Insert(DepartmentDTO oDepartmentData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Ins_DepartmentMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sDepartmentCode", DbType.String, oDepartmentData.DepartmentCode);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sDepartmentName", DbType.String, oDepartmentData.DepartmentName);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oDepartmentData = null;
                GF.LogError("clsDepartmentMaster.Insert", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
                oDepartmentData = null;
            }
            return true;
        }

        public bool Update(DepartmentDTO oDepartmentData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Upd_DepartmentMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@DepartmentId", DbType.Int32, oDepartmentData.DepartmentId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sDepartmentCode", DbType.String, oDepartmentData.DepartmentCode);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sDepartmentName", DbType.String, oDepartmentData.DepartmentName);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oDepartmentData = null;
                GF.LogError("clsDepartmentMaster.Insert", exp.Message);
                return false;
            }
            finally
            {
                oDepartmentData = null;
                oDB = null;
            }
            return true;
        }

        public bool Delete(DepartmentDTO oDepartmentData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Del_DepartmentMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@DepartmentId", DbType.Int32, oDepartmentData.DepartmentId);
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

        public DepartmentDTO[] GetData()
        {
            return GetData(0);
        }

        public DepartmentDTO[] GetData(int DepartmentId)
        {
            DepartmentDTO[] oDepartmentData;
            oDepartmentData = null;
            DataSet ds;
            string query = "select DepartmentId, DepartmentCode, DepartmentName from tblDepartmentMaster where 1=1";
            if (DepartmentId != 0)
            {
                query += " and DepartmentId=" + DepartmentId;
                query += " order by DepartmentCode";
            }
            ds = GetDataFromDB(query);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                oDepartmentData = new DepartmentDTO[ds.Tables[0].Rows.Count];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    oDepartmentData[i] = new DepartmentDTO();
                    oDepartmentData[i].DepartmentId = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                    oDepartmentData[i].DepartmentCode = Convert.ToString(ds.Tables[0].Rows[i][1]);
                    oDepartmentData[i].DepartmentName = Convert.ToString(ds.Tables[0].Rows[i][2]);
                }
            }
            return oDepartmentData;
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
                GF.LogError("clsDepartmentMaster.GetDataFromDB", exp.Message);
            }
            return ds;
        }

        #endregion
    }
}
