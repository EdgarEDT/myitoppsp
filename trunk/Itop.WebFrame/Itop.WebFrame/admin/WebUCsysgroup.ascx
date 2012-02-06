<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUCsysgroup.ascx.cs" Inherits="Itop.WebFrame.admin.WebUCsysgroup" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<ext:Store ID="sysgroupStore" runat="server" OnRefreshData="Data_Refresh" OnBeforeStoreChanged="HandleChanges"
        SkipIdForNewRecords="false" RefreshAfterSaving="None" AutoSave="true">
        <Reader>
            <ext:JsonReader>
                <Fields>
                    <ext:RecordField Name="id" />
                    <ext:RecordField Name="GroupCode" AllowBlank="false" />
                    <ext:RecordField Name="GroupName" AllowBlank="false" />
                    <ext:RecordField Name="GroupType" />
                    <ext:RecordField Name="Remark" />
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
    <ext:Viewport  runat="server" AutoHeight="true" Layout="BorderLayout">
        <Items>
            <ext:Panel  runat="server" Region="West" Width="350" Layout="FitLayout" Split="true">
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
                    <ext:GridPanel ID="GridPanel1" runat="server" StoreID="sysgroupStore" Layout="FitLayout" Border="false">
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
                                <ext:Column Header="组号" Width="60" DataIndex="GroupCode">
                                    <Editor>
                                        <ext:TextField ID="TextField1" runat="server">
                                        </ext:TextField>
                                    </Editor>
                                </ext:Column>
                                <ext:Column Header="组名" Width="75" DataIndex="GroupName">
                                    <Editor>
                                        <ext:TextField ID="TextField2" runat="server">
                                        </ext:TextField>
                                    </Editor>
                                </ext:Column>
                                <ext:Column Header="分类" Width="75" DataIndex="GroupType">
                                    <Editor>
                                        <ext:TextField ID="TextField3" runat="server">
                                        </ext:TextField>
                                    </Editor>
                                </ext:Column>
                                <ext:Column Header="备注" Width="75" DataIndex="Remark" Hidden="true">
                                </ext:Column>
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" />
                        </SelectionModel>
                        <LoadMask ShowMask="true" />
                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolBar2" runat="server" PageSize="18" StoreID="sysgroupStore" />
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
            <ext:Panel ID="Panel1"  runat="server" Region="Center" Layout="FitLayout">
            </ext:Panel>
        </Items>
    </ext:Viewport>