<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DoughnutChart.ascx.cs" Inherits="CogniTutor.UserControls.DoughnutChart" %>

<asp:CHART id="Chart1" runat="server" Palette="BrightPastel" 
    BackColor="#D3DFF0" Height="296px" 
	Width="412px" BorderlineDashStyle="Solid" BackGradientStyle="TopBottom" 
    BorderWidth="2" 
	BorderColor="26, 59, 105" IsSoftShadows="False" 
    ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)">
	<legends>
		<asp:Legend TitleFont="Microsoft Sans Serif, 8pt, style=Bold" 
            BackColor="Transparent" 
			IsEquallySpacedItems="True" Font="Trebuchet MS, 8pt, style=Bold" 
            IsTextAutoFit="False" 
			Name="Default"></asp:Legend>
	</legends>
	<borderskin SkinStyle="Emboss"></borderskin>
	<series>
		<asp:Series ChartArea="Area1" XValueType="String" Name="Series1" 
            ChartType="Doughnut" IsValueShownAsLabel="True"
			Font="Trebuchet MS, 8.25pt, style=Bold" 
			CustomProperties="DoughnutRadius=60, PieDrawingStyle=Concave, CollectedLabel=Other, MinimumRelativePieSize=20" 
			MarkerStyle="Circle" BorderColor="64, 64, 64, 64" 
            Color="180, 65, 140, 240" YValueType="Int32" Label="#LABEL">
			<points>
				<asp:DataPoint LegendText="Correct" YValues="40" 
                    Color="LightGreen" Label="#VAL Correct" />
				<asp:DataPoint LegendText="Incorrect" YValues="60" 
                    Color="red" Label="#VAL Incorrect" />
			</points>
		</asp:Series>
	</series>
	<chartareas>
		<asp:ChartArea Name="Area1" BorderColor="64, 64, 64, 64" BackSecondaryColor="Transparent" 
			BackColor="Transparent" ShadowColor="Transparent" BackGradientStyle="TopBottom">
		</asp:ChartArea>
	</chartareas>
</asp:CHART>