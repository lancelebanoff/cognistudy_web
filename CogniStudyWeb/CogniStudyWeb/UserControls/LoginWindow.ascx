<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginWindow.ascx.cs" Inherits="CogniTutor.UserControls.LoginWindow" %>

<% if(mPage is CogniTutor.Login) { %>
<!--Login modal-->
<div id="login" aria-hidden="true" class="modal fade" role="dialog" tabindex="-1">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button aria-hidden="true" class="close" data-dismiss="modal" type="button">
                    ×
                </button>
                <h1 class="text-center">Login</h1>
            </div>
            <div class="modal-body">

                <!--Update Panel for Body-->
                       <div class="form-group">
                           <asp:Label ID="lblLoginError" class="lead" runat="server" Text="There was a problem with your login." Visible="False" ForeColor="Red"></asp:Label>
                       </div>
                        <div class="form-group">
                           <asp:TextBox ID="tbLoginEmail" runat="server" AutoCompleteType="Email" CssClass="form-control input-lg" placeholder="Email"></asp:TextBox>
                       </div>
                       <div class="form-group">
                           <asp:TextBox ID="tbLoginPassword" runat="server" CssClass="form-control input-lg" placeholder="Password" type="password"></asp:TextBox>
                     </div>
                        <div class="form-group">
                           <asp:Button ID="btnSignIn" runat="server" CssClass="btn btn-primary btn-lg btn-block" OnClick="btnSignIn_Click" Text="Sign In" />
                            
                        </div>
                        <div class="form-group">
                            
                            <!-- Changed because incomplete-->
                           <span class="pull-right"><a href="Register.aspx">New user? Create a FREE account.</a></span><span><a href="#"><!--Forgot your password?--> </a></span>
                            <br />
                        </div>

            </div>
            <div class="modal-footer">
                <div class="col-md-12">
                    <button aria-hidden="true" class="btn" data-dismiss="modal">
                        Cancel</button>
                </div>
            </div>
        </div>
    </div>
</div>
<% } %>
