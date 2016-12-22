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
    public class SMTPMaster 
    {
        #region IMaster Members

        public bool Insert(SMTPDTO oSMTPData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Ins_SMTPMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);                
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@SMTPServer", DbType.String, oSMTPData.SMTPServer);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@FromId", DbType.String, oSMTPData.FromEmailId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ReplyToId", DbType.String, oSMTPData.ReplyToId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@FromDisplayName", DbType.String, oSMTPData.FromDisplayName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@UserId", DbType.String, oSMTPData.SMTPUserId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Password", DbType.String, oSMTPData.SMTPPassword);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Port", DbType.Int32, oSMTPData.Port);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Active", DbType.Boolean, oSMTPData.Active);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                GF.LogError("clsSMTPMaster.Insert", exp.Message.ToString());
                oDB = null;
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public bool Update(SMTPDTO oSMTPData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Upd_SMTPMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@SMTPID", DbType.String, oSMTPData.SMTPId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@SMTPServer", DbType.String, oSMTPData.SMTPServer);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@FromId", DbType.String, oSMTPData.FromEmailId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ReplyToId", DbType.String, oSMTPData.ReplyToId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@FromDisplayName", DbType.String, oSMTPData.FromDisplayName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@UserId", DbType.String, oSMTPData.SMTPUserId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Password", DbType.String, oSMTPData.SMTPPassword);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Port", DbType.Int32, oSMTPData.Port);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Active", DbType.Boolean, oSMTPData.Active);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                GF.LogError("clsSMTPMaster.Update", exp.Message.ToString());
                oDB = null;
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public bool Delete(SMTPDTO SMTPDTO)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Del_SMTPMaster";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@SMTPID", DbType.String, SMTPDTO.SMTPId);                
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                GF.LogError("clsSMTPMaster.Delete", exp.Message.ToString());
                oDB = null;
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public SMTPDTO[] GetSMTPDetails()
        {
            return GetSMTPDetails(0, false);
        }

        public SMTPDTO[] GetSMTPDetails(bool Active)
        {
           return GetSMTPDetails(0, Active);
        }

        public SMTPDTO[] GetSMTPDetails(int SMTPId)
        {
            return GetSMTPDetails(SMTPId, false);
        }

        public SMTPDTO[] GetSMTPDetails(int SMTPId, bool Active)
        {            
            DataSet ds;
            SMTPDTO[] SMTPData;

            SMTPData = null;
            ds = null;
            string sProcName;
            DatabaseManager oDB;
            DataRow dr;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Get_SMTPDetails";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@SMTPID", DbType.Int32, SMTPId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Active", DbType.Int32, Active);
                ds = oDB.ExecuteDataSet(oDB.DbCmd);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    SMTPData = new SMTPDTO[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        SMTPData[i] = new SMTPDTO();
                        dr= ds.Tables[0].Rows[i];

                        if (dr[0] != DBNull.Value)
                            SMTPData[i].SMTPId = Convert.ToInt32(dr[0].ToString());
                        if (dr[1] != DBNull.Value)
                            SMTPData[i].SMTPServer = Convert.ToString(dr[1].ToString());
                        if (dr[2] != DBNull.Value)
                            SMTPData[i].FromEmailId = Convert.ToString(dr[2].ToString());
                        if (dr[3] != DBNull.Value)
                            SMTPData[i].ReplyToId = Convert.ToString(dr[3].ToString());
                        if (dr[4] != DBNull.Value)
                            SMTPData[i].FromDisplayName = Convert.ToString(dr[4].ToString());
                        if (dr[5] != DBNull.Value)
                            SMTPData[i].SMTPUserId = Convert.ToString(dr[5].ToString());
                        if (dr[6] != DBNull.Value)
                            SMTPData[i].SMTPPassword = Convert.ToString(dr[6].ToString());
                        if (dr[7] != DBNull.Value)
                            SMTPData[i].Port = Convert.ToInt32(dr[7].ToString());
                        if (dr[8] != DBNull.Value)
                            SMTPData[i].Active = Convert.ToBoolean(dr[8].ToString());
                    }
                }
            }
            catch (Exception exp)
            {
                GF.LogError("clsSMTPMaster.GetSMTP", exp.Message.ToString());
                oDB = null;
                return null;
            }
            finally
            {
                oDB = null;
            }
            //            return oCombinedAccomData;           
            return SMTPData;
        }

        public bool IsValidEmailId(string EmailId)
        {
            return GF.ValidateEmailId(EmailId);
        }
        #endregion
    }
}
