<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Sub/Sub.Master" AutoEventWireup="true" CodeBehind="Groups.aspx.cs" Inherits="ControlMe.Pages.Sub.Groups" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <table>
            <tr>
                <td>
                    <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" OnItemCommand="DataGrid1_ItemCommand" Width="100%">
                        <Columns>
                            <asp:BoundColumn DataField="GroupName" HeaderText="Group" />
                            <asp:BoundColumn DataField="Allowed" HeaderText="Allowed" />
                            <asp:ButtonColumn ButtonType="LinkButton" CommandName="Select" Text="Select" />
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    <asp:Label ID="Errorstrlbl" runat="server"></asp:Label>
</asp:Content>
