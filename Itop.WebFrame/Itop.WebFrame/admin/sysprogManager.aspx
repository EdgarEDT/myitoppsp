<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sysprogManager.aspx.cs"
    Inherits="Itop.WebFrame.sysprogManager" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户管理</title>
    <link rel="stylesheet" type="text/css" href="../resources/admin.css" />

    <script type="text/javascript">
        
        var pid=null;
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server">
        <Listeners>
        </Listeners>
    </ext:ResourceManager>
    <ext:Store ID="Store1" runat="server" OnRefreshData="Data_Refresh" OnBeforeStoreChanged="HandleChanges"
        SkipIdForNewRecords="false" RefreshAfterSaving="None" AutoSave="true" AutoLoad="false">
        <Reader>
            <ext:JsonReader>
                <Fields>
                    <ext:RecordField Name="id" />
                    <ext:RecordField Name="orderID"  Type="Int"/>
                    <ext:RecordField Name="ProgCode" />
                    <ext:RecordField Name="ProgName" AllowBlank="false" />
                    <ext:RecordField Name="ProgClass"  />
                    <ext:RecordField Name="ProgIcon1" />
                    <ext:RecordField Name="ProgIcon2" />
                    <ext:RecordField Name="ParentID" />
                    <ext:RecordField Name="IsGroup"  />
                    <ext:RecordField Name="IsCore" />
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
            <ext:TreePanel ID="TreePanel1" runat="server" Region="West" Width="200" Layout="FitLayout" >
                <Root>
                    <ext:AsyncTreeNode Text="系统功能模块" NodeID="root" Expanded="true" >
                        <CustomAttributes>
                            <ext:ConfigItem Name="loaded" Value="false" Mode="Raw" />
                        </CustomAttributes>
                    </ext:AsyncTreeNode>
                </Root>
                <Loader>
                    <ext:PageTreeLoader RequestMethod="GET" OnNodeLoad="GetNodes" PreloadChildren="true" ClearOnLoad="true">
                        <EventMask ShowMask="true" Target="this" Msg="Loading..." />
                        <BaseAttributes>
                            <ext:Parameter Name="singleClickExpand" Value="false" Mode="Raw" />
                            <ext:Parameter Name="loaded" Value="false" Mode="Raw" />
                        </BaseAttributes>
                    </ext:PageTreeLoader>
                </Loader>
                <Listeners>
                <Click Handler="pid=null;if (!node.isLeaf()) { e.stopEvent();pid=node.id;#{Store1}.reload({params:{ParentID:pid}});}" />
                </Listeners>
                <TopBar>
                    <ext:Toolbar runat="server">
                        <Items>
                            <ext:Button ID="Button1" runat="server" Text="刷新" Icon="ArrowRefresh">
                                <Listeners>
                                    <Click Handler="#{TreePanel1}.reload();" />
                                </Listeners>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
            </ext:TreePanel>
            <ext:Panel runat="server" Region="Center" Layout="FitLayout" >
                <TopBar>
                    <ext:Toolbar ID="ToolBar1" runat="server">
                        <Items>
                            
                            <ext:Button ID="extbtnedit" runat="server" Icon="UserAdd" Text="增加">
                                <Listeners>
                                    <Click Handler="if(!pid) return;#{GridPanel1}.insertRecord(0,{orderID:0,ParentID:pid,IsGroup:'0',IsCore:'0'}); #{rowEditor1}.startEditing(0);" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button ID="Button2" runat="server" Icon="UserDelete" Text="删除">
                                <Listeners>
                                    <Click Handler="#{GridPanel1}.deleteSelected();" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button ID="btnLoad" runat="server" Text="刷新" Icon="ArrowRefresh">
                                <Listeners>
                                    <Click Handler="#{GridPanel1}.reload({params:{ParentID:pid}});" />
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
                            <ext:RowEditor ID="rowEditor1" runat="server" SaveText="保存" CancelText="取消">
                            </ext:RowEditor>
                        </Plugins>
                        <ColumnModel ID="ColumnModel1" runat="server">
                            <Columns>
                            <ext:RowNumbererColumn></ext:RowNumbererColumn>
                                <ext:Column Header="id" Hidden="true" DataIndex="id" />
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
