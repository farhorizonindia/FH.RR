// JScript File
    function isDate (value)     {
        value = getDateObject(value,"-");
        if(value == null)   {
            return false;
        }
        return (!isNaN (new Date (value).getYear() ) ) ;
    }
    
    function isDateTime(value)  {
        value = getDateTimeObject(value,"-");
        if(value == null)   {
            return false;
        }
        return (!isNaN (new Date (value).getYear() ) ) ;
    }
    
    function CalculateDateDifference(d1, d2) {
        CalculateDateDifference(d1, d2, false);
    }
    
    function CalculateDateDifference(d1, d2, isWithTime)   {        
        //var dtSDText = GetFormattedDate(d1);
        var startdate, enddate;
        var dtSDText = d1;
        if(isWithTime == true)  {
            startdate = getDateTimeObject(dtSDText,"-");
        }
        else    {
           startdate = getDateObject(dtSDText,"-");
        }
        
        var dtEDText = d2;
        if(isWithTime == true)  {
            enddate = getDateTimeObject(dtEDText,"-");
        }
        else    {
           enddate = getDateObject(dtEDText,"-");
        }       
        
        var oneday = 1000*60*60*24;        
        var daysdif = Math.ceil((enddate - startdate)/oneday);
        return daysdif;
    }

    function setupCalendar(inputbox, button)    {
        Calendar.setup({ 
        inputField  :   inputbox,
        ifFormat    :   "%d-%b-%Y", 
        showsTime   :   false,        
        button      :   button,
        singleClick: true,
        min: 20160408,
        max: 20160725,
        step        :   1
    });

    }

    
    function setupCalendarWithTime(inputbox, button)    {
        Calendar.setup({ 
        inputField  :   inputbox,
        ifFormat    :   "%d-%b-%Y %H:%M", 
        showsTime   :   true,        
        timeFormat  :   24,
        button      :   button,
        singleClick :   true,
        step        :   1
        });
    }
    
    function getDateObject(dateString,dateSeperator)
    {
        //This function return a date object after accepting 
        //a date string ans dateseparator as arguments                
        var cDate,cMonth,cYear;
        var splittedDate = dateString.split(dateSeperator);
        cDate = splittedDate[0];
        cMonth = splittedDate[1];
        cYear = splittedDate[2];
        
        if(cMonth.length > 2)   {
            cMonth = parseMonth(dateString);
        }       
        //Create Date Object
        dtObject = new Date(cYear,cMonth,cDate); 
        return dtObject;
    }
    
    function getDateTimeObject(dateString,dateSeperator)
    {
        //This function return a date object after accepting 
        //a date string ans dateseparator as arguments
        var curValue=dateString;
        var splittedDateAndTime = curValue.split(" ");
        dateString = splittedDateAndTime[0];
        timeString = splittedDateAndTime[1];
        
        var cDate,cMonth,cYear;
        var splittedDate = dateString.split(dateSeperator);
        cDate = splittedDate[0];
        cMonth = splittedDate[1];
        cYear = splittedDate[2];
        if(cMonth.length > 2)   {
            cMonth = parseMonth(dateString);
        }               
        
        var cHour, cMin;
        cHour = 0;
        cMin = 0;
        if(timeString != null && timeString != 'undefined')     {
            var splittedTime = timeString.split(":");
            cHour = splittedTime[0];
            cMin = splittedTime[1];        
        }
        else {
            return null;
        }
        
        //Create Date Object
        dtObject = new Date(cYear,cMonth,cDate, cHour, cMin); 
        return dtObject;
    }   
    
    function GetFormattedDate(date)    {
        var sd = date;
        if(sd != "")    {
            dd = sd.substring(0,2);
            mm = parseMonth(sd);            
            yyyy = sd.substring(7);
            if(mm == "00x")     {
                alert("Please enter date in one of the following format:\ndd-mmm-yyyy");                    
                return false;
            }
            return dd+"-"+mm+"-"+yyyy;
        }
   }
   
   function GetFormattedDateFromLongDate(date)    {
        var sd = date;
        if(sd != "")    {
            dd = sd.getDate();
            mm = sd.getMonth();            
            yyyy = sd.getFullYear();
            var mon;
            
            if(mm == 0)
                mon = "Jan";
            else if(mm == 1)
                mon = "Feb";
            else if(mm == 2)
                mon = "Mar";
            else if(mm == 3)
                mon = "Apr";
            else if(mm == 4)
                mon = "May";
            else if(mm == 5)
                mon = "Jun";
            else if(mm == 6)
                mon = "Jul";
            else if(mm == 7)
                mon = "Aug";
            else if(mm == 8)
                mon = "Sep";
            else if(mm == 9)
                mon = "Oct";
            else if(mm == 10)
                mon = "Nov";
            else if(mm == 11)
                mon = "Dec";
                
            if(dd < 10) {
                dd = "0" + dd;
            }
            return dd+"-"+mon+"-"+yyyy;
        }
   }
   
   function parseMonth(theDate)
        {
            var isTheDate = theDate;            
            var monthString=isTheDate.substring(2,7);
            var mon;
            
            if(monthString.substring(0,1) != '-') 
            {
                if(monthString.substring(0,1) != '/')
                {
                    mon ="00x";
                    return mon;
                }                
            }            
            if(monthString.substring(4,5) != "-")            
            {
                if(monthString.substring(4,5) != "/")
                {
                    mon ="00x";
                    return mon;
                }
            }
                        
            monthString =  monthString.substring(1,4);
            //parse month 
            if(monthString.toLowerCase() == "jan")
                mon = "00";
            else if(monthString.toLowerCase() == "feb")
                mon = "01"
            else if(monthString.toLowerCase() == "mar")
                mon = "02"
            else if(monthString.toLowerCase() == "apr")
                mon = "03"
            else if(monthString.toLowerCase() == "may")
                mon = "04"
            else if(monthString.toLowerCase() == "jun")
                mon = "05"
            else if(monthString.toLowerCase() == "jul")
                mon = "06"
            else if(monthString.toLowerCase() == "aug")
                mon = "07"
            else if(monthString.toLowerCase() == "sep")
                mon = "08"
            else if(monthString.toLowerCase() == "oct")
                mon = "09"
            else if(monthString.toLowerCase() == "nov")
                mon = "10"
            else if(monthString.toLowerCase() == "dec")
                mon = "11"
            else
                mon="00x"
            return mon;
        }

function allowNumberWithDecimal()    {
    // Get the ASCII value of the key that the user entered
    var key = window.event.keyCode;
    // Verify if the key entered was a numeric character (0-9) or a decimal (.)
    if ( (key > 47 && key < 58) || key == 46 )
    // If it was, then allow the entry to continue
    return;
    else
    // If it was not, then dispose the key and continue with entry
    window.event.returnValue = null; 
}

function isNumberKey(evt)    {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if(charCode > 31 && (charCode <48) || (charCode > 57))
    {        
    return false;
    }
    else
    return true;
}

function getImagesPath()    {
    return "../images/";
}

function trim(stringToTrim) {
	return stringToTrim.replace(/^\s+|\s+$/g,"");
}
        
    
function disableInput(evt) {
    var charCode = event.keyCode;
    if(charCode == 9)   {
        return true;
    }
    else    {
        return false;
    }
}

function fillEndDate(startDate, endDate)  {
    var elStartDate = $get(startDate);
    var elEndDate = $get(endDate);
    
    if(elStartDate && elStartDate.value != "" && isDate(elStartDate.value))  {
        elEndDate.value = dateAdd('d', 1, GetFormattedDate(elStartDate.value));        
    }
}


function dateAdd(p_Interval, p_Number, p_Date)  {
	if(!isDate(p_Date)){	return "invalid date: '" + p_Date + "'";	}
	if(isNaN(p_Number)){	return "invalid number: '" + p_Number + "'";	}	

	p_Number = new Number(p_Number);
	//var dt = p_Date;
	//var dt = new Date(p_Date);
	var dt = getDateObject(p_Date, "-");
	
	switch(p_Interval.toLowerCase()){
		case "yyyy": {
			dt.setFullYear(dt.getFullYear() + p_Number);
			break;
		}
		case "q": {
			dt.setMonth(dt.getMonth() + (p_Number*3));
			break;
		}
		case "m": {
			dt.setMonth(dt.getMonth() + p_Number);
			break;
		}
		case "y":			// day of year
		case "d":			// day
		case "w": {		// weekday
			dt.setDate(dt.getDate() + p_Number);
			break;
		}
		case "ww": {	// week of year
			dt.setDate(dt.getDate() + (p_Number*7));
			break;
		}
		case "h": {
			dt.setHours(dt.getHours() + p_Number);
			break;
		}
		case "n": {		// minute
			dt.setMinutes(dt.getMinutes() + p_Number);
			break;
		}
		case "s": {
			dt.setSeconds(dt.getSeconds() + p_Number);
			break;
		}
		case "ms": {	// JS extension
			dt.setMilliseconds(dt.getMilliseconds() + p_Number);
			break;
		}
		default: {
			return "invalid interval: '" + p_Interval + "'";
		}
	}
	return GetFormattedDateFromLongDate(dt);
}