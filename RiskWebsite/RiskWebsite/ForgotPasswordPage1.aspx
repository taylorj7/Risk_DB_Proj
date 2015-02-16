<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPasswordPage1.aspx.cs" Inherits="RiskWebsite.ForgotPasswordPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <p>
        If you forgot your password, you can reset it here.</p>
    <p>
        &nbsp;</p>
    <p>
        Username:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; New Password:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Confirmation Phrase:</p>
    <br />
        &nbsp;
    <br />
        <asp:TextBox ID="UsernameTextBox" runat="server"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="PasswordTextBox" runat="server"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="PhraseTextBox" runat="server"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" Text="Reset Password" OnClick="Button1_Click" />
        <br />
        <asp:Label ID="SuccessLabel" runat="server" Text=""></asp:Label>
    </form>
        <br />
</body>
</html>
