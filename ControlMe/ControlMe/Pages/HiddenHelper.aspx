<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HiddenHelper.aspx.cs" Inherits="ControlMe.Pages.HiddenHelper" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>
                <asp:DataGrid ID="DataGrid2" runat="server" AutoGenerateColumns="False" OnItemCommand="Verified_ItemCommand">
                    <columns>
                        <asp:BoundColumn DataField="User" HeaderText="User" />
                        <asp:BoundColumn DataField="Email" HeaderText="Email" />
                        <asp:BoundColumn DataField="Code" HeaderText="Code" />
                        <asp:BoundColumn DataField="Tries" HeaderText="Tries" />
                        <asp:ButtonColumn ButtonType="LinkButton" CommandName="Done" Text="Done" />
                    </columns>
                </asp:DataGrid>
            </td>
        </tr>
    </table>
    <asp:Label ID="Errorstrlbl" runat="server"></asp:Label>
</asp:Content>
