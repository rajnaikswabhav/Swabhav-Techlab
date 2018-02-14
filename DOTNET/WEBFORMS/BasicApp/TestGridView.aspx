<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestGridView.aspx.cs" Inherits="TestGridView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            margin-left: 327px;
        }
        .auto-style2 {
            margin-top: 118px;
        }
        .auto-style3 {
            margin-left: 24px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="submitBtn" runat="server" CssClass="auto-style1" 
            OnClick="DisplayData" Text="Submit" Width="63px" />
    
        <asp:Button ID="postBackBtn" runat="server" CssClass="auto-style3" Text="PostBack" />
    
    </div>
        <asp:GridView ID="gridView" runat="server" CssClass="auto-style2" Height="164px" Width="352px" EnableViewState="False">
        </asp:GridView>
    </form>
</body>
</html>
