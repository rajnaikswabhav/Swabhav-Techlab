<%@ Page Title="" Language="C#" MasterPageFile="~/StudentPage.master" AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            margin-left: 51px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <center>
           <asp:Label ID="Label1" runat="server" Text="Studnet WebSite" 
               Font-Bold="True" Font-Size="Large"></asp:Label>
           <br />
           <br />
        <asp:Label ID="userLabel" runat="server" Text="User Name:" 
               Font-Bold="True" Font-Size="Medium"></asp:Label>
        <asp:TextBox ID="userTxt" runat="server" Font-Size="Medium" 
            style="margin-left: 40px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="paasLabel" runat="server" Text="Password:" 
               Font-Bold="True" Font-Size="Medium"></asp:Label>
        <asp:TextBox ID="passwordTxt" runat="server" Font-Size="Medium" 
            CssClass="auto-style1" TextMode="Password"></asp:TextBox>
        <br /><br /><br />
        <asp:Button ID="loginBtn" runat="server" Text="Login" Width="76px" onClick ="login" />
    </center>
</asp:Content>

