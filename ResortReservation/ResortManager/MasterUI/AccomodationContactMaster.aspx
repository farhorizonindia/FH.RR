<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccomodationContactMaster.aspx.cs" Inherits="MasterUI_AccomodationContactMaster" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" /> 
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" /> 
    <link rel="stylesheet" type="text/css" media="all" href="../css/accomodationContacts.css" /> 
    <title>Accomodation Contacts Master</title>    
</head>
<body>
    <form id="form1" runat="server">    
    <phc:PageHeaderControl id="pageheader1" runat="server" PageTitle="Accomodation Contacts Master" />
        <div style="width:100%;">
            <asp:ScriptManager ID="scmgrAccomodationContactMaster" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="pnlAccomodationContactMaster" runat="server">
                <ContentTemplate>                
        <table id="filtersection" class="filtersection">
            <tr>
                <td class="labelcell">
                    Accomodation:</td>
                <td class="inputcell">
                    <asp:DropDownList ID="ddlAccomodation" runat="server" Width="268px" TabIndex="0" AutoPostBack="True" OnSelectedIndexChanged="ddlAccomodation_SelectedIndexChanged">
                    </asp:DropDownList></td>
                    
                <td class="labelcell" style="width:auto;">
                    Copy Contacts to this Accomodation:</td>
                <td class="inputcell">
                    <asp:DropDownList ID="ddlDestinationAccomodation" runat="server" Width="268px" TabIndex="0">
                    </asp:DropDownList></td>
                <td class="inputcell">
                    <asp:Button cssclass="appbutton" ID="btnCopyContacts" runat="server" Text="Copy Contacts" OnClick="btnCopyContacts_Click" Width="105px" />
                </td>
            </tr>
         </table>
        <div id="gridsection" class="gridsection">
           <asp:DataGrid ID="dgAccomodationContacts" runat="server" AutoGenerateColumns="False" BorderStyle="Ridge"
                CellPadding="4" DataKeyField="ContactId" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="dgAccomodationContacts_SelectedIndexChanged">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditItemStyle BackColor="#2461BF" />
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <AlternatingItemStyle BackColor="White" />
                <ItemStyle BackColor="#EFF3FB" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:BoundColumn DataField="ContactId" HeaderText="Message Id" Visible="False">
                    <ItemStyle CssClass="columnPadding" />
                    <HeaderStyle CssClass="columnPadding" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="AccomodationId" HeaderText="Accomodation Id" Visible="False">
                    <ItemStyle CssClass="columnPadding" />
                    <HeaderStyle CssClass="columnPadding" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Contactname" HeaderText="Contact Name">
                    <ItemStyle CssClass="columnPadding" />
                    <HeaderStyle CssClass="columnPadding" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="ToId" HeaderText="To" Visible="false">
                    <ItemStyle CssClass="columnPadding" />
                    <HeaderStyle CssClass="columnPadding" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="CCId" HeaderText="CC" Visible="false">
                    <ItemStyle CssClass="columnPadding" />
                    <HeaderStyle CssClass="columnPadding" />
                    </asp:BoundColumn>                    
                    <asp:BoundColumn DataField="BCCId" HeaderText="BCC" Visible="false">
                    <ItemStyle CssClass="columnPadding" />
                    <HeaderStyle CssClass="columnPadding" />
                    </asp:BoundColumn>                    
                    <asp:BoundColumn DataField="MailOnBooking" HeaderText="Booking Mail">
                    <ItemStyle CssClass="columnPadding" />
                    <HeaderStyle CssClass="columnPadding" />
                    </asp:BoundColumn>                    
                    <asp:BoundColumn DataField="MailOnBookingUpdate" HeaderText="Booking Update Mail">
                    <ItemStyle CssClass="columnPadding" />
                    <HeaderStyle CssClass="columnPadding" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="MailOnBookingConfirmation" HeaderText="Conf Mail">
                    <ItemStyle CssClass="columnPadding" />
                    <HeaderStyle CssClass="columnPadding" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="MailOnBookingConfirmationUpdate" HeaderText="Conf Update Mail">
                    <ItemStyle CssClass="columnPadding" />
                    <HeaderStyle CssClass="columnPadding" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="MailOnCancellation" HeaderText="Cancellation Mail">
                    <ItemStyle CssClass="columnPadding" />
                    <HeaderStyle CssClass="columnPadding" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="MailOnDeletion" HeaderText="Deletion Mail">
                    <ItemStyle CssClass="columnPadding" />
                    <HeaderStyle CssClass="columnPadding" />
                    </asp:BoundColumn>
                    <asp:ButtonColumn CommandName="Select" HeaderText="[...]" Text="Edit">
                    <ItemStyle CssClass="columnPadding" />
                    <HeaderStyle CssClass="columnPadding" />
                    </asp:ButtonColumn>
                </Columns>
            </asp:DataGrid> 
        </div>
        <table id="inputsection" class="inputsection">                        
        <tr>
            <td class="labelcell" style="width: 231px">
                Contact Name:</td>
            <td class="inputcell" style="width: 565px">
                <asp:TextBox cssclass="input"  ID="txtContactName" runat="server" MaxLength="50" Width="260px" TabIndex="2"></asp:TextBox></td>                
        </tr>
        <tr>
            <td class="labelcell" style="width: 231px">
                To Ids:</td>
            <td class="inputcell" style="width: 565px">
                <asp:TextBox cssclass="input"  ID="txtToIds" runat="server" MaxLength="4000" TabIndex="3" Width="427px"></asp:TextBox></td>                
        </tr>
        <tr>
            <td class="labelcell" style="width: 231px">
                CC Ids:</td>
            <td class="inputcell" style="width: 565px">
                <asp:TextBox cssclass="input"  ID="txtCCIds" runat="server" Width="426px" TabIndex="4" MaxLength="100"></asp:TextBox></td>                                  
        </tr>
            <tr>
                <td class="labelcell" style="width: 231px">
                    BCC Ids</td>
                <td class="inputcell" style="width: 565px">
                    <asp:TextBox ID="txtBCCIds" runat="server" CssClass="input" MaxLength="100" TabIndex="5"
                        Width="426px"></asp:TextBox></td>
            </tr>
        <tr>
            <td class="labelcell" style="height: 20px; width: 231px;">
                Mail On Booking:</td>
            <td class="inputcell" style="height: 20px; width: 565px;">
                <asp:RadioButton ID="rdBookingYes" GroupName="Booking" runat="server" Text="Yes" TabIndex="6" />
                <asp:RadioButton ID="rdBookingNo" GroupName="Booking" runat="server" Text="No" TabIndex="7"/></td> 
        </tr>
            <tr>
                <td class="labelcell" style="width: 231px; height: 20px">
                    Mail On Booking Update:</td>
                <td class="inputcell" style="width: 565px; height: 20px">
                    <asp:RadioButton ID="rdBookingUpdatYes" GroupName="BookingUpdate" runat="server" Text="Yes" TabIndex="10" />
                    <asp:RadioButton ID="rdBookingUpdatNo" GroupName="BookingUpdate" runat="server" Text="No" TabIndex="10"/></td>
            </tr>
            <tr>
                <td class="labelcell" style="width: 231px; height: 20px">
                    Mail On Confirmation</td>
                <td class="inputcell" style="width: 565px; height: 20px">
                    <asp:RadioButton ID="rdConfirmationYes" GroupName="Confirmation" runat="server" Text="Yes" TabIndex="11" />
                    <asp:RadioButton ID="rdConfirmationNo" GroupName="Confirmation" runat="server" Text="No" TabIndex="12"/></td>
            </tr>
            <tr>
                <td class="labelcell" style="width: 231px; height: 20px">
                    Mail On Confirmation Update</td>
                <td class="inputcell" style="width: 565px; height: 20px">
                    <asp:RadioButton ID="rdConfirmationUpdateYes" GroupName="ConfirmationUpdate" runat="server" Text="Yes" TabIndex="13" />
                    <asp:RadioButton ID="rdConfirmationUpdateNo" GroupName="ConfirmationUpdate" runat="server" Text="No" TabIndex="14"/></td>
            </tr>
            <tr>
                <td class="labelcell" style="width: 231px; height: 20px">
                    Mail On Cancellation</td>
                <td class="inputcell" style="width: 565px; height: 20px">
                    <asp:RadioButton ID="rdCancellationYes" GroupName="Cancellation" runat="server" Text="Yes" TabIndex="15" />
                    <asp:RadioButton ID="rdCancellationNo" GroupName="Cancellation" runat="server" Text="No" TabIndex="16"/></td>
            </tr>
            <tr>
                <td class="labelcell" style="width: 231px; height: 20px">
                    Mail On Deletion</td>
                <td class="inputcell" style="width: 565px; height: 20px">
                    <asp:RadioButton ID="rdDeletionYes" GroupName="Deletion" runat="server" Text="Yes" TabIndex="17" />
                    <asp:RadioButton ID="rdDeletionNo" GroupName="Deletion" runat="server" Text="No" TabIndex="18"/></td>
            </tr>
        </table>
        <table id="buttonsection" class="buttonsection">
            <tr>
                <td>
                    <asp:Button cssclass="appbutton" ID="btnAddNew" runat="server" OnClick="btnAddNew_Click"
                    Text="New" Width="65px" TabIndex="19" />
                </td>
                <td>
                <asp:Button cssclass="appbutton" ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save"
                    Width="65px" TabIndex="20"/>
                </td>
                <td>
                    <asp:Button cssclass="appbutton" ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Update"
                        Width="65px" Visible="false" TabIndex="21"/></td>
                <td>
                    <asp:Button cssclass="appbutton" ID="btnDelete" runat="server" OnClick="btnDelete_Click"
                        Text="Delete" Width="65px" TabIndex="22"/></td>
                <td>
                    <asp:Button cssclass="appbutton" ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                        Width="65px" TabIndex="23" /></td>
            </tr>
        </table>
        <table id="statussection" class="statussection">
        <tr>
        <td>
        <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label></td>
        </tr>
        </table>
        <table id="hiddensection" class="hiddensection">
        <tr>
        <td>
        <asp:HiddenField ID="hfId" runat="server" />
        </td></tr></table>        
        </ContentTemplate>
        </asp:UpdatePanel>
        </div>        
    </form>
</body>
</html>
