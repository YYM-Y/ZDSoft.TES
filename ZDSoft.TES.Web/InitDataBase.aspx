<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InitDataBase.aspx.cs" Inherits="ZDSoft.TES.Web.InitDataBase" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Button ID="btnCreateDB" runat="server" Text="初始化数据库" OnClick="btnCreateDB_Click" />
    </div>
        
    </form>
</body>
</html>
