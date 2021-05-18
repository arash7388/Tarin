<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShareDivPrint.aspx.cs" Inherits="Tashim.ShareDivPrint" %>
<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=13.0.19.116, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .borderAll {
            border: 1px solid black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <telerik:ReportViewer ID="ReportViewer1" runat="server" Width="100%"  
            ProgressText="در حال آماده سازی..." Height="596px"></telerik:ReportViewer>
    </form>
    <script type="text/javascript">
    ReportViewer.prototype.PrintReport = function () {
        switch (this.defaultPrintFormat) {
            case "Default":
                this.DefaultPrint();
                break;
            case "PDF":
                this.PrintAs("PDF");
                previewFrame = document.getElementById(this.previewFrameID);
                previewFrame.onload = function () { previewFrame.contentDocument.execCommand("print", true, null); }
                break;
        }
    };
</script>
</body>
</html>
