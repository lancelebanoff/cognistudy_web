<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="CogniTutor.Dashboard" %>

<%@ Register TagPrefix="COG" TagName="NavigationBar" Src="~/UserControls/NavigationBar.ascx" %>
<%@ Register TagPrefix="COG" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="COG" TagName="LoginWindow" Src="~/UserControls/LoginWindow.ascx" %>
<%@ Register Assembly="DelayedSubmit" Namespace="DelayedSubmit" TagPrefix="cc1"%>

<!DOCTYPE html>
<html lang="en">

<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Dashboard</title>

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
    <form id="form1" runat="server" autocomplete="false">
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
                                        My Students
                                    </h1>
                                    <ol class="breadcrumb hidden">
                                        <li class="active">
                                            <i class="fa fa-dashboard"></i> My Students
                                        </li>
                                    </ol>
                                </div>
                            </div>
                            <!-- /.row -->

                            <div class="row">
                                <div class="col-lg-12">
                                </div>
                            </div>
                            
                            <div class="row">
                                <div class="col-lg-6">
                                    <p class="large-text">
                                        Hi <%= PublicUserData.DisplayName %>!
                                    </p>
                                    <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
                                    <br />
                                    <p class="medium-text">
                                        
                                    </p>
                                    <asp:GridView CssClass="table table-hover table-striped" GridLines="None" ID="grdMyStudents" runat="server" 
                                        OnRowCommand="grdMyStudents_RowCommand" DataKeyNames="ObjectId" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="DisplayName" HeaderText="Student Name"/>
                                            <asp:ButtonField Text="Message" CommandName="Message" ButtonType="Button" ControlStyle-CssClass="btn btn-default align-center btn"/>
                                            <asp:ButtonField Text="See Profile" CommandName="SeeProfile" ButtonType="Button" ControlStyle-CssClass="btn btn-default align-center"  />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-lg-1">
                                </div>
                                <div class="col-lg-4">
                                    <h4 class="h4">Search Users</h4>
                                    <cc1:DelayedSubmitExtender ID="DisableButtonExtender1" runat="server" Timeout="1" TargetControlID="TextBox1"/>
                                    <asp:TextBox ID="TextBox1" CssClass="form-control input-lg fill-parent-width" placeholder="Type Student's Name..." runat="server" AutoPostBack="True" OnTextChanged="TextBox1_TextChanged" Columns="50" autocomplete="false"></asp:TextBox>        
                                    <br />
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="TextBox1" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:Repeater ID="Repeater1" runat="server">
                                                <HeaderTemplate><table class="table"></HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <a href="<%# (CogniTutor.Constants.UserType.IsTutor(Eval("UserType").ToString()) ? 
                                                            "Profile.aspx?tutorId=" : "StudentProfile.aspx?StudentId=") + Eval("ObjectId").ToString() %>" 
                                                                class="btn btn-default fill-parent-width">
                                                                <div class="pull-left">
                                                                    <asp:Image Height="75" Width="75" runat="server" ImageUrl='<%# Eval("profilePic.Url") ?? "Images/default_prof_pic.png" %>' />
                                                                    <%# Eval("DisplayName") %>
                                                                </div>
                                                            </a>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate></table></FooterTemplate>
                                            </asp:Repeater>
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
