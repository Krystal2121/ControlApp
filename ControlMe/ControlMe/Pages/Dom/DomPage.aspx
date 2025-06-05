<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Dom/Dom.Master" CodeBehind="DomPage.aspx.cs" Inherits="ControlMe.Pages.DomPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Style/DomStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table><tr><td>Invite sub :</td><td><asp:TextBox ID="InviteSub" runat="server"></asp:TextBox><asp:Button ID="senInviteBtn" Text="Invite" runat="server" OnClick="senInviteBtn_Click" /></td></tr></table>
    <table style="width: 100%;">
        <tr>
            <td>Sub :</td>
            <td>
                <asp:DropDownList ID="subs" runat="server" Width="25%" OnSelectedIndexChanged="subs_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 75px">Message History : </td>
            <td style="width: 90%;height:500px">
                <asp:TextBox ReadOnly="true" TextMode="MultiLine" runat="server" ID="ChatLog" CssClass="ChatLog" Width="100%" Height="100%"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Message :</td>
            <td>
                <asp:TextBox ID="sendmsg" runat="server" Width="60%"></asp:TextBox><asp:Button ID="sendmsgbtn" Text="Send" runat="server" OnClick="sendmsgbtn_Click" />
            </td>
        </tr>
    </table>
    <asp:Label ID="Errorlbl" runat="server"></asp:Label>
</asp:Content>
