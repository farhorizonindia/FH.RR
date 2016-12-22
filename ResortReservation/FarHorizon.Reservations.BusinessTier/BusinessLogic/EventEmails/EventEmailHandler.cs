using System;
using System.Collections.Generic;
using System.Text;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.BusinessTier.BusinessLogic.Reports;
using FarHorizon.Reservations.BusinessTier.BusinessLogic.BookingEngine;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.EMailManager;
using FarHorizon.Reservations.MasterServices;

namespace FarHorizon.Reservations.BusinessTier.BusinessLogic.EventEmails
{
    internal class EventEmailHandler
    {
        /// <summary>
        /// This Method will recive the booking Id, pull out the current status of the booking.
        /// and pull the email message and contacts and sent the email to the contacts.
        /// </summary>
        /// <param name="BookingId"></param>
        /// <returns></returns>
        public bool SendEventMail(int bookingId, ENums.EventName eventName)
        {
            return PrepareAndSendEmail(bookingId, eventName);
        }

        private bool PrepareAndSendEmail(int BookingId, ENums.EventName eventName)
        {
            StringBuilder EmailText = new StringBuilder();
            string text = string.Empty;

            EventMessageDTO[] EventMessageDTO;
            EventMessageDTO = PrepareEmailMessage(eventName, out text);
            EmailText.Append(text);

            EmailIdDTO emailIdDTO = new EmailIdDTO();            
            EmailIdDTO accomodationEmailIdDto = GetAccomodationContact(BookingId);
            EmailIdDTO userAgentEmailIdDTO = GetAgentUsersContact(BookingId);

            if (accomodationEmailIdDto != null)
            {
                emailIdDTO.TOs = accomodationEmailIdDto.TOs;
                emailIdDTO.CCs = accomodationEmailIdDto.CCs;
            }
            if (userAgentEmailIdDTO != null)
            {
                if (!String.IsNullOrEmpty(emailIdDTO.TOs.Trim()) && !emailIdDTO.TOs.Trim().EndsWith(";"))
                {
                    emailIdDTO.TOs = emailIdDTO.TOs.Trim() + ";";
                }
                if (!String.IsNullOrEmpty(emailIdDTO.CCs.Trim()) && !emailIdDTO.CCs.Trim().EndsWith(";"))
                {
                    emailIdDTO.CCs = emailIdDTO.CCs.Trim() + ";";
                }
                emailIdDTO.TOs += userAgentEmailIdDTO.TOs;
                emailIdDTO.CCs += userAgentEmailIdDTO.CCs;
            }

            if (String.IsNullOrEmpty(emailIdDTO.TOs.Trim()))
            {
                return false;
            }
            text = UpdateMessageWithContacts(EmailText.ToString(), emailIdDTO);
            EmailText.Remove(0, EmailText.Length);
            EmailText.Append(text);

            text = PrepareBookingDetails(BookingId);
            text = UpdateMessageWithBookingDetails(EmailText.ToString(), text);
            EmailText.Remove(0, EmailText.Length);
            EmailText.Append(text);

            if (emailIdDTO != null && EventMessageDTO[0] != null)
            {
                SendEmail(emailIdDTO, EventMessageDTO[0], EmailText.ToString());
            }
            return true;
        }

        private EmailIdDTO GetAgentUsersContact(int bookingId)
        {
            UserAgentMapperMaster userAgentMapperMaster = new UserAgentMapperMaster();
            AgentUserMapperDTO agentUserMapperDTO = userAgentMapperMaster.GetAgentUserEmailIds(bookingId);
            EmailIdDTO emailIdDTO = null;

            if (agentUserMapperDTO != null && agentUserMapperDTO.UserList != null)
            {
                foreach (UserDTO user in agentUserMapperDTO.UserList)
                {
                    if (!String.IsNullOrEmpty(user.EmailId) && GF.ValidateEmailId(user.EmailId))
                    {
                        if (emailIdDTO == null)
                        {
                            emailIdDTO = new EmailIdDTO();
                        }
                        emailIdDTO.TOs += user.EmailId + ";";
                    }
                }                
                if (!String.IsNullOrEmpty(agentUserMapperDTO.Agent.EmailId) && GF.ValidateEmailId(agentUserMapperDTO.Agent.EmailId))
                {
                    if (emailIdDTO == null)
                    {
                        emailIdDTO = new EmailIdDTO();
                    }
                    emailIdDTO.CCs = agentUserMapperDTO.Agent.EmailId + ";";
                }
            }
            return emailIdDTO;
        }

        private static EmailIdDTO GetAccomodationContact(int BookingId)
        {
            EmailIdDTO emailIdDTO = null;
            AccomodationContactsMaster accomodationContactsMaster = new AccomodationContactsMaster();
            AccomContactDTO[] accomContactDTO = accomodationContactsMaster.GetAccomodationContactsOfBooking(BookingId);

            if (accomContactDTO != null && accomContactDTO.Length > 0)
            {
                emailIdDTO = new EmailIdDTO();
                emailIdDTO.ContactPerson = accomContactDTO[0].ContactName;
                emailIdDTO.TOs = accomContactDTO[0].ToId;
                emailIdDTO.CCs = accomContactDTO[0].CCId;
                emailIdDTO.BCCs = accomContactDTO[0].BCCId;
            }
            return emailIdDTO;
        }

        private string UpdateMessageWithContacts(string text, EmailIdDTO emailIdDTO)
        {
            if (emailIdDTO != null && !String.IsNullOrEmpty(emailIdDTO.ContactPerson))
                text = text.Replace("PERSON", emailIdDTO.ContactPerson);

            text = text.Replace("\n", "<br/>");
            return text;
        }

        private string UpdateMessageWithBookingDetails(string text, string BookingDetails)
        {
            text = text.Replace("BOOKINGDETAILS", BookingDetails);
            return text;
        }

        private EventMessageDTO[] PrepareEmailMessage(ENums.EventName eventName, out string EmailMessageText)
        {
            EventMessageMaster eventMessageMaster = new EventMessageMaster();
            EventMessageDTO[] eventMessageDTO = eventMessageMaster.GetEventMessage(Enum.GetName(typeof(ENums.EventName), eventName));
            string _EmailMessageText = string.Empty;
            if (eventMessageDTO != null)
            {
                _EmailMessageText = PrepareMessageText(eventMessageDTO[0]);
            }
            EmailMessageText = _EmailMessageText;
            return eventMessageDTO;
        }

        private string PrepareBookingDetails(int BookingId)
        {
            #region GetBookingDetails
            BookingHandler bookingHandler = new BookingHandler();
            BookingDTO bookingDTO = bookingHandler.GetBookingDetails(BookingId);
            StringBuilder emailText = new StringBuilder();
            emailText.Append(string.Empty);
            string BookingText = string.Empty;
            if (bookingDTO != null)
            {
                BookingText = PrepareBookingText(bookingDTO);
            }
            emailText.Append(BookingText);
            #endregion

            emailText.Append("</br>");

            #region GetBookingRoomDetails
            BookingRoomReportsDTO[] oBRRD = null;
            BookingRoomReportsHandler oBRM = new BookingRoomReportsHandler();
            string BookingDetailsText = string.Empty;
            oBRRD = oBRM.GetDetailedBookingDetails(BookingId);
            if (oBRRD != null)
            {
                if (oBRRD.Length > 0)
                {
                    BookingDetailsText = PrepareBookingRoomDetailsText(oBRRD);
                }
            }
            emailText.Append(BookingDetailsText);
            #endregion

            return emailText.ToString();
        }

        private string PrepareMessageText(EventMessageDTO EventMessageDTO)
        {
            StringBuilder emailText = new StringBuilder();
            emailText.Append(string.Empty);
            //string HeaderStyle = "width:125; font-weight:bold";

            emailText.Append("<table id=tblMessage style='font-size:12px'>");
            #region Message Row
            emailText.Append("<tr>");
            emailText.Append("<td>" + EventMessageDTO.EventMessage + "</td>");
            emailText.Append("</tr>");
            #endregion
            return emailText.ToString();
        }

        private string PrepareBookingText(BookingDTO BookingDTO)
        {
            StringBuilder emailText = new StringBuilder();
            emailText.Append(string.Empty);

            StringBuilder Labelstyle = new StringBuilder();
            Labelstyle.Append(string.Empty);

            Labelstyle.Append("font-family: Arial, Helvetica, sans-serif; font-size: 10pt;");
            Labelstyle.Append("padding-right:10px;");
            Labelstyle.Append("border-style: none none solid;");
            Labelstyle.Append("background-color:#d8e1e8;");
            Labelstyle.Append("border-color:#e0dfe3;");
            Labelstyle.Append("border-bottom-width:1px;");

            StringBuilder Datastyle = new StringBuilder();
            Datastyle.Append(string.Empty);
            Datastyle.Append("font-family: Arial, Helvetica, sans-serif; font-size: 10pt;");
            Datastyle.Append("padding-right:10px; padding-left:5px;");
            Datastyle.Append("border-style: none none solid;");
            Datastyle.Append("border-color:#e0dfe3;");
            Datastyle.Append("border-bottom-width:1px;");

            emailText.Append("<table id=tblBooking style='font-size:12px'>");

            #region Booking Details
            emailText.Append("<tr>");
            emailText.Append("<td style='" + Labelstyle + "'>Booking Id:</td>");
            emailText.Append("<td style='" + Datastyle + "'>" + BookingDTO.BookingCode.ToString() + "</td>");
            emailText.Append("</tr>");

            emailText.Append("<tr>");
            emailText.Append("<td style='" + Labelstyle + "'>Accomodation:</td>");
            emailText.Append("<td style='" + Datastyle + "'>" + BookingDTO.AccomodationName + "</td>");
            emailText.Append("</tr>");

            emailText.Append("<tr>");
            emailText.Append("<td style='" + Labelstyle + "'>Check In:</td>");
            emailText.Append("<td style='" + Datastyle + "'>" + GF.GetDD_MMM_YYYY(BookingDTO.StartDate, false) + "</td>");
            emailText.Append("</tr>");

            emailText.Append("<tr>");
            emailText.Append("<td style='" + Labelstyle + "'>Check Out:</td>");
            emailText.Append("<td style='" + Datastyle + "'>" + GF.GetDD_MMM_YYYY(BookingDTO.EndDate, false) + "</td>");
            emailText.Append("</tr>");

            emailText.Append("<tr>");
            emailText.Append("<td style='" + Labelstyle + "'>Pax:</td>");
            emailText.Append("<td style='" + Datastyle + "'>" + BookingDTO.NoOfPersons.ToString() + "</td>");
            emailText.Append("</tr>");

            emailText.Append("<tr>");
            emailText.Append("<td style='" + Labelstyle + "'>Nights:</td>");
            emailText.Append("<td style='" + Datastyle + "'>" + BookingDTO.NoOfNights.ToString() + "</td>");
            emailText.Append("</tr>");

            emailText.Append("</table>");
            #endregion Booking Details
            return emailText.ToString();
        }

        private string PrepareBookingRoomDetailsText(BookingRoomReportsDTO[] BookingRoomReportsDTO)
        {
            StringBuilder emailText = new StringBuilder();
            emailText.Append(string.Empty);

            StringBuilder Labelstyle = new StringBuilder();
            Labelstyle.Append(string.Empty);

            Labelstyle.Append("font-family: Arial, Helvetica, sans-serif; font-size: 10pt;");
            Labelstyle.Append("padding-right:10px;");
            Labelstyle.Append("border-style: none none solid;");
            Labelstyle.Append("background-color:#d8e1e8;");
            Labelstyle.Append("border-color:#e0dfe3;");
            Labelstyle.Append("border-bottom-width:1px;");

            StringBuilder Datastyle = new StringBuilder();
            Datastyle.Append(string.Empty);
            Datastyle.Append("font-family: Arial, Helvetica, sans-serif; font-size: 10pt;");
            Datastyle.Append("padding-right:10px; padding-left:5px;");
            Datastyle.Append("border-style: none none solid;");
            Datastyle.Append("border-color:#e0dfe3;");
            Datastyle.Append("border-bottom-width:1px;");

            //string HeaderStyle = "width:125; font-weight:bold";            
            emailText.Append("<table id=tblBookingDetails>");

            #region Header Row
            emailText.Append("<tr>");
            emailText.Append("<td style='" + Labelstyle + "'>Room Category</td>");
            emailText.Append("<td style='" + Labelstyle + "'>Room Type</td>");
            emailText.Append("<td style='" + Labelstyle + "'>Booked</td>");
            emailText.Append("<td style='" + Labelstyle + "'>Waitlisted</td>");
            emailText.Append("</tr>");
            #endregion

            #region Room Details
            for (int i = 0; i < BookingRoomReportsDTO.Length; i++)
            {
                emailText.Append("<tr>");
                emailText.Append("<td style='" + Datastyle + "'>" + BookingRoomReportsDTO[i].RoomCategory + "</td>");
                emailText.Append("<td style='" + Datastyle + "'>" + BookingRoomReportsDTO[i].RoomType + "</td>");
                emailText.Append("<td style='" + Datastyle + "'>" + BookingRoomReportsDTO[i].TotalBooked.ToString() + "</td>");
                emailText.Append("<td style='" + Datastyle + "'>" + BookingRoomReportsDTO[i].TotalWaitlisted.ToString() + "</td>");
                emailText.Append("</tr>");
            }
            emailText.Append("</table>");
            #endregion Room Details

            return emailText.ToString();
        }

        private bool SendEmail(EmailIdDTO emailIds, EventMessageDTO EventMessage, string UpdatedMessage)
        {
            SMTPDTO projectSTMPDetails = GetSMTPDetails();
            if (projectSTMPDetails == null)
                return false;

            SMTPDTO smtpDTO = new SMTPDTO();
            smtpDTO.SMTPUserId = projectSTMPDetails.SMTPUserId;
            smtpDTO.SMTPPassword = projectSTMPDetails.SMTPPassword;
            smtpDTO.SMTPServer = projectSTMPDetails.SMTPServer;
            smtpDTO.Port = projectSTMPDetails.Port;
            smtpDTO.FromEmailId = projectSTMPDetails.FromEmailId;
            smtpDTO.ReplyToId = projectSTMPDetails.ReplyToId;
            smtpDTO.FromDisplayName = projectSTMPDetails.FromDisplayName;

            EmailDTO emailDTO = new EmailDTO();
            emailDTO.To = emailIds.TOs;
            emailDTO.CC = emailIds.CCs;
            emailDTO.BCC = emailIds.BCCs;
            emailDTO.Body = UpdatedMessage;
            emailDTO.Subject = EventMessage.EventSubject;

            EMailHelper emailHelper = new EMailHelper(smtpDTO);
            emailHelper.SendEmail(emailDTO);
            return true;
        }

        private SMTPDTO GetSMTPDetails()
        {
            SMTPDTO[] SMTPDTO;
            SMTPMaster SMTPMaster = new SMTPMaster();
            SMTPDTO = SMTPMaster.GetSMTPDetails(true);
            if (SMTPDTO != null && SMTPDTO.Length > 0)
                return SMTPDTO[0];
            else
                return null;
        }
    }

    class EmailIdDTO
    {
        String _contactPerson;
        String _tos;
        String _ccs;
        String _bccs;

        public EmailIdDTO()
        {
            _contactPerson =String.Empty;
            _tos = String.Empty;
            _ccs = String.Empty;
            _bccs = String.Empty;
        }

        public String ContactPerson
        {
            get { return _contactPerson; }
            set { _contactPerson = value; }
        }

        /// <summary>
        /// Semicolon or comma seperated string, with list of email ids.
        /// </summary>
        public String TOs
        {
            get { return _tos; }
            set { _tos = value; }
        }

        public String CCs
        {
            get { return _ccs; }
            set { _ccs = value; }
        }

        public String BCCs
        {
            get { return _bccs; }
            set { _bccs = value; }
        }

    }
}
