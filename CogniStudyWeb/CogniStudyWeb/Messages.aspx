<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="Messages.aspx.cs" Inherits="CogniTutor.Messages" %>

<%@ Register TagPrefix="COG" TagName="NavigationBar" Src="~/UserControls/NavigationBar.ascx" %>
<%@ Register TagPrefix="COG" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="COG" TagName="LoginWindow" Src="~/UserControls/LoginWindow.ascx" %>
<%@ Register TagPrefix="COG" TagName="ConversationPanel" Src="~/UserControls/ConversationPanel.ascx" %>

<!DOCTYPE html>
<html lang="en">

<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Messages</title>

    <link rel="shortcut icon" type="image/x-icon" href="Images/CogniTutor5.jpg" />

    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="css/modern-business.css" rel="stylesheet">

    <!-- Custom Fonts -->
    <link href="font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

    <!-- jQuery -->
    <script src="js/jquery.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="js/bootstrap.min.js"></script>
    
    <!-- Our custom javascript -->
    <script src="js/Custom.js"></script>

    <!-- Chat Bubble CSS -->
    <link href="css/chat_bubble.css" rel="stylesheet">

    
    <style>
     
        html, 
body, form {
    height: 100%;
}
        #container {
    
    height:100%;
}
div.fill{
    height: auto;
}
.fill-rest {
    display: table;
}
.fill-rest > div{
    display: table-row;
    height: 0;
}
.fill-rest > div.fill{
    height: auto;
}

.fill-width{
    width: 100%;
}
.fill-height{
    height: 100%;
}
    </style>
    <script>
        function FixHeights() {
            $(document).ready(function () {
                $("#divContent").height($(window).height() - $("#divNavBar").height() - $("#divHeader").height());
                $("#pnlMessages").height($("#divContent").height() - $("#tbType").height() - $("#btnSend").height() - $("#pName").height() - 100);
                $("#pnlConversations").height($("#divContent").height() - $("#hConversations").height() - 100);
                var div = document.getElementById("<%=pnlMessages.ClientID%>");
                div.scrollTop = div.scrollHeight - div.clientHeight;
            });
        }
    </script>
    <script>
        $(document).ready(function () {
            $("#divContent").height($(window).height() - $("#divNavBar").height() - $("#divHeader").height());
            $("#pnlMessages").height($("#divContent").height() - $("#tbType").height() - $("#btnSend").height() - $("#pName").height() - 100);
            $("#pnlConversations").height($("#divContent").height() - $("#hConversations").height() - 100);
            var div = document.getElementById("<%=pnlMessages.ClientID%>");
            div.scrollTop = div.scrollHeight - div.clientHeight;

        });
        $(window).resize(function () {
            $("#divContent").height($(window).height() - $("#divNavBar").height() - $("#divHeader").height());
            $("#pnlMessages").height($("#divContent").height() - $("#tbType").height() - $("#btnSend").height() - $("#pName").height() - 100);
            $("#pnlConversations").height($("#divContent").height() - $("#hConversations").height() - 100);
            var div = document.getElementById("<%=pnlMessages.ClientID%>");
            div.scrollTop = div.scrollHeight - div.clientHeight;
        }).resize();
    </script>

    <script>
        function NewMessage() {
            $('#tbMessage').val($('#tbType').val());
            $('#newMessage').removeClass('hidden');
            $('#newText').html($('#tbType').val());
            $('#tbType').val('');
            $('#tbType').prop('disabled', true);
            var div = document.getElementById("<%=pnlMessages.ClientID%>");
            div.scrollTop = div.scrollHeight - div.clientHeight;
            //$('#newMessage').html(new Date());
        }
    </script>

</head>

<body>
    <form id="form1" runat="server" autocomplete="off" defaultbutton="btnSend">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
                    <!--Login modal-->
        <COG:LoginWindow runat="server" />
        <!-- Navigation bar -->
        <div id="divNavBar">
            <COG:NavigationBar runat="server"/>
        </div>

    <!-- Page Content -->
    <div id="container" class="container fill">

        <!-- Page Heading -->
        <div id="divHeader" class="row">
            <div class="col-lg-12">
                <h1 class="page-header">
                    Messages
                </h1>
            </div>
        </div>
                <div id="divContent">
                    <div class="row fill-height">
                
                        <div class="col-lg-5 fill-height" style="padding-right:20px; border-right: 1px solid #ccc;">
                            <p id="hConversations" class="large-text text-center">
                                Conversations
                            </p>
                    
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <script type="text/javascript">
                                                Sys.Application.add_load(FixHeights);
                                            </script>
                                        <asp:Panel runat="server" ID="pnlConversations" CssClass="fill-height" ScrollBars="Vertical">
                                            <asp:Repeater runat="server" ID="repConversations" OnItemCommand="repConversations_ItemCommand">
                                                <ItemTemplate>
                                                    <div class="row fill-width">
                                                        <asp:LinkButton class="btn btn-default fill-parent-width" runat="server" ID="btnConversation" CommandName="Change" CommandArgument='<%# Eval("TheirUserID") %>'>
                                                            <div class="col-sm-3 col-xs-3">
                                                                <asp:Image Height="75" Width="75" runat="server" ImageUrl='<%# Eval("ProfilePicUrl") %>' />
                                                            </div>
                                                            <div class="col-sm-9 col-xs-9">
                                                                <div class="row">
                                                                    <asp:Label ID="lbTheirName" CssClass="large-text" runat="server" Text='<%# Eval("TheirName") %>'></asp:Label>
                                                                </div>
                                                                <div class="row">
                                                                    <asp:Label runat="server" ID="lbLastMessage" class="medium-text" Text='<%# Eval("LastMessage") %>'></asp:Label>
                                                                    <asp:Label ID="lbTheirUserID" class="hidden" runat="server" Text='<%# Eval("TheirUserID") %>'></asp:Label>
                                                                </div>
                                                            </div>
                                                        </asp:LinkButton>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </asp:Panel>

                                    </ContentTemplate>
                                        </asp:UpdatePanel>


                        </div>

                        <div class="col-lg-7 fill-height" style="border-left: 1px solid #ccc;">
                            <p id="pName" class="large-text text-center"><%= TheirName %></p>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <script type="text/javascript">
                                        Sys.Application.add_load(FixHeights);
                                    </script>
                                    <asp:Timer runat="server" id="Timer1" Interval="10000" OnTick="Timer1_Tick"></asp:Timer>
                                    <asp:Panel runat="server" ID="pnlMessages" CssClass="fill-height" ScrollBars="Vertical">
                                        <div class="row fill-height fill-width">
                                            <div class="col-lg-1"></div>
                                            <div class="col-lg-10 chat fill-height">
                                                <asp:Repeater runat="server" ID="repMessages">
                                                    <ItemTemplate>
                                                        <div class="bubble <%# Convert.ToBoolean(Eval("WasSentByMe")) ? "me" : "you" %>">
                                                            <span class="medium-text"><%# Eval("Text") %></span><br />
                                                            <span class="small"><%# Eval("sentAt") %></span>
                                                        </div>
                                                                
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <div class="bubble me hidden" id="newMessage">
                                                    <span class="medium-text" id="newText"></span><br />
                                                    <span class="small" id="newTime"></span>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <br />
                            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox runat="server" ID="tbType" CssClass="form-control"></asp:TextBox>
                                    <asp:Button runat="server" ID="btnSend" Text="Send" OnClick="btnSend_Click" CssClass="btn btn-default pull-right" UseSubmitBehavior="true" OnClientClick="NewMessage();" />
                                    
                                    <asp:TextBox runat="server" ID="tbMessage" class="hidden"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>

                    </div>
                </div>         
    </div>
            
    </form>

</body>

</html>
