function validateBeforeGettingBookings()   {
    if(validateInputForGettingBookings()== false)   {
        return false;
    }
    if(validateBookingDates() == false)     {
        return false;
    }                
    return true;
}

function validateInputForGettingBookings()  {
    var theForm = document.forms['form1'];
    if (!theForm) 
        theForm = document.form1;  
        
    if(theForm.txtBookingCode.value == "")    {
        if(theForm.txtStartDate.value == "")    {            
            alert("Please enter check-in date or enter booking code.");
            theForm.txtStartDate.focus();
            return false;
        }                
        if(theForm.txtEndDate.value == "")      {            
            alert("Please enter check-out date or enter booking code.");
            theForm.txtEndDate.focus();
            return false;
        }               
    
        if(theForm.ddlAccomType.selectedIndex == 0)     {
            alert("Please choose the accomodation type.");
            theForm.ddlAccomType.focus();
            return false;
        }
        if(theForm.ddlBookingStatusTypes.selectedIndex == 0)      {
            alert("Please choose the booking status");
            theForm.ddlBookingStatusTypes.focus();
            return false;
        }
    }
    
    return true;
 }
 
 function validateBookingDates()    {        
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
            alert("Startdate cannot be greater than enddate");                                                            
            theForm.txtStartDate.focus();
            return false;
        }               
    }
    return true;
}

/*function fillEndDate()  {
    var theForm = document.forms['form1'];
    if (!theForm) 
        theForm = document.form1;    
    if(theForm.txtStartDate.value != "" && isDate(theForm.txtStartDate.value))  {
        theForm.txtEndDate.value = theForm.txtStartDate.value;
    }
}*/