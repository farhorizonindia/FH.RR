using FarHorizon.Reservations.Bases.BasePages;
using FarHorizon.Reservations.BusinessServices;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Client;
using System;
using System.Collections;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class ChangeRoomPax : ClientBasePage
{
    int count = 0;
    Table tblMaster = null;
    int iBookingId = 0;
    const string CONVERTHEADER = "CONVERTHEADER";

    string sRoomCategory = "", sRoomType = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        AddAttributes();
        if (Request.QueryString["bid"] != null)
            int.TryParse(Request.QueryString["bid"], out iBookingId);
        if (Request.QueryString["roomcat"] != null)
            sRoomCategory = Request.QueryString["roomcat"].ToString();
        if (Request.QueryString["roomtype"] != null)
            sRoomType = Request.QueryString["roomtype"].ToString();

        PrepareRoomChart();
        if (!IsPostBack)
        {
            FindTheControls(pnlChotu);
            //if (string.Compare(Request.Path, "/ResortManager/ClientUI/BookingChangeRoomPax.aspx")==0)
            //SessionHandler"FC"] = Constants.DROPDOWNLIST_ROOMS + sRoomCategory.Trim().Replace(" ", "~") + "*" + sRoomType.Trim().Replace(" ", "~");
        }

        string ctrl = GetPostBackControlID();
        if (ctrl != null)
        {
            if (ctrl == "btnDone")
            {

            }
            else if (ctrl.StartsWith(Constants.DROPDOWNLIST_ROOMS))
            {

            }
        }



    }



    public void FindTheControls(Control parent)
    {

        foreach (var c in parent.Controls)
        {
            if (c is CheckBox)
            {
                CheckBox chk = c as CheckBox;
                if (chk.Checked)
                {
                    count++;

                }


            }
            else if (c is Control)
            {
                Control b = c as Control;
                if (b.Controls.Count > 0)
                {
                    this.FindTheControls(b);
                }
            }



        }
        ViewState["Selected"] = count;


    }


    private void AddAttributes()
    {
        btnDone.Attributes.Add("onclick", "return DoPostBack()");
    }

    private void ddlNoOfRoomsAvailable_SelectedIndexChanged(object sender, EventArgs e)
    {
        //This is the event handler to handle the no. of rooms selected for booking
        DropDownList ddl = null;
        int iRoomsToBeBooked = 0, iRoomsLeftToBeBooked = 0, iRoomsBooked = 0, iPax = 0, iTotalPax = 0;
        string[] sName;
        string sCtrlName = "", sCategory = "", sType = "";
        sCtrlName = GetPostBackControlID();

        ddl = (DropDownList)sender;
        int.TryParse(ddl.SelectedItem.Text, out iRoomsToBeBooked);
        sName = ddl.ID.Split('*');
        if (sName.Length > 0)
            sCategory = sName[1].Trim().Replace("~", " ");
        if (sName.Length > 1)
            sType = sName[2].Trim().Replace("~", " ");

        SetRoomsBookedToObject(tblMaster, sCategory, sType, iBookingId, iRoomsToBeBooked, out iRoomsBooked, out iRoomsLeftToBeBooked, out iPax, out iTotalPax);
        SetRoomsWaitListedToObject(tblMaster, sCategory, sType, iRoomsLeftToBeBooked, iBookingId);

        //SetTotalRoomsBookedLabel(tblMaster, iRoomsBooked, sCategory, sType);
        SetRoomsPax(tblMaster, iTotalPax, sCategory, sType);
        SetRoomsWaitlistedLabel(tblMaster, iRoomsLeftToBeBooked, sCategory, sType);
        //SetRoomsPaxLabel(tblMaster, iPax, sCategory, sType);
        //SetBookingTotalPax(iTotalPax);
    }

    private void SetRoomsBookedToObject(Control ParentControl, string Category, string RoomType, int BookingId, int RoomsToBeBooked, out int RoomsBooked, out int RoomsLeftToBeBooked, out int PaxStaying, out int TotalPaxStaying)
    {
        int iChkSelected = 0;
        string scntrlId = "";
        BookedRooms[] oBR = null;
        SortedList sl = new SortedList();
        SortedList slRoomsAvailable = null;
        int iTotalPax = 0, iRPax = 0, slCounter = 0;

        oBR = GetRoomObjectFromSession();
        if (oBR == null)
        {
            RoomsBooked = 0;
            RoomsLeftToBeBooked = 0;
            PaxStaying = 0;
            TotalPaxStaying = 0;
            return;
        }

        #region Rooms List Management
        //Assigning the roms of this particualr category and type ot the appropriate list
        for (int i = 0; i < oBR.Length; i++)
        {
            //Assigning the rooms of this particualr category and type ot the appropriate list
            if (oBR[i].RoomCategory.Trim().Replace(" ", "~") == Category.Replace(" ", "~") && oBR[i].RoomType.Trim().Replace(" ", "~") == RoomType.Replace(" ", "~"))
            {
                scntrlId = Constants.CHECKBOX_ROOM_NO + Category.Trim().Replace(" ", "~") + "*" + RoomType.Trim().Replace(" ", "~") + "*" + oBR[i].RoomNo.Trim().Replace(" ", "~").ToString();
                SetCheckBoxStatus(ParentControl, scntrlId, false);
                string ddlPax = scntrlId.Replace(Constants.CHECKBOX_ROOM_NO, Constants.DROPDOWNLIST_PAX);
                SetPaxDdlStatus(ParentControl, ddlPax, false);

                if (oBR[i].BookingId == BookingId || oBR[i].BookingId == 0)
                {
                    if (oBR[i].RoomStatus.CompareTo(Constants.BOOKED) == 0 || oBR[i].RoomStatus.CompareTo('\0') == 0 || oBR[i].RoomStatus.CompareTo(Constants.AVAILABLE) == 0)
                    {
                        if (slRoomsAvailable == null)
                            slRoomsAvailable = new SortedList();
                        if (!slRoomsAvailable.ContainsValue(scntrlId))
                        {
                            //slRoomsAvailable.Add(scntrlId, scntrlId);
                            slRoomsAvailable.Add(i, scntrlId);
                        }
                    }
                }
            }
        }
        #endregion Rooms List Management

        #region Checkbox setter
        if (slRoomsAvailable != null)
        {
            int key = -1;
            string val = "";
            int index;
            //On the basis of list setting the value of each check box to either true or false.
            if (RoomsToBeBooked == 0) //If roooms selected are 0 then all the rooms are being freed in the object.
            {
                if (slRoomsAvailable != null && slRoomsAvailable.Count > 0)
                {
                    for (int j = 0; j < slRoomsAvailable.Count; j++)
                    {
                        int.TryParse(slRoomsAvailable.GetKey(slCounter).ToString(), out key);
                        val = slRoomsAvailable[key].ToString();
                        SetCheckBoxStatus(ParentControl, val, false);
                        val = val.Replace(Constants.CHECKBOX_ROOM_NO, Constants.DROPDOWNLIST_PAX);
                        SetPaxDdlStatus(ParentControl, val, false);
                        index = key;
                        oBR[index].RoomStatus = Constants.AVAILABLE;
                        oBR[index].PaxStaying = 0;
                    }
                }
                slRoomsAvailable.Clear();
            }

            #region Rooms_Available
            if (slRoomsAvailable != null && slRoomsAvailable.Count > 0)
            {
                for (slCounter = 0; slCounter < slRoomsAvailable.Count; slCounter++)
                {
                    int.TryParse(slRoomsAvailable.GetKey(slCounter).ToString(), out key);
                    val = slRoomsAvailable[key].ToString();
                    index = key;
                    if (iChkSelected < RoomsToBeBooked)
                    {
                        oBR[index].BookingId = BookingId;
                        oBR[index].RoomStatus = Constants.BOOKED;
                        if (oBR[index].PaxStaying > 0)
                            iRPax = iRPax + oBR[index].PaxStaying;
                        else
                            iRPax = iRPax + oBR[index].DefaultNoOfBeds;
                        SetCheckBoxStatus(ParentControl, val, true);
                        val = val.Replace(Constants.CHECKBOX_ROOM_NO, Constants.DROPDOWNLIST_PAX);
                        SetPaxDdlStatus(ParentControl, val, true);
                        key = -1;
                        val = "";
                        index = 0;
                        iChkSelected++;
                    }
                    else
                    {
                        oBR[index].RoomStatus = Constants.AVAILABLE;
                        oBR[index].PaxStaying = 0;
                        SetCheckBoxStatus(ParentControl, val, false);
                        val = val.Replace(Constants.CHECKBOX_ROOM_NO, Constants.DROPDOWNLIST_PAX);
                        SetCheckBoxStatus(ParentControl, val, false);
                        key = -1;
                        val = "";
                        index = 0;
                    }
                }
                //slRoomsAvailable.Clear();                                        
            }
            #endregion Rooms_Available
        }
        #endregion Checkbox setter

        if (slRoomsAvailable != null)
            slRoomsAvailable.Clear();
        slRoomsAvailable = null;

        RoomsLeftToBeBooked = RoomsToBeBooked - iChkSelected;
        RoomsBooked = iChkSelected;
        PaxStaying = iRPax;
        TotalPaxStaying = iTotalPax + iRPax;
        SetRoomObjectToSession(oBR);
    }

    private void SetRoomsWaitListedToObject(Control ParentControl, string Category, string RoomType, int RoomsTobeWaitListed, int BookingId)
    {
        int iChkSelected = 0;
        string scntrlId = "";
        BookedRooms[] oBR = null;
        SortedList sl = new SortedList();
        SortedList slRoomsAvailable = null, slRoomsBooked_WithThisBooking = null, slRoomsBooked_WithOtherBooking = null;
        int slCounter = 0; //iTotalPax = 0, iRPax = 0, 

        oBR = GetRoomObjectFromSession();
        if (oBR == null)
            return;

        #region Rooms List Management
        //Assigning the roms of this particualr category and type ot the appropriate list
        #region Generating List
        for (int i = 0; i < oBR.Length; i++)
        {
            //Assigning the rooms of this particualr category and type ot the appropriate list
            if (oBR[i].RoomCategory.Trim().Replace(" ", "~") == Category.Replace(" ", "~") && oBR[i].RoomType.Trim().Replace(" ", "~") == RoomType.Replace(" ", "~"))
            {
                scntrlId = Constants.CHECKBOX_ROOM_NO + Category.Trim().Replace(" ", "~") + "*" + RoomType.Trim().Replace(" ", "~") + "*" + oBR[i].RoomNo.Trim().Replace(" ", "~").ToString();
                //SetCheckBoxStatus(ParentControl, scntrlId, false);

                if (oBR[i].BookingId == BookingId)
                {
                    if (oBR[i].RoomStatus.CompareTo(Constants.BOOKED) == 0 || oBR[i].RoomStatus.CompareTo('\0') == 0 || oBR[i].RoomStatus.CompareTo(Constants.AVAILABLE) == 0)
                    {
                        if (slRoomsAvailable == null)
                            slRoomsAvailable = new SortedList();
                        if (!slRoomsAvailable.ContainsValue(scntrlId))
                        {
                            //slRoomsAvailable.Add(scntrlId, scntrlId);
                            slRoomsAvailable.Add(i, scntrlId);
                        }
                    }
                    else if (oBR[i].RoomStatus.CompareTo(Constants.WAITLISTED) == 0)
                    {
                        if (slRoomsBooked_WithThisBooking == null)
                            slRoomsBooked_WithThisBooking = new SortedList();
                        if (!slRoomsBooked_WithThisBooking.ContainsValue(scntrlId))
                            slRoomsBooked_WithThisBooking.Add(i, scntrlId);
                    }
                }
                else if (oBR[i].BookingId != BookingId)
                {
                    if (slRoomsBooked_WithOtherBooking == null)
                        slRoomsBooked_WithOtherBooking = new SortedList();

                    //If this rooms is already booked and available with this Booking 
                    //then do not add to the BookedWithOther list
                    if (slRoomsAvailable != null)
                    {
                        if (!slRoomsAvailable.ContainsValue(scntrlId))
                        {
                            if (!slRoomsBooked_WithOtherBooking.ContainsValue(scntrlId))
                                slRoomsBooked_WithOtherBooking.Add(i, scntrlId);
                        }
                    }
                    //If this rooms is already booked and waitlisted with this Booking 
                    //then do not add to the BookedWithOther list
                    else if (slRoomsBooked_WithThisBooking != null)
                    {
                        if (!slRoomsBooked_WithThisBooking.ContainsValue(scntrlId))
                        {
                            if (!slRoomsBooked_WithOtherBooking.ContainsValue(scntrlId))
                                slRoomsBooked_WithOtherBooking.Add(i, scntrlId);
                        }
                    }
                    else
                    {
                        if (!slRoomsBooked_WithOtherBooking.ContainsValue(scntrlId))
                            slRoomsBooked_WithOtherBooking.Add(i, scntrlId);
                    }

                }
            }
        }
        #endregion Generating List

        #region Releasing all waitlisted rooms if rooms to be waitlisted is ZERO
        if (RoomsTobeWaitListed == 0)
        {
            for (int j = 0; j < oBR.Length; j++)
            {
                if (oBR[j].BookingId == iBookingId && oBR[j].RoomStatus == Constants.WAITLISTED && oBR[j].RoomCategory.Trim().Replace(" ", "~") == sRoomCategory && oBR[j].RoomType.Replace(" ", "~") == sRoomType)
                {
                    oBR[j].BookingId = oBR[j].PrevBookingId;
                    oBR[j].RoomStatus = oBR[j].PrevRoomStatus;
                    oBR[j].PaxStaying = oBR[j].PrevPaxStaying;
                }
            }
        }

        #endregion

        #region List Manager
        ////Remove the room from other list, if the rooms are in the Rooms available list.
        int listindex;
        if (slRoomsAvailable != null)
        {
            for (int i = 0; i < slRoomsAvailable.Count; i++)
            {
                if (slRoomsBooked_WithThisBooking != null)
                {
                    if (slRoomsBooked_WithThisBooking.ContainsValue(slRoomsAvailable.GetByIndex(i)))
                    {
                        listindex = -1;
                        listindex = slRoomsBooked_WithThisBooking.IndexOfValue(slRoomsAvailable.GetByIndex(i));
                        slRoomsBooked_WithThisBooking.RemoveAt(listindex);
                    }
                }
                if (slRoomsBooked_WithOtherBooking != null)
                {
                    if (slRoomsBooked_WithOtherBooking.ContainsValue(slRoomsAvailable.GetByIndex(i)))
                    {
                        listindex = -1;
                        listindex = slRoomsBooked_WithOtherBooking.IndexOfValue(slRoomsAvailable.GetByIndex(i));
                        slRoomsBooked_WithOtherBooking.RemoveAt(listindex);
                    }
                }
            }
        }

        if (slRoomsBooked_WithThisBooking != null)
        {
            for (int i = 0; i < slRoomsBooked_WithThisBooking.Count; i++)
            {
                if (slRoomsBooked_WithOtherBooking != null)
                {
                    if (slRoomsBooked_WithOtherBooking.ContainsValue(slRoomsBooked_WithThisBooking.GetByIndex(i)))
                    {
                        listindex = -1;
                        listindex = slRoomsBooked_WithOtherBooking.IndexOfValue(slRoomsBooked_WithThisBooking.GetByIndex(i));
                        slRoomsBooked_WithOtherBooking.RemoveAt(listindex);
                    }
                }
            }

        }
        #endregion List Manager

        #endregion Rooms List Management

        #region Checkbox setter
        if (slRoomsAvailable != null || slRoomsBooked_WithThisBooking != null || slRoomsBooked_WithOtherBooking != null)
        {
            int index = 0;
            int key = 0;
            string val = "";

            //On the basis of list setting the value of each check box to either true or false.
            #region Rooms_Booked_With_Booking
            if (slRoomsBooked_WithThisBooking != null && slRoomsBooked_WithThisBooking.Count > 0)
            {
                for (slCounter = 0; slCounter < slRoomsBooked_WithThisBooking.Count; slCounter++)
                {
                    if (iChkSelected < RoomsTobeWaitListed)
                    {
                        int.TryParse(slRoomsBooked_WithThisBooking.GetKey(slCounter).ToString(), out key);
                        val = slRoomsBooked_WithThisBooking[key].ToString();
                        SetCheckBoxStatus(ParentControl, val, true);
                        val = val.Replace(Constants.CHECKBOX_ROOM_NO, Constants.DROPDOWNLIST_PAX);
                        SetPaxDdlStatus(ParentControl, val, true);
                        index = key;
                        oBR[index].BookingId = BookingId;
                        oBR[index].RoomStatus = Constants.WAITLISTED;
                        key = -1;
                        val = "";
                        index = 0;
                        iChkSelected++;
                    }
                    else
                    {
                        val = slRoomsBooked_WithThisBooking.GetByIndex(slCounter).ToString();
                        SetCheckBoxStatus(ParentControl, val, false);
                        val = val.Replace(Constants.CHECKBOX_ROOM_NO, Constants.DROPDOWNLIST_PAX);
                        SetPaxDdlStatus(ParentControl, val, false);
                        oBR[index].BookingId = oBR[index].PrevBookingId;
                        oBR[index].RoomStatus = oBR[index].PrevRoomStatus;
                        oBR[index].PaxStaying = oBR[index].PrevPaxStaying;
                    }
                }
                slRoomsBooked_WithThisBooking.Clear();
            }
            #endregion Rooms_Booked_With_Booking

            #region Rooms_Booked_WithOtherBooking
            if (slRoomsBooked_WithOtherBooking != null && slRoomsBooked_WithOtherBooking.Count > 0)
            {
                for (slCounter = 0; slCounter < slRoomsBooked_WithOtherBooking.Count; slCounter++)
                {
                    if (iChkSelected < RoomsTobeWaitListed)
                    {
                        int.TryParse(slRoomsBooked_WithOtherBooking.GetKey(slCounter).ToString(), out key);
                        val = slRoomsBooked_WithOtherBooking[key].ToString();
                        SetCheckBoxStatus(ParentControl, val, true);
                        val = val.Replace(Constants.CHECKBOX_ROOM_NO, Constants.DROPDOWNLIST_PAX);
                        SetPaxDdlStatus(ParentControl, val, true);
                        index = key;
                        oBR[index].BookingId = BookingId;
                        oBR[index].RoomStatus = Constants.WAITLISTED;
                        key = -1;
                        val = "";
                        index = 0;
                        iChkSelected++;
                    }
                    else
                    {
                        val = slRoomsBooked_WithOtherBooking.GetByIndex(slCounter).ToString();
                        SetCheckBoxStatus(ParentControl, val, false);
                        val = val.Replace(Constants.CHECKBOX_ROOM_NO, Constants.DROPDOWNLIST_PAX);
                        SetPaxDdlStatus(ParentControl, val, false);
                    }
                }
                slRoomsBooked_WithOtherBooking.Clear();
            }
            #endregion Rooms_Booked_WithOtherBooking
        }

        #endregion Checkbox setter
        //SetTotalNoOfPerson(iTotalPax);
        SetRoomObjectToSession(oBR);
    }

    private void SetTotalNoOfPerson(int iTotalPax)
    {
        //txtNoOfPersons.Text = string.Empty;
        //txtNoOfPersons.Text = iTotalPax.ToString();
    }

    private void GetAvailableRoomsOfChosenCategoryAndType(string Category, string RoomType, int BookingID, int RoomsToBeBooked, out int RoomsLeftToBeBooked)
    {
        string sInvokingControl = GetPostBackControlID();
        if (sInvokingControl == "btnBookTour")
        {
            RoomsLeftToBeBooked = 0;
            return;
        }
        string sCat = "";
        string sType = "";
        if (sInvokingControl.StartsWith("ddl"))
        {
            string[] splitt = sInvokingControl.Split('*');
            sCat = splitt[1].Replace("~", " ").Trim();
            sType = splitt[2].Replace("~", " ").Trim();
        }
        if (sCat != Category || sType != RoomType)
        {
            RoomsLeftToBeBooked = 0;
            return;
        }
        BookedRooms[] oTotalRooms;
        SortedList sl = new SortedList();
        int iRB = 0;
        oTotalRooms = GetRoomObjectFromSession();
        if (oTotalRooms == null)
        {
            RoomsLeftToBeBooked = 0;
            return;
        }
        for (int i = 0; i < oTotalRooms.Length; i++)
        {
            if (oTotalRooms[i] != null)
            {
                if (oTotalRooms[i].RoomCategory.Trim() == Category.Trim() && oTotalRooms[i].RoomType.Trim() == RoomType.Trim())
                {
                    if (oTotalRooms[i].BookingId != BookingID || oTotalRooms[i].BookingId == 0)
                    {
                        if (iRB < RoomsToBeBooked)
                        {
                            if (oTotalRooms[i].RoomStatus != Constants.BOOKED && oTotalRooms[i].RoomStatus != Constants.WAITLISTED)
                            {
                                if (oTotalRooms[i].Status != "NA")
                                {
                                    oTotalRooms[i].RoomStatus = Constants.BOOKED;
                                    oTotalRooms[i].PaxStaying = oTotalRooms[i].DefaultNoOfBeds;
                                    oTotalRooms[i].BookingId = BookingID;
                                    iRB++;
                                }
                            }
                            else
                            {
                                string sKey = oTotalRooms[i].RoomNo.ToString();
                                string sVal = oTotalRooms[i].RoomStatus.ToString();
                                if (sl.Contains(sKey) == true)
                                    sl.Remove(sKey);
                                sl.Add(sKey, sVal);
                            }
                        }
                    }
                    else if (oTotalRooms[i].BookingId == BookingID)
                    {
                        if (oTotalRooms[i].RoomStatus != Constants.WAITLISTED)
                        {
                            if (RoomsToBeBooked == 0)
                            {
                                oTotalRooms[i].RoomStatus = '\0';
                                oTotalRooms[i].PaxStaying = 0;
                            }
                            else
                            {
                                if (iRB < RoomsToBeBooked)
                                {
                                    oTotalRooms[i].RoomStatus = Constants.BOOKED;
                                    oTotalRooms[i].PaxStaying = oTotalRooms[i].DefaultNoOfBeds;
                                    iRB++;
                                }
                                else
                                {
                                    oTotalRooms[i].RoomStatus = '\0';
                                    oTotalRooms[i].PaxStaying = 0;
                                }
                            }
                        }
                    }
                }
            }
        }
        /*
         * 
         * BY VIJAY:
         * THE BELOW CODE HAS BEEN WRITTEN FOR THE FOLLOWING CONDITION
         * IF I BOOK A BOOKING OF RC = 1 AND RT = 2 FOR 6 ROOMS FOR PARTICLUAR DATES
         * THEN I BOOK A SECOND BOOKING OF THE SAME RC AND RT FOR THE SAME OR LESS NO OF ROOMS FOR THE SAME DATES.
         * AND THEN I CANCEL 3 ROOMS OF THE FIRST BOOKING :
         * WHAT SHOULD HAPPEN IF I UPDATE THE SECOND BOOKING:
         * IT SHOULD BOOK THE ROOMS WHICH WERE RECENTLY RELEASED BY BOOKING NO 1 AND PUT THE REST INTO WAILISTED STATUS.....
         * HOPE U UNDERSTAND THE REASON FOR WRITING THIS CODE
         * 
         */
        if (sl.Count > 0)
        {
            if (iRB == 0)
            {
                int iCounter = 0;
                for (int k = 0; k < oTotalRooms.Length; k++)
                {
                    if (oTotalRooms[k].RoomCategory.Trim() == Category.Trim() && oTotalRooms[k].RoomType.Trim() == RoomType.Trim())
                    {
                        for (int l = 0; l < sl.Count; l++)
                        {
                            string sKey = sl.GetKey(l).ToString();
                            if (oTotalRooms[k].BookingId == BookingID)
                            {
                                if (oTotalRooms[k].RoomNo != sKey)
                                {
                                    iCounter++;
                                }
                            }
                        }
                        if (iCounter == sl.Count)
                        {
                            if (oTotalRooms[k].BookingId == BookingID)
                            {
                                if (iRB < RoomsToBeBooked)
                                {
                                    oTotalRooms[k].RoomStatus = Constants.BOOKED;
                                    oTotalRooms[k].PaxStaying = oTotalRooms[k].DefaultNoOfBeds;
                                    iRB++;
                                    iCounter = 0;
                                }
                            }
                        }
                        iCounter = 0;
                    }
                }
            }
        }
        RoomsLeftToBeBooked = RoomsToBeBooked - iRB;
        iRB = 0;
        SetRoomObjectToSession(oTotalRooms);
    }
    //private void WaitListRooms(string Category, string RoomType, int RoomsTobeWaitlisted, int BookingId)
    //{
    //    BookedRooms[] oTotalRooms;
    //    int iRoomsWaitListed =0;
    //    oTotalRooms = GetRoomObjectFromCache();
    //    if (oTotalRooms == null)
    //        return;
    //    for (int i = 0; i < oTotalRooms.Length; i++)
    //    {
    //        if (oTotalRooms[i].RoomCategory.Trim() == Category.Trim() && oTotalRooms[i].RoomType.Trim() == RoomType.Trim())
    //        {
    //            if (oTotalRooms[i].BookingId != BookingId && oTotalRooms[i].BookingId != 0)
    //            {

    //                if (iRoomsWaitListed < RoomsTobeWaitlisted)
    //                {
    //                    if (oTotalRooms[i].RoomStatus != Constants.WAITLISTED)
    //                        oTotalRooms[i].RoomStatus = Constants.WAITLISTED;
    //                    iRoomsWaitListed++;
    //                }
    //                else
    //                {
    //                    oTotalRooms[i].RoomStatus = '\0';
    //                }
    //            }
    //        }
    //        else
    //            iRoomsWaitListed = 0;
    //    }
    //}
    private void WaitListRooms(string Category, string RoomType, int RoomsTobeWaitlisted, int BookingId)
    {
        BookedRooms[] oTotalRooms;
        int iRoomsWaitListed = 0;
        oTotalRooms = GetRoomObjectFromSession();
        if (oTotalRooms == null)
            return;
        for (int i = 0; i < oTotalRooms.Length; i++)
        {
            if (oTotalRooms[i].RoomCategory.Trim() == Category.Trim() && oTotalRooms[i].RoomType.Trim() == RoomType.Trim())
            {
                if (oTotalRooms[i].BookingId != BookingId && oTotalRooms[i].BookingId != 0)
                {
                    if (iRoomsWaitListed < RoomsTobeWaitlisted)
                    {
                        if (oTotalRooms[i].RoomStatus != Constants.WAITLISTED)
                        {
                            oTotalRooms[i].RoomStatus = Constants.WAITLISTED;
                            iRoomsWaitListed++;
                        }
                    }
                    else
                    {
                        /*
                         * 
                         * THIS SECTION ADDED BY VIJAY .. JUST NOT REMEMBERING Y
                         * 
                         */

                        char cPrevRoomStatus = oTotalRooms[i].RoomStatus;
                        int iPrevPax = oTotalRooms[i].PaxStaying;
                        if (iRoomsWaitListed < RoomsTobeWaitlisted)
                        {
                            oTotalRooms[i].RoomStatus = cPrevRoomStatus;
                            oTotalRooms[i].PaxStaying = iPrevPax;
                        }
                    }
                }
                else if (oTotalRooms[i].BookingId == BookingId)
                {
                    /*
                     * 
                     * THIS SECTION ADDED BY VIJAY TO RETAIN THE NO OF WAITLISTED ROOMS FOR THE CURRENT BOOKING.....
                     * 
                     */

                    char cPrevRoomStatus = oTotalRooms[i].RoomStatus;
                    int iPrevPax = oTotalRooms[i].PaxStaying;
                    if (iRoomsWaitListed < RoomsTobeWaitlisted)
                    {
                        oTotalRooms[i].RoomStatus = cPrevRoomStatus;
                        oTotalRooms[i].PaxStaying = iPrevPax;
                        iRoomsWaitListed++;
                    }
                }
            }
            else
                iRoomsWaitListed = 0;
        }
        /*
         * ADDED BY VIJAY TO STORE THE MODIFICATIONS OF THE OBJECT BACK TO THE CACHE
         */
        SetRoomObjectToSession(oTotalRooms);
    }
    private void Get_Waitlisted_BookedRooms(Table ParentControl, string Category, string RoomType, int iRTBB, out int RoomsToBeBooked, out int WaitListedRooms)
    {
        int iRWL = 0, iRA = 0;
        Control c = null;
        string sCtrlID = "";
        Label lblRoomsAvailable = null;
        sCtrlID = Constants.LABEL_ROOMS_AVAILABLE + Category.Replace(" ", "~") + "*" + RoomType.Replace(" ", "~");
        c = FindControl(ParentControl, sCtrlID);
        if (c != null)
        {
            lblRoomsAvailable = (Label)c;
            if (lblRoomsAvailable != null)
            {
                string sRoomsAvailable = lblRoomsAvailable.Text.Replace("Rooms Available:", "");
                int.TryParse(sRoomsAvailable.Trim(), out iRA);
                if (iRTBB > iRA)
                    iRWL = iRTBB - iRA;
            }
        }
        if (iRTBB > iRA)
            iRTBB = iRA;

        RoomsToBeBooked = iRTBB;
        WaitListedRooms = iRWL;
        //return RoomsToBeBooked;

    }
    protected void btnDone_Click(object sender, EventArgs e)
    {
        FinalizeBookedRoomsandPaxToObject();
        CloseWindow();
    }

    private void PrepareRoomChart()
    {
        PrepareRoomChart(DateTime.MinValue, DateTime.MinValue, 0, false);
    }

    private void PrepareRoomChart(DateTime dtStartDate, DateTime EndDate, int iAccomID, bool PrepareFromDB)
    {
        BookedRooms[] oBookedRooms;

        //if (PrepareFromDB == true)
        //    oBookedRooms = GetAllRooms(dtStartDate, EndDate, iAccomID);
        //else
        //    oBookedRooms = GetAllRooms();

        oBookedRooms = GetRoomObjectFromSession();
        if (oBookedRooms == null)
        {
            oBookedRooms = GetRoomObjectFromParentSession();
            SetRoomObjectToSession(oBookedRooms);
        }

        if (oBookedRooms == null)
            return;

        tblMaster = new Table();
        tblMaster.Attributes.Add("class", "tblmaster");
        tblMaster = PrepareChart(oBookedRooms, sRoomCategory, sRoomType);
        if (tblMaster != null)
            pnlChotu.Controls.Add(tblMaster);

    }
    private BookedRooms[] GetAllRooms(DateTime dtStartDate, DateTime EndDate, int iAccomID)
    {
        //This will Return the Rooms from the Database
        BookedRooms[] oTotalRooms = null;
        BookingServices oBookingManager = null;
        if (dtStartDate == DateTime.MinValue || iAccomID <= 0 || EndDate == DateTime.MinValue)
        {
            //SessionHandler"AllRooms"] = null;
            if (GetRoomObjectFromSession() == null)
                return null;
        }
        else
        {
            oBookingManager = new BookingServices();
            oTotalRooms = oBookingManager.GetAllRooms(dtStartDate, EndDate, iAccomID, iBookingId);
            //SessionHandler"AllRooms"] = oTotalRooms;
            SetRoomObjectToSession(oTotalRooms);
        }
        return oTotalRooms;
    }

    private Table PrepareChart(BookedRooms[] oAllRooms, string Category, string RoomType)
    {
        //int iRoomsAvailable = 0, iRoomsBookedWithThisId = 0, iRBMain = 0, iRAMain = 0;
        int iRoomsAvailable = 0, iRoomsBookedWithThisId = 0, iRoomsWaitListed = 0, iRBMain = 0, iRAMain = 0, iRWLMain = 0;
        int iRoomCounter = 0, iRPax = 0, iRTotalRoomsPerType = 0;
        bool bConvertiableRoomFound = false;
        SortedList slRoomNo = new SortedList(), slRoomsBookedwithThisBooking = new SortedList();

        //string ctrlid="";
        //Control c = null;

        string sPrevRoomCategory = "", sPrevRoomType = "";
        const int TOTAL_CELLS_ALLOWED_PER_ROW = 4;
        if (oAllRooms == null)
        {
            lblErrorMsg.Text = "oAllrooms is null";
            return null;
        }
        Table tblRoomsMain = new Table();
        TableRow trRoomsmain = new TableRow();
        TableRow trRooms = null;
        TableCell tcRoomsMain = new TableCell();
        TableCell tcTraversar = new TableCell();
        TableCell tcRoomsDDL = new TableCell();
        Panel pnlRooms = null;
        Table tRooms = null;
        TableRow trPrev = new TableRow();
        tblRoomsMain.Attributes.Add("class", "tblRoomsMain");

        for (int i = 0; i < oAllRooms.Length; i++)
        {
            if (oAllRooms[i] != null)
            {
                if (oAllRooms[i].RoomCategory.Trim().Replace(" ", "~") == Category && oAllRooms[i].RoomType.Trim().Replace(" ", "~") == RoomType)
                {
                    #region Adding Category
                    if (oAllRooms[i].RoomCategory != sPrevRoomCategory)
                    {
                        if (sPrevRoomCategory != "")
                        {
                            if (trRooms != null)
                            {
                                tRooms.Rows.Add(trRooms);
                                pnlRooms = AddRoomsToPanel(tRooms, sPrevRoomCategory, sPrevRoomType);
                                SetRoomsBooked(tblRoomsMain, iRBMain, sPrevRoomCategory, sPrevRoomType);
                                SetRoomsPax(tblRoomsMain, iRPax, sPrevRoomCategory, sPrevRoomType);
                                SetTotalRooms(tblRoomsMain, iRBMain, iRAMain, sPrevRoomCategory, sPrevRoomType, iRTotalRoomsPerType);
                                SetRoomsWaitlistedLabel(tblRoomsMain, iRWLMain, sPrevRoomCategory, sPrevRoomType);
                                //SetRoomsAvailable(tblRoomsMain, iRBMain, iRAMain, sPrevRoomCategory, sPrevRoomType);
                                SetRoomsAvailable(tblRoomsMain, iRAMain, sPrevRoomCategory, sPrevRoomType);
                                iRPax = 0;
                                iRAMain = 0;
                                iRBMain = 0;
                                iRWLMain = 0;
                                iRTotalRoomsPerType = 0;

                                trRoomsmain = new TableRow();
                                tcRoomsMain = new TableCell();
                                tcRoomsMain.Controls.Add(pnlRooms);
                                trRoomsmain.Cells.Add(tcRoomsMain);
                                tblRoomsMain.Rows.Add(trRoomsmain);
                            }
                        }

                        sPrevRoomCategory = oAllRooms[i].RoomCategory;
                        trRoomsmain = new TableRow();
                        tcRoomsMain = new TableCell();
                        //tcRoomsMain.Controls.Add(AddCategory(sPrevRoomCategory));
                        tcRoomsMain.Controls.Add(AddCategory(sPrevRoomCategory, oAllRooms[i].RoomType));
                        trRoomsmain.Cells.Add(tcRoomsMain);
                        tblRoomsMain.Rows.Add(trRoomsmain);
                        sPrevRoomType = "";

                    }
                    #endregion Adding Category

                    #region Adding RoomType Etc.
                    if (oAllRooms[i].RoomType != sPrevRoomType)
                    {
                        if (sPrevRoomType != "")
                        {
                            if (trRooms != null)
                            {
                                #region Add Rooms
                                tRooms.Rows.Add(trRooms);
                                //tRooms.Rows.Add(AddPopUpUrl(sPrevRoomCategory, sPrevRoomType, _iBookingId));
                                pnlRooms = AddRoomsToPanel(tRooms, sPrevRoomCategory, sPrevRoomType);

                                SetRoomsBooked(tblRoomsMain, iRBMain, sPrevRoomCategory, sPrevRoomType);
                                SetRoomsPax(tblRoomsMain, iRPax, sPrevRoomCategory, sPrevRoomType);
                                SetTotalRooms(tblRoomsMain, iRBMain, iRAMain, sPrevRoomCategory, sPrevRoomType, iRTotalRoomsPerType);
                                SetRoomsWaitlistedLabel(tblRoomsMain, iRWLMain, sPrevRoomCategory, sPrevRoomType);
                                //SetRoomsAvailable(tblRoomsMain, iRBMain, iRAMain, sPrevRoomCategory, sPrevRoomType);
                                SetRoomsAvailable(tblRoomsMain, iRAMain, sPrevRoomCategory, sPrevRoomType);
                                iRPax = 0;
                                iRAMain = 0;
                                iRBMain = 0;
                                iRWLMain = 0;

                                iRTotalRoomsPerType = 0;
                                trRoomsmain = new TableRow();
                                tcRoomsMain = new TableCell();
                                tcRoomsMain.Controls.Add(pnlRooms);
                                trRoomsmain.Cells.Add(tcRoomsMain);
                                tblRoomsMain.Rows.Add(trRoomsmain);
                                #endregion Add Rooms
                            }
                        }

                        sPrevRoomType = oAllRooms[i].RoomType;
                        trRoomsmain = new TableRow();
                        tcRoomsMain = new TableCell();
                        tcRoomsMain.ColumnSpan = 20;
                        tcRoomsMain.Controls.Add(AddRoomType(sPrevRoomCategory, sPrevRoomType));
                        trRoomsmain.Cells.Add(tcRoomsMain);
                        tblRoomsMain.Rows.Add(trRoomsmain);
                        tRooms = new Table();
                        tRooms.Attributes.Add("class", "tRooms");
                        tRooms.Rows.Add(AddHeaderRow(sPrevRoomType));
                        trRooms = new TableRow();
                    }
                    #endregion Adding RoomType Etc.

                    #region Adding Rooms
                    if (iRoomCounter >= TOTAL_CELLS_ALLOWED_PER_ROW)
                    {
                        if (trRooms != null)
                            tRooms.Rows.Add(trRooms);
                        trRooms = new TableRow();
                        iRoomCounter = 0;
                    }
                    //c = null;
                    //ctrlid = "";

                    if (oAllRooms[i].BookingId == iBookingId)
                    {
                        if (!slRoomsBookedwithThisBooking.ContainsValue(oAllRooms[i]))
                            slRoomsBookedwithThisBooking.Add(i, oAllRooms[i]);
                    }
                    if (slRoomNo.ContainsKey(oAllRooms[i].RoomNo) == false)
                    {
                        slRoomNo.Add(oAllRooms[i].RoomNo, oAllRooms[i].RoomNo);
                        trRooms.Cells.Add(AddRoomCheckBox(oAllRooms[i]));
                        iRoomCounter++;
                        trRooms.Cells.Add(AddRoom(oAllRooms[i], iBookingId, out iRoomsBookedWithThisId, out iRoomsAvailable, out iRoomsWaitListed));
                        iRoomCounter++;
                        trPrev = trRooms;

                        iRBMain = iRBMain + iRoomsBookedWithThisId;
                        iRAMain = iRAMain + iRoomsAvailable;
                        iRWLMain = iRWLMain + iRoomsWaitListed;
                        iRTotalRoomsPerType = iRTotalRoomsPerType + 1;

                        tcTraversar = null;
                        tcTraversar = AddRoomPax(oAllRooms[i]);
                        if (tcTraversar != null)
                            trRooms.Cells.Add(tcTraversar);
                        iRoomCounter++;

                        tcTraversar = null;

                        tcTraversar = AddRoomConversion(oAllRooms[i]);
                        if (tcTraversar != null)
                        {
                            trRooms.Cells.Add(tcTraversar);
                            bConvertiableRoomFound = true;
                        }
                        iRoomCounter++;
                    }
                    //trRooms.Visible = SetControlAsPerBookingId(oAllRooms[i].BookingId);

                    #endregion Adding Rooms

                    #region Calculating Total No. Of Pax
                    if (oAllRooms[i].BookingId == 0)
                    {
                        if (oAllRooms[i].PaxStaying > 0)
                        {
                            //iTotalPax = iTotalPax + oAllRooms[i].PaxStaying;
                            iRPax = iRPax + oAllRooms[i].PaxStaying;
                        }
                    }
                    else if (oAllRooms[i].BookingId == iBookingId && oAllRooms[i].BookingId != 0)
                    {
                        if (oAllRooms[i].PaxStaying > 0)
                        {
                            //iTotalPax = iTotalPax + oAllRooms[i].PaxStaying;
                            iRPax = iRPax + oAllRooms[i].PaxStaying;
                        }
                    }
                    #endregion
                }
            }
        }
        #region For Last Row
        if (trRooms != null)
        {
            tRooms.Rows.Add(trRooms);
            //tRooms.Rows.Add(AddPopUpUrl(sPrevRoomCategory, sPrevRoomType, _iBookingId));
            pnlRooms = AddRoomsToPanel(tRooms, sPrevRoomCategory, sPrevRoomType);

            SetRoomsBooked(tblRoomsMain, iRBMain, sPrevRoomCategory, sPrevRoomType);
            SetRoomsPax(tblRoomsMain, iRPax, sPrevRoomCategory, sPrevRoomType);
            SetTotalRooms(tblRoomsMain, iRBMain, iRAMain, sPrevRoomCategory, sPrevRoomType, iRTotalRoomsPerType);
            SetRoomsWaitlistedLabel(tblRoomsMain, iRWLMain, sPrevRoomCategory, sPrevRoomType);
            //SetRoomsAvailable(tblRoomsMain, iRBMain, iRAMain, sPrevRoomCategory, sPrevRoomType);
            SetRoomsAvailable(tblRoomsMain, iRAMain, sPrevRoomCategory, sPrevRoomType);
            iRPax = 0;
            iRAMain = 0;
            iRBMain = 0;
            iRWLMain = 0;
            iRTotalRoomsPerType = 0;
            trRoomsmain = new TableRow();
            tcRoomsMain = new TableCell();
            tcRoomsMain.Controls.Add(pnlRooms);
            trRoomsmain.Cells.Add(tcRoomsMain);
            tblRoomsMain.Rows.Add(trRoomsmain);
        }
        #endregion For Last Row

        #region Overriding Status
        for (int i = 0; i < slRoomsBookedwithThisBooking.Count; i++)
        {
            overRideRoomStatus(tblRoomsMain, (BookedRooms)slRoomsBookedwithThisBooking.GetByIndex(i), out iRoomsAvailable, out iRoomsBookedWithThisId, out iRoomsWaitListed);
        }
        #endregion Overriding Status

        ShowConvertRoomHeader(tblRoomsMain, bConvertiableRoomFound);
        string ctrl = GetPostBackControlID();
        if (ctrl == null || string.Compare(ctrl, "btnDone") != 0)
        {
            SetRoomDropDownIndex();
        }
        return tblRoomsMain;
    }

    private void overRideRoomStatus(Control parentControl, BookedRooms oAllRooms, out int iRoomsAvailable, out int iRoomsBookedWithThisId, out int iRoomsWaitListed)
    {
        string ctrlid = "";
        Control c = null;
        iRoomsAvailable = 0;
        iRoomsBookedWithThisId = 0;
        iRoomsWaitListed = 0;

        #region override Checkbox status
        ctrlid = Constants.CHECKBOX_ROOM_NO + oAllRooms.RoomCategory.Trim().Replace(" ", "~") + "*" + oAllRooms.RoomType.Trim().Replace(" ", "~") + "*" + oAllRooms.RoomNo.Trim().Replace(" ", "~").ToString();
        c = FindControl(parentControl, ctrlid);

        if (c != null)
        {
            CheckBox chk = (CheckBox)c;
            SetCheckBoxStatus(chk, oAllRooms);
            chk.Enabled = AllowOperationsOnRoom(oAllRooms);
        }
        #endregion override Checkbox status

        #region override Room Status
        c = null;
        ctrlid = "";
        ctrlid = Constants.CELL_ROOMNO + oAllRooms.RoomNo;
        c = FindControl(parentControl, ctrlid);
        if (c != null)
        {
            TableCell cell = (TableCell)c;
            SetRoomStatus(cell, oAllRooms, iBookingId, out iRoomsBookedWithThisId, out iRoomsAvailable, out iRoomsWaitListed);
        }
        #endregion override Room Status

        #region override pax dll Status
        c = null;
        ctrlid = "";
        ctrlid = Constants.DROPDOWNLIST_PAX + oAllRooms.RoomCategory.Trim().Replace(" ", "~") + "*" + oAllRooms.RoomType.Trim().Replace(" ", "~") + "*" + oAllRooms.RoomNo.Trim().Replace(" ", "~").ToString();
        c = FindControl(parentControl, ctrlid);
        if (c != null)
        {
            DropDownList ddl = (DropDownList)c;
            ddl.Enabled = AllowOperationsOnRoom(oAllRooms);

            ListItem l = null;
            int iIndex = 0;
            string ctrl = GetPostBackControlID();
            if (ctrl == null || ctrl != "btnDone")
            {
                if (oAllRooms.PaxStaying == 0)
                    l = ddl.Items.FindByValue(oAllRooms.DefaultNoOfBeds.ToString());
                else
                    l = ddl.Items.FindByValue(oAllRooms.PaxStaying.ToString());

                iIndex = ddl.Items.IndexOf(l);
                keepRoomDropDownsSelectedIndex(ddl.ID, iIndex);
            }
        }
        #endregion override pax dll Status
    }

    private void ShowConvertRoomHeader(Table parentTable, bool ShowHeader)
    {
        Control c = null;
        TableCell tc = null;
        c = FindControl(parentTable, CONVERTHEADER);
        if (c != null)
        {
            tc = (TableCell)c;
            tc.Visible = ShowHeader;
        }
    }
    private static Panel AddRoomsToPanel(Table tRooms, string RoomCategory, string RoomType)
    {
        Panel p = new Panel();
        p.ID = Constants.PANEL_ROOMS + RoomCategory.Trim().Replace(" ", "").ToString() + "" + RoomType.Trim().Replace(" ", "").ToString();
        //p.Style[HtmlTextWriterStyle.Display] = "none";
        tRooms.Style[HtmlTextWriterStyle.FontFamily] = "verdana";
        tRooms.Style[HtmlTextWriterStyle.FontSize] = "x-small";
        p.Controls.Add(tRooms);
        return p;
    }
    private Panel AddCategory(string Category, string Type)
    {
        Panel pCat = new Panel();
        Table tCat = new Table();
        TableRow trCat = new TableRow();
        TableCell tcCat = null;
        tCat.Attributes.Add("class", "tblMaster");

        tcCat = new TableCell();
        tcCat.Attributes.Add("class", "tcCat");
        tcCat.Text = "Category:";
        trCat.Cells.Add(tcCat);

        tcCat = new TableCell();
        tcCat.Attributes.Add("class", "tcCat");
        tcCat.Text = Category;
        trCat.Cells.Add(tcCat);
        //tcCat.ColumnSpan = 17;

        tcCat = new TableCell();
        tcCat.Attributes.Add("class", "tcCat");
        tcCat.Text = "Type:";
        trCat.Cells.Add(tcCat);

        tcCat = new TableCell();
        tcCat.Attributes.Add("class", "tcCat");
        tcCat.Text = Type; ;
        trCat.Cells.Add(tcCat);


        tCat.Rows.Add(trCat);
        pCat.Controls.Add(tCat);
        return pCat;
    }
    private Panel AddRoomType(string Category, string RoomType)
    {
        Panel pCat = new Panel();
        Table tCat = new Table();
        TableRow trCat = new TableRow();
        TableCell tcCat = null;
        Label lRoomBooked = null;
        DropDownList ddlNoOfRoomsAvailable;

        tCat.Attributes.Add("class", "tblMaster");

        //tcCat = new TableCell();
        //tcCat.Attributes.Add("class", "tcCat");
        //tcCat.Text = RoomType;
        //tcCat.ColumnSpan = 6;
        //trCat.Cells.Add(tcCat);

        tcCat = new TableCell();
        tcCat.Attributes.Add("class", "tcCat");
        tcCat.Width = 75;
        tcCat.Text = "Add Rooms: ";
        trCat.Cells.Add(tcCat);

        tcCat = new TableCell();
        tcCat.Attributes.Add("class", "tcCat");
        ddlNoOfRoomsAvailable = new DropDownList();
        //ddlNoOfRoomsAvailable.EnableViewState = false;
        ddlNoOfRoomsAvailable.ID = Constants.DROPDOWNLIST_ROOMS + Category.Trim().Replace(" ", "~") + "*" + RoomType.Trim().Replace(" ", "~");
        ddlNoOfRoomsAvailable.Items.Clear();
        ddlNoOfRoomsAvailable.Items.Insert(0, "Choose");
        ddlNoOfRoomsAvailable.Items.Insert(1, "0");
        ddlNoOfRoomsAvailable.Enabled = false;
        ddlNoOfRoomsAvailable.AutoPostBack = true;
        ddlNoOfRoomsAvailable.SelectedIndexChanged += new EventHandler(ddlNoOfRoomsAvailable_SelectedIndexChanged);
        tcCat.Controls.Add(ddlNoOfRoomsAvailable);
        trCat.Cells.Add(tcCat);

        tcCat = new TableCell();
        tcCat.Attributes.Add("class", "tcCat");
        //tcCat.Width = 200;
        lRoomBooked = new Label();
        lRoomBooked.EnableViewState = false;
        lRoomBooked.ID = Constants.LABEL_PAX + Category.Trim().Replace(" ", "~") + "*" + RoomType.Trim().Replace(" ", "~");
        lRoomBooked.Text = "Pax: ";
        tcCat.Controls.Add(lRoomBooked);
        trCat.Cells.Add(tcCat);

        tcCat = new TableCell();
        tcCat.Attributes.Add("class", "tcCat");
        lRoomBooked = new Label();
        lRoomBooked.EnableViewState = false;
        lRoomBooked.ID = Constants.LABEL_ROOMS_WAITLISTED + Category.Trim().Replace(" ", "~") + "*" + RoomType.Trim().Replace(" ", "~");
        lRoomBooked.Text = "Waitlisted Rooms: 0";
        tcCat.Controls.Add(lRoomBooked);
        trCat.Cells.Add(tcCat);

        tcCat = new TableCell();
        tcCat.Attributes.Add("class", "tcCat");
        lRoomBooked = new Label();
        lRoomBooked.EnableViewState = false;
        lRoomBooked.ID = "lbl*" + Category.Trim().Replace(" ", "~") + "*" + RoomType.Trim().Replace(" ", "~");
        lRoomBooked.Text = "Rooms Booked: ";
        tcCat.Controls.Add(lRoomBooked);
        tcCat.Visible = false;
        trCat.Cells.Add(tcCat);




        tcCat = new TableCell();
        tcCat.Attributes.Add("class", "tcCat");
        tcCat.Visible = false;
        lRoomBooked = new Label();
        lRoomBooked.EnableViewState = false;
        lRoomBooked.ID = Constants.LABEL_ROOMS_AVAILABLE + Category.Trim().Replace(" ", "~") + "*" + RoomType.Trim().Replace(" ", "~");
        lRoomBooked.Text = "Rooms Available: ";
        tcCat.Controls.Add(lRoomBooked);
        trCat.Cells.Add(tcCat);


        tCat.Rows.Add(trCat);
        pCat.Controls.Add(tCat);
        return pCat;
    }
    private void SetRoomsWaitlistedLabel(Table tblParent, int Roomswaitlisted, string Category, string RoomType)
    {
        Control ctrl = null;
        Label lbl = null;
        string ctrlID = Constants.LABEL_ROOMS_WAITLISTED + Category.Trim().Replace(" ", "~") + "*" + RoomType.Trim().Replace(" ", "~");

        ctrl = FindControl(tblParent, ctrlID);
        if (ctrl != null)
        {
            lbl = (Label)ctrl;
            lbl.Text = "Waitlisted Rooms : " + Roomswaitlisted.ToString();
        }
    }
    private TableRow AddHeaderRow(string RoomType)
    {
        TableRow tr = new TableRow();
        TableCell tc = new TableCell();
        tc.Attributes.Add("class", "tcRoomHeader");
        tc.Text = "Select";
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Attributes.Add("class", "tcRoomHeader");
        tc.Text = "Room No.";
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Attributes.Add("class", "tcRoomHeader");
        tc.Text = "Pax";
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Attributes.Add("class", "tcRoomHeader");
        tc.ID = CONVERTHEADER;
        //if (string.Compare(RoomType.Trim(), "Double", true) == 0)
        //    tc.Text = "Convert to Twin";
        //else if (string.Compare(RoomType.Trim(), "Twin", true) == 0)
        //    tc.Text = "Convert to Double";
        //else
        tc.Text = "Convert";
        tr.Cells.Add(tc);

        return tr;
    }
    private TableCell AddRoom(BookedRooms oBookedRoom, int BookingId, out int RoomsBooked, out int RoomsAvailable, out int RoomsWaitListed)
    {
        int iRB = 0, iRA = 0, iRW = 0;
        TableCell tcRoom = new TableCell();
        tcRoom.Attributes.Add("class", "tcRoom");
        tcRoom.ID = Constants.CELL_ROOMNO + oBookedRoom.RoomNo;
        tcRoom.Text = oBookedRoom.RoomNo;
        tcRoom = SetRoomStatus(tcRoom, oBookedRoom, BookingId, out iRB, out iRA, out iRW);
        RoomsBooked = iRB;
        RoomsAvailable = iRA;
        RoomsWaitListed = iRW;
        return tcRoom;
    }
    private TableCell AddRoomConversion(BookedRooms oBookedRoom)
    {
        TableCell tcRoom = new TableCell();
        tcRoom.Attributes.Add("class", "tcRoom");
        //Label lbl = new Label();
        DropDownList ddlConversion = null;
        bool bAddConvertor;
        if (oBookedRoom.Convertable == true)
        {
            if (string.Compare(oBookedRoom.RoomType.Trim(), "Double", true) == 0)
            {
                //lbl.Text = "Convert To Twin:";
                //tcRoom.Text = "Convert To Twin";
                bAddConvertor = true;
            }
            else if (string.Compare(oBookedRoom.RoomType.Trim(), "Twin", true) == 0)
            {
                //lbl.Text = "Convert To Double:";
                //tcRoom.Text = "Convert To Double";
                bAddConvertor = true;
            }
            else
            {
                bAddConvertor = false;
            }
            if (bAddConvertor == true)
            {
                ddlConversion = new DropDownList();
                ddlConversion.EnableViewState = false;
                //ddlConversion.ID = "ddlConversion*" + oBookedRoom.RoomCategory.Trim().Replace(" ", "~") + "*" + oBookedRoom.RoomType.Trim().Replace(" ", "~") + "*" + oBookedRoom.RoomNo.Trim().Replace(" ", "~").ToString();
                ddlConversion.ID = Constants.DROPDOWNLIST_ROOMCONVERT + oBookedRoom.RoomCategory.Trim().Replace(" ", "~") + "*" + oBookedRoom.RoomType.Trim().Replace(" ", "~") + "*" + oBookedRoom.RoomNo.Trim().Replace(" ", "~").ToString();
                //ddlConversion.AccessKey = oBookedRoom.BookingId.ToString();
                ddlConversion.Enabled = AllowOperationsOnRoom(oBookedRoom);

                ddlConversion.Items.Insert(0, "Yes");
                ddlConversion.Items.Insert(1, "No");
                if (oBookedRoom.ConvertTo_Double_Twin == true)
                    ddlConversion.SelectedIndex = 0;
                else if (oBookedRoom.ConvertTo_Double_Twin == false)
                    ddlConversion.SelectedIndex = 1;
                //tcRoom.Controls.Add(lbl);
                tcRoom.Controls.Add(ddlConversion);
            }
            else if (bAddConvertor == false)
                tcRoom = null;
        }
        else
            tcRoom = null;
        return tcRoom;
    }

    private TableCell SetRoomStatusOld(TableCell tcRoom, BookedRooms oBookedRoom, int BookingId, out int RoomsBooked, out int RoomsAvailable)
    {
        TableCell tc = null;
        int iRB = 0, iRA = 0;
        tc = tcRoom;
        if (oBookedRoom.BookingId != 0)
        {
            if (oBookedRoom.BookingId == BookingId)
            {
                /*
                 * 
                 * THE BELOW PART HAS BEEN WRITTEN BY VIJAY AFTER VIEWING THAT THE ROOMS OF THE CURRENT BOOKING 
                 * WERE SHOWN IN BLUE COLOR EVEN IF THEY WERE IN 'WAITLISTED' STATUS.
                 * THE ORIGINAL CODE WRITTEN EARLIER HAS BEEN COMMENTED BELOW.
                 * 
                 */
                if (oBookedRoom.RoomStatus == Constants.BOOKED)
                {
                    tc.Style[HtmlTextWriterStyle.Color] = "Blue";
                }
                else if (oBookedRoom.RoomStatus == Constants.WAITLISTED)
                {
                    tc.Style[HtmlTextWriterStyle.Color] = "Red";
                }

                if (oBookedRoom.PaxStaying > 0)
                {
                    iRB++;
                }
                iRA++;


                /*tc.Style[HtmlTextWriterStyle.Color] = "Blue";
                if (oBookedRoom.PaxStaying > 0)
                {
                    iRB++;
                }
                iRA++;*/
            }
            else if (oBookedRoom.BookingId != BookingId)
            {
                tc.Style[HtmlTextWriterStyle.Color] = "Red";
            }
        }
        else if (oBookedRoom.BookingId == 0)
        {
            tc.Style[HtmlTextWriterStyle.Color] = "Green";
            iRA++;
            if (oBookedRoom.PaxStaying > 0)
                iRB++;
        }
        RoomsBooked = iRB;
        RoomsAvailable = iRA;
        return tc;
    }
    private TableCell SetRoomStatus(TableCell tcRoom, BookedRooms oBookedRoom, int BookingId, out int RoomsBooked, out int RoomsAvailable, out int RoomsWaitListed)
    {
        TableCell tc = null;
        int iRB = 0, iRA = 0, iRWL = 0;
        tc = tcRoom;

        if (oBookedRoom.BookingId != 0)
        {
            if (oBookedRoom.BookingId == BookingId)
            {
                //tc.Style[HtmlTextWriterStyle.Color] = "Blue";
                if (oBookedRoom.RoomStatus == Constants.BOOKED)
                {
                    tc.Style[HtmlTextWriterStyle.Color] = "Blue";
                    iRB++;
                }
                else if (oBookedRoom.RoomStatus == Constants.AVAILABLE)
                {
                    tc.Style[HtmlTextWriterStyle.Color] = "Blue";
                }
                else if (oBookedRoom.RoomStatus == Constants.WAITLISTED)
                {
                    tc.Style[HtmlTextWriterStyle.Color] = "Red";
                    iRWL++;
                }
                iRA++;
            }
            else if (oBookedRoom.BookingId != BookingId && oBookedRoom.BookingId != 0)
            {
                tc.Style[HtmlTextWriterStyle.Color] = "Red";
            }
        }
        else if (oBookedRoom.BookingId == 0)
        {
            if (oBookedRoom.RoomStatus == Constants.WAITLISTED)
            {
                tc.Style[HtmlTextWriterStyle.Color] = "Red";
                iRWL++;
            }
            else
            {
                tc.Style[HtmlTextWriterStyle.Color] = "Green";
                iRA++;
                if (oBookedRoom.RoomStatus == Constants.BOOKED)
                    iRB++;
            }
        }
        RoomsBooked = iRB;
        RoomsAvailable = iRA;
        RoomsWaitListed = iRWL;
        return tc;
    }

    private TableCell AddRoomCheckBox(BookedRooms oBookedRoom)
    {
        TableCell tcRoom = new TableCell();
        tcRoom.Attributes.Add("class", "tcRoom");
        CheckBox chkRoomNo = new CheckBox();
        chkRoomNo = new CheckBox();
        chkRoomNo.EnableViewState = false;
        chkRoomNo.ID = Constants.CHECKBOX_ROOM_NO + oBookedRoom.RoomCategory.Trim().Replace(" ", "~") + "*" + oBookedRoom.RoomType.Trim().Replace(" ", "~") + "*" + oBookedRoom.RoomNo.Trim().Replace(" ", "~").ToString();
        chkRoomNo.Enabled = AllowOperationsOnRoom(oBookedRoom);
        //chkRoomNo = SetCheckBoxStatus(chkRoomNo, oBookedRoom.PaxStaying);
        chkRoomNo = SetCheckBoxStatus(chkRoomNo, oBookedRoom);
        tcRoom.Controls.Add(chkRoomNo);
        return tcRoom;
    }

    private TableCell AddRoomPax(BookedRooms oBookedRoom)
    {
        TableCell tcRoom = new TableCell();
        tcRoom.Attributes.Add("class", "tcRoom");
        DropDownList ddlPax = null;

        string ctrl = GetPostBackControlID();

        ddlPax = new DropDownList();
        ddlPax.EnableViewState = false;
        ddlPax.ID = Constants.DROPDOWNLIST_PAX + oBookedRoom.RoomCategory.Trim().Replace(" ", "~") + "*" + oBookedRoom.RoomType.Trim().Replace(" ", "~") + "*" + oBookedRoom.RoomNo.Trim().Replace(" ", "~").ToString();
        //ddlPax.AccessKey = oBookedRoom.BookingId.ToString();
        ddlPax.Enabled = AllowOperationsOnRoom(oBookedRoom);
        for (int i = 1; i <= oBookedRoom.NoOfBeds; i++)
        {
            ddlPax.Items.Insert(i - 1, i.ToString());
        }
        tcRoom.Controls.Add(ddlPax);
        return tcRoom;
    }

    private void keepRoomDropDownsSelectedIndex(string dropDownId, int selectedItemIndex)
    {
        SortedList sl = null;

        if (SessionServices.RetrieveSession(Constants._BookingChangeRoomPax_DdlSelectedIndexes) != null)
        {
            sl = SessionServices.RetrieveSession<SortedList>(Constants._BookingChangeRoomPax_DdlSelectedIndexes);
        }

        if (sl == null)
            sl = new SortedList();

        if (sl.Contains(dropDownId))
            sl[dropDownId] = selectedItemIndex.ToString();
        else
            sl.Add(dropDownId, selectedItemIndex.ToString());

        SessionServices.SaveSession(Constants._BookingChangeRoomPax_DdlSelectedIndexes, sl);
    }

    private void SetRoomDropDownIndex()
    {
        string JSONObjectName = "";
        SortedList sl = null;
        string[,] ddindexes = null;
        BookingServices oBookingManager = new BookingServices();

        if (SessionServices.RetrieveSession(Constants._BookingChangeRoomPax_DdlSelectedIndexes) != null)
        {
            sl = SessionServices.RetrieveSession<SortedList>(Constants._BookingChangeRoomPax_DdlSelectedIndexes);
        }

        if (sl != null && sl.Count > 0)
        {
            ddindexes = new string[sl.Count, 2];
            for (int i = 0; i < sl.Count; i++)
            {
                ddindexes[i, 0] = sl.GetByIndex(i).ToString();
                ddindexes[i, 1] = sl.GetKey(i).ToString();
            }
            ClientScriptManager cs = Page.ClientScript;
            JSONObjectName = base.ConvertObjetToJSON(ddindexes);
            JSONObjectName = JSONObjectName.Replace(@"\", @"\\");
            if (!cs.IsStartupScriptRegistered("RoomDropDownSelectedIndexes"))
                cs.RegisterStartupScript(sl.GetType(), "RoomDropDownSelectedIndexes", "setRoomDropDownSelectedIndexes" + "('" + JSONObjectName + "');", true);
        }
        SessionServices.DeleteSession(Constants._BookingChangeRoomPax_DdlSelectedIndexes);
        oBookingManager = null;
    }

    private bool AllowOperationsOnRoom(BookedRooms BookedRooms)
    {
        bool retval = false;

        if (BookedRooms.RoomStatus == Constants.WAITLISTED)
        {
            retval = true;
            return retval;
        }
        if (BookedRooms.BookingId == 0)
            retval = true;
        else if (BookedRooms.BookingId != 0 && BookedRooms.BookingId == iBookingId)
            retval = true;
        else if (BookedRooms.BookingId != 0 && BookedRooms.BookingId != iBookingId)
            retval = true;
        return retval;
    }
    //private bool ActivatePaxDDLAsPerBookingIdandStatus(BookedRooms BookedRooms)
    //{
    //    bool retval = false;
    //    if (BookedRooms.RoomStatus == Constants.WAITLISTED)
    //    {
    //        retval = false;
    //        return retval;
    //    }
    //    if (BookedRooms.BookingId == 0)
    //        retval = true;
    //    else if (BookedRooms.BookingId != 0 && BookedRooms.BookingId == iBookingId)
    //        retval = true;
    //    else if (BookedRooms.BookingId != 0 && BookedRooms.BookingId != iBookingId)
    //        retval = false;
    //    return retval;
    //}  

    private CheckBox SetCheckBoxStatus(CheckBox chk, BookedRooms BookedRoom)
    {
        CheckBox ckB = null;
        ckB = chk;
        if (BookedRoom.BookingId == iBookingId && iBookingId != 0)
        {
            if (BookedRoom.RoomStatus == Constants.BOOKED || BookedRoom.RoomStatus == Constants.WAITLISTED)
                ckB.Checked = true;
            else
                ckB.Checked = false;
        }
        else if (BookedRoom.BookingId == 0 && iBookingId == 0)
        {
            if (BookedRoom.RoomStatus == Constants.BOOKED || BookedRoom.RoomStatus == Constants.WAITLISTED)
                ckB.Checked = true;
            else
                ckB.Checked = false;
        }
        else
            ckB.Checked = false;
        return ckB;
    }

    private void SetCheckBoxStatus(Control ParentControl, string ControlId, bool value)
    {
        //This Method is called when the dropdown of the rooms available is clicked on the booking screen. 
        //The other one is called through PrepareChart
        Control c = null;
        CheckBox ch = null;
        c = FindControl(ParentControl, ControlId);

        if (c != null)
        {
            if (string.Compare(c.GetType().ToString(), "System.Web.UI.WebControls.CheckBox", true) == 0)
            {
                ch = (CheckBox)c;
                if (ch != null)
                    ch.Checked = value;
            }
        }
    }

    private void SetPaxDdlStatus(Control ParentControl, string ControlId, bool value)
    {
        //This Method is called when the dropdown of the rooms available is clicked on the booking screen. 
        //The other one is called through PrepareChart
        Control c = null;
        DropDownList ddl = null;
        c = FindControl(ParentControl, ControlId);

        if (c != null)
            ddl = (DropDownList)c;
        if (ddl != null)
            ddl.Enabled = value;
    }

    private void SetRoomsBooked(Table tblParent, int RoomsBooked, string Category, string RoomType)
    {
        Control ctrl = null;
        Label lbl = null;
        string ctrlID = "lbl*" + Category.Trim().Replace(" ", "~") + "*" + RoomType.Trim().Replace(" ", "~");
        ctrl = FindControl(tblParent, ctrlID);
        if (ctrl != null)
        {
            lbl = (Label)ctrl;
            lbl.Text = "Rooms Booked: " + RoomsBooked.ToString();
        }
    }
    private void SetRoomsPax(Table tblParent, int Pax, string Category, string RoomType)
    {
        Control ctrl = null;
        Label lbl = null;
        string ctrlID = Constants.LABEL_PAX + Category.Trim().Replace(" ", "~") + "*" + RoomType.Trim().Replace(" ", "~");
        ctrl = FindControl(tblParent, ctrlID);
        if (ctrl != null)
        {
            lbl = (Label)ctrl;
            lbl.Text = "Pax: " + Pax.ToString();
        }
    }
    private void SetTotalRooms(Table tblParent, int RoomsBooked, int RoomsAvailable, string Category, string RoomType, int RoomsPerType)
    {
        //    int TotalRooms = RoomsBooked + RoomsAvailable;
        Control ctrl = null;
        DropDownList ddl = null;
        int iIndex = 0;
        string ctrlID = Constants.DROPDOWNLIST_ROOMS + Category.Trim().Replace(" ", "~") + "*" + RoomType.Trim().Replace(" ", "~");
        ctrl = FindControl(tblParent, ctrlID);
        if (ctrl != null)
        {
            ddl = (DropDownList)ctrl;
            if (ddl != null)
            {
                ddl.Items.Clear();
                ddl.Items.Insert(0, "Choose");
                ddl.Items.Insert(1, "0");
                for (int i = 0; i < RoomsPerType; i++)
                {
                    if ((i + 1) == RoomsBooked)
                        iIndex = i + 2;
                    ddl.Items.Insert(i + 2, Convert.ToString(i + 1));
                }
                ddl.Items[iIndex].Selected = true;
            }
        }
    }
    private void SetRoomsAvailable(Table tblParent, int Roomsavailable, string Category, string RoomType)
    {
        Control ctrl = null;
        Label lbl = null;
        string ctrlID = Constants.LABEL_ROOMS_AVAILABLE + Category.Trim().Replace(" ", "~") + "*" + RoomType.Trim().Replace(" ", "~");
        ctrl = FindControl(tblParent, ctrlID);
        if (ctrl != null)
        {
            lbl = (Label)ctrl;
            lbl.Text = "Rooms Available: " + Roomsavailable.ToString();
        }
    }

    private void FinalizeBookedRoomsandPaxToObject()
    {
        BookedRooms[] oFinalizedRooms = null;
        Control c;
        DropDownList ddl;
        string sCntrlId = string.Empty, Cat, Type, RoomNo;
        SortedList slSelectedRooms = new SortedList();
        int iPax = 0, iRoomsBooked = 0;
        //bool bChangeHappened = false;
        string[] sControls = Request.Form.AllKeys;
        string Id = string.Empty;
        string[] IdSplit = null;
        oFinalizedRooms = (BookedRooms[])GetRoomObjectFromSession();
        if (oFinalizedRooms == null)
            return;

        #region Get Selected Rooms
        for (int i = 0; i < sControls.Length; i++)
        {
            if (sControls[i].StartsWith(Constants.CHECKBOX_ROOM_NO))
            {
                slSelectedRooms.Add(Convert.ToString(sControls[i]), i);
            }
        }
        #endregion







        for (int j = 0; j < oFinalizedRooms.Length; j++)
        {
            if (oFinalizedRooms[j].BookingId == iBookingId && GF.ReplaceSpace(oFinalizedRooms[j].RoomCategory) == sRoomCategory && GF.ReplaceSpace(oFinalizedRooms[j].RoomType) == sRoomType)
            {
                sCntrlId = Constants.CHECKBOX_ROOM_NO + GF.ReplaceSpace(oFinalizedRooms[j].RoomCategory) + "*" + GF.ReplaceSpace(oFinalizedRooms[j].RoomType) + "*" + oFinalizedRooms[j].RoomNo;
                if (slSelectedRooms.ContainsKey(sCntrlId))
                {
                    slSelectedRooms.Remove(sCntrlId);
                    #region Setting the values of selected Rooms to the Object
                    sCntrlId = sCntrlId.Replace(Constants.CHECKBOX_ROOM_NO, Constants.DROPDOWNLIST_PAX);
                    c = null;
                    c = FindControl(pnlChotu, sCntrlId);
                    if (c != null)
                    {
                        #region Set Pax
                        ddl = (DropDownList)c;
                        int.TryParse(ddl.SelectedItem.Text, out iPax);
                        oFinalizedRooms[j].PaxStaying = iPax;
                        iRoomsBooked++;
                        #endregion
                        #region Set the Conversion Status
                        sCntrlId = sCntrlId.Replace(Constants.DROPDOWNLIST_PAX, Constants.DROPDOWNLIST_ROOMCONVERT);
                        c = null;
                        c = FindControl(pnlChotu, sCntrlId);
                        if (c != null)
                        {
                            ddl = (DropDownList)c;
                            if (string.Compare(ddl.SelectedItem.Text, "Yes", true) == 0)
                            {
                                oFinalizedRooms[j].ConvertTo_Double_Twin = true;
                            }
                            else if (string.Compare(ddl.SelectedItem.Text, "No", true) == 0)
                            {
                                oFinalizedRooms[j].ConvertTo_Double_Twin = false;
                            }
                        }
                        #endregion
                    }
                    #endregion
                }
                else if (!slSelectedRooms.ContainsKey(sCntrlId))
                {
                    #region Releasing Previously Booked Rooms, if they are released
                    if (oFinalizedRooms[j].RoomStatus == Constants.BOOKED)
                    {
                        oFinalizedRooms[j].BookingId = 0;
                        oFinalizedRooms[j].RoomStatus = Constants.AVAILABLE;
                        oFinalizedRooms[j].PaxStaying = oFinalizedRooms[j].DefaultNoOfBeds;
                    }
                    #endregion
                }
            }
        }

        #region Get Selected Rooms
        for (int i = 0; i < sControls.Length; i++)
        {
            if (sControls[i].StartsWith(Constants.CHECKBOX_ROOM_NO))
            {
                if (slSelectedRooms.Contains(Convert.ToString(sControls[i])))
                {
                }
                else
                {
                    slSelectedRooms.Add(Convert.ToString(sControls[i]), i);
                }
            }
        }
        #endregion


        if (slSelectedRooms.Count > 0)
        {
            for (int i = 0; i < slSelectedRooms.Count; i++)
            {
                sCntrlId = Convert.ToString(slSelectedRooms.GetKey(i));
                IdSplit = sCntrlId.Split('*');
                Cat = IdSplit[1];
                Type = IdSplit[2];
                RoomNo = IdSplit[3];

                for (int j = 0; j < oFinalizedRooms.Length; j++)
                {
                    if (oFinalizedRooms[j].BookingId == 0 && GF.ReplaceSpace(oFinalizedRooms[j].RoomCategory) == sRoomCategory && GF.ReplaceSpace(oFinalizedRooms[j].RoomType) == sRoomType && oFinalizedRooms[j].RoomNo == RoomNo && oFinalizedRooms[j].RoomStatus == Constants.AVAILABLE)
                    {
                        oFinalizedRooms[j].BookingId = iBookingId;
                        oFinalizedRooms[j].RoomStatus = Constants.BOOKED;
                        #region Setting the values of selected Rooms to the Object
                        sCntrlId = sCntrlId.Replace(Constants.CHECKBOX_ROOM_NO, Constants.DROPDOWNLIST_PAX);
                        c = null;
                        c = FindControl(pnlChotu, sCntrlId);
                        if (c != null)
                        {
                            #region Set Pax
                            ddl = (DropDownList)c;
                            int.TryParse(ddl.SelectedItem.Text, out iPax);
                            oFinalizedRooms[j].PaxStaying = iPax;
                            iRoomsBooked++;
                            #endregion
                            #region Set the Conversion Status
                            sCntrlId = sCntrlId.Replace(Constants.DROPDOWNLIST_PAX, Constants.DROPDOWNLIST_ROOMCONVERT);
                            c = null;
                            c = FindControl(pnlChotu, sCntrlId);
                            if (c != null)
                            {
                                ddl = (DropDownList)c;
                                if (string.Compare(ddl.SelectedItem.Text, "Yes", true) == 0)
                                {
                                    oFinalizedRooms[j].ConvertTo_Double_Twin = true;
                                }
                                else if (string.Compare(ddl.SelectedItem.Text, "No", true) == 0)
                                {
                                    oFinalizedRooms[j].ConvertTo_Double_Twin = false;
                                }
                            }
                            #endregion
                        }
                        #endregion
                        break;
                    }
                }
            }
        }

        #region Old Code
        /* 
        for (int i = 0; i < oFinalizedRooms.Length; i++)
        {
            c = null;
            iPax = 0;
            if (oFinalizedRooms[i].BookingId == iBookingId && oFinalizedRooms[i].RoomCategory.Trim().Replace(" ", "~") == sRoomCategory && oFinalizedRooms[i].RoomType.Replace(" ", "~") == sRoomType)
            {
                sCntrlId = Constants.CHECKBOX_ROOM_NO + oFinalizedRooms[i].RoomCategory.Trim().Replace(" ", "~") + "*" + oFinalizedRooms[i].RoomType.Trim().Replace(" ", "~") + "*" + oFinalizedRooms[i].RoomNo.Trim().Replace(" ", "~").ToString();
                c = FindControl(pnlChotu, sCntrlId);
                if (c != null)
                {
                    ch = (CheckBox)c;
                    if (ch.Checked == true)
                    {
                        sCntrlId = Constants.DROPDOWNLIST_PAX + oFinalizedRooms[i].RoomCategory.Trim().Replace(" ", "~") + "*" + oFinalizedRooms[i].RoomType.Trim().Replace(" ", "~") + "*" + oFinalizedRooms[i].RoomNo.Trim().Replace(" ", "~").ToString();
                        //sCntrlId = "ddlPax*" + oFinalizedRooms[i].RoomCategory.Trim().Replace(" ", "~") + "*" + oFinalizedRooms[i].RoomType.Trim().Replace(" ", "~") + "*" + oFinalizedRooms[i].RoomNo.Trim().Replace(" ", "~").ToString();
                        c = null;
                        c = FindControl(pnlChotu, sCntrlId);
                        if (c != null)
                        {
                            ddl = (DropDownList)c;
                            int.TryParse(ddl.SelectedItem.Text, out iPax);
                            oFinalizedRooms[i].PaxStaying = iPax;
                            iRoomsBooked++;
                            #region Set the Conversion Status
                            sCntrlId = Constants.DROPDOWNLIST_ROOMCONVERT + oFinalizedRooms[i].RoomCategory.Trim().Replace(" ", "~") + "*" + oFinalizedRooms[i].RoomType.Trim().Replace(" ", "~") + "*" + oFinalizedRooms[i].RoomNo.Trim().Replace(" ", "~").ToString();
                            //sCntrlId = "ddlConversion*" + oFinalizedRooms[i].RoomCategory.Trim().Replace(" ", "~") + "*" + oFinalizedRooms[i].RoomType.Trim().Replace(" ", "~") + "*" + oFinalizedRooms[i].RoomNo.Trim().Replace(" ", "~").ToString();
                            c = null;
                            c = FindControl(pnlChotu, sCntrlId);
                            if (c != null)
                            {
                                ddl = (DropDownList)c;
                                if (string.Compare(ddl.SelectedItem.Text, "Yes", true) == 0)
                                {
                                    oFinalizedRooms[i].ConvertTo_Double_Twin = true;
                                }
                                else if (string.Compare(ddl.SelectedItem.Text, "No", true) == 0)
                                {
                                    oFinalizedRooms[i].ConvertTo_Double_Twin = false;
                                }
                            }
                            #endregion
                        }
                    }
                    else if (ch.Checked == false)
                    {
                        oFinalizedRooms[i].PaxStaying = 0;
                        if (oFinalizedRooms[i].PrevBookingId != oFinalizedRooms[i].PrevBookingId)
                        {
                            oFinalizedRooms[i].BookingId = oFinalizedRooms[i].PrevBookingId;
                            oFinalizedRooms[i].RoomStatus = oFinalizedRooms[i].PrevRoomStatus;
                        }
                        else
                        {
                            oFinalizedRooms[i].BookingId = 0;
                        }
                    }
                }
            }
            else if (oFinalizedRooms[i].BookingId == 0)
            {
                sCntrlId = Constants.CHECKBOX_ROOM_NO + oFinalizedRooms[i].RoomCategory.Trim().Replace(" ", "~") + "*" + oFinalizedRooms[i].RoomType.Trim().Replace(" ", "~") + "*" + oFinalizedRooms[i].RoomNo.Trim().Replace(" ", "~").ToString();
                c = FindControl(pnlChotu, sCntrlId);
                if (c != null)
                {
                    ch = (CheckBox)c;
                    if (ch.Checked == true)
                    {
                        //sCntrlId = "ddlPax*" + oFinalizedRooms[i].RoomCategory.Trim().Replace(" ", "~") + "*" + oFinalizedRooms[i].RoomType.Trim().Replace(" ", "~") + "*" + oFinalizedRooms[i].RoomNo.Trim().Replace(" ", "~").ToString();
                        sCntrlId = Constants.DROPDOWNLIST_PAX + oFinalizedRooms[i].RoomCategory.Trim().Replace(" ", "~") + "*" + oFinalizedRooms[i].RoomType.Trim().Replace(" ", "~") + "*" + oFinalizedRooms[i].RoomNo.Trim().Replace(" ", "~").ToString();
                        c = null;
                        c = FindControl(pnlChotu, sCntrlId);
                        if (c != null)
                        {
                            ddl = (DropDownList)c;
                            int.TryParse(ddl.SelectedItem.Text, out iPax);
                            oFinalizedRooms[i].PaxStaying = iPax;
                            iRoomsBooked++;

                            #region Set the Conversion Status
                            //sCntrlId = "ddlConversion*" + oFinalizedRooms[i].RoomCategory.Trim().Replace(" ", "~") + "*" + oFinalizedRooms[i].RoomType.Trim().Replace(" ", "~") + "*" + oFinalizedRooms[i].RoomNo.Trim().Replace(" ", "~").ToString();
                            sCntrlId = Constants.DROPDOWNLIST_ROOMCONVERT + oFinalizedRooms[i].RoomCategory.Trim().Replace(" ", "~") + "*" + oFinalizedRooms[i].RoomType.Trim().Replace(" ", "~") + "*" + oFinalizedRooms[i].RoomNo.Trim().Replace(" ", "~").ToString();
                            c = null;
                            c = FindControl(pnlChotu, sCntrlId);
                            if (c != null)
                            {
                                ddl = (DropDownList)c;
                                if (string.Compare(ddl.SelectedItem.Text, "Yes", true) == 0)
                                {
                                    oFinalizedRooms[i].ConvertTo_Double_Twin = true;
                                }
                                else if (string.Compare(ddl.SelectedItem.Text, "No", true) == 0)
                                {
                                    oFinalizedRooms[i].ConvertTo_Double_Twin = false;
                                }
                            }
                            #endregion
                        }
                    }
                }
            }
        }
        */
        //SessionHandler["AllRooms"] = oFinalizedRooms;
        //bChangeHappened = IsObjectUpdated(oOriginalRooms, oFinalizedRooms);
        //if (bChangeHappened == true)
        //{
        //SetRoomObjectToParentSession(oFinalizedRooms);
        #endregion Old Code
        SetRoomObjectToSession(oFinalizedRooms);
    }

    private bool IsObjectUpdated(BookedRooms[] oOriginalRooms, BookedRooms[] oUpdatedRooms)
    {
        bool bChangeHappened = false;
        for (int i = 0; i < oOriginalRooms.Length; i++)
        {
            if (oOriginalRooms[i].ConvertTo_Double_Twin != oUpdatedRooms[i].ConvertTo_Double_Twin)
            {
                bChangeHappened = true;
                break;
            }
            if (oOriginalRooms[i].PaxStaying != oUpdatedRooms[i].PaxStaying)
            {
                bChangeHappened = true;
                break;
            }
            if (oOriginalRooms[i].RoomStatus != oUpdatedRooms[i].RoomStatus)
            {
                bChangeHappened = true;
                break;
            }
        }
        return bChangeHappened;
    }

    private void BookRooms(Control ParentControl, string Category, string RoomType, int RoomsTobeBooked, int BookingId)
    {
        int iChkSelected = 0;
        string scntrlId = "", sPrevRoomCategory = "", sPrevRoomType = "", sPrevCtrlId = "";
        Control c = null;
        Control x = null;
        CheckBox ch = null;
        BookedRooms[] oBR = null;
        SortedList sl = new SortedList();
        bool bOperatecb;
        int iTotalPax = 0, iRPax = 0;

        oBR = GetRoomObjectFromSession();
        if (oBR == null)
            return;

        for (int i = 0; i < oBR.Length; i++)
        {
            bOperatecb = false;

            #region CHECK THE CHECKBOXES OF AVAILABLE ROOMS
            if (oBR[i].RoomCategory.Trim().Replace(" ", "~") == Category.Replace(" ", "~") && oBR[i].RoomType.Trim().Replace(" ", "~") == RoomType.Replace(" ", "~"))
            {
                if (sPrevRoomCategory != oBR[i].RoomCategory.Trim().Replace(" ", "~") || sPrevRoomType != oBR[i].RoomType.Trim().Replace(" ", "~"))
                {
                    iRPax = 0;
                    sPrevRoomCategory = oBR[i].RoomCategory.Trim().Replace(" ", "~");
                    sPrevRoomType = oBR[i].RoomType.Trim().Replace(" ", "~");
                }

                #region If Block
                //if (oBR[i].BookingId == BookingId && oBR[i].BookingId != 0)
                //{
                //    scntrlId = Constants.CHECKBOX_ROOM_NO + Category.Trim().Replace(" ", "~") + "*" + RoomType.Trim().Replace(" ", "~") + "*" + oBR[i].RoomNo.Trim().Replace(" ", "~").ToString();
                //    bOperatecb = true;
                //}
                //else if (oBR[i].BookingId == 0)
                //{
                //    scntrlId = Constants.CHECKBOX_ROOM_NO + Category.Trim().Replace(" ", "~") + "*" + RoomType.Trim().Replace(" ", "~") + "*" + oBR[i].RoomNo.Trim().Replace(" ", "~").ToString();
                //    bOperatecb = true;
                //}
                //else if (oBR[i].BookingId != BookingId && oBR[i].BookingId != 0)
                //{
                //    scntrlId = Constants.CHECKBOX_ROOM_NO + Category.Trim().Replace(" ", "~") + "*" + RoomType.Trim().Replace(" ", "~") + "*" + oBR[i].RoomNo.Trim().Replace(" ", "~").ToString();
                //    bOperatecb = true;
                //}

                scntrlId = Constants.CHECKBOX_ROOM_NO + Category.Trim().Replace(" ", "~") + "*" + RoomType.Trim().Replace(" ", "~") + "*" + oBR[i].RoomNo.Trim().Replace(" ", "~").ToString();
                bOperatecb = true;

                if (bOperatecb == true)
                {
                    c = FindControl(ParentControl, scntrlId);
                    if (c != null)
                        ch = (CheckBox)c;
                    if (ch != null)
                    {
                        //ch.Checked = false;
                        if (sPrevCtrlId != scntrlId)
                            ch.Checked = false;
                        if (iChkSelected < RoomsTobeBooked)
                        {
                            if (oBR[i].BookingId == BookingId || oBR[i].BookingId == 0)
                            {
                                if (oBR[i].RoomStatus != Constants.WAITLISTED)
                                {
                                    ch.Checked = true;
                                    sPrevCtrlId = scntrlId;
                                    iTotalPax = oBR[i].PaxStaying;
                                    iRPax = iRPax + oBR[i].PaxStaying;
                                    iChkSelected++;
                                }
                                else
                                {
                                    if (sl.Contains(scntrlId))
                                        sl.Remove(scntrlId);
                                    sl.Add(scntrlId, "sarllogics"); // ADDED sarllogics AS VALUE COZ THIS SORTED LIST WILL ALWAYS REMEMBER
                                    // ITS CREATOR ... HA HA HA HA HA (BY VIJAY)
                                }
                            }
                            else
                            {
                                if (oBR[i].RoomStatus == Constants.BOOKED || oBR[i].RoomStatus == Constants.WAITLISTED)
                                {
                                    if (sl.Contains(scntrlId) == false)
                                        sl.Add(scntrlId, 1);
                                }
                            }
                        }
                    }

                    #region Set Labels
                    scntrlId = Constants.LABEL_ROOMS_BOOKED + oBR[i].RoomCategory.Trim().Replace(" ", "~") + "*" + oBR[i].RoomType.Trim().Replace(" ", "~");
                    x = FindControl(ParentControl, scntrlId);
                    if (x != null)
                    {
                        Label lbl = (Label)x;
                        lbl.Text = "Rooms Booked: " + RoomsTobeBooked.ToString();
                    }

                    x = null;
                    scntrlId = Constants.LABEL_PAX + oBR[i].RoomCategory.Trim().Replace(" ", "~") + "*" + oBR[i].RoomType.Trim().Replace(" ", "~");
                    x = FindControl(ParentControl, scntrlId);
                    if (x != null)
                    {
                        Label lbl = (Label)x;
                        lbl.Text = "Pax: " + iRPax.ToString();
                        //iRPax = 0;
                    }
                    #endregion Set Labels
                }
                #endregion If Block
            }
            else if (oBR[i].BookingId == BookingId || oBR[i].BookingId == 0)
            {
                if (sPrevRoomCategory != oBR[i].RoomCategory.Trim().Replace(" ", "~") && sPrevRoomType != oBR[i].RoomType.Trim().Replace(" ", "~"))
                {
                    if (iRPax != 0)
                    {
                        x = null;
                        scntrlId = Constants.LABEL_PAX + sPrevRoomCategory.Trim().Replace(" ", "~") + "*" + sPrevRoomType.Trim().Replace(" ", "~");
                        x = FindControl(ParentControl, scntrlId);
                        if (x != null)
                        {
                            Label lbl = (Label)x;
                            lbl.Text = "Pax: " + iRPax.ToString();
                            iRPax = 0;
                        }
                    }
                    sPrevRoomCategory = oBR[i].RoomCategory.Trim().Replace(" ", "~");
                    sPrevRoomType = oBR[i].RoomType.Trim().Replace(" ", "~");
                }
                #region Calculating Total No. Of Pax
                //if (oBR[i].PaxStaying > 0)
                //{
                //    iRPax = iRPax + oBR[i].PaxStaying;
                //    iTotalPax = iTotalPax + oBR[i].PaxStaying;
                //}

                #endregion
            }
            #endregion CHECK THE CHECKBOXES OF AVAILABLE ROOMS
        }

        #region COMMENTS
        /*
         * 
         * ADDED BY VIJAY:
         * THIS PROCEDURE HAS BEEN MODIFIED TO LET THE CHECKBOXES OF THE AVAILABLE ROOMS TO BE CHECKED FIRST.
         * THE BELOW FEW LINES GET EXECUTED IF THERE ARE ANY CHECKBOXES OF 'BOOKED' OR 'WAITLISTED' ROOMS OF OTHER BOOKINGS,
         * TO BE CHECKED FOR THE CURRENT BOOKING.
         * 
         */

        #endregion
        if (sl.Count > 0)
        {
            #region CHECK THE ALREADY BOOKED CHECKBOXES, IF REQUIRED
            c = null;
            for (int m = 0; m < sl.Count; m++)
            {
                if (iChkSelected < RoomsTobeBooked)
                {
                    string scntrlIDNew = sl.GetKey(m).ToString();
                    string slVal = sl[scntrlIDNew].ToString();
                    if (slVal == "sarllogics")
                    {
                        c = FindControl(ParentControl, scntrlIDNew);
                        if (c != null)
                            ch = (CheckBox)c;
                        if (ch != null)
                        {
                            ch.Checked = true;
                            iChkSelected++;
                        }
                    }

                }
            }
            if (iChkSelected < RoomsTobeBooked)
            {
                for (int n = 0; n < sl.Count; n++)
                {
                    if (iChkSelected < RoomsTobeBooked)
                    {
                        string scntrlIDNew = sl.GetKey(n).ToString();
                        string slVal = sl[scntrlIDNew].ToString();
                        if (slVal == "1")
                        {
                            c = FindControl(ParentControl, scntrlIDNew);
                            if (c != null)
                                ch = (CheckBox)c;
                            if (ch != null)
                            {
                                if (ch.Checked == false)
                                {
                                    ch.Checked = true;
                                    iChkSelected++;
                                }
                            }
                        }
                    }
                }
            }
            #endregion CHECK THE ALREADY BOOKED CHECKBOXES, IF REQUIRED
        }
        //SessionHandler"TotalPax"] = Convert.ToInt32(SessionHandler"TotalPax"]) + iTotalPax;
        SetRoomObjectToSession(oBR);
        //SessionHandler"FC"] = null;
    }

    protected override Control FindControl(Control c, string ID)
    {
        Control cntrl = null;
        if (c != null)
        {
            if (c.HasControls() == false)
            {
                if (c.ID != null)
                {
                    if (c.ID.Replace("ABchkRoomNo", "chkRoomNo") == ID)
                        return c;
                    else
                        return null;
                }
            }
            else if (c.Controls.Count > 0)
            {
                for (int i = 0; i < c.Controls.Count; i++)
                {
                    cntrl = FindControl(c.Controls[i], ID);
                    if (cntrl != null)
                    {
                        break;
                    }
                }
            }
        }
        return cntrl;
    }
    private void CloseWindow()
    {
        StringBuilder sb = new StringBuilder();
        //ArrayList RetArgs = new ArrayList();
        //RetArgs.Add(hfRoomCategory.Value);
        //RetArgs.Add(hfRoomType.Value);       
        //sb.Append("window.returnValue = " + RetArgs);
        //sb.Append("window.opener.RefreshParentPage();");
        //RemoveRoomObjectFromSession();
        sb.Append("window.opener.document.forms[0].submit();");
        sb.Append("window.close();");
        ClientScript.RegisterClientScriptBlock(this.GetType(), "CloseWindowScript", sb.ToString(), true);
    }
    private bool ValidateRooms()
    {
        string[] AllControls;
        Control c;
        DropDownList ddl;
        string sCntrlId;
        int RoomsBooked = 0;
        sCntrlId = Constants.DROPDOWNLIST_ROOMS + sRoomCategory.Trim().Replace(" ", "~") + "*" + sRoomType.Trim().Replace(" ", "~");
        c = FindControl(pnlChotu, sCntrlId);
        if (c != null)
        {
            ddl = (DropDownList)c;
            int.TryParse(ddl.Text, out RoomsBooked);
        }
        AllControls = Request.Form.AllKeys;

        int TotalRoomsBooked = 0;

        for (int i = 0; i < AllControls.Length; i++)
        {
            if (AllControls[i].ToString().StartsWith(Constants.CHECKBOX_ROOM_NO) == true)
            {
                TotalRoomsBooked++;
            }
        }
        if (TotalRoomsBooked != RoomsBooked)
        {
            string msg = "No. of rooms selected and required is different.";
            lblErrorMsg.Text = msg;
            base.DisplayAlert(msg);
            return false;
        }
        return true;
    }

    #region Session Section
    private void SetRoomObjectToSession(BookedRooms[] BookedRooms)
    {
        SessionServices.Booking_AllRoomsDataPAX = BookedRooms;
    }
    private BookedRooms[] GetRoomObjectFromSession()
    {
        return SessionServices.Booking_AllRoomsDataPAX;
    }
    private void RemoveRoomObjectFromSession()
    {
        SessionServices.DeleteSession(Constants._Booking_AllRoomsDataPAX);
    }
    #endregion Session Section

    #region Parent Session
    private BookedRooms[] GetRoomObjectFromParentSession()
    {
        return SessionServices.Booking_AllRoomsData;
    }

    #endregion Parent Session

    protected void btnClose_Click(object sender, EventArgs e)
    {
        CloseWindow();
    }


    private Table PrepareChart(BookedRooms[] oAllRooms)
    {
        int iRoomsAvailable = 0, iRoomsBookedWithThisId = 0, iRoomsWaitListed = 0, iRBMain = 0, iRAMain = 0, iRWL = 0;
        int iRoomCounter = 0, iRTotalRoomsPerType = 0;
        string sPrevRoomCategory = "", sPrevRoomType = "";
        const int TOTAL_CELLS_ALLOWED_PER_ROW = 12;
        string ctrl = GetPostBackControlID();
        if (oAllRooms == null)
        {
            lblErrorMsg.Text = "oAllrooms is null";
            return null;
        }
        Table tblRoomsMain = new Table();
        TableRow trRoomsmain = new TableRow();
        TableRow trRooms = null;
        TableCell tcRoomsMain = new TableCell();
        TableCell tcRoomsDDL = new TableCell();
        Panel pnlRooms = null;
        Table tRooms = null;
        //string sUrlChotu = "";

        tblRoomsMain.Attributes.Add("class", "tblRoomsMain");

        for (int i = 0; i < oAllRooms.Length; i++)
        {
            if (oAllRooms[i] != null)
            {
                #region Adding Category
                if (oAllRooms[i].RoomCategory != sPrevRoomCategory)
                {
                    if (sPrevRoomCategory != "")
                    {
                        if (trRooms != null)
                        {
                            tRooms.Rows.Add(trRooms);
                            pnlRooms = AddRoomsToPanel(tRooms, sPrevRoomCategory, sPrevRoomType);
                            SetRoomsBooked(tblRoomsMain, iRBMain, sPrevRoomCategory, sPrevRoomType);
                            SetTotalRooms(tblRoomsMain, iRBMain, iRAMain, sPrevRoomCategory, sPrevRoomType, iRTotalRoomsPerType);
                            //SetRoomsAvailable(tblRoomsMain, iRBMain, iRAMain, sPrevRoomCategory, sPrevRoomType);
                            SetRoomsAvailable(tblRoomsMain, iRAMain, sPrevRoomCategory, sPrevRoomType);
                            iRAMain = 0;
                            iRBMain = 0;
                            iRTotalRoomsPerType = 0;

                            trRoomsmain = new TableRow();
                            tcRoomsMain = new TableCell();
                            tcRoomsMain.Controls.Add(pnlRooms);
                            trRoomsmain.Cells.Add(tcRoomsMain);
                            tblRoomsMain.Rows.Add(trRoomsmain);
                        }
                    }

                    sPrevRoomCategory = oAllRooms[i].RoomCategory;
                    trRoomsmain = new TableRow();
                    tcRoomsMain = new TableCell();
                    //tcRoomsMain.Controls.Add(AddCategory(sPrevRoomCategory));
                    tcRoomsMain.Controls.Add(AddCategory(sPrevRoomCategory, oAllRooms[i].RoomType));
                    trRoomsmain.Cells.Add(tcRoomsMain);
                    tblRoomsMain.Rows.Add(trRoomsmain);
                    sPrevRoomType = "";

                }
                #endregion Adding Category

                #region Adding RoomType Etc.
                if (oAllRooms[i].RoomType != sPrevRoomType)
                {
                    if (sPrevRoomType != "")
                    {
                        if (trRooms != null)
                        {
                            tRooms.Rows.Add(trRooms);
                            //tRooms.Rows.Add(AddPopUpUrl(sPrevRoomCategory, sPrevRoomType, _iBookingId));
                            pnlRooms = AddRoomsToPanel(tRooms, sPrevRoomCategory, sPrevRoomType);

                            SetRoomsBooked(tblRoomsMain, iRBMain, sPrevRoomCategory, sPrevRoomType);
                            SetTotalRooms(tblRoomsMain, iRBMain, iRAMain, sPrevRoomCategory, sPrevRoomType, iRTotalRoomsPerType);
                            //SetRoomsAvailable(tblRoomsMain, iRBMain, iRAMain, sPrevRoomCategory, sPrevRoomType);
                            SetRoomsAvailable(tblRoomsMain, iRAMain, sPrevRoomCategory, sPrevRoomType);
                            iRAMain = 0;
                            iRBMain = 0;
                            iRTotalRoomsPerType = 0;

                            trRoomsmain = new TableRow();
                            tcRoomsMain = new TableCell();
                            tcRoomsMain.Controls.Add(pnlRooms);
                            trRoomsmain.Cells.Add(tcRoomsMain);
                            tblRoomsMain.Rows.Add(trRoomsmain);
                        }
                    }

                    sPrevRoomType = oAllRooms[i].RoomType;
                    trRoomsmain = new TableRow();
                    tcRoomsMain = new TableCell();
                    tcRoomsMain.ColumnSpan = 20;
                    tcRoomsMain.Controls.Add(AddRoomType(sPrevRoomCategory, sPrevRoomType));
                    trRoomsmain.Cells.Add(tcRoomsMain);
                    tblRoomsMain.Rows.Add(trRoomsmain);
                    tRooms = new Table();
                    trRooms = new TableRow();
                }
                #endregion Adding RoomType Etc.

                #region Adding Rooms
                if (iRoomCounter >= TOTAL_CELLS_ALLOWED_PER_ROW)
                {
                    if (trRooms != null)
                        tRooms.Rows.Add(trRooms);
                    trRooms = new TableRow();
                    iRoomCounter = 0;
                }
                trRooms.Cells.Add(AddRoomCheckBox(oAllRooms[i]));
                iRoomCounter++;
                trRooms.Cells.Add(AddRoom(oAllRooms[i], iBookingId, out iRoomsBookedWithThisId, out iRoomsAvailable, out iRoomsWaitListed));
                iRoomCounter++;
                iRBMain = iRBMain + iRoomsBookedWithThisId;
                iRAMain = iRAMain + iRoomsAvailable;
                iRWL = iRWL + iRoomsWaitListed;
                iRTotalRoomsPerType++;
                trRooms.Cells.Add(AddRoomPax(oAllRooms[i]));
                iRoomCounter++;
                #endregion Adding Rooms
            }
        }
        #region For Last Row
        if (trRooms != null)
        {
            tRooms.Rows.Add(trRooms);
            //tRooms.Rows.Add(AddPopUpUrl(sPrevRoomCategory, sPrevRoomType, _iBookingId));
            pnlRooms = AddRoomsToPanel(tRooms, sPrevRoomCategory, sPrevRoomType);

            SetRoomsBooked(tblRoomsMain, iRBMain, sPrevRoomCategory, sPrevRoomType);
            SetTotalRooms(tblRoomsMain, iRBMain, iRAMain, sPrevRoomCategory, sPrevRoomType, iRTotalRoomsPerType);
            //SetRoomsAvailable(tblRoomsMain, iRBMain, iRAMain, sPrevRoomCategory, sPrevRoomType);
            SetRoomsAvailable(tblRoomsMain, iRAMain, sPrevRoomCategory, sPrevRoomType);
            iRAMain = 0;
            iRBMain = 0;
            iRTotalRoomsPerType = 0;

            trRoomsmain = new TableRow();
            tcRoomsMain = new TableCell();
            tcRoomsMain.Controls.Add(pnlRooms);
            trRoomsmain.Cells.Add(tcRoomsMain);
            tblRoomsMain.Rows.Add(trRoomsmain);
        }
        #endregion For Last Row

        if (ctrl == null || string.Compare(ctrl, "btnDone") != 0)
        {
            SetRoomDropDownIndex();
        }

        return tblRoomsMain;
    }
}

