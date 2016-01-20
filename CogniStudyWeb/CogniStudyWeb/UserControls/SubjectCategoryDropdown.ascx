<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubjectCategoryDropdown.ascx.cs" Inherits="CogniTutor.UserControls.SubjectCategoryDropdown" %>

<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <asp:Label runat="server" Text="Subject"/><br />
        <asp:DropDownList ID="ddlSubject" runat="server" AutoPostBack="true" 
            OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged"></asp:DropDownList><br /><br />
        <asp:Label runat="server" Text="Category"/><br />
        <asp:DropDownList ID="ddlCategory" runat="server"></asp:DropDownList><br /><br />
                                            
    </ContentTemplate>
</asp:UpdatePanel>