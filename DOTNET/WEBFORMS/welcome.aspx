<%@ Page Language="c#" CodeFile="welcome.aspx.cs" Inherits ="Welcome" %>

<html>
    <body>
        <h1>Welcome to aspx page</h1>
        <form runat="server">
            <asp:Label runat = "server" id="label" Text = "Enter your Name"></asp:Label>
            <asp:Textbox runat = "server" id="textBox" Text = "HiTech"></asp:Textbox><br>
            <asp:Button runat = "server" id="btnHello" Text="Hello Button" onclick="btnHello_click">
            </asp:Button><br>
            <asp:Label runat = "server" id="label2" Text = "Hello Mr. "></asp:Label>
        </form>
    </body>
</html>