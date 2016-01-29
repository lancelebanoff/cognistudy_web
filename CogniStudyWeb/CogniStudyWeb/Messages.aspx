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

    <!-- Morris Charts CSS -->
    <link href="css/plugins/morris.css" rel="stylesheet">

    

</head>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
                    <!--Login modal-->
        <COG:LoginWindow runat="server" />
        <!-- Navigation bar -->
        <COG:NavigationBar runat="server"/>

    <!-- Page Content -->
    <div class="container">

        <div class="row">
            <div class="col-lg-12">
                <div id="wrapper">

                    <div id="page-wrapper">
                        <div class="container-fluid">

                            <!-- Page Heading -->
                            <div class="row">
                                <div class="col-lg-12">
                                    <h1 class="page-header">
                                        Messages
                                    </h1>
                                </div>
                            </div>
                            
                            <div class="row">
                                <div class="col-lg-5" style="padding-right:20px; border-right: 1px solid #ccc;">
                                    <h3 class="page-header center-block">
                                        Conversations
                                    </h3>


                                    
                                    <asp:Repeater runat="server" ID="repConversations" OnItemCommand="repConversations_ItemCommand">
                                        <ItemTemplate>
                                            <div class="row">
                                                <asp:LinkButton class="btn-block btn-default" runat="server" ID="btnConversation" CommandName="Change" CommandArgument='<%# Eval("TheirUserID") %>'>
                                                    <div class="col-sm-12 col-xs-12">
                                                        <asp:Label ID="lbTheirName" runat="server" Text='<%# Eval("TheirName") %>'></asp:Label>
                                                        <asp:Label runat="server" ID="lbLastMessage" class="medium-text" Text='<%# Eval("LastMessage") %>'></asp:Label>
                                                        <asp:Label ID="lbTheirUserID" class="hidden" runat="server" Text='<%# Eval("TheirUserID") %>'></asp:Label>
                                                    </div>
                                                </asp:LinkButton>
                                            </div>
                                            <hr />
                                        </ItemTemplate>
                                    </asp:Repeater>


                                </div>

                                <div class="col-lg-7" style="border-left: 1px solid #ccc;">
                                    <asp:UpdatePanel runat="server" ID="pnlMessages">
                                        <ContentTemplate>
                                            <p class="large-text"><%= TheirName %></p>
                                            <asp:Panel runat="server" ID="pnlScroll" ScrollBars="Vertical" Height="400px">
                                                <div class="col-lg-1"></div>
                                                <div class="col-lg-10">
                                                    <asp:Repeater runat="server" ID="repMessages">
                                                        <ItemTemplate>
                                                            <div class="row">
                                                                <p><%# Eval("Text") %></p>
                                                            </div>
                                                            <hr />
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>

                                            </asp:Panel>
                                            <asp:TextBox runat="server" ID="tbType" style="width: 100%"></asp:TextBox>
                                            <asp:Button runat="server" ID="btnSend" Text="Send" OnClick="btnSend_Click" CssClass="pull-right" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    

                                </div>
                            </div>


                        </div>
                        <!-- /.container-fluid -->

                    </div>
                    <!-- /#page-wrapper -->

                </div>
                <!-- /#wrapper -->

            </div>
        </div>
        <hr>
    </div>
            
    <!-- Footer -->
    <COG:Footer runat="server" />
    </form>

</body>

</html>
