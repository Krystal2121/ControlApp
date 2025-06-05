<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ControlMe.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="greet" runat="server"></asp:Label>
    <p>
        How it works to receive pc commands :<br />
        1) Download the latest version of the app. (See below)<br />
        2) Register on this website.<br />
        3) Unzip the app.<br />
        4) Run the controlapp.exe, look for the app in your system tray, right click it and press open.
        <br />
        5) Change the setting. Set the folder to a new location, enter user name and password you registered with.<br />
        6) While the main form is open it will check for commands every 30 seconds. Closing the form does not close the app but it does stop it looking for commands.</br>
        <br />
        How it works to send pc commands :<br />
        <br />
        METHOD 1 : <br />
        Run steps 1-4 from above.<br />
        5) Put a web loction in the text box at the top and press the button the describes what you want to happen.<br />
        6) Once you have added all the commands you want press the "Copy to clipboard."<br />
        7) Press the "Control someones's PC" button below or follow the link they send.<br />
        Or leave the User Name box empty and the command will be sent to random user. <br />
        8) Paste the commands string in the message box.<br />
        9) Press Send.<br />
        <br />
        METHOD 2 :<br />
        Run steps 1-4 from above.<br />
        5) Put a web loction in the text box at the top and press the button the describes what you want to happen.<br />
        6) Once you have added all the commands type the targets user name in the send to box at the bottom.<br /> 
        (Or leave empty to send to random sub user)<br />
        7) Press the "Send To" button. <b>*THIS WILL OPEN A WEBPAGE TO SEND DATA*</b><br />
        <br />
    </p>
    <table><tr><td>Discord Server</td><td><asp:HyperLink NavigateUrl="https://discord.gg/27VSnVv9P8" Text="Here" runat="server"></asp:HyperLink></td></tr>
        <tr>
            <td>User manual for new users</td><td><asp:HyperLink NavigateUrl="~/Downloads/The Control App new users.docx" Text="Here" runat="server"></asp:HyperLink></td>
            </tr><tr>
            <td>Latest version of control app is v0.1.2<br /> (check form title for v number) :</td>
            <td><asp:HyperLink NavigateUrl="~/Downloads/ControlApp.zip" Text="Here" runat="server"></asp:HyperLink>
                </td>
        </tr>
        <tr>
            <td>Control someone's PC</td>
            <td>
                <asp:HyperLink NavigateUrl="~/Pages/ControlPC.aspx" Text="Here" runat="server"></asp:HyperLink></td>
        </tr>
    </table>
</asp:Content>
