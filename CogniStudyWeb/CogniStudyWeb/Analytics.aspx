<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Analytics.aspx.cs" Inherits="CogniTutor.Analytics" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

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
                                        Analytics
                                    </h1>
                                    <ol class="breadcrumb hidden">
                                        <li class="active">
                                            <i class="fa fa-dashboard"></i> Analytics
                                        </li>
                                    </ol>
                                </div>
                            </div>
                            <!-- /.row -->
                            <asp:Chart ID="Chart2" runat="server">
                                <Series>
                                    <asp:Series Name="Series1"></asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:DropDownList ID="ddlFilterSubject" runat="server" OnSelectedIndexChanged="ddlFilterSubject_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    <asp:CHART id="Chart1" runat="server" Palette="BrightPastel" BackColor="#D3DFF0" Height="296px" 
                                        Width="412px" BorderlineDashStyle="Solid" BackGradientStyle="TopBottom" BorderWidth="2" 
                                        BorderColor="26, 59, 105" IsSoftShadows="False" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)">
							            <legends>
								            <asp:Legend TitleFont="Microsoft Sans Serif, 8pt, style=Bold" BackColor="Transparent" 
                                                IsEquallySpacedItems="True" Font="Trebuchet MS, 8pt, style=Bold" IsTextAutoFit="False" 
                                                Name="Default"></asp:Legend>
							            </legends>
							            <borderskin SkinStyle="Emboss"></borderskin>
							            <series>
								            <asp:Series ChartArea="Area1" XValueType="String" Name="Series1" ChartType="Doughnut" IsValueShownAsLabel="True"
                                                Font="Trebuchet MS, 8.25pt, style=Bold" 
                                                CustomProperties="DoughnutRadius=60, PieDrawingStyle=Concave, CollectedLabel=Other, MinimumRelativePieSize=20" 
                                                MarkerStyle="Circle" BorderColor="64, 64, 64, 64" Color="180, 65, 140, 240" YValueType="Int32" Label="#LABEL">
									            <points>
										            <asp:DataPoint LegendText="Correct" YValues="40" Color="LightGreen" Label="#VAL Correct" />
										            <asp:DataPoint LegendText="Incorrect" YValues="60" Color="red" Label="#VAL Incorrect" />
									            </points>
								            </asp:Series>
							            </series>
							            <chartareas>
								            <asp:ChartArea Name="Area1" BorderColor="64, 64, 64, 64" BackSecondaryColor="Transparent" 
                                                BackColor="Transparent" ShadowColor="Transparent" BackGradientStyle="TopBottom">
								            </asp:ChartArea>
							            </chartareas>
						            </asp:CHART>

                                    
						<asp:CHART id="Chart3" runat="server" Height="296px" Width="412px" BackColor="#D3DFF0" Palette="BrightPastel" BorderlineDashStyle="Solid" BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="26, 59, 105">
							<legends>
								<asp:Legend TitleFont="Microsoft Sans Serif, 8pt, style=Bold" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Enabled="False" Name="Default"></asp:Legend>
							</legends>							
							<borderskin SkinStyle="Emboss"></borderskin>
							<series>
								<asp:Series Name="Series1" ChartType="StackedArea100" BorderColor="180, 26, 59, 105" Color="220, 65, 140, 240"></asp:Series>
								<asp:Series Name="Series2" ChartType="StackedArea100" BorderColor="180, 26, 59, 105" Color="220, 252, 180, 65"></asp:Series>
								<asp:Series Name="Series3" ChartType="StackedArea100" BorderColor="180, 26, 59, 105" Color="220, 224, 64, 10"></asp:Series>
								<asp:Series Name="Series4" ChartType="StackedArea100" BorderColor="180, 26, 59, 105" Color="220, 5, 100, 146"></asp:Series>
							</series>
							<chartareas>
								<asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="Transparent" BackColor="64, 165, 191, 228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
									<area3dstyle Rotation="10" Inclination="15" WallWidth="0" />
									<position Y="3" Height="92" Width="92" X="2"></position>
									<axisy LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8">
										<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
										<MajorGrid LineColor="64, 64, 64, 64" />
									</axisy>
									<axisx LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8">
										<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
										<MajorGrid LineColor="64, 64, 64, 64" />
									</axisx>
								</asp:ChartArea>
							</chartareas>
						</asp:CHART>
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
