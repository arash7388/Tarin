<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel.Master" AutoEventWireup="true" CodeBehind="SendToSubscribers.aspx.cs" Inherits="AdminPanel.SendToSubscribers" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2014.2.724.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
     <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
     </telerik:RadAjaxManager>
     <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
         <Scripts>
             <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js">
             </asp:ScriptReference>
             <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js">
             </asp:ScriptReference>
             <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js">
             </asp:ScriptReference>
         </Scripts>
     </telerik:RadScriptManager>
     <h3>ارسال مطلب به مشترکین خبرنامه</h3>
                <div >
                    <telerik:RadEditor runat="server" ID="RadEditor1" Height="350px" DialogsCssFile="./RTL/RadEditor_Dialogs_RTL.css"
                        ContentAreaCssFile="./RTL/EditorContentArea_RTL.css">
                        <ImageManager ViewPaths="~/Editor/images/UserDir/Marketing,~/Editor/images/UserDir/PublicRelations"
                            UploadPaths="~/Editor/images/UserDir/Marketing,~/Editor/images/UserDir/PublicRelations"
                            DeletePaths="~/Editor/images/UserDir/Marketing,~/Editor/images/UserDir/PublicRelations" />
                        <FlashManager ViewPaths="~/Editor/images/UserDir/Marketing,~/Editor/images/UserDir/PublicRelations"
                            UploadPaths="~/Editor/images/UserDir/Marketing,~/Editor/images/UserDir/PublicRelations"
                            DeletePaths="~/Editor/images/UserDir/Marketing,~/Editor/images/UserDir/PublicRelations" />
                        <MediaManager ViewPaths="~/Editor/images/UserDir/Marketing,~/Editor/images/UserDir/PublicRelations"
                            UploadPaths="~/Editor/images/UserDir/Marketing,~/Editor/images/UserDir/PublicRelations"
                            DeletePaths="~/Editor/images/UserDir/Marketing,~/Editor/images/UserDir/PublicRelations" />
                        <DocumentManager ViewPaths="~/Editor/images/UserDir/Marketing,~/Editor/images/UserDir/PublicRelations"
                            UploadPaths="~/Editor/images/UserDir/Marketing,~/Editor/images/UserDir/PublicRelations"
                            DeletePaths="~/Editor/images/UserDir/Marketing,~/Editor/images/UserDir/PublicRelations" />
                        <TemplateManager ViewPaths="~/Editor/images/UserDir/Marketing,~/Editor/images/UserDir/PublicRelations"
                            UploadPaths="~/Editor/images/UserDir/Marketing,~/Editor/images/UserDir/PublicRelations"
                            DeletePaths="~/Editor/images/UserDir/Marketing,~/Editor/images/UserDir/PublicRelations"
                            SearchPatterns="*.html,*.html" />
                        <Modules>
                            <telerik:EditorModule Visible="false" />
                        </Modules>
                    </telerik:RadEditor>
                </div>
    <asp:Button runat="server" CssClass="btn btn-black" ID="btnSendToSubscribers" Text="ارسال" OnClick="btnSendToSubscribers_Click"/>
</asp:Content>
