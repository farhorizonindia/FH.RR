using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BusinessTier.App_Code.Interfaces;
using BusinessTier.App_Code.DataEntities;
using DataLayer;

namespace BusinessTier.App_Code
{
    public class clsBookingTouristHandler:IBookingTouristDetails
    {
        #region ITouristTourDetails Members

        public bool InsertTouristDetails(clsBookingTouristData oBookingTouristData)
        {
            int iTouristNo;
            clsDatabaseManager oDB;
            try
            {
                oDB = new clsDatabaseManager();                    
                string sProcName = "up_Ins_BookingTourist";
                SqlParameter[] p = new SqlParameter[31];
                p[0] = new SqlParameter("@BookingId", SqlDbType.Int, 4);
                p[1] = new SqlParameter("@BookingCode", SqlDbType.VarChar, 12);
                //p[2] = new SqlParameter("@TouristNo", SqlDbType.Int, 4);
                p[2] = new SqlParameter("@FirstName", SqlDbType.VarChar, 50);
                p[3] = new SqlParameter("@MiddleName", SqlDbType.VarChar, 50);
                p[4] = new SqlParameter("@LastName", SqlDbType.VarChar, 50);
                p[5] = new SqlParameter("@Gender", SqlDbType.Char, 1);
                p[6] = new SqlParameter("@Nationality", SqlDbType.VarChar, 20);
                p[7] = new SqlParameter("@PassportNo", SqlDbType.VarChar, 20);
                p[8] = new SqlParameter("@DOB", SqlDbType.DateTime, 8);
                p[9] = new SqlParameter("@PlaceofBirth", SqlDbType.VarChar, 50);
                p[10] = new SqlParameter("@PPIssueDate", SqlDbType.DateTime, 8);
                p[11] = new SqlParameter("@PPExpiryDate", SqlDbType.DateTime, 8);
                p[12] = new SqlParameter("@VisaNo", SqlDbType.VarChar, 20);
                p[13] = new SqlParameter("@VisaExpiryDate", SqlDbType.DateTime, 8);
                p[14] = new SqlParameter("@IndiaEntryDate", SqlDbType.DateTime, 8);
                p[15] = new SqlParameter("@ProposedStayInIndia", SqlDbType.Int, 4);
                p[16] = new SqlParameter("@ArrivalDateTime", SqlDbType.DateTime, 8);
                p[17] = new SqlParameter("@ArrivedFrom", SqlDbType.VarChar, 50);
                p[18] = new SqlParameter("@VehicleNo", SqlDbType.VarChar, 50);
                p[19] = new SqlParameter("@TransportCompany", SqlDbType.VarChar, 100);
                p[20] = new SqlParameter("@TransportMode", SqlDbType.VarChar, 50);
                p[21] = new SqlParameter("@RoomDetails", SqlDbType.VarChar, 30);
                p[22] = new SqlParameter("@NextDestination", SqlDbType.VarChar, 50);
                p[23] = new SqlParameter("@DepartureDateTime", SqlDbType.DateTime, 8);
                p[24] = new SqlParameter("@EmployedinIndia", SqlDbType.Bit, 1);
                p[25] = new SqlParameter("@VisitPurpose", SqlDbType.VarChar, 50);
                p[26] = new SqlParameter("@PermanentAddressInIndia", SqlDbType.VarChar, 150);
                p[27] = new SqlParameter("@MealPlan", SqlDbType.VarChar, 50);
                p[28] = new SqlParameter("@Allergies", SqlDbType.VarChar, 50);
                p[29] = new SqlParameter("@MealPref", SqlDbType.VarChar, 20);
                p[30] = new SqlParameter("@SpecialMessage", SqlDbType.VarChar, 200);

                iTouristNo = oDB.ExecuteStoredProcedure(sProcName, p);
            }
            catch
            {
                throw new Exception("The method or operation is not implemented.");
            }
            //return iTouristNo;
            return true;
        }

        public clsBookingTouristData[] GetTouristDetails(string WhereClause)
        {
            DataSet ds;
            DataRow dr;
            StringBuilder Query;
            clsDatabaseManager oDB;
            clsBookingTouristData[] oBookingTouristData;
            oBookingTouristData = null;
            Query = new StringBuilder("select ");
            
            Query.Append(" BookingId, BookingCode, TouristNo, FirstName, MiddleName, LastName, Gender, ");
            Query.Append(" Nationality, PassportNo, DOB, PlaceofBirth, PPIssueDate, PPExpiryDate, VisaNo, ");
            Query.Append(" VisaExpiryDate, IndiaEntryDate, ProposedStayInIndia, ArrivalDateTime, ArrivedFrom, ");
            Query.Append(" VehicleNo, TransportCompany, TransportMode, RoomDetails, NextDestination, ");
            Query.Append(" DepartureDateTime, EmployedinIndia, VisitPurpose, PermanentAddressInIndia, ");
            Query.Append(" MealPlan, Allergies, MealPref, SpecialMessage");
            Query.Append(" from tblRoomTouristDetails where 1=1");
            if (WhereClause.Trim() != string.Empty)
                Query.Append(WhereClause);
            oDB = new clsDatabaseManager();
            ds = oDB.FetchRecords("tblRoomTouristDetails", Query.ToString());
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    oBookingTouristData = new clsBookingTouristData[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dr = ds.Tables[0].Rows[i];
                        oBookingTouristData[i].BookingId = Convert.ToInt32(dr.ItemArray.GetValue(0));
                        oBookingTouristData[i].BookingCode = Convert.ToString(dr.ItemArray.GetValue(1));
                        oBookingTouristData[i].TouristNo = Convert.ToInt32(dr.ItemArray.GetValue(2));
                        oBookingTouristData[i].FirstName =  Convert.ToString(dr.ItemArray.GetValue(3));
                        oBookingTouristData[i].MiddleName = Convert.ToString(dr.ItemArray.GetValue(4));
                        oBookingTouristData[i].LastName = Convert.ToString(dr.ItemArray.GetValue(5));
                        oBookingTouristData[i].Gender = Convert.ToChar(dr.ItemArray.GetValue(6));
                        oBookingTouristData[i].Nationality = Convert.ToString(dr.ItemArray.GetValue(7));
                        oBookingTouristData[i].PassportNo = Convert.ToString(dr.ItemArray.GetValue(8));
                        oBookingTouristData[i].DateOfBirth = Convert.ToDateTime(dr.ItemArray.GetValue(9));
                        oBookingTouristData[i].PlaceofBirth = Convert.ToString(dr.ItemArray.GetValue(10));
                        oBookingTouristData[i].PassportIssueDate = Convert.ToDateTime(dr.ItemArray.GetValue(11));
                        oBookingTouristData[i].PassportExpiryDate = Convert.ToDateTime(dr.ItemArray.GetValue(12));
                        oBookingTouristData[i].VisaNo = Convert.ToString(dr.ItemArray.GetValue(13));
                        oBookingTouristData[i].VisaExpiryDate = Convert.ToDateTime(dr.ItemArray.GetValue(14));
                        oBookingTouristData[i].IndiaEntryDate = Convert.ToDateTime(dr.ItemArray.GetValue(15));
                        oBookingTouristData[i].ProposedStayInIndia = Convert.ToInt32(dr.ItemArray.GetValue(16));
                        oBookingTouristData[i].ArrivalDateTime =Convert.ToDateTime(dr.ItemArray.GetValue(17));
                        oBookingTouristData[i].ArrivedFrom = Convert.ToString(dr.ItemArray.GetValue(18));
                        oBookingTouristData[i].VehicleNo = Convert.ToString(dr.ItemArray.GetValue(19));
                        oBookingTouristData[i].TransportCompany = Convert.ToString(dr.ItemArray.GetValue(20));
                        oBookingTouristData[i].TransportMode = Convert.ToString(dr.ItemArray.GetValue(21));
                        oBookingTouristData[i].RoomDetails = Convert.ToString(dr.ItemArray.GetValue(22));
                        oBookingTouristData[i].NextDestination = Convert.ToString(dr.ItemArray.GetValue(23));
                        oBookingTouristData[i].DepartureDateTime = Convert.ToDateTime(dr.ItemArray.GetValue(24));
                        oBookingTouristData[i].EmployedinIndia = Convert.ToBoolean(dr.ItemArray.GetValue(25));
                        oBookingTouristData[i].VisitPurpose = Convert.ToString(dr.ItemArray.GetValue(26));
                        oBookingTouristData[i].PermanentAddressInIndia = Convert.ToString(dr.ItemArray.GetValue(26));
                        oBookingTouristData[i].MealPlan = Convert.ToString(dr.ItemArray.GetValue(27));
                        oBookingTouristData[i].Allergies = Convert.ToString(dr.ItemArray.GetValue(28));
                        oBookingTouristData[i].MealPreferences = Convert.ToString(dr.ItemArray.GetValue(29));
                        oBookingTouristData[i].SpecialMessage = Convert.ToString(dr.ItemArray.GetValue(30));
                    }
                }
            }
            return oBookingTouristData;

        }

        public clsBookingTouristData[] GetTouristDetails(int BookingId)
        {
            return GetTouristDetails(BookingId);
        }

        public clsBookingTouristData GetTouristDetails(int BookingId, int TouristNo)
        {
            string sWhereClause;
            clsBookingTouristData[] oBookingTouristData;
            sWhereClause = " and BookingId = " + BookingId;
            if(TouristNo !=0)
                sWhereClause = " and BookingId = " + BookingId + " and TouristNo = " + TouristNo;

            oBookingTouristData = GetTouristDetails(sWhereClause);
            return oBookingTouristData[0];
        }

        public bool DeleteTourist(int BookingId)
        {
            return DeleteTourist(BookingId, 0);
        }

        public bool DeleteTourist(int BookingId, int TouristNo)
        {
            string Query="";
            bool deleted;
            clsDatabaseManager oDB;
            Query = "Delete from tblTouristTourDetails where 1=1";
            if (BookingId != 0)
                Query += " and BookingId = " + BookingId;
            if(TouristNo !=0)
                Query += " and BookingId = " + TouristNo;
            
            oDB = new clsDatabaseManager();
            deleted = oDB.ExecuteQuery(Query);
            return deleted;
        }

        public bool UpdateTouristDetails(clsBookingTouristData oBookingTouristData)
        {
            clsDatabaseManager oDB;
            try
            {
                oDB = new clsDatabaseManager();
                string sProcName = "up_Upd_BookingTourist";
                SqlParameter[] p = new SqlParameter[31];
                p[0] = new SqlParameter("@BookingId", SqlDbType.Int, 4);
                p[1] = new SqlParameter("@BookingCode", SqlDbType.VarChar, 12);
                p[2] = new SqlParameter("@TouristNo", SqlDbType.Int, 4);
                p[3] = new SqlParameter("@FirstName", SqlDbType.VarChar, 50);
                p[4] = new SqlParameter("@MiddleName", SqlDbType.VarChar, 50);
                p[5] = new SqlParameter("@LastName", SqlDbType.VarChar, 50);
                p[6] = new SqlParameter("@Gender", SqlDbType.Char, 1);
                p[7] = new SqlParameter("@Nationality", SqlDbType.VarChar, 20);
                p[8] = new SqlParameter("@PassportNo", SqlDbType.VarChar, 20);
                p[9] = new SqlParameter("@DOB", SqlDbType.DateTime, 8);
                p[10] = new SqlParameter("@PlaceofBirth", SqlDbType.VarChar, 50);
                p[11] = new SqlParameter("@PPIssueDate", SqlDbType.DateTime, 8);
                p[12] = new SqlParameter("@PPExpiryDate", SqlDbType.DateTime, 8);
                p[13] = new SqlParameter("@VisaNo", SqlDbType.VarChar, 20);
                p[14] = new SqlParameter("@VisaExpiryDate", SqlDbType.DateTime, 8);
                p[15] = new SqlParameter("@IndiaEntryDate", SqlDbType.DateTime, 8);
                p[16] = new SqlParameter("@ProposedStayInIndia", SqlDbType.Int, 4);
                p[17] = new SqlParameter("@ArrivalDateTime", SqlDbType.DateTime, 8);
                p[18] = new SqlParameter("@ArrivedFrom", SqlDbType.VarChar, 50);
                p[19] = new SqlParameter("@VehicleNo", SqlDbType.VarChar, 50);
                p[20] = new SqlParameter("@TransportCompany", SqlDbType.VarChar, 100);
                p[21] = new SqlParameter("@TransportMode", SqlDbType.VarChar, 50);
                p[22] = new SqlParameter("@RoomDetails", SqlDbType.VarChar, 30);
                p[23] = new SqlParameter("@NextDestination", SqlDbType.VarChar, 50);
                p[24] = new SqlParameter("@DepartureDateTime", SqlDbType.DateTime, 8);
                p[25] = new SqlParameter("@EmployedinIndia", SqlDbType.Bit, 1);
                p[26] = new SqlParameter("@VisitPurpose", SqlDbType.VarChar, 50);
                p[27] = new SqlParameter("@PermanentAddressInIndia", SqlDbType.VarChar, 150);
                p[28] = new SqlParameter("@MealPlan", SqlDbType.VarChar, 50);
                p[29] = new SqlParameter("@Allergies", SqlDbType.VarChar, 50);
                p[30] = new SqlParameter("@MealPref", SqlDbType.VarChar, 20);
                p[31] = new SqlParameter("@SpecialMessage", SqlDbType.VarChar, 200);

                oDB.ExecuteStoredProcedure(sProcName, p);                
            }
            catch
            {
                throw new Exception("The method or operation is not implemented.");
            }
            return true;
        }

        #endregion
    }
}
