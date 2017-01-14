using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Masters
{
    [Serializable]
    public class AccomActivityDTO 
    {
        private int _iAccomId;
        private string _sAccomodationName;
        private int _iActivityId;
        private string _sActivityName;
        private string _sActivityDesc;

        public int AccomodationId
        {
            get { return _iAccomId; }
            set { _iAccomId = value; }
        }        
        public string AccomodationName
        {
            get { return _sAccomodationName; }
            set { _sAccomodationName = value; }
        }        
        public int ActivityId
        {
            get { return _iActivityId; }
            set { _iActivityId = value; }
        }        
        public string ActivityName
        {
            get { return _sActivityName; }
            set { _sActivityName = value; }
        }       
        public string ActivityDesc
        {
            get { return _sActivityDesc; }
            set { _sActivityDesc = value; }
        }       
    }
}
