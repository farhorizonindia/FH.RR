// JScript File
function setRoomDropDownSelectedIndexes(jsonObject) {
    var dropdownId, selectedIndex;
    if(jsonObject != null) {
        roomDropDownSelectedIndexes = JSON.parse(jsonObject);
        if( !roomDropDownSelectedIndexes) {
            alert("JSON decode error");
            return;
        }    
        for(i=0; i< roomDropDownSelectedIndexes.length; i=i+2) {
            selectedIndex = roomDropDownSelectedIndexes[i];
            dropdownId= roomDropDownSelectedIndexes[i+1];
            var x = document.getElementById(dropdownId);
            if(x != null)   {
                x.selectedIndex = selectedIndex;    
            }
        }
    }    
} 

