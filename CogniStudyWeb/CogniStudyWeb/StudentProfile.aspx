<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="StudentProfile.aspx.cs" Inherits="CogniTutor.StudentProfile" %>

<%@ Register TagPrefix="COG" TagName="NavigationBar" Src="~/UserControls/NavigationBar.ascx" %>
<%@ Register TagPrefix="COG" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="COG" TagName="LoginWindow" Src="~/UserControls/LoginWindow.ascx" %>
<%@ Register TagPrefix="COG" TagName="AnalyticsPanel" Src="~/UserControls/AnalyticsPanel.ascx" %>

<!DOCTYPE html>
<html lang="en">

<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Profile</title>

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

        <div class="row">
            <div class="col-lg-1"></div>
            <div class="container target col-lg-10">
                <asp:Panel runat="server" class="alert alert-success" ID="pnlSuccess" Visible="false">
                    <strong>Success! </strong><asp:Label runat="server" ID="lbSuccess" Text="Your request was successfully sent to the student."></asp:Label>
                </asp:Panel>
                <hr class="">
                <div class="row float-bottom">
                    <div class="col-sm-2 col-xs-5">
                        <asp:Image ID="Image1" class="img-rounded img-responsive" runat="server" />
                    </div>
                    <div class="col-sm-10 col-xs-7">
                        <h1><%= StudentName %></h1>
                        <asp:Button runat="server" ID="btnRequestStudent" OnClick="btnRequestStudent_Click" class="btn btn-success" Text="Send request to student" />
                        <asp:Button runat="server" ID="btnAcceptStudent" OnClick="btnAcceptStudent_Click" class="btn btn-success" Text="Accept request from student"/>
                        <asp:Button runat="server" ID="btnRequestSent" class="btn btn-success disabled" Text="Request sent to student" />
                        <asp:Button runat="server" ID="btnStudentAdded" class="btn btn-success disabled" Text="Student added" />
                        <asp:Button runat="server" ID="btnSendMessage" OnClick="btnSendMessage_Click" class="btn btn-info" Text="Send a message" />
                        <asp:Button runat="server" ID="btnBlockStudent" OnClick="btnBlockStudent_Click" class="btn btn-danger hidden" Text="Block" />
                        <asp:Button runat="server" ID="btnRemoveStudent" OnClick="btnRemoveStudent_Click" class="btn btn-danger" Text="Remove student" />

                    </div>
                </div>
                <hr />
                
                <div id="MainEditDiv" class="row" runat="server">
                    <div class="col-sm-7" contenteditable="false" style="">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div class="panel-title-bold">
                                    Activity
                                </div>
                            </div>
                            <div class="panel-body" id="div1" runat="server">
                                <div id="div2" runat="server">
                                    <p class="medium-text" style="white-space: pre-wrap">Date started using CogniStudy: <%= PublicUserData.CreatedAt.Value.ToShortDateString() %></p>
                                    <p class="medium-text" style="white-space: pre-wrap">Number of Questions Answered: <%= StudentAllTimeAnswered() %></p>
                                </div>
                            </div>
                        </div>
                        

                    </div>
                </div>

                <asp:Panel class="row" runat="server" Visible="false" ID="pnlAnalyticsHolder">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div class="panel-title-bold">
                                    Analytics
                                </div>
                            </div>
                            <div class="panel-body" id="div5" runat="server">
                                <COG:AnalyticsPanel runat="server" id="pnlAnalytics" ShowStudentDropdown="false"></COG:AnalyticsPanel>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel class="row" runat="server" Visible="false" ID="pnlAssignedQuestions">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div class="panel-title-bold">
                                    Assigned Questions
                                </div>
                            </div>
                            <div class="panel-body" id="div3" runat="server">
                                <div id="div4" runat="server">
                                    <asp:GridView ID="grdAssignedQuestions" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Subject/Category">
                                                <ItemTemplate>
                                                    <%# Eval("question.subject") + " - " + Eval("question.category") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Question">
                                                <ItemTemplate>
                                                    <%# Eval("question.questionContents.questionText") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Answers">
                                                <ItemTemplate>
                                                    <%# Eval("question.questionContents.FriendlyAnswers") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Student Response">
                                                <ItemTemplate>
                                                    <%# Convert.ToBoolean(Eval("answered")) ? (char)(((Parse.ParseObject)Eval("response")).Get<int>("selectedAnswer") + 'A') + "" : "Not answered" %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Correct Answer">
                                                <ItemTemplate>
                                                    <%# (char)((int)Eval("question.questionContents.correctAnswer") + 'A') + "" %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>


            </div>
        </div>

    <!-- Footer -->
    <COG:Footer runat="server" />
    </form>

</body>

</html>
