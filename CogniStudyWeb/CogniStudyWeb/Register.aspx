<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="CogniTutor.Register" %>

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

    <title>Registration</title>

    <link rel="shortcut icon" type="image/x-icon" href="Images/CogniTutor5.jpg" />

    <!-- Bootstrap Core CSS -->
    <link href="/css/bootstrap.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="/css/modern-business.css" rel="stylesheet">

    <!-- Custom Fonts -->
    <link href="/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

    <!-- jQuery -->
    <script src="/js/jquery.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="/js/bootstrap.min.js"></script>

    <!-- Our custom javascript -->
    <script src="/js/Custom.js"></script>

</head>

<body>

    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server" />

        <COG:NavigationBar runat="server" />

        <COG:LoginWindow runat="server" />

        <!-- Page Content -->
        <div class="container">

            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Tutor Registration</h1>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-12">
                    <asp:Label ID="lblError" runat="server" ForeColor="Red" />
                </div>
            </div>

            <br />
                
            <div id="divForm">
                <div class="row">
                    <div class="col-lg-4">
                        <div class="form-group">
                            <label for="tbFirstName">First Name</label>
                            <asp:TextBox CssClass="form-control" ID="tbFirstName" placeholder="Enter First Name" runat="server" />
                        </div>
                        <div class="form-group">
                            <label for="tbLastName">Last Name</label>
                            <asp:TextBox CssClass="form-control" ID="tbLastName" placeholder="Enter Last Name" runat="server" />
                        </div>
                        <div class="form-group">
                            <label for="tbEmail">Email address</label>
                            <asp:TextBox CssClass="form-control" ID="tbEmail" placeholder="Enter Email" runat="server" />
                        </div>
                        <div class="form-group">
                            <label for="tbPassword">Password</label>
                            <asp:TextBox CssClass="form-control" ID="tbPassword" placeholder="Enter Password" runat="server" type="password" />
                        </div>
                        <div class="form-group">
                            <label for="tbPasswordRetype">Retype Password</label>
                            <asp:TextBox CssClass="form-control" ID="tbPasswordRetype" placeholder="Retype Password" runat="server" type="password" />
                        </div>
                        
                        <div class="hidden">
                            
                        <div class="form-group">
                            <label for="tbPhoneNumber">Phone Number</label>
                            <asp:TextBox CssClass="form-control" ID="tbPhoneNumber" placeholder="Phone Number" runat="server" />
                        </div>
                        <div class="form-group">
                            <label for="tbStreetAddress">Street Address</label>
                            <asp:TextBox CssClass="form-control" ID="tbStreetAddress" placeholder="Address" runat="server" />
                        </div>
                        <div class="form-group">
                            <label for="tbAddress2">Address Line 2</label>
                            <asp:TextBox CssClass="form-control" ID="tbAddress2" placeholder="Address 2" runat="server" />
                        </div>
                        <div class="form-group">
                            <label for="tbCity">City</label>
                            <asp:TextBox CssClass="form-control" ID="tbCity" placeholder="City" runat="server" />
                        </div>
                        <div class="form-group">
                            <label for="ddState">State</label>
                            <br />
                            <asp:DropDownList ID="ddState" Height="30px" runat="server" CssClass="form-control">
                                <asp:ListItem Value="">Select State</asp:ListItem>
                                <asp:ListItem Value="AL">Alabama</asp:ListItem>
                                <asp:ListItem Value="AK">Alaska</asp:ListItem>
                                <asp:ListItem Value="AZ">Arizona</asp:ListItem>
                                <asp:ListItem Value="AR">Arkansas</asp:ListItem>
                                <asp:ListItem Value="CA">California</asp:ListItem>
                                <asp:ListItem Value="CO">Colorado</asp:ListItem>
                                <asp:ListItem Value="CT">Connecticut</asp:ListItem>
                                <asp:ListItem Value="DC">District of Columbia</asp:ListItem>
                                <asp:ListItem Value="DE">Delaware</asp:ListItem>
                                <asp:ListItem Value="FL">Florida</asp:ListItem>
                                <asp:ListItem Value="GA">Georgia</asp:ListItem>
                                <asp:ListItem Value="HI">Hawaii</asp:ListItem>
                                <asp:ListItem Value="ID">Idaho</asp:ListItem>
                                <asp:ListItem Value="IL">Illinois</asp:ListItem>
                                <asp:ListItem Value="IN">Indiana</asp:ListItem>
                                <asp:ListItem Value="IA">Iowa</asp:ListItem>
                                <asp:ListItem Value="KS">Kansas</asp:ListItem>
                                <asp:ListItem Value="KY">Kentucky</asp:ListItem>
                                <asp:ListItem Value="LA">Louisiana</asp:ListItem>
                                <asp:ListItem Value="ME">Maine</asp:ListItem>
                                <asp:ListItem Value="MD">Maryland</asp:ListItem>
                                <asp:ListItem Value="MA">Massachusetts</asp:ListItem>
                                <asp:ListItem Value="MI">Michigan</asp:ListItem>
                                <asp:ListItem Value="MN">Minnesota</asp:ListItem>
                                <asp:ListItem Value="MS">Mississippi</asp:ListItem>
                                <asp:ListItem Value="MO">Missouri</asp:ListItem>
                                <asp:ListItem Value="MT">Montana</asp:ListItem>
                                <asp:ListItem Value="NE">Nebraska</asp:ListItem>
                                <asp:ListItem Value="NV">Nevada</asp:ListItem>
                                <asp:ListItem Value="NH">New Hampshire</asp:ListItem>
                                <asp:ListItem Value="NJ">New Jersey</asp:ListItem>
                                <asp:ListItem Value="NM">New Mexico</asp:ListItem>
                                <asp:ListItem Value="NY">New York</asp:ListItem>
                                <asp:ListItem Value="NC">North Carolina</asp:ListItem>
                                <asp:ListItem Value="ND">North Dakota</asp:ListItem>
                                <asp:ListItem Value="OH">Ohio</asp:ListItem>
                                <asp:ListItem Value="OK">Oklahoma</asp:ListItem>
                                <asp:ListItem Value="OR">Oregon</asp:ListItem>
                                <asp:ListItem Value="PA">Pennsylvania</asp:ListItem>
                                <asp:ListItem Value="RI">Rhode Island</asp:ListItem>
                                <asp:ListItem Value="SC">South Carolina</asp:ListItem>
                                <asp:ListItem Value="SD">South Dakota</asp:ListItem>
                                <asp:ListItem Value="TN">Tennessee</asp:ListItem>
                                <asp:ListItem Value="TX">Texas</asp:ListItem>
                                <asp:ListItem Value="UT">Utah</asp:ListItem>
                                <asp:ListItem Value="VT">Vermont</asp:ListItem>
                                <asp:ListItem Value="VA">Virginia</asp:ListItem>
                                <asp:ListItem Value="WA">Washington</asp:ListItem>
                                <asp:ListItem Value="WV">West Virginia</asp:ListItem>
                                <asp:ListItem Value="WI">Wisconsin</asp:ListItem>
                                <asp:ListItem Value="WY">Wyoming</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="tbZipCode">Zip Code</label>
                            <asp:TextBox CssClass="form-control" ID="tbZipCode" placeholder="Zip Code" runat="server" />
                        </div>
                        <div class="form-group">
                            <asp:CheckBox ID="cbTerms" runat="server" Text="I have read and agree to the " />
                            <a href="TermsOfUse.aspx" target="_blank">Terms and Conditions.</a>
                        </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-4">
                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-lg btn-success" Text="Submit" OnClick="btnSubmit_Click" />
                    </div>
                </div>
                <!-- Hidden because incomplete
                <hr />
                Want to register as a student?  <a href="RegisterStudent.aspx">Register here</a>
                    -->
                <br />
                <br />
                <br />
            </div>
            <!-- /.divForm -->
        </div>
        <!-- /.container -->
            
        <!-- Footer -->
        <COG:Footer runat="server" />

    </form>


</body>

</html>
