<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sysgroupManager.aspx.cs" Inherits="Itop.WebFrame.admin.sysgroupManager" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register src="WebUCsysgroup.ascx" tagname="WebUCsysgroup" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server">
        <Listeners>
        </Listeners>
    </ext:ResourceManager>
    
    <uc1:WebUCsysgroup ID="WebUCsysgroup1" runat="server">
    </uc1:WebUCsysgroup>
    
    </form>
</body>
</html>
