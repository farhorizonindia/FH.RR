using System;
using System.Collections.Generic;
using System.Text;
using BusinessTier.App_Code.DataEntities.Client;
using DataTier;
using System.Data;

namespace BusinessTier.App_Code.BusinessLogic.BookingEngine.Reports
{
    class clsBookingRoomReportsHandler1
    {
        public clsBookingRoomReportsData[] GetDetailedBookingDetails(int BookingId)
        {
            clsBookingRoomReportsData[] oBRD = null;
            clsDatabaseManager oDB = null;
            try
            {
                oDB = new clsDatabaseManager();
                string sProcName = "up_GetDetailedBookingDetails";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@BookingId", DbType.Int32, BookingId);
                DataSet dsBRD = null;
                dsBRD = oDB.ExecuteDataSet(oDB.DbCmd);
                oDB = null;

                if (dsBRD != null)
                {
                    oBRD = new clsBookingRoomReportsData[dsBRD.Tables[0].Rows.Count];
                    for (int i = 0; i < oBRD.Length; i++)
                    {
                        oBRD[i] = new clsBookingRoomReportsData();
                        oBRD[i].RoomCategory = dsBRD.Tables[0].Rows[i][0].ToString();
                        oBRD[i].RoomType = dsBRD.Tables[0].Rows[i][1].ToString();
                        oBRD[i].TotalBooked = Convert.ToInt32(dsBRD.Tables[0].Rows[i][2].ToString());
                        oBRD[i].TotalWaitlisted = Convert.ToInt32(dsBRD.Tables[0].Rows[i][3].ToString());
                    }
                    dsBRD = null;
                }
            }
            catch(Exception exp)
            {
                oDB = null;
                GenFunctions.LogError("clsBookingRoomReportsHandler.GetDetailedBookingDetails", exp.Message);
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
