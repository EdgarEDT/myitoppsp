<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="webdesk.aspx.cs" Inherits="Itop.WebFrame.webdesk" %>
<%@ Register assembly="Ext.Net" namespace="Ext.Net" tagprefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Web桌面</title>    
    
    <style type="text/css">        
        .start-button {
            background-image: url(vista_start_button.gif) !important;
            font: normal 12px tahoma,arial,verdana,sans-serif;
        }
        
        .shortcut-icon {
            width: 48px;
            height: 48px;
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src="window.png", sizingMethod="scale");
        }
        
        .icon-grid48 {
            background-image: url(grid48x48.png) !important;
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src="grid48x48.png", sizingMethod="scale");
        }
        
        .icon-user48 {
            background-image: url(user48x48.png) !important;
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src="user48x48.png", sizingMethod="scale");
        }
        
        .icon-window48 {
            background-image: url(window48x48.png) !important;
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src="window48x48.png", sizingMethod="scale");
        }
        
        .desktopEl {
            position: absolute !important;
        }
        .x-shortcut-text {
            font: normal 12px tahoma,arial,verdana,sans-serif;
        }
    </style>
    
    <script type="text/javascript">
        var alignPanels = function () {
            pnlSample.getEl().alignTo(Ext.getBody(), "tr", [-505, 5], false)
        };

        var template = '<span style="color:{0};">{1}</span>';

        var change = function (value) {
            return String.format(template, (value > 0) ? "green" : "red", value);
        };

        var pctChange = function (value) {
            return String.format(template, (value > 0) ? "green" : "red", value + "%");
        };
        var wins={};


        var createDynamicWindow = function (app) {
            var desk = app.getDesktop();

            var w = desk.createWindow({
                title  : "Dynamic Web Browser",
                width  : 1000,
                height : 600,
                maximizable : true,
                minimizable : true,
                autoLoad : {
                url: "../overview",
                    mode : "iframe",
                    showMask : true
                }
            });

            w.center();
            w.show();
        };
        function createwin(app,url) {
            var desk = app.getDesktop();

            var w = desk.createWindow({
                title: "",
                width: 800,
                height: 500,
                maximizable: true,
                minimizable: true,
                autoLoad: {
                    url: url,
                    mode: "iframe",
                    showMask: true
                }
            });

            w.center();
            return w;
        }
        function showmodule(app, id) {
            if (wins[id]) {
                wins[id].show();
            } else {
            util.geturl(id, {
                success: function(sysprog) {
                    var w = createwin(app, sysprog.ProgClass);
                    w.title = sysprog.ProgName;
                    w.show();
                    //wins[id] = w;
                }
            });
            }
        }
        var menuclick = function(app, id) {
        var d = app.getDesktop(); if (id == 'scTile') { d.tile(); } else if (id == 'scCascade') { d.cascade(); } else { showmodule(app, id); }
        };
    </script>
</head>
<body>
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server">
            <Listeners>
                <DocumentReady Handler="alignPanels();" />
                <WindowResize Handler="alignPanels();" />
            </Listeners>
        </ext:ResourceManager>
        
        <%--Quick Search--%>
        
        <ext:Store 
            ID="QuickSearchStore" 
            runat="server" 
            AutoLoad="false" 
            OnRefreshData="GetQuickSearchItems">
            <Proxy>
                <ext:PageProxy />
            </Proxy>
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="SearchItem" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <BaseParams>
                <ext:Parameter Name="Filter" Value="#{QuickSearchFilter}.getValue()" Mode="Raw" />
            </BaseParams>
        </ext:Store>
        
        <%--End Quick Search--%>

        <ext:Desktop 
            ID="MyDesktop" 
            runat="server" 
            
            BackgroundColor="Black" 
            ShortcutTextColor="White" 
            Wallpaper="desktop.jpg">
            <StartButton Text="开始" IconCls="start-button" />
            <%-- NOTE: Body Controls must be added to a container with position:absolute --%>
            <Content>
                <ext:Panel 
                    ID="pnlSample" 
                    runat="server" 
                    Title="任务表"
                    Cls="desktopEl" 
                    Height="400" 
                    Width="500"
                    Padding="5"
                    CollapseMode="Mini"
                    Collapsible="true">
                    <Items>
                        
                    </Items>
                </ext:Panel>
            </Content>
            <Modules>
                <%--<ext:DesktopModule ModuleID="DesktopModule1" WindowID="winCustomer" AutoRun="false">
                    <Launcher ID="Launcher1" runat="server" Text="Add Customer" Icon="Add" />
                </ext:DesktopModule>
                
                <ext:DesktopModule ModuleID="DesktopModule2" WindowID="winCompany" AutoRun="false">
                    <Launcher ID="Launcher2" runat="server" Text="Company Info" Icon="Lorry" />
                </ext:DesktopModule>
                
                <ext:DesktopModule ModuleID="DesktopModule3" WindowID="winBrowser">
                    <Launcher ID="Launcher3" runat="server" Text="Web Browser" Icon="World" />
                </ext:DesktopModule>--%>
            </Modules>  
            
            <Shortcuts>
                <%--<ext:DesktopShortcut ModuleID="DesktopModule1" Text="人员管理" IconCls="shortcut-icon icon-user48" />
                <ext:DesktopShortcut ModuleID="DesktopModule2" Text="单位管理" IconCls="shortcut-icon icon-grid48" />--%>
                <%--<ext:DesktopShortcut ShortcutID="m1" Text="模块1" IconCls="shortcut-icon icon-window48" />
                <ext:DesktopShortcut ShortcutID="m1" Text="模块2" IconCls="shortcut-icon icon-window48"  />--%>
                <ext:DesktopShortcut ShortcutID="scTile" Text="层叠窗口" IconCls="shortcut-icon icon-window48"
                    X="{DX}-90" Y="{DY}-90" />
                <ext:DesktopShortcut ShortcutID="scCascade" Text="平铺窗口" IconCls="shortcut-icon icon-window48"
                    X="{DX}-90" Y="{DY}-170" />
            </Shortcuts>
            
            <Listeners>
                <ShortcutClick Handler="menuclick(#{MyDesktop},id);" />
            </Listeners>
            
            <StartMenu Width="400" Height="400" ToolsWidth="227" Title="功能导航">
                <%--<ToolItems>
                    <ext:MenuItem Text="Settings" Icon="Wrench">
                        <Listeners>
                            <Click Handler="Ext.Msg.alert('Message', 'Settings Clicked');" />
                        </Listeners>
                    </ext:MenuItem>
                    <ext:MenuItem Text="Logout" Icon="Disconnect">
                        <DirectEvents>
                            <Click OnEvent="Logout_Click">
                                <EventMask ShowMask="true" Msg="Good Bye..." MinDelay="1000" />
                            </Click>
                        </DirectEvents>
                    </ext:MenuItem>
                    
                    <ext:MenuSeparator />
                    
                    <ext:ComponentMenuItem runat="server" Shift="false">   
                        <Component>
                            <ext:GridPanel ID="QuickSearchGrid" runat="server" Width="210" Height="275" StoreID="QuickSearchStore" AutoExpandColumn="SearchItem">
                                <ColumnModel>
                                    <Columns>
                                        <ext:CommandColumn Width="30">
                                            <Commands>
                                                <ext:GridCommand Icon="Note" />
                                            </Commands>
                                        </ext:CommandColumn>
                                        
                                        <ext:Column ColumnID="SearchItem" Header="SearchItem" DataIndex="SearchItem" />
                                    </Columns>
                                </ColumnModel>
                                <SelectionModel>
                                    <ext:RowSelectionModel runat="server" SingleSelect="true" />
                                </SelectionModel>
                                <LoadMask ShowMask="true" />
                            </ext:GridPanel>
                        </Component>                     
                    </ext:ComponentMenuItem>
                    
                    <ext:ComponentMenuItem runat="server" Target="#{QuickSearchFilter}" Shift="false" ComponentElement="Wrap"> 
                        <Component>
                            <ext:TriggerField ID="QuickSearchFilter" runat="server" Width="210">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Handler="if (index === 1) { trigger.hide(); this.setValue(''); } else { this.triggers[1].show(); } #{QuickSearchGrid}.reload();" />
                                </Listeners>
                            </ext:TriggerField>
                        </Component>                      
                    </ext:ComponentMenuItem>
                </ToolItems>--%>
                
                <Items>
                    <ext:MenuItem ID="MenuItem1" runat="server" Text="测试" Icon="Folder" HideOnClick="false">
                        <Menu>
                            <ext:Menu ID="Menu1" runat="server">
                                <Items>
                                    <%--<ext:MenuItem Text="Add Customer" Icon="Add">
                                        <Listeners>
                                            <Click Handler="#{winCustomer}.show();" />
                                        </Listeners>
                                    </ext:MenuItem>
                                    <ext:MenuItem Text="Company Info" Icon="Lorry">
                                        <Listeners>
                                            <Click Handler="#{winCompany}.show();" />
                                        </Listeners>
                                    </ext:MenuItem>
                                    <ext:MenuItem Text="Web Browser" Icon="World">
                                        <Listeners>
                                            <Click Handler="#{winBrowser}.show();" />
                                        </Listeners>
                                    </ext:MenuItem>--%>
                                    <ext:MenuItem Text="Create dynamic" Icon="World">
                                        <Listeners>
                                            <Click Handler="createDynamicWindow(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>
                                </Items>
                            </ext:Menu>
                        </Menu>
                    </ext:MenuItem>
                    <ext:MenuSeparator />
                </Items>
            </StartMenu>
        </ext:Desktop>
        
        <ext:Store ID="Store1" runat="server" OnRefreshData="MyData_Refresh">
            <Reader>
                <ext:ArrayReader>
                    <Fields>
                        <ext:RecordField Name="company" />
                        <ext:RecordField Name="price" Type="Float" />
                        <ext:RecordField Name="change" Type="Float" />
                        <ext:RecordField Name="pctChange" Type="Float" />
                        <ext:RecordField Name="lastChange" Type="Date" DateFormat="yyyy-MM-ddTHH:mm:ss" />
                    </Fields>
                </ext:ArrayReader>
            </Reader>
        </ext:Store>
        
        <ext:DesktopWindow 
            ID="winCustomer" 
            runat="server" 
            Title="Add Customer" 
            InitCenter="false"
            Modal="true"
            Icon="User" 
            Padding="5"
            Width="350"
            Height="200"
            PageX="100" 
            PageY="25"
            Layout="Form">
            <Items>
                <ext:TextField ID="txtFirstName" runat="server" FieldLabel="First Name" Text="Steve" AnchorHorizontal="100%" />
                <ext:TextField ID="txtLastName" runat="server" FieldLabel="Last Name" Text="Caballero" AnchorHorizontal="100%" />
                <ext:TextField ID="txtCompany" runat="server" FieldLabel="Company" Text="Awesome Industries" AnchorHorizontal="100%" />
                <ext:ComboBox ID="cmbCountry" runat="server" FieldLabel="Country" AnchorHorizontal="100%">
                    <SelectedItem Value="United States" />
                    <Items>
                        <ext:ListItem Text="Australia" />
                        <ext:ListItem Text="Canada" />
                        <ext:ListItem Text="Great Britian" />
                        <ext:ListItem Text="Japan" />
                        <ext:ListItem Text="United States" />
                    </Items>
                </ext:ComboBox>
                <ext:Checkbox ID="chkPremium" runat="server" FieldLabel="Premium Member" Checked="true" AnchorHorizontal="100%" />
            </Items>
            <Buttons>
                <ext:Button ID="btnSaveCustomer" runat="server" Text="Save" Icon="Disk">
                    <Listeners>
                        <Click Handler="Ext.net.DirectMethods.AddCustomer({
                            success: function (customer) {
                                var template = 'ID: {0}{7} Name: {1} {2}{7} Company: {3}{7} Country: {4}{7} Premium Member: {5}{7} Date Created: {6}{7}',
                                    msg = String.format(template, 
                                            customer.ID, 
                                            customer.FirstName, 
                                            customer.LastName, 
                                            customer.Company, 
                                            customer.Country.Name, 
                                            customer.Premium, 
                                            customer.DateCreated,
                                            '&lt;br /&gt;&lt;br /&gt;');
                                
                                Ext.Msg.alert('Customer Saved', msg);
                            }
                        });" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:DesktopWindow>
        
        <ext:DesktopWindow 
            ID="winCompany" 
            runat="server" 
            InitCenter="false"
            Title="Company Info" 
            Icon="Lorry"             
            Width="550"
            Height="320"
            PageX="200" 
            PageY="125"
            Layout="Fit">
            <TopBar>
                <ext:Toolbar ID="ToolBar1" runat="server">
                    <Items>
                        <ext:Button ID="btnSave" runat="server" Text="Save" Icon="Disk">
                            <Listeners>
                                <Click Handler="#{GridPanel1}.save();" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="btnLoad" runat="server" Text="Reload" Icon="ArrowRefresh">
                            <Listeners>
                                <Click Handler="#{GridPanel1}.load();" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="extbtnedit" runat="server" Icon="Add">
                            <ToolTips>
                                <ext:ToolTip ID="ToolTip2" Title="Edit Entry" runat="server" Html="Edit" />
                            </ToolTips>
                        </ext:Button>
                    </Items>
                </ext:Toolbar>
            </TopBar>           
            <Items>
                <ext:GridPanel 
                    ID="GridPanel1" 
                    runat="server" 
                    StoreID="Store1" 
                    StripeRows="true"
                    Border="false"
                    AutoExpandColumn="Company">
                    <ColumnModel ID="ColumnModel1" runat="server">
                        <Columns>
                            <ext:Column ColumnID="Company" Header="Company" Width="160" DataIndex="company" />
                            <ext:Column Header="Price" Width="75" DataIndex="price">
                                <Renderer Format="UsMoney" />
                            </ext:Column>
                            <ext:Column Header="Change" Width="75" DataIndex="change">
                                <Renderer Fn="change" />
                            </ext:Column>
                            <ext:Column Header="Change" Width="75" DataIndex="pctChange">
                                <Renderer Fn="pctChange" />
                            </ext:Column>
                        </Columns>
                    </ColumnModel>
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" />
                    </SelectionModel>
                    <LoadMask ShowMask="true" />
                    <BottomBar>
                        <ext:PagingToolbar ID="PagingToolBar2" runat="server" PageSize="10" StoreID="Store1" />
                    </BottomBar>
                </ext:GridPanel>
            </Items>
        </ext:DesktopWindow>
        
        <%--<ext:DesktopWindow 
            ID="winBrowser" 
            runat="server" 
            Title="Web Browser" 
            Icon="World"
            InitCenter="true"             
            Width="600"
            Height="400"
             
            >
            <AutoLoad Url="~/desktop" Mode="IFrame" ShowMask="true" />
        </ext:DesktopWindow>--%>
    </form>
</body>
</html>