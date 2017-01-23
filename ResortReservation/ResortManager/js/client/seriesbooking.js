function validateBeforeGeneratingSeries()   {
    if(validateInput()== false)   {
        return false;
    }
    /*if(validateSeriesDate() == false)     {
        return false;
    }*/
    if(validateSelectedRoomTypes() == false)   {
        return false;
    }    
    return true;
}

function validateBeforeSavingSeries()   {
    if(validateInput()== false)   {
        return false;
    }
    if(validateIfInputModified() == false)  {
        return false;
    }
    if(validateSelectedBookings() == false) {
        return false;
    }    
    /*if(validateSeriesDate() == false)     {
        return false;
    }*/
    return true;
}

function validateInput()     {
    var theForm = document.forms['form1'];
    if (!theForm) 
        theForm = document.form1;  
    
    if(theForm.ddlAccomType.selectedIndex == 0)     {
        alert("Please choose the accomodation type.");
        theForm.ddlAccomType.focus();
        return false;
    }
    if(theForm.ddlAccomName.selectedIndex == 0)      {
        alert("Please choose the accomodation name.");
        theForm.ddlAccomName.focus();
        return false;
    }
    if(theForm.txtFirstCheckInDate.value == "")    {            
        alert("Please enter series start date.");
        theForm.txtFirstCheckInDate.focus();
        return false;
    }
    if(theForm.ddlAgent.selectedIndex == 0)      {
        alert("Please choose the agent.");
        theForm.ddlAgent.focus();
        return false;
    }        
    if(theForm.ddlNoOfNights.selectedIndex == 0)     {
        alert("Please choose no. of nights of each booking.");
        theForm.ddlNoOfNights.focus();
        return false;
    }
    if(theForm.ddlGap.selectedIndex == 0)      {
        alert("Please ddlGap the gap between each booking.");
        theForm.ddlGap.focus();
        return false;
    }
    if(theForm.ddlNoOfDeps.selectedIndex == 0)     {
        alert("Please choose the total no. of departures in this series.");
        theForm.ddlNoOfDeps.focus();
        return false;
    }                  
    return true;
}

function validateSeriesDate()   {
    var theForm = document.forms['form1'];
    if (!theForm)   { 
        theForm = document.form1;  
    }            
    if(theForm.txtFirstCheckInDate.value != "" )      {            
        if(isDate(theForm.txtFirstCheckInDate.value) == false)     {
            alert("Invalid first check-in date. The correct format is 'dd-mmm-yyyy'.");
            theForm.txtFirstCheckInDate.focus();
            return false;
        }                                           
        /*daysdif  = CalculateDateDifference(theForm.txtFirstCheckInDate.value, Date());                
        if(daysdif < 0)     {
            alert("First check-in date cannot be greater than today.");
            theForm.txtFirstCheckInDate.focus();
            return false;
        }*/
    }
    return true;    
}

function validateSelectedRoomTypes()    {
    var id = ""; var selectedCheckBox = 0;
    var selected = false; var chosen = false;
    var el, ddl;
    var theForm = document.forms['form1'];
    if (!theForm) 
        theForm = document.form1;  
    
    for(j=0; j < theForm.elements.length; j++)    {
        el = theForm.elements[j];
        if(el.type=="checkbox") {
            if(el.checked == true)  {
                id = el.id;
                if(id.substring(0, 10) == "chkCatType") {                
                    selected = true;                
                    id = id.replace('chkCatType', 'ddlCatType');
                    ddl = document.getElementById(id);
                    if(ddl != null )    {
                        if(ddl.selectedIndex == 0)  {                        
                            alert("Please choose the no. of rooms to book.");
                            ddl.focus();
                            return false;
                        }                    
                    }
                }
            }        
        }
    }           
    if(selected == false)   {
        alert("Please choose at least one type to rooms to generate the series.");
        return false;
    }  
    return true;
}

function validateIfInputModified()   {
    var theForm = document.forms['form1'];
    var id, el;
    if (!theForm) 
        theForm = document.form1;  
    /*if(theForm.hfAccomTypeId.value != theForm.ddlAccomType.value)   {
        alert("You have changed the Accomodation type. \n Please click on generate series to generate the series again.");
        return false;
    } */   
    if(theForm.hfAccomId.value != theForm.ddlAccomName.value)   {
        alert("You have changed the Accomodation. \n Please click on generate series to generate the series again.");
        theForm.btnGenerateSeries.focus();
        return false;
    }
    if(theForm.hfFirstCheckInDate.value != theForm.txtFirstCheckInDate.value)   {
        alert("You have changed the first check in date. \n Please click on generate series to generate the series again.");
        theForm.btnGenerateSeries.focus();
        return false;
    }
    if(theForm.hfNoOfNights.value != theForm.ddlNoOfNights.value)   {
        alert("You have changed the no. of nights. \n Please click on generate series to generate the series again.");
        theForm.btnGenerateSeries.focus();
        return false;
    }
    if(theForm.hfGap.value != theForm.ddlGap.value)     {
        alert("You have changed the gap between bookings. \n Please click on generate series to generate the series again.");
        theForm.btnGenerateSeries.focus();
        return false;
    }
    if(theForm.hfDepartures.value != theForm.ddlNoOfDeps.value)     {
        alert("You have changed the no. of departures. \n Please click on generate series to generate the series again.");
        theForm.btnGenerateSeries.focus();
        return false;
    }
    return true;
}


function setValueToHiddenField(field)   {
    var theForm = document.forms['form1'];
    var id, el;
    if (!theForm) 
        theForm = document.form1;  
    if(field == 'AccomType')    {        
        theForm.hfAccomTypeId.value = theForm.ddlAccomType.value;
    }    
    if(field == 'Accom')    {     
        theForm.hfAccomId.value = theForm.ddlAccomName.value;
    }
    if(field == 'FirstCheckInDate')    {     
        theForm.hfFirstCheckInDate.value = theForm.txtFirstCheckInDate.value;
    }
    if(field == 'NoOfNights')    {     
        theForm.hfNoOfNights.value = theForm.ddlNoOfNights.value;
    }
    if(field == 'Gap')    {     
        theForm.hfGap.value = theForm.ddlGap.value;
    }
    if(field == 'Departures')    {     
        theForm.hfDepartures.value = theForm.ddlNoOfDeps.value;
    }
}


function selectCheckBoxes()     {
    var id = ""; var selectedCheckBox = 0;
    var checkBoxSelected = false;
    var el; 
    var j;
    var theForm = document.forms['form1'];
    if (!theForm) 
        theForm = document.form1;  
        
    el = document.getElementById('divSelect');
    if(el.innerHTML == "Check All")    {
        checkBoxSelected = false;
        el.innerHTML = "Uncheck All";
    }
    else    {
        checkBoxSelected = true;
        el.innerHTML = "Check All";
    }
    for(j=0; j < theForm.elements.length; j++)    {
        el = theForm.elements[j];
        if(el.type == "checkbox") {            
            if(el.id.substring(0, 10) == "chkBooking") {   
                if(checkBoxSelected == false)   {
                    el.checked = true;
                }
                else    {
                    el.checked = false;
                }                
            }            
        }
    }       
}

function validateSelectedBookings()    {
    var id = ""; var selectedCheckBox = 0;
    var CheckInDate; 
    var CheckOutDate;
    var CheckInId;
    var CheckOutId;
    var bookingSelected = false;
    var el; 
    var j;
    var theForm = document.forms['form1'];
    if (!theForm) 
        theForm = document.form1;  
    
    for(j=0; j < theForm.elements.length; j++)    {
        el = theForm.elements[j];
        if(el.type == "checkbox") {
            if(el.checked == true)  {
                id = el.id;
                if(id.substring(0, 10) == "chkBooking") {                
                    CheckInId =  id.replace("chkBooking","txtCheckIndate");
                    CheckOutId = id.replace("chkBooking","txtCheckOutdate");
                    
                    CheckInDate = document.getElementById(CheckInId);
                    CheckOutDate = document.getElementById(CheckOutId);
                    
                    if(isDate(CheckInDate.value) == false)     {
                       alert("Invalid check-in date. The correct format is 'dd-mmm-yyyy'.");
                       CheckInDate.focus();
                       return false;
                    }
                    
                    if(isDate(CheckOutDate.value) == false)     {
                       alert("Invalid check-out date. The correct format is 'dd-mmm-yyyy'.");
                       CheckOutDate.focus();
                       return false;
                    }
                    
                    if(CheckInDate && CheckOutDate) {
                        daysdif  = CalculateDateDifference(CheckInDate.value, CheckOutDate.value);                   
                        if(daysdif < 0 || isNaN(daysdif))     {
                            alert("Check-in date cannot be greater than check out date.");
                            CheckInDate.focus();
                            return false;
                        }       
                    }                    
                    bookingSelected = true;                
                    //break;                    
                }
            }        
        }
    }           
    if(bookingSelected == false)   {
        alert("Please choose at least one booking to save the series.");
        return false;
    }  
    return true;
}

function fillCheckOutDate(startDate, endDate) {
    var elStartDate = $get(startDate);
    var elEndDate = $get(endDate);
    var noOfNights = $get('ddlNoOfNights').value;
    
    if (elStartDate && elStartDate.value != "" && isDate(elStartDate.value)) {
        elEndDate.value = dateAdd('d', noOfNights, GetFormattedDate(elStartDate.value));
    }

    var pnlRegenSeries = $get('pnlRegenSeries');
    if (pnlRegenSeries) {
        pnlRegenSeries.style.display = 'block';
    }

    PageMethods.PostSeriesDateChanges(elStartDate.id, elStartDate.value, elEndDate.id, elEndDate.value);
}
