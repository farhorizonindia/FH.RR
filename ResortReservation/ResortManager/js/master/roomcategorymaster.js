// JScript File
function validateSave() {
    if(validateForm() ==false)  {
        return false;
    }    
    return true;
}

function validateForm()  {
    var el;
    el = getElement('txtRoomCategory');
    if(el.value == "")   {
        alert("Please enter the room category.");
        return false;
    }
    return true;
}

function getElement(id) { 
    var x=document.getElementById(id) 
    return x;
}


