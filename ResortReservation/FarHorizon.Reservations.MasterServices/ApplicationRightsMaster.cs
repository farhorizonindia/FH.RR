using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.XMLManager;
using FarHorizon.Reservations.Common;

namespace FarHorizon.Reservations.MasterServices
{
    public class ApplicationRightsMaster 
    {
        public List<ApplicationRightsDTO> GetApplicationRights()
        {
            List<ApplicationRightsDTO> applicationRightsDtoList = new List<ApplicationRightsDTO>();
            ApplicationRightsDTO applicationRightsDto;

            XmlDocument appRights = LoadApplicationRightsXML();
            XmlNodeList nodeList;
            if (appRights != null && appRights.ChildNodes.Count > 0)
            {
                nodeList = appRights.SelectNodes("//Pages//PageType");
                if (nodeList != null && nodeList.Count > 0)
                {
                    foreach (XmlNode pageTypeNode in nodeList)
                    {
                        applicationRightsDto = new ApplicationRightsDTO();
                        applicationRightsDto.OperationType = pageTypeNode.Attributes["Description"].Value;
                        applicationRightsDto.PageWiseRights = GetPageWiseRights(pageTypeNode);
                        applicationRightsDtoList.Add(applicationRightsDto);
                    }
                }
            }
            return applicationRightsDtoList;
        }

        private List<PageWiseRights> GetPageWiseRights(XmlNode pageTypeNode)
        {
            List<PageWiseRights> pageWiseRightsList = new List<PageWiseRights>();
            PageWiseRights pageWiseRights = new PageWiseRights();
            if (pageTypeNode.ChildNodes != null && pageTypeNode.ChildNodes.Count > 0)
            {
                foreach (XmlNode pageNode in pageTypeNode.ChildNodes)
                {
                    pageWiseRights = new PageWiseRights();
                    pageWiseRights.PageId = pageNode.Attributes["Key"].Value;
                    pageWiseRights.PageDescription = pageNode.Attributes["Description"].Value;
                    pageWiseRights.Rights = GetRights(pageNode);
                    pageWiseRightsList.Add(pageWiseRights);
                }
            }
            return pageWiseRightsList;
        }

        private List<Rights> GetRights(XmlNode pageNode)
        {
            List<Rights> rightsList = new List<Rights>();
            Rights rights;

            if (pageNode != null && pageNode.ChildNodes.Count > 0)
            {
                foreach (XmlNode rightsNode in pageNode.ChildNodes)
                {
                    foreach (XmlNode rightNode in rightsNode.ChildNodes)
                    {
                        rights = new Rights();
                        rights.Id = rightNode.Attributes["Key"].Value;
                        rights.Description = rightNode.Attributes["Description"].Value;
                        rightsList.Add(rights);
                    }
                }
            }
            return rightsList;
        }

        private XmlDocument LoadApplicationRightsXML()
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                return XMLHelper.LoadXML(ENums.UploadXMLType.ApplicationRights);
            }
            catch (Exception exp)
            {
                throw exp;
            }            
        }

    }
}
