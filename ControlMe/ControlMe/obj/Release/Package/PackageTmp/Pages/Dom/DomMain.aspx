<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Dom/Dom.Master" AutoEventWireup="true" CodeBehind="DomMain.aspx.cs" Inherits="ControlMe.Pages.Doms.DomMain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:Panel id="NotVar" Visible="false" runat="server">You are not verified would you like to do this now? <asp:Button Text="Verify" ID="VarifyBtn" runat="server" OnClick="VarifyBtn_Click" /></asp:Panel>
    <asp:Panel ID="VarReq" Visible="false" runat="server">Please enter your verification code : <asp:TextBox ID="VarCode" runat="server"></asp:TextBox> <asp:Button ID="entrbtn" Text="Enter" runat="server" OnClick="entrbtn_Click" /></asp:Panel>
    <asp:Label ID="Errorlbl" runat="server"></asp:Label>
</asp:Content>
