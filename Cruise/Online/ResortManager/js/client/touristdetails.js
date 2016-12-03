function validateBeforeSearchingTourist()
{
    var theForm = document.forms['form1'];
    if(!theForm)
        theForm=document.form1;
    if(theForm.txtFName.value == '' && theForm.txtLName.value == '' && theForm.txtPassportNo.value == '' && theForm.ddlNationality.selectedIndex == 0)
    {
        alert("Please Choose A Lookup Field");
        theForm.txtFName.focus();
        return false;
    }
    PrepareUrl()
}

function ValidateForm()
{
    var theForm = document.forms['fomr1'];
    if(!theForm)
        theForm=document.fomr1;
    if(theForm.txtFName.value == "")
    {    
        alert("Please fill in First Name of the tourist.");
        theForm.txtStartDate.focus();
        return false;
    }
    if(theForm.txtLName.value == "")
    {    
        alert("Please fill in First Name of the tourist.");
        theForm.txtStartDate.focus();
        return false;
    }
    if(theForm.ddlNationality.value == "")
    {    
        alert("Please select the nationality of the tourist.");
        theForm.ddlNationality.focus();
        return false;
    }
    return true;
}
function PrepareUrl()
{
    var theForm = document.forms['form1'];
    if(!theForm)
        theForm=document.form1;
    var fn = '', ln='', pn= '', nid ='';
    fn = theForm.txtFName.value;
    ln = theForm.txtLName.value;
    pn = theForm.txtPassportNo.value;
    nid = theForm.ddlNationality.selectedIndex
    var url = "SearchedTourists.aspx?FN=" + fn + "&LN=" + ln + "&PN=" + pn + "&NID=" + nid;
    popUpRoomTypeChanger(url);
}
