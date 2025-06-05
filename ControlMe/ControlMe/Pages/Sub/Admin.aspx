<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Sub/Sub.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="ControlMe.Pages.Sub.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Main" Visible="false" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Label runat="server">Screen Name :</asp:Label><asp:TextBox ID="screenname" runat="server"></asp:TextBox><br />
                    <asp:Label runat="server">Id&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:</asp:Label><asp:TextBox ID="userid" runat="server">0</asp:TextBox><br />
                    <asp:Button ID="getdata" runat="server" Text="Get Data" OnClick="getdata_Click" />
                    <asp:DataGrid ID="DataGrid3" runat="server" AutoGenerateColumns="False" OnItemCommand="Verified_ItemCommand">
                        <Columns>
                            <asp:BoundColumn DataField="Screen Name" HeaderText="User" />
                            <asp:BoundColumn DataField="Login Name" HeaderText="Email" />
                            <asp:BoundColumn DataField="Password" HeaderText="Password" />
                            <asp:BoundColumn DataField="LoginDate" HeaderText="LoginDate" />
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DataGrid2" runat="server" AutoGenerateColumns="False" OnItemCommand="Verified_ItemCommand">
                        <Columns>
                            <asp:BoundColumn DataField="User" HeaderText="User" />
                            <asp:BoundColumn DataField="Email" HeaderText="Email" />
                            <asp:BoundColumn DataField="Code" HeaderText="Code" />
                            <asp:BoundColumn DataField="Tries" HeaderText="Tries" />
                            <asp:ButtonColumn ButtonType="LinkButton" CommandName="Done" Text="Done" />
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" OnItemCommand="DataGrid1_ItemCommand" Width="100%">
                        <Columns>
                            <asp:BoundColumn DataField="Sender" HeaderText="Sender" />
                            <asp:BoundColumn DataField="SubId" HeaderText="SubId" />
                            <asp:BoundColumn DataField="Decrypt" HeaderText="Decrypt" />
                            <asp:BoundColumn DataField="GroupRefId" HeaderText="GroupRefId" />
                            <asp:ButtonColumn ButtonType="LinkButton" CommandName="Select" Text="Select" />
                            <asp:BoundColumn DataField="Content" HeaderText="Content" />
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
 
        <tr>
            <td>
                <asp:DataGrid ID="Scores" runat="server"  AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundColumn DataField="Screen Name" HeaderText="User" />
                        <asp:BoundColumn DataField="ThumbsUp" HeaderText="ThumbsUp"/>
                    </Columns>
                </asp:DataGrid>
            </td>
        </tr>

        </table>
        <asp:Button Text="truncate" runat="server" OnClick="Unnamed1_Click" />
        <asp:Label ID="Errorstrlbl" runat="server"></asp:Label>

    </asp:Panel>
</asp:Content>
