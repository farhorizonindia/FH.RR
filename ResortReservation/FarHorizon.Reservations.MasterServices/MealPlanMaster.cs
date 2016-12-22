using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using FarHorizon.Reservations.Common;


using FarHorizon.Reservations.DataBaseManager;
using FarHorizon.Reservations.Common.DataEntities.Masters;

namespace FarHorizon.Reservations.MasterServices
{
    public class MealPlanMaster 
    {
        #region IMaster Members

        public bool Insert(MealPlanDTO oMealPlanData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Ins_MealPlanMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sMealPlanCode", DbType.String, oMealPlanData.MealPlanCode);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sMealPlan", DbType.String, oMealPlanData.MealPlan);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sMealPlanDesc", DbType.String, oMealPlanData.MealPlanDesc);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@bWelcomeDrink", DbType.Boolean, oMealPlanData.WelcomeDrink);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@bBreakFast", DbType.Boolean, oMealPlanData.Breakfast);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@bLunch", DbType.Boolean, oMealPlanData.Lunch);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@bEveSnacks", DbType.Boolean, oMealPlanData.EveningSnacks);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@bDinner", DbType.Boolean, oMealPlanData.Dinner);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oMealPlanData = null;
                GF.LogError("clsMealPlanMaster.Insert", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public bool Update(MealPlanDTO oMealPlanData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Upd_MealPlanMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@MealPlanId", DbType.Int32, oMealPlanData.MealPlanId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sMealPlanCode", DbType.String, oMealPlanData.MealPlanCode);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sMealPlan", DbType.String, oMealPlanData.MealPlan);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sMealPlanDesc", DbType.String, oMealPlanData.MealPlanDesc);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@bWelcomeDrink", DbType.Boolean, oMealPlanData.WelcomeDrink);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@bBreakFast", DbType.Boolean, oMealPlanData.Breakfast);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@bLunch", DbType.Boolean, oMealPlanData.Lunch);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@bEveSnacks", DbType.Boolean, oMealPlanData.EveningSnacks);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@bDinner", DbType.Boolean, oMealPlanData.Dinner);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oMealPlanData = null;
                GF.LogError("clsMealPlanMaster.Update", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public bool Delete(MealPlanDTO oMealPlanData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Del_MealPlanMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@MealPlanId", DbType.Int32, oMealPlanData.MealPlanId);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                oMealPlanData = null;
                GF.LogError("clsMealPlanMaster.Delete", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public MealPlanDTO[] GetData()
        {
            MealPlanDTO[] oMealPlanData;
            oMealPlanData = GetMeals(0);
            return oMealPlanData;
        }

        public MealPlanDTO[] GetMeals()
        {
            return GetMeals(0);
        }

        public MealPlanDTO[] GetMeals(int MealPlanId)
        {
            MealPlanDTO[] oMealPlanData=null;
            string sProcName = "up_Get_MealPlans";
            DatabaseManager oDB = new DatabaseManager();
            DataSet ds = null;
            //int iVal = 0;
            try
            {
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@MealPlanId", DbType.Int32, MealPlanId);
                ds = oDB.ExecuteDataSet(oDB.DbCmd);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    #region Populating Object
                    oMealPlanData = new MealPlanDTO[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        oMealPlanData[i] = new MealPlanDTO();
                        oMealPlanData[i].MealPlanId = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                        oMealPlanData[i].MealPlanCode = Convert.ToString(ds.Tables[0].Rows[i][1]);
                        oMealPlanData[i].MealPlan = Convert.ToString(ds.Tables[0].Rows[i][2]);
                        if (ds.Tables[0].Rows[i][3] != DBNull.Value)
                            oMealPlanData[i].WelcomeDrink = Convert.ToBoolean(ds.Tables[0].Rows[i][3]);                         
                        if (ds.Tables[0].Rows[i][4] != DBNull.Value)
                           oMealPlanData[i].Breakfast = Convert.ToBoolean(ds.Tables[0].Rows[i][4]);
                        if (ds.Tables[0].Rows[i][5] != DBNull.Value)
                            oMealPlanData[i].Lunch = Convert.ToBoolean(ds.Tables[0].Rows[i][5]);
                        if (ds.Tables[0].Rows[i][6] != DBNull.Value)
                            oMealPlanData[i].EveningSnacks = Convert.ToBoolean(ds.Tables[0].Rows[i][6]);
                        if (ds.Tables[0].Rows[i][7] != DBNull.Value)
                            oMealPlanData[i].Dinner = Convert.ToBoolean(ds.Tables[0].Rows[i][7]);
                    }
                    #endregion Populating Object
                }
            }
            catch (Exception exp)
            {
                GF.LogError("clsMealPlanMaster.GetMeals", exp.Message);
            }
            return oMealPlanData;
        }        

        public MealPlanDTO GetMealDetails(int MealPlanId)
        {
            MealPlanDTO oMealPlanData=null;
            oMealPlanData = null;
            DataSet ds;
            if (MealPlanId == 0)
                return oMealPlanData;
            string query = "select MealPlanId, MealPlanCode, MealPlan, MealPlanDesc, " +
                " WelcomeDrink, BreakFast, Lunch, EveSnacks, Dinner from tblMealPlanMaster where 1=1";
            if (MealPlanId != 0)
            {
                query += " and MealPlanId=" + MealPlanId;
                query += " order by MealPlanCode";
            }
            ds = GetDataFromDB(query);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                oMealPlanData = new MealPlanDTO();
                    oMealPlanData.MealPlanId = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                    oMealPlanData.MealPlanCode = Convert.ToString(ds.Tables[0].Rows[0][1]);
                    oMealPlanData.MealPlan = Convert.ToString(ds.Tables[0].Rows[0][2]);
                    if (ds.Tables[0].Rows[0][3] != DBNull.Value && !ds.Tables[0].Rows[0][3].Equals(string.Empty))
                        oMealPlanData.WelcomeDrink = Convert.ToBoolean(ds.Tables[0].Rows[0][3]);
                    if (ds.Tables[0].Rows[0][4] != DBNull.Value && !ds.Tables[0].Rows[0][4].Equals(string.Empty))
                        oMealPlanData.Breakfast = Convert.ToBoolean(ds.Tables[0].Rows[0][4]);
                    if (ds.Tables[0].Rows[0][5] != DBNull.Value && !ds.Tables[0].Rows[0][5].Equals(string.Empty))
                        oMealPlanData.Lunch = Convert.ToBoolean(ds.Tables[0].Rows[0][5]);
                    if (ds.Tables[0].Rows[0][6] != DBNull.Value && !ds.Tables[0].Rows[0][6].Equals(string.Empty))
                        oMealPlanData.EveningSnacks = Convert.ToBoolean(ds.Tables[0].Rows[0][6]);
                    if (ds.Tables[0].Rows[0][7] != DBNull.Value && !ds.Tables[0].Rows[0][7].Equals(string.Empty))
                        oMealPlanData.Dinner = Convert.ToBoolean(ds.Tables[0].Rows[0][7]);
            }
            return oMealPlanData;
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
                GF.LogError("clsMealPlanMaster.Delete", exp.Message);
                ds = null;
            }
            return ds;
        }

        #endregion        
    }
}
