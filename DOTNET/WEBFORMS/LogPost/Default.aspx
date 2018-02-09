<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 190px;
        }
        .auto-style2 {
            width: 74px;
        }
        .auto-style3 {
            margin-left: 5px;
        }
        .auto-style4 {
            width: 100%;
            height: 283px;
        }
        .auto-style5 {
            margin-left: 43px;
            margin-top: 0px;
        }
        .auto-style6 {
            width: 74px;
            height: 33px;
        }
        .auto-style7 {
            width: 190px;
            height: 33px;
        }
        .auto-style8 {
            height: 33px;
        }
        .auto-style9 {
            margin-right: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table class="auto-style4">
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    Message:
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtMessage" runat="server" CssClass="auto-style3" Width="174px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="deleteBtn" runat="server" OnClick="AddItem" Text="Add" />
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style6">
                    <asp:ListBox ID="messageListBox" runat="server" Height="240px" Width="122px" CssClass="auto-style9"></asp:ListBox>
                </td>
                <td class="auto-style7">
                    <asp:Button ID="delete" runat="server" 
                        CssClass="auto-style5" Text="Delete" Width="78px" OnClick="DeleteItem"/>
                </td>
                <td class="auto-style8"></td>
            </tr>
            <tr>
                <td class="auto-style2">
                    &nbsp;</td>
                <td class="auto-style1">
                    &nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Button ID="clearBtn" runat="server" Text="Clear" OnClick="ClearItems"/>
                </td>
                <td class="auto-style1">
                    &nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
