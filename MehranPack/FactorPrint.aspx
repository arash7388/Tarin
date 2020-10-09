<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FactorPrint.aspx.cs" Inherits="MehranPack.FactorPrint" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.ReportViewer.WebForms" Assembly="Telerik.ReportViewer.WebForms, Version=9.0.15.225, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>چاپ فاکتور</title>
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
        <telerik:ReportViewer ID="ReportViewer1" runat="server" Width="100%"  ProgressText="در حال آماده سازی..." Height="596px"></telerik:ReportViewer>
    </form>
</body>
</html>
