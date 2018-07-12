<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewuserRegister.aspx.cs" Inherits="Cruise_Masters_NewuserRegister" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" media="all" href="../../css/calendar-blue2.css" title="win2k-cold-1" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../../style.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../../bootstrap.min.css" />
    <link href="css/style.css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../../js/calendar/calendar.js"></script>
    <script language="javascript" type="text/javascript" src="../../js/calendar/calendar-en.js"></script>
    <script language="javascript" type="text/javascript" src="../../js/calendar/calendar-setup.js"></script>
    <script language="javascript" type="text/javascript" src="../../js/popups.js"></script>
    <script language="javascript" type="text/javascript" src="../../js/global.js"></script>
    <script language="javascript" type="text/javascript" src="../../js/client/booking.js"></script>
    <script language="javascript" type="text/javascript" src="../../js/jquery-1.11.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../../js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.12.0/themes/base/jquery-ui.css" />
    <script type="text/javascript" src="https://code.jquery.com/ui/1.12.0/jquery-ui.js"></script>
    <link href="../../css/newcss.css" rel="stylesheet" />
    <style type="text/css">
        .tbl-main {
        }

            .tbl-main table {
                width: 60%;
                margin: 0 auto;
                float: none;
                background-color: #eaeaea;
                border: 1px solid #ddd;
                padding: 10px;
            }

                .tbl-main table tr td {
                    width: 33.3%;
                    font-size: 17px;
                    color: #333;
                }

                    .tbl-main table tr td table {
                        width: 100%;
                    }




        .tbl-main2 table {
            width: 60%;
            margin: 0 auto;
            float: none;
            background-color: #eaeaea;
            border: 1px solid #ddd;
            padding: 10px;
        }

            .tbl-main2 table tr td {
                width: 25%;
                font-size: 17px;
                color: #333;
            }

                .tbl-main2 table tr td select {
                    border: solid 1px #CCCCCC;
                    background-color: #FFFFFF;
                    text-align: left !important;
                    padding: 7px 12px;
                    font-weight: 400;
                    line-height: 1.42857143;
                    width: 100%;
                }


                .tbl-main2 table tr td input[type=submit] {
                    color: #fff;
                    background-color: #5bc0de;
                    border-color: #46b8da;
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
                    float: right;
                }

        .tbl-main4 {
            width: 60%;
            margin: 0 auto;
            float: none;
            background-color: #eaeaea;
        }

            .tbl-main4 table {
                width: 60%;
                margin: 0 auto;
                float: none;
                background-color: #eaeaea;
                border: 1px solid #ddd;
                padding: 10px;
            }

                .tbl-main4 table tr td {
                    width: 33.3%;
                    font-size: 17px;
                    color: #333;
                }


                    .tbl-main4 table tr td select {
                        border: solid 1px #CCCCCC;
                        background-color: #FFFFFF;
                        text-align: left !important;
                        padding: 7px 12px;
                        font-weight: 400;
                        line-height: 1.42857143;
                        width: 100%;
                    }



        .tbl-main5 table {
            width: 60%;
            margin: 0 auto;
            float: none;
            background-color: #eaeaea;
            border: 1px solid #ddd;
            padding: 10px;
        }

            .tbl-main5 table tr td {
                width: 27.3%;
                font-size: 17px;
                color: #333;
            }



                .tbl-main5 table tr td input[type=text] {
                    border: solid 1px #CCCCCC;
                    background-color: #FFFFFF;
                    text-align: left !important;
                    padding: 7px 12px;
                    font-weight: 400;
                    line-height: 1.42857143;
                    width: 95%;
                }


                .tbl-main5 table tr td .datebutton {
                    background-color: #5bc0de;
                    border-color: #46b8da;
                    height: 34px;
                    width: 30px;
                    margin-left: -6px;
                    color: #fff;
                }


                .tbl-main5 table tr td input[type=submit] {
                    color: #fff;
                    background-color: #5bc0de;
                    border-color: #46b8da;
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
                    width: 100%;
                    margin-top: 15px;
                }

                .tbl-main5 table tr td select {
                    border: solid 1px #CCCCCC;
                    background-color: #FFFFFF;
                    text-align: left !important;
                    padding: 7px 12px;
                    font-weight: 400;
                    line-height: 1.42857143;
                    width: 100%;
                }


        .header-part {
            padding: 20px;
            background-color: #5bc0de;
            border-color: #46b8da;
            margin-bottom: 30px;
            text-align: center;
        }


            .header-part h2 {
                color: #fff;
                font-size: 30px;
            }

        .auto-style1 {
            width: 33%;
        }

        .button-link {
            padding: 10px 15px;
            background: #4479BA;
            color: #FFF;
            font-size: medium;
        }
    </style>
    <script type="text/javascript">
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r;
            i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            },
            i[r].l = 1 * new Date();
            a = s.createElement(o),
            m = s.getElementsByTagName(o)[0];
            a.async = 1;
            a.src = g;
            m.parentNode.insertBefore(a, m)
        })(window, document, 'script', 'https://www.google-analytics.com/analytics.js', 'ga');
        ga('create', 'UA-81338023-3', 'auto');
        ga('send', 'pageview');
    </script>
</head>
<body class="bg-img1" style="font-family: 'Times New Roman'; font-style: italic">
    <form id="form1" runat="server">
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
                        <asp:TextBox ID="txtMailAddress" runat="server" class="form-control" AutoPostBack="True"></asp:TextBox>
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
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info font16" ValidationGroup="Cust" OnClick="btnSubmit_Click" />

                        <asp:Button ID="btnCloseCust" runat="server" Text="Close" />
                    </td>

                </tr>

            </table>
        </div>
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
        </asp:Panel>
    </form>
</body>
</html>
