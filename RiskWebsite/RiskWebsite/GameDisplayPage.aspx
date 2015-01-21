<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GameDisplayPage.aspx.cs" Inherits="RiskWebsite.GameDisplayPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Game<br />
    
    </div>
    <table width="100%" align="center" cellpadding="2" cellspacing="2" border="0" bgcolor="#EAEAEA" >
        <tr align="left" style="background-color:#004080;color:White;" >
            <td> ID </td>                        
            <td> CurrentPosition </td>                
        </tr>

        <%=getWhileLoopData()%>

    </table>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Create New Game" />
    </form>
</body>
</html>
