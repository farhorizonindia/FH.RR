using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BusinessTier.App_Code.DataEntities;
using DataLayer;

namespace BusinessTier.App_Code
{
    class clsBookingRoomHandler
    {
        public bool AddBookingRooms(clsBookingRoomData[] oBookingRoomData)
        {
            clsDatabaseManager oDB;
            try
            {
                oDB = new clsDatabaseManager();
                string sProcName = "up_Ins_BookingRoom";
                SqlParameter[] p = new SqlParameter[7];                               
                p[0] = new SqlParameter("@iBookingId", SqlDbType.Int);                
                p[1] = new SqlParameter("@sBookingCode", SqlDbType.VarChar, 12);
                p[2] = new SqlParameter("@iAccomId", SqlDbType.Int);
                p[3] = new SqlParameter("@iOccupancyId", SqlDbType.Int);
                p[4] = new SqlParameter("@iTypeId", SqlDbType.Int);
                p[5] = new SqlParameter("@dtStartDate", SqlDbType.DateTime);
                p[6] = new SqlParameter("@dtEndDate", SqlDbType.DateTime);

                for (int i = 0; i < oBookingRoomData.Length; i++)
                {
                    p[0].Value = oBookingRoomData[i].BookingId;
                    p[1].Value = oBookingRoomData[i].BookingCode;
                    p[2].Value = oBookingRoomData[i].AccomodationId;
                    p[3].Value = oBookingRoomData[i].OccupancyData.OccupancyID;
                    p[4].Value = oBookingRoomData[i].RoomTypeData.RoomTypeID;
                    p[5].Value = oBookingRoomData[i].StartDate;
                    p[6].Value = oBookingRoomData[i].EndDate;
                    oDB.ExecuteStoredProcedure(sProcName, p);
                }                
            }
            catch
            {
                throw new Exception("The method or operation is not implemented.");
            }
            return true;
        }

        private clsBookingRoomData[] GetBookingRoomDetails(string WhereClause)
        {
            StringBuilder sQuery;
            DataSet dsBookingRoomData;
            clsBookingRoomData[] oBookingRoomData;
            DataRow dr;
            clsDatabaseManager oDB;

            dsBookingRoomData = null;
            oBookingRoomData = null;
            sQuery = new StringBuilder("");
            sQuery.Append(" select RB.BookingID, RB.BookingCode, RB.AccomId, RB.RoomNo, RB.StartDate, RB.Enddate, ");
            sQuery.Append(" OM.OccupancyId, OM.OccupancyType, RT.RoomTypeId, RT.RoomType from tblRoomMaster RM ");
            sQuery.Append(" join tblRoomType RT on RM.TypeId = RT.RoomTypeId ");
            sQuery.Append(" join tblOccupancyTypeMaster OM on RM.OccupancyId = OM.OccupancyId ");
            sQuery.Append(" join tblBookingRoom RB on RB.AccomId = RM.AccomId and RB.RoomNo = RM.RoomNo");
            sQuery.Append(" where 1=1");
            sQuery.Append(WhereClause);
            
            try
            {            
                oDB = new clsDatabaseManager();
                dsBookingRoomData = oDB.FetchRecords("tblBookingRoom", sQuery.ToString());
                if (dsBookingRoomData != null)
                {
                    oBookingRoomData[dsBookingRoomData.Tables[0].Rows.Count] = new clsBookingRoomData();
                    for (int i = 0; i < dsBookingRoomData.Tables[0].Rows.Count; i++)
                    {
                        dr = dsBookingRoomData.Tables[0].Rows[i];
                        oBookingRoomData[i].BookingId = Convert.ToInt32(dr.ItemArray.GetValue(0));
                        oBookingRoomData[i].BookingCode = Convert.ToString(dr.ItemArray.GetValue(1));
                        oBookingRoomData[i].AccomodationId = Convert.ToInt32(dr.ItemArray.GetValue(2));
                        oBookingRoomData[i].RoomNo = Convert.ToString(dr.ItemArray.GetValue(3));
                        oBookingRoomData[i].StartDate = Convert.ToDateTime(dr.ItemArray.GetValue(4));
                        oBookingRoomData[i].EndDate = Convert.ToDateTime(dr.ItemArray.GetValue(5));
                        oBookingRoomData[i].OccupancyData.OccupancyID = Convert.ToInt32(dr.ItemArray.GetValue(6));
                        oBookingRoomData[i].OccupancyData.OccupancyType = Convert.ToString(dr.ItemArray.GetValue(7));
                        oBookingRoomData[i].RoomTypeData.RoomTypeID = Convert.ToInt32(dr.ItemArray.GetValue(8));
                        oBookingRoomData[i].RoomTypeData.RoomType = Convert.ToString(dr.ItemArray.GetValue(9));
                    }
                }
            }
            catch
            {
                throw new Exception("The method or operation is not implemented.");
            }
            return oBookingRoomData;
        }

        public clsBookingRoomData[] GetBookingRoomDetails(int BookingId)
        {
            string WhereClause = "";
            if (BookingId != 0)
                WhereClause = " and BookingId = " + BookingId;
            return GetBookingRoomDetails(WhereClause);
        }

        public clsBookingRoomData[] GetBookingRoomDetails(int BookingId, int AccomodationId)
        {
            string WhereClause = "";
            if (BookingId != 0)
                WhereClause = " and BookingId = " + BookingId;
            if (AccomodationId != 0)
                WhereClause += " and AccomId = " + AccomodationId;
            return GetBookingRoomDetails(WhereClause);
        }

        public clsBookingRoomData GetBookingRoomDetails(int BookingId, int AccomodationId, string RoomNo)
        {
            clsBookingRoomData[] oBookingRoomData;
            string WhereClause = "";
            if (BookingId != 0)
                WhereClause = " and BookingId = " + BookingId;
            if (AccomodationId != 0)
                WhereClause += " and AccomId = " + AccomodationId;
            if (RoomNo.Trim() != "")
                WhereClause += " and RoomNo = '" + RoomNo + "'";
            oBookingRoomData = GetBookingRoomDetails(WhereClause);
            return oBookingRoomData[0];
        }

        public clsBookingRoomData[] GetBookingRoomDetails(DateTime FromDate, DateTime ToDate)
        {
            string WhereClause="";
            if (FromDate != DateTime.MinValue && ToDate != DateTime.MaxValue)
                WhereClause = " and EndDate between " + FromDate + " and " + ToDate;
            return GetBookingRoomDetails(WhereClause);
        }

        private bool DeleteBookingRooms(string WhereClause)
        {
            string sQuery;
            bool deleted;
            clsDatabaseManager oDB;
            oDB = new clsDatabaseManager();
            
            sQuery = "Delete from tblBookingRoom where 1=1";
            if (WhereClause != string.Empty)
                sQuery += WhereClause;
            deleted = oDB.ExecuteQuery(sQuery);
            return deleted;
        }

        public bool DeleteBookingRooms(int BookingId)
        {            
            return DeleteBookingRooms(BookingId,0);
        }

        public bool DeleteBookingRooms(int BookingId, int AccomodationId)
        {            
            return DeleteBookingRooms(BookingId,AccomodationId,"");
        }

        public bool DeleteBookingRooms(int BookingId, int AccomodationId, string RoomNo)
        {
            string WhereClause = ""; ;
            if(BookingId !=0)
                WhereClause = " and BookingId = " + BookingId;
            if(AccomodationId !=0)
                WhereClause += " and AccomId = " + AccomodationId;
            if(RoomNo !="")
                WhereClause += " and RoomNo = " + RoomNo;
            return DeleteBookingRooms(WhereClause);
        }

        public bool UpdateBooking(clsBookingRoomData[] objBookingRoomData)
        {
            bool Updated;
            DeleteBookingRooms(objBookingRoomData[0].BookingId);
            Updated = AddBookingRooms(objBookingRoomData);
            return Updated;
        }

        private DataSet GetDataFromDB(string Query)
        {
            clsDatabaseManager oDB = new clsDatabaseManager();
            DataSet ds = oDB.FetchRecords("tblBookingDetails", Query);
            return ds;
        }

    }
}
