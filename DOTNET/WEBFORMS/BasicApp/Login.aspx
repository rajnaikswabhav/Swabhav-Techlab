<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            margin-left: 39px;
        }
        .auto-style2 {
            margin-left: 66px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <p>
            <asp:Label ID="userLabel" runat="server" Text="UserName"></asp:Label>
            <asp:TextBox ID="userTxt" runat="server" CssClass="auto-style1" Width="165px"></asp:TextBox>
        </p>
        <asp:Button ID="loginBtn" runat="server" Text="Login" OnClick="LoginUser"
            CssClass="auto-style2" Width="94px" />
    </form>
</body>
</html>
