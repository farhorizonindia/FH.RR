// JScript File

function popUpTreeTypeChanger(url, attributes)
{    
    if(attributes == '')
    {
       attributes = 'height=400, width=450, left=200, top=200, scrollbars=yes';
    }
    newwindow=window.open(url,'name',attributes);    
    if(newwindow.focus !=null)
    {
        newwindow.focus();
    }
    return false;
}
 
function popUpRoomTypeChanger(url)
{    
    newwindow=window.open(url,'name','height=410, width=460, left=200, top=200, scrollbars=yes, location=true');        
    if(newwindow.focus !=null)
    {
        newwindow.focus();
    }
    return false;
}

function popUpRoomPaxChanger(url)
{    
    newwindow=window.open(url,'name','height=450, width=425, left=325, top=150, scrollbars=yes, location=true');        
    if(newwindow.focus !=null)
    {
        newwindow.focus();
    }
    return false;
}

function popUpGeneral(url)
{    
    newwindow=window.open(url,'name','height= 300, width= 500,left=200, top=200, scrollbars=yes, location=true');        
    if(newwindow.focus !=null)
    {
        newwindow.focus();
    }
    return false;
}
