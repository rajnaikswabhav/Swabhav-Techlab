<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TextValidation.aspx.cs" Inherits="TextValidation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            margin-left: 41px;
        }

        .auto-style2 {
            margin-left: 7px;
        }

        .auto-style3 {
            margin-left: 52px;
        }

        .auto-style4 {
            margin-left: 38px;
        }
        .auto-style5 {
            margin-left: 39px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:Label ID="nameLabel" runat="server" Text="Name"></asp:Label>
        <asp:TextBox ID="nameTxt" runat="server" CssClass="auto-style1"></asp:TextBox>
        <asp:RequiredFieldValidator ID="requireField"
            runat="server" ErrorMessage="Please Enter Name..." ControlToValidate="nameTxt" ForeColor="Red"></asp:RequiredFieldValidator>
        <p>
            <asp:Label ID="ageLabel" runat="server" Text="Age"></asp:Label>
            <asp:TextBox ID="ageTxt" runat="server" CssClass="auto-style3" Width="120px"></asp:TextBox>
            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ageTxt"
                ErrorMessage="Age is not in Range..." MinimumValue="18" MaximumValue="35" ForeColor="Red"></asp:RangeValidator>
        </p>
        <p>
            <asp:Label ID="salaryLabel" runat="server" Text="Salary"></asp:Label>
            <asp:TextBox ID="salaryTxt" runat="server" CssClass="auto-style4"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator1" runat="server"
                ErrorMessage="CompareValidator" Operator="GreaterThan" ValueToCompare="5000"
                ControlToValidate="salaryTxt" Type="Double" ForeColor="Red"></asp:CompareValidator>
        </p>
        <p>
            &nbsp;
            <asp:Label ID="emailLabel" runat="server" Text="Email"></asp:Label>
            <asp:TextBox ID="emailTxt" runat="server" CssClass="auto-style5"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ControlToValidate="emailTxt" ErrorMessage="Email Required..." ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
        </p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="submitBtn" runat="server" CssClass="auto-style2"
            OnClick="SubmitPage" Text="Submit" />
        </p>
        <p>
            <asp:Label ID="resLabel" runat="server" Text="Response:"></asp:Label>
        </p>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="resNamelabel" runat="server"></asp:Label>
        <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="resAgeLabel" runat="server"></asp:Label>
        </p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="resSalaryLabel" runat="server"></asp:Label>
        <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="resEmailLabel" runat="server"></asp:Label>
        </p>
    </form>
</body>
</html>
