    var datePickerDivID = "datepicker";
    var iFrameDivID = "datepickeriframe";
    
    var dayArrayShort = new Array('&#1588;&#8192',
                                    '&#8192&#1740;&#8192',
                                    '&#8192;&#1583;&#8192',
                                    '&#1587;&#8192',
                                    '&#8192;&#1670;&#8192',
                                    '&#8192;&#1662;&#8192',
                                    '&#8192;&#1580;&#8192');
    var dayArrayMed = new Array('&#1588;&#1606;&#1576;&#1607;' , '&#1740;&#1705;&#1588;&#1606;&#1576;&#1607;', '&#1583;&#1608;&#1588;&#1606;&#1576;&#1607;'
                                , '&#1587;&#1607;&#32;&#1588;&#1606;&#1576;&#1607;', '&#1670;&#1607;&#1575;&#1585;&#1588;&#1606;&#1576;&#1607;'
                                , '&#1662;&#1606;&#1580;&#1588;&#1606;&#1576;&#1607;' , '&#1580;&#1605;&#1593;&#1607;');
                                
    var dayArrayLong = dayArrayMed;
    var monthArrayShort = new Array('&#1601;&#1585;&#1608;&#1585;&#1583;&#1740;&#1606;','&#1575;&#1585;&#1583;&#1740;&#1576;&#1607;&#1588;&#1578;'
                                    ,'&#1582;&#1585;&#1583;&#1575;&#1583;','&#1578;&#1740;&#1585;','&#1605;&#1585;&#1583;&#1575;&#1583;'
                                    ,'&#1588;&#1607;&#1585;&#1740;&#1608;&#1585;','&#1605;&#1607;&#1585;','&#1570;&#1576;&#1575;&#1606;'
                                    ,'&#1570;&#1584;&#1585;','&#1583;&#1740;','&#1576;&#1607;&#1605;&#1606;'
                                    ,'&#1575;&#1587;&#1601;&#1606;&#1583;');
                                    
    var monthArrayMed = monthArrayShort;
    var monthArrayLong = monthArrayShort;
    
    // these variables define the date formatting we're expecting and outputting.
    // If you want to use a different format by default, change the defaultDateSeparator
    // and defaultDateFormat variables either here or on your HTML page.
    
    var defaultDateSeparator = "/";        // common values would be "/" or "."
    var defaultDateFormat = "ymd"    // valid values are "mdy", "dmy", and "ymd"
    var dateSeparator = defaultDateSeparator;
    var dateFormat = defaultDateFormat;
    
    function displayDatePicker(dateFieldName,oldDate, displayBelowThisObject, dtFormat, dtSep)
    {
        var targetDateField = document.getElementById(dateFieldName);
        // if we weren't told what node to display the datepicker beneath, just display it
        // beneath the date field we're updating
    
        if (!displayBelowThisObject)
            displayBelowThisObject = targetDateField;
        
        // if a date separator character was given, update the dateSeparator variable
        if (dtSep)
            dateSeparator = dtSep;
        else
            dateSeparator = defaultDateSeparator;
    
        // if a date format was given, update the dateFormat variable
        if (dtFormat)
            dateFormat = dtFormat;
        else
            dateFormat = defaultDateFormat;
        
        var x = displayBelowThisObject.offsetLeft;
        var y = displayBelowThisObject.offsetTop + displayBelowThisObject.offsetHeight ;
    
        // deal with elements inside tables and such
        var parent = displayBelowThisObject;
        
        while (parent.offsetParent) {
            parent = parent.offsetParent;
            x += parent.offsetLeft;
            y += parent.offsetTop ;
        }
        
        drawDatePicker(targetDateField,oldDate, x, y);
    }
    
    /**Draw the datepicker object (which is just a table with calendar elements) at the
    specified x and y coordinates, using the targetDateField object as the input tag
    that will ultimately be populated with a date.
    This function will normally be called by the displayDatePicker function.*/

    function drawDatePicker(targetDateField,oldDate, x, y)
    
    {
        var dt = getFieldDate(oldDate);
        
        // the datepicker table will be drawn inside of a <div> with an ID defined by the
        // global datePickerDivID variable. If such a div doesn't yet exist on the HTML
        // document we're working with, add one.
    
        if (!document.getElementById(datePickerDivID)) {
            // don't use innerHTML to update the body, because it can cause global variables
            // that are currently pointing to objects on the page to have bad references
            //document.body.innerHTML += "<div id='" + datePickerDivID + "' class='dpDiv'></div>";
            
            var newNode = document.createElement("div");
            newNode.setAttribute("id", datePickerDivID);
            newNode.setAttribute("class", "dpDiv");
            newNode.setAttribute("style", "visibility: hidden;");
            
            document.body.appendChild(newNode);
        }
    
        // move the datepicker div to the proper x,y coordinate and toggle the visiblity
        var pickerDiv = document.getElementById(datePickerDivID);
        pickerDiv.style.position = "absolute";
        pickerDiv.style.left = x + "px";
        pickerDiv.style.top = y + "px";
        pickerDiv.style.visibility = (pickerDiv.style.visibility == "visible" ? "hidden" : "visible");
        pickerDiv.style.display = (pickerDiv.style.display == "block" ? "none" : "block");
        pickerDiv.style.zIndex = 10000;
    
        // draw the datepicker table
        refreshDatePicker(targetDateField.name, dt[0], dt[1], dt[2]);
    }

    /**This is the function that actually draws the datepicker calendar.*/
    function refreshDatePicker(dateFieldName, year, month, day)
    {
        // if no arguments are passed, use today's date; otherwise, month and year
        // are required (if a day is passed, it will be highlighted later)

        var thisDay = getTodayPersian();
        //var weekday = (thisDay[3] - thisDay[2] + 1)%7;
        var weekday = calcPersian(thisDay[0],thisDay[1],1)[3];
            
        if(!day)
            day = 1;

        if ((month >= 1) && (year > 0)) {
            thisDay = calcPersian(year,month,1);
            weekday = thisDay[3];
            thisDay = new Array(year,month,day,weekday);
            thisDay[2] = 1;
        }
        else{
            day = thisDay[2];
            thisDay[2] = 1;
        }

        // the calendar will be drawn as a table
        // you can customize the table elements with a global CSS style sheet,
        // or by hardcoding style and formatting elements below
        var crlf = "\r\n";
        var tdSetter = "<td style='width:20px';></td>";
        var TABLE = "<table cols=7 class='dpTable' dir='rtl'>" + crlf;
        TABLE += "<tr>"+tdSetter+tdSetter+tdSetter+tdSetter+tdSetter+tdSetter+tdSetter+"</tr>";
        var xTABLE = "</table>" + crlf;
        var TR = "<tr class='dpTR'>";
        var TR_title = "<tr class='dpTitleTR'>";
        var TR_days = "<tr class='dpDayTR'>";
        var TR_todaybutton = "<tr class='dpTodayButtonTR'>";
        var xTR = "</tr>" + crlf;
        // leave this tag open, because we'll be adding an onClick event
        var TD = "<td class='dpTD' onMouseOut='this.className=\"dpTD\";' onMouseOver=' this.className=\"dpTDHover\";' ";    
        var TD_title = "<td colspan=3 class='dpTitleTD'>";
        var TD_buttons = "<td class='dpButtonTD'>";
        var TD_todaybutton = "<td colspan=7 class='dpTodayButtonTD'>";
        var TD_days = "<td class='dpDayTD'>";
        // leave this tag pen, because we'll be adding an onClick event
        var TD_selected = "<td class='dpDayHighlightTD' onMouseOut='this.className=\"dpDayHighlightTD\";' onMouseOver = 'this.className=\"dpTDHover\";' ";
                             
        var xTD = "</td>" + crlf;
        var DIV_title = "<div class='dpTitleText'>";
        var DIV_selected = "<div class='dpDayHighlight'>";
        var xDIV = "</div>";
        
        // start generating the code for the calendar table
        var html = TABLE;
        // this is the title bar, which displays the month and the buttons to
        // go back to a previous month or forward to the next month
        html += TR_title;
        html += TD_buttons + getNewYear(dateFieldName, thisDay, -1, "&lt;&lt;") + xTD;
        html += TD_buttons + getButtonCode(dateFieldName, thisDay, -1, "&lt;") + xTD;
        html += TD_title + DIV_title + monthArrayLong[ thisDay[1] - 1 ] + " " + toPersianDigit(thisDay[0]) + xDIV + xTD;
        html += TD_buttons + getButtonCode(dateFieldName, thisDay, 1, "&gt;") + xTD;
        html += TD_buttons + getNewYear(dateFieldName, thisDay, 1, "&gt;&gt;") + xTD;
        html += xTR;

        // this is the row that indicates which day of the week we're on
        html += TR_days;
        var i;

        for(i = 0; i < dayArrayShort.length; i++)
            html += TD_days + dayArrayShort[i] + xTD;
        
        html += xTR;
        
        // now we'll start populating the table with days of the month
        html += TR;
        // first, the leading blanks
        
        if(weekday != 6)
            for (i = 0; i <= weekday; i++)
                html += TD + "&nbsp;" + xTD;

        // now, the days of the month
        var len = 31;
        
        if( thisDay[1] > 6)
            len = 30;
        
        if( thisDay[1] == 12 && !leap_persian(thisDay[0]))
            len = 29;
        
        for(var dayNum = thisDay[2]; dayNum <= len; dayNum++) {
            TD_onclick = " onclick=\"updateDateField('" + dateFieldName + "', '" + getDateString(thisDay) + "');\">";

            if (dayNum == day)
                html += TD_selected + TD_onclick + DIV_selected +toPersianDigit(dayNum) + xDIV + xTD;
            else
                html += TD + TD_onclick + toPersianDigit(dayNum) + xTD;
            
            // if this is a Friday, start a new row
            if (weekday == 5)
                html += xTR + TR;
            
            weekday++;
            weekday = weekday % 7;

            // increment the day
            thisDay[2]++;
        } 
        
        // fill in any trailing blanks
        if (weekday > 0) {
            for (i = 6; i >weekday; i--)
                html += TD + "&nbsp;" + xTD;
        }

        html += xTR;
        // add a button to allow the user to easily return to today, or close the calendar
        var today = new Date()
        var todayString = "Today is " + dayArrayMed[today.getDay()] + ", " + monthArrayMed[ today.getMonth()] + " " + today.getDate();
        html += TR_todaybutton + TD_todaybutton;
        html += "<button style=\"background:LightGray;width:50px;\" class='dpTodayButton' onClick='refreshDatePicker(\"" + dateFieldName +
                    "\");'>&#1575;&#1605;&#1585;&#1608;&#1586;</button> ";
        html += "<button style=\"background:LightGray;width:50px\" class='dpTodayButton' onClick='updateDateField(\"" + dateFieldName +
                    "\");'>&#1576;&#1587;&#1578;&#1606;</button>";

        html += xTD + xTR;

        // and finally, close the table
        html += xTABLE;
        document.getElementById(datePickerDivID).innerHTML = html;
        // add an "iFrame shim" to allow the datepicker to display above selection lists
        adjustiFrame();
    }
    
    function toPersianDigit(str) {
        var result="";
        var strTmp = str.toString();
        for (var i=0;i<strTmp.length;i++) {
            if (strTmp.charAt(i) >= '0' &&  strTmp.charAt(i) <= '9') {
                result += "&#" + (1728 + (strTmp.charCodeAt(i) - '0'));
            }
        }
        return result;
    }

    /**Convenience function for writing the code for the buttons that bring us back or forward a month.*/
    function getButtonCode(dateFieldName, dateVal, adjust, label)
    {
        var newMonth = (dateVal[1] + adjust) % 12;
        var newYear = dateVal[0] + parseInt((dateVal[1] + adjust) / 12);

        if (newMonth < 1) {
            newMonth += 12;
            newYear += -1;
        }

    return "<button class='dpButton' onClick='refreshDatePicker(\"" + dateFieldName + "\", " + newYear
            + ", " + newMonth + ");' style=\"background:LightGray;font-family: tahoma; font-size: x-small;width:21px\" >" + label + "</button>";
    }
    function getNewYear(dateFieldName, dateVal, adjust, label)
    {
        var newMonth = dateVal[1];
        var newYear = dateVal[0] + adjust;
        
        return "<button class='dpButton' onClick='refreshDatePicker(\"" + dateFieldName + "\", " + newYear
                + ", " + newMonth + ");' style=\"background:LightGray;font-family: tahoma; font-size: x-small;width:20px\" >" + label + "</button>";
    }
    /**Convert a JavaScript Date object to a string, based on the dateFormat and dateSeparator
    variables at the beginning of this script library.*/
    
    function getDateString(dateVal)
    {
        var dayString = "00" + dateVal[2];
        var monthString = "00" + (dateVal[1]);
        dayString = dayString.substring(dayString.length - 2);
        monthString = monthString.substring(monthString.length - 2);
        
        switch (dateFormat) {
            case "dmy" :
                return dayString + dateSeparator + monthString + dateSeparator + dateVal[0];
            case "ymd" :
                return dateVal[0] + dateSeparator + monthString + dateSeparator + dayString;
            case "mdy" :
            default :
                return monthString + dateSeparator + dayString + dateSeparator + dateVal[0];
        }
        
    }

    /**Convert a string to a JavaScript Date object.*/
    function getFieldDate(dateString)
    {
        var dateVal;
        var dArray;
        var d, m, y;
        
        try {
            dArray = splitDateString(dateString);

                if (dArray) 
                {
                    
                    switch (dateFormat)
                    {
                        case "dmy" :
                            d = parseInt(dArray[0], 10);
                            m = parseInt(dArray[1], 10);
                            y = parseInt(dArray[2], 10);
                            break;
                        case "ymd" :
                            d = parseInt(dArray[2], 10);
                            m = parseInt(dArray[1], 10);
                            y = parseInt(dArray[0], 10);
                            break;
                        case "mdy" :
                        default :
                            d = parseInt(dArray[1], 10);
                            m = parseInt(dArray[0], 10);
                            y = parseInt(dArray[2], 10);
                            break;
                    }
        
                dateVal = new Array(y, m, d);
            } 
            else if (dateString) 
            {
                dateVal = getTodayPersian();
            }
            else 
            {
                dateVal = getTodayPersian();
            }
    
        }
        catch(e) {
            dateVal = getTodayPersian();
        }
        
        return dateVal;
    }

    /**Try to split a date string into an array of elements, using common date separators.
    If the date is split, an array is returned; otherwise, we just return false.*/
    function splitDateString(dateString)
    {
        var dArray;
        
        if (dateString.indexOf("/") >= 0)
            dArray = dateString.split("/");
        else if (dateString.indexOf(".") >= 0)
            dArray = dateString.split(".");
        else if (dateString.indexOf("-") >= 0)
            dArray = dateString.split("-");
        else if (dateString.indexOf("\\") >= 0)
            dArray = dateString.split("\\");
        else
            dArray = false;
    
        return dArray;
    }

    function updateDateField(dateFieldName, dateString)
    {
            var targetDateField = document.getElementsByName (dateFieldName).item(0);
            var currentTime = new Date();
            var currentHours = currentTime.getHours().toString().length != 1 ? currentTime.getHours() : '0' + currentTime.getHours();
            var currentMinutes = currentTime.getMinutes().toString().length != 1 ? currentTime.getMinutes() : '0' + currentTime.getMinutes();
            var currentSeconds = currentTime.getSeconds().toString().length != 1 ? currentTime.getSeconds() : '0' + currentTime.getSeconds();
            
            if (dateString)
                targetDateField.value = dateString + ' - ' + currentHours + ':' + currentMinutes + ':' + currentSeconds; 
            
            var pickerDiv = document.getElementById(datePickerDivID);
            pickerDiv.style.visibility = "hidden";
            pickerDiv.style.display = "none";
            adjustiFrame();
            targetDateField.focus();
    }

    /**Use an "iFrame shim" to deal with problems where the datepicker shows up behind
    selection list elements, if they're below the datepicker. The problem and solution are
    described at:
    http://dotnetjunkies.com/WebLog/jking/archive/2003/07/21/488.aspx
    http://dotnetjunkies.com/WebLog/jking/archive/2003/10/30/2975.aspx*/

    function adjustiFrame(pickerDiv, iFrameDiv)
    {
        // we know that Opera doesn't like something about this, so if we
        // think we're using Opera, don't even try
        var is_opera = (navigator.userAgent.toLowerCase().indexOf("opera") != -1);
        
        if (is_opera)
            return;
        
        // put a try/catch block around the whole thing, just in case
        try {
            
            if (!document.getElementById(iFrameDivID)) 
            {
                // don't use innerHTML to update the body, because it can cause global variables
                // that are currently pointing to objects on the page to have bad references
                //document.body.innerHTML += "<iframe id='" + iFrameDivID + "' src='javascript:false;' scrolling='no' frameborder='0'>";
                var newNode = document.createElement("iFrame");
                newNode.setAttribute("id", iFrameDivID);
                newNode.setAttribute("src", "javascript:false;");
                newNode.setAttribute("scrolling", "no");
                newNode.setAttribute ("frameborder", "0");
                document.body.appendChild(newNode);
            }
        
            if (!pickerDiv)
                pickerDiv = document.getElementById(datePickerDivID);

            if (!iFrameDiv)
                iFrameDiv = document.getElementById(iFrameDivID);
        
            try {
                iFrameDiv.style.position = "absolute";
                iFrameDiv.style.width = pickerDiv.offsetWidth;
                iFrameDiv.style.height = pickerDiv.offsetHeight ;
                iFrameDiv.style.top = pickerDiv.style.top;
                iFrameDiv.style.left = pickerDiv.style.left;
                iFrameDiv.style.zIndex = pickerDiv.style.zIndex - 1;
                iFrameDiv.style.visibility = pickerDiv.style.visibility ;
                iFrameDiv.style.display = pickerDiv.style.display;
            }
            catch(e) {}
        }
        catch (ee) {}
    }
    
    function displayPersianDate(e,ctrl){ 
        
        var pressedkey;
        
        if(typeof event!='undefined')
        { 
            pressedkey=window.event.keyCode; 
        }
        else
        { 
            pressedkey=e.keyCode ;
        } 
        
        if(pressedkey == 32)
        {
            var currentPersianDate = getTodayPersian();
            var currentTime = new Date();
            var currentHours = currentTime.getHours().toString().length != 1 ? currentTime.getHours() : '0' + currentTime.getHours();
            var currentMinutes = currentTime.getMinutes().toString().length != 1 ? currentTime.getMinutes() : '0' + currentTime.getMinutes();
            var currentSeconds = currentTime.getSeconds().toString().length != 1 ? currentTime.getSeconds() : '0' + currentTime.getSeconds();
            
            if (currentPersianDate[1].toString().length == 1)
                currentPersianDate[1] = '0' +  currentPersianDate[1];
            
            if (currentPersianDate[2].toString().length == 1)
                currentPersianDate[2] = "0" +  currentPersianDate[2]; 
                 
            ctrl.value = currentPersianDate[0] + '/' + currentPersianDate[1] + '/' + currentPersianDate[2] + 
                        ' - ' + currentHours + ':' + currentMinutes + ':' + currentSeconds; 
        }
        
    } 
