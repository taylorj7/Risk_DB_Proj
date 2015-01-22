<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateAccountPage.aspx.cs" Inherits="RiskWebsite.CreateAccountPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Please enter a username and a password for your account.<br />
        <br />
     <asp:Label ID ="UsernameLabel" runat="server" Text="Username: "></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="Password: "></asp:Label>
        <br />
        
        <asp:TextBox ID="UsernameTextBox" runat="server"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="PasswordTextBox" runat="server"></asp:TextBox>
    
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="CreateAccountButton" runat="server" Text="Create Account" OnClick="CreateAccountButton_Click" />
    
        <br />
        <asp:Label ID="SuccessLabel" runat="server" Text=""></asp:Label>
    
    </div>
    </form>
</body>
</html>
