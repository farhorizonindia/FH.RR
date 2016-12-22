using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessTier.App_Code.DataEntities.Client
{
    public class clsAccomodationSeasonDTO
    {
        string _AccomodationId;
        string _SeasonStartDate;
        string _SeasonEndDate;

        public string AccomodationId
        {
            get { return _AccomodationId; }
            set { _AccomodationId = value; }
        }
        
        public string SeasonStartDate
        {
            get { return _SeasonStartDate; }
            set { _SeasonStartDate = value; }
        }        

        public string SeasonEndDate
        {
            get { return _SeasonEndDate; }
            set { _SeasonEndDate = value; }
        }
    }
}
