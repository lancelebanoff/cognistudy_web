<%@ Page Title="Home Page" Async="true" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CogniTutor._Default" %>

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

    <title>CogniStudy</title>

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
	<link href='homepage/css/style.css' rel='stylesheet' type='text/css' />
	<link href='http://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css'>
    <script type="text/javascript" src="homepage/js/move-top.js"></script>
    <script type="text/javascript" src="homepage/js/easing.js"></script>
	<script type="text/javascript">
	    jQuery(document).ready(function ($) {
	        $(".scroll").click(function (event) {
	            event.preventDefault();
	            $('html,body').animate({ scrollTop: $(this.hash).offset().top }, 1200);
	        });
	    });
	</script>

</head>

<body>

    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server" />

        <COG:NavigationBar runat="server" />

        <COG:LoginWindow runat="server" />
        
	<!---start-image-banner---->
	<div class="img-banner" id="home">
		<div class="img-banner-info">
			<h1><span>Cogni</span>Study</h1>
			<h2>Practice for the SAT/ACT anywhere</h2>
			<p class="">The most entertaining app on the market for test preparation. You'll always come back to study more.</p>
			<div class="clear"> </div>
			<div class="info-btns">
				<ul>
					<li class="hidden"><a href="#"> </a></li>
					<li><a href="https://play.google.com/apps/testing/com.cognitutor.cognistudyapp"> </a></li>
				</ul>
			</div>
		</div>
		<div class="img-banner-pic">
		</div>
		<div class="clear"> </div>
	</div>

	<!---//End-image-banner---->
	<!---start-top-grids----->
	<div class="top-grids hidden" id="features">
	<div class="wrap">
		<div class="top-grid">
			<a class="icon1" href="#"><span> </span> </a>
			<h3>Notifications</h3>
			<p>Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium,<label> et quasi architecto beatae vitae dicta sunt explicabo.</label></p>
			<a class="btn" href="#">Read more<span> </span></a>
		</div>
		<div class="top-grid">
			<a class="icon2" href="#"><span> </span> </a>
			<h3>Backup data</h3>
			<p>Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium,<label> et quasi architecto beatae vitae dicta sunt explicabo.</label></p>
			<a class="btn" href="#">Read more<span> </span></a>
		</div>
		<div class="top-grid">
			<a class="icon3" href="#"><span> </span> </a>
			<h3>Up to 5gb storage</h3>
			<p>Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium,<label> et quasi architecto beatae vitae dicta sunt explicabo.</label></p>
			<a class="btn" href="#">Read more<span> </span></a>
		</div>
		<div class="clear"> </div>
	</div>
	</div>
	<!---//End-top-grids----->
	<!---start-mid-grids----->
	<div class="mid-grids" id="about">
		<div class="mid-grid-top">
		<div class="wrap">
			<div class="mid-grid-top-pic">
			</div>
			<div class="mid-grid-top-info">
				<h3>Make studying <strong>addicting</strong>!</h3>
				<p>Challenge your friends in a battle to answer the most questions correctly!</p>
			</div>
			<div class="clear"> </div>
		</div>
		</div>
		<div class="mid-grids-center hidden" id="demo">
			<div class="wrap">
				<h3>Demo of our cloud app</h3>
				<p> quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur,</p>
				<iframe src="//player.vimeo.com/video/73528012?byline=0" width="80%" height="500" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
			</div>
		</div>
		<div class="mid-grids-bottom hidden" id="contact">
			<div class="wrap">
				<h3>Sign up and You'll be Notified when it's Done !</h3>
				<form>
					<input type="text" class="textbox" value="Your email plase?" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Your email plase?';}"><input type="submit" value="Send" />
				</form>
			</div>
		</div>
	<!---//End-mid-grids----->
	<!---start-footer----->
	<div class="footer">
		<div class="wrap">
			<script type="text/javascript">
			    $(document).ready(function () {
			        /*
                    var defaults = {
                        containerID: 'toTop', // fading element id
                        containerHoverID: 'toTopHover', // fading element hover id
                        scrollSpeed: 1200,
                        easingType: 'linear' 
                    };
                    */

			        $().UItoTop({ easingType: 'easeOutQuart' });

			    });
		</script>
    <a href="#" id="toTop" style="display: block;"><span id="toTopHover" style="opacity: 1;"></span></a>
		</div>
		</div>
	</div>
	<!---//End-footer----->
<!---//End-wrap---->
</form>
</body>
</html>