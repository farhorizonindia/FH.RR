<%@ Page Language="C#" AutoEventWireup="true" Inherits="Cruise_booking_SummarizedDetails" CodeFile="~/Cruise/booking/SummarizedDetails.aspx.cs" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html>
<script runat="server">
</script>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../../style.css" />
    <link href="../../css/newcss.css" rel="stylesheet" />

    <script type="text/javascript" src="../../js/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="../../js/bootstrap.min.js"></script>

    <style type="text/css">
        /*.button-link
        {
            padding: 10px 15px;
            background: #4479BA;
            color: #FFF;
            font-size: medium;
        }*/
        table.PerformanceTable {
            table-layout: fixed;
            width: 500px;
        }

            table.PerformanceTable td.PerformanceCell {
                width: 75px;
            }


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

        th {
            text-align: center;
        }

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
                border-width: 0px 0px 1px 1px;
            font-size: 16px;
            font-family: Arial;
            font-weight: bold;
            color: #ffffff;*/
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

        .auto-style1 {
            height: 30px;
        }

        .auto-style2 {
            height: 23px;
        }

        p {
            font-size: 20px;
            text-align: left;
        }



        ul {
            list-style-type: none;
            padding: 0px;
        }

        li {
            padding: 0px;
            margin: 0px;
        }

        li {
            font-size: 16px;
            font-family: 'Times New Roman';
            padding: 8px;
            margin: 0px;
        }

        h1 {
            font-size: 34px;
        }

        h3 {
            font-size: 17px;
        }

        h2 {
            font-size: 17px;
        }

        .auto-style3 {
            width: 41%;
            height: 19px;
        }

        .auto-style4 {
            height: 19px;
        }

        .auto-style5 {
            width: 194px;
        }
    </style>
</head>
<body class="bg-img1" style="font-family: 'Times New Roman'; font-style: italic">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <%--    <asp:UpdatePanel runat="server">
            <ContentTemplate>--%>

        <div class="sitecontainer whiteBackground1 ">
            <section>

                <div class="m2">

                    <div class="container">
                        <div class="row">
                            <div class="col-md-12 text-center White-Box">

                                <h2>Final Booking Details <span class=" pull-right">
                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button-link" OnClick="LinkButton1_Click">Logout</asp:LinkButton></span></h2>
                                <div class=" clearfix"></div>
                            </div>
                        </div>
                        <div class=" clearfix"></div>
                    </div>
                </div>
                <div class="container">
                    <div class="row">
                        <div class="col-md-12 White-Box">
                            <ul>
                                <li style="font-family: 'Times New Roman'; font-style: italic; font-weight: bold; font-size: 21px;">
                                    <asp:Label ID="lblPackName" runat="server" Text="Label">Package: <%Response.Write(Request.QueryString["PackName"]); %></asp:Label>
                                </li>
                                <li style="font-family: 'Times New Roman'; font-style: italic"><%Response.Write(Request.QueryString["NoOfNights"] + " Nights/" + (Convert.ToInt32(Request.QueryString["NoOfNights"]) + 1).ToString() + " Days"); %> </li>
                                <li style="font-family: 'Times New Roman'; font-style: italic">
                                    <asp:Label ID="lblChkin" runat="server" Text=""></asp:Label></li>
                                <li style="font-family: 'Times New Roman'; font-style: italic">
                                    <asp:Label ID="lblChkout" runat="server" Text=""></asp:Label></li>
                                <li style="font-family: 'Times New Roman'; font-style: italic">
                                    <asp:Label ID="lblDesc" runat="server">

                                    </asp:Label>

                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div>
                    <div class="booking-online-box White-Box2 padding2">
                        <div class="CSSTableGenerator1 booking-online2">
                            <table style="display: none">
                                <tr style="color: White; font-weight: bold;" class="GridHeader">
                                    <td>Package Name
                                    </td>
                                    <td>Nights
                                    </td>
                                    <td>Checkin Date
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%Response.Write(Request.QueryString["PackName"]); %>
                                    </td>
                                    <td>
                                        <%Response.Write(Request.QueryString["NoOfNights"]); %>NIGHTS 
                                    </td>
                                    <td>
                                        <%Response.Write(Convert.ToDateTime(Request.QueryString["CheckinDate"]).ToString("dddd, MMMM d, yyyy")); %>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <div class="booking-online2">
                            <div>
                                <center><h3 style="font-family:'Times New Roman';font-weight:bold">Cabin Details</h3></center>
                            </div>

                            <asp:GridView ID="GridRoomPaxDetail" AutoGenerateColumns="False" runat="server" Font-Size="12px" Font-Italic="false" ShowFooter="True">
                                <HeaderStyle ForeColor="Black" />

                                <Columns>
                                    <asp:TemplateField HeaderText="No">

                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="RoomId" Text='<%#Eval("RoomNumber") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category">

                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="RoomCategoryId" Visible="false" Text='<%#Eval("RoomCategoryId") %>'></asp:Label>
                                            <asp:Label runat="server" ID="lbcategoryName" Text='<%#Eval("categoryName") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bed Configuration">

                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="BedConfig" Text='<%#Eval("Bed Configuration") %>'></asp:Label>
                                        </ItemTemplate>


                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="No of Guests">

                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Pax" Text='<%#Eval("Pax") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField Visible="false">

                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblCurr" Text='<%#Eval("Currency") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Price">

                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Price" Text='<%#Eval("CRPrice") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Tax" Visible="false">

                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Tax" Text='<%#Eval("Tax") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Delete" Visible="false">
                                        <ItemTemplate>

                                            <asp:ImageButton Width="25px" ImageUrl="~/images/delete-icon.png" ID="imgbtnDelete" CommandName="Remove" runat="server" />
                                        </ItemTemplate>


                                    </asp:TemplateField>



                                </Columns>
                                <HeaderStyle CssClass="GridHeader" Font-Bold="True" HorizontalAlign="Center" />
                                <AlternatingRowStyle BackColor="White" />
                                <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
                                <FooterStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
                            </asp:GridView>

                            <asp:GridView ID="GridSummerizeRoomDetails" runat="server" CellPadding="4" AutoGenerateColumns="false" ForeColor="#333333" GridLines="Both" Font-Size="Medium" Visible="false">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#5bc0de" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5bc0de" Font-Bold="True" CssClass="GridHeader" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                <Columns>
                                    <asp:BoundField HeaderText="Cabin" DataField="categoryName" />
                                    <asp:BoundField HeaderText="Travellers" DataField="Pax" />
                                    <asp:BoundField HeaderText="" DataField="Currency" />
                                    <asp:BoundField HeaderText="Sub Total" DataField="Price" />
                                </Columns>
                            </asp:GridView>

                            <div class=" clear"></div>
                            <div id="DivContinue" class=" btn-summerised">
                                <asp:HiddenField ID="hdnfTotalPaybleAmt" runat="server" />
                                <asp:Button runat="server" ID="btnBack" Text="Back" OnClick="btnBack_Click" />
                                <asp:Button runat="server" ID="btnSmbt" Text="Proceed" OnClick="btnSmbt_Click" />
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
                            <div style="height: 30px;"></div>
                            <asp:Panel ID="customerLogin" runat="server">
                                <table id="tableVerify" runat="server" visible="false">
                                    <tr>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txtCode" placeholder="Enter Code" runat="server"></asp:TextBox>
                                            <asp:Button ID="btnVerify" runat="server" OnClick="btnVerify_Click" Text="Verify" />
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hfVCode" runat="server" />
                                <table id="TableCust" style="width: 23%;" runat="server">
                                    <tr>
                                        <td>Enter your Email</td>
                                        <td>
                                            <asp:TextBox ID="txtCustMailId" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Enter password</td>
                                        <td>
                                            <asp:TextBox ID="txtCustPass" runat="server" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtCustPass" ValidationGroup="CustLogin"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="txtRegNow" runat="server" Text="Register Now" OnClick="txtRegNow_Click" />
                                        </td>
                                        <td style="text-align: left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" ID="btnCustLogin" Text="Login" ValidationGroup="CustLogin" OnClick="btnCustLogin_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>

                            <asp:Panel ID="pnlCustReg" runat="server">
                                <div id="formTop" class="row noSideMargin backgroundPaperDark innerDropShadow topbotPadding">
                                    <div class="col-sm-8 col-xs-8 m3" style="margin-top: 50px;">
                                        <h1 style="font-size: 30px; text-align: center; margin-bottom: 30px;"><span>NEW USER REGISTER</span></h1>
                                        <div class=" clearfix"></div>
                                        <table>
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
                                                <td class="auto-style1">
                                                    <label for="txtName" class="control-label">Lastname</label></td>
                                                <td class="auto-style1">
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
                                                    <asp:TextBox ID="txtaddress2" runat="server" class="form-control" Style="margin-bottom: 15px;"></asp:TextBox></td>


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

                                            <tr style="display: none">
                                                <td>
                                                    <label for="txtPaymentMethod" class="control-label">Payment Method</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtPaymentMethod" runat="server" class="form-control"></asp:TextBox>




                                                </td>


                                            </tr>

                                            <tr style="text-align: center">
                                                <td colspan="2">
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info font16" OnClick="btnSubmit_Click" ValidationGroup="Cust" />

                                                    <asp:Button ID="btnCloseCust" runat="server" OnClick="btnCloseCust_Click" Text="Close" />
                                                </td>

                                            </tr>

                                        </table>
                                    </div>
                                    <div class=" clearfix"></div>
                                    <div class="puller"></div>
                                </div>
                            </asp:Panel>
                            <div>
                                <center>
                                <asp:Panel ID="pnlFullDetails" class="CSSTableGenerator" Width="70%" runat="server">
                                    <div id="BookRef" runat="server">
                                        <table style="border: 1px solid; background: -webkit-gradient( linear, left top, left bottom, color-stop(0.05, #a9cae3), color-stop(1, #a9cae3) );">
                                            <tr>
                                                <td class="auto-style5">Enter Booking Reference Name.</td>
                                                <td style="text-align: left">
                                                    <asp:TextBox ID="txtBookRef" runat="server" Width="281px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="ReqBookRef" runat="server" ControlToValidate="txtBookRef" ErrorMessage="*" ValidationGroup="Pay"></asp:RequiredFieldValidator></td>
                                            </tr>
                                        </table>
                                    </div>
                                    <br />
                                    <h2 style="font-family: 'Times New Roman'; font-style: italic; font-weight: bold">Payment Details</h2>
                                    <table style="border: thin solid #000000">
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
                                    <div style="height: 40px"></div>
                                    <asp:Panel ID="panelwithoutCreditAgent" Width="100%" runat="server" Font-Size="Medium">
                                        <div>
                                            <table style="border: thin solid #000000;">
                                                <tr style="background-color: rgba(149, 190, 222, 0.8) !important">
                                                    <td style="border: thin solid #000000; font-weight: bold; width: 22%;" class="auto-style3">Total Amount</td>
                                                    <td style="border: thin solid #000000" class="auto-style4">
                                                        <asp:Label ID="lbltotAmt" runat="server" Text="Label"></asp:Label></td>

                                                </tr>
                                                <tr style="background-color: #EFF3FB;">
                                                    <td style="border: thin solid #000000; font-weight: bold">Booking Amount</td>
                                                    <td style="border: thin solid #000000">
                                                        <asp:Label ID="lblCurrency" runat="server" Text="Label"></asp:Label><asp:Label ID="txtPaidAmt" runat="server"></asp:Label>
                                                        <asp:Label ID="lblpertext" runat="server"></asp:Label>
                                                    </td>

                                                </tr>
                                                <tr style="background-color: rgba(149, 190, 222, 0.8) !important">
                                                    <td style="border: thin solid #000000; font-weight: bold">Balance Amount</td>
                                                    <td style="border: thin solid #000000">
                                                        <asp:Label ID="lblBalanceAmt" runat="server" Text="Label"></asp:Label>
                                                        <asp:Label ID="Priorto" runat="server"></asp:Label>
                                                        <asp:Label ID="lblPrToDate" runat="server" Text="Label"></asp:Label></td>
                                                </tr>
                                            </table>
                                            <br />
                                            <div style="float: left">Note: Payment alert will be sent to you on Day-110.</div>
                                        </div>
                                        <br />
                                    </asp:Panel>
                                </asp:Panel>
                                <div style="height: 40px"></div>
                                <asp:Panel ID="pnlBookButton" Width="70%" runat="server">
                                    <asp:Button ID="btnPayProceed" runat="server" Text="Proceed For Payment" OnClick="btnPayProceed_Click" ValidationGroup="Pay" Font-Size="Medium" />
                                    <asp:Label ID="lblPaymentErr" runat="server" ForeColor="#FF3300"></asp:Label>
                                    <asp:Label ID="lblBookingLockFound" runat="server" ForeColor="#FF3300" Visible="false"></asp:Label>
                                    <asp:HyperLink ID="lnkBackToCruiseBooking" runat="server" CssClass="applink" Text="Back To Rooms Selection" Visible="false"></asp:HyperLink>
                                    <br />
                                    <asp:HiddenField ID="hftxtpaidamt" runat="server" />
                                </asp:Panel>
                                </center>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
    </form>
</body>
</html>
