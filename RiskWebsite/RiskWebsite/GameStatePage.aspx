<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GameStatePage.aspx.cs" Inherits="RiskWebsite.GameStatePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Game:
        <asp:Label ID="GameIDLabel" runat="server" Text=""></asp:Label>
        <br />
        <asp:Image id="Image1" runat="server"
           AlternateText="Image text"
           ImageAlign="left"
           ImageUrl="http://dev.filkor.org/images/risk/risk-colored-small.jpg"/>
        <br />
        <table width="100%" align="center" cellpadding="2" cellspacing="2" border="0" bgcolor="#EAEAEA" >
        <tr align="left" style="background-color:#004080;color:White;" >
            <td> Country </td>                        
            <td> User </td>
            <td> Number of Soldiers </td>                
        </tr>

        <%=getWhileLoopData()%>

    </table>
    
    </div>
    </form>
</body>
</html>
