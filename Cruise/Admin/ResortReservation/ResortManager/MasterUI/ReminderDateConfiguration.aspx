<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReminderDateConfiguration.aspx.cs" Inherits="MasterUI_ReminderDateConfiguration" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    <title>Reminder Date Configuration</title>

    <script language="javascript" type="text/javascript" src="../js/calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-en.js"></script>

    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-setup.js"></script>

    <script language="javascript" type="text/javascript" src="../js/master/accommaster.js"></script>

    <script language="javascript" type="text/javascript" src="../js/global.js"></script>

    <link rel="stylesheet" type="text/css" media="all" href="../css/calendar-blue2.css"
        title="win2k-cold-1" />
</head>
<body>
    <form id="form1" runat="server">
         <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Reminder Date Configuration" />
   
    <asp:ScriptManager ID="scmgrAccomMaster" runat="server">
        </asp:ScriptManager>
      
 <div>
      <div id="gridsection" class="gridsection">


          <asp:GridView ID="gdvReminderDays" runat="server" AutoGenerateColumns="False" DataKeyNames="Id">
               <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  />
                <Columns>
                    <asp:BoundField DataField="EmailId" HeaderText="Email Id" />
                    <asp:BoundField DataField="Days" HeaderText="Remind After(Days)" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkEdit_Click">Edit</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
               </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <AlternatingRowStyle BackColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />


          </asp:GridView>

          </div>


        <table id="inputsection" class="inputsection">
                    <tr>
                        <td class="labelcell">
                            
                            Email Id:
                        </td>
                        <td class="inputcell">
                            <asp:TextBox CssClass="input" ID="txtEmailId" runat="server" MaxLength="100" Width="260px"
                                TabIndex="4"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="labelcell">
                           Remind After:
                        </td>
                        <td class="inputcell">
                            <asp:TextBox CssClass="input" ID="txtRemAfter" runat="server" MaxLength="5" TabIndex="5"
                                Width="39px"></asp:TextBox>Day(s)
                        </td>
                    </tr>
                </table>

     <table id="buttonsection" class="buttonsection">
                    <tr>
                        <td>
                            <asp:Button cssclass="appbutton" ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                        </td>
                        <td>
                            <asp:Button cssclass="appbutton" ID="btnClear" runat="server" Text="clear" OnClick="btnClear_Click" />
                        </td>
                        <td>
                          
                            <asp:HiddenField ID="hfId" runat="server" />
                          
                        </td>
                    </tr>
                </table>

  </div>
              



  
    </form>
</body>
</html>
