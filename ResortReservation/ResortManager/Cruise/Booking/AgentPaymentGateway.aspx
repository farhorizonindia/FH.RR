<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgentPaymentGateway.aspx.cs" Inherits="AgentPaymentGateway" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

      
                <asp:Panel ID="pnlFullDetails" class="CSSTableGenerator" Width="100%" runat="server" Style="box-shadow: 3px 3px 9px 2px rgba(0,0,0,0.4); padding-top: 1%; border: 1px solid rgba(0,0,0,0.5) !important; border-radius: 4px; padding-bottom: 35px;">
                                    <div id="BookRef" runat="server"  style="width: 90%; margin: auto;display:none; padding-top: 5%; padding-bottom: 5px;">
                                        <table id="tbl-booking-name" style="border: 1px solid #fff; width: 100%;">
                                            <tr>
                                                <td class="auto-style5" style="padding: 0px 4px 0px 8px; font-weight: bold;">Enter Booking Reference Name.</td>
                                                <td style="text-align: left">
                                                    <asp:TextBox ID="txtBookRef" runat="server" Width="100%"></asp:TextBox>

                                                    <asp:RequiredFieldValidator ID="ReqBookRef" runat="server" ControlToValidate="txtBookRef" ErrorMessage="*" ValidationGroup="Pay"></asp:RequiredFieldValidator></td>
                                            </tr>
                                        </table>
                                    </div>
                                    <h4 style="font-size: 17px; padding-top: 14px; padding-bottom: 14px; text-align: center;font-family: 'Montserrat', sans-serif;">Payment Details</h4>

                                    <table class="table table-bordered" id="tbl-full-detail" style="width: 90%; margin: 0 auto; border: 1px solid #ddd;">
                                      
                                        
                                         <tr style="background-color: #ECECEC !important;">
                                            <td style="font-weight: bold; width: 27%; padding-right: 12%; padding-top: 10px;">Package Name</td>
                                            <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                <asp:Label ID="lblpckgname" runat="server"></asp:Label></td>
                                        </tr>
                                         <tr style="background-color: #ECECEC !important;">
                                            <td style="font-weight: bold; width: 27%; padding-right: 12%; padding-top: 10px;">CheckIn Date</td>
                                            <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                <asp:Label ID="lblchkin" runat="server"></asp:Label></td>
                                        </tr>
                                         <tr style="background-color: #ECECEC !important;">
                                            <td style="font-weight: bold; width: 27%; padding-right: 12%; padding-top: 10px;">Check Out Date</td>
                                            <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                <asp:Label ID="lblchkout" runat="server"></asp:Label></td>
                                        </tr>
                                        
                                        
                                          <tr style="background-color: #ECECEC !important;">
                                            <td style="font-weight: bold; width: 27%; padding-right: 12%; padding-top: 10px;">Invoice To</td>
                                            <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                <asp:Label ID="lblAgentName" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr style="background-color: #f9f9f9 !important;">
                                            <td style="font-weight: bold; padding-right: 7%; padding-top: 10px;">
                                                <asp:Label ID="lblBilling" runat="server" Text="Billing Address "></asp:Label></td>
                                            <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                <asp:Label ID="lblBillingAddress" runat="server" Text=""></asp:Label></td>

                                        </tr>
                                        <%-- <tr style="background-color: #ECECEC !important; height: 40px;">
                                            <td style="font-weight: bold; padding-right: 6%; padding-top: 10px;">
                                                <asp:Label ID="Label3" runat="server" Text="Special Request  "></asp:Label></td>
                                            <td style="">
                                                <asp:Label ID="lblSpecialRequest" runat="server" Text=""></asp:Label></td>

                                        </tr>--%>
                                        <tr style="background-color: #ECECEC !important;">
                                            <td style="font-weight: bold; padding-right: 5%; padding-top: 10px;">
                                                <asp:Label ID="lbPayment" runat="server" Text="Payment Method "></asp:Label></td>
                                            <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                <asp:Label ID="lbPaymentMethod" runat="server" Text=""></asp:Label><asp:HiddenField ID="hdnfPhoneNumber" runat="server" />
                                            </td>

                                        </tr>

                                        <tr style="display: none;">
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:HiddenField ID="hdnfCreditLimit" runat="server" />
                                            </td>
                                        </tr>
                                    </table>



                                    <asp:Panel ID="panelwithoutCreditAgent" Style="padding-top: 15px" Width="100%" runat="server" Font-Size="Medium">
                                        <div>

                                            <table class="table table-bordered" style="width: 90%; margin: 0 auto; border: 1px solid #ddd;">
                                                <tr style="background-color: #ECECEC !important;">
                                                    <td style="font-weight: bold; width: 27%; padding-right: 8%; padding-top: 10px;" class="auto-style3">Total amount</td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;" class="auto-style4">
                                                        <asp:Label ID="lbltotAmt" runat="server" Text="Label"></asp:Label></td>

                                                </tr>
                                                <tr style="background: #f9f9f9 !important;" id="getdiscount" runat="server">
                                                    <td style="font-weight: bold; padding-right: 17%; padding-top: 10px;">Discount(<asp:Label ID="lblDiscountper" runat="server" Text=" "></asp:Label>)</td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                        <asp:Label ID="lblDiscount" runat="server" Text=" "></asp:Label>
                                                    </td>

                                                </tr>
                                                <tr style="background: #ECECEC !important;">
                                                    <td style="font-weight: bold; padding-right: 17%; padding-top: 10px;">Taxable Amount</td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                        <asp:Label ID="lbltaxin" runat="server" Text=" "></asp:Label>
                                                    </td>

                                                </tr>
                                                <tr style="background: #f9f9f9 !important;">
                                                    <td style="font-weight: bold; padding-right: 19%; padding-top: 10px;">GST <%# Session["gettaxpercentage"].ToString() %></td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                        <asp:Label ID="lblTax" runat="server" Text=" "></asp:Label>
                                                    </td>

                                                </tr>
                                                <tr style="background: #f9f9f9 !important;" runat="server" id="lvlt" visible="false">
                                                    <td style="font-weight: bold; padding-right: 17%; padding-top: 10px;">Total</td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                        <asp:Label ID="lblalltotal" runat="server" Text=" "></asp:Label>
                                                    </td>

                                                </tr>

                                                <tr style="background-color: #ECECEC !important;">
                                                    <td style="font-weight: bold; padding-right: 17%; padding-top: 10px;">Gross</td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                        <asp:Label ID="lblGross" runat="server" Text=" "></asp:Label>
                                                    </td>

                                                </tr>
                                                <tr style="background: #f9f9f9 !important;">
                                                    <td style="font-weight: bold; padding-right: 4%; padding-top: 10px;">Advance Amount</td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                        <asp:Label ID="lblCurrency" runat="server" Text=" "></asp:Label><asp:Label ID="txtPaidAmt" runat="server"></asp:Label>
                                                        <asp:Label ID="lbl25" runat="server" Text=" "></asp:Label></td>

                                                </tr>
                                                <tr style="background-color: #ECECEC !important;" id="trbalanceamount" runat="server">
                                                    <td style="font-weight: bold; padding-right: 4%; padding-top: 10px;">Balance Amount</td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                        <asp:Label ID="lblBalanceAmt" runat="server" Text=" "></asp:Label>
                                                    </td>

                                                </tr>
                                                <tr style="background: #f9f9f9 !important;" id="trbalancedate" runat="server">
                                                    <td style="font-weight: bold; padding-right: 8%; padding-top: 10px;">Balance Payment Date</td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                        <asp:Label ID="lblBalancedate" runat="server" Text=" "></asp:Label>
                                                    </td>

                                                </tr>
                                            </table>

                                            <br />
                                           
                                        </div>
                                        <br />

                                        <div id="pnlBookButton" style="text-align: center;" width="70%" runat="server" class="text-center">
                                <asp:Button ID="btnSubmit"  runat="server" OnClientClick="openWin()"  Text="Proceed" CssClass="btn btn-info btnWidth100 btnFont" Font-Size="Medium"   />
                            
                                            
                                          

                                          
                              
                            </div>
                                    </asp:Panel>
                                </asp:Panel>
            
    </form>
</body>
</html>
 <script type ="text/javascript">

        function openWin()
        {
        
            window.open('<%= Session["agnturi"] %>');           
        }  

   

    </script>