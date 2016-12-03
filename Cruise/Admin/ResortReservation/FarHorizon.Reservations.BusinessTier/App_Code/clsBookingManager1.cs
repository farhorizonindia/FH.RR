using System;
using System.Collections.Generic;
using System.Text;
using DataLayer;
using System.Data;
using System.Data.SqlClient;
using BusinessTier.App_Code.Interfaces;

namespace BusinessTier.App_Code
{
    public class clsBookingManager:IBookingManager
    {              
        #region IBookingManager Members
        clsDatabaseManager oDB;        
        public DataSet GetBookingData(DateTime StartDate, DateTime EndDate, int RegionId, int AccomodationTypeId, int AccomodationId)
        {
            clsDatabaseManager oDB;
            DataSet ds;
            ds = null;
            oDB = new clsDatabaseManager();
            string Query = "";
            Query = "select RB.BookingNo, RB.AccomId, RB.RoomNo, RB.StartDate, RB.EndDate,  RB.BookingStatusId, " +
                    " AGM.AgentCode, BSM.BookingStatusColor from tblRoomBooking RB " +
                    " left join tblAgentMaster AGM on RB.AgentId = AGM.AgentId " +
                    " join tblBookingStatusMaster BSM on BSM.BookingStatusId = RB.BookingStatusId " +
                    " join tblAccomMaster AM on RB.AccomId = AM.AccomId " +
                    " where 1=1 " +
                    " and StartDate between '" + StartDate.Year + "-" + StartDate.Month + "-" + StartDate.Day + "' and '" + EndDate.Year + "-" + EndDate.Month + "-" + EndDate.Day + "'";
            if (RegionId != 0)
                Query += " and AM.RegionId = " + RegionId;
            if (AccomodationTypeId != 0)
                Query += " and AM.AccomTypeId = " + AccomodationTypeId;
            if (AccomodationId != 0)
                Query += " and AM.AccomId = " + AccomodationId;

            ds = oDB.FetchRecords("BookingData", Query);
            return ds;
        }
        #endregion

        #region IBookingManager Members

        public int BookTour(clsBookingData objBooking)
        {
            int iBookingID;
            try
            {
                if (oDB == null)
                    oDB = new clsDatabaseManager();
                string sProcName = "up_BookTour";
                SqlParameter[] p = new SqlParameter[7];
                p[0] = new SqlParameter("@sGroupID", SqlDbType.VarChar, 50);
                p[0].Value = objBooking.TourId;
                p[1] = new SqlParameter("@dtStartDate", SqlDbType.DateTime);
                p[1].Value = objBooking.ArrivalDate;
                p[2] = new SqlParameter("@dtEndDate", SqlDbType.DateTime);
                p[2].Value = objBooking.DepartureDate;
                p[3] = new SqlParameter("@iAccomType", SqlDbType.Int);
                p[3].Value = objBooking.AccomodationTypeId;
                p[4] = new SqlParameter("@iAccomSubType", SqlDbType.Int);
                p[4].Value = objBooking.AccomodationId;
                p[5] = new SqlParameter("@iNights", SqlDbType.Int);
                p[5].Value = objBooking.NoOfNights;
                p[6] = new SqlParameter("@iPersons", SqlDbType.Int);
                p[6].Value = objBooking.NoOfPersons;
                iBookingID = oDB.ExecuteStoredProcedure(sProcName, p);         
            }
            catch
            {
                throw new Exception("The method or operation is not implemented.");                
            }
            return iBookingID;
        }

        public clsBookingData[] GetBookingDetails(string WhereClause)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public clsBookingData[] GetBookingDetails(DateTime FromDate,DateTime ToDate)
        {
            string sQuery = "";
            DataSet dsBookingData;
            clsBookingData[] oBookingData;
            DataRow dr;
            dsBookingData = null;
            oBookingData = null;
            sQuery =" select BookingID, BookingCode, TourID, StartDate, enddate, " +
                    " ATM.AccomType, AM.AccomName, B.AccomTypeId, B.AccomId, NoOFNights, NoOfPersons  " +
                    " from tblBooking B join tblAccomTypeMaster ATM on B.AccomTypeId = ATM.AccomTypeId " +
                    " join tblAccomMaster AM on B.AccomId = AM.AccomId where 1=1";
            if (FromDate != DateTime.MinValue && ToDate != DateTime.MaxValue)
            {
                sQuery += " and BookingDate between " + FromDate + " and " + ToDate;
            }
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
                    }
                }
            }
            catch
            {
                throw new Exception("The method or operation is not implemented.");
            }
            return oBookingData;
        }

        public clsBookingData GetBookingDetails(int BookingId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public bool DeleteBooking(int BookingId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public bool UpdateBooking(clsBookingData objBooking)
        {
            throw new Exception("The method or operation is not implemented.");
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

