<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="CogniTutor.Profile" %>

<%@ Register TagPrefix="COG" TagName="NavigationBar" Src="~/UserControls/NavigationBar.ascx" %>
<%@ Register TagPrefix="COG" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="COG" TagName="LoginWindow" Src="~/UserControls/LoginWindow.ascx" %>

<!DOCTYPE html>
<html lang="en">

<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Profile</title>

    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.min.css?version=1" rel="stylesheet">

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

    
    <script>
        function picClick() {
            $('#FileUpload1').click();
        }

        function fileChosen() {
            if (this.value !== '') {
                $('#btnUpload').click();
            }
        }

        function mouseOverProfilePicture() {
            $('#edit').removeClass('hidden');
        }

        function mouseLeaveProfilePicture() {
            $('#edit').addClass('hidden');
        }

        function checkAboutMeEdit() {
            var goingToStatic = $('#divAboutMeDynamic').is(':visible');
            if (goingToStatic) {
                var r = confirm('Discard changes?');
                if (r == false)
                    return false;
            }
            return true;
        }

    </script>

</head>
    
<body onpageshow="setNamePadding()" onresize="setNamePadding()">
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <COG:NavigationBar runat="server" />
        <COG:LoginWindow runat="server" />

        <div class="col-lg-1"></div>
        <div class="container target col-lg-11">
            <div class="row float-bottom">
                <div class="col-sm-2 col-xs-5">
                    <asp:Image ID="Image1" class='img-rounded img-responsive <%= IsMyProfile?"cursor-pointer":"" %>' runat="server"
                        onclick='<%= IsMyProfile?"picClick()":"" %>' onmouseenter='mouseOverProfilePicture()' onmouseleave="mouseLeaveProfilePicture()"
                        />
                    <asp:Image class="hidden cursor-pointer" ID="edit" src="Images/edit.png" Style="position: absolute; bottom: 5px; right: 20px;" runat="server"
                        onclick="picClick()" onmouseenter="mouseOverProfilePicture()" onmouseleave="mouseLeaveProfilePicture()" />
                    <asp:FileUpload class="hidden" ID="FileUpload1" runat="server" onchange="fileChosen()" />
                    <asp:Button class="hidden" ID="btnUpload" runat="server" Text="Upload" OnClick="Upload" />
                </div>
                <div class="col-sm-10 col-xs-7">
                    <h1><%= theirPublicUserData.DisplayName %></h1>
                    <asp:Button runat="server" ID="btnSendMessage" OnClick="btnSendMessage_Click" class="btn btn-info" Text="Send a message" />
                </div>
            </div>
            <br>
            <div id="MainEditDiv" class="row" runat="server">
                <div class="col-sm-7" contenteditable="false" style="">
                    
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="panel-title-bold">
                                        About Me
                                        <span class="float-right">
                                            <asp:LinkButton ID="EditAboutMeBtn" OnClientClick="var r = checkAboutMeEdit(); if(r == false) return false;"
                                                OnClick="SetAboutMeVisibilities" class="btn-default btn-md" runat="server">
                                                <span class="glyphicon glyphicon-pencil"></span>
                                            </asp:LinkButton>
                                        </span>
                                    </div>
                                </div>
                                <div class="panel-body" id="divAboutMeBody" runat="server">
                                    <div id="divAboutMeStatic" runat="server">
                                        <p class="medium-text" style="white-space: pre-wrap"><%= theirTutor.Biography %></p>
                                        <%--<asp:Label Text='<%# UserField("About") %>' runat="server" />--%>
                                    </div>
                                    <div id="divAboutMeDynamic" visible="false" runat="server">
                                        <asp:TextBox ID="tbEditAboutMe" Height="200px" Text='<%# theirTutor.Biography %>'
                                            Width="100%" TextMode="MultiLine" Columns="50" Rows="5" MaxLength="8000" runat="server" />
                                    </div>
                                    <div id="divAboutMeButtons" runat="server">
                                        <span class="float-right">
                                            <asp:LinkButton ID="aboutMeCancelBtn" OnClientClick="var r = checkAboutMeEdit(); if(r == false) return false;"
                                                Visible="false" type="button" class="btn btn-default"
                                                Text="Cancel" OnClick="SetAboutMeVisibilities" runat="server" />
                                            <asp:LinkButton ID="aboutMeSaveChangesBtn" Visible="false" class="btn btn-primary"
                                                Text="Save Changes" OnClick="aboutMeSaveChangesBtn_Click" runat="server" />
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="panel-title-bold">
                                Activity
                            </div>
                        </div>
                        <div class="panel-body" id="div1" runat="server">
                            <div id="div2" runat="server">
                                <p class="medium-text" style="white-space: pre-wrap">Number of Questions Created: <%= theirTutor.NumQuestionsCreated %></p>
                                <p class="medium-text" style="white-space: pre-wrap">Number of Questions Reviewed: <%= theirTutor.NumQuestionsReviewed %></p>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <script src="/plugins/bootstrap-pager.js"></script>
        </div>

        <COG:Footer runat="server" />
    </form>
</body>

</html>
