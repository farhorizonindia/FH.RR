// JScript File
function validateSelection()    {
    if(validateBookingDates() == false)     {
        return false;
    }
    if(validateInputBeforeGettingWaitListedBookings() == false) {
        return false;    
    }
    doPostBackShowWaitListedBookings();
    return true;
}

function validateInputBeforeGettingWaitListedBookings()     {
    var theForm = document.forms['form1'];
    if (!theForm) 
        theForm = document.form1;  
        
    if(theForm.txtStartDate.value == "")    {            
        alert("Please Enter Check In Date");
        theForm.txtStartDate.focus();
        return false;
    }                
    if(theForm.txtEndDate.value == "")      {            
        alert("Please Enter Check Out Date");
        theForm.txtEndDate.focus();
        return false;
    }               
    if(theForm.ddlAccomodation.selectedIndex == 0)     {
        alert("Please Choose The Accomodation");
        theForm.ddlAccomType.focus();
        return false;
    }    
    return true;
 }

function validateBookingDates()     {        
    var theForm = document.forms['form1'];
    if (!theForm)   { 
        theForm = document.form1;  
    }            
    if(theForm.txtStartDate.value != "" && theForm.txtEndDate.value != "")      {            
        if(isDate(theForm.txtStartDate.value) == false)     {
            alert("Invalid check-in date. The correct format is 'dd-mm-yyyy'.");
            theForm.txtStartDate.focus();
            return false;
        }                   
        if(isDate(theForm.txtEndDate.value) == false)       {
            alert("Invalid check-out date. The correct format is 'dd-mm-yyyy'.");
            theForm.txtEndDate.focus();
            return false;
        }                
        daysdif  = CalculateDateDifference(theForm.txtStartDate.value, theForm.txtEndDate.value);                
        if(daysdif < 0)     {
            alert("Check-In date cannot be greater than Check-out date");
            theForm.txtStartDate.focus();
            return false;
        }        
    }
    return true;
}

function validateRoomAllocation(bookingId)   {
    var RoomCategory, RoomType, selectedRooms, maxRoomsAllowed = 0;
    var theForm = document.forms['form1'];
    if (!theForm) 
        theForm = document.form1;  
    
    for(i=0; i < theForm.elements.length; i++)    {
        el = theForm.elements[i];
        if(el.type == "hidden")    {
            id = el.id;
            splitId = null;
            splitId = id.split('*');
            
            if(bookingId == splitId[1])    {
                RoomCategory = splitId[2];
                RoomType = splitId[3];
                maxRoomsAllowed = el.value;
                selectedRooms = getSelectedRoomsCount(bookingId, RoomCategory, RoomType);
//                if(selectedRooms == 0)   {
//                   alert("Please select at least one room to allocate to the waitlisted booking.");
//                   return false;
//                }
                if(selectedRooms > maxRoomsAllowed)     {
                    alert("Category: " + RoomCategory + "\n" + "Type: " + RoomType + "\nYou cannot allocate more than " + maxRoomsAllowed + " rooms.");
                    return false;
                }
            }
        }
    }       
    return true;
}


function getSelectedRoomsCount(bookingId, RoomCategory, RoomType)   {
    var id = ""; var selectedCheckBox = 0; 
    var theForm = document.forms['form1'];
    if (!theForm) 
        theForm = document.form1;  
    
    for(j=0; j < theForm.elements.length; j++)    {
        el = theForm.elements[j];
        if(el.type=="checkbox")     {
            id = el.id;
            splitId = null;
            splitId = id.split('*');
            if(bookingId == splitId[1] && RoomCategory == splitId[2] && RoomType == splitId[3])    {
                if(el.checked == true)  {
                    selectedCheckBox++;
                }
            }
        }           
    }
    return selectedCheckBox;
}


function showhide(layer_ref) 
{     
    var el = document.getElementById(layer_ref);
    if(el == 'undefined' || el == null)
        return;
    var displaystate = document.getElementById(layer_ref).style.display;    
    var source = '';
    if (displaystate == 'block') 
    { 
        //source = "/images/icon_summary_plus_On.gif";
        source = getImagesPath() + "icon_summary_plus_On.gif";
        displaystate = 'none'; 
    }    
    else     
    { 
        //source = "/images/icon_summary_minus_On.gif";
        source = getImagesPath() + "icon_summary_minus_On.gif";
        displaystate = 'block'; 
    } 
    if (document.all) 
    { //IS IE 4 or 5 (or 6 beta) 
        eval( "document.all." + layer_ref + ".style.display = displaystate"); 
        el = document.getElementById("img" + layer_ref);
        document.getElementById("img" + layer_ref).src = source;
    } 
    if (document.layers) 
    { //IS NETSCAPE 4 or below 
        document.layers[layer_ref].display = displaystate; 
        document.getElementById("img" + layer_ref).src = source;
    } 
    if (document.getElementById &&!document.all) 
    { 
        hza = document.getElementById(layer_ref); 
        hza.style.display = displaystate;         
        document.getElementById("img" + layer_ref).src = source;
    } 
}


function doPostBackShowWaitListedBookings()    {
        __doPostBack('btnShow','This Gets Waitlisted Bookings');
        return true;
}

/*function fillEndDate()  {
    var theForm = document.forms['form1'];
    if (!theForm) 
        theForm = document.form1;                    
    //if(theForm.txtStartDate.value != "" && isDate(theForm.txtStartDate.value) && (theForm.txtEndDate.value == "" || !isDate(theForm.txtEndDate.value)))  {
    if(theForm.txtStartDate.value != "" && isDate(theForm.txtStartDate.value))  {
        theForm.txtEndDate.value = theForm.txtStartDate.value;
    }
}*/