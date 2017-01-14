using System;
using System.Collections.Generic;
using System.Text;

namespace FarHorizon.Reservations.Common.DataEntities.Masters
{
    [Serializable]
    public class ApplicationRightsDTO 
    {
        string _operationType;
        List<PageWiseRights> _pageWiseRights;

        public string OperationType
        {
            get { return _operationType; }
            set { _operationType = value; }
        }        

        public List<PageWiseRights> PageWiseRights
        {
            get { return _pageWiseRights; }
            set { _pageWiseRights = value; }
        }
    }

    [Serializable]
    public class PageWiseRights
    {
        string _pageId;
        string _pageDescription;

        public string PageId
        {
            get { return _pageId; }
            set { _pageId = value; }
        }

        public string PageDescription
        {
            get { return _pageDescription; }
            set { _pageDescription = value; }
        }

        List<Rights> _rights;

        public List<Rights> Rights
        {
            get { return _rights; }
            set { _rights = value; }
        }
    }
    
    [Serializable]
    public class Rights
    {
        string _id;
        string _description;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
    }
}
