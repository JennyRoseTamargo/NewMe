<%@ Page Title="Sign In" MasterPageFile="../Site.master" Theme="Default" 
    Language="C#" AutoEventWireup="true" CodeFile="SignIn.aspx.cs" Inherits="ARMS.Security.SignIn"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cpHcontent" Runat="server">
	<div class="post">
	    <h2 class="title">Sign In</h2>
	    <br />
	    <div class="entry">
            <asp:Login ID="Login1" runat="server" OnAuthenticate="OnAuthenticate" OnLoggedIn="OnLoggedIn">
                <LayoutTemplate>
                    <table border="0" cellpadding="1" cellspacing="0" 
                        style="border-collapse:collapse;">
                        <tr>
                            <td>
                                <table border="0" cellpadding="0">
                                    <tr>
                                        <td colspan="2">Enter your user name and password to sign in.</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                                ControlToValidate="UserName" ErrorMessage="User Name is required." 
                                                ToolTip="User Name is required." ValidationGroup="vgLogin">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                                ControlToValidate="Password" ErrorMessage="Password is required." 
                                                ToolTip="Password is required." ValidationGroup="vgLogin">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2" style="color:Red;">
                                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="2">
                                            <asp:Button ID="btnLogin" runat="server" CommandName="Login" Text="Sign In" 
                                                ValidationGroup="vgLogin" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
            </asp:Login>                                
	    </div>
    </div>
</asp:Content>

	
