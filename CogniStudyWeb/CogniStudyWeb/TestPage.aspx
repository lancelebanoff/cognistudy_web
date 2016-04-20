<%@ Page Async="true" Language="C#" AutoEventWireup="true" Inherits="CogniTutor.TestPage" Codebehind="TestPage.aspx.cs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel runat="server" id="UpdatePanel1">
<ContentTemplate>
<asp:Label runat="server" Text="Page not refreshed yet." id="Label1">
</asp:Label>
</ContentTemplate>
</asp:UpdatePanel>
<asp:Label runat="server" Text="Label" id="Label2"></asp:Label>
    </form>
</body>
</html>
