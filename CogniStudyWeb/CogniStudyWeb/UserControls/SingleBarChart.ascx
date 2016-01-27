<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleBarChart.ascx.cs" Inherits="CogniTutor.UserControls.SingleBarChart" %>

<asp:CHART id="Chart2" runat="server" Height="50px" Width="412px" BackColor="#D3DFF0" Palette="BrightPastel" 
    BorderlineDashStyle="Solid" BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="26, 59, 105">
	<legends>
		<asp:Legend TitleFont="Microsoft Sans Serif, 8pt, style=Bold" BackColor="Transparent" 
            Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Enabled="False" Name="Default"></asp:Legend>
	</legends>							
	<borderskin SkinStyle="Emboss"></borderskin>
	<series>
		<asp:Series Name="Series1" ChartType="StackedBar100" BorderColor="180, 26, 59, 105" Color="LightGreen" 
            IsValueShownAsLabel="True" Label="#VAL"></asp:Series>
		<asp:Series Name="Series2" ChartType="StackedBar100" BorderColor="180, 26, 59, 105" Color="Red" 
            IsValueShownAsLabel="True" Label="#VAL"></asp:Series>
	</series>
	<chartareas>
		<asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="Transparent" 
            BackColor="64, 165, 191, 228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
			<area3dstyle Rotation="3" Inclination="15" WallWidth="0" Enable3D="true" />
			<position Y="3" Height="92" Width="92" X="2"></position>
			<axisy LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8" Enabled="False">
				<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
				<MajorGrid LineColor="64, 64, 64, 64" />
			</axisy>
			<axisx LineColor="64, 64, 64, 64"  LabelAutoFitMaxFontSize="8" IsMarginVisible="false" Enabled="False">
				<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
				<MajorGrid LineColor="64, 64, 64, 64" />
			</axisx>
		</asp:ChartArea>
	</chartareas>
</asp:CHART>