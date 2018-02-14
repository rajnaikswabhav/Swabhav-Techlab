<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestTimestamp.aspx.cs" Inherits="TestTimeStamp" %>

<%@ Register Src="~/Timestamp.ascx" TagPrefix="ts" TagName="TimeStamp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ts:TimeStamp runat="server" ID="timeStamp" />
            <br /><br /><br />
            <ts:TimeStamp runat="server" ID="timeStamp2" />
        </div>
    </form>
</body>
</html>
