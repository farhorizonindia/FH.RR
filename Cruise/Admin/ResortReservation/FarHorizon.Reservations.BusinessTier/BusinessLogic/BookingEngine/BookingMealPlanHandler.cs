using System;
using System.Collections.Generic;
using System.Text;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.DataBaseManager;
using System.Data;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Client;

namespace FarHorizon.Reservations.BusinessTier.BusinessLogic.BookingEngine
{    
    internal class BookingMealPlanHandler
    {
        DatabaseManager oDB;
        public bool AddBookingMealPlan(BookingMealPlanDTO[] oBookingMealPlanDTO)
        {
            bool bActionCompleted;
            bActionCompleted = SaveData(oBookingMealPlanDTO);
            oBookingMealPlanDTO = null;
            return bActionCompleted;
        }

        public bool UpdateBookingMealPlan(BookingMealPlanDTO[] oBookingMealPlanDTO)
        {
            bool bActionCompleted;
            bActionCompleted = SaveData(oBookingMealPlanDTO);
            oBookingMealPlanDTO = null;
            return bActionCompleted;
        }

        private bool SaveData(BookingMealPlanDTO[] oBookingMealPlanDTO)
        {
            try
            {
                if (oBookingMealPlanDTO != null && oBookingMealPlanDTO.Length > 0)
                {
                    DeleteBookingMealPlan(oBookingMealPlanDTO[0].BookingId);
                    if (oDB == null)
                        oDB = new DatabaseManager();
                    string sProcName = "up_Ins_BookingMealPlan";
                    for (int i = 0; i < oBookingMealPlanDTO.Length; i++)
                    {
                        if (oBookingMealPlanDTO[i] != null)
                        {
                            oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iBookingId", DbType.Int32, oBookingMealPlanDTO[i].BookingId);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iMealPlanId", DbType.Int32, oBookingMealPlanDTO[i].MealPlanId);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@dtMealDate", DbType.DateTime, oBookingMealPlanDTO[i].MealPlanDate);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@bWelcomeDrink", DbType.Boolean, oBookingMealPlanDTO[i].WelcomeDrink);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@bBreakfast", DbType.Boolean, oBookingMealPlanDTO[i].Breakfast);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@bLunch", DbType.Boolean, oBookingMealPlanDTO[i].Lunch);
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@bEveSnacks", DbType.Boolean, oBookingMealPlanDTO[i].EveningSnacks);                            
                            oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@bDinner", DbType.Boolean, oBookingMealPlanDTO[i].Dinner);
                            oDB.ExecuteNonQuery(oDB.DbCmd);
                            oDB.DbCmd.Parameters.Clear();
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                oDB = null;
                oBookingMealPlanDTO = null;
                GF.LogError("clsBookingMealPlanHandler.SaveData", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
                oBookingMealPlanDTO = null;
            }
            return true;
        }

        public bool DeleteBookingMealPlan(int BookingId)
        {
            if (oDB == null)
                oDB = new DatabaseManager();
            string sProcName = "up_Del_BookingMealPlan";
            try
            {
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iBookingId", DbType.Int32, BookingId);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                GF.LogError("clsBookingMealPlanHandler.DeleteBookingMealPlan", exp.Message);
                return false;   
            }            
            return true;
        }

        public BookingMealPlanDTO[] GetBookingMealPlan(int BookingId)
        {            
            return GetBookingMealPlan(BookingId, DateTime.MinValue);
        }

        public BookingMealPlanDTO[] GetBookingMealPlan(int BookingId, DateTime MealPlanDate)
        {
            DataSet dsBookingMealPlanData;
            DataRow dr;
            BookingMealPlanDTO[] oBookingMealPlanDTO;
            string sProcName;
            DatabaseManager oDB;

            dsBookingMealPlanData = null;
            oBookingMealPlanDTO = null;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Get_BookingMealPlan";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@BookingId", DbType.Int32, BookingId);
                if (MealPlanDate != DateTime.MinValue)
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@MealPlanDate", DbType.Date, MealPlanDate);
                else
                    oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@MealPlanDate", DbType.Date, DBNull.Value);

                dsBookingMealPlanData = oDB.ExecuteDataSet(oDB.DbCmd);
                oDB = null;

                if (dsBookingMealPlanData != null)
                {
                    oBookingMealPlanDTO = new BookingMealPlanDTO[dsBookingMealPlanData.Tables[0].Rows.Count];
                    for (int i = 0; i < dsBookingMealPlanData.Tables[0].Rows.Count; i++)
                    {
                        oBookingMealPlanDTO[i] = new BookingMealPlanDTO();
                        dr = dsBookingMealPlanData.Tables[0].Rows[i];
                        if (dr.ItemArray.GetValue(0) != DBNull.Value)
                            oBookingMealPlanDTO[i].BookingId = Convert.ToInt32(dr.ItemArray.GetValue(0));
                        if (dr.ItemArray.GetValue(1) != DBNull.Value)
                            oBookingMealPlanDTO[i].MealPlanId = Convert.ToInt32(dr.ItemArray.GetValue(1));
                        if (dr.ItemArray.GetValue(2) != DBNull.Value)
                            oBookingMealPlanDTO[i].MealPlanCode = Convert.ToString(dr.ItemArray.GetValue(2));
                        if (dr.ItemArray.GetValue(3) != DBNull.Value)
                            oBookingMealPlanDTO[i].MealPlanDate = Convert.ToDateTime(dr.ItemArray.GetValue(3).ToString()); 
                        if (dr.ItemArray.GetValue(4) != DBNull.Value)
                            oBookingMealPlanDTO[i].WelcomeDrink = Convert.ToBoolean(dr.ItemArray.GetValue(4));
                        if (dr.ItemArray.GetValue(5) != DBNull.Value)
                            oBookingMealPlanDTO[i].Breakfast = Convert.ToBoolean(dr.ItemArray.GetValue(5));
                        if (dr.ItemArray.GetValue(6) != DBNull.Value)
                            oBookingMealPlanDTO[i].Lunch = Convert.ToBoolean(dr.ItemArray.GetValue(6));
                        if (dr.ItemArray.GetValue(7) != DBNull.Value)
                            oBookingMealPlanDTO[i].Dinner = Convert.ToBoolean(dr.ItemArray.GetValue(7));
                    }
                }
            }
            catch (Exception exp)
            {
                oDB = null;
                dsBookingMealPlanData = null;
                throw exp;
            }
            return oBookingMealPlanDTO;
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
            catch (Exception exp)
            {
                oDB = null;
                ds = null;
                GF.LogError("clsBookingMealPlanMaster.GetDataFromDB", exp.Message);
            }
            return ds;
        }
    }
}
