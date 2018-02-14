<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LoginPage.aspx.cs" Inherits="LoginPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            margin-left: 20px;
        }
        .auto-style2 {
            margin-left: 27px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br /><br />
    <center>
        <asp:Label ID="label" Text="Login First..." runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
        <br /><br /><br />
        <asp:Label ID="userLabel" Text="Username" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
        <asp:TextBox ID="userTxt" runat="server" CssClass="auto-style1" Font-Size="Large"></asp:TextBox>
        <br /><br />
        <asp:Label ID="passLabel" Text="Password" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
        <asp:TextBox ID="passTxt" runat="server" CssClass="auto-style2" Font-Size="Large" TextMode="Password"></asp:TextBox>
        <br /><br /><br />
        <asp:Button ID="login" Text="Login" runat="server" Width="71px" OnClick="Login" />
        <br /><br /><br />
        <asp:Label ID="authLabel" Text="" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
    </center>
</asp:Content>

