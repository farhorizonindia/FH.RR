using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FarHorizon.Reservations.DataBaseManager;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.Common.DataEntities.Masters;

namespace FarHorizon.Reservations.BusinessTier.BusinessLogic.BookingEngine
{
    internal class SeriesDetailHandler
    {
        public bool AddSeriesDetail(List<SeriesDetailDTO> oSeriesDetailDatas)
        {
            DatabaseManager oDB;            
            try
            {
                oDB = new DatabaseManager();
                string sProcName = "up_Ins_SeriesDetail";
                if (oSeriesDetailDatas != null)
                {
                    foreach (SeriesDetailDTO SeriesData in oSeriesDetailDatas)
                    {
                        oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                        oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@SeriesID", DbType.Int32, SeriesData.SeriesID);
                        oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@CheckIn", DbType.DateTime, SeriesData.CheckIn);
                        oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@CheckOut", DbType.DateTime, SeriesData.CheckOut);
                        oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@RoomCategoryId", DbType.Int32, SeriesData.RoomCategoryId);
                        oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@RoomTypeId", DbType.Int32, SeriesData.RoomTypeId);
                        oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Requested", DbType.Int32, SeriesData.Requested);
                        oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Available", DbType.Int32, SeriesData.Available);
                        oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Waitlisted", DbType.Int32, SeriesData.Waitlisted);
                        oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@Confirmed", DbType.Int32, SeriesData.Confirmed);
                        oDB.ExecuteNonQuery(oDB.DbCmd);
                        oDB.DbCmd.Parameters.Clear();
                    }
                }
            }
            catch (Exception exp)
            {
                oDB = null;
                GF.LogError("clsSeriesManager.AddSeriesBooking", exp.Message);
                return false;
            }
            finally
            {
                oDB = null;
                oSeriesDetailDatas = null;
            }
            return true;
        }
        public List<SeriesDetailDTO> GetSeriesDetail(int SeriesId)
        {
            List<SeriesDetailDTO> oSeriesDetailDatas = new List<SeriesDetailDTO>();
            SeriesDetailDTO oSeriesDetailData;
            DataSet dsSeriesDetailData;
            DataRow dr;
            string sProcName;
            DatabaseManager oDB;

            dsSeriesDetailData = null;
            oSeriesDetailData = null;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Get_SeriesDetail";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iSeriesId", DbType.Int32, SeriesId);
                dsSeriesDetailData = oDB.ExecuteDataSet(oDB.DbCmd);
                oDB = null;
            }
            catch (Exception exp)
            {
                oDB = null;
                dsSeriesDetailData = null;
                GF.LogError("clsBookingRoomHandler.GetSeriesDetail", exp.Message);
            }
            if (dsSeriesDetailData != null)
            {                
                for (int i = 0; i < dsSeriesDetailData.Tables[0].Rows.Count; i++)
                {
                    oSeriesDetailData = new SeriesDetailDTO();
                    dr = dsSeriesDetailData.Tables[0].Rows[i];
                    oSeriesDetailData.SeriesID = (dr.ItemArray.GetValue(0) == DBNull.Value ? 0 : Convert.ToInt32(dr.ItemArray.GetValue(0)));
                    oSeriesDetailData.CheckIn = (dr.ItemArray.GetValue(1) == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr.ItemArray.GetValue(1)));
                    oSeriesDetailData.CheckOut = (dr.ItemArray.GetValue(2) == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr.ItemArray.GetValue(2)));
                    oSeriesDetailData.RoomCategoryId = (dr.ItemArray.GetValue(3) == DBNull.Value ? 0 : Convert.ToInt32(dr.ItemArray.GetValue(3)));
                    oSeriesDetailData.RoomTypeId = (dr.ItemArray.GetValue(4) == DBNull.Value ? 0 : Convert.ToInt32(dr.ItemArray.GetValue(4)));
                    oSeriesDetailData.Requested = (dr.ItemArray.GetValue(5) == DBNull.Value ? 0 : Convert.ToInt32(dr.ItemArray.GetValue(5)));
                    oSeriesDetailData.Available = (dr.ItemArray.GetValue(6) == DBNull.Value ? 0 : Convert.ToInt32(dr.ItemArray.GetValue(6)));
                    oSeriesDetailData.Waitlisted = (dr.ItemArray.GetValue(7) == DBNull.Value ? 0 : Convert.ToInt32(dr.ItemArray.GetValue(7)));
                    oSeriesDetailData.Confirmed = (dr.ItemArray.GetValue(8) == DBNull.Value ? 0 : Convert.ToInt32(dr.ItemArray.GetValue(8)));
                    oSeriesDetailData.RoomCategory = (dr.ItemArray.GetValue(9) == DBNull.Value ? string.Empty : Convert.ToString(dr.ItemArray.GetValue(9)));
                    oSeriesDetailData.RoomType = (dr.ItemArray.GetValue(10) == DBNull.Value ? string.Empty : Convert.ToString(dr.ItemArray.GetValue(10)));
                    oSeriesDetailData.BookingId = (dr.ItemArray.GetValue(11) == DBNull.Value ? 0 : Convert.ToInt32(dr.ItemArray.GetValue(11)));
                    oSeriesDetailData.ProposedBooking = dr.ItemArray.GetValue(12) == DBNull.Value ? false : Convert.ToBoolean(dr.ItemArray.GetValue(12));
                    oSeriesDetailData.BookingCode = dr.ItemArray.GetValue(13) == DBNull.Value ? String.Empty : Convert.ToString(dr.ItemArray.GetValue(13));
                    oSeriesDetailData.BookingRef = dr.ItemArray.GetValue(14) == DBNull.Value ? String.Empty : Convert.ToString(dr.ItemArray.GetValue(14));
                    oSeriesDetailDatas.Add(oSeriesDetailData);
                }
            }
            return oSeriesDetailDatas;
        }
    }
}
