<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="ControlMe.Upload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript">
    function copyToClipboard() {
        var copyText = document.getElementById('<%= filefull.ClientID %>');
        copyText.select();
        document.execCommand("copy");
    }
</script>

<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="filefull" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Copy to Clipboard" OnClientClick="copyToClipboard(); return false;" /><br />
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:Button ID="UploadButton" runat="server" Text="Upload" OnClick="UploadButton_Click" />
            <asp:Label ID="StatusLabel" runat="server" Text="" />
        </div>
    </form>
</body>
</html>
