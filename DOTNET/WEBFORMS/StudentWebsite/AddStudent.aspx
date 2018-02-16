<%@ Page Title="" Language="C#" MasterPageFile="~/StudentPage.master" AutoEventWireup="true" CodeFile="AddStudent.aspx.cs" Inherits="StudentAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            margin-left: 53px;
        }

        .auto-style2 {
            margin-left: 75px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <center>
        <asp:Label ID="userLabel" runat="server"
            Font-Bold="True" Font-Size="Large"></asp:Label> 
        <br /><br />
        <asp:Label ID="nameLabel" runat="server" Font-Bold="true"
            Font-Size="Medium" Text="Student Name "></asp:Label>
        <asp:TextBox ID="nameTxt" runat="server" Font-Size="Medium" 
            style="margin-left: 40px"></asp:TextBox>
        <br /><br />
        <asp:Label ID="ageLabel" runat="server" Font-Bold="true"
            Font-Size="Medium" Text="Student Age "></asp:Label>
        <asp:TextBox ID="ageTxt" runat="server" Font-Size="Medium" 
            CssClass="auto-style1"></asp:TextBox>
        <br /><br />
        <asp:Label ID="locationLabel" runat="server" Font-Bold="true"
            Font-Size="Medium" Text="Location "></asp:Label>
        <asp:TextBox ID="locationTxt" runat="server" Font-Size="Medium" 
            CssClass="auto-style2"></asp:TextBox>
        <br><br><br>
        <asp:Button ID="addBtn" Text="Add" runat="server" Width="87px" OnClick="Add"/>
    </center>
</asp:Content>
