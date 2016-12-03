using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Web.Configuration;
using FarHorizon.Reservations.BusinessTier.BusinessLogic;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.DataBaseManager;
using FarHorizon.Reservations.MasterServices;

namespace FarHorizon.Reservations.BusinessTier.BusinessLogic.BookingChart
{
    internal class TreeViewHandler
    {
        public BookingChartTreeDTO[] GetTreeData(out string DefaultTreeTypeCode)
        {
            DataSet dsTreeData = null;
            DatabaseManager oDB = null;
            BookingChartTreeDTO[] oBookingChartDTO =null;
            String sProcName = String.Empty;
            String DefaultTreeType = String.Empty;
            
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Get_BookingTreeData";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddOutParameter(oDB.DbCmd, "@sDefaultTreeType", DbType.String, 20); //, DefaultTreeType); 
                dsTreeData = oDB.ExecuteDataSet(oDB.DbCmd);

                oBookingChartDTO = MapDBDataToObject(dsTreeData);
                DefaultTreeType = oDB.DbCmd.Parameters[0].Value.ToString();
            }
            catch (Exception exp)
            {
                oDB = null;
                dsTreeData = null;
                GF.LogError("clsTreeViewManager.GetTreeData", exp.Message);
            }
            DefaultTreeTypeCode = DefaultTreeType;
            return oBookingChartDTO;
        }

        private BookingChartTreeDTO[] MapDBDataToObject(DataSet dsTreeData)
        {
            DataRow dr;
            BookingChartTreeDTO[] oBookingChartDTO = null;
            try
            {
                if (dsTreeData != null && dsTreeData.Tables[0].Rows.Count > 0)
                {
                    oBookingChartDTO = new BookingChartTreeDTO[dsTreeData.Tables[0].Rows.Count];
                    for (int i = 0; i < dsTreeData.Tables[0].Rows.Count; i++)
                    {
                        oBookingChartDTO[i] = new BookingChartTreeDTO();
                        oBookingChartDTO[i].Region = new RegionDTO();
                        oBookingChartDTO[i].AccomodationType = new AccomTypeDTO();
                        oBookingChartDTO[i].Accomodation = new AccomodationDTO();
                        dr = dsTreeData.Tables[0].Rows[i];
                        oBookingChartDTO[i].Region.RegionId = Convert.ToInt32(dr.ItemArray.GetValue(0));
                        oBookingChartDTO[i].Region.RegionName = Convert.ToString(dr.ItemArray.GetValue(1));
                        oBookingChartDTO[i].AccomodationType.AccomodationTypeId = Convert.ToInt32(dr.ItemArray.GetValue(2));
                        oBookingChartDTO[i].AccomodationType.AccomodationType = Convert.ToString(dr.ItemArray.GetValue(3));
                        oBookingChartDTO[i].Accomodation.AccomodationId = Convert.ToInt32(dr.ItemArray.GetValue(4));
                        oBookingChartDTO[i].Accomodation.AccomodationName = Convert.ToString(dr.ItemArray.GetValue(5));
                    }
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return oBookingChartDTO;
        }

        public string GetDefaultTreeType()
        {
            TreeTypeMaster oTreeTypeMaster = null;
            TreeTypeDTO oTreeTypeData = null;
            try
            {
                oTreeTypeMaster = new TreeTypeMaster();
                oTreeTypeData = oTreeTypeMaster.GetDefaultTreeType();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return oTreeTypeData.TreeType;
        }

        public TreeTypeDTO[] GetTreeTypes()
        {
            TreeTypeMaster oTreeTypeMaster;
            oTreeTypeMaster = new TreeTypeMaster();
            return oTreeTypeMaster.GetTreeTypes();
        }

        public void SetDefaultTreeType(int TreeTypeId)
        {
            TreeTypeMaster oTreeTypeMaster;
            oTreeTypeMaster = new TreeTypeMaster();
            oTreeTypeMaster.SetDefaultTree(TreeTypeId);
        }
    }
}
