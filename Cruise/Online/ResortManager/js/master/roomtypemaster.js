// JScript File
function validateSave() {
    if(validateForm() ==false)  {
        return false;
    }    
    return true;
}

function validateForm()  {
    var roomType = getElement('txtRoomType');
    var defaultNoofBeds = getElement('ddlDefaultNoOfBeds');
    if(roomType.value == "")    {
        alert("Enter the room type.");
        return false;
    }
    if(defaultNoofBeds.value == "" || defaultNoofBeds.value == 0 || defaultNoofBeds.value == "Choose")   {
        alert("Select the no. of default beds in this type.");
        return false;
    }
    return true;
}

function getElement(id) { 
    var x=document.getElementById(id) 
    return x;
}

