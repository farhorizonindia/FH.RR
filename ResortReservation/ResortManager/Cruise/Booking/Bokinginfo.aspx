<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bokinginfo.aspx.cs" Inherits="Cruise_Booking_Bokinginfo" %>

<!DOCTYPE html>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        lblpckgname.Text = Request.QueryString["PackageName"].ToString();
         lblchkin.Text = Request.QueryString["CheckInDate"].ToString();
         lblchkout.Text = Request.QueryString["CheckOutdate"].ToString();
         lblAgentName.Text = Request.QueryString["InvoiceTo"].ToString();
         lbltotAmt.Text = Request.QueryString["TotalAmt"].ToString();
         Label1.Text = Request.QueryString["GST"].ToString();
         lbltaxin.Text = Request.QueryString["TaxableAmt"].ToString();
           lblGross.Text = Request.QueryString["Gross"].ToString();
         txtPaidAmt.Text = Request.QueryString["AdvanceAmt"].ToString();
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Panel ID="pnlFullDetails" class="CSSTableGenerator" Width="100%" runat="server" Style="box-shadow: 3px 3px 9px 2px rgba(0,0,0,0.4); padding-top: 1%; border: 1px solid rgba(0,0,0,0.5) !important; border-radius: 4px; padding-bottom: 35px;">
                                    
                                    <h4 style="font-size: 17px; padding-top: 14px; padding-bottom: 14px; text-align: center;font-family: 'Montserrat', sans-serif;">Payment Details</h4>

                                    <table class="table table-bordered" id="tbl-full-detail" style="width: 90%; margin: 0 auto; border: 1px solid #ddd;">
                                      
                                        
                                         <tr style="background-color: #ECECEC !important;">
                                            <td style="font-weight: bold; width: 27%; padding-right: 12%; padding-top: 10px;">Package Name</td>
                                            <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                <asp:Label ID="lblpckgname" Text=" " runat="server"></asp:Label></td>
                                        </tr>
                                         <tr style="background-color: #ECECEC !important;">
                                            <td style="font-weight: bold; width: 27%; padding-right: 12%; padding-top: 10px;">CheckIn Date</td>
                                            <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                <asp:Label ID="lblchkin" Text=" " runat="server"></asp:Label></td>
                                        </tr>
                                         <tr style="background-color: #ECECEC !important;">
                                            <td style="font-weight: bold; width: 27%; padding-right: 12%; padding-top: 10px;">Check Out Date</td>
                                            <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                <asp:Label ID="lblchkout" Text=" " runat="server"></asp:Label></td>
                                        </tr>
                                        
                                        
                                          <tr style="background-color: #ECECEC !important;">
                                            <td style="font-weight: bold; width: 27%; padding-right: 12%; padding-top: 10px;">Invoice To</td>
                                            <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                <asp:Label ID="lblAgentName" Text=" " runat="server"></asp:Label></td>
                                        </tr>
                                       
                                     
                                        

                                   
                                    </table>



                                    <asp:Panel ID="panelwithoutCreditAgent" Style="padding-top: 15px" Width="100%" runat="server" Font-Size="Medium">
                                        <div>

                                            <table class="table table-bordered" style="width: 90%; margin: 0 auto; border: 1px solid #ddd;">
                                                <tr style="background-color: #ECECEC !important;">
                                                    <td style="font-weight: bold; width: 27%; padding-right: 8%; padding-top: 10px;" class="auto-style3">Total amount</td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;" class="auto-style4">
                                                        <asp:Label ID="lbltotAmt" runat="server" Text=" "></asp:Label></td>

                                                </tr>
                                             
                                                   <tr style="background-color: #ECECEC !important;">
                                                    <td style="font-weight: bold; padding-right: 17%; padding-top: 10px;">GST</td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                        <asp:Label ID="Label1" runat="server" Text=" "></asp:Label>
                                                    </td>

                                                </tr>

                                                <tr style="background: #ECECEC !important;">
                                                    <td style="font-weight: bold; padding-right: 17%; padding-top: 10px;">Taxable Amount</td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                        <asp:Label ID="lbltaxin" runat="server" Text=" "></asp:Label>
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
                                                        <asp:Label ID="lblCurrency" runat="server" Text=" "></asp:Label><asp:Label ID="txtPaidAmt" Text=" " runat="server"></asp:Label>
                                                        <asp:Label ID="lbl25" runat="server" ></asp:Label></td>

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
    </div>
    </form>
</body>
</html>
