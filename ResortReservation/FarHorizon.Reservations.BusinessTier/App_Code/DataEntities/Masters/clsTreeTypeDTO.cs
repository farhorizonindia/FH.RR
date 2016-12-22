using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessTier.App_Code.DataEntities.Masters
{
    public class clsTreeTypeDTO
    {
        private int _iTreeTypeId;
        private string _sTreeType;
        private string _sDescription;        
        private bool _bSelected;

        public int TreeTypeId
        {
            get { return _iTreeTypeId; }
            set { _iTreeTypeId = value; }
        }
        
        public string TreeType
        {
            get { return _sTreeType; }
            set { _sTreeType = value; }
        }

        public string Description
        {
            get { return _sDescription; }
            set { _sDescription = value; }
        }

        public bool Selected
        {
            get { return _bSelected; }
            set { _bSelected = value; }
        }
    }
}
