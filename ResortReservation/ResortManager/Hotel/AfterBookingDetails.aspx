<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Hotel/AfterBookingDetails.aspx.cs" Inherits="Hotel_AfterBookingDetails" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    <link href="../../css/newcss.css" rel="stylesheet" />

    <script type="text/javascript" src="../../js/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="../../js/bootstrap.min.js"></script>

    <link rel="stylesheet" type="text/css" media="all" href="../css/newcss.css" />


    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../css/newcss.css" />

    <style type="text/css">
        .CSSTableGenerator {
            margin: 0px;
            padding: 0px;
            width: 100%;
            box-shadow: 10px 10px 5px #888888;
            border: 1px solid #000000;
            -moz-border-radius-bottomleft: 0px;
            -webkit-border-bottom-left-radius: 0px;
            border-bottom-left-radius: 0px;
            -moz-border-radius-bottomright: 0px;
            -webkit-border-bottom-right-radius: 0px;
            border-bottom-right-radius: 0px;
            -moz-border-radius-topright: 0px;
            -webkit-border-top-right-radius: 0px;
            border-top-right-radius: 0px;
            -moz-border-radius-topleft: 0px;
            -webkit-border-top-left-radius: 0px;
            border-top-left-radius: 0px;
        }
            /*th
        {
            text-align:center;
        }*/

            .CSSTableGenerator table {
                border-collapse: collapse;
                border-spacing: 0;
                width: 100%;
                height: 100%;
                margin: 0px;
                padding: 0px;
            }

            .CSSTableGenerator tr:last-child td:last-child {
                -moz-border-radius-bottomright: 0px;
                -webkit-border-bottom-right-radius: 0px;
                border-bottom-right-radius: 0px;
            }

            .CSSTableGenerator table tr:first-child td:first-child {
                -moz-border-radius-topleft: 0px;
                -webkit-border-top-left-radius: 0px;
                border-top-left-radius: 0px;
            }

            .CSSTableGenerator table tr:first-child td:last-child {
                -moz-border-radius-topright: 0px;
                -webkit-border-top-right-radius: 0px;
                border-top-right-radius: 0px;
            }

            .CSSTableGenerator tr:last-child td:first-child {
                -moz-border-radius-bottomleft: 0px;
                -webkit-border-bottom-left-radius: 0px;
                border-bottom-left-radius: 0px;
            }

            .CSSTableGenerator tr:hover td {
                background-color: #ffffff;
            }

            .CSSTableGenerator td {
                /*vertical-align: middle;
                background: -o-linear-gradient(bottom, #ffaa56 5%, #ffffff 100%);
                background: -webkit-gradient( linear, left top, left bottom, color-stop(0.05, #a9cae3), color-stop(1, #ffffff) );
                background: -moz-linear-gradient( center top, #ffaa56 5%, #ffffff 100% );
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr="#ffaa56", endColorstr="#ffffff");
                background: -o-linear-gradient(top,#ffaa56,ffffff);
                background-color: #337ab7;
                border: 1px solid #000000;
                border-width: 0px 1px 1px 0px;*/
                text-align: left;
                padding: 7px;
                font-size: 12px;
                font-family: Arial;
                font-weight: normal;
                color: #000000;
                border-color: #000 !important;
                border: 1px solid #000;
            }

            .CSSTableGenerator tr:last-child td {
                border-width: 0px 1px 0px 0px;
            }

            .CSSTableGenerator tr td:last-child {
                border-width: 0px 0px 1px 0px;
            }

            .CSSTableGenerator tr:last-child td:last-child {
                border-width: 0px 0px 0px 0px;
            }

            .CSSTableGenerator tr:first-child td {
                /*background: -o-linear-gradient(bottom, #ff7f00 5%, #bf5f00 100%);
                background: -webkit-gradient( linear, left top, left bottom, color-stop(0.05, #a9cae3), color-stop(1, #a9cae3) );
                background: -moz-linear-gradient( center top, #ff7f00 5%, #bf5f00 100% );
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr="#ff7f00", endColorstr="#bf5f00");
                background: -o-linear-gradient(top,#ff7f00,bf5f00);
                background-color: #ff7f00;
                border: 0px solid #000000;
                text-align: center;
                border-width: 0px 0px 1px 1px;*/
                font-size: 16px;
                font-family: Arial;
                font-weight: bold;
                color: #ffffff;
            }

            .CSSTableGenerator tr:first-child:hover td {
                background: -o-linear-gradient(bottom, #ff7f00 5%, #bf5f00 100%);
                background: -webkit-gradient( linear, left top, left bottom, color-stop(0.05, #a9cae3), color-stop(1, #a9cae3) );
                background: -moz-linear-gradient( center top, #ff7f00 5%, #bf5f00 100% );
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr="#ff7f00", endColorstr="#bf5f00");
                background: -o-linear-gradient(top,#ff7f00,bf5f00);
                background-color: #ff7f00;
            }

            .CSSTableGenerator tr:first-child td:first-child {
                border-width: 0px 0px 1px 0px;
            }

            .CSSTableGenerator tr:first-child td:last-child {
                border-width: 0px 0px 1px 1px;
            }

        .button-link {
            padding: 10px 15px;
            background: #4479BA;
            color: #FFF;
            font-size: medium;
        }

        .auto-style1 {
            width: 52px;
        }

        .auto-style2 {
            width: 258px;
        }

        .auto-style3 {
            height: 56px;
        }



        h2 {
            font-size: 25px;
            text-align: center;
            line-height: 44px;
            margin-bottom: 30px;
        }

        #pnlRooms table {
            margin: 0 auto;
            float: none;
        }

        input submit {
            background-color: #2492E2 !important;
            border: solid 1px #2492E2 !important;
            display: inline-block;
            padding: 6px 12px;
            margin-bottom: 0;
            font-size: 14px;
            font-weight: 400;
            line-height: 1.42857143;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -ms-touch-action: manipulation;
            touch-action: manipulation;
            cursor: pointer;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            background-image: none;
            border: 1px solid transparent;
            border-radius: 4px;
        }
    </style>
</head>
<body class="bg-img-H">
    <form id="form1" runat="server">
        <div class="RoomRatesBox" style="margin-bottom: 25px;">
            <div class="col-md-12 White-Box text-center">
                <h2>Booking Details<samp class=" pull-right">
                    <asp:LinkButton ID="lnkLogout" runat="server" CssClass="button-link" OnClick="lnkLogout_Click">Logout</asp:LinkButton></samp></h2>
                <div class=" clearfix"></div>
            </div>
        </div>
        <div class=" clearfix"></div>

        <div class="RoomRatesBox2 White-Box2 m2" style="padding: 20px;">



            <div>


                <%--  <table style="border: thin solid #000000;width:30%">
                   <tr style="background-color: #5BC0DE">
                       <td>Check in </td>
                       <td class="auto-style1"><asp:Label ID="lblChkin" runat="server" Text="Label"></asp:Label></td>

                   </tr>
                   <tr style="background-color:white">
                        <td>Check out </td>
                       <td class="auto-style1"><asp:Label ID="lblChkout" runat="server" Text="Label"></asp:Label></td>

                   </tr>
                   </table>--%>
            </div>

            <asp:UpdatePanel ID="upd1" runat="server">
                <ContentTemplate>
                    <h2 style="font-family: 'Times New Roman'; font-style: italic; font-weight: bold; font-size: 17px;">Selected Rooms</h2>
                    <asp:Panel ID="pnlRooms" runat="server">
                    </asp:Panel>
                    <div style="display: none">


                        <h2>Selected Rooms</h2>
                        <asp:GridView ID="gdvSelectedRooms" runat="server" Width="50%" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" Font-Size="Medium">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>

                                <asp:TemplateField>
                                    <ItemTemplate>

                                        <asp:HiddenField ID="hdnRmno" runat="server" Value='<%#Eval("RoomCategoryId") %>' />
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:BoundField DataField="categoryName" HeaderText="categoryName" />
                                <asp:BoundField DataField="Pax" HeaderText="Pax" />
                                <asp:BoundField DataField="Price" HeaderText="Price" />

                                <asp:BoundField DataField="Nights" HeaderText="Nights" />
                                <asp:BoundField DataField="Total" HeaderText="Total" />
                            </Columns>
                            <EditRowStyle BackColor="#7C6F57" />
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5BC0DE" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#E3EAEB" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F8FAFA" />
                            <SortedAscendingHeaderStyle BackColor="#246B61" />
                            <SortedDescendingCellStyle BackColor="#D4DFE1" />
                            <SortedDescendingHeaderStyle BackColor="#15524A" />
                        </asp:GridView>



                    </div>
                    <div class=" clear"></div>
                    <div id="DivContinue" class=" btn-summerised">
                        <asp:HiddenField ID="hdnfTotalPaybleAmt" runat="server" />
                        <asp:Button runat="server" CssClass="btn btn-info btnWidth100 btnFont" ID="btnBack" Text="Back" OnClick="btnBack_Click" />
                        <asp:Button CssClass="btn btn-info btnWidth100 btnFont" runat="server" ID="btnSmbt" Text="Proceed" OnClick="btnSmbt_Click" />
                    </div>
                    <asp:Panel ID="pnlLogin" runat="server">
                        <table style="font-family: Calibri;">
                            <tr>
                                <td>your Email Id </td>
                                <td class="auto-style2">
                                    <asp:TextBox ID="txtmailied" Width="250px" Height="27px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Enter Your Password </td>
                                <td class="auto-style2">
                                    <asp:TextBox ID="txtPasswoprd" Width="250px" Height="27px" runat="server" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td></td>
                                <td class="auto-style2" style="text-align: left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSbmt" runat="server" Height="26px" OnClick="btnSbmt_Click" Text="Login" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>

                    <asp:Panel ID="customerLogin" runat="server" Style="margin-top: 30px;">
                        <center>
                 <table id="tableVerify" runat="server" visible="false">

                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtCode" placeholder="Enter Code" runat="server" ></asp:TextBox>
                                             <asp:Button ID="btnVerify" runat="server" OnClick="btnVerify_Click" Text="Verify" />

                                        </td>
                                        <td>   </td>
                                    </tr>
                                </table>
                              
                            </center>
                        <asp:HiddenField ID="hfVCode" runat="server" />



                        <table id="TableCust" runat="server" style="margin: 30px auto; float: none;">

                            <tr>
                                <td>Enter your Email</td>
                                <td>
                                    <asp:TextBox ID="txtCustMailId" runat="server"></asp:TextBox></td>


                            </tr>

                            <tr>
                                <td>Enter password</td>
                                <td>
                                    <asp:TextBox ID="txtCustPass" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1ee" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtCustPass" ValidationGroup="CustLogin"></asp:RequiredFieldValidator>

                                </td>



                            </tr>


                            <tr>
                                <td>
                                    <div style="height: 30px;"></div>
                                </td>

                                <td>
                                    <div style="height: 30px;"></div>
                                </td>

                            </tr>
                            <tr style="margin-top: 30px;">
                                <td>
                                    <asp:Button ID="txtRegNow" CssClass="btn btn-info btnWidth100 btnFont" runat="server" Text="Register Now" OnClick="txtRegNow_Click" />
                                </td>
                                <td style="text-align: left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" CssClass="btn btn-info btnWidth100 btnFont" ID="btnCustLogin" ValidationGroup="CustLogin" Text="Login" OnClick="btnCustLogin_Click" />
                                </td>
                            </tr>

                        </table>

                    </asp:Panel>

                    <asp:Panel ID="pnlCustReg" runat="server">
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <section>

                            <div class="row">

                                <div class="col-md-12 text-center topbotPadding">

                                    <h2 class="goldLine"><span>NEW USER REGISTER</span></h2>

                                </div>

                                <div class="col-md-12 text-left noTopPadding">

                                    <div class="insideSkin bottomPadding30">

                                        <p></p>

                                    </div>

                                </div>

                            </div>

                        </section>



                        <div id="formTop" class="row noSideMargin backgroundPaperDark innerDropShadow topbotPadding">

                            <div class="col-xs-12">
                            </div>
                            <table style="margin: 0 auto; float: none; width: 51%;">
                                <tr>
                                    <td>
                                        <label for="ddlTitle" class="control-label">Title</label></td>
                                    <td>
                                        <asp:DropDownList ID="ddltitle" runat="server" class="form-control" Style="margin-bottom: 15px">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Mr</asp:ListItem>
                                            <asp:ListItem>Mrs</asp:ListItem>
                                            <asp:ListItem>Miss</asp:ListItem>
                                            <asp:ListItem>Ms</asp:ListItem>

                                        </asp:DropDownList></td>

                                </tr>

                                <tr>
                                    <td>
                                        <label for="txtName" class="control-label">Firstname</label></td>
                                    <td>
                                        <asp:TextBox ID="txtFirstName" runat="server" class="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtFirstName" ValidationGroup="Cust"></asp:RequiredFieldValidator>

                                    </td>

                                </tr>

                                <tr>
                                    <td>
                                        <label for="txtName" class="control-label">Lastname</label></td>
                                    <td>
                                        <asp:TextBox ID="txtLastName" runat="server" class="form-control"></asp:TextBox>


                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtLastName" ValidationGroup="Cust"></asp:RequiredFieldValidator>

                                    </td>


                                </tr>

                                <tr>
                                    <td>
                                        <label for="txtEmailAddress" class="control-label">Email Address</label></td>
                                    <td>
                                        <asp:TextBox ID="txtMailAddress" runat="server" class="form-control" OnTextChanged="txtMailAddress_TextChanged" AutoPostBack="True"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtMailAddress" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>

                                    </td>

                                </tr>

                                <tr>
                                    <td>
                                        <label for="txtEmailAddress" class="control-label">Telephone</label></td>
                                    <td>
                                        <asp:TextBox ID="txtTelephone" runat="server" class="form-control"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtTelephone" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid" ControlToValidate="txtTelephone" ValidationGroup="Cust" ValidationExpression="^([0-9]{8,15})$"></asp:RegularExpressionValidator>

                                    </td>

                                </tr>

                                <tr>
                                    <td>
                                        <label for="txtAddress1" class="control-label">Address 1</label></td>
                                    <td>
                                        <asp:TextBox ID="txtAddress1" runat="server" class="form-control"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtAddress1" ValidationGroup="Cust"></asp:RequiredFieldValidator>

                                    </td>

                                </tr>

                                <tr>
                                    <td>
                                        <label for="txtAddress2" class="control-label">Address 2</label></td>
                                    <td>
                                        <asp:TextBox ID="txtaddress2" runat="server" class="form-control" Style="margin-bottom: 15px"></asp:TextBox></td>


                                </tr>

                                <tr>
                                    <td>
                                        <label for="txtCity" class="control-label">City</label></td>
                                    <td>

                                        <asp:TextBox ID="txtCity" runat="server" class="form-control"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtCity" ValidationGroup="Cust"></asp:RequiredFieldValidator>

                                    </td>


                                </tr>

                                <tr>
                                    <td>
                                        <label for="txtState" class="control-label">State</label></td>
                                    <td>
                                        <asp:TextBox ID="txtState" runat="server" class="form-control"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtState" ValidationGroup="Cust"></asp:RequiredFieldValidator></td>

                                </tr>

                                <tr>
                                    <td>
                                        <label for="txtPostcode" class="control-label">Postcode</label></td>
                                    <td>
                                        <asp:TextBox ID="txtPostcode" runat="server" class="form-control"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtPostcode" ValidationGroup="Cust"></asp:RequiredFieldValidator></td>

                                </tr>

                                <tr>
                                    <td>
                                        <label for="txtPassword" class="control-label">Password</label></td>
                                    <td>
                                        <asp:TextBox ID="txtPassWord" runat="server" class="form-control" TextMode="Password"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtPassWord" ValidationGroup="Cust"></asp:RequiredFieldValidator>

                                    </td>


                                </tr>

                                <tr>
                                    <td>
                                        <label for="ddlCountry" class="control-label">Country</label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control"></asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlCountry" InitialValue="0" ValidationGroup="Cust"></asp:RequiredFieldValidator>


                                    </td>


                                </tr>

                                <%--    <tr>
                            <td>   <label for="txtPaymentMethod" class="control-label">Payment Method</label></td>
                            <td>
                                     <asp:TextBox ID="txtPaymentMethod" runat="server" class="form-control"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtPaymentMethod" ValidationGroup="Cust"></asp:RequiredFieldValidator>


                            </td>


                        </tr>--%>

                                <tr style="text-align: center">

                                    <td colspan="2">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info font16" OnClick="btnSubmit_Click" ValidationGroup="Cust" />

                                        <asp:Button ID="btnCloseCust" runat="server" CssClass="btn btn-info font16" OnClick="btnCloseCust_Click" Text="Close" />

                                    </td>

                                </tr>

                            </table>


                            <div class="puller"></div>

                        </div>





                    </asp:Panel>
                    <div style="width: 70%; margin: 0 auto; float: none;">
                        <asp:Panel ID="pnlFullDetails" class="CSSTableGenerator" Width="100%" runat="server">
                            <div id="BookRef" runat="server">
                                <table style="border: 1px solid; background: -webkit-gradient( linear, left top, left bottom, color-stop(0.05, #a9cae3), color-stop(1, #a9cae3) );">
                                    <tr>
                                        <td class="auto-style5">Enter Booking Reference Name.</td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtBookRef" Style="color: black" runat="server" Width="281px"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="ReqBookRef" runat="server" ControlToValidate="txtBookRef" ErrorMessage="*" ValidationGroup="Pay"></asp:RequiredFieldValidator></td>

                                    </tr>

                                </table>








                            </div>

                            <h2 style="font-family: 'Times New Roman'; font-style: italic; font-weight: bold; font-size: 17px;">Payment Details</h2>





                            <table style="border: thin solid #000000; width: 80%; margin: 0 auto;">
                                <tr style="background-color: rgba(149, 190, 222, 0.8) !important">
                                    <td style="border: thin solid #000000; font-weight: bold; width: 22%;">Invoice To</td>
                                    <td style="border: thin solid #000000">
                                        <asp:Label ID="lblAgentName" runat="server"></asp:Label></td>

                                </tr>
                                <tr style="background-color: #EFF3FB;">
                                    <td style="border: thin solid #000000; font-weight: bold">
                                        <asp:Label ID="lblBilling" runat="server" Text="Billing Address : "></asp:Label></td>
                                    <td style="border: thin solid #000000">
                                        <asp:Label ID="lblBillingAddress" runat="server" Text=""></asp:Label></td>

                                </tr>
                                <tr style="background-color: rgba(149, 190, 222, 0.8) !important">
                                    <td style="border: thin solid #000000; font-weight: bold">
                                        <asp:Label ID="lbPayment" runat="server" Text="Payment Method : "></asp:Label></td>
                                    <td style="border: thin solid #000000">
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
                            <br />


                            <br />
                            <div style="height: 40px"></div>
                            <asp:Panel ID="panelwithoutCreditAgent" Width="100%" runat="server" Font-Size="Medium">
                                <div>
                                    <table style="border: thin solid #000000; width: 80%; margin: 0 auto;">
                                        <tr style="background-color: rgba(149, 190, 222, 0.8) !important">
                                            <td style="border: thin solid #000000; font-weight: bold; width: 22%;" class="auto-style3">Total amount</td>
                                            <td style="border: thin solid #000000" class="auto-style4">
                                                <asp:Label ID="lbltotAmt" runat="server" Text="Label"></asp:Label></td>

                                        </tr>
                                        <tr style="background-color: #EFF3FB;">
                                            <td style="border: thin solid #000000; font-weight: bold">Booking Amount</td>
                                            <td style="border: thin solid #000000">
                                                <asp:Label ID="lblCurrency" runat="server" Text="Label"></asp:Label>
                                                <asp:Label ID="txtPaidAmt" runat="server"></asp:Label>
                                                <%--(25% of total)--%>
                                            </td>

                                        </tr>
                                        <tr style="background-color: rgba(149, 190, 222, 0.8) !important">
                                            <td style="border: thin solid #000000; font-weight: bold">Balance Amount</td>
                                            <td style="border: thin solid #000000">
                                                <asp:Label ID="lblBalanceAmt" runat="server" Text="Label"></asp:Label>

                                                <%--(75% of total) to be paid prior to <%Response.Write(Convert.ToDateTime(System.DateTime.Now).AddDays(-90).ToString("dddd, MMMM d, yyyy")); %>--%>

                                            </td>

                                        </tr>

                                    </table>
                                    <br />
                                    <div style="float: left"></div>


                                </div>


                                <br />



                            </asp:Panel>


                        </asp:Panel>
                        <div style="height: 40px"></div>



                        <%--    <asp:Panel ID="fhtfr"  Width="70%" runat="server">
            <div>
                  <p> Amount</p>
                     <p><asp:TextBox ID="txtPaidAmt" runat="server"></asp:TextBox>
                         <asp:Label ID="lblCurrency" runat="server" Text="Label"></asp:Label>
                     </p> 

             </div>

                 
                    <br />
                


    </asp:Panel>--%>
                        <div style="height: 40px"></div>
                        <center>
     <asp:Panel ID="pnlBookButton"  Width="70%" runat="server">


         <asp:Button ID="btnPayProceed" runat="server" Text="Proceed For Payment" CssClass="btn btn-info btnWidth100 btnFont" OnClick="btnPayProceed_Click" ValidationGroup="Pay" Font-Size="Medium" />
             <asp:Label ID="lblPaymentErr" runat="server" ForeColor="#FF3300"></asp:Label>
             <asp:HiddenField ID="hftxtpaidamt" runat="server" />
             <br />
         </asp:Panel>

    </center>




                    </div>
                    </div>

                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                    <asp:PostBackTrigger ControlID="btnCloseCust" />

                </Triggers>
            </asp:UpdatePanel>
    </form>
</body>
</html>
