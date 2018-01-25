<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookingWiseBookingPosition.aspx.cs" Inherits="ClientUI_BookingWiseBookingPosition" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Booking Summery Report</title>

    <script language="javascript" type="text/javascript" src="../js/calendar/calendar.js"></script>
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-en.js"></script>
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-setup.js"></script>
    <script language="javascript" type="text/javascript" src="../js/global.js"></script>
    <script language="javascript" type="text/javascript" src="../js/client/viewbookings.js"></script>
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../css/viewbookings.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../css/calendar-blue2.css"
        title="win2k-cold-1" />

    <style type="text/css">
        .auto-style1 {
            width: 79px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Booking, Revenue and Availability Summary" />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Cruise/Booking/ARC_Logo.jpg.png" Style="padding-left: 36%; padding-bottom: 2%;" />
        </div>
        <div>
            <div>
                <table>
                    <tr>
                        <td>Packages:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPackage" runat="server"></asp:DropDownList>
                        </td>
                        <td>Operation Period From:
                        </td>
                        <td>
                            <asp:TextBox ID="txtfrom" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtfrom" runat="server" />
                        </td>
                        <td>Operation Period To:
                        </td>
                        <td>
                            <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtTo" runat="server" />
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlStatus" runat="server">
                                <asp:ListItem Text="Select"></asp:ListItem>
                                <asp:ListItem Text="Proposed"></asp:ListItem>
                                <asp:ListItem Text="Booked+Confirm"></asp:ListItem>
                                <asp:ListItem Text="Waiting"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        </td>
                    </tr>

                </table>
            </div>
            <div style="float: right; padding-right: 17%;">
                <table>
                    <tr>
                        <td>as on:
                        </td>
                        <td>
                            <asp:Label ID="lblCurrentDateTime" runat="server"></asp:Label>
                            <asp:Label ID="lblTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table border="1">
                    <tr>
                        <th style="background-color: #507CD1;">Sno</th>
                        <th class="auto-style2" style="background-color: #507CD1;">Despatch Date</th>
                        <th style="background-color: #507CD1;">Nights</th>
                        <th style="background-color: #507CD1;">U/D</th>
                        <th style="background-color: #507CD1;">Booking Code</th>
                        <th style="background-color: #507CD1;">Booking Refrence</th>
                        <th style="background-color: #507CD1;">Main Agent</th>
                        <th style="background-color: #507CD1;">Ref Agent</th>
                        <th style="background-color: #507CD1;">Borading Locatiuon</th>

                        <th style="background-color: #507CD1;">De Boarding Location</th>
                        <th style="background-color: #507CD1;">Status</th>
                        <th style="background-color: #507CD1;">Suite</th>
                        <th style="background-color: #507CD1;">Swb</th>
                        <th style="background-color: #507CD1;">Swob</th>
                        <th style="background-color: #507CD1;">Total</th>
                        <th style="background-color: #507CD1;">Booking/ Revenue Amount</th>
                    </tr>
                    <tr>
                        <% if (dt != null && dt.Rows.Count > 0)
                            { %>


                        <% 
                            try
                            {
                        %>
                        <td style="background-color: yellow;">Group1:</td>
                        <td style="width: 23%; background-color: yellow;"><%Response.Write((dt.Rows[0]["BordingFrom"]).ToString().Split('(')[0] + (dt.Rows[0]["BoadingTo"]).ToString().Split('(')[0] + "Upstream,Starting " + Convert.ToDateTime(dt.Rows[0]["StartDate"].ToString()).ToString("dd MMMM yyyy")); %></td>
                        <% 
                            int Sno = 1;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {%>
                    </tr>
                    <tr>
                        <td><%

                                if (dt.Rows[i]["StartDate"].ToString() != "")
                                {
                                    Response.Write(Sno);
                                }


                        %></td>

                        <td class="auto-style2"><%Response.Write(Convert.ToDateTime(dt.Rows[i]["StartDate"].ToString()).ToString("dd-MMM-yyyy")); %></td>
                        <td><%Response.Write((dt.Rows[i]["NoOFNights"]).ToString()); %></td>
                        <td>
                            <%
                                if (dt.Rows[i]["PackageName"].ToString().Contains("Downstream"))
                                {
                                    Response.Write("Downstream");
                                }
                                else if (dt.Rows[i]["PackageName"].ToString().Contains("Upstream"))
                                {
                                    Response.Write("Upstream");
                                }
                                else
                                {

                                }
                            %>
                        </td>
                        <%--<td><%Response.Write((dt.Rows[i]["PackageName"]).ToString().Split(' ')[28]); %></td>--%>
                        <td><%Response.Write((dt.Rows[i]["BookingCode"]).ToString()); %></td>
                        <td><%Response.Write((dt.Rows[i]["BookingRef"]).ToString()); %></td>
                        <td><%Response.Write((dt.Rows[i]["AgentName"]).ToString()); %></td>
                        <td><%Response.Write((dt.Rows[i]["Lagent"]).ToString()); %></td>
                        <td><%Response.Write((dt.Rows[i]["BordingFrom"]).ToString()); %></td>
                        <td><%Response.Write((dt.Rows[i]["BoadingTo"]).ToString()); %></td>
                        <td><%Response.Write((dt.Rows[i]["RoomStatus"]).ToString()); %> </td>
                        <td><%Response.Write((dt.Rows[i]["TotalSuit"]).ToString()); %></td>
                        <td><%Response.Write((dt.Rows[i]["TotalSwb"]).ToString()); %></td>
                        <td><%Response.Write((dt.Rows[i]["TotalSwob"]).ToString()); %></td>
                        <td><%Response.Write((dt.Rows[i]["AllTotal"]).ToString()); %></td>
                        <td><%Response.Write((dt.Rows[i]["PaidAmt"]).ToString()); %></td>

                    </tr>
                    <% 

                            Sno++;
                        }%>
                    <tr>
                        <td class="auto-style2">Booking Details</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>

                        <td>
                            <table class="auto-style1">
                                <tr>
                                    <td>Propsed</td>


                                </tr>
                                <tr>
                                    <td>Booked/Confirmed</td>
                                </tr>
                                <tr>
                                    <td>Waitlisted</td>
                                </tr>
                                <tr>
                                    <td>Total</td>
                                </tr>
                            </table>
                        </td>

                        <td>
                            <table class="auto-style3">

                                <tr>
                                    <td><%Response.Write((dt2.Rows[0]["SuitP"]).ToString()); %> </td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt2.Rows[0]["SuitB"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt2.Rows[0]["SuitW"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt2.Rows[0]["TotalSuit"]).ToString()); %></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table class="auto-style3">
                                <tr>
                                    <td><%Response.Write((dt2.Rows[0]["SwbP"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt2.Rows[0]["SwbB"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt2.Rows[0]["SwbW"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt2.Rows[0]["TotalSwb"]).ToString()); %></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table class="auto-style3">
                                <tr>
                                    <td><%Response.Write((dt2.Rows[0]["SwobP"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt2.Rows[0]["SwobB"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt2.Rows[0]["SwobW"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt2.Rows[0]["TotalSwob"]).ToString()); %></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table class="auto-style3">
                                <tr>
                                    <td><%Response.Write((dt2.Rows[0]["AllP"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt2.Rows[0]["AllB"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt2.Rows[0]["AllW"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt2.Rows[0]["AllTotal"]).ToString()); %></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table class="auto-style3">
                                <tr>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt2.Rows[0]["PaidAmt"]).ToString()); %></td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr>
                        <td>Availbility</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>


                            <table>
                                <% 
                                    for (int i = 0; i < dt4.Rows.Count; i++)
                                    {%>
                                <tr>
                                    <td><%Response.Write((dt4.Rows[i]["Getpackage"]).ToString()); %></td>
                                </tr>
                                <%} %>
                            </table>

                        </td>

                        <td>

                            <table class="auto-style3">
                                <% 

                                    for (int i = 0; i < dt4.Rows.Count; i++)
                                    {%>
                                <tr>
                                    <td><%Response.Write((dt4.Rows[i]["suite"]).ToString()); %></td>
                                </tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>

                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                               

                                <%} %>

                                <%--<tr>
                                    <td><%Response.Write((dt.Rows[i]["supwithbal"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt.Rows[i]["supwithoutbal"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt.Rows[i]["AllTotal"]).ToString()); %></td>
                                </tr>--%>
                            </table>

                        </td>
                        <td>

                            <table class="auto-style3">
                                <% 

                                    for (int i = 0; i < dt4.Rows.Count; i++)
                                    {%>
                                <tr>
                                    <td><%Response.Write((dt4.Rows[i]["supwithbal"]).ToString()); %></td>
                                </tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>

                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                
                                <%} %>

                                <%-- <tr>
                                    <td><%Response.Write((dt.Rows[i]["AllB"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt.Rows[i]["AllW"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt.Rows[i]["AllTotal"]).ToString()); %></td>
                                </tr>--%>
                            </table>

                        </td>
                        <td>



                            <table class="auto-style3">
                                <% 

                                    for (int i = 0; i < dt4.Rows.Count; i++)
                                    {%>
                                <tr>
                                    <td><%Response.Write((dt4.Rows[i]["supwithoutbal"]).ToString()); %></td>
                                </tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>

                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                
                                <%} %>

                                <%--  <tr>
                                    <td><%Response.Write((dt.Rows[i]["AllB"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt.Rows[i]["AllW"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt.Rows[i]["AllTotal"]).ToString()); %></td>
                                </tr>--%>
                            </table>

                        </td>
                        <td>

                            <table class="auto-style3">

                                <% 

                                    for (int i = 0; i < dt4.Rows.Count; i++)
                                    {%>

                                <tr>
                                    <td><%Response.Write((dt4.Rows[i]["getcaabtot"]).ToString()); %></td>
                                </tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>

                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                               
                                <%} %>

                                <%--  <tr>
                                    <td><%Response.Write((dt.Rows[i]["AllB"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt.Rows[i]["AllW"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt.Rows[i]["AllTotal"]).ToString()); %></td>
                                </tr>--%>
                            </table>

                        </td>

                    </tr>

                    <%
                            }
                            catch
                            {
                            }

                        }
                    %>

                    <tr>
                        <% if (dt1 != null && dt1.Rows.Count > 0)
                            { %>


                        <% 
                            try
                            {
                        %>
                        <td style="background-color: yellow;">Group2:</td>
                        <td style="width: 23%; background-color: yellow;"><%Response.Write((dt1.Rows[0]["BordingFrom"]).ToString().Split('(')[0] + (dt1.Rows[0]["BoadingTo"]).ToString().Split('(')[0] + "Downstream,Starting " + Convert.ToDateTime(dt1.Rows[0]["StartDate"].ToString()).ToString("dd MMMM yyyy")); %></td>
                        <% 
                            int Sno = 1;
                            for (int i = 0; i < dt1.Rows.Count; i++)
                            {%>
                    </tr>
                    <tr>
                        <td><%

                                if (dt1.Rows[i]["StartDate"].ToString() != "")
                                {
                                    Response.Write(Sno);
                                }


                        %></td>

                        <td class="auto-style2"><%Response.Write(Convert.ToDateTime(dt1.Rows[i]["StartDate"].ToString()).ToString("dd-MMM-yyyy")); %></td>
                        <td><%Response.Write((dt1.Rows[i]["NoOFNights"]).ToString()); %></td>
                        <td>
                            <%
                                if (dt1.Rows[i]["PackageName"].ToString().Contains("Downstream"))
                                {
                                    Response.Write("Downstream");
                                }
                                else if (dt1.Rows[i]["PackageName"].ToString().Contains("Upstream"))
                                {
                                    Response.Write("Upstream");
                                }
                                else
                                {

                                }
                            %>
                        </td>
                        <%--<td><%Response.Write((dt.Rows[i]["PackageName"]).ToString().Split(' ')[28]); %></td>--%>
                        <td><%Response.Write((dt1.Rows[i]["BookingCode"]).ToString()); %></td>
                        <td><%Response.Write((dt1.Rows[i]["BookingRef"]).ToString()); %></td>
                        <td><%Response.Write((dt1.Rows[i]["AgentName"]).ToString()); %></td>
                        <td><%Response.Write((dt1.Rows[i]["Lagent"]).ToString()); %></td>
                        <td><%Response.Write((dt1.Rows[i]["BordingFrom"]).ToString()); %></td>
                        <td><%Response.Write((dt1.Rows[i]["BoadingTo"]).ToString()); %></td>
                        <td><%Response.Write((dt1.Rows[i]["RoomStatus"]).ToString()); %> </td>
                        <td><%Response.Write((dt1.Rows[i]["TotalSuit"]).ToString()); %></td>
                        <td><%Response.Write((dt1.Rows[i]["TotalSwb"]).ToString()); %></td>
                        <td><%Response.Write((dt1.Rows[i]["TotalSwob"]).ToString()); %></td>
                        <td><%Response.Write((dt1.Rows[i]["AllTotal"]).ToString()); %></td>
                        <td><%Response.Write((dt1.Rows[i]["PaidAmt"]).ToString()); %></td>

                    </tr>
                    <% 

                            Sno++;
                        }%>
                    <tr>
                        <td class="auto-style2">Booking Details</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>

                        <td>
                            <table class="auto-style1">
                                <tr>
                                    <td>Propsed</td>


                                </tr>
                                <tr>
                                    <td>Booked/Confirmed</td>
                                </tr>
                                <tr>
                                    <td>Waitlisted</td>
                                </tr>
                                <tr>
                                    <td>Total</td>
                                </tr>
                            </table>
                        </td>

                        <td>
                            <table class="auto-style3">
                                <tr>
                                    <td><%Response.Write((dt3.Rows[0]["SuitP"]).ToString()); %> </td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt3.Rows[0]["SuitB"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt3.Rows[0]["SuitW"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt3.Rows[0]["TotalSuit"]).ToString()); %></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table class="auto-style3">
                                <tr>
                                    <td><%Response.Write((dt3.Rows[0]["SwbP"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt3.Rows[0]["SwbB"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt3.Rows[0]["SwbW"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt3.Rows[0]["TotalSwb"]).ToString()); %></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table class="auto-style3">
                                <tr>
                                    <td><%Response.Write((dt3.Rows[0]["SwobP"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt3.Rows[0]["SwobB"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt3.Rows[0]["SwobW"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt3.Rows[0]["TotalSwob"]).ToString()); %></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table class="auto-style3">
                                <tr>
                                    <td><%Response.Write((dt3.Rows[0]["AllP"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt3.Rows[0]["AllB"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt3.Rows[0]["AllW"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt3.Rows[0]["AllTotal"]).ToString()); %></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table class="auto-style3">
                                <tr>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt3.Rows[0]["PaidAmt"]).ToString()); %></td>
                                </tr>
                            </table>
                        </td>

                    </tr>

                    <tr>
                        <td>Availbility</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>

                            <table>
                                <% 

                                    for (int i = 0; i < dt5.Rows.Count; i++)
                                    {%>
                                <tr>
                                    <td><%Response.Write((dt5.Rows[i]["Getpackage"]).ToString()); %></td>
                                </tr>
                                <%} %>
                            </table>

                        </td>

                        <td>

                            <table class="auto-style3">
                                <% 

                                    for (int i = 0; i < dt5.Rows.Count; i++)
                                    {%>
                                <tr>
                                    <td><%Response.Write((dt5.Rows[i]["suite"]).ToString()); %></td>
                                </tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>

                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                
                                <%} %>

                                <%--<tr>
                                    <td><%Response.Write((dt.Rows[i]["supwithbal"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt.Rows[i]["supwithoutbal"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt.Rows[i]["AllTotal"]).ToString()); %></td>
                                </tr>--%>
                            </table>

                        </td>
                        <td>

                            <table class="auto-style3">
                                <% 

                                    for (int i = 0; i < dt5.Rows.Count; i++)
                                    {%>
                                <tr>
                                    <td><%Response.Write((dt5.Rows[i]["supwithbal"]).ToString()); %></td>
                                </tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>

                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                
                                <%} %>

                                <%-- <tr>
                                    <td><%Response.Write((dt.Rows[i]["AllB"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt.Rows[i]["AllW"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt.Rows[i]["AllTotal"]).ToString()); %></td>
                                </tr>--%>
                            </table>

                        </td>
                        <td>

                            <table class="auto-style3">
                                <% 

                                    for (int i = 0; i < dt5.Rows.Count; i++)
                                    {%>
                                <tr>
                                    <td><%Response.Write((dt5.Rows[i]["supwithoutbal"]).ToString()); %></td>
                                </tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>

                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                
                                <%} %>

                                <%--  <tr>
                                    <td><%Response.Write((dt.Rows[i]["AllB"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt.Rows[i]["AllW"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt.Rows[i]["AllTotal"]).ToString()); %></td>
                                </tr>--%>
                            </table>

                        </td>
                        <td>

                            <table class="auto-style3">
                                <% 

                                    for (int i = 0; i < dt5.Rows.Count; i++)
                                    {%>
                                <tr>
                                    <td><%Response.Write((dt5.Rows[i]["getcaabtot"]).ToString()); %></td>
                                </tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>

                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                <tr></tr>
                                
                                <%} %>

                                <%--  <tr>
                                    <td><%Response.Write((dt.Rows[i]["AllB"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt.Rows[i]["AllW"]).ToString()); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write((dt.Rows[i]["AllTotal"]).ToString()); %></td>
                                </tr>--%>
                            </table>

                        </td>

                    </tr>

                    <%
                            }
                            catch
                            {
                            }

                        }
                    %>
                </table>

            </div>

        </div>
    </form>
</body>
</html>
