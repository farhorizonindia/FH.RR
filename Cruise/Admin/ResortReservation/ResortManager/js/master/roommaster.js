// JScript File
function validateSave() {
    if(validateForm() ==false)  {
        return false;
    }    
    return true;
}

function validateForm()  {
    var el;
    el = getElement('ddlAccomType');
    if(el.value == 0 || el.value == null)   {
        alert("Please select the accomodation type.");
        return false;
    }
    el = getElement('ddlAccomodation');
    if(el.value == 0 || el.value == null)   {
        alert("Please select the accomodation.");
        return false;
    }
    el = getElement('ddlRoomCategory');
    if(el.value == 0 || el.value == null)   {
        alert("Please select the room category.");
        return false;
    }
    el = getElement('ddlRoomType');
    if(el.value == 0 || el.value == null)   {
        alert("Please select the room type.");
        return false;
    }        
    el = getElement('txtRoomNo');
    if(el.value == "")   {
        alert("Please enter the Room No.");
        return false;
    }
    el = getElement('ddlExtraBeds');
    if(el.value == "-1" || el.value == null)   {
        alert("Please select the extra beds.");
        return false;
    }
    if(el.value > 0  && el.value != null)   {
        el = getElement('txtExtraBedRate');
        if(el.value == "")  {
            alert("Please enter the extra bed rate.");
            return false;
        }
    }    
    el = getElement('txtNoOfBeds');
    if(el.value == "")   {
        alert("Please enter the No. of beds.");
        return false;
    }
    el = getElement('ddlConvert');
    if(el != null)  {
        if(el.value == 0 || el.value == null)   {
            alert("Please select the room conversion status.");
            return false;
        }
    }    
    return true;
}

function getElement(id) { 
    var x=document.getElementById(id) 
    return x;
}

