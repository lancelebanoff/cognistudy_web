<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProgressBarChart.ascx.cs" Inherits="CogniStudyWeb.UserControls.ProgressBarChart" %>


<asp:CHART id="Chart1" runat="server" Height="400px" Width="500px" BackColor="#D3DFF0" Palette="BrightPastel" BorderlineDashStyle="Solid" 
    BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="26, 59, 105">
    <Titles><asp:Title Font="Trebuchet MS, 10pt, style=Bold"></asp:Title></Titles>
	<legends>
		<asp:Legend TitleFont="Microsoft Sans Serif, 8pt, style=Bold" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" 
            IsTextAutoFit="False" Enabled="False" Name="Default"></asp:Legend>
	</legends>							
	<borderskin SkinStyle="Emboss"></borderskin>
	<series>
		<asp:Series Name="Series1" ChartType="StackedColumn" BorderColor="180, 26, 59, 105" Color="LightGreen" IsValueShownAsLabel="true"></asp:Series>
		<asp:Series Name="Series2" ChartType="StackedColumn" BorderColor="180, 26, 59, 105" Color="red" IsValueShownAsLabel="true"></asp:Series>
	</series>
	<chartareas>
		<asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="Transparent" 
            BackColor="64, 165, 191, 228" ShadowColor="Transparent" BackGradientStyle="TopBottom" Area3DStyle-Enable3D="true" >
			<area3dstyle Rotation="10" Inclination="7" WallWidth="0" Enable3D="true" />
			<position Y="15" Height="85" Width="92" X="2"></position>
			<axisy LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8" IsMarginVisible="false">
				<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
				<MajorGrid LineColor="64, 64, 64, 64" />
			</axisy>
			<axisx LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8" IsMarginVisible="false">
				<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
				<MajorGrid LineColor="64, 64, 64, 64" />
			</axisx>
		</asp:ChartArea>
	</chartareas>
</asp:CHART>