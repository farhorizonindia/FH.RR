using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataLayer;


namespace BusinessTier.App_Code
{
    public class clsBookingChart
    {
        clsDatabaseManager objDatabaseManager;

        public clsBookingChart()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataSet GetRoomDetails(int RegionId, int AccomodationTypeId, int AccomodationId)
        {
            DataSet ds;
            ds = new DataSet();
            string Query = "";
            /*Query = " select ATM.AccomTypeId, ATM.AccomType, AM.AccomId, AM.AccomName, Sequence, RoomNo from " +
                    " tblAccomTypeMaster ATM join tblAccomMaster AM on ATM.AccomTypeId = AM.AccomTypeId " +
                    " join tblRoomMaster RM on AM.AccomId = RM.AccomId " +
                    " where 1=1 " +
                    " order by AccomType, AccomName, Sequence";*/
            Query = " select ATM.AccomTypeId, ATM.AccomType, AM.AccomId, AM.AccomName, Sequence, RoomNo, " +
                    " null as '1', null as '2', null as '3', null as '4', null as '5', null as '6', null as '7', " +
                    " null as '8', null as '9', null as '10', null as '11', null as '12', null as '13', null as '14', " +
                    " null as '15', null as '16', null as '17', null as '18', null as '19', null as '20', null as '21', " +
                    " null as '22', null as '23', null as '24', null as '25', null as '26', null as '27', null as '28', " +
                    " null as '29', null as '30', null as '31' " +
                    " from tblAccomTypeMaster ATM join tblAccomMaster AM on ATM.AccomTypeId = AM.AccomTypeId " +
                    " join tblRoomMaster RM on AM.AccomId = RM.AccomId " +
                    " where 1=1 ";
            if (RegionId != 0)
                Query += " and AM.RegionId = " + RegionId;
            if (AccomodationTypeId != 0)
                Query += " and ATM.AccomTypeId = " + AccomodationTypeId;
            if (AccomodationId != 0)
                Query += " and AM.AccomId = " + AccomodationId;

            Query += " order by AccomType, AccomName, Sequence";

            objDatabaseManager = new clsDatabaseManager();
            ds = objDatabaseManager.FetchRecords("BookingChart", Query);
            return ds;
        }
    }
}
