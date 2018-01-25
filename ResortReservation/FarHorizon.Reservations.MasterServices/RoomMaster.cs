using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.DataBaseManager;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.Common.DataEntities.Client;


namespace FarHorizon.Reservations.MasterServices
{
    public class RoomMaster 
    {
        #region IMaster Members

        public bool Insert(RoomDTO oAccomRoomData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Ins_RoomMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);

                oDB.DbDatabase.AddInParameter(oDB.DbCmd,"@sRoomNo", DbType.String, oAccomRoomData.RoomNo);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd,"@iAccomId", DbType.Int32,oAccomRoomData.AccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd,"@iFloorId", DbType.Int32,oAccomRoomData.FloorId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd,"@iRoomCategoryId", DbType.Int32,oAccomRoomData.RoomCategoryId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd,"@iRoomTypeId", DbType.Int32,oAccomRoomData.RoomTypeId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd,"@iNo_of_Beds", DbType.Int32,oAccomRoomData.No_of_Beds);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd,"@sDescription", DbType.String, oAccomRoomData.Description);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd,"@sTelExtnNo", DbType.String, oAccomRoomData.TelExtnNo);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd,"@iExtraBeds", DbType.Int32, oAccomRoomData.ExtraBeds);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd,"@fExtraBedRate", DbType.Double,oAccomRoomData.ExtraBedRate);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sUsername", DbType.String, "admin");
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtUseDate", DbType.DateTime, DateTime.Now);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@bConvertable", DbType.Boolean, oAccomRoomData.Convertable);
              
                oDB.ExecuteNonQuery(oDB.DbCmd);                
            }
            catch (Exception exp)
            {
                oDB = null;
                oAccomRoomData = null;
                GF.LogError("clsRoomMaster.Insert", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
                oAccomRoomData = null;
            }
            return true;
        }
        public bool updatestatus(RoomDTO oAccomRoomData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "updateactivestatus";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);

                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sRoomNo", DbType.String, oAccomRoomData.RoomNo);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomId", DbType.Int32, oAccomRoomData.AccomodationId);
               

                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oAccomRoomData = null;
                GF.LogError("clsRoomMaster.Insert", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
                oAccomRoomData = null;
            }
            return true;
        }

        public bool Update(RoomDTO oAccomRoomData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Upd_RoomMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);

                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sRoomNo", DbType.String, oAccomRoomData.RoomNo);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomId", DbType.Int32, oAccomRoomData.AccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iSequence",DbType.Int32,oAccomRoomData.Sequence);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iFloorId", DbType.Int32, oAccomRoomData.FloorId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iRoomCategoryId", DbType.Int32, oAccomRoomData.RoomCategoryId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iRoomTypeId", DbType.Int32, oAccomRoomData.RoomTypeId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iNo_of_Beds", DbType.Int32, oAccomRoomData.No_of_Beds);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sDescription", DbType.String, oAccomRoomData.Description);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sTelExtnNo", DbType.String, oAccomRoomData.TelExtnNo);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iExtraBeds", DbType.Int32, oAccomRoomData.ExtraBeds);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@fExtraBedRate", DbType.Double, oAccomRoomData.ExtraBedRate);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sUsername", DbType.String, "admin");
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtUseDate", DbType.DateTime, DateTime.Now);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@bConvertable", DbType.Boolean, oAccomRoomData.Convertable);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@status", DbType.Boolean, oAccomRoomData.Status);
              
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oAccomRoomData = null;
                GF.LogError("clsRoomMaster.Update", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
                oAccomRoomData = null;
            }
            return true;
        }

        public bool Delete(RoomDTO oAccomRoomData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Del_RoomMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd,"@iAccomId", DbType.Int32,oAccomRoomData.AccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sRoomNo", DbType.String, oAccomRoomData.RoomNo);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oAccomRoomData = null;
                GF.LogError("clsRoomMaster.Delete", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
                oAccomRoomData = null;
            }
            return true;
        }

        public RoomDTO[] GetData()
        {
            return GetData("");
        }

        public RoomDTO[] GetData(int AccomodationId, string RoomNo)
        {
            //clsAccomRoomData[] oAccomRoomData;
            //oAccomRoomData = null;
            string wClause ="";
            if (AccomodationId != 0)
                wClause += " and AccomId=" + AccomodationId;
            if (RoomNo != "")
                wClause += " and RoomNo='" + RoomNo + "'";
            return GetData(wClause);
        }               

        public RoomDTO[] GetData(int AccomodationId)
        {
            string wClause ="";
            if (AccomodationId != 0)
                wClause += " and AccomId=" + AccomodationId;
            return GetData(wClause);
        }

        private RoomDTO[] GetData(string WhereClause)
        {
            RoomDTO[] oAccomRoomData;
            oAccomRoomData = null;

            string query = "select RoomNo, Sequence, AccomId, FloorId, RoomCategoryId, RoomTypeId, " +
                " No_of_Beds, Description, TelExtnNo, ExtraBeds, ExtraBedRate, Convertable,isnull(Status,'true') " +
                " from tblRoomMaster where 1=1";

            if (WhereClause != "")
                query += WhereClause;
            query += " order by Sequence";
            oAccomRoomData = PopulateDataObject(query);
            return oAccomRoomData;
        }

        private RoomDTO[] PopulateDataObject(string Query)
        {
            RoomDTO[] oAccomRoomData;
            DataSet ds;
            oAccomRoomData = null;
            ds = GetDataFromDB(Query);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                //RoomNo, Sequence, AccomId, FloorId, TypeID, Occupancy, No_of_Beds, Description, 
                //TelExtnNo, ExtraBedAllowed, ExtraBedRate
                oAccomRoomData = new RoomDTO[ds.Tables[0].Rows.Count];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    oAccomRoomData[i] = new RoomDTO();                                        
                    oAccomRoomData[i].RoomNo = Convert.ToString(ds.Tables[0].Rows[i][0]);
                    oAccomRoomData[i].Sequence = Convert.ToInt32(ds.Tables[0].Rows[i][1]);
                    oAccomRoomData[i].AccomodationId = Convert.ToInt32(ds.Tables[0].Rows[i][2]);
                    oAccomRoomData[i].FloorId = Convert.ToInt32(ds.Tables[0].Rows[i][3]);
                    oAccomRoomData[i].RoomCategoryId = Convert.ToInt32(ds.Tables[0].Rows[i][4]);
                    oAccomRoomData[i].RoomTypeId = Convert.ToInt32(ds.Tables[0].Rows[i][5]);
                    oAccomRoomData[i].No_of_Beds = Convert.ToInt32(ds.Tables[0].Rows[i][6]);
                    oAccomRoomData[i].Description = Convert.ToString(ds.Tables[0].Rows[i][7]);
                    oAccomRoomData[i].TelExtnNo = Convert.ToString(ds.Tables[0].Rows[i][8]);
                    oAccomRoomData[i].Status = Convert.ToBoolean(ds.Tables[0].Rows[i][12]);
                 
                    if (ds.Tables[0].Rows[i].IsNull(9) == false)
                        oAccomRoomData[i].ExtraBeds = Convert.ToInt32(ds.Tables[0].Rows[i][9]);
                    else
                        oAccomRoomData[i].ExtraBeds = 0;
                    if(ds.Tables[0].Rows[i].IsNull(10)==false)
                        oAccomRoomData[i].ExtraBedRate = Convert.ToDouble(ds.Tables[0].Rows[i][10]);

                    if (ds.Tables[0].Rows[i][11] != DBNull.Value)
                        oAccomRoomData[i].Convertable = Convert.ToBoolean(ds.Tables[0].Rows[i][11]);
                }
            }
            return oAccomRoomData;
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
                oDB = null;
                ds = null;
                GF.LogError("clsRoomMaster.GetDataFromDB", exp.Message);
            }
            return ds;
        }

        /*private clsAccomRoomData[] GetAvailableRooms(DateTime dtStartDate, int iAccomID)
        {
            clsAccomRoomData[] oAccomRoomData=null;
            //DataSet ds_L = GetTotalNoOfRoomsAvailable(dtStartDate, iAccomID);            
            //oAccomRoomData = new clsRoomData[ds_L.Tables[0].Rows.Count];
            //for (int i = 0; i <oAccomRoomData.Length ; i++)
            //{
            //    oAccomRoomData[i] = new clsRoomData();
            //    oAccomRoomData[i].RoomNo =Convert.ToString(ds_L.Tables[0].Rows[i][1].ToString()); 
            //}
            return oAccomRoomData;
        }*/

        private RoomTypewiseRooms[] GetRoomsAvailable(DateTime dtStartDate, int iAccomID)
        {
            RoomTypewiseRooms[] oRoomsAvailable = null;            
            oRoomsAvailable = GetTotalNoOfRoomsAvailable(dtStartDate,iAccomID) ;
            return oRoomsAvailable;
        }

        //public RoomsAvailable[] GetTotalNoOfRoomsAvailable(DateTime dtStartDate, int iAccomID)
        //{
        //    string sProcName = "up_GetAvailableRooms";
        //    RoomsAvailable[] oRoomsAvailable = null;
        //    clsDatabaseManager oDB = new clsDatabaseManager();
        //    DataSet ds_L=null;
        //    oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
        //    oDB.DbDatabase.AddInParameter(oDB.DbCmd,"@dtStartDate", DbType.DateTime,dtStartDate);
        //    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomID", DbType.Int32, iAccomID);
        //    ds_L= oDB.ExecuteDataSet(oDB.DbCmd);            
        //    int iCount = Convert.ToInt32(ds_L.Tables[0].Rows.Count);
        //    if (iCount>0)
        //    {
        //        oRoomsAvailable=new RoomsAvailable[iCount];
        //        for (int i = 0; i < iCount; i++)
        //        {
        //            oRoomsAvailable[i] = new RoomsAvailable();
        //            oRoomsAvailable[i].oRoomTypeData = new clsOccupancyTypeData();
        //            oRoomsAvailable[i].oRoomTypeData.OccupancyID = Convert.ToInt32(ds_L.Tables[0].Rows[i][0].ToString());
        //            oRoomsAvailable[i].oRoomTypeData.OccupancyType = Convert.ToString(ds_L.Tables[0].Rows[i][1].ToString());
        //            oRoomsAvailable[i].NoOfRoomsAvailable = Convert.ToInt32(ds_L.Tables[0].Rows[i][2].ToString());
        //        }
        //    }
        //    return oRoomsAvailable;
        //}

        public RoomTypewiseRooms[] GetTotalNoOfRoomsAvailable(DateTime dtStartDate, int iAccomID)
        {
            string sProcName; 
            RoomTypewiseRooms[] oRoomsAvailable = null;
            DatabaseManager oDB = new DatabaseManager();
            DataSet ds_L = null;

            sProcName = "up_GetAvailableRooms";
            oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtStartDate", DbType.DateTime, Convert.ToDateTime(dtStartDate));
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomID", DbType.Int32, iAccomID);
            ds_L = oDB.ExecuteDataSet(oDB.DbCmd);
            int iCount = Convert.ToInt32(ds_L.Tables[0].Rows.Count);
            if (iCount > 0)
            {
                oRoomsAvailable = new RoomTypewiseRooms[iCount];
                for (int i = 0; i < iCount; i++)
                {
                    oRoomsAvailable[i] = new RoomTypewiseRooms();
                    oRoomsAvailable[i].RoomTypeId = Convert.ToInt32(ds_L.Tables[0].Rows[i][0].ToString());
                    oRoomsAvailable[i].RoomType = Convert.ToString(ds_L.Tables[0].Rows[i][1].ToString());
                    oRoomsAvailable[i].NoOfRoomsBooked = Convert.ToInt32(ds_L.Tables[0].Rows[i][2].ToString());
                }
            }
            return oRoomsAvailable;
        }

        public BookedRoomsTotal[] GetTotalRoomsBooked(int iBookingID)
        {
            string sProcName = "up_GetBookedRooms";
            BookedRoomsTotal[] oRoomsBooked = null;
            DatabaseManager oDB = new DatabaseManager();
            DataSet ds_L = null;
            oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iBookingID", DbType.Int32, iBookingID);            
            ds_L = oDB.ExecuteDataSet(oDB.DbCmd);
            int iCount =Convert.ToInt32(ds_L.Tables[0].Rows.Count);
            if (iCount > 0)
            {
                oRoomsBooked = new BookedRoomsTotal[iCount];
                for (int i = 0; i < oRoomsBooked.Length; i++)
                {
                    oRoomsBooked[i] = new BookedRoomsTotal();
                    oRoomsBooked[i].RoomCategory = Convert.ToString(ds_L.Tables[0].Rows[i][0].ToString());
                    oRoomsBooked[i].RoomType = Convert.ToString(ds_L.Tables[0].Rows[i][1].ToString());
                    oRoomsBooked[i].RoomsBooked = Convert.ToInt32(ds_L.Tables[0].Rows[i][2].ToString());                    
                }
            }
            return oRoomsBooked;
        }

        public RoomDTO[] GetAllRooms(int AccomodationId)
        {
            return GetAllRooms(AccomodationId, "");
        }

        public RoomDTO[] GetAllRooms(int AccomodationId, string RoomNo)
        {
            string sProcName = "up_Get_Rooms";
            RoomDTO[] oAccomRoomData = null;
            DatabaseManager oDB = new DatabaseManager();
            DataSet ds = null;
            try
            {
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomId", DbType.Int32, AccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sRoomNo", DbType.String, RoomNo);
                ds = oDB.ExecuteDataSet(oDB.DbCmd);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    //RoomNo, Sequence, AccomId, FloorId, TypeID, Occupancy, No_of_Beds, Description, 
                    //TelExtnNo, ExtraBedAllowed, ExtraBedRate
                    oAccomRoomData = new RoomDTO[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        oAccomRoomData[i] = new RoomDTO();
                        oAccomRoomData[i].RoomNo = Convert.ToString(ds.Tables[0].Rows[i][0]);
                        oAccomRoomData[i].AccomodationId = Convert.ToInt32(ds.Tables[0].Rows[i][1]);
                        oAccomRoomData[i].RoomCategory = Convert.ToString(ds.Tables[0].Rows[i][2]).Trim();
                        oAccomRoomData[i].RoomType = Convert.ToString(ds.Tables[0].Rows[i][3]).Trim();
                        oAccomRoomData[i].ExtraBeds = Convert.ToInt32(ds.Tables[0].Rows[i][5]);
                        oAccomRoomData[i].No_of_Beds = Convert.ToInt32(ds.Tables[0].Rows[i][4]);
                        oAccomRoomData[i].Status = Convert.ToBoolean(ds.Tables[0].Rows[i][6]);
                        oAccomRoomData[i].activestatus = Convert.ToString(ds.Tables[0].Rows[i][7]);
                    }
                }
            }
            catch (Exception exp)
            {
                GF.LogError("clsRoomMaster.GetAllRooms", exp.Message);
            }
            return oAccomRoomData;
           
        }

        public DataSet checkifroombooked(roommaintainDTO rmdto)
        {
            DatabaseManager oDB = new DatabaseManager();
            DataSet ds = null;
            try
            {
                string sProcName = "sp_CheckifRoomBooked";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomId", DbType.Int32, rmdto.AccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@RoomNo", DbType.String, rmdto.roomId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@FromDt", DbType.Date, rmdto.StartDate);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ToDt", DbType.Date, rmdto.EndDate);
                ds = oDB.ExecuteDataSet(oDB.DbCmd);
                //DataSet ds = oDB.FetchRecords("tblBookingDetails", Query);
                
            }
            catch (Exception exp)
            {
                oDB = null;
                ds = null;
                //GF.LogError("clsRoomMaster.GetDataFromDB", exp.Message);
            }
            return ds;
        }



        public bool InsertmaintenanceDates(roommaintainDTO maintaindate)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                sProcName = "[dbo].[sp_roomMaintenacedates]";
                oDB = new DatabaseManager();
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomId", DbType.Int32, maintaindate.AccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@RoomNo", DbType.String, maintaindate.roomId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@reason", DbType.String, maintaindate.Reason);
                if (maintaindate.StartDate == DateTime.MinValue)
                    maintaindate.StartDate = GF.ParseDate("1900-01-01");
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@FromDt", DbType.DateTime, maintaindate.StartDate);

                if (maintaindate.EndDate == DateTime.MinValue)
                    maintaindate.EndDate = GF.ParseDate("9999-12-31");
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ToDt", DbType.DateTime, maintaindate.EndDate);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
               // GF.LogError("clsAccomodationMaster.InsertAccomodationSeason", exp.Message.ToString());
                oDB = null;
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }


        public List<roommaintainDTO> GetroommaintainDates(int AccomodationId,string roomid)
        {
            DataSet ds;
            List<roommaintainDTO> mainataimdateslist = new List<roommaintainDTO>();
            roommaintainDTO mainataimdates = null;
            ds = null;
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "[dbo].[get_roommaintenancedates]";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomId", DbType.Int32, AccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@RoomNo", DbType.String, roomid);
                ds = oDB.ExecuteDataSet(oDB.DbCmd);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        mainataimdates = new roommaintainDTO();
                        mainataimdates.AccomodationId = Convert.ToInt32(ds.Tables[0].Rows[i][1]);
                        mainataimdates.roomId = Convert.ToString(ds.Tables[0].Rows[i][2]).Trim();
                        mainataimdates.StartDate = ds.Tables[0].Rows[i][3] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[i][3].ToString()) : DateTime.MinValue;
                        mainataimdates.EndDate = ds.Tables[0].Rows[i][4] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[i][4].ToString()) : DateTime.MinValue;
                        mainataimdates.Reason = Convert.ToString(ds.Tables[0].Rows[i][5]).Trim();
                        mainataimdates.AccomName = Convert.ToString(ds.Tables[0].Rows[i][6]).Trim();
                        mainataimdateslist.Add(mainataimdates);
                    }
                }
            }
            catch (Exception exp)
            {
               // GF.LogError("clsAccomodationMaster.Update", exp.Message.ToString());
                oDB = null;
            }
            finally
            {
                oDB = null;
            }
            return mainataimdateslist;
        }




        public bool UpdateroomMaintaindates(roommaintainDTO Maintainold, roommaintainDTO maintainnew)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                sProcName = "[update_RoommaintainDates]";
                oDB = new DatabaseManager();
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomId", DbType.Int32, Maintainold.AccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@RoomNo", DbType.String, Maintainold.roomId);

                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@reason", DbType.String, maintainnew.Reason);
                if (Maintainold.StartDate == DateTime.MinValue)
                    Maintainold.StartDate = GF.ParseDate("1900-01-01");
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@oldFromdt", DbType.DateTime, Maintainold.StartDate);

                if (Maintainold.EndDate == DateTime.MinValue)
                    Maintainold.EndDate = GF.ParseDate("1900-01-01");
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@oldtodt", DbType.DateTime, Maintainold.EndDate);

                if (maintainnew.StartDate == DateTime.MinValue)
                    maintainnew.StartDate = GF.ParseDate("1900-01-01");
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@FromDt", DbType.DateTime, maintainnew.StartDate);

                if (maintainnew.EndDate == DateTime.MinValue)
                    maintainnew.EndDate = GF.ParseDate("9999-12-31");
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ToDt", DbType.DateTime, maintainnew.EndDate);

                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                //GF.LogError("clsAccomodationMaster.up_Upd_AccomodationMasterSeasonDate", exp.Message.ToString());
                oDB = null;
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }



        public bool DeletemaintainDates(roommaintainDTO rmdto)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "[Delete_RoommaintainDates]";
                oDB = new DatabaseManager();
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomId", DbType.Int32, rmdto.AccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@RoomNo", DbType.Int32, rmdto.roomId);
                if (rmdto.StartDate == DateTime.MinValue)
                    rmdto.StartDate = GF.ParseDate("1900-01-01");
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@FromDt", DbType.DateTime, rmdto.StartDate);

                if (rmdto.EndDate == DateTime.MinValue)
                    rmdto.EndDate = GF.ParseDate("1900-01-01");
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ToDt", DbType.DateTime, rmdto.EndDate);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
               // GF.LogError("clsAccomodationMaster.Delete", exp.Message.ToString());
                oDB = null;
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }



        #endregion
    }
    
}



