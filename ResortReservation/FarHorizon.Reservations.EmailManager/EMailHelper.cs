using System;
using System.Collections.Generic;
using System.Text;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.EMailManager;

namespace FarHorizon.Reservations.EMailManager
{
    public class EMailHelper
    {
        EMailHandler emailHandler;
        public EMailHelper(SMTPDTO SMTPDetails)
        {
            emailHandler = new EMailHandler(SMTPDetails);
        }

        public bool SendEmail(EmailDTO emailContents)
        {
            try
            {
                return emailHandler.SendEmail(emailContents);
            }
            catch (Exception exp)
            {
                throw exp;
            }            
        }        
    }
}
