using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.DataBaseManager;
using System.Data;
using System.Data.SqlClient;
using FarHorizon.Reservations.Common.DataEntities.Masters;

namespace FarHorizon.Reservations.MasterServices
{
    public class AccomodationContactsMaster 
    {
        #region IMaster Members

        public bool Insert(AccomContactDTO oAccomContactData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Ins_AccomodationContact";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomodationId", DbType.Int32, oAccomContactData.AccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ContactName", DbType.String, oAccomContactData.ContactName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ToId", DbType.String, oAccomContactData.ToId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@CCId", DbType.String, oAccomContactData.CCId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@BCCId", DbType.String, oAccomContactData.BCCId);

                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@MailOnBooking", DbType.Boolean, oAccomContactData.MailOnBooking);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@MailOnBookingUpdate", DbType.Boolean, oAccomContactData.MailOnBookingUpdate);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@MailOnBookingConfirmation", DbType.Boolean, oAccomContactData.MailOnBookingConfirmation);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@MailOnBookingConfirmationUpdate", DbType.Boolean, oAccomContactData.MailOnBookingConfirmationUpdate);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@MailOnCancellation", DbType.Boolean, oAccomContactData.MailOnCancellation);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@MailOnDeletion", DbType.Boolean, oAccomContactData.MailOnDeletion);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                GF.LogError("clsAccomodationContactsMaster.Insert", exp.Message.ToString());
                oDB = null;
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public bool Update(AccomContactDTO oAccomContactData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Upd_AccomodationContact";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ContactId", DbType.Int32, oAccomContactData.ContactId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomodationId", DbType.Int32, oAccomContactData.AccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ContactName", DbType.String, oAccomContactData.ContactName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ToId", DbType.String, oAccomContactData.ToId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@CCId", DbType.String, oAccomContactData.CCId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@BCCId", DbType.String, oAccomContactData.BCCId);

                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@MailOnBooking", DbType.Boolean, oAccomContactData.MailOnBooking);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@MailOnBookingUpdate", DbType.Boolean, oAccomContactData.MailOnBookingUpdate);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@MailOnBookingConfirmation", DbType.Boolean, oAccomContactData.MailOnBookingConfirmation);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@MailOnBookingConfirmationUpdate", DbType.Boolean, oAccomContactData.MailOnBookingConfirmationUpdate);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@MailOnCancellation", DbType.Boolean, oAccomContactData.MailOnCancellation);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@MailOnDeletion", DbType.Boolean, oAccomContactData.MailOnDeletion);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                GF.LogError("clsAccomodationContactsMaster.Update", exp.Message.ToString());
                oDB = null;
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public bool Delete(AccomContactDTO oAccomContactData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Del_AccomodationContact";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iContactId", DbType.Int32, oAccomContactData.ContactId);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                GF.LogError("clsAccomodationContact.Delete", exp.Message.ToString());
                oDB = null;
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public AccomContactDTO[] GetAccomodationContacts()
        {
            return GetAccomodationContacts(0, 0);
        }

        public AccomContactDTO[] GetAccomodationContactsOfBooking(int bookingAccomodationId)
        {
            return GetAccomodationContacts(bookingAccomodationId);
        }

        public AccomContactDTO[] GetAccomodationContacts(int AccomodationId)
        {
            return GetAccomodationContacts(AccomodationId, 0);
        }

        public AccomContactDTO[] GetAccomodationContacts(int AccomodationId, int ContactId)
        {
            DataSet ds;
            AccomContactDTO[] AccomContactData;

            AccomContactData = null;
            ds = null;
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Get_AccomodationContact";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@AccomodationId", DbType.Int32, AccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@ContactId", DbType.Int32, ContactId);
                ds = oDB.ExecuteDataSet(oDB.DbCmd);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    AccomContactData = new AccomContactDTO[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        AccomContactData[i] = new AccomContactDTO();
                        if (ds.Tables[0].Rows[i][0] != DBNull.Value)
                            AccomContactData[i].ContactId = Convert.ToInt32(ds.Tables[0].Rows[i][0].ToString());
                        if (ds.Tables[0].Rows[i][1] != DBNull.Value)
                            AccomContactData[i].AccomodationId = Convert.ToInt32(ds.Tables[0].Rows[i][1].ToString());
                        if (ds.Tables[0].Rows[i][2] != DBNull.Value)
                            AccomContactData[i].ContactName = Convert.ToString(ds.Tables[0].Rows[i][2].ToString());
                        if (ds.Tables[0].Rows[i][3] != DBNull.Value)
                            AccomContactData[i].ToId = Convert.ToString(ds.Tables[0].Rows[i][3].ToString());
                        if (ds.Tables[0].Rows[i][4] != DBNull.Value)
                            AccomContactData[i].CCId = Convert.ToString(ds.Tables[0].Rows[i][4].ToString());
                        if (ds.Tables[0].Rows[i][5] != DBNull.Value)
                            AccomContactData[i].BCCId = Convert.ToString(ds.Tables[0].Rows[i][5].ToString());
                        if (ds.Tables[0].Rows[i][6] != DBNull.Value)
                            AccomContactData[i].MailOnBooking = Convert.ToBoolean(ds.Tables[0].Rows[i][6].ToString());
                        if (ds.Tables[0].Rows[i][7] != DBNull.Value)
                            AccomContactData[i].MailOnBookingUpdate = Convert.ToBoolean(ds.Tables[0].Rows[i][7].ToString());
                        if (ds.Tables[0].Rows[i][8] != DBNull.Value)
                            AccomContactData[i].MailOnBookingConfirmation = Convert.ToBoolean(ds.Tables[0].Rows[i][8].ToString());
                        if (ds.Tables[0].Rows[i][9] != DBNull.Value)
                            AccomContactData[i].MailOnBookingConfirmationUpdate = Convert.ToBoolean(ds.Tables[0].Rows[i][9].ToString());
                        if (ds.Tables[0].Rows[i][10] != DBNull.Value)
                            AccomContactData[i].MailOnCancellation = Convert.ToBoolean(ds.Tables[0].Rows[i][10].ToString());
                        if (ds.Tables[0].Rows[i][11] != DBNull.Value)
                            AccomContactData[i].MailOnDeletion = Convert.ToBoolean(ds.Tables[0].Rows[i][11].ToString());
                    }
                }
            }
            catch (Exception exp)
            {
                GF.LogError("clsAccomodationContacts.Update", exp.Message.ToString());
                oDB = null;
                return null;
            }
            finally
            {
                oDB = null;
            }
            //            return oCombinedAccomData;           
            return AccomContactData;
        }

        public bool CopyContacts(int SourceAccomodationId, int DestinationAccomodationId)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Copy_AccomodationContacts";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iSourceAccomodationId", DbType.Int32, SourceAccomodationId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iDestinationAccomodationId", DbType.Int32, DestinationAccomodationId);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                GF.LogError("clsAccomodationContact.CopyContacts", exp.Message.ToString());
                oDB = null;
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        #endregion
    }
}
