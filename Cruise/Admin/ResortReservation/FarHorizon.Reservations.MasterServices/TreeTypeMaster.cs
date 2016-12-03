using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FarHorizon.Reservations.Common;

using FarHorizon.Reservations.DataBaseManager;
using FarHorizon.Reservations.Common.DataEntities.Masters;



namespace FarHorizon.Reservations.MasterServices
{
    public class TreeTypeMaster 
    {
        public TreeTypeDTO[] GetTreeTypes()
        {
            DataSet dsTreeTypeData;
            DatabaseManager oDB;
            DataRow dr;
            TreeTypeDTO[] oTreeTypeData = null;
            string sProcName;
            dsTreeTypeData = null;
            oTreeTypeData = null;

            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Get_TreeTypes";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                dsTreeTypeData = oDB.ExecuteDataSet(oDB.DbCmd);

                if (dsTreeTypeData != null)
                {
                    oTreeTypeData = new TreeTypeDTO[dsTreeTypeData.Tables[0].Rows.Count];
                    for (int i = 0; i < dsTreeTypeData.Tables[0].Rows.Count; i++)
                    {
                        dr = dsTreeTypeData.Tables[0].Rows[i];
                        oTreeTypeData[i] = new TreeTypeDTO();
                        oTreeTypeData[i].TreeTypeId = Convert.ToInt32(dr.ItemArray.GetValue(0));
                        oTreeTypeData[i].TreeType = Convert.ToString(dr.ItemArray.GetValue(1));
                        oTreeTypeData[i].Description = Convert.ToString(dr.ItemArray.GetValue(2));
                        oTreeTypeData[i].Selected = Convert.ToBoolean(dr.ItemArray.GetValue(3));
                    }
                }                
            }
            catch (Exception exp)
            {
                GF.LogError("clsTreeTypeMaster.GetTreeType", exp.Message);
            }
            return oTreeTypeData;
        }

        public TreeTypeDTO GetDefaultTreeType()
        {
            DataSet dsTreeTypeDefault=null;
            DataRow dr=null;
            DatabaseManager oDB;
            TreeTypeDTO oTreeTypeDefault = null;
            string sProcName;
            dsTreeTypeDefault = null;
            oTreeTypeDefault = null;

            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Get_TreeTypeDefault";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                dsTreeTypeDefault = oDB.ExecuteDataSet(oDB.DbCmd);

                if (dsTreeTypeDefault != null)
                {
                    oTreeTypeDefault = new TreeTypeDTO();
                    dr = dsTreeTypeDefault.Tables[0].Rows[0];
                    oTreeTypeDefault.TreeTypeId = Convert.ToInt32(dr.ItemArray.GetValue(0));
                    oTreeTypeDefault.TreeType = Convert.ToString(dr.ItemArray.GetValue(1));
                    oTreeTypeDefault.Description = Convert.ToString(dr.ItemArray.GetValue(2));
                    oTreeTypeDefault.Selected = Convert.ToBoolean(dr.ItemArray.GetValue(3));
                }
            }
            catch (Exception exp)
            {
                GF.LogError("clsTreeTypeMaster.GetTreeType", exp.Message);
            }
            return oTreeTypeDefault;
        }

        public void SetDefaultTree(int TreeTypeId)
        {
            DatabaseManager oDB;
            string sProcName;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Set_DefaultTreeType";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iTreeTypeId", DbType.Int32, TreeTypeId);
                oDB.ExecuteNonQuery(oDB.DbCmd);               
            }
            catch (Exception exp)
            {
                GF.LogError("clsTreeTypeMaster.SetDefaultTree", exp.Message);
            }
        }

    }
}
