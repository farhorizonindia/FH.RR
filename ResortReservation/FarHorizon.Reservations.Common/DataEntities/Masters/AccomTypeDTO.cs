using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Masters
{
    [Serializable]
    public class AccomTypeDTO 
    {
        private int _iAccomTypeId;
        private string _sAccomType;
        private AccomodationDTO[] _AccomData;

        public int AccomodationTypeId
        {
            get { return _iAccomTypeId; }
            set { _iAccomTypeId = value; }
        }
        public string AccomodationType
        {
            get { return _sAccomType; }
            set { _sAccomType = value; }
        }

        public AccomodationDTO[] Accomodations
        {
            get { return _AccomData; }
            set { _AccomData = value; }
        }
    }
}
