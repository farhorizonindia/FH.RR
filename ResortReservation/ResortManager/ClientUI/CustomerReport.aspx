<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerReport.aspx.cs" Inherits="ClientUI_CustomerReport"  EnableEventValidation="false" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration Report</title>
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../css/viewbookings.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../css/calendar-blue2.css" title="win2k-cold-1" />
    <link rel="stylesheet" type="text/css" media="all" href="https://cdn.datatables.net/1.10.15/css/jquery.dataTables.min.css" />
    <link rel="stylesheet" type="text/css" media="all" href="https://cdn.datatables.net/buttons/1.3.1/css/buttons.dataTables.min.css" />
    
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar.js"></script>
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-en.js"></script>
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-setup.js"></script>
    <script language="javascript" type="text/javascript" src="../js/global.js"></script>
    <script language="javascript" type="text/javascript" src="../js/client/viewbookings.js"></script>

    <%-- <script type="text/javascript">
        function PrintGridData() {
            var prtGrid = document.getElementById('<%=dgTouristCount.ClientID %>');
            prtGrid.border = 0;
            var prtwin = window.open('', 'PrintGridViewData', 'left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.outerHTML);
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.close();
        }
    </script>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#example').DataTable({
                dom: 'Bfrtip',
                buttons: [
                    'copy', 'csv', 'excel', 'pdf', 'print'
                ]
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Customer Registration Report" />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

         <div style="width: 75%; margin-top:1%; margin-bottom:1%" align="center">

             <asp:TextBox ID="txtSearch" Width="300px" placeholder="Search For Name,Email,ContactNo"  runat="server"></asp:TextBox>
              <asp:DropDownList ID="ddlcountries" runat="server" Width="150px"></asp:DropDownList>
         <asp:DropDownList ID="drpStatus" runat="server">
             
                                        <asp:ListItem Value="true">Active</asp:ListItem>
                                        <asp:ListItem Value="false">InActive</asp:ListItem>
                                      
                                    </asp:DropDownList>
             <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"   />
             </div>

        <div style="width: 75%;" align="center">

            <asp:UpdatePanel ID="pnlTouristCount" runat="server">
                <ContentTemplate>

                     



                    <div style="width: 100%;" id="ContentDiv" runat="server">
                        <asp:GridView ID="dgTouristCount" CellPadding="4" ForeColor="#333333"
                            runat="server" Width="133%" AutoGenerateColumns="false" AllowPaging="True" PageSize="20" OnRowCommand="dgTouristCount_RowCommand" CellSpacing="10" OnPageIndexChanging="dgTouristCount_PageIndexChanging" OnRowEditing="dgTouristCount_RowEditing" AllowCustomPaging="False">


                            <AlternatingRowStyle BackColor="White" />
                            <RowStyle HorizontalAlign="Left" />


                            <Columns>
                                <%-- <asp:BoundField DataField="CustId" HeaderText="Customer Id" Visible="false" />--%>
                                <asp:BoundField DataField="Title" HeaderText="Title" />
                                <asp:BoundField DataField="FirstName" HeaderText="FirstName" />
                                <asp:BoundField DataField="LastName" HeaderText="LastName" />
                                <asp:BoundField DataField="Email" HeaderText="Email" />
                                <asp:BoundField DataField="Telephone" HeaderText="Contact Number" />
                                <%-- <asp:BoundField DataField="Address1" HeaderText="Address 1" />
                                <asp:BoundField DataField="Address2" HeaderText="Address 2" />--%>
                                <asp:BoundField DataField="City" HeaderText="City" />
                                <asp:BoundField DataField="State" HeaderText="State" />
                                <asp:BoundField DataField="PostalCode" HeaderText="Postal Code" />
                                <asp:BoundField DataField="CountryName" HeaderText="Country Name" />
                                <asp:BoundField DataField="Password" HeaderText="Password" Visible="false" />
                                <asp:BoundField DataField="PaymentMethod" HeaderText="PaymentMethod" />
                                 <asp:BoundField DataField="AgentName" HeaderText="AgentName" />
                                <%--<asp:BoundField DataField="PostalCode" HeaderText="Postal Code" />--%>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("CustId")%>' CommandName="Select">Edit</asp:LinkButton>
                                        <%--<asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you certain you want to delete this Rate Card?') ">Delete</asp:LinkButton>--%>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reset Password">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("CustId")%>' CommandName="Sendmail">Send Email</asp:LinkButton>                                     
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>

                    </div>
                    <div>
                    </div>
                    <br />
                    <asp:HiddenField ID="hfId" runat="server" />
                    <br />
                    <div style="display:none">
                        <input type="button" id="btnPrint" value="Print" onclick="PrintPanel()" />
                     <asp:Button ID="Button1" runat="server" Text="print" visible="false" OnClick="Button1_Click" />
                    </div>
                    <br />
                    <div style="margin-top: 1%; margin-left: 2%;">
                        <table>
                            <tr>
                                <td>Title</td>
                                <td>
                                    <asp:DropDownList ID="ddlTitle" runat="server">
                                        <asp:ListItem>Mr</asp:ListItem>
                                        <asp:ListItem>Mrs</asp:ListItem>
                                        <asp:ListItem>Miss</asp:ListItem>
                                        <asp:ListItem>Ms</asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="height: 3px;"></td>
                            </tr>
                            <tr>
                                <td>First Name</td>
                                <td>
                                    <asp:TextBox ID="txtFisrtName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="abc" ControlToValidate="txtFisrtName" ForeColor="Red" runat="server" Text="*" ErrorMessage=""></asp:RequiredFieldValidator>
                                    </td>
                            </tr>
                            <tr>
                                <td style="height: 3px;"></td>
                            </tr>
                            <tr>
                                <td>Last Name</td>
                                <td>
                                    <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>

                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="abc" ControlToValidate="txtLastName" ForeColor="Red" runat="server" Text="*" ErrorMessage=""></asp:RequiredFieldValidator>
                                          </td>
                            </tr>
                            <tr>
                                <td style="height: 3px;"></td>
                            </tr>
                            <tr>
                                <td>Email</td>
                                <td>
                                    <asp:TextBox ID="txtEmail" ReadOnly="false" runat="server"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="abc" ControlToValidate="txtEmail" ForeColor="Red" runat="server" Text="*" ErrorMessage=""></asp:RequiredFieldValidator>
                                          </td>
                            </tr>
                            <tr>
                                <td style="height: 3px;"></td>
                            </tr>
                            <tr>
                                <td>Contact No</td>
                                <td>
                                    <asp:TextBox ID="txtContactNo" runat="server" MaxLength="10"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="abc" ControlToValidate="txtContactNo" ForeColor="Red" runat="server" Text="*" ErrorMessage=""></asp:RequiredFieldValidator>
                                          </td>
                            </tr>
                            <tr>
                                <td style="height: 3px;"></td>
                            </tr>
                            <tr>
                                <td>Address1</td>
                                <td>
                                    <asp:TextBox ID="txtAddress1" runat="server"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="abc" ControlToValidate="txtAddress1" ForeColor="Red" runat="server" Text="*" ErrorMessage=""></asp:RequiredFieldValidator>
                                          </td>
                            </tr>
                            <tr>
                                <td style="height: 3px;"></td>
                            </tr>
                            <tr>
                                <td>Address2</td>
                                <td>
                                    <asp:TextBox ID="txtAddress2" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="height: 3px;"></td>
                            </tr>
                            <tr>
                                <td>City</td>
                                <td>
                                    <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="abc" ControlToValidate="txtCity" ForeColor="Red" runat="server" Text="*" ErrorMessage=""></asp:RequiredFieldValidator>
                                          </td>
                            </tr>
                            <tr>
                                <td style="height: 3px;"></td>
                            </tr>
                            <tr>
                                <td>State</td>
                                <td>
                                    <asp:TextBox ID="txtState" runat="server"></asp:TextBox>

                              <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="abc" ControlToValidate="txtState" ForeColor="Red" runat="server" Text="*" ErrorMessage=""></asp:RequiredFieldValidator>
                                          </td>
                            </tr>
                            <tr>
                                <td style="height: 3px;"></td>
                            </tr>
                            <tr>
                                <td>Post Code</td>
                                <td>
                                    <asp:TextBox ID="txtPost" runat="server" ></asp:TextBox>

                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="abc" ControlToValidate="txtPost" ForeColor="Red" runat="server" Text="*" ErrorMessage=""></asp:RequiredFieldValidator>
                                          </td>
                            </tr>
                            <tr>
                                <td style="height: 3px;"></td>
                            </tr>
                            <tr>
                                <td>Country</td>
                                <td>
                                    <asp:DropDownList ID="ddlCountry" runat="server" Width="52%"></asp:DropDownList>

                              <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="abc" ControlToValidate="ddlCountry" ForeColor="Red" runat="server" Text="*" ErrorMessage=""></asp:RequiredFieldValidator>
                                          </td>
                            </tr>
                            <tr>
                                <td style="height: 3px;"></td>
                            </tr>
                              <tr>
                                <td>Active</td>
                                <td>
                                    <asp:CheckBox ID="chkStatus"  runat="server" />
                                    </td>
                            </tr>
                            <tr>
                                <td style="height: 3px;"></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnUpdate" runat="server" ValidationGroup="abc" Text="Update" OnClick="btnUpdate_Click" /></td>
                                <td>
                                    <%-- <asp:Button ID="btnDelete" runat="server" Text="Delete" />--%></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label></td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

         <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="200">
        <ProgressTemplate>
            <iframe id="pgrIFrame" frameborder="0" marginheight="0" marginwidth="0" scrolling="no"
                src="javascript:'<html></html>';" style="position: absolute; top: 729px; left: 36px;
                height: 68px; width: 208px; z-index: 19999"></iframe>
            <asp:Panel ID="Panel1" runat="server" BackColor="white" BorderColor="#C2D3FC" BorderStyle="solid"
                BorderWidth="1" Height="100" Style="z-index: 20000" Width="300">
                <div style="position: relative; top: 20px; left: 70px;">
                    <asp:Image ID="image2" runat="server" ImageUrl="~/images/indicator.gif" />
                    
                Please Wait....
            </asp:Panel>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <cc1:AlwaysVisibleControlExtender ID="AlwaysVisibleControlExtender1" runat="server"
        TargetControlID="Panel1" HorizontalOffset="300" VerticalOffset="150">
    </cc1:AlwaysVisibleControlExtender>
    </form>
</body>
</html>
