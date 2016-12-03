function validateBeforeGettingSeries()   {
        if(validateInputForGettingSeries()== false)   {
            return false;
        }
        if(validateSeriesStartDate() == false)     {
            return false;
        }                
        return true;
    }
    
    function validateInputForGettingSeries()
    {
        var theForm = document.forms['form1'];
        if (!theForm) 
            theForm = document.form1;  
            
        if(theForm.txtStartDate.value == "" && theForm.ddlAccomName.selectedIndex == 0)    {            
            alert("Please enter either series start date or select the accomodation.");
            theForm.txtStartDate.focus();
            return false;
        }                        
        return true;
     }
     
     function validateSeriesStartDate()
        {        
            var theForm = document.forms['form1'];
            if (!theForm)   { 
                theForm = document.form1;  
            }            
            if(theForm.txtStartDate.value != "")      {            
                if(isDate(theForm.txtStartDate.value) == false)     {
                    alert("Invalid series start date. The correct format is 'dd-mmm-yyyy'.");
                    theForm.txtStartDate.focus();
                    return false;
                }                                   
            }
            return true;
        }

