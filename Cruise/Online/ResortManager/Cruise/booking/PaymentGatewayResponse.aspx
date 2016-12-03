<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentGatewayResponse.aspx.cs" Inherits="response" EnableEventValidation="false" %>




<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

    protected void Button1_Click(object sender, EventArgs e)
    {

    }
   
        
    
</script>
<script type="text/javascript" src="http://jqueryjs.googlecode.com/files/jquery-1.3.1.min.js"> </script>
<script type="text/javascript">


    function MyFunction() {

        document.getElementById("divDisplay").style.display = "none";
        window.print();
        document.getElementById("divDisplay").style.display = "block";
    }



</script>


<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate" />
    <meta http-equiv="Cache-Control" content="post-check=0, pre-check=0', false" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="Sat, 26 Jul 1997 05:00:00 GMT" />
    <meta http-equiv="Last-Modified" content='" + now1( D, d M Y H:i:s ) + "GMT' />

    <script type="text/javascript" src="resources/js/jquery.js"></script>


    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>:: Invoice ::</title>
    <style>
        html, body
        {
            margin: 0;
            height: 100%;
        }


        .rightalign
        {
            text-align: center;
        }

        .ralign
        {
            text-align: right;
        }

        .clear
        {
            clear: both;
        }

        .th
        {
            background-color: #d9d9d9;
        }

        .Invoice_Wrp
        {
            width: 1000px;
            margin: 40px auto;
            float: none;
            font-size: large;
        }

        .Invoice_Wrp_header
        {
            margin-bottom: 20px;
        }

        .PROFORMA_div
        {
            margin-bottom: 20px;
        }

        .PROFORMA_text
        {
            text-align: center;
            margin-bottom: 20px;
        }

        .Invoice_header_L
        {
            width: 25%;
            float: left;
        }

        .Invoice_header_R
        {
            width: 50%;
            float: left;
        }

        .Invoice_Wrp_body
        {
        }

        .Invoice_Package
        {
            margin-bottom: 20px;
        }

        .Invoice_Package_L
        {
            width: 50%;
            float: left;
        }

        .Invoice_Package_R
        {
            width: 50%;
            float: left;
        }


        .Payment_details
        {
            margin-bottom: 20px;
            margin-top: 20px;
        }

        .PROFORMA_L
        {
            width: 50%;
            float: left;
        }

        .PROFORMA_R
        {
            width: 50%;
            float: left;
        }

        .Invoice_Package_L
        {
        }

        .Invoice_Package_R
        {
        }

        .Payment_details_L
        {
            width: 50%;
            float: left;
        }

        .Payment_details_R
        {
            width: 50%;
            float: left;
        }

        .Invoice_Wrp_footer
        {
        }

        .Wrp_footer_text
        {
            margin-bottom: 20px;
        }

        .Invoice_Wrp_Tbl
        {
            margin-bottom: 20px;
        }

        .auto-style3
        {
            height: 24px;
            width: 352px;
        }

        .auto-style4
        {
            width: 352px;
        }

        .auto-style5
        {
            height: 23px;
            width: 352px;
        }

        .auto-style7
        {
            width: 242px;
        }
        .auto-style8
        {
            width: 244px;
        }
    </style>
</head>
<body>
    <span id="DivInvoice" runat="server">

        <form id="form1" runat="server">
            <% 
                // This is landing page where you will receive response from airpay. 
                // The name of the page should be as per you have configured in airpay system
                // All columns are mandatory    


                // Generating Secure Hash
                // $mercid = 	Merchant Id, $username = username
                // You will find above two keys on the settings page, which we have defined here in config.php
                try
                {
                    string username = ConfigurationManager.AppSettings.Get("username").ToString();
                    string password = ConfigurationManager.AppSettings.Get("password").ToString();
                    string secretKey = ConfigurationManager.AppSettings.Get("secret").ToString();
                    string MID = ConfigurationManager.AppSettings.Get("mercid").ToString();
                    string error = "";
                    string TRANSACTIONSTATUS = Request.Params.Get("TRANSACTIONSTATUS").Trim();
                    string APTRANSACTIONID = Request.Params.Get("APTRANSACTIONID").Trim();
                    string MESSAGE = Request.Params.Get("MESSAGE").Trim();
                    string TRANSACTIONID = Request.Params.Get("TRANSACTIONID").Trim();
                    string AMOUNT = Request.Params.Get("AMOUNT").Trim();
                    string ap_SecureHash = Request.Params.Get("ap_SecureHash").Trim();
                    if (TRANSACTIONSTATUS == "" || APTRANSACTIONID == "" || TRANSACTIONID == "" || AMOUNT == "" || ap_SecureHash == "")
                    {
                        if (TRANSACTIONID == "") { error = "TRANSACTIONID"; }
                        if (APTRANSACTIONID == "") { error = "APTRANSACTIONID"; }
                        if (AMOUNT == "") { error = "AMOUNT"; }
                        if (TRANSACTIONSTATUS == "") { error = "TRANSACTIONSTATUS"; }
                        if (ap_SecureHash == "") { error = "ap_SecureHash"; }
                    }
                    //comparing Secure Hash with Hash sent by Airpay
                    string sTemp = TRANSACTIONID + ":" + APTRANSACTIONID + ":" + AMOUNT + ":" + TRANSACTIONSTATUS + ":" + MESSAGE + ":" + MID + ":" + username;
                    string strCRC = CRCCode(sTemp, ap_SecureHash, TRANSACTIONSTATUS, APTRANSACTIONID, MESSAGE, TRANSACTIONID, AMOUNT);
                    if (error == "")
                    {
                        if (TRANSACTIONSTATUS == "200")
                        {
                            // Literal1.Text = "<table width='100%'><tr width='100%'><td align='left' width='50%'>Transaction Id</td><td align='left' width='50%' style='color:black;'>" + TRANSACTIONID + "</td></tr><tr width='100%'><td align='left' width='50%'>Airpay Transaction Id</td><td align='left' width='50%' style='color:black;'>" + APTRANSACTIONID + "</td></tr><tr width='100%'><td align='left' width='50%'>Amount</td><td align='left' width='50%' style='color:black;'>" + AMOUNT + "</td></tr><tr width='100%'><td align='left' width='50%'>Transaction Status Code</td><td align='left' width='50%' style='color:black;'>" + TRANSACTIONSTATUS + "</td></tr><tr width='100%'><td align='left' width='50%'>Message</td><td align='left' width='50%' style='color:black;'>" + MESSAGE + "</td></tr><tr width='100%'><td align='left' width='50%'>Status</td><td align='left' width='50%' style='color:green;'>Success</td></tr></table>";

                        }
                        else
                        {
                            //  Literal1.Text = "<table width='100%'><tr width='100%'><td align='left' width='50%'>Transaction Id</td><td align='left' width='50%' style='color:black;'>" + TRANSACTIONID + "</td></tr><tr width='100%'><td align='left' width='50%'>Airpay Transaction Id</td><td align='left' width='50%' style='color:black;'>" + APTRANSACTIONID + "</td></tr><tr width='100%'><td align='left' width='50%'>Amount</td><td align='left' width='50%' style='color:black;'>" + AMOUNT + "</td></tr><tr width='100%'><td align='left' width='50%'>Transaction Status Code</td><td align='left' width='50%' style='color:black;'>" + TRANSACTIONSTATUS + "</td></tr><tr width='100%'><td align='left' width='50%'>Message</td><td align='left' width='50%' style='color:black;'>" + MESSAGE + "</td></tr><tr width='100%'><td align='left' width='50%'>Status</td><td align='left' width='50%' style='color:green;'>Failed</td></tr></table>";
                        }
                    }
                    else
                    {
                        //  Literal1.Text = "<table width='100%'><tr><td align='center'>Variable(s) " + error + " is/are empty.</td></tr></table>";
                    }
                }
                catch
                {
                    //  Response.Write("Airpay response is unreadable.");
                    // Response.Write("<br/>Booking Added");
                }
            %>

            <center>
                <div style="width: 450px; margin: 0px auto;">
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </div>
            </center>


            <%--   <div>
            <table>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td></td>

                </tr>
                <tr>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Arrival Date</td>
                    <td>
                        &nbsp;</td>

                </tr>

                <tr>
                    <td>Departure Date</td>
                    <td>
                        &nbsp;</td>


                </tr>

            </table>
            <div style="width: 30px"></div>


           

            <hr />
            <table>
                <tr>
                    <td>Total Amount</td>
                    <td></td>
                    <td>
                        &nbsp;</td>

                </tr>
            </table>
            <hr />
            <table>
                <tr>
                    <td>Total Balance</td>
                    <td></td>
                    <td>
                        &nbsp;</td>

                </tr>
            </table>


        </div>
        <p>
            Valid With Computer Print Only
        </p>
        <p>
            Thanks For Choosing&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblAccomName" runat="server" Text="Mv Mahabaahu India"></asp:Label>
        </p>
            --%>


            <div class="Invoice_Wrp" id="mydiv">
                <div class="Invoice_Wrp_header">
                    <div class="Invoice_header_L">
                        <div>Registered Office</div>
                        <div>B-209</div>
                        <div>C.R. Park</div>
                        <div>New Delhi-110019</div>
                        <div>PAN: AAACF6111G</div>
                        <div>Service Tax No. AAACF6111GSD002</div>


                    </div>
                    <div class="Invoice_header_R">

                        <img style="margin-left: 31%;" src="http://adventureresortscruises.in/Cruise/booking/img_logo.png" alt="" />


                    </div>
                    <div class="clear"></div>

                </div>
                <!--Invoice_Wrp_header-->
                <div class="Invoice_Wrp_body">
                    <div class="PROFORMA_div">
                        <div class="PROFORMA_text"><strong>PROFORMA INVOICE</strong></div>
                        <div class="clear"></div>
                        <div class="PROFORMA_L">
                            <div style="width: 40%; word-wrap: break-word;">
                                To
                        <br />
                                <asp:Label ID="lblBuyerName" runat="server"></asp:Label>

                                <br />
                                <asp:Label ID="lblBuyerAddress" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="PROFORMA_R">
                            <table style="width: 100%">
                                <tr>
                                    <td style="text-align: right">Dated: </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="dated" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">Invoice No: </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="lbInvoiceNO" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">No of Pax: </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="lbPax" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">Booking No: </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="lbBookingNo" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">Booking Date: </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="lbBookinDate" runat="server"></asp:Label></td>
                                </tr>
                            </table>




                        </div>

                        <div class="clear"></div>

                    </div>
                    <!--PROFORMA_div-->



                    <div class="Invoice_Package">
                        <strong><%--Package: 7 nights 8 days Upstream Cruise, 7 nights--%>
                            <asp:Label ID="lbpackageName" runat="server" Font-Underline="True"></asp:Label></strong>
                        <div class="clear"></div>

                        <div class="Invoice_Package_L">
                            <div>
                                <asp:Label ID="lblVessel" runat="server"></asp:Label><%-- MV Mahabaahu--%>
                                <asp:Label ID="lblacm" runat="server"></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="lbStrtEnd" runat="server" Text=""></asp:Label></div>

                        </div>
                        <div class="Invoice_Package_R">


                            <div>
                                <div>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="text-align: right;width: 209px;" >Start Date: </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lblArrvDate" runat="server"></asp:Label></td>
                                        </tr>

                                    </table>



                                </div>
                                <div>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="text-align: right;width: 209px;"  >End Date: </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lblDepartDate" runat="server"></asp:Label></td>
                                        </tr>

                                    </table>



                                </div>

                            </div>
                            <!--Invoice_Package_R-->

                            <div class="clear"></div>
                        </div>
                    </div>
                    <!--Invoice_Package-->

                    <div class="Invoice_Wrp_Tbl" style="display: table-cell; padding-top: 30px; padding-bottom: 30px; width: 900px;">
                        <%--<table width="100%" border="1" cellspacing="0" cellpadding="5">
                        <tr>
                            <th><strong>Particulars</strong></th>
                            <th><strong>Pax</strong></th>
                            <th><strong>Rate in INR</strong></th>
                            <th><strong>Total in INR </strong></th>
                        </tr>
                        <tr>
                            <td>
                                <div>Suite Cabin with Balcony (Double Bed), Cabin no 107 2 234 468 </div>
                                <div>Suite Cabin with Balcony (Double Bed), Cabin no 108</div>
                            </td>
                            <td>
                                <div>
                                    2
            <div>
                <div>
                    2
            <div>
                            </td>
                            <td>
                                <div>
                                    234
            <div>
                <div>
                    234
            <div>
                            </td>
                            <td>
                                <div>
                                    468
            <div>
                <div>
                    468
            <div>
                            </td>
                        </tr>
                        <tr>
                            <td>Sub Total</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>936 </td>
                        </tr>
                        <tr>
                            <td>Service Tax @ 4.50% </td>
                            <td>INR 43 </td>
                            <td>&nbsp;</td>
                            <td>43 </td>
                        </tr>
                        <tr>
                            <td colspan="3">Grand Total </td>
                            <td>979 </td>
                        </tr>
                    </table>--%>

                        <asp:GridView ID="gdvCruiseRooms" DataKeyNames="Bed Configuration,RoomNumber" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="True" OnRowDataBound="gdvCruiseRooms_RowDataBound">
                            <Columns>


                               
                                <asp:BoundField DataField="categoryName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Particulars" />
                                <asp:BoundField DataField="Pax" HeaderStyle-CssClass="rightalign" ItemStyle-CssClass="rightalign" HeaderText="Pax" />
                                <asp:BoundField DataField="Price" HeaderText="Rate in INR" HeaderStyle-CssClass="rightalign" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="166px" />
                                <asp:BoundField DataField="Price" HeaderText="Total in INR" HeaderStyle-CssClass="rightalign" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="118px" />


                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" CssClass="ralign" />
                            <HeaderStyle BackColor="#CCCCCC" />
                        </asp:GridView>

                        <asp:GridView ID="gdvSelectedRooms" runat="server" Width="100%" AutoGenerateColumns="false" ShowFooter="True" Font-Size="Medium">
                            <Columns>

                                <asp:BoundField DataField="categoryName" HeaderText="Particulars" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Pax" HeaderText="Pax" HeaderStyle-CssClass="rightalign" ItemStyle-CssClass="rightalign" />
                                <asp:BoundField DataField="Price" HeaderText="Rate in INR" HeaderStyle-CssClass="rightalign" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="166px" />
                                <asp:BoundField DataField="Total" HeaderText="Total in INR" HeaderStyle-CssClass="rightalign" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="118px" />


                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" CssClass="ralign" />
                            <HeaderStyle BackColor="#CCCCCC" />

                        </asp:GridView>

                    </div>

                    <!--Invoice_Wrp_Tbl-->

                    <strong>Amount in Words :</strong> <%--INR Nine Hundred and Seventy Nine Only--%>
                    <asp:Label ID="lbRuppeeinwords" runat="server" Text="......"></asp:Label>
                    <div class="Payment_details">



                        <%--     <div class="Payment_details_L">
                        <div></div>
            <div></div>
            <div></div>
            <div></div>
        </div>
        <!--Payment_details_L-->
        <div class="Payment_details_R">
         <div>

             
         </div>
            <div>
                </div>
            <div><strong>
                       </strong></div>
            <div></div>
        </div>--%>

                        <div><strong>&nbsp;Payment details:</strong></div>
                        <table style="width: 550px">
                            <tr style="display: none">
                                <td class="auto-style3">Total Amout(after adding VAT(4.50%)(in INR)</td>
                                <td class="ralign">
                                    <asp:Label ID="lblTotAMt" runat="server"></asp:Label></td>


                            </tr>
                            <tr>
                                <td class="auto-style4">Total Amout Paid (in INR)</td>
                                <td class="ralign">
                                    <asp:Label ID="lblTotPaid" runat="server"></asp:Label></td>


                            </tr>

                            <tr>
                                <td class="auto-style5">Total Due (In INR)</td>
                                <td class="ralign">
                                    <asp:Label ID="lblBalance" runat="server"></asp:Label></td>


                            </tr>

                            <tr>
                                <td class="auto-style4">Balance Due On</td>
                                <td class="ralign">
                                    <asp:Label ID="lbBalanceDueIn" runat="server"></asp:Label></td>


                            </tr>

                        </table>



                        <!--Payment_details_R-->
                        <div class="clear"></div>
                    </div>
                    <!--Payment_details-->


                    <!--Invoice_Wrp_body-->
                    <div class="Invoice_Wrp_footer">
                        <div class="Wrp_footer_text">For Adventure Resorts & Cruises Pvt Limited</div>
                        <div>This is a computer generated invoice and does not require a signature</div>
                    </div>
                    <!--Invoice_Wrp_footer-->
                </div>

                <asp:HiddenField ID="hfBookingId" runat="server" />
            </div>
            <%-- <input type="button" value="Print" onclick="PrintElem('#mydiv')" />--%>
            <div style="margin-top: 100px"  style="display:dwew;">


             <center> 
                 <div id="divDisplay" >
                  <asp:Button ID="btnBack" runat="server" Text="Home" OnClick="btnBack_Click" />
                <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="MyFunction()"  />
                     </div>
                 <%--  <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="MyFunction()"  />--%>


             </center> 

            </div>



        </form>
    </span>
</body>
</html>
