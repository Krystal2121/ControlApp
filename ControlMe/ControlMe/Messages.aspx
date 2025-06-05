<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Messages.aspx.cs" Inherits="ControlMe.Messages" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width:725px;height:250px">
                <tr style="height:250px">
                    <td>
                        <asp:TextBox ID="Message" TextMode="MultiLine" runat="server" Width="100%" Height="250px"></asp:TextBox>
                    </td>
                </tr>
                <tr><td><asp:TextBox ID="sndmsgtxt" runat="server" Width="100%"></asp:TextBox></td></tr>
                <tr><td><asp:Button ID="Sendmsgbtn" Text="Send" runat="server" OnClick="Sendmsgbtn_Click" /></td></tr>
            </table>
        </div>
    </form>
</body>
</html>
