using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Masters
{
    [Serializable]
    public class DepartmentDTO 
    {
        private int _DepartmentId;
        private string _DepartmentCode;
        private string _DepartmentName;

        #region DepartmentMasterProperties

        public int DepartmentId
        {
            get { return _DepartmentId; }
            set { _DepartmentId = value; }
        }

        public string DepartmentCode
        {
            get { return _DepartmentCode; }
            set { _DepartmentCode = value; }
        }

        public string DepartmentName
        {
            get { return _DepartmentName; }
            set { _DepartmentName = value; }
        }

        #endregion DepartmentMasterProperties
    }
}
