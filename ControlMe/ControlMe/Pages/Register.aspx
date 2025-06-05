<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="ControlMe.Pages.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" DefaultButton="SubmitReg">
        <table>
            <tr>
                <td>
                    <asp:Label runat="server">Screen Name :</asp:Label></td>
                <td>
                    <asp:TextBox ID="ScreenNameTxt" runat="server"></asp:TextBox></td>
            </tr>

            <tr>
                <td>
                    <asp:Label runat="server">Email :</asp:Label></td>
                <td>
                    <asp:TextBox ID="EmailTxt" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server">Password :</asp:Label></td>
                <td>
                    <asp:TextBox ID="PasswordTxt" runat="server" TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server">Role :</asp:Label></td>
                <td>
                    <asp:DropDownList ID="DDRoleTxt" runat="server">
                        <asp:ListItem>Sub</asp:ListItem>
                        <asp:ListItem>Dom</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server">Random Opt In :</asp:Label></td>
                <td>
                    <asp:DropDownList ID="RndOpt" runat="server">
                        <asp:ListItem>Opt In</asp:ListItem>
                        <asp:ListItem>Opt Out</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
        </table>
        <asp:Button ID="SubmitReg" Text="Submit" runat="server" OnClick="SubmitReg_Click" />
    </asp:Panel>

    <asp:Label ID="errorlbl" runat="server"></asp:Label>
</asp:Content>
