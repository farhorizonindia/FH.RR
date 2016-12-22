using System;
using System.Collections.Generic;
using System.Text;
using System.Data;  
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.DataBaseManager;
using FarHorizon.Reservations.BusinessTier.BusinessLogic;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Client;

namespace FarHorizon.Reservations.BusinessTier.BusinessLogic.BookingEngine
{
    internal class BookingConfirmationHandler
    {
        DatabaseManager oDB;

        #region IBookingManager Members

        public bool ConfirmBooking(BookingDTO objBooking)
        {
            try
            {
                if (oDB == null)
                    oDB = new DatabaseManager();
                string sProcName = "up_Booking_Confirmation";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iBookingID", DbType.Int32, objBooking.BookingId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sExOrderNo", DbType.String, objBooking.ExchangeOrderNo);

                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtVoucherDate", DbType.DateTime, GF.Handle19000101(objBooking.VoucherDate,false));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtArrivalDT", DbType.DateTime, GF.HandleMaxMinDates(objBooking.ArrivalDateTime,true));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sArrivalTransport", DbType.String, objBooking.ArrivalTransport);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iArrivalCityId", DbType.Int32, objBooking.ArrivalCityId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtDepartureDT", DbType.DateTime, GF.HandleMaxMinDates(objBooking.DepartureDateTime,true));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sDepartureTransport", DbType.String, objBooking.DepartureTransport);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iDepartureCityId", DbType.Int32, objBooking.DepartureCityId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sArrivalVehicleNo", DbType.String, objBooking.ArrivalVehicleNo);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sArrivalTransportCompany", DbType.String, objBooking.ArrivalTransportCompany);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sDepartureVehicleNo", DbType.String, objBooking.DepartureVehicleNo);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sDepartureTransportCompany", DbType.String, objBooking.DepartureTransportCompany);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sTourID", DbType.String, objBooking.TourId);

                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ArrivalVehicleNameType", DbType.String, objBooking.ArrivalVehicleNameType);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ArrivalTransportCompanyPhoneNo", DbType.String, objBooking.ArrivalTransportCompanyPhoneNo);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ArrivalDriverPhoneNo", DbType.String, objBooking.ArrivalDriverPhoneNo);

                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@DepartureVehicleNameType", DbType.String, objBooking.DepartureVehicleNameType);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@DepartureTransportCompanyPhoneNo", DbType.String, objBooking.DepartureTransportCompanyPhoneNo);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@DepartureDriverPhoneNo", DbType.String, objBooking.DepartureDriverPhoneNo);

                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@arrivalTransportId", DbType.String, objBooking.ArrivalTransportId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@departureTransportId", DbType.String, objBooking.DepartureTransportId);


                oDB.ExecuteNonQuery(oDB.DbCmd);                
            }
            catch (Exception exp)
            {
                oDB = null;
                objBooking = null;
                GF.LogError("clsBookingConfirmationHandler.ConfirmBooking", exp.Message);
            }
            return true;
        }

        public bool CancelBookingConfirmation(int BookingId)
        {
            BookingMealPlanHandler oBMPH;
            string sProcName;
            try
            {
                if (oDB == null)
                    oDB = new DatabaseManager();                
                oBMPH = new BookingMealPlanHandler();
                oBMPH.DeleteBookingMealPlan(BookingId);

                sProcName = "up_Booking_Confirmation_Cancel";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iBookingID", DbType.Int32, BookingId);                
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                GF.LogError("clsBookingConfirmationHandler.CancelBookingConfirmation", exp.Message);
                return false;
            }
            return true;
        }

        private BookingDTO[] GetBookingConfirmationDetails(string WhereClause)
        {
            DataSet dsBookingConfirmationData;
            DataRow dr;
            BookingDTO[] oBookingData;
            string sProcName;
            DatabaseManager oDB;

            dsBookingConfirmationData = null;
            oBookingData = null;

            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Get_BookingConfirmationDetails";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sWhereClause", DbType.String, WhereClause); //Max. 2000
                dsBookingConfirmationData = oDB.ExecuteDataSet(oDB.DbCmd);

                if (dsBookingConfirmationData != null)
                {
                    //oBookingData[dsBookingData.Tables[0].Rows.Count] = new clsBookingData();
                    oBookingData = new BookingDTO[dsBookingConfirmationData.Tables[0].Rows.Count];
                    for (int i = 0; i < dsBookingConfirmationData.Tables[0].Rows.Count; i++)
                    {
                        oBookingData[i] = new BookingDTO();
                        dr = dsBookingConfirmationData.Tables[0].Rows[i];
                        if (dr.ItemArray.GetValue(0) != DBNull.Value)
                            oBookingData[i].BookingId = Convert.ToInt32(dr.ItemArray.GetValue(0));
                        if (dr.ItemArray.GetValue(2) != DBNull.Value)
                            oBookingData[i].ExchangeOrderNo = Convert.ToString(dr.ItemArray.GetValue(2));
                        if (dr.ItemArray.GetValue(3) != DBNull.Value)
                            oBookingData[i].VoucherDate = Convert.ToDateTime(dr.ItemArray.GetValue(3).ToString());
                        if (dr.ItemArray.GetValue(4) != DBNull.Value)
                            oBookingData[i].ArrivalDateTime = Convert.ToDateTime(dr.ItemArray.GetValue(4));
                        if (dr.ItemArray.GetValue(5) != DBNull.Value)
                            oBookingData[i].ArrivalTransport = Convert.ToString(dr.ItemArray.GetValue(5));
                        if (dr.ItemArray.GetValue(6) != DBNull.Value)
                            oBookingData[i].ArrivalCityId = Convert.ToInt32(dr.ItemArray.GetValue(6));
                        if (dr.ItemArray.GetValue(7) != DBNull.Value)
                            oBookingData[i].DepartureDateTime = Convert.ToDateTime(dr.ItemArray.GetValue(7));
                        if (dr.ItemArray.GetValue(8) != DBNull.Value)
                            oBookingData[i].DepartureTransport = Convert.ToString(dr.ItemArray.GetValue(8));
                        if (dr.ItemArray.GetValue(9) != DBNull.Value)
                            oBookingData[i].DepartureCityId = Convert.ToInt32(dr.ItemArray.GetValue(9));
                        if (dr.ItemArray.GetValue(10) != DBNull.Value)
                            oBookingData[i].ArrivalVehicleNo = Convert.ToString(dr.ItemArray.GetValue(10));
                        if (dr.ItemArray.GetValue(11) != DBNull.Value)
                            oBookingData[i].ArrivalTransportCompany = Convert.ToString(dr.ItemArray.GetValue(11));
                        if (dr.ItemArray.GetValue(12) != DBNull.Value)
                            oBookingData[i].DepartureVehicleNo = Convert.ToString(dr.ItemArray.GetValue(12));
                        if (dr.ItemArray.GetValue(13) != DBNull.Value)
                            oBookingData[i].DepartureTransportCompany = Convert.ToString(dr.ItemArray.GetValue(13));
                        if (dr.ItemArray.GetValue(14) != DBNull.Value)
                        oBookingData[i].BookingStatusId = Convert.ToInt32(dr.ItemArray.GetValue(14));
                    }
                }
            }
            catch (Exception exp)
            {
                oDB = null;
                oBookingData = null;
                dsBookingConfirmationData = null;
                GF.LogError("clsBookingConfirmationHandler.GetBookingConfirmationDetails", exp.Message);
            }
            finally
            {
                oDB = null;
                dsBookingConfirmationData = null;
            }            
            return oBookingData;
        }

        public BookingDTO GetBookingConfirmationDetails(int BookingId)
        {
            BookingDTO[] oBookingData;
            string WhereClause = ""; ;
            if (BookingId != 0)
            {
                WhereClause += " and BookingId = " + BookingId;
            }
            oBookingData = GetBookingConfirmationDetails(WhereClause);
            if (oBookingData.Length > 0)
                return oBookingData[0];
            else
            {
                return null;
            }
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
            catch(Exception exp)
            {
                oDB = null;
                GF.LogError("clsBookingConfirmationHandler.GetDataFromDB", exp.Message);
            }
            return ds;
        }

        #endregion        

    }
}
