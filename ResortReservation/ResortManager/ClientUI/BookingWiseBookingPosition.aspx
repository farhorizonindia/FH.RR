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
        .clstimedate { text-align:right; padding:0 10px 10px 0;
        }

        .cls_bokingdtl table tr td {
    padding: 2px 4px;
}

        .cls_bokingdtl {
    padding: 0 5px;
}
        .cls_bokingdtl table {
    width: 100%;
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
                            <asp:TextBox ID="txtTo" runat="server" AutoPostBack="True" OnTextChanged="txtTo_TextChanged"></asp:TextBox>
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
            <div class="clstimedate">
as on:<asp:Label ID="lblCurrentDateTime" runat="server"></asp:Label> <asp:Label ID="lblTime" runat="server"></asp:Label>

                <%--<table>
                    <tr>
                        <td>
                        </td>
                        <td>
                            
                            
                        </td>
                    </tr>
                </table>--%>
            </div>
            <div id="div1" runat="server" class="cls_bokingdtl">
                <table  border="1" cellpadding="0" cellspacing="0">
                    <tr>
                        <th width="20" style="background-color: #507CD1;">Sno</th>
                        <th class="auto-style2" style="background-color: #507CD1;">Dispatch Date</th>
                        <th style="background-color: #507CD1;">Nights</th>
                     <%--   <th style="background-color: #507CD1;">U/D</th>--%>
                        <th style="background-color: #507CD1;">Booking Code</th>
                        <th style="background-color: #507CD1;">Booking Refrence</th>
                        <th style="background-color: #507CD1;">Main Agent</th>
                        <th style="background-color: #507CD1;">Ref Agent</th>
                        <th style="background-color: #507CD1;">Borading Location</th>

                        <th style="background-color: #507CD1;">De-Boarding Location</th>
                        <th style="background-color: #507CD1;">Status</th>
                        <th style="background-color: #507CD1;">Suite</th>
                        <th style="background-color: #507CD1;">Swb</th>
                        <th style="background-color: #507CD1;">Swob</th>
                       <%--  <th style="background-color: #507CD1;">Lcwb</th>--%>
                        <th style="background-color: #507CD1;">Total</th>
                        <th width="80" style="background-color: #507CD1;">Booking/ Revenue Amount</th>
                    </tr>
                    <tr>
                        <%
                            Int32 groupcount = 0;
                            for (int k = 0; k < dtgroupby.Rows.Count; k++)
                            {
                                groupcount++;

                                 int Sno = 1;
                            System.Data.DataTable dtnew = new System.Data.DataTable();

                            System.Data.DataView dvopendates = new  System.Data.DataView(dtall);
                            string filter = "StartDate>='"+ dtgroupby.Rows[k]["StartDate"]+"' and StartDate<'"+dtgroupby.Rows[k]["EndDate"]+"'";
                            dvopendates.RowFilter = filter;

                            dtnew= dvopendates.ToTable();

                                if (dtnew != null && dtnew.Rows.Count > 0)
                                { %>


                        <% 

                            try
                            {
                                string name = (dtnew.Rows[0]["ShortPackName"]).ToString();

                                //if (dt.Rows[0]["PackageName"].ToString().Contains("Downstream"))
                                //{
                                //    name = ("Downstream");
                                //}
                                //else if (dt.Rows[0]["PackageName"].ToString().Contains("Upstream"))
                                //{
                                //    name = ("Upstream");
                                //}
                        %>
                        <td style="background-color: yellow;"><%{
                                                                      Response.Write("Group" + groupcount);
                                                                      

                                                                  } %> :</td>
                        <td colspan="15" style="background-color: yellow;"><%Response.Write((dtnew.Rows[0]["BordingFrom"]).ToString().Split('(')[0] + (dtnew.Rows[0]["BoadingTo"]).ToString().Split('(')[0] + "" + name + ", Starting " + Convert.ToDateTime(dtnew.Rows[0]["StartDate"].ToString()).ToString("dd MMMM yyyy")); %></td>
                        <% 
                           


                            for (int i = 0; i < dtnew.Rows.Count; i++)
                            {%>
                    </tr>
                    <tr>
                        <td><%

                                if (dtnew.Rows[i]["StartDate"].ToString() != "")
                                {
                                    Response.Write(Sno);
                                }


                        %></td>

                        <td class="auto-style2"><%Response.Write(Convert.ToDateTime(dtnew.Rows[i]["StartDate"].ToString()).ToString("dd-MMM-yyyy")); %></td>
                        <td><%Response.Write((dtnew.Rows[i]["NoOFNights"]).ToString()); %></td>
                      <%--  <td>
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
                        </td>--%>
                        <%--<td><%Response.Write((dt.Rows[i]["PackageName"]).ToString().Split(' ')[28]); %></td>--%>
                        <td><%Response.Write((dtnew.Rows[i]["BookingCode"]).ToString()); %></td>
                        <td><%Response.Write((dtnew.Rows[i]["BookingRef"]).ToString()); %></td>
                        <td><%Response.Write((dtnew.Rows[i]["AgentName"]).ToString()); %></td>
                        <td><%Response.Write((dtnew.Rows[i]["RefAgent"]).ToString()); %></td>
                        <td><%Response.Write((dtnew.Rows[i]["BordingFrom"]).ToString()); %></td>
                        <td><%Response.Write((dtnew.Rows[i]["BoadingTo"]).ToString()); %></td>
                        <td><%Response.Write((dtnew.Rows[i]["BStaus"]).ToString()); %> </td>
                        <td><%Response.Write((dtnew.Rows[i]["TotalSuit"]).ToString()); %></td>
                        <td><%Response.Write((dtnew.Rows[i]["TotalSwb"]).ToString()); %></td>
                        <td><%Response.Write((dtnew.Rows[i]["TotalSwob"]).ToString()); %></td>
                     <%--    <td><%Response.Write((dtnew.Rows[i]["TotalLcwb"]).ToString()); %></td>--%>
                        <td><%Response.Write((dtnew.Rows[i]["AllTotal"]).ToString()); %></td>
                        <td><%Response.Write((dtnew.Rows[i]["PaidAmt"]).ToString()); %></td>

                    </tr>
                    <% 

                            Sno++;
                        }%>
                    <tr>
                        <td class="auto-style2"><b>Booking Details</b></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                      <%--  <td></td>--%>

                      <td></td>
                     <%--   <td></td>--%>

                        <td>
                            <table class="auto-style1">
                                <tr>
                                    <td><b> Proposed</b></td>
                                    

                                </tr>
                                <tr>

                                     <td><b> Confirmed</b></td>
                                </tr>
                                <tr>
                                    <td><b> Booked</b></td>
                                </tr>
                                <tr>
                                    <td><b> Waitlisted</b></td>
                                </tr>
                                <tr>
                                    <td><b> Total </b></td>
                                </tr>
                            </table>
                        </td>

                        <td>
                            <table class="auto-style3">

                                <tr>
                                    <td><%--<%Response.Write((dt2.Rows[0]["SuitP"]).ToString()); %>--%>
                                        <%
                                            int sum = 0;
                                            System.Data.DataRow[] foundAuthors = dtnew.Select("BStaus = 'Proposed'");
                                            if (foundAuthors.Length != 0)
                                            {
                                                sum = Convert.ToInt32(dtnew.Compute("SUM(TotalSuit)", "BStaus='Proposed'"));

                                            }
                                            Response.Write(sum);

                                             %>

                                    </td>
                                    </tr>

                             <%--   <tr>
                                  <td>
                                        <%
                                            int sum7 = 0;
                                            System.Data.DataRow[] check7 = dtnew.Select("BStaus = 'Proposed'");
                                            if (check7.Length != 0)
                                            {
                                                sum7 = Convert.ToInt32(dtnew.Compute("SUM(TotalSwob)", "BStaus='Proposed'"));

                                            }

                                            Response.Write(sum7);
                                             %>
                                    </td>
                                    

                                    </tr>--%>
                                <tr>
                                    <td>

                                         <%
                                            int sumc = 0;
                                            System.Data.DataRow[] foundAuthorsc = dtnew.Select("BStaus = 'Confirm'");
                                            if (foundAuthorsc.Length != 0)
                                            {
                                                sumc = Convert.ToInt32(dtnew.Compute("SUM(TotalSuit)", "BStaus='Confirm'"));

                                            }
                                            Response.Write(sumc);

                                             %>
                                    </td>
                                </tr>
                                <tr>
                                    <td><%--<%Response.Write((dt2.Rows[0]["SuitB"]).ToString()); %>--%>
                                         <%
                                             int sum2 = 0;
                                             System.Data.DataRow[] check2 = dtnew.Select("BStaus = 'Booked'");
                                             if (check2.Length != 0)
                                             {
                                                 sum2 = Convert.ToInt32(dtnew.Compute("SUM(TotalSuit)", "BStaus='Booked'"));

                                             }
                                             Response.Write(sum2);
                                             %>
                                    </td>
                                  
                                </tr>
                                <tr>
                                    <td><%--<%Response.Write((dt2.Rows[0]["SuitW"]).ToString()); %>--%>

                                          <%
                                              int sum3 = 0;
                                              System.Data.DataRow[] check3 = dtnew.Select("BStaus = 'Waiting'");
                                              if (check3.Length != 0)
                                              {
                                                  sum3 = Convert.ToInt32(dtnew.Compute("SUM(TotalSuit)", "BStaus='Waiting'"));

                                              }

                                              Response.Write(sum3);
                                             %>
                                    </td>
                                </tr>
                                <tr>
                                    <td><%--<%Response.Write((dt2.Rows[0]["TotalSuit"]).ToString()); %>--%>

                                       <%
                                           Response.Write(sum + Convert.ToInt32(sum2) + sum3+sumc);
                                            %>

                                    </td>
                                </tr>
                            </table>
                        </td>


                        <td>
                            <table class="auto-style3">
                                <tr>
                                    <td><%--<%Response.Write((dt2.Rows[0]["SwbP"]).ToString()); %>--%>
                                       <%
                                           int sum4 = 0;
                                           System.Data.DataRow[] check4 = dtnew.Select("BStaus = 'Proposed'");
                                           if (check4.Length != 0)
                                           {
                                               sum4 = Convert.ToInt32(dtnew.Compute("SUM(TotalSwb)", "BStaus='Proposed'"));

                                           }

                                           Response.Write(sum4);

                                            %>
                                    </td>
                                     </tr>
                                <tr>
                                     <td><%--<%Response.Write((dt2.Rows[0]["SwbP"]).ToString()); %>--%>
                                       <%
                                           int sum4c = 0;
                                           System.Data.DataRow[] check4c = dtnew.Select("BStaus = 'Confirm'");
                                           if (check4c.Length != 0)
                                           {
                                               sum4c = Convert.ToInt32(dtnew.Compute("SUM(TotalSwb)", "BStaus='Confirm'"));

                                           }

                                           Response.Write(sum4c);

                                            %>
                                    </td>
                                </tr>
                                <tr>
                                    <td><%--<%Response.Write((dt2.Rows[0]["SwbB"]).ToString()); %>--%>
                                    <%
                                        int sum5 = 0;
                                        System.Data.DataRow[] check5 = dtnew.Select("BStaus = 'Booked'");
                                        if (check5.Length != 0)
                                        {
                                            sum5 = Convert.ToInt32(dtnew.Compute("SUM(TotalSwb)", "BStaus='Booked'"));

                                        }

                                        Response.Write(sum5);

                                        %>
                                    </td>
                                </tr>
                                <tr>
                                    <td> 
                                         <%  
                                             int sum6 = 0;
                                             System.Data.DataRow[] check6 = dtnew.Select("BStaus = 'Waiting'");
                                             if (check6.Length != 0)
                                             {
                                                 sum6 = Convert.ToInt32(dtnew.Compute("SUM(TotalSwb)", "BStaus='Waiting'"));

                                             }

                                             Response.Write(sum6);


                                               %>

                                    </td>
                                </tr>
                                <tr>
                                    <td><%--<%Response.Write((dt2.Rows[0]["TotalSwb"]).ToString()); %>--%>
                                        <%    Response.Write(sum4 + sum5 + sum6+sum4c);

                                               %>

                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table class="auto-style3">
                                <tr>
                                    <td>
                                        <%
                                            int sum7 = 0;
                                            System.Data.DataRow[] check7 = dtnew.Select("BStaus = 'Proposed'");
                                            if (check7.Length != 0)
                                            {
                                                sum7 = Convert.ToInt32(dtnew.Compute("SUM(TotalSwob)", "BStaus='Proposed'"));

                                            }

                                            Response.Write(sum7);
                                             %>
                                    </td>
                                     </tr>
                                <tr>
                                      <td>
                                        <%
                                            int sum7c = 0;
                                            System.Data.DataRow[] check7c = dtnew.Select("BStaus = 'Confirm'");
                                            if (check7c.Length != 0)
                                            {
                                                sum7c = Convert.ToInt32(dtnew.Compute("SUM(TotalSwob)", "BStaus='Confirm'"));

                                            }

                                            Response.Write(sum7c);
                                             %>
                                    </td>
                                </tr>
                                <tr>
                                    <td> <%
                                             int sum8 = 0;
                                             System.Data.DataRow[] check8 = dtnew.Select("BStaus = 'Booked'");
                                             if (check8.Length != 0)
                                             {
                                                 sum8 = Convert.ToInt32(dtnew.Compute("SUM(TotalSwob)", "BStaus='Booked'"));

                                             }

                                             Response.Write(sum8);
                                             %></td>
                                </tr>
                                <tr>
                                    <td>
                                         <%
                                             int sum9 = 0;
                                             System.Data.DataRow[] check9 = dtnew.Select("BStaus = 'Waiting'");
                                             if (check9.Length != 0)
                                             {
                                                 sum9 = Convert.ToInt32(dtnew.Compute("SUM(TotalSwob)", "BStaus='Waiting'"));

                                             }

                                             Response.Write(sum9);
                                             %>
                                    </td>
                                </tr>
                                <tr>
                                    <td><%

                                            Response.Write(sum7 + sum8 + sum9+sum7c);
                                             %></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table class="auto-style3">
                                <tr>
                                    <td>
                                        <%

                                            Response.Write(sum + sum4 + sum7);
                                             %>

                                    </td>
                                    </tr>
                                    <tr>
                                        <td>
 <%

                                            Response.Write(sumc + sum4c + sum7c);
                                             %>

                                        </td>
                                    
                                </tr>
                                <tr>
                                    <td><%Response.Write(sum2 + sum5 + sum8); %></td>
                                </tr>
                                <tr>
                                    <td><% Response.Write(sum3 + sum6 + sum9); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write(sum + sum2 + sum3 + sum4 + sum5 + sum6 + sum7 + sum8 + sum9+sumc+sum4c+sum7c); %></td>
                                </tr>
                            </table>
                        </td>
                    <%--    <td>
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
                        </td>--%>
                    </tr>

                    

                    <%
                                }
                                catch (Exception ex)
                                {
                                }

                            }
                        }
                    %>

                  
                </table>

            </div>


            <div id="div2" runat="server" visible="false" class="cls_bokingdtl">
                <table  border="1" cellpadding="0" cellspacing="0">
                    <tr>
                        <th width="20" style="background-color: #507CD1;">Sno</th>
                        <th class="auto-style2" style="background-color: #507CD1;">Dispatch Date</th>
                        <th style="background-color: #507CD1;">Nights</th>
                     <%--   <th style="background-color: #507CD1;">U/D</th>--%>
                        <th style="background-color: #507CD1;">Booking Code</th>
                        <th style="background-color: #507CD1;">Booking Refrence</th>
                        <th style="background-color: #507CD1;">Main Agent</th>
                        <th style="background-color: #507CD1;">Ref Agent</th>
                        <th style="background-color: #507CD1;">Borading Location</th>

                        <th style="background-color: #507CD1;">De-Boarding Location</th>
                        <th style="background-color: #507CD1;">Status</th>
                        <th style="background-color: #507CD1;">Suite</th>
                        <th style="background-color: #507CD1;">Swb</th>
                        <th style="background-color: #507CD1;">Swob</th>
                         <th style="background-color: #507CD1;">Lcwb</th>
                        <th style="background-color: #507CD1;">Total</th>
                        <th width="80" style="background-color: #507CD1;">Booking/ Revenue Amount</th>
                    </tr>
                    <tr>
                        <%
                            Int32 groupcount = 0;
                            for (int k = 0; k < dtgroupby.Rows.Count; k++)
                            {
                                groupcount++;

                                 int Sno = 1;
                            System.Data.DataTable dtnew = new System.Data.DataTable();

                            System.Data.DataView dvopendates = new  System.Data.DataView(dtall);
                            string filter = "StartDate>='"+ dtgroupby.Rows[k]["StartDate"]+"' and StartDate<'"+dtgroupby.Rows[k]["EndDate"]+"'";
                            dvopendates.RowFilter = filter;

                            dtnew= dvopendates.ToTable();

                                if (dtnew != null && dtnew.Rows.Count > 0)
                                { %>


                        <% 

                            try
                            {
                                string name = (dtnew.Rows[0]["ShortPackName"]).ToString();

                                //if (dt.Rows[0]["PackageName"].ToString().Contains("Downstream"))
                                //{
                                //    name = ("Downstream");
                                //}
                                //else if (dt.Rows[0]["PackageName"].ToString().Contains("Upstream"))
                                //{
                                //    name = ("Upstream");
                                //}
                        %>
                        <td style="background-color: yellow;"><%{
                                                                      Response.Write("Group" + groupcount);
                                                                      

                                                                  } %> :</td>
                        <td colspan="15" style="background-color: yellow;"><%Response.Write((dtnew.Rows[0]["BordingFrom"]).ToString().Split('(')[0] + (dtnew.Rows[0]["BoadingTo"]).ToString().Split('(')[0] + "" + name + ", Starting " + Convert.ToDateTime(dtnew.Rows[0]["StartDate"].ToString()).ToString("dd MMMM yyyy")); %></td>
                        <% 
                           


                            for (int i = 0; i < dtnew.Rows.Count; i++)
                            {%>
                    </tr>
                    <tr>
                        <td><%

                                if (dtnew.Rows[i]["StartDate"].ToString() != "")
                                {
                                    Response.Write(Sno);
                                }


                        %></td>

                        <td class="auto-style2"><%Response.Write(Convert.ToDateTime(dtnew.Rows[i]["StartDate"].ToString()).ToString("dd-MMM-yyyy")); %></td>
                        <td><%Response.Write((dtnew.Rows[i]["NoOFNights"]).ToString()); %></td>
                      <%--  <td>
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
                        </td>--%>
                        <%--<td><%Response.Write((dt.Rows[i]["PackageName"]).ToString().Split(' ')[28]); %></td>--%>
                        <td><%Response.Write((dtnew.Rows[i]["BookingCode"]).ToString()); %></td>
                        <td><%Response.Write((dtnew.Rows[i]["BookingRef"]).ToString()); %></td>
                        <td><%Response.Write((dtnew.Rows[i]["AgentName"]).ToString()); %></td>
                        <td><%Response.Write((dtnew.Rows[i]["RefAgent"]).ToString()); %></td>
                        <td><%Response.Write((dtnew.Rows[i]["BordingFrom"]).ToString()); %></td>
                        <td><%Response.Write((dtnew.Rows[i]["BoadingTo"]).ToString()); %></td>
                        <td><%Response.Write((dtnew.Rows[i]["BStaus"]).ToString()); %> </td>
                        <td><%Response.Write((dtnew.Rows[i]["TotalSuit"]).ToString()); %></td>
                        <td><%Response.Write((dtnew.Rows[i]["TotalSwb"]).ToString()); %></td>
                        <td><%Response.Write((dtnew.Rows[i]["TotalSwob"]).ToString()); %></td>
                         <td><%Response.Write((dtnew.Rows[i]["TotalLcwb"]).ToString()); %></td>
                        <td><%Response.Write((dtnew.Rows[i]["AllTotal"]).ToString()); %></td>
                        <td><%Response.Write((dtnew.Rows[i]["PaidAmt"]).ToString()); %></td>

                    </tr>
                    <% 

                            Sno++;
                        }%>
                    <tr>
                        <td class="auto-style2"><b>Booking Details</b></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                 

                      <td></td>
                     <%--   <td></td>--%>

                        <td>
                            <table class="auto-style1">
                                <tr>
                                    <td><b> Proposed</b></td>
                                    

                                </tr>
                                <tr>

                                     <td><b> Confirmed</b></td>
                                </tr>
                                <tr>
                                    <td><b> Booked</b></td>
                                </tr>
                                <tr>
                                    <td><b> Waitlisted</b></td>
                                </tr>
                                <tr>
                                    <td><b> Total </b></td>
                                </tr>
                            </table>
                        </td>

                          <%--<td></td>--%>
                        <td>
                            <table class="auto-style3">

                                <tr>
                                    <td><%--<%Response.Write((dt2.Rows[0]["SuitP"]).ToString()); %>--%>
                                        <%
                                            int sum = 0;
                                            System.Data.DataRow[] foundAuthors = dtnew.Select("BStaus = 'Proposed'");
                                            if (foundAuthors.Length != 0)
                                            {
                                                sum = Convert.ToInt32(dtnew.Compute("SUM(TotalSuit)", "BStaus='Proposed'"));

                                            }
                                            Response.Write(sum);

                                             %>

                                    </td>
                                    </tr>

                             <%--   <tr>
                                  <td>
                                        <%
                                            int sum7 = 0;
                                            System.Data.DataRow[] check7 = dtnew.Select("BStaus = 'Proposed'");
                                            if (check7.Length != 0)
                                            {
                                                sum7 = Convert.ToInt32(dtnew.Compute("SUM(TotalSwob)", "BStaus='Proposed'"));

                                            }

                                            Response.Write(sum7);
                                             %>
                                    </td>
                                    

                                    </tr>--%>
                                <tr>
                                    <td>

                                         <%
                                            int sumc = 0;
                                            System.Data.DataRow[] foundAuthorsc = dtnew.Select("BStaus = 'Confirm'");
                                            if (foundAuthorsc.Length != 0)
                                            {
                                                sumc = Convert.ToInt32(dtnew.Compute("SUM(TotalSuit)", "BStaus='Confirm'"));

                                            }
                                            Response.Write(sumc);

                                             %>
                                    </td>
                                </tr>
                                <tr>
                                    <td><%--<%Response.Write((dt2.Rows[0]["SuitB"]).ToString()); %>--%>
                                         <%
                                             int sum2 = 0;
                                             System.Data.DataRow[] check2 = dtnew.Select("BStaus = 'Booked'");
                                             if (check2.Length != 0)
                                             {
                                                 sum2 = Convert.ToInt32(dtnew.Compute("SUM(TotalSuit)", "BStaus='Booked'"));

                                             }
                                             Response.Write(sum2);
                                             %>
                                    </td>
                                  
                                </tr>
                                <tr>
                                    <td><%--<%Response.Write((dt2.Rows[0]["SuitW"]).ToString()); %>--%>

                                          <%
                                              int sum3 = 0;
                                              System.Data.DataRow[] check3 = dtnew.Select("BStaus = 'Waiting'");
                                              if (check3.Length != 0)
                                              {
                                                  sum3 = Convert.ToInt32(dtnew.Compute("SUM(TotalSuit)", "BStaus='Waiting'"));

                                              }

                                              Response.Write(sum3);
                                             %>
                                    </td>
                                </tr>
                                <tr>
                                    <td><%--<%Response.Write((dt2.Rows[0]["TotalSuit"]).ToString()); %>--%>

                                       <%
                                           Response.Write(sum + Convert.ToInt32(sum2) + sum3+sumc);
                                            %>

                                    </td>
                                </tr>
                            </table>
                        </td>


                        <td>
                            <table class="auto-style3">
                                <tr>
                                    <td><%--<%Response.Write((dt2.Rows[0]["SwbP"]).ToString()); %>--%>
                                       <%
                                           int sum4 = 0;
                                           System.Data.DataRow[] check4 = dtnew.Select("BStaus = 'Proposed'");
                                           if (check4.Length != 0)
                                           {
                                               sum4 = Convert.ToInt32(dtnew.Compute("SUM(TotalSwb)", "BStaus='Proposed'"));

                                           }

                                           Response.Write(sum4);

                                            %>
                                    </td>
                                     </tr>
                                <tr>
                                     <td><%--<%Response.Write((dt2.Rows[0]["SwbP"]).ToString()); %>--%>
                                       <%
                                           int sum4c = 0;
                                           System.Data.DataRow[] check4c = dtnew.Select("BStaus = 'Confirm'");
                                           if (check4c.Length != 0)
                                           {
                                               sum4c = Convert.ToInt32(dtnew.Compute("SUM(TotalSwb)", "BStaus='Confirm'"));

                                           }

                                           Response.Write(sum4c);

                                            %>
                                    </td>
                                </tr>
                                <tr>
                                    <td><%--<%Response.Write((dt2.Rows[0]["SwbB"]).ToString()); %>--%>
                                    <%
                                        int sum5 = 0;
                                        System.Data.DataRow[] check5 = dtnew.Select("BStaus = 'Booked'");
                                        if (check5.Length != 0)
                                        {
                                            sum5 = Convert.ToInt32(dtnew.Compute("SUM(TotalSwb)", "BStaus='Booked'"));

                                        }

                                        Response.Write(sum5);

                                        %>
                                    </td>
                                </tr>
                                <tr>
                                    <td> 
                                         <%  
                                             int sum6 = 0;
                                             System.Data.DataRow[] check6 = dtnew.Select("BStaus = 'Waiting'");
                                             if (check6.Length != 0)
                                             {
                                                 sum6 = Convert.ToInt32(dtnew.Compute("SUM(TotalSwb)", "BStaus='Waiting'"));

                                             }

                                             Response.Write(sum6);


                                               %>

                                    </td>
                                </tr>
                                <tr>
                                    <td><%--<%Response.Write((dt2.Rows[0]["TotalSwb"]).ToString()); %>--%>
                                        <%    Response.Write(sum4 + sum5 + sum6+sum4c);

                                               %>

                                    </td>
                                </tr>
                            </table>
                        </td>
                       
                        
                         <td>
                            <table class="auto-style3">
                                <tr>
                                    <td>
                                        <%
                                            int sum7 = 0;
                                            System.Data.DataRow[] check7 = dtnew.Select("BStaus = 'Proposed'");
                                            if (check7.Length != 0)
                                            {
                                                sum7 = Convert.ToInt32(dtnew.Compute("SUM(TotalSwob)", "BStaus='Proposed'"));

                                            }

                                            Response.Write(sum7);
                                             %>
                                    </td>
                                     </tr>
                                <tr>
                                      <td>
                                        <%
                                            int sum7c = 0;
                                            System.Data.DataRow[] check7c = dtnew.Select("BStaus = 'Confirm'");
                                            if (check7c.Length != 0)
                                            {
                                                sum7c = Convert.ToInt32(dtnew.Compute("SUM(TotalSwob)", "BStaus='Confirm'"));

                                            }

                                            Response.Write(sum7c);
                                             %>
                                    </td>
                                </tr>
                                <tr>
                                    <td> <%
                                             int sum8 = 0;
                                             System.Data.DataRow[] check8 = dtnew.Select("BStaus = 'Booked'");
                                             if (check8.Length != 0)
                                             {
                                                 sum8 = Convert.ToInt32(dtnew.Compute("SUM(TotalSwob)", "BStaus='Booked'"));

                                             }

                                             Response.Write(sum8);
                                             %></td>
                                </tr>
                                <tr>
                                    <td>
                                         <%
                                             int sum9 = 0;
                                             System.Data.DataRow[] check9 = dtnew.Select("BStaus = 'Waiting'");
                                             if (check9.Length != 0)
                                             {
                                                 sum9 = Convert.ToInt32(dtnew.Compute("SUM(TotalSwob)", "BStaus='Waiting'"));

                                             }

                                             Response.Write(sum9);
                                             %>
                                    </td>
                                </tr>
                                <tr>
                                    <td><%

                                            Response.Write(sum7 + sum8 + sum9+sum7c);
                                             %></td>
                                </tr>
                            </table>
                        </td>
                       
                        <td>
                            <table class="auto-style3">
                                <tr>
                                    <td>
                                        <%
                                            int sum11 = 0;
                                            System.Data.DataRow[] check11 = dtnew.Select("BStaus = 'Proposed'");
                                            if (check11.Length != 0)
                                            {
                                                sum11 = Convert.ToInt32(dtnew.Compute("SUM(TotalLcwb)", "BStaus='Proposed'"));

                                            }

                                            Response.Write(sum11);
                                             %>
                                    </td>
                                     </tr>
                                <tr>
                                      <td>
                                        <%
                                            int sum11c = 0;
                                            System.Data.DataRow[] check11c = dtnew.Select("BStaus = 'Confirm'");
                                            if (check11c.Length != 0)
                                            {
                                                sum11c = Convert.ToInt32(dtnew.Compute("SUM(TotalLcwb)", "BStaus='Confirm'"));

                                            }

                                            Response.Write(sum11c);
                                             %>
                                    </td>
                                </tr>
                                <tr>
                                    <td> <%
                                             int sum12 = 0;
                                             System.Data.DataRow[] check12 = dtnew.Select("BStaus = 'Booked'");
                                             if (check12.Length != 0)
                                             {
                                                 sum12 = Convert.ToInt32(dtnew.Compute("SUM(TotalLcwb)", "BStaus='Booked'"));

                                             }

                                             Response.Write(sum12);
                                             %></td>
                                </tr>
                                <tr>
                                    <td>
                                         <%
                                             int sum13 = 0;
                                             System.Data.DataRow[] check13 = dtnew.Select("BStaus = 'Waiting'");
                                             if (check13.Length != 0)
                                             {
                                                 sum13 = Convert.ToInt32(dtnew.Compute("SUM(TotalLcwb)", "BStaus='Waiting'"));

                                             }

                                             Response.Write(sum13);
                                             %>
                                    </td>
                                </tr>
                                <tr>
                                    <td><%

                                            Response.Write(sum11 + sum12 + sum13 + sum11c);
                                             %></td>
                                </tr>
                            </table>
                        </td>
                        
                        
                         <td>
                            <table class="auto-style3">
                                <tr>
                                    <td>
                                        <%

                                            Response.Write(sum + sum4 + sum7+ sum11);
                                             %>

                                    </td>
                                    </tr>
                                    <tr>
                                        <td>
 <%

                                            Response.Write(sumc + sum4c + sum7c+sum11c);
                                             %>

                                        </td>
                                    
                                </tr>
                                <tr>
                                    <td><%Response.Write(sum2 + sum5 + sum8 + sum12); %></td>
                                </tr>
                                <tr>
                                    <td><% Response.Write(sum3 + sum6 + sum9 +sum13); %></td>
                                </tr>
                                <tr>
                                    <td><%Response.Write(sum + sum2 + sum3 + sum4 + sum5 + sum6 + sum7 + sum8 + sum9 + sumc + sum4c + sum7c + sum11 + sum12 + sum13 + sum11c); %></td>
                                </tr>
                            </table>
                        </td>
                    <%--    <td>
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
                        </td>--%>
                    </tr>

                    

                    <%
                                }
                                catch (Exception ex)
                                {
                                }

                            }
                        }
                    %>

                  
                </table>

            </div>

        </div>
    </form>
</body>
</html>
