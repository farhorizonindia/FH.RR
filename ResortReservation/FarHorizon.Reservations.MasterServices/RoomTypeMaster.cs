using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using FarHorizon.Reservations.Common;


using FarHorizon.Reservations.DataBaseManager;
using System.Data;
using System.Data.SqlClient;
using FarHorizon.Reservations.Common.DataEntities.Masters;


namespace FarHorizon.Reservations.MasterServices
{
    public class RoomTypeMaster 
    {
        #region IMaster Members
        public bool Insert(RoomTypeDTO oRoomTypeData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();                
                sProcName = "up_Ins_RoomTypeMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sRoomType", DbType.String, oRoomTypeData.RoomType.Trim());
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iDefaultNoOfBeds", DbType.Int32, oRoomTypeData.DefaultNoOfBeds);                                
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oRoomTypeData = null;
                GF.LogError("clsRoomtypeMaster.Insert", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
                oRoomTypeData = null;
            }
            return true;
        }
        public bool Update(RoomTypeDTO oRoomTypeData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Upd_RoomTypeMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iRoomTypeId", DbType.Int32,oRoomTypeData.RoomTypeId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sRoomType", DbType.String,oRoomTypeData.RoomType.Trim());
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iDefaultNoOfBeds", DbType.Int32, oRoomTypeData.DefaultNoOfBeds);                                
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oRoomTypeData = null;
                GF.LogError("clsRoomtypeMaster.Update", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
                oRoomTypeData = null;
            }
            return true;
        }
        public bool Delete(RoomTypeDTO oRoomTypeData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Del_RoomTypeMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iRoomtypeId", DbType.Int32, oRoomTypeData.RoomTypeId);                
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oRoomTypeData = null;
                GF.LogError("clsRoomtypeMaster.Delete", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }
        public RoomTypeDTO[] GetData()
        {
            return GetData(0);
        }
        public RoomTypeDTO[] GetData(int RoomTypeId)
        {
            DataSet ds;
            RoomTypeDTO[] oRoomTypeData;
            oRoomTypeData = null;
            ds = null;

            string query = "select RoomTypeId, RoomType, DefaultNoOfBeds from tblRoomTypeMaster where 1=1";
            if (RoomTypeId != 0)
            {
                query += " and RoomTypeId=" + RoomTypeId;
            }
            query += " order by RoomType";
            ds = GetDataFromDB(query);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                oRoomTypeData = new RoomTypeDTO[ds.Tables[0].Rows.Count];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    oRoomTypeData[i] = new RoomTypeDTO();
                    oRoomTypeData[i].RoomTypeId = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                    oRoomTypeData[i].RoomType = Convert.ToString(ds.Tables[0].Rows[i][1]).Trim();
                    oRoomTypeData[i].DefaultNoOfBeds = Convert.ToInt32(ds.Tables[0].Rows[i][2]);
                }
            }
            return oRoomTypeData;
        }
        public DataSet GetallData()
        {
            DataSet ds;
            RoomTypeDTO[] oRoomTypeData;
            oRoomTypeData = null;
            ds = null;
            DataTable dt;
            string query = "select RoomTypeId, RoomType, DefaultNoOfBeds from tblRoomTypeMaster where 1=1";
            //if (RoomTypeId != 0)
            //{
            //    query += " and RoomTypeId=" + RoomTypeId;
            //}
            //query += " order by RoomType";
            ds = GetDataFromDB(query);
            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    oRoomTypeData = new RoomTypeDTO[ds.Tables[0].Rows.Count];
            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {
            //        oRoomTypeData[i] = new RoomTypeDTO();
            //        oRoomTypeData[i].RoomTypeId = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
            //        oRoomTypeData[i].RoomType = Convert.ToString(ds.Tables[0].Rows[i][1]).Trim();
            //        oRoomTypeData[i].DefaultNoOfBeds = Convert.ToInt32(ds.Tables[0].Rows[i][2]);
            //    }
            //}
            return ds;
        }
        private DataSet GetDataFromDB(string query)
        {
            DatabaseManager oDB;
            DataSet ds =null;
            try
            {
                oDB = new DatabaseManager();
                oDB.DbCmd = oDB.GetSqlStringCommand(query);
                //DataSet ds = oDB.FetchRecords("tblOccupancyTypeMaster", query);
                ds = oDB.ExecuteDataSet(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                ds = null;
                GF.LogError("clsRoomtypeMaster.GetDataFromDB", exp.Message);
            }
            return ds;
        }
        #endregion
    }
}
