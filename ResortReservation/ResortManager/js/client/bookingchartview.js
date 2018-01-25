// JScript File
var lastselectedDiv;
function validateStartDate() {
    var startDate = getElement('txtStartDate');
    if (startDate.value == "") {
        alert("Please enter the chart start date.");
        startDate.focus();
        return false;
    }
    if (isDate(startDate.value) == false) {
        alert("Invalid chart start date. The correct format is 'dd-mm-yyyy'.");
        startDate.focus();
        return false;
    }
    return true;
}

function headerAligner() {
    var mainTable = getElement("MainTable");
    var pageheadertable = getElement("pageheader");
    pageheadertable.style.pixelWidth = mainTable.style.pixelWidth;
}

function showRoomBookings(parentCellId, contentPanelId) {
    var currDiv, lastDiv, headerDiv, parentCell;
    var divId, toolTipText, lastselectedDiv;

    //document.getElementById('bookingdetails').innerHTML = "Click on booking cell to see the booking details.";
    currDiv = document.getElementById(contentPanelId);
    parentCell = document.getElementById(parentCellId);

    divId = currDiv.id;
    //document.getElementById('bookingdetails').innerHTML = currDiv.innerHTML;
    toolTipText = currDiv.innerHTML;
    divid = divId.replace("Detail", "Header");
    headerDiv = document.getElementById(divid);
    if (headerDiv != null) {
        headerDiv.style.borderStyle = "none";
        headerDiv.style.color = "white";
    }


    /*if(lastselectedDiv !=null && lastselectedDiv != undefined && lastselectedDiv !="")    {
    lastselectedDiv = lastselectedDiv.replace("Header", "Detail");
    lastDiv = document.getElementById(lastselectedDiv);
    }
    
    if(lastDiv == undefined)  {
    divId = currDiv.id;
    //document.getElementById('bookingdetails').innerHTML = currDiv.innerHTML;
    toolTipText = currDiv.innerHTML;
    divid = divId.replace("Detail", "Header");
    headerDiv = document.getElementById(divid);
    if(headerDiv !=null)  {                            
    headerDiv.style.borderStyle="groove";
    headerDiv.style.color="white";
    lastselectedDiv = divId;
    }
    }
    else if(currDiv.id != lastDiv.id) {
    divId = lastDiv.id;
    divid = divId.replace("Detail", "Header");
    headerDiv = document.getElementById(divid);
    if(headerDiv !=null)  {                            
    headerDiv.style.borderStyle="none";
    headerDiv.style.color="black";
    lastselectedDiv = "";
    }        
    divId = currDiv.id;
    //document.getElementById('bookingdetails').innerHTML = currDiv.innerHTML;
    toolTipText = currDiv.innerHTML;
    divid = divId.replace("Detail", "Header");
    headerDiv = document.getElementById(divid);
    if(headerDiv !=null)  {                            
    headerDiv.style.borderStyle="groove";
    headerDiv.style.color="white";
    lastselectedDiv = divId;
    }
    }        
    else if(currDiv.id == lastDiv.id) {
    divId = currDiv.id;
    //document.getElementById('bookingdetails').innerHTML = "Click on booking cell to see the booking details.";
    divid = divId.replace("Detail", "Header");
    headerDiv = document.getElementById(divid);
    if(headerDiv !=null)  {                            
    headerDiv.style.borderStyle="none";
    headerDiv.style.color="black";
    lastselectedDiv = "";
    }
    } */
    var left = parentCell.offsetLeft + 15;
    var top = parentCell.offsetTop + 20;
    //debugger;
    //document.getElementById('roomCellDivDetail10120180304').css('left', '-18px');
    showBubbleTooltip('Booking Details', toolTipText, parentCell, left, top);
   
}



function showRoomBookingsm(parentCellId, contentPanelId) {
    var currDiv, lastDiv, headerDiv, parentCell;
    var divId, toolTipText, lastselectedDiv;

    //document.getElementById('bookingdetails').innerHTML = "Click on booking cell to see the booking details.";
    currDiv = document.getElementById(contentPanelId);
    parentCell = document.getElementById(parentCellId);

    divId = currDiv.id;
    //document.getElementById('bookingdetails').innerHTML = currDiv.innerHTML;
    toolTipText = currDiv.innerHTML;
    divid = divId.replace("Detail", "Header");
    headerDiv = document.getElementById(divid);
    if (headerDiv != null) {
        headerDiv.style.borderStyle = "none";
        headerDiv.style.color = "white";
    }


    /*if(lastselectedDiv !=null && lastselectedDiv != undefined && lastselectedDiv !="")    {
    lastselectedDiv = lastselectedDiv.replace("Header", "Detail");
    lastDiv = document.getElementById(lastselectedDiv);
    }
    
    if(lastDiv == undefined)  {
    divId = currDiv.id;
    //document.getElementById('bookingdetails').innerHTML = currDiv.innerHTML;
    toolTipText = currDiv.innerHTML;
    divid = divId.replace("Detail", "Header");
    headerDiv = document.getElementById(divid);
    if(headerDiv !=null)  {                            
    headerDiv.style.borderStyle="groove";
    headerDiv.style.color="white";
    lastselectedDiv = divId;
    }
    }
    else if(currDiv.id != lastDiv.id) {
    divId = lastDiv.id;
    divid = divId.replace("Detail", "Header");
    headerDiv = document.getElementById(divid);
    if(headerDiv !=null)  {                            
    headerDiv.style.borderStyle="none";
    headerDiv.style.color="black";
    lastselectedDiv = "";
    }        
    divId = currDiv.id;
    //document.getElementById('bookingdetails').innerHTML = currDiv.innerHTML;
    toolTipText = currDiv.innerHTML;
    divid = divId.replace("Detail", "Header");
    headerDiv = document.getElementById(divid);
    if(headerDiv !=null)  {                            
    headerDiv.style.borderStyle="groove";
    headerDiv.style.color="white";
    lastselectedDiv = divId;
    }
    }        
    else if(currDiv.id == lastDiv.id) {
    divId = currDiv.id;
    //document.getElementById('bookingdetails').innerHTML = "Click on booking cell to see the booking details.";
    divid = divId.replace("Detail", "Header");
    headerDiv = document.getElementById(divid);
    if(headerDiv !=null)  {                            
    headerDiv.style.borderStyle="none";
    headerDiv.style.color="black";
    lastselectedDiv = "";
    }
    } */
 
    var left = parentCell.offsetLeft + 350;
    var top = parentCell.offsetTop + 30;
    showBubbleTooltip('', toolTipText, parentCell, left, top);

}




function hideRoomBookings(contentPanelId) {
    var currDiv, divId, headerDiv;

    hideBubbleTooltip();

    currDiv = document.getElementById(contentPanelId);
    divId = currDiv.id;
    divid = divId.replace("Detail", "Header");
    headerDiv = document.getElementById(divid);
    if (headerDiv != null) {
        headerDiv.style.borderStyle = "none";
        headerDiv.style.color = "black";
    }
}

function getElement(id) {
    var x = document.getElementById(id)
    return x;
} 