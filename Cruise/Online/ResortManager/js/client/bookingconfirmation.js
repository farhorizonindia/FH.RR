  function validateBeforeConfirmation()   {
    if(validateForm() == false) {
        return false;
    }
    if(validateArrivateDepartureDates() == false)   {
        return false;
    }
    if(validateVoucherDate() == false ) {
        return false;
    }
    
    if(validateSelectedMealPlans() == false ) {
        return false;
    }
    return true;
  }
        
    function validateForm()    {
        var theForm = document.forms['form1'];
        if (!theForm) 
            theForm = document.form1;
        if(theForm.txtExchangeOrderNo.value == "")
        {
            alert("Please enter the exchange order no.");
            theForm.txtExchangeOrderNo.focus();
            return false;
        }
        if(theForm.txtVoucherDate.value == "")
        {
            alert("Please enter the voucher date");
            theForm.txtVoucherDate.focus();
            return false;
        }
        if(theForm.txtTourId.value == "")
        {
            alert("Please enter the tour no.");
            theForm.txtTourId.focus();
            return false;
        }
        if(theForm.txtArrivalDate.value == "")
        {
            alert("Please enter the arrival date.");
            theForm.txtArrivalDate.focus();
            return false;
        }
        if(theForm.txtDepartureDate.value == "")
        {
            alert("Please enter the departure date");
            theForm.txtDepartureDate.focus();
            return false;
        }
        /*if(theForm.txtArrivalTransport.value == "")
        {
            alert("Please Enter The Arrival Transport Source");
            theForm.txtArrivalTransport.focus();
            return false;
        }
        if(theForm.txtDepartureTransport.value == "")
        {
            alert("Please Enter The Departure Transport Source");
            theForm.txtDepartureTransport.focus();
            return false;
        }*/
        
        if(theForm.ddlArrivalCity.selectedIndex == 0)
        {
            alert("Please select the arrival city");
            theForm.ddlArrivalCity.focus();
            return false;
        }
        if(theForm.ddlDepartureCity.selectedIndex == 0)
        {
            alert("Please select the departure city");
            theForm.ddlDepartureCity.focus();
            return false;
        }
        
        return true;
    }    
    
    function validateArrivateDepartureDates()   {
        var theForm = document.forms['form1'];
        if (!theForm)   { 
            theForm = document.form1;  
        }            
        if(theForm.txtArrivalDate.value != "" && theForm.txtDepartureDate.value != "")      {            
            if(isDateTime(theForm.txtArrivalDate.value) == false)     {
                alert("Invalid arrival date and time. The correct format is 'dd-mmm-yyyy hh:mm'.");
                theForm.txtArrivalDate.focus();
                return false;
            }                   
            if(isDateTime(theForm.txtDepartureDate.value) == false)       {
                alert("Invalid departure date. The correct format is 'dd-mmm-yyyy hh:mm'.");
                theForm.txtDepartureDate.focus();
                return false;
            }                
            daysdif  = CalculateDateDifference(theForm.txtArrivalDate.value, theForm.txtDepartureDate.value, true);                
            if(daysdif < 0)     {
                alert("Arrival date cannot be greater than departure date");
                theForm.txtArrivalDate.focus();
                return false;
            }            
        }
        return true;
    }
    
    function validateVoucherDate()   {
        var theForm = document.forms['form1'];
        if (!theForm)   { 
            theForm = document.form1;  
        }            
        if(theForm.txtStartDate.value != "" && theForm.txtVoucherDate.value != "")      {            
            if(isDate(theForm.txtVoucherDate.value) == false)     {
                alert("Invalid voucher date. The correct format is 'dd-mmm-yyyy'.");
                theForm.txtVoucherDate.focus();
                return false;
            }                   
            if(isDate(theForm.txtStartDate.value) == false)       {
                alert("Invalid Check-In date. The correct format is 'dd-mmm-yyyy'.");
                theForm.txtStartDate.focus();
                return false;
            }                
            daysdif  = CalculateDateDifference(theForm.txtVoucherDate.value, theForm.txtStartDate.value, false);                
            if(daysdif <= 0)     {
                alert("Voucher date should be less than check-in date");                                        
                theForm.txtVoucherDate.focus();
                return false;
            }            
        }
        return true;
    }
    
    
    function SetFocus()
    {
        var theForm = document.forms['form1'];
        if (!theForm) 
            theForm = document.form1; 
        theForm.txtExchangeOrderNo.focus();
    }
    
    function ResetMealPlanCheckBoxes()
    {
        for(i=0; i<form1.elements.length; i++)
        {
            if(form1.elements[i].item == "checkbox")
            {
                ALERT("HI");
            }
        }
    }

    function showhide(layer_ref) 
    {     
        var displaystate = document.getElementById(layer_ref).style.display;    
        var source = '';
        if (displaystate == 'block') 
        { 
            source = "../images/icon_summary_plus_On.gif";
            displaystate = 'none'; 
        }    
        else     
        { 
            source = "../images/icon_summary_minus_On.gif";
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
    
    function validateSelectedMealPlans()    {
        var id = ""; var selectedCheckBox = 0;
        var selected = false; var chosen = false;
        var el, ddl, chk;
        var mpOptionSelected = false;
        
        var theForm = document.forms['form1'];
        if (!theForm) 
            theForm = document.form1;  
        
        for(j=0; j < theForm.elements.length; j++)    {
            el = theForm.elements[j];
            if(el.type == "select-one") {
                id = el.id;            
                if(id.substring(0, 7) == "ddl*mp*") {                
                    ddl = document.getElementById(id);
                    if(ddl != null )    {
                        if(ddl.selectedIndex == 0)  {                        
                            alert("Please choose the meal plan. Confirmation cannot be completed without meal plan.");
                            ddl.focus();
                            return false;
                        }  
                        else    {
                            id = id.replace("ddl*mp*", "chk*W*");
                            chk = document.getElementById(id);
                            if(chk != null) {   
                                if(chk.checked == true)   {
                                    mpOptionSelected = true;
                                    continue;
                                }                            
                            }
                            id = id.replace("chk*W*", "chk*B*");
                            chk = document.getElementById(id);
                            if(chk != null) {   
                                if(chk.checked == true)   {
                                    mpOptionSelected = true;
                                    continue;
                                }                            
                            }
                            id = id.replace("chk*B*", "chk*L*");
                            chk = document.getElementById(id);
                            if(chk != null) {   
                                if(chk.checked == true)   {
                                    mpOptionSelected = true;
                                    continue;
                                }                            
                            }
                            id = id.replace("chk*L*", "chk*E*");
                            chk = document.getElementById(id);
                            if(chk != null) {   
                                if(chk.checked == true)   {
                                    mpOptionSelected = true;
                                    continue;
                                }                            
                            }
                            id = id.replace("chk*E*", "chk*D*");
                            chk = document.getElementById(id);
                            if(chk != null) {   
                                if(chk.checked == true)   {
                                    mpOptionSelected = true;
                                    continue;
                                }                            
                            } 
                            if(mpOptionSelected == false)   {
                                alert("Please choose at least one option of the meal plan.");
                                ddl.focus();
                                return false;                        
                            } 
                        }                  
                    }
                }
            }
        }               
        return true;
    }
    
    function setTextFocusOnTab(e)    {
        if (e.keyCode == 9 && !e.shiftKey) {
			var tabContainer = $get('tabContainer');
            if (tabContainer != undefined && tabContainer != null)  {
                tabContainer = tabContainer.control;
                tabContainer.set_activeTabIndex(1);
                selectFirstControl();
            } 
		}        
    }
    
    function selectFirstControl()   {
        var mealPlanPanel = $get('tabContainer_tabMealandActivity_pnlDateWiseSchedule');
        var controls = mealPlanPanel.getElementsByClassName('mealPlan');
        // Loop through the control using any of the javascript looping technique.
        //Inside the loop check the following condition.
        if(controls)    {
            for(i=0; i < controls.length; i++)    {
                //if (controls[i].tagName == "select" && controls[i].style.visibility != "hidden")                        
                if (controls[i].type == "select-one" && controls[i].style.visibility != "hidden")
                {
                    controls[i].focus();
                    //controls[i].selectedIndex = 0;
                    break;
                }
            }
        }
    }