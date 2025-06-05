<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ControlMe.Pages.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="panSearch" runat="server" DefaultButton="SubmitBtn" Width="100%" >
    <table><tr><td>User Name/Email</td><td><asp:TextBox ID="UserNameTxt" runat="server"></asp:TextBox></td></tr>
        <tr><td>Password</td><td><asp:TextBox ID="PasswordTxt" TextMode="Password" runat="server" ></asp:TextBox></td></tr>
    </table>
    <asp:Button ID="SubmitBtn" Text="Submit" OnClick="SubmitBtn_Click" runat="server" /><br />
    <asp:Label ID="errorstrlbl" runat="server"></asp:Label>
        </asp:Panel>
</asp:Content>
