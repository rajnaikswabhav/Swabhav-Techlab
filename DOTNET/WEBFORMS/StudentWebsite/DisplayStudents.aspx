<%@ Page Title="" Language="C#" MasterPageFile="~/StudentPage.master" AutoEventWireup="true" CodeFile="DisplayStudents.aspx.cs" Inherits="DisplayStudents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
        <asp:Label ID="userLabel" Font-Bold="true" 
            Font-Size="Large" runat="server"></asp:Label>\
        <br />
        <br />
        <br />
     <asp:GridView ID="gridView" runat="server" Width="249px"></asp:GridView>
    </center>

</asp:Content>

