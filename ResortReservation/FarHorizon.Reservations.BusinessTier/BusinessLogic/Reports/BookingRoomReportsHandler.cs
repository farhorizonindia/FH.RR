using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.DataBaseManager;
using System;
using System.Data;

namespace FarHorizon.Reservations.BusinessTier.BusinessLogic.Reports
{
    internal class BookingRoomReportsHandler
    {
        public BookingRoomReportsDTO[] GetDetailedBookingDetails(int BookingId)
        {
            BookingRoomReportsDTO[] oBRD = null;
            DatabaseManager oDB = null;
            try
            {
                oDB = new DatabaseManager();
                string sProcName = "up_GetDetailedBookingDetails";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@BookingId", DbType.Int32, BookingId);
                DataSet dsBRD = null;
                dsBRD = oDB.ExecuteDataSet(oDB.DbCmd);
                oDB = null;

                if (dsBRD != null)
                {
                    oBRD = new BookingRoomReportsDTO[dsBRD.Tables[0].Rows.Count];
                    for (int i = 0; i < oBRD.Length; i++)
                    {
                        oBRD[i] = new BookingRoomReportsDTO();
                        oBRD[i].RoomCategory = dsBRD.Tables[0].Rows[i][0].ToString();
                        oBRD[i].RoomType = dsBRD.Tables[0].Rows[i][1].ToString();
                        oBRD[i].TotalBooked = Convert.ToInt32(dsBRD.Tables[0].Rows[i][2].ToString());
                        oBRD[i].TotalWaitlisted = Convert.ToInt32(dsBRD.Tables[0].Rows[i][3].ToString());
                    }
                    dsBRD = null;
                }
            }
            catch (Exception exp)
            {
                oDB = null;
                GF.LogError("clsBookingRoomReportsHandler.GetDetailedBookingDetails", exp.Message);
                return null;
            }
            finally
            {
                oDB = null;
            }
            return oBRD;
        }


        public BookingRoomReportsDTO[] GetroomBookingDetailsmail(int BookingId)
        {
            BookingRoomReportsDTO[] oBRD = null;
            DatabaseManager oDB = null;
            try
            {
                oDB = new DatabaseManager();
                string sProcName = "RoomBookingDetailsformail";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@bookingid", DbType.Int32, BookingId);
                DataSet dsBRD = null;
                dsBRD = oDB.ExecuteDataSet(oDB.DbCmd);
                oDB = null;

                if (dsBRD != null)
                {
                    oBRD = new BookingRoomReportsDTO[dsBRD.Tables[0].Rows.Count];
                    for (int i = 0; i < oBRD.Length; i++)
                    {
                        oBRD[i] = new BookingRoomReportsDTO();
                        oBRD[i].RoomCategory = dsBRD.Tables[0].Rows[i][0].ToString();
                        oBRD[i].RoomType = dsBRD.Tables[0].Rows[i][1].ToString();
                        oBRD[i].TotalBooked = Convert.ToInt32(dsBRD.Tables[0].Rows[i][2].ToString());
                        oBRD[i].TotalWaitlisted = Convert.ToInt32(dsBRD.Tables[0].Rows[i][3].ToString());
                        oBRD[i].Proposed = Convert.ToInt32(dsBRD.Tables[0].Rows[i][4].ToString());
                    }
                    dsBRD = null;
                }
            }
            catch (Exception exp)
            {
                oDB = null;
                //  GF.LogError("clsBookingRoomReportsHandler.GetDetailedBookingDetails", exp.Message);
                return null;
            }
            finally
            {
                oDB = null;
            }
            return oBRD;
        }

        public BookingDTMail[] GetBookingDetailsmail(int bookingid)
        {
            BookingDTMail[] oBRD = null;
            DatabaseManager oDB = null;
            try
            {
                oDB = new DatabaseManager();
                string sProcName = "BookingDetailsformail";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@bookingid", DbType.Int32, bookingid);
                DataSet dsBRD = null;
                dsBRD = oDB.ExecuteDataSet(oDB.DbCmd);
                oDB = null;

                if (dsBRD != null)
                {
                    oBRD = new BookingDTMail[dsBRD.Tables[0].Rows.Count];
                    for (int i = 0; i < oBRD.Length; i++)
                    {
                        oBRD[i] = new BookingDTMail();
                        oBRD[i].Bookingcode = dsBRD.Tables[0].Rows[i][0].ToString();
                        oBRD[i].AgentName = dsBRD.Tables[0].Rows[i][1].ToString();
                        oBRD[i].Accomodation = Convert.ToString(dsBRD.Tables[0].Rows[i][2].ToString());
                        oBRD[i].checkin = Convert.ToDateTime(dsBRD.Tables[0].Rows[i][3].ToString());
                        oBRD[i].checkout = Convert.ToDateTime(dsBRD.Tables[0].Rows[i][4].ToString());
                        oBRD[i].bookingstatus = Convert.ToString(dsBRD.Tables[0].Rows[i][5].ToString());
                        oBRD[i].pax = Convert.ToInt32(dsBRD.Tables[0].Rows[i][6].ToString());
                        oBRD[i].Nights = Convert.ToInt32(dsBRD.Tables[0].Rows[i][7].ToString());
                        oBRD[i].Bookingref = Convert.ToString(dsBRD.Tables[0].Rows[i][8].ToString());
                        oBRD[i].chartered = Convert.ToBoolean((dsBRD.Tables[0].Rows[i][9] != null && dsBRD.Tables[0].Rows[i][9].ToString() !="")? dsBRD.Tables[0].Rows[i][9] : false);
                    }
                    dsBRD = null;
                }
            }
            catch (Exception exp)
            {
                oDB = null;
                //    GF.LogError("clsBookingRoomReportsHandler.GetDetailedBookingDetails", exp.Message);
                return null;
            }
            finally
            {
                oDB = null;
            }
            return oBRD;
        }





        public BookingRoomReportsDTO[] GetBookingWithinCurrentBookingDates(int BookingId)
        {
            BookingRoomReportsDTO[] oBRD = null;
            DatabaseManager oDB = null;
            try
            {
                oDB = new DatabaseManager();
                string sProcName = "up_GetBookingsWithinDates";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iBookingID", DbType.Int32, BookingId);
                DataSet dsBRD = null;
                dsBRD = oDB.ExecuteDataSet(oDB.DbCmd);
                oDB = null;

                if (dsBRD != null)
                {
                    oBRD = new BookingRoomReportsDTO[dsBRD.Tables[0].Rows.Count];
                    for (int i = 0; i < oBRD.Length; i++)
                    {
                        oBRD[i] = new BookingRoomReportsDTO();
                        oBRD[i].BookingId = Convert.ToInt32(dsBRD.Tables[0].Rows[i][0].ToString());
                        oBRD[i].BookingRef = Convert.ToString(dsBRD.Tables[0].Rows[i][1].ToString());
                    }
                    dsBRD = null;
                }
            }
            catch (Exception exp)
            {
                GF.LogError("clsBookingRoomReportsHandler.GetDetailedBookingDetails", exp.Message);
                return null;
            }
            finally
            {
                oDB = null;
            }
            return oBRD;
        }
        public BookingRoomReportsDTO[] GetOtherBookingsOfThisRoom(string RoomNo, DateTime StartDate, DateTime EndDate)
        {
            BookingRoomReportsDTO[] oBRD = null;
            DatabaseManager oDB = null;
            try
            {
                oDB = new DatabaseManager();
                string sProcName = "up_GetOtherBookingsOfThisRoom";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sRoomNo", DbType.String, RoomNo);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtSD", DbType.DateTime, Convert.ToDateTime(StartDate.ToString()));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtED", DbType.DateTime, Convert.ToDateTime(EndDate.ToString()));
                DataSet dsBRD = null;
                dsBRD = oDB.ExecuteDataSet(oDB.DbCmd);
                oDB = null;

                if (dsBRD != null)
                {
                    oBRD = new BookingRoomReportsDTO[dsBRD.Tables[0].Rows.Count];
                    for (int i = 0; i < oBRD.Length; i++)
                    {
                        oBRD[i] = new BookingRoomReportsDTO();
                        oBRD[i].BookingId = Convert.ToInt32(dsBRD.Tables[0].Rows[i][0].ToString());
                        oBRD[i].BookingRef = Convert.ToString(dsBRD.Tables[0].Rows[i][1].ToString());
                    }
                    dsBRD = null;
                }
            }
            catch (Exception exp)
            {
                GF.LogError("clsBookingRoomReportsHandler.GetOtherBookingsOfThisRoom", exp.Message);
                return null;
            }
            finally
            {
                oDB = null;
            }
            return oBRD;
        }

    }
}
