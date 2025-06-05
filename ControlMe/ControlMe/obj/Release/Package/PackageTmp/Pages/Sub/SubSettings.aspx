<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Sub/Sub.Master" AutoEventWireup="true" CodeBehind="SubSettings.aspx.cs" Inherits="ControlMe.Pages.Sub.SubSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>Random Opt In :</td>
            <td>
                <asp:CheckBox ID="optinchk" runat="server" AutoPostBack="true" /></td>
        </tr>
        <tr>
            <td>Receive Anonymous :</td>
            <td>
                <asp:CheckBox ID="anonchk" runat="server" AutoPostBack="true" /></td>
        </tr>
                <tr>
            <td>Email :</td>
            <td>
                <asp:TextBox ID="emailtxt" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Password :</td>
            <td>
                <asp:TextBox ID="passwordtxt" runat="server"></asp:TextBox></td>
        </tr>
    </table>
    <asp:Button ID="SaveBtn" Text="Save" runat="server" OnClick="SaveBtn_Click" /><br />
    <asp:Label ID="errorsstrlbl" runat="server"></asp:Label>
</asp:Content>
