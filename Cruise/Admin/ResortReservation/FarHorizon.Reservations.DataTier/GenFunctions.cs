using System;
using System.Data;
using System.Configuration;
using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;

namespace DataTier
{
    /// <summary>
    /// Summary description for GenFunctions
    /// </summary>
    public class GenFunctions
    {
        private static clsDatabaseManager objDB;

        private static DateTime CurrentDate;

        public enum DateFormats
        {
            ddmmyy = 1,
            ddmmyyyy = 2,
            ddmmmyy = 3,
            ddmmmyyyy = 4,
            mmddyyyy = 5,
            mmmddyyyy = 6
        }

        public enum GenderType
        {
            Male = 1,
            Female = 2
        }

        public enum ChartOf
        {
            Height,
            Weight,
            HeadCircumference
        }

        public enum BookingStatus
        {
            BOOKED = 1,
            CONFIRMED = 2,
            AVAILABLE = 3,
            WAITING = 4
        }


        public struct FollowUpDetails
        {
            public int RegNo;
            public string ObservationDate;
            public string Head;
            public string Height;
            public string Weight;
            public string SortOrder;
        }

        public struct VaccineDetails
        {
            public int RegNo;
            public int VaccineNo;
            public string VaccinationDate;
            public int SortOrder;
            public bool VaccineDone;
        }

        public struct ChildEmailDetails
        {
            public int RegNo;
            public string EmailId;
            public string ChildName;
            public string FatherName;
        }

        //public static Cursor WaitCursor()
        //{
        //    return Cursors.WaitCursor;
        //}

        //public static Cursor NormalCursor()
        //{
        //    return Cursors.Arrow;
        //}

        //public static Cursor ApplicationStartingCursor()
        //{
        //    return Cursors.AppStarting;
        //}

        public static bool Number(char KeyChar)
        {
            if (!Char.IsDigit(KeyChar) && KeyChar != (char)8 && KeyChar != (char)46)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool NumberOnly(char KeyChar)
        {
            if (!Char.IsDigit(KeyChar) && KeyChar != (char)8)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public static void CenterForm(System.Windows.Forms.Form frm)
        //{
        //    // Get the Width and Height of the form
        //    int frm_width = frm.Width;
        //    int frm_height = frm.Height;

        //    //Get the Width and Height (resolution) 
        //    //     of the screen
        //    System.Windows.Forms.Screen src = System.Windows.Forms.Screen.PrimaryScreen;
        //    int src_height = src.Bounds.Height;
        //    int src_width = src.Bounds.Width;

        //    //put the form in the center
        //    frm.Left = (src_width - frm_width) / 2;
        //    frm.Top = (src_height - frm_height) / 2;
        //}

        public static char GetDatabaseType()
        {
            //if (ConfigurationManager.AppSettings["DATABASETYPE"] == "MSACCESS")
            //    return 'M';
            //else if (ConfigurationManager.AppSettings["DATABASETYPE"] == "SQLSERVER")
            //    return 'S';
            //return 'M';
            return 'S';
        }

        public static long GetTotalRecords(string TableName, string FieldName, string FieldValue, bool FieldIsInteger)
        {
            objDB = new clsDatabaseManager();
            return objDB.GetCount(TableName, FieldName, FieldValue, FieldIsInteger);
        }

        public static bool IsRecordExist(string TableName, string WhereClause)
        {
            objDB = new clsDatabaseManager();
            return objDB.IsRecordExists(TableName, WhereClause);
        }

        public static long GetMaximumValue(String TableName, String FieldName, bool FieldIsInteger)
        {
            objDB = new clsDatabaseManager();
            return objDB.GetMax(TableName, FieldName, false);
        }

        public static DateTime GetDate()
        {

            string str = "";

            if (DateTime.Compare(CurrentDate, DateTime.MinValue) == 0)
            {
                objDB = new clsDatabaseManager();
                if (GetDatabaseType() == 'M')
                    str = objDB.GetColumnValue("select Date()");
                else if (GetDatabaseType() == 'S')
                    str = objDB.GetColumnValue("select GetDate()");
                CurrentDate = Convert.ToDateTime(str);
            }
            return CurrentDate.Date;
        }

        public static string GetGender(GenderType gType)
        {
            string Gender = "";
            switch (gType)
            {
                case GenFunctions.GenderType.Male:
                    Gender = "M";
                    break;
                case GenFunctions.GenderType.Female:
                    Gender = "F";
                    break;
                default:
                    break;
            }
            return Gender;
        }

        public static string GetMailSolution()
        {
            string str;
            str = " select MailSolution from tblSystemMaster ";
            objDB = new clsDatabaseManager();
            return objDB.GetColumnValue(str);
        }

        public static int DateDiff(DateTime FirstDate, DateTime SecondDate)
        {
            //1 = First Date is greater than Second Date
            //0 = Both dates are equal;
            //-1= First Date is lesser than Second Date
            int Diff;
            Diff = (DateTime.Compare(FirstDate, SecondDate));
            return Diff;
        }

        public static int GetLastDay(DateTime Date)
        {
            switch (Date.Month)
            {
                case 1:
                    return 31;
                case 2:
                    if (DateTime.IsLeapYear(Date.Year) == true)
                        return 29;
                    else
                        return 28;

                case 3:
                    return 31;

                case 4:
                    return 30;

                case 5:
                    return 31;

                case 6:
                    return 30;

                case 7:
                    return 31;

                case 8:
                    return 31;

                case 9:
                    return 30;

                case 10:
                    return 31;

                case 11:
                    return 30;

                case 12:
                    return 31;

                default:
                    return 30;

            }
        }

        public static string RemoveTimePart(DateTime Date)
        {
            string datetime;
            datetime = Date.ToString();
            datetime = datetime.Remove(datetime.Length - 11).Trim();
            return datetime;
        }

        //public static void ClearTextBoxes(System.Windows.Forms.Control.ControlCollection controls)
        //{
        //    //frm.Control.ControlCollection controls
        //    foreach (System.Windows.Forms.Control ctl in controls)
        //    {
        //        if (ctl.GetType().ToString() == "System.Windows.Forms.TextBox")
        //        {
        //            ctl.Text = String.Empty;
        //        }

        //        if (ctl.GetType().ToString() == "System.Windows.Forms.ComboBox")
        //        {
        //            ctl.Text = String.Empty;
        //        }
        //        if (ctl.GetType().ToString() == "System.Windows.Forms.DateTimePicker")
        //        {
        //            ctl.Text = GetDate().ToString();
        //        }

        //        if (ctl.Controls.Count > 1)
        //        {
        //            ClearTextBoxes(ctl.Controls);
        //        }
        //    }
        //}

        public static string GetMainFolderPath(string CurrentPath)
        {
            return StringFunctions.Left(CurrentPath, (CurrentPath.Length - Convert.ToString("\\bin\\debug").Length));
        }

        public static string GetAppPath()
        {
            //return Application.StartupPath;
            return "";
        }

        public static string GetMarketName()
        {
            return GetSystemParameterValue("MarketName");
        }

        public static string GetSystemParameterValue(string FieldName)
        {
            string strQuery;
            strQuery = "select " + FieldName + " from tblSystemMaster";
            return objDB.GetColumnValue(strQuery);
        }

        public static void LogError(string ModuleName, string ErrorString)
        {
            //string AppPath;
            //FileStream fs;
            //StreamWriter sw;
            //AppPath = @Application.StartupPath;
            //AppPath += @"\Log\ChildCare.log";
            ////XMLFilePath = @Application.StartupPath;
            ////XMLFilePath += @"\XML\DatabaseConfig.xml";
            //try
            //{
            //    MessageBox.Show(ErrorString);
            //    fs = new FileStream(AppPath, FileMode.Append, FileAccess.Write);
            //    sw = new StreamWriter(fs);
            //    sw.WriteLine("At " + DateTime.Now.ToString() + " Error: " + ErrorString + " in " + ModuleName);
            //    sw.Close();
            //    fs.Close();
            //}
            //catch (Exception exp)
            //{
            //    Console.WriteLine("Error in writing in File" + exp.Message);
            //}
        }

        public static bool DeleteFileontheDisk(string FilePath)
        {
            //string AppPath;            
            //FileInfo f;
            //try
            //{
            //    if (IsFileExists(FilePath) == true)
            //    {
            //        f = new FileInfo(FilePath);
            //        f.Delete();
            //    }
            //    return true;
            //}
            //catch (Exception exp)
            //{
            //    LogError("GenFunctions.DeleteFileontheDisk", exp.Message);
            //    return false;
            //}
            return true;
        }
        //Changing the Color of Radio Button
        public static void ChangeOptionColor(object s)
        {
            //RadioButton objRadio = (RadioButton)s;
            ////objTip = new ToolTip();
            ////objTip.UseAnimation = true;
            ////objTip.UseFading = true;
            //if (objRadio.Checked == true)
            //{
            //    objRadio.ForeColor = Color.Blue;
            //    // objTip.SetToolTip(objRadio, "Press Ctrl and click on me to deselect me.");
            //}
            //if (objRadio.Checked == false)
            //{
            //    objRadio.ForeColor = Color.Black;
            //    //objTip.SetToolTip(objRadio, "Click to select me.");
            //}
        }

        //Deselecting the Radio Button
        public static void DeselectOption(object s, bool ControlKey)
        {
            //RadioButton objRadio = (RadioButton)s;
            //if (ControlKey == true)
            //{
            //    if (objRadio.Checked == true)
            //        objRadio.Checked = false;
            //}
        }

        public static bool IsFileExists(string FilePath)
        {
            //FilePath is the path of the file and the file name
            //return File.Exists(FilePath);
            return true;
        }

        public static bool IsDate(string DateText)
        {
            DateTime dt;
            try
            {
                dt = DateTime.Parse(DateText);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static string GetNumberSuperScript(int Number)
        {
            string value;
            if (Number >= 11 && Number <= 20)
            {
                switch (Number)
                {
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                    case 17:
                    case 18:
                    case 19:
                    case 20:
                        value = "th";
                        return value;

                }
            }
            value = Number.ToString();
            value = value.Substring(value.Length - 1, 1);
            switch (value)
            {
                case "0":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    value = "th";
                    break;
                case "1":
                    value = "st";
                    break;
                case "2":
                    value = "nd";
                    break;
                case "3":
                    value = "rd";
                    break;
                default:
                    break;
            }
            return value;
        }

        public static string ConvertmmmTOmm(DateTime DateTobeConverted)
        {
            string dt;
            string NewDate = "";
            dt = DateTobeConverted.Day.ToString();
            if (dt.Length == 1)
                NewDate = "0";

            NewDate += DateTobeConverted.Day.ToString() + "-";

            dt = "";
            dt = DateTobeConverted.Month.ToString();
            if (dt.Length == 1)
                NewDate = NewDate + "0";
            NewDate = NewDate + DateTobeConverted.Month.ToString() + "-";
            dt = "";
            NewDate = NewDate + DateTobeConverted.Year.ToString();
            return NewDate;
        }

        public static int TrytoMakeInt(string ValueToBeChanged)
        {
            int result;
            try
            {
                Int32.TryParse(ValueToBeChanged, out result);
            }
            catch (Exception E)
            {
                Console.Write(Convert.ToString(E));
                result = 0;
            }
            return result;
        }

        //public static void ConvertToByte(string FilePath, out Byte[] b)
        //{
        //    //FileStream fs;
        //    //fs = new System.IO.FileStream(FilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
        //    //b = new Byte[fs.Length];
        //    //fs.Read(b, 0, b.Length);
        //    //fs.Close();
        //}
    }

    public class StringFunctions
    {
        public static string Left(string param, int length)
        {
            //we start at 0 since we want to get the characters starting from the
            //left and with the specified lenght and assign it to a variable
            string result = param.Substring(0, length);
            //return the result of the operation
            return result;
        }
        public static string Right(string param, int length)
        {
            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }
        public static string Mid(string param, int startIndex, int length)
        {
            //start at the specified index in the string ang get N number of
            //characters depending on the lenght and assign it to a variable
            string result = param.Substring(startIndex, length);
            //return the result of the operation
            return result;
        }
        public static string Mid(string param, int startIndex)
        {
            //start at the specified index and return all characters after it
            //and assign it to a variable
            string result = param.Substring(startIndex);
            //return the result of the operation
            return result;
        }
    }

}