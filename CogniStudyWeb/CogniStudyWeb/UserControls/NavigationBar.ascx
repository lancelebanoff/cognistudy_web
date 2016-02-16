<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavigationBar.ascx.cs" Inherits="CogniTutor.NavigationBar" %>

<!-- Navigation -->
<nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
    <div class="container">
        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="navbar-header">
            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                <span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </button>
            <a class="navbar-brand" href="../Default.aspx">CogniStudy</a>
        </div>
        <!-- Collect the nav links, forms, and other content for toggling -->
        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
            <% if(!mPage.LoggedIn) { %>
            <ul class="nav navbar-nav navbar-right">
                <li>
                    <!-- Changed because incomplete-->
                    <a style="cursor:pointer;" href="../Register.aspx">
                        REGISTER
                    </a>
                </li>
                <li>
                    <a style="cursor:pointer;" id="loginpopup">
                        LOGIN
                    </a>
                </li>
            </ul>
            <% } else { %>
            <ul class="nav navbar-nav navbar-left">
                <li class="dropdown">
                    <a href="../Analytics.aspx">Analytics</a>
                </li>
                <li>
                    <a href="../QuestionArena.aspx">Question Arena</a>
                </li>
                <li>
                    <a href="../SuggestQuestion.aspx">Suggest Question</a>
                </li>
            </ul>
            <ul class="nav navbar-nav navbar-right">
                <li>
                    <a href="Messages"><i class="fa fa-fw fa-envelope"></i></a>
                </li>
                <li>
                    <a href="#"><i class="fa fa-fw fa-bell"></i></a>
                </li>
                <li class="dropdown">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="fa fa-user"></i> <%= mPage.Name %> <b class="caret"></b></a>
                    <ul class="dropdown-menu">
                        <li>
                            <a href="Dashboard.aspx"><i class="fa fa-fw fa-dashboard"></i> Dashboard</a>
                        </li>
                        <li>
                                <a href="Profile.aspx"><i class="fa fa-fw fa-user"></i> Edit Profile</a>
                        </li>
                        <li>
                            <a href="Settings"><i class="fa fa-fw fa-gear"></i> Settings</a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <asp:LinkButton runat="server" id="btnLogout" OnClick="LogOut"><i class="fa fa-fw fa-power-off"></i> Log Out </asp:LinkButton>
                        </li>
                    </ul>
                </li>
            </ul>
            <% } %>
        </div>
        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
            
        </div>
        <!-- /.navbar-collapse -->
    </div>
    <!-- /.container -->
</nav>