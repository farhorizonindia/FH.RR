function validateSave() {
    if(validateForm() == false) {
        return false;
    }  
    return true;
}

function validateForm()  {
    var theForm = document.forms['form1'];
    if (!theForm) 
        theForm = document.form1;  
        
    if(theForm.txtAccomName.value == "")    {    
        alert("Please Fill In Accomodation Name");
        theForm.txtAccomName.focus();
        return false;
    }
    if(theForm.txtAccomInitial.value == "")    {
        alert("Please Fill In Accomodation Initial");
        theForm.txtAccomInitial.focus();
        return false;
    }    

    /*if(theForm.txtSeasonStartDate.value == "")    {
       alert("Please enter Season start date.");
       return false;
    }

    if(theForm.txtSeasonEndDate.value == "")    {
       alert("Please enter Season end date.");
        return false;
    } */
              
    if(theForm.ddlAccomTypeId.value == "")  {
        alert("Please select accomodation type.");
    }  
    if(theForm.ddlRegion.value == "")  {
        alert("Please select region.");
    }  
 }


function validateSeasonDates()     {        
    var theForm = document.forms['form1'];
    if (!theForm)   { 
        theForm = document.form1;  
    }
                
    if(theForm.txtSeasonStartDate.value != "")      {            
        if(isDate(theForm.txtSeasonStartDate.value) == false)     {
            alert("Invalid check-in date. The correct format is 'dd-mm-yyyy'.");
            return false;
        }                   
    }
    
    if(theForm.txtSeasonEndDate.value != "")      {            
        if(isDate(theForm.txtSeasonEndDate.value) == false)       {
            alert("Invalid check-out date. The correct format is 'dd-mm-yyyy'.");
            return false;
        }                
    }
    
    if(theForm.txtSeasonStartDate.value != "" && theForm.txtSeasonEndDate.value != "")      {              
        daysdif  = CalculateDateDifference(theForm.txtSeasonStartDate.value, theForm.txtSeasonEndDate.value);                
        if(daysdif < 0)     {
            alert("Season Startdate cannot be greater than Season enddate");                                        
            return false;
        }            
    }
    return true;
}
    
    
function disablebackspace() {
  if (window.focus) window.focus();
  document.onkeydown=catchbackspace;
  document.onkeyup=catchbackspace;
}

function catchbackspace(e) {
  if (!e) e=window.event;
  if (e.keyCode==8){
    return false;
  }
}


/*function fillEndDate()  {
    var theForm = document.forms['form1'];
    if (!theForm) 
        theForm = document.form1;   
    //if(theForm.txtSeasonStartDate.value != "" && isDate(theForm.txtSeasonStartDate.value) && (theForm.txtSeasonEndDate.value == "" || !isDate(theForm.txtSeasonEndDate.value)))  {                     
    if(theForm.txtStartDate.value != "" && isDate(theForm.txtStartDate.value))  {
        theForm.txtSeasonEndDate.value = theForm.txtSeasonStartDate.value;
    }
}*/
