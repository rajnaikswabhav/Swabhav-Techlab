<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestAjax.aspx.cs" Inherits="TestAjax" %>

<%@ Register Src="~/Timestamp.ascx" TagPrefix="uc1" TagName="Timestamp" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            margin-left: 117px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:Timestamp runat="server" ID="Timestamp1" />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="updatePanel">
            <%--<Triggers>
                <asp:AsyncPostBackTrigger ControlID="update" EventName="Click" />
            </Triggers>--%>
            <ContentTemplate>
                <uc1:Timestamp runat="server" ID="Timestamp" />
                <asp:Button ID="update" runat="server" OnClick="Update" 
                    Text="Update" CssClass="auto-style1" />
            </ContentTemplate>
        </asp:UpdatePanel>
                
    </div>
    </form>
</body>
</html>
