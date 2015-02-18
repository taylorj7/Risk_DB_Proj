<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GameDisplayPage.aspx.cs" Inherits="RiskWebsite.GameDisplayPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 110px">
    <form id="form1" runat="server">
    <div>
    
        Game<br />
    
    </div>
    <table width="100%" align="center" cellpadding="2" cellspacing="2" border="0" bgcolor="#EAEAEA" >
        <tr align="left" style="background-color:#004080;color:White;" >
            <td> ID </td>                        
            <td> CurrentPosition </td>  
            <td> Started </td>              
        </tr>

        <%=getWhileLoopData()%>

    </table>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Create New Game" />
    <div>
    <br />
        Add a friend to one of your games that hasn&#39;t started:
        <br />
        Friend Username:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; GameID:
        <br />
        <asp:TextBox ID="UsernameTextBox" runat="server"></asp:TextBox>

        &nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="GameIDTextBox" runat="server"></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="GameIDTextBox" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
        
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="AddUserButton" runat="server" Text="Add User" OnClick="AddUserButton_Click" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        <br />
        <br />
        Enter Game:<br />
        GameID:<br />
        <asp:TextBox ID="GameIDTextBox2" runat="server"></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="GameIDTextBox2" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
        
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="EnterGameButton" runat="server" Text="Enter Game" OnClick="EnterGameButton_Click" />
    </div>
    </form>
    </body>
</html>
