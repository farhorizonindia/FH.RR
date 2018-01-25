using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using FarHorizon.Reservations.Common;

using FarHorizon.Reservations.DataBaseManager;
using System.Data;
using System.Data.SqlClient;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using System.Configuration;

namespace FarHorizon.Reservations.MasterServices
{
    public class EventMessageMaster 
    {
        #region IMaster Members
    string    strCon = ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;
        public bool Insert(EventMessageDTO oEventMessageData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Ins_EventMessage";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@EventName", DbType.String, oEventMessageData.EventName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@EventMessage", DbType.String, oEventMessageData.EventMessage);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@EventSubject", DbType.String, oEventMessageData.EventSubject);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@EventMessageDefault", DbType.String, oEventMessageData.EventMessageDefault);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@MailAllowed", DbType.Boolean, oEventMessageData.MailAllowed);
                
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                GF.LogError("clsEventMessageMaster.Insert", exp.Message.ToString());
                oDB = null;
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public bool Update(EventMessageDTO oEventMessageData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Upd_EventMessage";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@MessageId", DbType.String, oEventMessageData.MessageId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@EventName", DbType.String, oEventMessageData.EventName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@EventMessage", DbType.String, oEventMessageData.EventMessage);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@EventSubject", DbType.String, oEventMessageData.EventSubject);                
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@MailAllowed", DbType.Boolean, oEventMessageData.MailAllowed);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                GF.LogError("clsEventMessageMaster.Update", exp.Message.ToString());
                oDB = null;
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }
        public bool Delete(EventMessageDTO oEventMessageData)
        {
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Del_EventMessage";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iMessageId", DbType.Int32, oEventMessageData.MessageId);
                oDB.ExecuteNonQuery(oDB.DbCmd);
            }
            catch (Exception exp)
            {
                GF.LogError("clsEventMessage.Delete", exp.Message.ToString());
                oDB = null;
                return false;
            }
            finally
            {
                oDB = null;
            }
            return true;
        }

        public EventMessageDTO[] GetEventMessage()
        {
            return GetEventMessage(0, string.Empty);
        }

        public EventMessageDTO[] GetEventMessage(int MessageId)
        {
            return GetEventMessage(MessageId, string.Empty);
        }

        public EventMessageDTO[] GetEventMessage(string EventName)
        {
            return GetEventMessage(0, EventName);
        }
        public DataTable getmessgaeforpassword()
        {
            try
            {
                SqlConnection cn = new SqlConnection(strCon);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("[dbo].[getmailforpassword]", cn);
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                cn.Open();
                da.SelectCommand.ExecuteReader();
                DataTable dtReturnData = new DataTable();
                cn.Close();
                da.Fill(dtReturnData);
                if (dtReturnData != null)
                    return dtReturnData;
                else
                    return null;
            }
            catch (Exception exp)
            {
                Console.Write(exp.Message);
                return null;
            }
        }
        private EventMessageDTO[] GetEventMessage(int MessageId, string EventName)
        {
            DataSet ds;
            EventMessageDTO[] EventMessageData;

            EventMessageData = null;
            ds = null;
            string sProcName;
            DatabaseManager oDB;
            try
            {
                oDB = new DatabaseManager();

                sProcName = "up_Get_EventMessageDetails";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iMessageId", DbType.Int32, MessageId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@EventName", DbType.String, EventName);
                ds = oDB.ExecuteDataSet(oDB.DbCmd);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    EventMessageData = new EventMessageDTO[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        EventMessageData[i] = new EventMessageDTO();
                        //MessageId, EventName, EventMessage, EventSubject,
                        //EventMessageDefault, MailAllowed

                        EventMessageData[i].MessageId = Convert.ToInt32(ds.Tables[0].Rows[i][0].ToString());
                        EventMessageData[i].EventName = Convert.ToString(ds.Tables[0].Rows[i][1].ToString());
                        EventMessageData[i].EventMessage = Convert.ToString(ds.Tables[0].Rows[i][2].ToString());
                        EventMessageData[i].EventSubject = Convert.ToString(ds.Tables[0].Rows[i][3].ToString());
                        EventMessageData[i].EventMessageDefault = Convert.ToString(ds.Tables[0].Rows[i][4].ToString());
                        EventMessageData[i].MailAllowed = Convert.ToBoolean(ds.Tables[0].Rows[i][5].ToString());
                    }
                }
            }
            catch (Exception exp)
            {
                GF.LogError("clsEventMessageMaster.GetEventMessage", exp.Message.ToString());
                oDB = null;
                return null;
            }
            finally
            {
                oDB = null;
            }
            //            return oCombinedAccomData;           
            return EventMessageData;
        }
        #endregion
    }
}
