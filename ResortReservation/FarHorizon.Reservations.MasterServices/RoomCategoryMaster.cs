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
    public class RoomCategoryMaster 
    {
        #region IMaster Members

        public bool Insert(RoomCategoryDTO oRoomCategoryData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Ins_RoomCategoryMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sRoomCategory", DbType.String, oRoomCategoryData.RoomCategory.Trim());
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@categoryAlias", DbType.String, oRoomCategoryData.CategoryAlias.Trim());
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oRoomCategoryData = null;
                GF.LogError("clsRoomCategoryMaster.Insert", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public bool Update(RoomCategoryDTO oRoomCategoryData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Upd_RoomCategoryMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd,"@iRoomCategoryID", DbType.Int32,oRoomCategoryData.RoomCategoryId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sRoomCategory", DbType.String, oRoomCategoryData.RoomCategory.Trim());
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@categoryAlias", DbType.String, oRoomCategoryData.CategoryAlias.Trim());
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oRoomCategoryData = null;
                GF.LogError("clsRoomCategoryMaster.Update", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
                oRoomCategoryData = null;   
            }
            return true;
        }

        public bool Delete(RoomCategoryDTO oRoomCategoryData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Del_RoomCategoryMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);                
                oDB.DbDatabase.AddInParameter(oDB.DbCmd,"@iRoomCategoryId", DbType.Int32, oRoomCategoryData.RoomCategoryId);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oRoomCategoryData = null;
                GF.LogError("clsRoomCategoryMaster.Delete", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
                oRoomCategoryData = null;
            }
            return true;
        }

        public RoomCategoryDTO[] GetData()
        {
            return GetData(0);
        }
        public DataSet GetallData()
        {
            RoomCategoryDTO[] oRoomCategoryData;
            oRoomCategoryData = null;

            string query = "select RoomCategoryID, RoomCategory, CategoryAlias from tblRoomCategoryMaster where 1=1";
            //if (RoomCategoryId != 0)
            //    query += " and RoomCategoryID=" + RoomCategoryId;
            //query += " order by RoomCategory";
            oRoomCategoryData = PopulateDataObject(query);
            DataSet ds = GetDataFromDB(query);
            return ds;
        }
        public RoomCategoryDTO[] GetData(int RoomCategoryId)
        {
            RoomCategoryDTO[] oRoomCategoryData;
            oRoomCategoryData = null;

            string query = "select RoomCategoryID, RoomCategory, CategoryAlias from tblRoomCategoryMaster where 1=1";
            if (RoomCategoryId != 0)
                query += " and RoomCategoryID=" + RoomCategoryId;
            query += " order by RoomCategory";
            oRoomCategoryData = PopulateDataObject(query);
            return oRoomCategoryData;
        }

        private RoomCategoryDTO[] PopulateDataObject(string Query)
        {
            RoomCategoryDTO[] oRoomCategoryData;
            DataSet ds;
            oRoomCategoryData = null;
            ds = GetDataFromDB(Query);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                oRoomCategoryData = new RoomCategoryDTO[ds.Tables[0].Rows.Count];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    oRoomCategoryData[i] = new RoomCategoryDTO();
                    oRoomCategoryData[i].RoomCategoryId = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                    oRoomCategoryData[i].RoomCategory = Convert.ToString(ds.Tables[0].Rows[i][1]).Trim();
                    oRoomCategoryData[i].CategoryAlias = Convert.ToString(ds.Tables[0].Rows[i][2]).Trim();
                }
            }
            return oRoomCategoryData;
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
                GF.LogError("clsRoomCategoryMaster.GetDataFromDB", exp.Message);
                oDB = null;
                ds = null;
            }
            return ds;
        }

        #endregion
    }
}
