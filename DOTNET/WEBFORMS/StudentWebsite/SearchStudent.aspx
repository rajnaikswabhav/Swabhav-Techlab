<%@ Page Title="" Language="C#" MasterPageFile="~/StudentPage.master" AutoEventWireup="true" CodeFile="SearchStudent.aspx.cs" Inherits="SearchStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            margin-left: 22px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
        <asp:Label ID="userLabel" Font-Bold="true" 
            Font-Size="Large" runat="server"></asp:Label>
        <br />
        <br />
        <br />
        </center>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="nameLabel" Font-Bold="true" Text="Name"
            Font-Size="Large" runat="server"></asp:Label>
        <asp:TextBox ID="nameTxt" runat="server" CssClass="auto-style1"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="idLabel" Font-Bold="true" Text="Id"
            Font-Size="Large" runat="server"></asp:Label>
    <asp:TextBox ID="idTxt" runat="server" CssClass="auto-style1"></asp:TextBox>
    <br /><br /><br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="locationLabel" Font-Bold="true" Text="Location"
            Font-Size="Large" runat="server"></asp:Label>
        <asp:TextBox ID="locationTxt" runat="server" CssClass="auto-style1"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="ageLabel" Font-Bold="true" Text="Age"
            Font-Size="Large" runat="server"></asp:Label>
    <asp:TextBox ID="ageTxt" runat="server" CssClass="auto-style1"></asp:TextBox>
    <br><br><br>
    <center>
          <asp:Button ID="searchBtn" runat="server" Text="Search" Width="76px" onClick ="findStudent" />
    </center>
    <br><br><br>
    <center>
        <asp:GridView ID="searchGridView" runat="server" Width="249px"></asp:GridView>
        </center>
</asp:Content>

