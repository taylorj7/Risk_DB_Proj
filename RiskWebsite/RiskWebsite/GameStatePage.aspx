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
        <asp:Button ID="StartButton" runat="server" Text="Start Game" OnClick="StartButton_Click" />
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
    <div>
        <asp:Label ID="TurnLabel" runat="server" Text="It's not your turn yet!"></asp:Label>
        <br />
        <br />
        Place Troops&nbsp;&nbsp;&nbsp;
        <asp:Label ID="RemainingTroops" runat="server" Text="0"></asp:Label>
&nbsp; remaining<br />
        <asp:DropDownList ID="YourCountriesPlace" runat="server"></asp:DropDownList> &nbsp;
        <asp:TextBox ID="TextBox1" runat="server" placeholder ="number of troops"></asp:TextBox>
        &nbsp; <asp:Button ID="PlaceButton" runat="server" Text="Place" />
         <br />
        <br />
        Attack 
        <br />
        <asp:DropDownList ID="YourCountriesAttack" runat="server"></asp:DropDownList> &nbsp;
        <asp:DropDownList ID="BorderingCountriesAttack" runat="server"></asp:DropDownList> &nbsp;
        &nbsp; <asp:Button ID="AttackButton" runat="server" Text="Attack" />
        <br />
        <br />
        Move Troops 
        <br />
        <asp:DropDownList ID="YourCountriesMove" runat="server"></asp:DropDownList> &nbsp;
        <asp:DropDownList ID="YourBorderingCountriesMove" runat="server"></asp:DropDownList> &nbsp;
        <asp:TextBox ID="MoveTroopsNumber" runat="server" placeholder ="number of troops"></asp:TextBox>
        &nbsp; <asp:Button ID="MoveTroopsButton" runat="server" Text="Move" />
        <br />
        <asp:Button ID="EndTurn" runat="server" Text="End Turn" />
    </div>
    </form>
</body>
</html>
