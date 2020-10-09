<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel.Master" AutoEventWireup="true" CodeBehind="Post.aspx.cs" Inherits="AdminPanel.Post" %>

<%@ Import Namespace="MTO" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2014.2.724.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">

    <br />
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js"></asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js"></asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js"></asp:ScriptReference>
        </Scripts>
    </telerik:RadScriptManager>
    <div class="row">
        <div class="col-md-10">
            <h3>مطالب</h3>
        </div>
    </div>

    <div class="row">
        <div class="col-md-1" align="left">
            کد :
        </div>
        <div class="col-md-11">
            <asp:TextBox runat="server" ID="txtCode"></asp:TextBox>
        </div>

    </div>

    <div class="row">
        <div class="col-md-1" align="left">
            عنوان :
        </div>
        <div class="col-md-11">
            <asp:TextBox runat="server" ID="txtTitle"></asp:TextBox>
        </div>

    </div>

    <div class="row">
        <div class="col-md-1" align="left" style="padding-right: 0px;">
            تصویر :
        </div>
        <div class="col-md-11">
            <asp:Image runat="server" ID="imgPost" CssClass="imgPost" />
            <asp:FileUpload ID="fileUploadControl" runat="server" BorderStyle="Solid" EnableTheming="True" />
            <asp:Label runat="server" ID="statusLabel" Text="" />
        </div>

    </div>
    <br />

    <div class="row">
        <div class="col-md-1" align="left" style="padding-right: 0px;">
            تگ ها :
        </div>
        <div class="col-md-3">
            <asp:TreeView ID="treeViewTags" SkipLinkText="" runat="server" Width="150" ShowCheckBoxes="All" BorderColor="Black" BorderStyle="Solid">
            </asp:TreeView>
        </div>

    </div>
    <br />
    <div class="row">
        <div class="col-md-10">
            <div>
                <telerik:RadEditor runat="server" ID="RadEditorContext" Height="450px">
                    <%--<Tools>
                        <telerik:EditorToolGroup Tag="FileManagers">
                            <telerik:EditorTool Name="ImageManager">
                                
                            </telerik:EditorTool>

                        </telerik:EditorToolGroup>
                        <telerik:EditorToolGroup>
                            <telerik:EditorTool Name="Bold"></telerik:EditorTool>
                            <telerik:EditorTool Name="Italic"></telerik:EditorTool>
                            <telerik:EditorTool Name="Underline"></telerik:EditorTool>
                            <telerik:EditorSeparator></telerik:EditorSeparator>
                            <telerik:EditorTool Name="ForeColor"></telerik:EditorTool>
                            <telerik:EditorTool Name="BackColor"></telerik:EditorTool>
                            <telerik:EditorSeparator></telerik:EditorSeparator>
                            <telerik:EditorTool Name="FontName"></telerik:EditorTool>
                            <telerik:EditorTool Name="RealFontSize"></telerik:EditorTool>
                        </telerik:EditorToolGroup>
                    </Tools>--%>

                    <ImageManager ViewPaths="~/Editor/PostImages/"
                        UploadPaths="~/Editor/PostImages"
                        DeletePaths="~/Editor/PostImages"
                        SearchPatterns="*.jpeg,*.jpg,*.png,*.gif,*.bmp"
                        EnableAsyncUpload="true" 
                        MaxUploadFileSize="500000"/>

                    <FlashManager ViewPaths="~/Editor/PostFlashes/"
                        UploadPaths="~/Editor/PostFlashes"
                        DeletePaths="~/Editor/PostFlashes"
                        EnableAsyncUpload="true" 
                        MaxUploadFileSize="500000"/>

                    <MediaManager ViewPaths="~/Editor/PostMedias/"
                        UploadPaths="~/Editor/PostMedias"
                        DeletePaths="~/Editor/PostMedias"
                        EnableAsyncUpload="true" 
                        MaxUploadFileSize="1048576"/>

                    <DocumentManager ViewPaths="~/Editor/PostDocuments/"
                        UploadPaths="~/Editor/PostDocuments"
                        DeletePaths="~/Editor/PostDocuments"
                        EnableAsyncUpload="true" 
                        MaxUploadFileSize="500000"/>

                    <TemplateManager ViewPaths="~/Editor/PostTemplates/"
                        UploadPaths="~/Editor/PostTemplates"
                        DeletePaths="~/Editor/PostTemplates"
                        EnableAsyncUpload="true" 
                        MaxUploadFileSize="500000"/>


                </telerik:RadEditor>
            </div>
            <asp:Button runat="server" CssClass="btn btn-black" ID="btnSave" Text="ذخیره" OnClick="btnSave_Click" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-10">
            <asp:Label runat="server" ID="lblResult"></asp:Label>
        </div>
    </div>

</asp:Content>
