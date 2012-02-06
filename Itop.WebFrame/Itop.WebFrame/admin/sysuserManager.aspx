<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sysuserManager.aspx.cs"
    Inherits="Itop.WebFrame.sysuserManager" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户管理</title>
    <link rel="stylesheet" type="text/css" href="../resources/admin.css" />

    <script type="text/javascript">
        var template = '<span style="color:{0};">{1}</span>';
        var change = function(value) {
            return String.format(template, (value > 0) ? "green" : "red", value);
        };

        var pctChange = function(value) {
            return String.format(template, (value > 0) ? "green" : "red", value + "%");
        };
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server">
        <Listeners>
        </Listeners>
    </ext:ResourceManager>
    <ext:Store ID="Store1" runat="server" OnRefreshData="MyData_Refresh" OnBeforeStoreChanged="HandleChanges"
        SkipIdForNewRecords="false" RefreshAfterSaving="None" AutoSave="true">
        <Reader>
            <ext:JsonReader>
                <Fields>
                    <ext:RecordField Name="id" />
                    <ext:RecordField Name="LoginID" AllowBlank="false" />
                    <ext:RecordField Name="UserName" AllowBlank="false" />
                    <ext:RecordField Name="isadmin" />
                    <ext:RecordField Name="IsUser" Type="Boolean"/>
                    <ext:RecordField Name="pwd" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        <Listeners>
            <Exception Handler="
                    Ext.net.Notification.show({
                        iconCls    : 'icon-exclamation', 
                        html       : e.message, 
                        title      : 'EXCEPTION', 
                        autoScroll : true, 
                        hideDelay  : 5000, 
                        width      : 300, 
                        height     : 200
                    });" />
            <BeforeSave Handler="var valid = true; this.each(function(r){if(r.dirty && !r.isValid()){valid=false;}}); return valid;" />
        </Listeners>
    </ext:Store>
    <ext:Viewport runat="server" AutoHeight="true" Layout="BorderLayout">
        <Items>
            <ext:Panel runat="server" Region="Center" Layout="FitLayout">
                <TopBar>
                    <ext:Toolbar ID="ToolBar1" runat="server">
                        <Items>
                            <%--<ext:Button ID="btnSave" runat="server" Text="Save" Icon="Disk">
                                <Listeners>
                                    <Click Handler="#{GridPanel1}.save();" />
                                </Listeners>
                            </ext:Button>--%>
                            <ext:Button ID="extbtnedit" runat="server" Icon="UserAdd" Text="增加">
                                <Listeners>
                                    <Click Handler="#{GridPanel1}.insertRecord();#{rowEditor1}.startEditing(0);" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button ID="Button2" runat="server" Icon="UserDelete" Text="删除">
                                <Listeners>
                                    <Click Handler="#{GridPanel1}.deleteSelected();" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button ID="btnLoad" runat="server" Text="刷新" Icon="ArrowRefresh">
                                <Listeners>
                                    <Click Handler="#{GridPanel1}.load();" />
                                </Listeners>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:GridPanel ID="GridPanel1" runat="server" StoreID="Store1" Border="false">
                        <View>
                            <ext:GridView ID="GridView1" runat="server" ForceFit="true" />
                        </View>
                        <Plugins>
                            <ext:RowEditor ID="rowEditor1" runat="server" SaveText="保存" CancelText="取消" ErrorText="警告" CommitChangesText="数据已被修改，请先保存或取消!" ErrorSummary="True">
                            </ext:RowEditor>
                        </Plugins>
                        <ColumnModel ID="ColumnModel1" runat="server">
                            <Columns>
                            <ext:RowNumbererColumn></ext:RowNumbererColumn>
                                <ext:Column Header="id" Hidden="true" DataIndex="id" />
                                <ext:Column Header="用户号" Width="160" DataIndex="LoginID">
                                    <Editor>
                                        <ext:TextField runat="server">
                                        </ext:TextField>
                                    </Editor>
                                </ext:Column>
                                <ext:Column Header="姓名" Width="75" DataIndex="UserName">
                                    <Editor>
                                        <ext:TextField runat="server">
                                        </ext:TextField>
                                    </Editor>
                                </ext:Column>
                                <ext:Column Header="口令" Width="75" DataIndex="pwd">
                                    <Editor>
                                        <ext:TextField runat="server">
                                        </ext:TextField>
                                    </Editor>
                                </ext:Column>
                                <ext:Column Header="管理员" Width="75" DataIndex="isadmin">
                                    <Renderer Fn="function(value) {return value=='true'?'是':'否';}" />
                                    <Editor>
                                        <ext:ComboBox runat="server">
                                            <Items>
                                                <ext:ListItem Text="是" Value="true" />
                                                <ext:ListItem Text="否" Value="false" />
                                            </Items>
                                        </ext:ComboBox>
                                        <%--<ext:Checkbox runat="server"></ext:Checkbox>--%>
                                    </Editor>
                                </ext:Column>
                                <ext:Column Header="启用" Width="75" DataIndex="IsUser">
                                    <Renderer Fn="function(value) {return value=='true'?'是':'否';}" />
                                    <Editor>
                                        <ext:ComboBox runat="server">
                                            <Items>
                                                <ext:ListItem Text="是" Value="true" />
                                                <ext:ListItem Text="否" Value="false" />
                                            </Items>
                                        </ext:ComboBox>
                                        <%--<ext:Checkbox runat="server"></ext:Checkbox>--%>
                                    </Editor>
                                </ext:Column>
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" />
                        </SelectionModel>
                        <LoadMask ShowMask="true" />
                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolBar2" runat="server" PageSize="18" StoreID="Store1" />
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
