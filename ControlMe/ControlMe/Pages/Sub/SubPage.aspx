<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Sub/Sub.Master" CodeBehind="SubPage.aspx.cs" Inherits="ControlMe.Pages.SubPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../Style/SubStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table style="width: 100%;">
        <tr>
            <td>Dom :</td>
            <td>
                <asp:DropDownList ID="doms" runat="server" OnSelectedIndexChanged="doms_SelectedIndexChanged" Width="25%" AutoPostBack="true"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 75px">Message History : </td>
            <td style="width: 90%; height: 500px">
                <asp:TextBox ReadOnly="true" TextMode="MultiLine" runat="server" ID="ChatLog" CssClass="ChatLog" Width="100%" Height="100%"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Message :</td>
            <td>
                <asp:TextBox ID="sendmsg" runat="server" Width="60%"></asp:TextBox><asp:Button ID="sendmsgbtn" Text="Send" runat="server" OnClick="sendmsgbtn_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="Invites" runat="server" Visible="false">
                    <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" OnItemCommand="DataGrid1_ItemCommand" Width="100%">
                        <Columns>
                            <asp:BoundColumn DataField="DomUser" HeaderText="Dom Invite" />
                            <asp:ButtonColumn ButtonType="LinkButton" CommandName="Accept" Text="Accept" />
                            <asp:ButtonColumn ButtonType="LinkButton" CommandName="Reject" Text="Reject" />
                        </Columns>
                    </asp:DataGrid>
                </asp:Panel>
            </td>
        </tr>
    </table>

    <asp:TextBox Visible="false" ID="ErrorLb" runat="server"></asp:TextBox>
</asp:Content>
