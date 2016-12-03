using System;
using System.Collections.Generic;
using System.Text;
using DataLayer;
using System.Data;
using System.Data.SqlClient;
using BusinessTier.App_Code.Interfaces;
using BusinessTier.App_Code.DataEntities;

namespace BusinessTier.App_Code
{
    public class clsBookingHandler:IBookingManager
    {              
        clsDatabaseManager oDB;        

        #region IBookingManager Members

        public int AddBooking(clsBookingData objBooking)
        {
            int iBookingID;
            try
            {
                if (oDB == null)
                    oDB = new clsDatabaseManager();
                string sProcName = "up_Ins_Booking";                
                SqlParameter[] p = new SqlParameter[9];
                p[0] = new SqlParameter("@sTourID", SqlDbType.VarChar, 100);
                p[0].Value = objBooking.TourId;
                p[1] = new SqlParameter("@dtStartDate", SqlDbType.DateTime);
                p[1].Value = objBooking.ArrivalDate;
                p[2] = new SqlParameter("@dtEndDate", SqlDbType.DateTime);
                p[2].Value = objBooking.DepartureDate;
                p[3] = new SqlParameter("@iAccomTypeId", SqlDbType.Int);
                p[3].Value = objBooking.AccomodationTypeId;
                p[4] = new SqlParameter("@iAccomId", SqlDbType.Int);
                p[4].Value = objBooking.AccomodationId;
                p[5] = new SqlParameter("@iAgentId", SqlDbType.Int);
                p[5].Value = objBooking.AgentId;
                p[6] = new SqlParameter("@iNights", SqlDbType.Int);
                p[6].Value = objBooking.NoOfNights;
                p[7] = new SqlParameter("@iPersons", SqlDbType.Int);
                p[7].Value = objBooking.NoOfPersons;
                p[8] = new SqlParameter("@BookingStatusId", SqlDbType.Int);
                p[8].Value = objBooking.BookingStatus;                

                iBookingID = oDB.ExecuteStoredProcedure(sProcName, p);         
            }
            catch
            {
                throw new Exception("The method or operation is not implemented.");                
            }
            return iBookingID;
        }

        private clsBookingData[] GetBookingDetails(string WhereClause)
        {
            string sQuery = "";
            DataSet dsBookingData;
            clsBookingData[] oBookingData;
            DataRow dr;
            dsBookingData = null;
            oBookingData = null;
            sQuery = " select BookingID, BookingCode, TourID, StartDate, enddate, " +
                    " ATM.AccomType, AM.AccomName, B.AccomTypeId, B.AccomId, NoOFNights, NoOfPersons, AgentId  " +
                    " from tblBooking B join tblAccomTypeMaster ATM on B.AccomTypeId = ATM.AccomTypeId " +
                    " join tblAccomMaster AM on B.AccomId = AM.AccomId where 1=1 ";
            if (WhereClause != "")
                sQuery += WhereClause;
            try
            {
                if (oDB == null)
                    oDB = new clsDatabaseManager();
                dsBookingData = oDB.FetchRecords("BookingData", sQuery);
                if (dsBookingData != null)
                {
                    oBookingData[dsBookingData.Tables[0].Rows.Count] = new clsBookingData();
                    for (int i = 0; i < dsBookingData.Tables[0].Rows.Count; i++)
                    {
                        dr = dsBookingData.Tables[0].Rows[i];
                        oBookingData[i].BookingId = Convert.ToInt32(dr.ItemArray.GetValue(0));
                        oBookingData[i].BookingCode = Convert.ToString(dr.ItemArray.GetValue(1));
                        oBookingData[i].TourId = Convert.ToString(dr.ItemArray.GetValue(2));
                        oBookingData[i].ArrivalDate = Convert.ToDateTime(dr.ItemArray.GetValue(3));
                        oBookingData[i].DepartureDate = Convert.ToDateTime(dr.ItemArray.GetValue(4));
                        oBookingData[i].AccomodationType = Convert.ToString(dr.ItemArray.GetValue(5));
                        oBookingData[i].AccomodationName = Convert.ToString(dr.ItemArray.GetValue(6));
                        oBookingData[i].AccomodationTypeId = Convert.ToInt32(dr.ItemArray.GetValue(7));
                        oBookingData[i].AccomodationId = Convert.ToInt32(dr.ItemArray.GetValue(8));
                        oBookingData[i].NoOfNights = Convert.ToInt32(dr.ItemArray.GetValue(9));
                        oBookingData[i].NoOfPersons = Convert.ToInt32(dr.ItemArray.GetValue(10));
                        oBookingData[i].AgentId = Convert.ToInt32(dr.ItemArray.GetValue(11));

                    }
                }
            }
            catch
            {
                throw new Exception("The method or operation is not implemented.");
            }
            return oBookingData;
        }

        public clsBookingData[] GetBookingDetails(DateTime FromDate,DateTime ToDate)
        {
            string WhereClause = ""; ;
            if (FromDate != DateTime.MinValue && ToDate != DateTime.MaxValue)
            {
                WhereClause += " and BookingDate between " + FromDate + " and " + ToDate;
            }
            return GetBookingDetails(WhereClause);
        }

        public clsBookingData GetBookingDetails(int BookingId)
        {
            clsBookingData[] oBookingData;
            string WhereClause = ""; ;
            if (BookingId !=0)
            {
                WhereClause += " and BookingId = " + BookingId;
            }
            oBookingData = GetBookingDetails(WhereClause);
            return oBookingData[0];
        }

        public bool DeleteBooking(int BookingId)
        {
            clsBookingRoomHandler oBRoomHandler;
            clsBookingTouristHandler oBTouristHandler;
            clsDatabaseManager oDB;
            string Query="";

            oBRoomHandler = new clsBookingRoomHandler();
            oBTouristHandler = new clsBookingTouristHandler();
            oDB = new clsDatabaseManager();
            oBTouristHandler.DeleteTourist(BookingId);
            oBRoomHandler = new clsBookingRoomHandler();
            oBRoomHandler.DeleteBookingRooms(BookingId);
            Query = "delete from tblBooking where BookingId = " + BookingId;
            return oDB.ExecuteQuery(Query);
        }

        public bool UpdateBooking(clsBookingData objBooking)
        {
            try
            {
                if (oDB == null)
                    oDB = new clsDatabaseManager();
                string sProcName = "up_Ins_Booking";
                SqlParameter[] p = new SqlParameter[12];
                p[0] = new SqlParameter("@iBookingID",SqlDbType.Int);
                p[0].Value = objBooking.BookingId;
                p[1] = new SqlParameter("@sBookingCode", SqlDbType.VarChar, 12);
                p[1].Value = objBooking.BookingCode;
                p[2] = new SqlParameter("@sTourID", SqlDbType.VarChar, 100);
                p[2].Value = objBooking.TourId;
                p[3] = new SqlParameter("@dtStartDate", SqlDbType.DateTime);
                p[3].Value = objBooking.ArrivalDate;
                p[4] = new SqlParameter("@dtEndDate", SqlDbType.DateTime);
                p[4].Value = objBooking.DepartureDate;
                p[5] = new SqlParameter("@iAccomTypeId", SqlDbType.Int);
                p[5].Value = objBooking.AccomodationTypeId;
                p[6] = new SqlParameter("@iAccomId", SqlDbType.Int);
                p[6].Value = objBooking.AccomodationId;
                p[7] = new SqlParameter("@iAgentId", SqlDbType.Int);
                p[7].Value = objBooking.AgentId;
                p[8] = new SqlParameter("@iNights", SqlDbType.Int);
                p[8].Value = objBooking.NoOfNights;
                p[9] = new SqlParameter("@iPersons", SqlDbType.Int);
                p[9].Value = objBooking.NoOfPersons;
                p[10] = new SqlParameter("@AgentId", SqlDbType.VarChar, 5);
                p[10].Value = objBooking.AgentId;
                p[11] = new SqlParameter("@BookingStatusId", SqlDbType.Int);
                p[11].Value = objBooking.BookingStatus;

                oDB.ExecuteStoredProcedure(sProcName, p);
            }
            catch
            {
                throw new Exception("The method or operation is not implemented.");
            }
            return true;    
        }

        private DataSet GetDataFromDB(string Query)
        {
            clsDatabaseManager oDB = new clsDatabaseManager();
            DataSet ds = oDB.FetchRecords("tblBookingDetails", Query);
            return ds;
        }

        #endregion        
    }
}

