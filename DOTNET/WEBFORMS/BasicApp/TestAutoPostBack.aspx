<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestAutoPostBack.aspx.cs" Inherits="TestAutoPostBack" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            margin-left: 50px;
        }
        .auto-style2 {
            margin-top: 161px;
        }
    </style>
</head>
<body>  
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:Label ID="deptLabel" runat="server" Text="Select Department"></asp:Label>
        <asp:DropDownList ID="deptList" runat="server" CssClass="auto-style1" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:GridView ID="gridView" runat="server" CssClass="auto-style2" Width="473px">
        </asp:GridView>
    </form>
</body>
</html>
