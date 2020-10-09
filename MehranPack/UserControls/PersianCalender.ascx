<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PersianCalender.ascx.cs"
    Inherits="MehranPack.UserControls.PersianCalender" %>

<script type="text/javascript" language="JavaScript" src="/Scripts/Utility.js"></script>
<script type="text/javascript" language="JavaScript" src="/Scripts/PersianCalendar.js"></script>
<link href="/Content/css/calendar.css" type="text/css" rel="stylesheet" />

<script language="javascript" type="text/javascript">
  
    function changePlace(control, form) {
        var controlID = getPageID(control);
        if (control.value.length == control.maxLength) {
            var index = getIndex(control, form);
            if (form.elements[index + 1].disabled == false && form.elements[index].type != "hidden") {
                if (index != 8)
                    form.elements[index + 1].focus();
                else if (form.elements[10] != null)
                    form.elements[10].focus();
            }
        }
        else if (control.value.length == 0 && controlID != "pan1") {
            var index = getIndex(control, form);
            setRange(form.elements[index - 1], form.elements[index - 1].value.length);
        }
    }
    function setRange(ctrl, start) {
        var end = start;
        if (ctrl.setSelectionRange) {
            ctrl.setSelectionRange(start, start);
        }
        else {
            var range;
            try {
                range = ctrl.createTextRange();
            }
            catch (e) {
                try {
                    range = document.body.createTextRange();
                    range.moveToElementText(ctrl);
                }
                catch (e) {
                    range = null;
                }
            }

            if (!range) return;
            range.collapse(true);
            range.moveStart("character", start);
            range.moveEnd("character", end - start);
            range.select();
        }
    }
    function setDate(clientID) {
        debugger;
        var year = clientID + "year";
        var yearCtl = document.getElementById(clientID + "year");

        if (yearCtl == null)
            yearCtl = document.getElementById("year");

        var mounthCtl = document.getElementById(clientID + "mounth");
        if (mounthCtl == null)
            mounthCtl = document.getElementById("mounth");

        var dayCtl = document.getElementById(clientID + "day");
        if (dayCtl == null)
            dayCtl = document.getElementById("day");

        var prevDate = yearCtl.value + "/" + mounthCtl.value + "/" + dayCtl.value;

        if (document.getElementById(clientID + "year") == null)
            year = "year";

        displayDatePicker(year, prevDate, false);
    }
    function spilitDate(clientID) {
        var yearCtl = document.getElementById(clientID + "year") || document.getElementById("year");
        var dateTime = yearCtl.value;
        if (yearCtl.value.length > 4) {
            var mounthCtl = document.getElementById(clientID + "mounth") || document.getElementById("mounth")
            var dayCtl = document.getElementById(clientID + "day") || document.getElementById("day");
            yearCtl.value = dateTime.substring(0, 4);
            mounthCtl.value = dateTime.substring(5, 7);
            dayCtl.value = dateTime.substring(8, 10);
            if ((document.getElementById(clientID + "_timePanel") || document.getElementById("_timePanel")) != null) {
                var hourCtl = document.getElementById(clientID + "hour") || document.getElementById("hour");
                var minuteCtl = document.getElementById(clientID + "minute") || document.getElementById("minute");
                var secCtl = document.getElementById(clientID + "second") || document.getElementById("second");
                hourCtl.value = dateTime.substring(13, 15);
                minuteCtl.value = dateTime.substring(16, 18);
                secCtl.value = dateTime.substring(19, 21);
            }
        }
        //yearCtl.value=dateTime;
    }

    function stepNext(isForm, isField, year) {
        var yearVal = year.value;
        if (yearVal.length > 4) {
            nextField = 0;
            nElements = isForm.length;
            for (i = 0; i < nElements; i++) {
                if (isForm[i].name == isField) {
                    nextField = i;
                    if (nextField < nElements) { nextField++ }
                    isForm[nextField].focus();
                }
            }
        }
    }
    function getPageID(control) {
        var result = control.id;

        var index = 0;
        var pindex = -1
        while ((index = result.indexOf("_", pindex + 1)) != -1)
            pindex = index;

        return result.substring(pindex + 1, result.length);
    }
    function getIndex(control, form) {
        var result = 0;
        for (; result < form.elements.length; result++)
            if (form.elements[result].id == control.id)
                break;
        return result;
    }

    function checkAllFieldValues(source, argument, clientID) {
        clientID = clientID.id.substring(0, clientID.id.lastIndexOf("_") + 1);
        var result = true;
        var errorMessage = "تاریخ نامعتبر";
        var y = document.getElementById(clientID + "year") || document.getElementById("year")
        var year = y.value;
        var m = document.getElementById(clientID + "mounth") || document.getElementById("mounth")
        var mounth = m.value;

        var d = document.getElementById(clientID + "day") || document.getElementById("day");
        var day = d.value;

        var hf = document.getElementById(clientID + "hfAllowEmpty") || document.getElementById("hfAllowEmpty")
        var AllowEmpty = hf.value;

        var error = document.getElementById(clientID + "finalError") || document.getElementById("finalError");
//        if (AllowEmpty == "0") {
            if (year < 1360)
                result = false;

            if (year.length < 4 || mounth < 1 || mounth > 12 || day < 1 || day > 31)
                result = false;
        if (document.getElementById(clientID + "_timePanel") || document.getElementById("_timePanel") != null) {
            var hour = (document.getElementById(clientID + "hour") || document.getElementById("hour")).value;
            var minute = (document.getElementById(clientID + "minute") || document.getElementById("minute")).value;
            var sec = (document.getElementById(clientID + "second") || document.getElementById("second")).value;
                if (hour.length == 0 || hour < 0 || hour > 23 || minute.length == 0 || minute < 0 || minute > 59 || sec.length == 0 || sec < 0 || sec > 59)
                    result = false;
            }
//        }
        if (AllowEmpty == "1") {
            if (year.length == 0 && mounth.length == 0 && day.length == 0)
                result = true;
        }

        if (!result) {
            error.innerHTML = errorMessage;
        }
        else {
            error.innerHTML = "";
        }
        argument.IsValid = result;
    }

</script>

<asp:HiddenField ID="hfAllowEmpty" runat="server" />
<table dir="ltr" cellpadding="0" cellspacing="0" style="font-family: Tahoma">
    <tr>
        <td>
            &nbsp;<asp:CustomValidator ID="finalError" runat="server" CssClass="validation" ClientValidationFunction="checkAllFieldValues(val, args, this);"
                ErrorMessage="تاریخ نامعتبر" ValidateEmptyText="true" ControlToValidate="year"
                Display="Dynamic"></asp:CustomValidator>
            <img src="/UserControls/cal.png" onclick="setDate('<%= ClientIDPerfix %>');" alt="تقویم" />&nbsp;
        </td>
        <td style="border: 1px solid #5B92C8;">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 30px">
                        <asp:TextBox ID="year" runat="server" onkeypress="JustNum(event)" Font-Names="Tahoma"
                            Font-Size="9pt" dir="ltr" onkeyup="changePlace(this, this.form);" Width="30px"
                            MaxLength="4" BorderColor="White" BorderStyle="None" onfocus="stepNext(this.form,this.name,this)"></asp:TextBox>
                    </td>
                    <td style="width: 5px">
                        /
                    </td>
                    <td style="width: 15px">
                        <asp:TextBox ID="mounth" runat="server" onkeypress="JustNum(event)" Font-Names="Tahoma"
                            Font-Size="9pt" dir="ltr" onkeyup="changePlace(this, this.form);" Width="15px"
                            MaxLength="2" BorderColor="White" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td style="width: 5px">
                        /
                    </td>
                    <td style="width: 15px">
                        <asp:TextBox ID="day" runat="server" onkeypress="JustNum(event)" Font-Names="Tahoma"
                            Font-Size="9pt" dir="ltr" onkeyup="changePlace(this, this.form);" Width="15px"
                            MaxLength="2" BorderColor="White" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td>
                        <table dir="ltr" runat="server" id="_timePanel" cellspacing="0" visible="false" cellpadding="0">
                            <tr>
                                <td style="width: 5px">
                                    -
                                </td>
                                <td style="width: 15px">
                                    <asp:TextBox ID="hour" runat="server" Font-Names="Tahoma" Font-Size="9pt" dir="ltr"
                                        onkeyup="changePlace(this, this.form);" Width="15px" MaxLength="2" BorderColor="White"
                                        BorderStyle="None"></asp:TextBox>
                                </td>
                                <td style="font-weight: bold; font-size: 8pt; font-family: tahoma; width: 5px">
                                    :
                                </td>
                                <td style="width: 15px">
                                    <asp:TextBox ID="minute" runat="server" Font-Names="Tahoma" Font-Size="9pt" dir="ltr"
                                        onkeyup="changePlace(this, this.form);" Width="16px" MaxLength="2" BorderColor="White"
                                        BorderStyle="None"></asp:TextBox>
                                </td>
                                <td style="font-weight: bold; font-size: 8pt; font-family: tahoma; width: 5px">
                                    :
                                </td>
                                <td style="direction: ltr; width: 15px">
                                    <asp:TextBox ID="second" runat="server" Font-Names="Tahoma" Font-Size="9pt" dir="ltr"
                                        Width="15px" MaxLength="2" BorderColor="White" BorderStyle="None"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
