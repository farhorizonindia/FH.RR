<%@ Page Language="C#" AutoEventWireup="true" Inherits="Cruise_booking_SearchProperty"
    CodeFile="~/Cruise/booking/SearchProperty2.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
<body class="bg-img">
    <script type="text/javascript">
        function setDate() {
            $("#txtChkin").datepicker({
                dateFormat: "dd-MM-yy",
                minDate: new Date(),
                onSelect: function () {
                    var dt2 = $('#txtChkOut');
                    var startDate = $(this).datepicker('getDate');
                    //add 30 days to selected date
                    startDate.setDate(startDate.getDate() + 30);
                    var minDate = $("#txtChkin").datepicker('getDate', '+1d');
                    minDate.setDate(minDate.getDate() + 1);
                    //minDate of dt2 datepicker = dt1 selected day
                    dt2.datepicker('setDate', minDate);
                    //sets dt2 maxDate to the last day of 30 days window
                    dt2.datepicker('option', 'maxDate', startDate);
                    //first day which can be selected in dt2 is selected date in dt1
                    dt2.datepicker('option', 'minDate', minDate);

                }
            });
            $('#txtChkOut').datepicker({
                dateFormat: "dd-MM-yy"
            });
        }
        $(document).ready(function () {
            setDate();
        });
    </script>

    <form id="form1" runat="server">
        <div class="sitecontainer whiteBackground1">
            <%--<section>--%>
            <asp:ScriptManager ID="scmgrLocation" runat="server">
            </asp:ScriptManager>
            <div class="container">
                <div class="row">
                    <div class="col-md-12 text-center White-Box">
                        <h2>Search Accomodations <span class=" pull-right" style="padding-top: 7px;">
                            <asp:Label ID="lblUsername" runat="server" Font-Bold="true" ForeColor="Red" Text=" "></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button-link" OnClick="LinkButton1_Click">Logout</asp:LinkButton></span></h2>
                        <div class=" clearfix"></div>
                    </div>
                </div>
            </div>
            <asp:UpdatePanel ID="updatepanel1" runat="server">
                <ContentTemplate>
                    <div class="tbl-main White-Box2 padding2">
                        <table class="table table-striped">
                            <tr>
                                <td>
                                    <asp:Label ID="lblLoginas" runat="server" Text=" Login as "></asp:Label>
                                    <asp:LinkButton ID="lnkLogin" runat="server" PostBackUrl="~/Cruise/booking/agentLogin.aspx" Font-Underline="True"> Partner</asp:LinkButton>
                                    <asp:LinkButton ID="lnkCustLogin" runat="server" Font-Underline="True" OnClick="lnkCustLogin_Click">Customer</asp:LinkButton>
                                    <asp:LinkButton ID="lnkCustomerRegis" runat="server" Font-Underline="True" OnClick="lnkCustomerRegis_Click">Or Customer Registration</asp:LinkButton>

                                    <asp:LinkButton ID="lnkView" Style="margin-left: 10px" runat="server" OnClick="lnkView_Click">View your Bookings</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbtnSelectAccomtype" runat="server" OnSelectedIndexChanged="rbtnSelectAccomtype_SelectedIndexChanged" RepeatDirection="Horizontal" AutoPostBack="True">
                                        <asp:ListItem>Cruise</asp:ListItem>
                                        <asp:ListItem>Other</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>

                        <div id="divCruise" runat="server">
                            <div class="tbl-main2">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:DropDownList Enabled="false" ID="ddlDestination" Visible="false" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDestination_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlPackege" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlYear" runat="server">
                                            </asp:DropDownList>

                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDates" runat="server">
                                                <asp:ListItem Text="Month"></asp:ListItem>
                                                <asp:ListItem Text="Jan"></asp:ListItem>
                                                <asp:ListItem Text="Feb"></asp:ListItem>
                                                <asp:ListItem Text="Mar"></asp:ListItem>
                                                <asp:ListItem Text="Apr"></asp:ListItem>
                                                <asp:ListItem Text="May"></asp:ListItem>
                                                <asp:ListItem Text="Jun"></asp:ListItem>
                                                <asp:ListItem Text="Jul"></asp:ListItem>
                                                <asp:ListItem Text="Aug"></asp:ListItem>
                                                <asp:ListItem Text="Sept"></asp:ListItem>
                                                <asp:ListItem Text="Oct"></asp:ListItem>
                                                <asp:ListItem Text="Nov"></asp:ListItem>
                                                <asp:ListItem Text="Dec"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlRiver" Visible="false" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>

                        <div id="OtherAccoms" runat="server">
                            <div class="tbl-main4">
                                <table style="width: 80%;">
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlAccomType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAccomType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlAccomodationName" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="tbl-main5">
                                <table>
                                    <tr>
                                        <td>Check in
                        <asp:TextBox ID="txtChkin" runat="server" FontSize="17"></asp:TextBox>
                                            <%--<input type="button" id="btnStartDate" name="btnStartDate" onfocus="return setupCalendar('txtChkin', 'btnStartDate')"
                                                onclick="return setupCalendar('txtChkin', 'btnStartDate')" value="..." style="background: url('../../images/calender.png') no-repeat; background-size: 100%; width: 30px; height: 30px; color: white" />--%>
                                        </td>
                                        <td class="auto-style1">Check out
                        <asp:TextBox ID="txtChkOut" runat="server"></asp:TextBox>
                                            <%--<input type="button" id="btnEndDate" name="btnEndDate" onclick="return setupCalendar('txtChkOut', 'btnEndDate')"
                                                onfocus="return setupCalendar('txtChkOut','btnEndDate')" value="..." style="background: url('../../images/calender.png') no-repeat; background-size: 100%; width: 30px; height: 30px; color: white" />--%>
                                        </td>
                                        <td>No of Rooms :<asp:DropDownList ID="ddlNoofrooms" runat="server" OnSelectedIndexChanged="ddlNoofrooms_SelectedIndexChanged" Width="95%" AutoPostBack="True">
                                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                        </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="0" ValidationGroup="Search" ControlToValidate="ddlNoofrooms" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gdvRooms" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Rooms">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Guests">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlGuests" runat="server">
                                                                <asp:ListItem>1</asp:ListItem>
                                                                <asp:ListItem>2</asp:ListItem>
                                                                <asp:ListItem>3</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#9abfda" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnSearchOthAccom" runat="server" ValidationGroup="Search" Text="Check Availability" OnClick="btnSearchOthAccom_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <%--</section>--%>
        </div>

    </form>
</body>
</html>
