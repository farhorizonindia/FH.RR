using FarHorizon.DataSecurity;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.DataBaseManager;
using FarHorizon.Reservations.MasterServices;
using System;
using System.Collections.Generic;
using System.Data;

namespace FarHorizon.Reservations.BusinessTier.BusinessLogic.BookingEngine
{
    internal class BookingTouristHandler
    {
        #region ITouristTourDetails Members
        enum Action
        {
            insert,
            update
        }
        DatabaseManager oDB;

        public bool InsertTouristDetails(BookingTouristDTO oBookingTouristDTO, out int TouristNo)
        {
            return SaveData(oBookingTouristDTO, Action.insert, out TouristNo);
        }

        public bool UpdateTouristDetails(BookingTouristDTO oBookingTouristDTO)
        {
            int iTouristNo;
            return SaveData(oBookingTouristDTO, Action.update, out iTouristNo);
        }

        private bool SaveData(BookingTouristDTO oBookingTouristDTO, Action oAction, out int TouristNo)
        {
            int iTouristNo = 0;
            TouristNo = 0;
            try
            {
                oDB = new DatabaseManager();
                string sProcName = "";
                if (oAction == Action.insert)
                    sProcName = "up_Ins_BookingTourist";
                else if (oAction == Action.update)
                    sProcName = "up_Upd_BookingTourist";

                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);

                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@BookingId", DbType.Int32, oBookingTouristDTO.BookingId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@BookingCode", DbType.String, oBookingTouristDTO.BookingCode == null ? string.Empty : oBookingTouristDTO.BookingCode);
                if (oAction == Action.update)
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@TouristNo", DbType.Int32, oBookingTouristDTO.TouristNo);

                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@FirstName", DbType.String, DataSecurityManager.Encrypt(oBookingTouristDTO.FirstName));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@MiddleName", DbType.String, DataSecurityManager.Encrypt(oBookingTouristDTO.MiddleName));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@LastName", DbType.String, DataSecurityManager.Encrypt(oBookingTouristDTO.LastName));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Gender", DbType.String, oBookingTouristDTO.Gender);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@NationalityId", DbType.Int32, oBookingTouristDTO.NationalityId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@PassportNo", DbType.String, DataSecurityManager.Encrypt(oBookingTouristDTO.PassportNo));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@DOB", DbType.DateTime, oBookingTouristDTO.DateOfBirth);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@PlaceofBirth", DbType.String, oBookingTouristDTO.PlaceofBirth);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@PPIssueDate", DbType.DateTime, GF.HandleMaxMinDates(oBookingTouristDTO.PassportIssueDate, false));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@PPExpiryDate", DbType.DateTime, GF.HandleMaxMinDates(oBookingTouristDTO.PassportExpiryDate, false));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@VisaNo", DbType.String, DataSecurityManager.Encrypt(oBookingTouristDTO.VisaNo));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@VisaExpiryDate", DbType.DateTime, GF.HandleMaxMinDates(oBookingTouristDTO.VisaExpiryDate, false));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@IndiaEntryDate", DbType.DateTime, GF.HandleMaxMinDates(oBookingTouristDTO.IndiaEntryDate, false));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ProposedStayInIndia", DbType.String, oBookingTouristDTO.ProposedStayInIndia);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ArrivalDateTime", DbType.DateTime, GF.HandleMaxMinDates(oBookingTouristDTO.ArrivalDateTime, true));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ArrivedFrom", DbType.String, String.Empty);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@VehicleNo", DbType.String, String.Empty);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@TransportCompany", DbType.String, String.Empty);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@TransportMode", DbType.String, String.Empty);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@RoomDetails", DbType.String, oBookingTouristDTO.RoomDetails);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@NextDestination", DbType.String, String.Empty);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@DepartureDateTime", DbType.DateTime, GF.HandleMaxMinDates(DateTime.MinValue, false));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@EmployedinIndia", DbType.Boolean, oBookingTouristDTO.EmployedinIndia);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@VisitPurpose", DbType.String, oBookingTouristDTO.VisitPurpose);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@PermanentAddressInIndia", DbType.String, oBookingTouristDTO.PermanentAddressInIndia);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@MealPlan", DbType.String, String.Empty);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Allergies", DbType.String, oBookingTouristDTO.Allergies);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@MealPref", DbType.String, oBookingTouristDTO.MealPreferences);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@SpecialMessage", DbType.String, oBookingTouristDTO.SpecialMessage);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@VehicleName", DbType.String, String.Empty);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@DriverName", DbType.String, String.Empty);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@DriverPhoneNo", DbType.String, String.Empty);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@TransportPhoneNo", DbType.String, String.Empty);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Suffix", DbType.String, oBookingTouristDTO.Suffix);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@EmailId", DbType.String, oBookingTouristDTO.EmailId);
                iTouristNo = 0;
                if (oAction == Action.insert)
                    iTouristNo = Convert.ToInt32(oDB.ExecuteScalar(oDB.DbCmd));
                else if (oAction == Action.update)
                    oDB.ExecuteNonQuery(oDB.DbCmd);
                TouristNo = iTouristNo;
            }
            catch (Exception exp)
            {
                oDB = null;
                oBookingTouristDTO = null;
                GF.LogError("clsBookingTouristHandler.SaveData", exp.Message);
                return false;
            }
            return true;
        }

        public bool DeleteTourist(int BookingId)
        {
            return DeleteTourist(BookingId, 0);
        }

        public bool DeleteTourist(int BookingId, int TouristNo)
        {
            string sProcName;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Del_BookingTourist";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@BookingId", DbType.Int32, BookingId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@TouristNo", DbType.Int32, TouristNo);
                oDB.ExecuteNonQuery(oDB.DbCmd);
                oDB = null;
            }
            catch (Exception exp)
            {
                oDB = null;
                GF.LogError("clsBookingTouristHandler.DeleteTourist", exp.Message);
                return false;
            }
            return true;
        }

        public BookingTouristDTO[] GetAllTouristDetails(int BookingId)
        {
            return GetTouristsDetails(BookingId, 0);
        }

        public BookingTouristDTO GetTouristDetails(int BookingId, int TouristNo)
        {
            try
            {
                BookingTouristDTO[] oBookingTouristDTO;
                BookingTouristDTO oBTD = null;
                oBookingTouristDTO = GetTouristsDetails(BookingId, TouristNo);
                if (oBookingTouristDTO != null && oBookingTouristDTO.Length > 0)
                    oBTD = oBookingTouristDTO[0];
                return oBTD;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        private BookingTouristDTO[] GetTouristsDetails(int BookingId, int TouristNo)
        {
            DataSet ds;
            DataRow dr;
            BookingTouristDTO[] oBookingTouristDTO;
            oBookingTouristDTO = null;

            string sProcName;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Get_TouristDetails";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);

                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@BookingId", DbType.Int32, BookingId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@TouristNo", DbType.Int32, TouristNo);

                ds = oDB.ExecuteDataSet(oDB.DbCmd);
                oDB = null;
            }
            catch (Exception exp)
            {
                oDB = null;
                ds = null;
                GF.LogError("clsBookingTouristhandler.GetTouristDetails", exp.Message);
            }

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    oBookingTouristDTO = new BookingTouristDTO[ds.Tables[0].Rows.Count];
                    #region AssigningValues
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dr = ds.Tables[0].Rows[i];
                        oBookingTouristDTO[i] = new BookingTouristDTO();
                        oBookingTouristDTO[i].BookingId = dr.ItemArray.GetValue(0) != DBNull.Value ? Convert.ToInt32(dr.ItemArray.GetValue(0)) : 0;
                        oBookingTouristDTO[i].BookingCode = dr.ItemArray.GetValue(1) != DBNull.Value ? Convert.ToString(dr.ItemArray.GetValue(1)) : "";
                        oBookingTouristDTO[i].TouristNo = dr.ItemArray.GetValue(2) != DBNull.Value ? Convert.ToInt32(dr.ItemArray.GetValue(2)) : 0;
                        oBookingTouristDTO[i].FirstName = dr.ItemArray.GetValue(3) != DBNull.Value ? DataSecurityManager.Decrypt(Convert.ToString(dr.ItemArray.GetValue(3))) : "";
                        oBookingTouristDTO[i].MiddleName = dr.ItemArray.GetValue(4) != DBNull.Value ? DataSecurityManager.Decrypt(Convert.ToString(dr.ItemArray.GetValue(4))) : "";
                        oBookingTouristDTO[i].LastName = dr.ItemArray.GetValue(5) != DBNull.Value ? DataSecurityManager.Decrypt(Convert.ToString(dr.ItemArray.GetValue(5))) : "";
                        oBookingTouristDTO[i].Gender = dr.ItemArray.GetValue(6) != DBNull.Value ? Convert.ToChar(dr.ItemArray.GetValue(6)) : '\0';
                        oBookingTouristDTO[i].NationalityId = dr.ItemArray.GetValue(7) != DBNull.Value ? Convert.ToInt32(dr.ItemArray.GetValue(7)) : 0;
                        oBookingTouristDTO[i].PassportNo = dr.ItemArray.GetValue(8) != DBNull.Value ? DataSecurityManager.Decrypt(Convert.ToString(dr.ItemArray.GetValue(8))) : "";
                        oBookingTouristDTO[i].DateOfBirth = dr.ItemArray.GetValue(9) != DBNull.Value ? Convert.ToDateTime(dr.ItemArray.GetValue(9).ToString()) : DateTime.MinValue;
                        oBookingTouristDTO[i].PlaceofBirth = dr.ItemArray.GetValue(10) != DBNull.Value ? Convert.ToString(dr.ItemArray.GetValue(10)) : "";
                        oBookingTouristDTO[i].PassportIssueDate = dr.ItemArray.GetValue(11) != DBNull.Value ? Convert.ToDateTime(dr.ItemArray.GetValue(11).ToString()) : DateTime.MinValue;
                        oBookingTouristDTO[i].PassportExpiryDate = dr.ItemArray.GetValue(12) != DBNull.Value ? Convert.ToDateTime(dr.ItemArray.GetValue(12).ToString()) : DateTime.MinValue;
                        oBookingTouristDTO[i].VisaNo = dr.ItemArray.GetValue(13) != DBNull.Value ? DataSecurityManager.Decrypt(Convert.ToString(dr.ItemArray.GetValue(13))) : "";
                        oBookingTouristDTO[i].VisaExpiryDate = dr.ItemArray.GetValue(14) != DBNull.Value ? Convert.ToDateTime(dr.ItemArray.GetValue(14).ToString()) : DateTime.MinValue;
                        oBookingTouristDTO[i].IndiaEntryDate = dr.ItemArray.GetValue(15) != DBNull.Value ? Convert.ToDateTime(dr.ItemArray.GetValue(15).ToString()) : DateTime.MinValue;
                        oBookingTouristDTO[i].ProposedStayInIndia = dr.ItemArray.GetValue(16) != DBNull.Value ? Convert.ToString(dr.ItemArray.GetValue(16)) : "";
                        oBookingTouristDTO[i].ArrivalDateTime = dr.ItemArray.GetValue(17) != DBNull.Value ? Convert.ToDateTime(dr.ItemArray.GetValue(17)) : DateTime.MinValue;
                        //oBookingTouristDTO[i].ArrivedFrom = dr.ItemArray.GetValue(18) != DBNull.Value ? Convert.ToString(dr.ItemArray.GetValue(18)):"";
                        //oBookingTouristDTO[i].VehicleNo = dr.ItemArray.GetValue(19) != DBNull.Value ? Convert.ToString(dr.ItemArray.GetValue(19)):"";
                        //oBookingTouristDTO[i].TransportCompany = dr.ItemArray.GetValue(20) != DBNull.Value ? Convert.ToString(dr.ItemArray.GetValue(20)):"";
                        //oBookingTouristDTO[i].TransportMode = dr.ItemArray.GetValue(21) != DBNull.Value ? Convert.ToString(dr.ItemArray.GetValue(21)):"";
                        oBookingTouristDTO[i].RoomDetails = dr.ItemArray.GetValue(22) != DBNull.Value ? Convert.ToString(dr.ItemArray.GetValue(22)) : "";
                        //oBookingTouristDTO[i].NextDestination = dr.ItemArray.GetValue(23) != DBNull.Value ? Convert.ToString(dr.ItemArray.GetValue(23)):"";
                        //oBookingTouristDTO[i].DepartureDateTime = dr.ItemArray.GetValue(24) != DBNull.Value ? Convert.ToDateTime(dr.ItemArray.GetValue(24)):DateTime.MinValue;
                        oBookingTouristDTO[i].EmployedinIndia = dr.ItemArray.GetValue(25) != DBNull.Value ? Convert.ToBoolean(dr.ItemArray.GetValue(25)) : false;
                        oBookingTouristDTO[i].VisitPurpose = dr.ItemArray.GetValue(26) != DBNull.Value ? Convert.ToString(dr.ItemArray.GetValue(26)) : "";
                        oBookingTouristDTO[i].PermanentAddressInIndia = dr.ItemArray.GetValue(27) != DBNull.Value ? Convert.ToString(dr.ItemArray.GetValue(27)) : "";
                        //oBookingTouristDTO[i].MealPlan = dr.ItemArray.GetValue(28) != DBNull.Value ? Convert.ToString(dr.ItemArray.GetValue(28)):"";
                        oBookingTouristDTO[i].Allergies = dr.ItemArray.GetValue(29) != DBNull.Value ? Convert.ToString(dr.ItemArray.GetValue(29)) : "";
                        oBookingTouristDTO[i].MealPreferences = dr.ItemArray.GetValue(30) != DBNull.Value ? Convert.ToString(dr.ItemArray.GetValue(30)) : "";
                        oBookingTouristDTO[i].SpecialMessage = dr.ItemArray.GetValue(31) != DBNull.Value ? Convert.ToString(dr.ItemArray.GetValue(31)) : "";
                        //oBookingTouristDTO[i].VehicleName = dr.ItemArray.GetValue(32) != DBNull.Value ? Convert.ToString(dr.ItemArray.GetValue(32)) : "";
                        //oBookingTouristDTO[i].DriverName = dr.ItemArray.GetValue(33) != DBNull.Value ? Convert.ToString(dr.ItemArray.GetValue(33)) : "";
                        //oBookingTouristDTO[i].DriverPhoneNo = dr.ItemArray.GetValue(34) != DBNull.Value ? Convert.ToString(dr.ItemArray.GetValue(34)) : "";
                        //oBookingTouristDTO[i].TransportPhoneNo = dr.ItemArray.GetValue(35) != DBNull.Value ? Convert.ToString(dr.ItemArray.GetValue(35)) : "";
                        oBookingTouristDTO[i].Suffix = dr.ItemArray.GetValue(36) != DBNull.Value ? Convert.ToString(dr.ItemArray.GetValue(36)) : "";
                        oBookingTouristDTO[i].Nationality = GetNationalityName(oBookingTouristDTO[i].NationalityId);
                        oBookingTouristDTO[i].CFormNo = dr.ItemArray.GetValue(37) != DBNull.Value ? Convert.ToInt32(dr.ItemArray.GetValue(37)) : 0;
                        oBookingTouristDTO[i].EmailId = dr.ItemArray.GetValue(38) != DBNull.Value ? Convert.ToString(dr.ItemArray.GetValue(38)) : "";
                    }
                    #endregion AssigningValues
                }
            }
            return oBookingTouristDTO;
        }

        private string GetNationalityName(int nationalityId)
        {
            NationalityMaster nationalityMaster = new NationalityMaster();
            return nationalityMaster.GetNationalityName(nationalityId);
        }

        public BookingTouristDTO[] GetTourists(int BookingId)
        {
            DataSet ds;
            DataRow dr;
            BookingTouristDTO[] oBookingTouristDTO;

            string sProcName;
            oBookingTouristDTO = null;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Get_Tourists";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);

                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@BookingId", DbType.Int32, BookingId);
                ds = oDB.ExecuteDataSet(oDB.DbCmd);
                oDB = null;
            }
            catch (Exception exp)
            {
                oDB = null;
                ds = null;
                GF.LogError("clsBookingTouristHandler.GetTourist", exp.Message);
            }

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    oBookingTouristDTO = new BookingTouristDTO[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dr = ds.Tables[0].Rows[i];
                        oBookingTouristDTO[i] = new BookingTouristDTO();
                        if (dr.ItemArray.GetValue(0) != DBNull.Value)
                            oBookingTouristDTO[i].BookingId = Convert.ToInt32(dr.ItemArray.GetValue(0));
                        if (dr.ItemArray.GetValue(1) != DBNull.Value)
                            oBookingTouristDTO[i].BookingCode = Convert.ToString(dr.ItemArray.GetValue(1));
                        if (dr.ItemArray.GetValue(2) != DBNull.Value)
                            oBookingTouristDTO[i].TouristNo = Convert.ToInt32(dr.ItemArray.GetValue(2));
                        if (dr.ItemArray.GetValue(3) != DBNull.Value)
                            oBookingTouristDTO[i].FirstName = DataSecurityManager.Decrypt(Convert.ToString(dr.ItemArray.GetValue(3)));
                        if (dr.ItemArray.GetValue(4) != DBNull.Value)
                            oBookingTouristDTO[i].MiddleName = DataSecurityManager.Decrypt(Convert.ToString(dr.ItemArray.GetValue(4)));
                        if (dr.ItemArray.GetValue(5) != DBNull.Value)
                            oBookingTouristDTO[i].LastName = DataSecurityManager.Decrypt(Convert.ToString(dr.ItemArray.GetValue(5)));
                    }
                }
            }
            return oBookingTouristDTO;
        }


        public BookingTouristDTO[] GetTouristsftr(string bcode, string email, string ppno, string firstName, DateTime chkin, DateTime chkout, int accomid)
        {
            DataSet ds;
            DataRow dr;
            BookingTouristDTO[] oBookingTouristDTO;

            string sProcName;
            oBookingTouristDTO = null;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Get_Touristsftr";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);

                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@BookingCode", DbType.String, bcode);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@name", DbType.String, DataSecurityManager.Encrypt(firstName));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@EmailId", DbType.String, DataSecurityManager.Encrypt(email));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@passportNo", DbType.String, DataSecurityManager.Encrypt(ppno));

                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@chkindate", DbType.Date, chkin);

                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@chkoutdate", DbType.Date, chkout);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@accomid", DbType.Int32, accomid);
                ds = oDB.ExecuteDataSet(oDB.DbCmd);
                oDB = null;
            }
            catch (Exception exp)
            {
                oDB = null;
                ds = null;
                //    GF.LogError("clsBookingTouristHandler.GetTourist", exp.Message);
            }

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    oBookingTouristDTO = new BookingTouristDTO[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dr = ds.Tables[0].Rows[i];
                        oBookingTouristDTO[i] = new BookingTouristDTO();
                        if (dr.ItemArray.GetValue(0) != DBNull.Value)
                            oBookingTouristDTO[i].BookingCode = Convert.ToString(dr.ItemArray.GetValue(0));
                        if (dr.ItemArray.GetValue(1) != DBNull.Value)
                            oBookingTouristDTO[i].BookingRef = Convert.ToString(dr.ItemArray.GetValue(1));
                        if (dr.ItemArray.GetValue(2) != DBNull.Value)
                            oBookingTouristDTO[i].AgentName = dr.ItemArray.GetValue(2) != DBNull.Value ? DataSecurityManager.Decrypt(Convert.ToString(dr.ItemArray.GetValue(2))) : string.Empty;
                        //if (dr.ItemArray.GetValue(3) != DBNull.Value)
                        //    oBookingTouristDTO[i].ClientName = Convert.ToString(dr.ItemArray.GetValue(3));
                        if (dr.ItemArray.GetValue(4) != DBNull.Value)
                            oBookingTouristDTO[i].Gender = Convert.ToChar(dr.ItemArray.GetValue(4));
                        if (dr.ItemArray.GetValue(5) != DBNull.Value)
                            oBookingTouristDTO[i].DateOfBirth = Convert.ToDateTime(dr.ItemArray.GetValue(5));
                        if (dr.ItemArray.GetValue(6) != DBNull.Value)
                            oBookingTouristDTO[i].Nationality = Convert.ToString(dr.ItemArray.GetValue(6));
                        if (dr.ItemArray.GetValue(7) != DBNull.Value)
                            oBookingTouristDTO[i].PassportNo = Convert.ToString(dr.ItemArray.GetValue(7));
                        if (dr.ItemArray.GetValue(8) != DBNull.Value)
                            oBookingTouristDTO[i].AccomName = Convert.ToString(dr.ItemArray.GetValue(8));
                        if (dr.ItemArray.GetValue(9) != DBNull.Value)
                            oBookingTouristDTO[i].CheckinDate = Convert.ToDateTime(dr.ItemArray.GetValue(9));
                        if (dr.ItemArray.GetValue(10) != DBNull.Value)
                            oBookingTouristDTO[i].CheckoutDate = Convert.ToDateTime(dr.ItemArray.GetValue(10));
                        if (dr.ItemArray.GetValue(11) != DBNull.Value)
                            oBookingTouristDTO[i].EmailId = Convert.ToString(dr.ItemArray.GetValue(11));

                        if (dr.ItemArray.GetValue(12) != DBNull.Value)
                            oBookingTouristDTO[i].BookingId = Convert.ToInt32(dr.ItemArray.GetValue(12));
                        if (dr.ItemArray.GetValue(13) != DBNull.Value)
                            oBookingTouristDTO[i].TouristNo = Convert.ToInt32(dr.ItemArray.GetValue(13));

                        if (dr.ItemArray.GetValue(14) != DBNull.Value)
                            oBookingTouristDTO[i].FirstName = DataSecurityManager.Decrypt(Convert.ToString(dr.ItemArray.GetValue(14)));
                        if (dr.ItemArray.GetValue(15) != DBNull.Value)
                            oBookingTouristDTO[i].MiddleName = DataSecurityManager.Decrypt(Convert.ToString(dr.ItemArray.GetValue(15)));
                        if (dr.ItemArray.GetValue(16) != DBNull.Value)
                            oBookingTouristDTO[i].LastName = DataSecurityManager.Decrypt(Convert.ToString(dr.ItemArray.GetValue(16)));
                    }
                }
            }
            return oBookingTouristDTO;
        }

        public BookingTouristDTO[] SearchTourists(string Firstname, string Lastname, string Passportno, int Nationality)
        {
            DataSet ds;
            BookingTouristDTO[] oBookingTouristDTO;
            oBookingTouristDTO = null;

            string sProcName;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_SearchTourists";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);

                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sFirstName", DbType.String, DataSecurityManager.Encrypt(Firstname));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sLastName", DbType.String, DataSecurityManager.Encrypt(Lastname));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sPassportNo", DbType.String, DataSecurityManager.Encrypt(Passportno));
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iNationalityid", DbType.Int32, Nationality);

                ds = oDB.ExecuteDataSet(oDB.DbCmd);
                oDB = null;
            }
            catch (Exception exp)
            {
                oDB = null;
                ds = null;
                GF.LogError("clsBookingTouristhandler.GetTouristDetails", exp.Message);
            }

            oBookingTouristDTO = FillTouristDetails(ds);
            return oBookingTouristDTO;
        }

        public List<clsTouristCountDTO> GetTouristCount(DateTime FromDate, DateTime ToDate, int AccomTypeId, int AccomId)
        {
            List<clsTouristCountDTO> touristCountList;
            clsTouristCountDTO touristCountDto;
            DataRow dr;
            DataSet dsTouristCount;
            string sProcName;
            dsTouristCount = null;
            touristCountDto = null;

            if (FromDate == DateTime.MinValue || FromDate == DateTime.MaxValue)
                FromDate = GF.GetDate();
            if (ToDate == DateTime.MinValue || ToDate == DateTime.MaxValue)
                ToDate = FromDate.AddMonths(1);
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Get_TouristCount";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@FromDate", DbType.Date, FromDate);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ToDate", DbType.Date, ToDate);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomTypeId", DbType.Int32, AccomTypeId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomId", DbType.Int32, AccomId);

                dsTouristCount = oDB.ExecuteDataSet(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                dsTouristCount = null;
                GF.LogError("clsBookingHandler.GetTouristCount", exp.Message);
            }

            touristCountList = new List<clsTouristCountDTO>();
            if (dsTouristCount != null)
            {
                for (int i = 0; i < dsTouristCount.Tables[0].Rows.Count; i++)
                {
                    touristCountDto = new clsTouristCountDTO();
                    dr = dsTouristCount.Tables[0].Rows[i];
                    touristCountDto.AccomodationTypeId = Convert.ToInt32(dr.ItemArray.GetValue(0));
                    touristCountDto.AccomodationType = Convert.ToString(dr.ItemArray.GetValue(1));
                    touristCountDto.AccomodationId = Convert.ToInt32(dr.ItemArray.GetValue(2));
                    touristCountDto.AccomodationName = Convert.ToString(dr.ItemArray.GetValue(3));
                    touristCountDto.BookingId = Convert.ToInt32(dr.ItemArray.GetValue(4));
                    touristCountDto.BookingReference = Convert.ToString(dr.ItemArray.GetValue(5).ToString());
                    touristCountDto.BookingStartDate = Convert.ToDateTime(dr.ItemArray.GetValue(6).ToString());
                    touristCountDto.BookingEndDate = Convert.ToDateTime(dr.ItemArray.GetValue(7).ToString());
                    touristCountDto.TotalTourist = Convert.ToInt32(dr.ItemArray.GetValue(8));
                    touristCountList.Add(touristCountDto);
                }
            }
            return touristCountList;
        }

        private BookingTouristDTO[] FillTouristDetails(DataSet ds)
        {
            DataRow dr;
            BookingTouristDTO[] oBookingTouristDTO = null;
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    oBookingTouristDTO = new BookingTouristDTO[ds.Tables[0].Rows.Count];
                    #region AssigningValues
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dr = ds.Tables[0].Rows[i];
                        oBookingTouristDTO[i] = new BookingTouristDTO();
                        if (dr.ItemArray.GetValue(0) != DBNull.Value)
                            oBookingTouristDTO[i].TouristNo = Convert.ToInt32(dr.ItemArray.GetValue(0));
                        if (dr.ItemArray.GetValue(1) != DBNull.Value)
                            oBookingTouristDTO[i].FirstName = DataSecurityManager.Decrypt(Convert.ToString(dr.ItemArray.GetValue(1)));
                        if (dr.ItemArray.GetValue(2) != DBNull.Value)
                            oBookingTouristDTO[i].LastName = DataSecurityManager.Decrypt(Convert.ToString(dr.ItemArray.GetValue(2)));
                        if (dr.ItemArray.GetValue(3) != DBNull.Value)
                            oBookingTouristDTO[i].PassportNo = DataSecurityManager.Decrypt(Convert.ToString(dr.ItemArray.GetValue(3)));
                        if (dr.ItemArray.GetValue(4) != DBNull.Value)
                            oBookingTouristDTO[i].Nationality = Convert.ToString(dr.ItemArray.GetValue(4));

                    }
                    #endregion AssigningValues
                }
            }

            return oBookingTouristDTO;
        }

        private DataSet GetDataFromDB(string Query)
        {
            DatabaseManager oDB = new DatabaseManager();
            DataSet ds = null;
            try
            {
                oDB.DbCmd = oDB.GetSqlStringCommand(Query);
                ds = oDB.ExecuteDataSet(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                ds = null;
                GF.LogError("clsBookingTouristhandler.GetDataFromDB", exp.Message);
            }

            return ds;
        }
        #endregion

    }
}
