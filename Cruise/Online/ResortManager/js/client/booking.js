// JScript File
var roomOtherBookings;
var accomodationDetails;
var maintenance;

function validateBeforeGettingRooms() {
    if (validateInputForGettingRooms() == false) {
        return false;
    }
    if (validateBookingDates() == false) {
        return false;
    }
    if (validateAccomodationSeasonDates() == false) {
        return false;
    }
    else {
        doPostBackGetRooms();
    }
    InitializeBookingDates();
    return true;
}

function validateBeforeBookingRooms() {
    if (validateInputForGettingRooms() == false) {
        return false;
    }
    if (validateBookingDates() == false) {
        return false;
    }
    if (validateAccomodationSeasonDates() == false) {
        return false;
    }
    if (validateForm() == false) {
        return false;
    }
    if (isBookingDateChanged() == true) {
        return false;
    }
    else {
        doPostBackBookThisTour();
    }
    return true;
}


function validateInputForGettingRooms() {
    var theForm = document.forms['form1'];
    if (!theForm)
        theForm = document.form1;

    if (theForm.txtStartDate.value == "") {
        alert("Please Enter Check In Date");
        txtStartDate.focus();
        return false;
    }
    if (theForm.txtEndDate.value == "") {
        alert("Please Enter Check Out Date");
        txtEndDate.focus();
        return false;
    }
    if (theForm.ddlAccomType.selectedIndex == 0) {
        alert("Please Choose The Accomodation Type");
        theForm.ddlAccomType.focus();
        return false;
    }
    if (theForm.ddlAccomName.selectedIndex == 0) {
        alert("Please Choose The Accomodation Name");
        theForm.ddlAccomName.focus();
        return false;
    }
    return true;
}


function validateAccomodationSeasonDates() {
    var dd, mm, yyyy;
    var seasonstartdate, seasonenddate, startdate, enddate;
    var accomid = getElement('ddlAccomName').value;

    var txtStartDate = getElement('txtStartDate');

    var start = getElement('txtStartDate').value;
    var end = getElement('txtEndDate').value;
    var datesAreAllowed = false;
    var seasonDatesMsg = '';
    startdate = getDateObject(start, "-");
    enddate = getDateObject(end, "-");

    start = ""; end = "";
    if (accomodationDetails == undefined || accomodationDetails == null) {
        datesAreAllowed = true;
    }
    else if (accomodationDetails != undefined && accomodationDetails != null) {
        for (i = 0; i < accomodationDetails.length; i++) {
            if (accomodationDetails[i].AccomodationId == accomid) {
                yyyy = accomodationDetails[i].SeasonStartDate.substring(0, 4);
                mm = accomodationDetails[i].SeasonStartDate.substring(4, 6);
                mm = mm - 1;
                dd = accomodationDetails[i].SeasonStartDate.substring(6);
                start = dd + "-" + mm + "-" + yyyy;
                seasonstartdate = getDateObject(start, "-");

                yyyy = accomodationDetails[i].SeasonEndDate.substring(0, 4);
                mm = accomodationDetails[i].SeasonEndDate.substring(4, 6);
                mm = mm - 1;
                dd = accomodationDetails[i].SeasonEndDate.substring(6);
                end = dd + "-" + mm + "-" + yyyy;
                seasonenddate = getDateObject(end, "-");

                if (seasonstartdate == undefined || seasonenddate == undefined) {
                    datesAreAllowed = true;
                    break;
                }
                if (startdate >= seasonstartdate && enddate <= seasonenddate) {
                    datesAreAllowed = true;
                    break;
                }
                if (datesAreAllowed == false) {
                    seasonDatesMsg += start + " to " + end + "\n";
                }
            }
        }
        if (datesAreAllowed == false) {
            var finalMsg = "Bookings for these dates are not allowed at this accomodation.";

            if (seasonDatesMsg && seasonDatesMsg != '') {
                seasonDatesMsg = "Season dates are from:\n " + seasonDatesMsg + ".";
                finalMsg += "\n" + seasonDatesMsg;
            }
            alert(finalMsg);
            if (txtStartDate) {
                txtStartDate.focus();
            }
        }
    }
    return datesAreAllowed;
}

function validateForm() {
    var theForm = document.forms['form1'];
    if (!theForm)
        theForm = document.form1;
    if (theForm.txtBookingRef.value == "") {
        alert("Please Enter Your Booking Reference");
        theForm.txtBookingRef.focus();
        return false;
    }
    if (theForm.txtStartDate.value == "") {
        alert("Please Enter Start Date");
        theForm.txtStartDate.focus();
        return false;
    }
    if (theForm.txtEndDate.value == "") {
        alert("Please Enter End Date");
        theForm.txtEndDate.focus();
        return false;
    }
    if (theForm.ddlAccomType.selectedIndex == 0) {
        alert("Please Choose The Accomodation Type");
        theForm.ddlAccomType.focus();
        return false;
    }
    if (theForm.ddlAccomName.selectedIndex == 0) {
        alert("Please Choose The Accomodation Name");
        theForm.ddlAccomName.focus();
        return false;
    }
    if (theForm.ddlAgent.selectedIndex == 0) {
        alert("Please Choose The Agent");
        theForm.ddlAgent.focus();
        return false;
    }
    if (theForm.txtNoOfPersons.value == "") {
        alert("Please Enter The No. Of Pax");
        theForm.txtNoOfPersons.focus();
        return false;
    }
    return true;
}

function validateBookingDates() {
    var theForm = document.forms['form1'];
    if (!theForm) {
        theForm = document.form1;
    }
    if (theForm.txtStartDate.value != "" && theForm.txtEndDate.value != "") {
        if (isDate(theForm.txtStartDate.value) == false) {
            alert("Invalid check-in date. The correct format is 'dd-mmm-yyyy'.");
            txtStartDate.focus();
            return false;
        }
        if (isDate(theForm.txtEndDate.value) == false) {
            alert("Invalid check-out date. The correct format is 'dd-mmm-yyyy'.");
            txtEndDate.focus();
            return false;
        }
        daysdif = CalculateDateDifference(theForm.txtStartDate.value, theForm.txtEndDate.value);
        if (daysdif < 0) {
            alert("Check-In date cannot be greater than Check-out date");
            theForm.txtNoOfNights.value = "";
            txtStartDate.focus();
            return false;
        }
        else {
            theForm.txtNoOfNights.value = daysdif;
        }
    }
    return true;
}


function doPostBackGetRooms() {
    __doPostBack('btnGetAvailableRooms', 'This Gets Available Rooms');
    return true;
}

function doPostBackConfirmTour() {
    __doPostBack('btnConfirmBooking', 'This Confirms booking');
    return true;
}

function doPostBackBookThisTour() {
    __doPostBack('btnBookTour', 'This Books Tour');
    return true;
}

function InitializeBookingDates() {
    var theForm = document.forms['form1'];
    if (!theForm)
        theForm = document.form1;
    theForm.hidStartDate.value = theForm.txtStartDate.value;
    theForm.hidEndDate.value = theForm.txtEndDate.value;
}

function isBookingDateChanged() {
    var theForm = document.forms['form1'];
    if (!theForm)
        theForm = document.form1;
    if (theForm.txtStartDate.value != theForm.hidStartDate.value) {
        alert("Please Click on GetAvailableRooms");
        theForm.btnGetAvailableRooms.focus();
        return true;
    }
    if (theForm.txtEndDate.value != theForm.hidEndDate.value) {
        alert("Please Click on GetAvailableRooms");
        theForm.btnGetAvailableRooms.focus();
        return true;
    }
    return false;
}



//

function showhide(layer_ref) {
    var displaystate = document.getElementById(layer_ref).style.display;
    var source = '';
    if (displaystate == 'block') {
        source = "../images/icon_summary_plus_On.gif";
        displaystate = 'none';
    }
    else {
        source = "../images/icon_summary_minus_On.gif";
        displaystate = 'block';
    }
    if (document.all) { //IS IE 4 or 5 (or 6 beta) 
        eval("document.all." + layer_ref + ".style.display = displaystate");
        el = document.getElementById("img" + layer_ref);
        document.getElementById("img" + layer_ref).src = source;
    }
    if (document.layers) { //IS NETSCAPE 4 or below 
        document.layers[layer_ref].display = displaystate;
        document.getElementById("img" + layer_ref).src = source;
    }
    if (document.getElementById && !document.all) {
        hza = document.getElementById(layer_ref);
        hza.style.display = displaystate;
        document.getElementById("img" + layer_ref).src = source;
    }
}

function setAccomodationDetails(JSONAccomodationDetails) {
    if (JSONAccomodationDetails != null) {
        accomodationDetails = JSON.parse(JSONAccomodationDetails);
        if (!accomodationDetails) {
            alert("JSON decode error");
            return;
        }
    }
}

function setRoomOtherBookings(JSONRoomOtherBookings) {
    if (JSONRoomOtherBookings != null) {
        roomOtherBookings = JSON.parse(JSONRoomOtherBookings);
        if (!roomOtherBookings) {
            alert("JSON decode error");
            return;
        }
    }
}


function setRoommaintenance(JSONRoommaintenance) {
    if (JSONRoommaintenance != null) {
        maintenance = JSON.parse(JSONRoommaintenance);
        if (!maintenance) {
            alert("JSON decode error");
            return;
        }
    }
}

function showRoomMaintenance(roomno, bookingid) {

    setTooltipDataMaintenance(roomno, bookingid);
    showBubbleTooltip('tooltip', this);
}


function showRoomOtherBookings(roomno, bookingid) {

    setTooltipData(roomno, bookingid);
    showBubbleTooltip('tooltip', this);
}

function hideRoomOtherBookings() {
    hideBubbleTooltip('tooltip');
}



function setTooltipDataMaintenance(roomno, bookingid) {
    var detailText = "";
    var found = false;
    var cntr = 0;
    if (maintenance != null) {
        for (var i in maintenance) {
            //  i = cntr;
         
          
         

            if (maintenance[i].Reason != null) {
               
                if (maintenance[i].RoomNo == roomno) {
                    cntr++;
                    detailText = detailText + cntr + ". "
                    found = true;
                    detailText = detailText + " From:" + maintenance[i].StartdtFormatted + " To:" + maintenance[i].EndDtFormatted + "," + maintenance[i].Reason;
                    detailText = detailText + "</br>";
                }
                
            }

        

           // 

        }





        //   }
        if (found == true) {

            document.getElementById('header').innerHTML = "Under Maintenance:";
            document.getElementById('detail').innerHTML = detailText;
        }

        else {

            document.getElementById('header').innerHTML = "No reason Mentioned for maintenance";
            document.getElementById('detail').innerHTML = "";
        }


    }
}



function setTooltipData(roomno, bookingid) {
    var detailText = "";
    var found = false;
    var cntr = 0;
    if (roomOtherBookings != null) {
        for (var i in roomOtherBookings) {
            if (roomOtherBookings[i].BookingId != bookingid) {
                if (roomOtherBookings[i].RoomNo == roomno) {
                    found = true;
                    cntr++;
                    detailText = detailText + cntr + ". "
                    if (roomOtherBookings[i].BookingCode != null) {
                        detailText = detailText + roomOtherBookings[i].BookingCode;
                    }
                    else {
                        detailText = detailText + "-";
                    }
                    if (roomOtherBookings[i].BookingReference != null) {
                        detailText = detailText + ", " + roomOtherBookings[i].BookingReference;
                    }
                    else {
                        detailText = detailText + "-";
                    }
                    if (roomOtherBookings[i].StartDateFormatted != null) {
                        detailText = detailText + ", " + roomOtherBookings[i].StartDateFormatted;
                    }
                    else {
                        detailText = detailText + "-";
                    }
                    if (roomOtherBookings[i].EndDateFormatted != null) {
                        detailText = detailText + ", " + roomOtherBookings[i].EndDateFormatted;
                    }
                    else {
                        detailText = detailText + "-";
                    }
                    detailText = detailText + "</br>";
                }
            }
        }
        if (found == true) {
            document.getElementById('header').innerHTML = "Other bookings of this room are:";
            document.getElementById('detail').innerHTML = detailText;
        }
        else {
            document.getElementById('header').innerHTML = "No other bookings of this room.";
            document.getElementById('detail').innerHTML = "";
        }
    }
}

// JScript File
popped = '';
function showBubbleTooltip(id, e) {
    if (window.event) {
        tempX = event.clientX + document.body.scrollLeft;
        tempY = event.clientY + document.body.scrollTop;
    } else {
        tempX = e.pageX;
        tempY = e.pageY;
    }
    if (tempX == 'undefined') {
        tempX = 500;
    }
    if (tempY == 'undefined') {
        tempY = 500;
    }

    if (popped == "") {
        document.getElementById(id).style.display = 'block';
        document.getElementById(id).style.top = tempY + 'px';
        document.getElementById(id).style.left = tempX + 'px';
        popped = id;
    } else {
        document.getElementById(popped).style.display = 'none';
        document.getElementById(id).style.display = 'block';
        document.getElementById(id).style.top = tempY + 'px';
        document.getElementById(id).style.left = tempX + 'px';
        popped = id;
    }
    return false;
}

function hideBubbleTooltip(id, e) {
    document.getElementById(id).style.display = 'none';
    document.getElementById(id).style.top = '0px';
    document.getElementById(id).style.left = '0px';
}

function getElement(id) {
    var x = document.getElementById(id)
    return x;
}

function setRoomDropDownSelectedIndexes(jsonObject) {
    var dropdownId, selectedIndex;
    if (jsonObject != null) {
        roomDropDownSelectedIndexes = JSON.parse(jsonObject);
        if (!roomDropDownSelectedIndexes) {
            alert("JSON decode error");
            return;
        }
        for (i = 0; i < roomDropDownSelectedIndexes.length; i = i + 2) {
            selectedIndex = roomDropDownSelectedIndexes[i];
            dropdownId = roomDropDownSelectedIndexes[i + 1];
            var x = document.getElementById(dropdownId);
            if (x != null) {
                x.selectedIndex = selectedIndex;
            }
        }
    }
}


/*function fillEndDate()  {
var theForm = document.forms['form1'];
if (!theForm) 
theForm = document.form1;                        
if(theForm.txtStartDate.value != "" && isDate(theForm.txtStartDate.value))  {
theForm.txtEndDate.value = theForm.txtStartDate.value;
DateAdd('d', 1, GetFormattedDate(theForm.txtStartDate.value))
}
}*/
