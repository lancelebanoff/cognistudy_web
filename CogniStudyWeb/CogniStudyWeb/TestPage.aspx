<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestPage.aspx.cs" Inherits="CogniStudyWeb.TestPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        html, 
body, form {
    height: 100%;
}
        #container {
    display: table;
    width: 100%;
    height: 100%;
    background-color:red;
}
#container > div{
    display: table-row;
    height: 0;
}
#container > div.fill{
    height: auto;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        
          <div id="container" class="fill">
          </div>
        <div id="div3">
        </div>
    </form>
</body>
</html>
