<%@ Page Language="C#" %>

<%@ Register assembly="Ext.Net" namespace="Ext.Net" tagprefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>webµÇÂ¼</title>    
    
    <script runat="server">
        protected void Button1_Click(object sender, DirectEventArgs e)
        {
            // Do some Authentication...
            
            // Then user send to application
            Response.Redirect("webdesk.aspx");
        }
    </script>
</head>
<body>
    <form runat="server">
        <ext:ResourceManager runat="server" />
        
        <ext:Window 
            ID="Window1" 
            runat="server" 
            Closable="false"
            Resizable="false"
            Height="150" 
            Icon="Lock" 
            Title="µÇÂ¼"
            Draggable="false"
            Width="350"
            Modal="true"
            Padding="5"
            Layout="Form">
            <Items>
                <ext:TextField 
                    ID="txtUsername" 
                    runat="server" 
                    ReadOnly="false"
                    FieldLabel="¹¤ºÅ" 
                    AllowBlank="false"
                    BlankText="ÊäÈë¹¤ºÅ."
                    Text="Demo"
                    />
                <ext:TextField 
                    ID="txtPassword" 
                    runat="server" 
                    ReadOnly="false"
                    InputType="Password" 
                    FieldLabel="ÃÜÂë" 
                    AllowBlank="false" 
                    BlankText="ÊäÈëÃÜÂë."
                    Text="Demo"
                    />
            </Items>
            <Buttons>
                <ext:Button ID="Button1" runat="server" Text="µÇÂ¼" Icon="Accept">
                    <DirectEvents>
                        <Click OnEvent="Button1_Click" Success="Window1.close();">
                            <EventMask ShowMask="true" Msg="¼ÓÔØÊý¾Ý..." MinDelay="1000" />
                        </Click>
                    </DirectEvents>
                </ext:Button>
            </Buttons>
        </ext:Window>
    </form>
</body>
</html>