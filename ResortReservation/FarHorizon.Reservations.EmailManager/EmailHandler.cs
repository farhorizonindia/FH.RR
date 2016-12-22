using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.Common.DataEntities.Client;

namespace FarHorizon.Reservations.EMailManager
{
    internal class EMailHandler
    {
        SMTPDTO _SMTPDetails;

        public EMailHandler(SMTPDTO SMTPDetails)
        {
            _SMTPDetails = SMTPDetails;
        }

        public bool SendEmail(EmailDTO EmailContents)
        {
            try
            {
                SMTPSolution SMTP = new SMTPSolution(_SMTPDetails);
                return SMTP.SendMail(EmailContents.To, EmailContents.CC, EmailContents.BCC,
                        EmailContents.Subject, EmailContents.Body, EmailContents.Attachments);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }

    class SMTPSolution
    {
        string _sSmtpServer;
        string _sTo;
        string _sCC;
        string _sBCC;
        string _sSubject;
        string _sBody;
        //string _sAttachments;
        //string _sUserName;
        //string _sPassword;
        SMTPDTO objSMTP;

        public SMTPSolution(SMTPDTO SMTPDetails)
        {
            objSMTP = SMTPDetails;
        }

        public string SMTPServer
        {
            //    get{return _sSmtpServer ;}
            set { _sSmtpServer = value; }
        }

        public bool SendMail(string To, string CC, string BCC, string Subject, string Body, string[] Attachments)
        {
            // To can be a semicolon (;) seperated string for multiple to recipients
            // CC can be a semicolon (;) seperated string for multiple to recipients
            // BCC can be a semicolon (;) seperated string for multiple to recipients
            // Attachments can be a semicolon (;) seperated string for multiple to recipients

            //_sMailStatus = "Failure while sending mail, Please refer to Error description for detailed error.";

            MailMessage Mail;
            string[] EmailIds;
            char[] Seperator;
            string AttachFile;
            Attachment attach1;

            _sTo = To;
            _sCC = CC;
            _sBCC = BCC;
            _sSubject = Subject;
            _sBody = Body;            

            Seperator = new char[1];
            Seperator[0] = ';';

            try
            {
                #region initiating Mail Message
                Mail = new MailMessage();
                if (_sSubject != "")
                    Mail.Subject = _sSubject;

                if (_sBody != "")
                    Mail.Body = _sBody;

                Mail.From = new MailAddress(objSMTP.FromEmailId, objSMTP.FromDisplayName, System.Text.Encoding.UTF8);
                Mail.ReplyTo = new MailAddress(objSMTP.ReplyToId);
                Mail.IsBodyHtml = true;
                #endregion

                #region Setting EmailIds
                #region Setting Tos
                if (_sTo != null)
                {
                    EmailIds = _sTo.Split(Seperator, StringSplitOptions.RemoveEmptyEntries);
                    if (EmailIds.Length > 0)
                    {
                        for (int i = 0; i < EmailIds.Length; i++)
                        {
                            if (ValidateEmail(EmailIds[i]) == true)
                            {
                                Mail.To.Add(EmailIds[i]);
                            }
                            //else
                            //_sErrorDescription = "Invalid Email id in TO list: " + EmailIds[i];
                        }
                        if (Mail == null || Mail.To.Count <= 0)
                        {
                            //_sErrorDescription = "Please set at least one recipiant in To list.";
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }

                #endregion Setting Tos

                #region Setting CCs
                EmailIds = null;
                if (_sCC != null)
                {
                    EmailIds = _sCC.Split(Seperator, StringSplitOptions.RemoveEmptyEntries);
                    if (EmailIds.Length > 0)
                    {
                        for (int i = 0; i < EmailIds.Length; i++)
                        {
                            if (ValidateEmail(EmailIds[i]) == true)
                            {
                                Mail.CC.Add(EmailIds[i]);
                            }
                            //else
                            //  _sErrorDescription = "Invalid Email id in CC list: " + EmailIds[i];
                        }
                    }
                }
                #endregion Setting CCs

                #region SettingBCCs
                EmailIds = null;
                if (_sBCC != null)
                {
                    EmailIds = _sBCC.Split(Seperator, StringSplitOptions.RemoveEmptyEntries);
                    if (EmailIds.Length > 0)
                    {
                        for (int i = 0; i < EmailIds.Length; i++)
                        {
                            EmailIds[i] = EmailIds[i].Trim().Replace(" ", "");
                            if (ValidateEmail(EmailIds[i]) == true)
                            {
                                Mail.Bcc.Add(EmailIds[i]);
                            }
                        }
                    }
                }
                #endregion SettingBCCs
                #endregion

                #region Setting Attachments
                if (Attachments != null)
                {
                    for (int i = 0; i < Attachments.Length; i++)
                    {
                        if (Attachments[i] != null)
                        {
                            AttachFile = Attachments[i].ToString();
                            attach1 = new Attachment(AttachFile);
                            Mail.Attachments.Add(attach1);
                        }
                    }
                }
                #endregion

                return SendMail(Mail);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        private bool SendMail(MailMessage Mail)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Credentials = new NetworkCredential(objSMTP.SMTPUserId, objSMTP.SMTPPassword);
                if (objSMTP.Port != 0)
                    smtpClient.Port = objSMTP.Port;
                smtpClient.Host = objSMTP.SMTPServer;

                smtpClient.EnableSsl = objSMTP.EnableSSL;
                Mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                smtpClient.Send(Mail);
                return true;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        private bool ValidateEmail(string sEmail)
        {
            sEmail = sEmail.Trim().Replace(" ", "");
            string Pattern = @"^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$";
            System.Text.RegularExpressions.Match match = Regex.Match(sEmail, Pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;
            else
                return false;
        }
    }
}
