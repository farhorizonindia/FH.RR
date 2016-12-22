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
    public class AccomodationMaster
    {
        #region IMaster Members
        /// <summary>
        /// THIS STRUCTURE IS BEING CONSTRUCTED JSUT TO HOLD THE COMBINED ACCOMTYPEID AND ACCOMID SO THAT 
        /// THEY CAN BE USED WHEN GRANTING RIGHTS TO USERS ON THOSE ACCOMS
        /// </summary>
        public struct CombinedAccomData
        {
            string _iCombinedIDs;
            string _sAccomName;

            public string CombinedIDs
            {
                get { return _iCombinedIDs; }
                set { _iCombinedIDs = value; }
            }

            public string AccomName
            {
                get { return _sAccomName; }
                set { _sAccomName = value; }
            }
        }

        public bool Insert(AccomodationDTO oAccomData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Ins_AccomodationMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomodationTypeId", DbType.Int32, oAccomData.AccomodationTypeId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sAccomodation", DbType.String, oAccomData.AccomodationName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@RegionId", DbType.Int32, oAccomData.RegionId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomInitial", DbType.String, oAccomData.AccomInitial);
                int accomId = Convert.ToInt32(oDB.ExecuteScalar(oDB.DbCmd));

            }
            catch (Exception exp)
            {
                GF.LogError("clsAccomodationMaster.Insert", exp.Message.ToString());
                oDB = null;
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public bool InsertAccomodationSeason(AccomodationSeasonDTO accomodationSeason)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                sProcName = "up_Ins_AccomodationMasterSeasonDate";
                oDB = new DatabaseManager();
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);                
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomId", DbType.Int32, accomodationSeason.AccomodationId);

                if (accomodationSeason.SeasonStartDate == DateTime.MinValue)
                    accomodationSeason.SeasonStartDate = GF.ParseDate("1900-01-01");
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@SeasonStartDate", DbType.DateTime, accomodationSeason.SeasonStartDate);

                if (accomodationSeason.SeasonEndDate == DateTime.MinValue)
                    accomodationSeason.SeasonEndDate = GF.ParseDate("1900-01-01");
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@SeasonEndDate", DbType.DateTime, accomodationSeason.SeasonEndDate);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                GF.LogError("clsAccomodationMaster.InsertAccomodationSeason", exp.Message.ToString());
                oDB = null;
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }
        
        public bool Update(AccomodationDTO oAccomData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Upd_AccomodationMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomId", DbType.Int32, oAccomData.AccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomodationTypeId", DbType.Int32, oAccomData.AccomodationTypeId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sAccomodation", DbType.String, oAccomData.AccomodationName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@RegionId", DbType.Int32, oAccomData.RegionId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomInitial", DbType.String, oAccomData.AccomInitial);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                GF.LogError("clsAccomodationMaster.Update", exp.Message.ToString());
                oDB = null;
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public bool UpdateAccomodationSeason(AccomodationSeasonDTO accomodationOldSeason, AccomodationSeasonDTO accomodationNewSeason)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                sProcName = "up_Upd_AccomodationMasterSeasonDate";
                oDB = new DatabaseManager();
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomId", DbType.Int32, accomodationOldSeason.AccomodationId);

                if (accomodationOldSeason.SeasonStartDate == DateTime.MinValue)
                    accomodationOldSeason.SeasonStartDate = GF.ParseDate("1900-01-01");
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@OldSeasonStartDate", DbType.DateTime, accomodationOldSeason.SeasonStartDate);

                if (accomodationOldSeason.SeasonEndDate == DateTime.MinValue)
                    accomodationOldSeason.SeasonEndDate = GF.ParseDate("1900-01-01");
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@OldSeasonEndDate", DbType.DateTime, accomodationOldSeason.SeasonEndDate);

                if (accomodationNewSeason.SeasonStartDate == DateTime.MinValue)
                    accomodationNewSeason.SeasonStartDate = GF.ParseDate("1900-01-01");
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@NewSeasonStartDate", DbType.DateTime, accomodationNewSeason.SeasonStartDate);

                if (accomodationNewSeason.SeasonEndDate == DateTime.MinValue)
                    accomodationNewSeason.SeasonEndDate = GF.ParseDate("1900-01-01");
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@NewSeasonEndDate", DbType.DateTime, accomodationNewSeason.SeasonEndDate);

                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                GF.LogError("clsAccomodationMaster.up_Upd_AccomodationMasterSeasonDate", exp.Message.ToString());
                oDB = null;
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public bool Delete(AccomodationDTO oAccomData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Del_AccomodationMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomodationId", DbType.Int32, oAccomData.AccomodationId);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                GF.LogError("clsAccomodationMaster.Delete", exp.Message.ToString());
                oDB = null;
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public bool DeleteAccomodationSeason(AccomodationSeasonDTO accomodationSeason)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Del_AccomodationMasterSeasonDate";
                oDB = new DatabaseManager();
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomId", DbType.Int32, accomodationSeason.AccomodationId);

                if (accomodationSeason.SeasonStartDate == DateTime.MinValue)
                    accomodationSeason.SeasonStartDate = GF.ParseDate("1900-01-01");
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@SeasonStartDate", DbType.DateTime, accomodationSeason.SeasonStartDate);

                if (accomodationSeason.SeasonEndDate == DateTime.MinValue)
                    accomodationSeason.SeasonEndDate = GF.ParseDate("1900-01-01");
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@SeasonEndDate", DbType.DateTime, accomodationSeason.SeasonEndDate);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                GF.LogError("clsAccomodationMaster.Delete", exp.Message.ToString());
                oDB = null;
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public AccomodationDTO[] GetData()
        {
            AccomodationDTO[] AccomodationData;
            AccomodationData = GetData(0, 0, 0);
            return AccomodationData;
        }

        public AccomodationDTO[] GetAccomodations()
        {
            return GetAccomodations(0);
        }
        public AccomodationDTO[] GetAccomodations(int AccomodationTypeId)
        {
            DataSet ds;
            AccomodationDTO[] AccomData;
            //CombinedAccomData[] oCombinedAccomData = null; ;
            AccomData = null;
            ds = null;
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_GetAccomodations";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomodationTypeID", DbType.Int32, AccomodationTypeId);
                ds = oDB.ExecuteDataSet(oDB.DbCmd);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    AccomData = new AccomodationDTO[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        AccomData[i] = new AccomodationDTO();
                        AccomData[i].AccomodationId = Convert.ToInt32(ds.Tables[0].Rows[i][0].ToString());
                        AccomData[i].AccomodationName = Convert.ToString(ds.Tables[0].Rows[i][1].ToString());
                        AccomData[i].AccomInitial = Convert.ToString(ds.Tables[0].Rows[i][2].ToString());
                    }
                }
            }
            catch (Exception exp)
            {
                GF.LogError("clsAccomodationMaster.Update", exp.Message.ToString());
                oDB = null;
                return null;
            }
            finally
            {
                oDB = null;
            }
            //            return oCombinedAccomData;           
            return AccomData;
        }

        public AccomodationDTO GetAccomodation(int accomodationTypeId, int accomodationId)
        {
            AccomodationDTO[] accomodationDTOList = GetAccomodations(accomodationTypeId);
            AccomodationDTO accomodation = null;
            for (int i = 0; i < accomodationDTOList.Length; i++)
            {
                if (accomodationDTOList[i].AccomodationId == accomodationId)
                {
                    accomodation = accomodationDTOList[i];
                }
            }
            return accomodation;
        }

        /*
         * Added an overloaded function by vijay by adding the fourth argument to accept userids
         * so taht accomodations of this particluar user can be fetched
         * 
         * 
         * */
        public AccomodationDTO[] GetData(int RegionId, int AccomodationTypeId, int AccomodationId, int UserId)
        {
            DataSet ds;
            AccomodationDTO[] AccomData;
            AccomData = null;
            ds = null;
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Get_Accomodations_New";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomTypeId", DbType.Int32, AccomodationTypeId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomId", DbType.Int32, AccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iRegionId", DbType.Int32, RegionId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iUserId", DbType.Int32, UserId);
                ds = oDB.ExecuteDataSet(oDB.DbCmd);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    AccomData = new AccomodationDTO[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        AccomData[i] = new AccomodationDTO();
                        AccomData[i].AccomodationTypeId = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                        AccomData[i].AccomodationType = Convert.ToString(ds.Tables[0].Rows[i][1]);
                        AccomData[i].AccomodationId = Convert.ToInt32(ds.Tables[0].Rows[i][2]);
                        AccomData[i].AccomodationName = Convert.ToString(ds.Tables[0].Rows[i][3]);
                        AccomData[i].RegionId = Convert.ToInt32(ds.Tables[0].Rows[i][4]);
                        AccomData[i].Region = Convert.ToString(ds.Tables[0].Rows[i][5]);
                        AccomData[i].AccomInitial = Convert.ToString(ds.Tables[0].Rows[i][6]);
                    }
                }
            }
            catch (Exception exp)
            {
                GF.LogError("clsAccomodationMaster.Update", exp.Message.ToString());
                oDB = null;
            }
            finally
            {
                oDB = null;
            }
            return AccomData;
        }

        public AccomodationDTO[] GetData(int RegionId, int AccomodationTypeId, int AccomodationId)
        {
            DataSet ds;
            AccomodationDTO[] AccomData;
            AccomData = null;
            ds = null;
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Get_Accomodations";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomTypeId", DbType.Int32, AccomodationTypeId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomId", DbType.Int32, AccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iRegionId", DbType.Int32, RegionId);
                ds = oDB.ExecuteDataSet(oDB.DbCmd);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    AccomData = new AccomodationDTO[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        AccomData[i] = new AccomodationDTO();
                        AccomData[i].AccomodationTypeId = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                        AccomData[i].AccomodationType = Convert.ToString(ds.Tables[0].Rows[i][1]);
                        AccomData[i].AccomodationId = Convert.ToInt32(ds.Tables[0].Rows[i][2]);
                        AccomData[i].AccomodationName = Convert.ToString(ds.Tables[0].Rows[i][3]);
                        AccomData[i].RegionId = Convert.ToInt32(ds.Tables[0].Rows[i][4]);
                        AccomData[i].Region = Convert.ToString(ds.Tables[0].Rows[i][5]);
                        AccomData[i].AccomInitial = Convert.ToString(ds.Tables[0].Rows[i][6]);
                        AccomData[i].AccomodationSeasonList = GetAccomodationSeasonDates(AccomData[i].AccomodationId);
                    }
                }
            }
            catch (Exception exp)
            {
                GF.LogError("clsAccomodationMaster.Update", exp.Message.ToString());
                oDB = null;
            }
            finally
            {
                oDB = null;
            }
            return AccomData;
        }

        public List<AccomodationSeasonDTO> GetAccomodationSeasonDates(int AccomodationId)
        {
            DataSet ds;
            List<AccomodationSeasonDTO> accomodationSeasonList = new List<AccomodationSeasonDTO>();
            AccomodationSeasonDTO accomodationSeason = null;
            ds = null;
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Get_AccomodationMasterSeasonDates";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomId", DbType.Int32, AccomodationId);
                ds = oDB.ExecuteDataSet(oDB.DbCmd);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        accomodationSeason = new AccomodationSeasonDTO();
                        accomodationSeason.AccomodationId = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                        accomodationSeason.SeasonStartDate = ds.Tables[0].Rows[i][1] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[i][1].ToString()) : DateTime.MinValue;
                        accomodationSeason.SeasonEndDate = ds.Tables[0].Rows[i][2] != DBNull.Value ? Convert.ToDateTime(ds.Tables[0].Rows[i][2].ToString()) : DateTime.MinValue;
                        accomodationSeasonList.Add(accomodationSeason);
                    }
                }
            }
            catch (Exception exp)
            {
                GF.LogError("clsAccomodationMaster.Update", exp.Message.ToString());
                oDB = null;
            }
            finally
            {
                oDB = null;
            }
            return accomodationSeasonList;
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
                GF.LogError("clsAccomodationMaster.GetDataFromDB", exp.Message.ToString());
                ds = null;
            }
            finally
            {
                oDB = null;
            }
            return ds;
        }
        #endregion
    }
}
