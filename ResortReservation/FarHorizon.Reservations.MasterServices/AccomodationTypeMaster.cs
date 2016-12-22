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
    public class AccomodationTypeMaster 
    {
        #region IMaster Members

        public bool Insert(AccomTypeDTO oAccomTypeData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Ins_AccomodationTypeMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sAccomType", DbType.String, oAccomTypeData.AccomodationType);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                GF.LogError("clsAccomodationMaster.Insert", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public bool Update(AccomTypeDTO oAccomTypeData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Upd_AccomodationTypeMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomTypeId", DbType.Int32, oAccomTypeData.AccomodationTypeId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@sAccomType", DbType.String, oAccomTypeData.AccomodationType);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                GF.LogError("clsAccomodationMaster.Update", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public bool Delete(AccomTypeDTO oAccomTypeData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Del_AccomodationTypeMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iAccomodationTypeId", DbType.Int32, oAccomTypeData.AccomodationTypeId);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                oDB = null;
                GF.LogError("clsAccomodationMaster.Delete", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public AccomTypeDTO[] GetData()
        {
            AccomTypeDTO[] objAccomodationTypeData;
            objAccomodationTypeData = GetData(0);
            return objAccomodationTypeData;
        }

        public AccomTypeDTO[] GetData(int AccomodationTypeId)
        {
            AccomTypeDTO[] objAccomodationTypeData;
            objAccomodationTypeData = null;
            DataSet ds;
            /*string query = "select distinct a.accomtypeid, a.accomtype from tbluseraccomrights r  ";
            query += " inner join tblaccomtypemaster a on  r.accomtypeid = a.accomtypeid  where 1=1 ";*/

            string query = "select distinct a.accomtypeid, a.accomtype from tblaccomtypemaster a where 1=1 ";

            if (AccomodationTypeId != 0)
            {
                query += " and a.AccomTypeId=" + AccomodationTypeId;
                query += " order by AccomType";
            }
            ds = GetDataFromDB(query);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                objAccomodationTypeData = new AccomTypeDTO[ds.Tables[0].Rows.Count];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    objAccomodationTypeData[i] = new AccomTypeDTO();
                    objAccomodationTypeData[i].AccomodationTypeId = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                    objAccomodationTypeData[i].AccomodationType = Convert.ToString(ds.Tables[0].Rows[i][1]);
                }
            }
            return objAccomodationTypeData;
        }
        /*
         * 
         * change made by vijay... by adding an overloaded function 
         * of GetData which accepts UserId as the argument to get the accomodation types assigend to this
         * particular user
         *              
         */
        private AccomTypeDTO[] GetData(int AccomodationTypeId, int UserId)
        {
            AccomTypeDTO[] objAccomodationTypeData;
            objAccomodationTypeData = null;
            DataSet ds;
            string query = "select distinct a.accomtypeid, a.accomtype from tbluseraccomrights r  ";
            query += " inner join tblaccomtypemaster a on  r.accomtypeid = a.accomtypeid  ";
            query += " and userid = " + Convert.ToString(UserId);
            if (AccomodationTypeId != 0)
            {
                query += " and AccomTypeId=" + AccomodationTypeId;
                query += " order by AccomType";
            }
            ds = GetDataFromDB(query);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                objAccomodationTypeData = new AccomTypeDTO[ds.Tables[0].Rows.Count];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    objAccomodationTypeData[i] = new AccomTypeDTO();
                    objAccomodationTypeData[i].AccomodationTypeId = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                    objAccomodationTypeData[i].AccomodationType = Convert.ToString(ds.Tables[0].Rows[i][1]);
                }
            }
            return objAccomodationTypeData;
        }
        private AccomTypeDTO[] GetAccomodationTypesPermitted(int UserID)
        {
            string sProcName;
            DatabaseManager oDB;
            AccomTypeDTO[] objAccomodationTypeData = null;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Get_AccomodationType_New";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iUserID", DbType.Int32, UserID);
                DataSet ds = oDB.ExecuteDataSet(oDB.DbCmd);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    objAccomodationTypeData = new AccomTypeDTO[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        objAccomodationTypeData[i] = new AccomTypeDTO();
                        objAccomodationTypeData[i].AccomodationTypeId = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                        objAccomodationTypeData[i].AccomodationType = Convert.ToString(ds.Tables[0].Rows[i][1]);
                    }
                }
            }
            catch (Exception exp)
            {
                oDB = null;
                GF.LogError("clsAccomodationMaster.GetAccomodationTypesPermitted", exp.Message);
                return null;
            }
            finally
            {
                oDB = null;
            }
            return objAccomodationTypeData; ;
        }
        /*
         * 
         * change made by vijay... by adding an overloaded function 
         * of GetAccomTypeWithAccomDetails which accepts
         * UserId as the argument to get the accomodations assigend to this
         * particular user
         *              
         */
        private AccomTypeDTO[] GetAccomTypeWithAccomDetails(int AccomodationTypeId, int UserId)
        {
            AccomTypeDTO[] objAccomodationTypeData;
            AccomodationMaster objAccomMaster;
            objAccomMaster = new AccomodationMaster();
            //
            //following line commented  by vijay to get accomodationtypes according to the rights of the user
            //

            //objAccomodationTypeData = GetData(AccomodationTypeId);

            objAccomodationTypeData = GetData(AccomodationTypeId, UserId);
            if (objAccomodationTypeData != null && objAccomodationTypeData.Length > 0)
            {
                for (int i = 0; i < objAccomodationTypeData.Length; i++)
                {
                    objAccomodationTypeData[i].Accomodations = objAccomMaster.GetData(0, objAccomodationTypeData[i].AccomodationTypeId, 0, UserId);
                }
            }
            return objAccomodationTypeData;
        }

        public AccomTypeDTO[] GetAccomTypeWithAccomDetails()
        {
            return GetAccomTypeWithAccomDetails(0);
        }

        public AccomTypeDTO[] GetAccomTypeWithAccomDetails(int AccomodationTypeId)
        {
            AccomTypeDTO[] objAccomodationTypeData;
            AccomodationMaster objAccomMaster;
            objAccomMaster = new AccomodationMaster();
            objAccomodationTypeData = GetData(AccomodationTypeId);
            if (objAccomodationTypeData != null && objAccomodationTypeData.Length > 0)
            {
                for (int i = 0; i < objAccomodationTypeData.Length; i++)
                {
                    objAccomodationTypeData[i].Accomodations = objAccomMaster.GetData(0, objAccomodationTypeData[i].AccomodationTypeId, 0);
                }
            }
            return objAccomodationTypeData;
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
                GF.LogError("clsAccomodationMaster.GetDataFromDB", exp.Message);
                oDB = null;
                ds = null;
            }
            return ds;
        }

        #endregion
    }
}
