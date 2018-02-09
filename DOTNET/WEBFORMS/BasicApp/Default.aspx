<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>Welcome to aspx page</h1>
    <form id="form1" runat="server">
        <asp:Label runat="server" ID="label" Text="Enter your Name"></asp:Label>
        <asp:TextBox runat="server" ID="textBox" Text="HiTech"></asp:TextBox><br>
        <asp:Button runat="server" ID="btnHello" Text="Hello Button" OnClick="btnHello_click"></asp:Button><br>
        <asp:Label runat="server" ID="label2" Text="Hello Mr. "></asp:Label>

    </form>
</body>
</html>
