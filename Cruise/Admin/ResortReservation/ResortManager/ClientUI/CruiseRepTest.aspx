<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CruiseRepTest.aspx.cs" Inherits="ClientUI_CruiseRepTest" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Booking List</title>
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
        .auto-style1
        {
            width: 80px;
            height: 19px;
        }

        table
        {
            border-collapse: collapse;
        }

        table, th, td
        {
            border: 1px solid black;
            padding: 5px;
        }

        .header1
        {
            background-color:darkgray;
            color:white
        }
    </style>

    <script type="text/javascript">
        function fnExcelReport() {
            var tab_text = "<table border='2px'><tr bgcolor='#87AFC6'>";
            var textRange; var j = 0;
            tab = document.getElementById('tbl'); // id of table
          
            for (j = 0 ; j < tab.rows.length ; j++) {
                tab_text = tab_text + tab.rows[j].innerHTML + "</tr>";
                //tab_text=tab_text+"</tr>";
            }

            tab_text = tab_text + "</table>";
            tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
            tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
            tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params

            var ua = window.navigator.userAgent;
            var msie = ua.indexOf("MSIE ");

            if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
            {
                txtArea1.document.open("txt/html", "replace");
                txtArea1.document.write(tab_text);
                txtArea1.document.close();
                txtArea1.focus();
                sa = txtArea1.document.execCommand("SaveAs", true, "abc.xls");
            }
            else                 //other browser not tested on IE 11
                sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));
           
          

            return (sa);
        }
</script>
</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Cruise Booking List" />
        <div>
            <asp:ScriptManager ID="scmgrViewBookings" runat="server">
            </asp:ScriptManager>
            <iframe id="txtArea1" style="display:none"></iframe>
            <table id="filtersection" class="filtersection">
                <tr>
                    <td class="filtersectionCell">Check-In:</td>
                    <td class="filtersectionCell">
                        <asp:TextBox CssClass="input" ID="txtStartDate" runat="server" Font-Size="8pt" Width="95px"></asp:TextBox>
                        <input type="button" class="datebutton" id="btnStartDate" name="btnStartDate" onfocus="return setupCalendar('txtStartDate','btnStartDate')"
                            onclick="return setupCalendar('txtStartDate', 'btnStartDate')" value="..." /></td>
                    <td class="filtersectionCell">Check-out:</td>
                    <td class="filtersectionCell">
                        <asp:TextBox CssClass="input" ID="txtEndDate" runat="server" Font-Size="8pt" Width="95px"></asp:TextBox>
                        <input type="button" class="datebutton" id="btnEndDate" name="btnEndDate" onclick="return setupCalendar('txtEndDate', 'btnEndDate')"
                            onfocus="return setupCalendar('txtEndDate','btnEndDate')" value="..." /></td>
                    <td class="filtersectionCell">Booking Code:</td>
                    <td class="filtersectionCell">
                        <asp:TextBox CssClass="input" ID="txtBookingCode" runat="server" Font-Size="8pt" Width="213px"></asp:TextBox></td>
                    <td class="filtersectionCell"></td>
                    <td class="filtersectionCell"></td>
                </tr>
                <tr>
                    <td class="filtersectionCell">Accom Type:</td>
                    <td class="filtersectionCell">
                        <asp:DropDownList CssClass="select" ID="ddlAccomType" runat="server"
                            Width="150px">
                        </asp:DropDownList></td>
                    <td class="filtersectionCell">Booking State:</td>
                    <td class="filtersectionCell">
                        <asp:DropDownList CssClass="select" ID="ddlBookingStatusTypes" runat="server" Width="125px">
                        </asp:DropDownList></td>
                    <td class="filtersectionCell">Agent:</td>
                    <td class="filtersectionCell">
                        <asp:DropDownList CssClass="select" ID="ddlAgent" runat="server" Width="220px">
                        </asp:DropDownList></td>
                    <td class="filtersectionCell"></td>
                    <td class="filtersectionCell">
                        <asp:Button CssClass="appbutton" ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" />

                        <button class="appbutton" id="btnExport" onclick="fnExcelReport();"> EXPORT </button>
                    </td>
                </tr>
            </table>
            <table id="LegendSection">
            <tr>
                <td style="background-color: blue; color: White; padding-left: 4px;" class="auto-style1">Proposed</td>
                <td style="background-color: Aqua; padding-left: 4px;" class="auto-style1">Booked</td>
                <td style="background-color: orange; padding-left: 4px;" class="auto-style1">Wait Listed</td>
                <td style="background-color: Lime; padding-left: 4px;" class="auto-style1">Confirmed</td>
                <td style="background-color: Red; padding-left: 4px;" class="auto-style1">Cancelled</td>
                <td style="background-color: teal; padding-left: 4px;" class="auto-style1">Chartered</td>
            </tr>
        </table>


            <%--   <div id="gridsection" style="overflow:scroll">
                        <asp:DataGrid CellPadding="4" ForeColor="#333333"
                             ID="dgBookings" runat="server"  AllowPaging="True"  OnPageIndexChanged="dgBookings_PageIndexChanged" PageSize="25" >
                            
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditItemStyle BackColor="#2461BF" />
                            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                            <AlternatingItemStyle BackColor="White" />
                            <ItemStyle BackColor="#EFF3FB" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        </asp:DataGrid>--%>
            <div style="width: 100%; overflow: scroll;max-height: 600px;width:99% ">
                <table id="tbl" >

                    <tr class="header1" id="headings" runat="server">

                        <td>Grp</td>
                        <td>EmbarkationPort</td>
                        <td>Start Date</td>

                        <td>End Date</td>
                        <td>Disembarkation Port</td>
                      

                      
                        <td>Booking Reference</td>
                        <td>Agent/FTO</td>

                        <td>Nights</td>
                        <td>Cabins</td>
                        <td>PAX</td>

                        <td style="color:red">Suite</td>
                        <td style="color:blue">SwB</td>
                        <td style="color:brown">SwoB</td>

                        <td>Status</td>

                          <td>Total Cabins</td>
                        <td>Total Pax</td>
                          <td>unit</td>

                        <td>BookingAmount</td>
                          <td>BookingCode</td>
                      

                    </tr>

                    <tr class="header1" id="headings1" runat="server">

                        <td></td>
                        <td></td>
                        <td></td>

                        <td></td>
                        <td></td>
                    

                     
                        <td></td>
                        <td></td>

                        <td></td>
                        <td></td>
                        <td></td>

                        <td style="color:red">2</td>
                        <td style="color:blue">9</td>
                        <td style="color:brown">12</td>

                        <td></td>
                          <td>23</td>
                        <td>46</td>
                            <td></td>
                        <td></td>
                           <td></td>

                      

                    </tr>

                    <%try
                      {

                          int suite = 0;
                          int swb = 0;
                          int swob = 0;
                          int pax = 0;
                          int group = 1;
                          for (int m = 0; m < ds1.Tables[0].Rows.Count; m++)
                          {



                              try
                              {
                                  
                                   
                             
                                  
                    %>





                    <% 
                  
                   
                   
                              if (m == ds1.Tables[0].Rows.Count - 1)
                              {

                                  suite += Convert.ToInt32(ds1.Tables[0].Rows[m]["Suite"]);
                                  swb += Convert.ToInt32(ds1.Tables[0].Rows[m]["SwB"]);
                                  swob += Convert.ToInt32(ds1.Tables[0].Rows[m]["SwoB"]);
                                  pax += Convert.ToInt32(ds1.Tables[0].Rows[m]["PAX"]);
                    %>
                    <tr>
                        <td></td>
                        <td><%
                                  if (ds1.Tables[0].Rows[m]["StartDate"].ToString() != "")
                                  {
                                      Response.Write(ds1.Tables[0].Rows[m]["EmbarkationPort"].ToString());
                                  }
                                    
                                    
                        %></td>
                        <td><%Response.Write(ds1.Tables[0].Rows[m]["StartDate"].ToString()); %></td>

                        <td>


                            <%
                                        
                                  if (ds1.Tables[0].Rows[m]["StartDate"].ToString() != "")
                                  {
                                      Response.Write(ds1.Tables[0].Rows[m]["EndDate"].ToString());
                                  }
                                      
                                      
                                      
                            %></td>
                        <td><%
                                    
                                  if (ds1.Tables[0].Rows[m]["StartDate"].ToString() != "")
                                  {
                                      Response.Write(ds1.Tables[0].Rows[m]["DisembarkationPort"].ToString());
                                  }
                                    
                                    
                                    
                        %></td>



                      
                        <td><%Response.Write(ds1.Tables[0].Rows[m]["BookingRef"].ToString()); %></td>
                        <td><%Response.Write(ds1.Tables[0].Rows[m]["Agent"].ToString()); %></td>

                        <td><%Response.Write(ds1.Tables[0].Rows[m]["NoOFNights"].ToString()); %></td>
                        <td><%Response.Write(ds1.Tables[0].Rows[m]["Cabins"].ToString()); %></td>
                        <td><%Response.Write(ds1.Tables[0].Rows[m]["PAX"].ToString()); %></td>

                        <td style="color:red"><%Response.Write(ds1.Tables[0].Rows[m]["Suite"].ToString()); %></td>
                        <td style="color:blue"><%Response.Write(ds1.Tables[0].Rows[m]["SwB"].ToString()); %></td>
                        <td style="color:brown"><%Response.Write(ds1.Tables[0].Rows[m]["SwoB"].ToString()); %></td>
                        <% if (ds1.Tables[0].Rows[m]["BStatus"].ToString() == "BOOKED")
                         { %>
                        <td style="background-color:Aqua"><%Response.Write(ds1.Tables[0].Rows[m]["BStatus"].ToString()); %></td>
                        <%}%>
                          
                          <%else if (ds1.Tables[0].Rows[m]["BStatus"].ToString() == "CANCELLED")
                         { %>
                        <td style="background-color:Red"><%Response.Write(ds1.Tables[0].Rows[m]["BStatus"].ToString()); %></td>
                        <%}%>
                        <% else if (ds1.Tables[0].Rows[m]["BStatus"].ToString() == "CONFIRMED")
                         { %>
                        <td style="background-color:lime"><%Response.Write(ds1.Tables[0].Rows[m]["BStatus"].ToString()); %></td>
                        <%}%>
                        <% else if (ds1.Tables[0].Rows[m]["BStatus"].ToString() == "WAITLISTED")
                         { %>
                        <td style="background-color:orange"><%Response.Write(ds1.Tables[0].Rows[m]["BStatus"].ToString()); %></td>
                        <%}%>
                           <% else if (ds1.Tables[0].Rows[m]["BStatus"].ToString() == "PROPOSED")
                         { %>
                        <td style="background-color:blue"><%Response.Write(ds1.Tables[0].Rows[m]["BStatus"].ToString()); %></td>
                        <%}%>

                        <% else 
                         { %>
                        <td><%Response.Write(ds1.Tables[0].Rows[m]["BStatus"].ToString()); %></td>
                        <%}%>
                            <td><%Response.Write((suite + swb + swob).ToString()); %></td>
                        <td><%Response.Write(pax.ToString()); %></td>
                        
                        <td><%
                                     
                                  if (ds1.Tables[0].Rows[m]["StartDate"].ToString() != "")
                                  {
                                      Response.Write(ds1.Tables[0].Rows[m]["unit"].ToString());
                                  }
                                     
                                     
                        %></td>

                    <td><%Response.Write(ds1.Tables[0].Rows[m]["BookingAmount"].ToString()); %></td>
                          <td><%Response.Write(ds1.Tables[0].Rows[m]["BookingCode"].ToString()); %></td>
                      
                    </tr>
                    <tr style="background-color:darkgray">

                        <td colspan="10" >Balance Cabins</td>
                       
                        <td style="color:red"><%Response.Write((2 - suite).ToString()); %></td>
                        <td style="color:blue"><%Response.Write((9 - swb).ToString()); %></td>
                        <td style="color:brown"><%Response.Write((12 - swob).ToString());
                                  
                                   
                        %> </td>

                        <td></td>
                         <td><%Response.Write((23- (swb + suite + swob)).ToString());
                              suite = 0;
                              swb = 0;
                              swob = 0;
                              pax = 0;
                        %></td>
                        <td></td>
                         <td></td>
                        <td></td>

                          <td></td>
                    </tr>



                    <%
                              }
                              else if (ds1.Tables[0].Rows[m + 1]["StartDate"].ToString() == "")
                              {

                                  suite += Convert.ToInt32(ds1.Tables[0].Rows[m]["Suite"]);
                                  swb += Convert.ToInt32(ds1.Tables[0].Rows[m]["SwB"]);
                                  swob += Convert.ToInt32(ds1.Tables[0].Rows[m]["SwoB"]);
                                  pax += Convert.ToInt32(ds1.Tables[0].Rows[m]["PAX"]);
                    %>
                    <tr>
                        <td><%
                            
                                      if (ds1.Tables[0].Rows[m]["StartDate"].ToString() != "")
                                      {
                                          Response.Write(group);
                                      }
                            
                            
                        %></td>
                        <td><%
                                      if (ds1.Tables[0].Rows[m]["StartDate"].ToString() != "")
                                      {
                                          Response.Write(ds1.Tables[0].Rows[m]["EmbarkationPort"].ToString());
                                      }
                                    
                                    
                        %></td>
                        <td><%Response.Write(ds1.Tables[0].Rows[m]["StartDate"].ToString()); %></td>

                        <td>


                            <%
                                        
                                      if (ds1.Tables[0].Rows[m]["StartDate"].ToString() != "")
                                      {
                                          Response.Write(ds1.Tables[0].Rows[m]["EndDate"].ToString());
                                      }
                                      
                                      
                                      
                            %></td>
                        <td><%
                                    
                                      if (ds1.Tables[0].Rows[m]["StartDate"].ToString() != "")
                                      {
                                          Response.Write(ds1.Tables[0].Rows[m]["DisembarkationPort"].ToString());
                                      }
                                    
                                    
                                    
                        %></td>


                       

                      
                        <td><%Response.Write(ds1.Tables[0].Rows[m]["BookingRef"].ToString()); %></td>
                        <td><%Response.Write(ds1.Tables[0].Rows[m]["Agent"].ToString()); %></td>

                        <td><%Response.Write(ds1.Tables[0].Rows[m]["NoOFNights"].ToString()); %></td>
                        <td><%Response.Write(ds1.Tables[0].Rows[m]["Cabins"].ToString()); %></td>
                        <td><%Response.Write(ds1.Tables[0].Rows[m]["PAX"].ToString()); %></td>

                        <td style="color:red"><%Response.Write(ds1.Tables[0].Rows[m]["Suite"].ToString()); %></td>
                        <td style="color:blue"><%Response.Write(ds1.Tables[0].Rows[m]["SwB"].ToString()); %></td>
                        <td style="color:brown"><%Response.Write(ds1.Tables[0].Rows[m]["SwoB"].ToString()); %></td>

                        <% if (ds1.Tables[0].Rows[m]["BStatus"].ToString() == "BOOKED")
                         { %>
                        <td style="background-color:Aqua"><%Response.Write(ds1.Tables[0].Rows[m]["BStatus"].ToString()); %></td>
                        <%}%>
                          
                          <%else if (ds1.Tables[0].Rows[m]["BStatus"].ToString() == "CANCELLED")
                         { %>
                        <td style="background-color:Red"><%Response.Write(ds1.Tables[0].Rows[m]["BStatus"].ToString()); %></td>
                        <%}%>
                        <% else if (ds1.Tables[0].Rows[m]["BStatus"].ToString() == "CONFIRMED")
                         { %>
                        <td style="background-color:lime"><%Response.Write(ds1.Tables[0].Rows[m]["BStatus"].ToString()); %></td>
                        <%}%>
                        <% else if (ds1.Tables[0].Rows[m]["BStatus"].ToString() == "WAITLISTED")
                         { %>
                        <td style="background-color:orange"><%Response.Write(ds1.Tables[0].Rows[m]["BStatus"].ToString()); %></td>
                        <%}%>
                           <% else if (ds1.Tables[0].Rows[m]["BStatus"].ToString() == "PROPOSED")
                         { %>
                        <td style="background-color:blue"><%Response.Write(ds1.Tables[0].Rows[m]["BStatus"].ToString()); %></td>
                        <%}%>

                        <% else 
                         { %>
                        <td><%Response.Write(ds1.Tables[0].Rows[m]["BStatus"].ToString()); %></td>
                        <%}%>
                      
                        <td></td>
                         <td></td>
                         <td><%
                                     
                                      if (ds1.Tables[0].Rows[m]["StartDate"].ToString() != "")
                                      {
                                          Response.Write(ds1.Tables[0].Rows[m]["unit"].ToString());
                                      }
                                     
                                     
                        %></td>

                          <td><%Response.Write(ds1.Tables[0].Rows[m]["BookingAmount"].ToString()); %></td>
                         <td><%Response.Write(ds1.Tables[0].Rows[m]["BookingCode"].ToString()); %></td>
                    </tr>
                    <%
                                  }
                              else
                              {


                                  suite += Convert.ToInt32(ds1.Tables[0].Rows[m]["Suite"]);
                                  swb += Convert.ToInt32(ds1.Tables[0].Rows[m]["SwB"]);
                                  swob += Convert.ToInt32(ds1.Tables[0].Rows[m]["SwoB"]);
                                  pax += Convert.ToInt32(ds1.Tables[0].Rows[m]["PAX"]);
                    %>
                    <tr>
                        <td><%
                            
                       if (ds1.Tables[0].Rows[m]["StartDate"].ToString() != "")
                       {
                           Response.Write(group.ToString());
                       }
                            
                            
                            
                        %></td>
                        <td><%
                       if (ds1.Tables[0].Rows[m]["StartDate"].ToString() != "")
                       {

                           Response.Write(ds1.Tables[0].Rows[m]["EmbarkationPort"].ToString());
                       }
                                    
                                    
                        %></td>
                        <td><%Response.Write(ds1.Tables[0].Rows[m]["StartDate"].ToString()); %></td>

                        <td><%
                                        
                                        
                                        
                       if (ds1.Tables[0].Rows[m]["StartDate"].ToString() != "")
                       {
                           Response.Write(ds1.Tables[0].Rows[m]["EndDate"].ToString());
                       }
                                        
                                        
                                        
                        %></td>
                        <td><%
                       if (ds1.Tables[0].Rows[m]["StartDate"].ToString() != "")
                       {

                           Response.Write(ds1.Tables[0].Rows[m]["DisembarkationPort"].ToString());
                       }
                                    
                                    
                        %></td>
                      

                      
                        <td><%Response.Write(ds1.Tables[0].Rows[m]["BookingRef"].ToString()); %></td>
                        <td><%Response.Write(ds1.Tables[0].Rows[m]["Agent"].ToString()); %></td>

                        <td><%Response.Write(ds1.Tables[0].Rows[m]["NoOFNights"].ToString()); %></td>
                        <td><%Response.Write(ds1.Tables[0].Rows[m]["Cabins"].ToString()); %></td>
                        <td><%Response.Write(ds1.Tables[0].Rows[m]["PAX"].ToString()); %></td>

                        <td style="color:red"><%Response.Write(ds1.Tables[0].Rows[m]["Suite"].ToString()); %></td>
                        <td style="color:blue"><%Response.Write(ds1.Tables[0].Rows[m]["SwB"].ToString()); %></td>
                        <td style="color:brown"><%Response.Write(ds1.Tables[0].Rows[m]["SwoB"].ToString()); %></td>

                        <% if (ds1.Tables[0].Rows[m]["BStatus"].ToString() == "BOOKED")
                         { %>
                        <td style="background-color:Aqua"><%Response.Write(ds1.Tables[0].Rows[m]["BStatus"].ToString()); %></td>
                        <%}%>
                          
                          <%else if (ds1.Tables[0].Rows[m]["BStatus"].ToString() == "CANCELLED")
                         { %>
                        <td style="background-color:Red"><%Response.Write(ds1.Tables[0].Rows[m]["BStatus"].ToString()); %></td>
                        <%}%>
                        <% else if (ds1.Tables[0].Rows[m]["BStatus"].ToString() == "CONFIRMED")
                         { %>
                        <td style="background-color:lime"><%Response.Write(ds1.Tables[0].Rows[m]["BStatus"].ToString()); %></td>
                        <%}%>
                        <% else if (ds1.Tables[0].Rows[m]["BStatus"].ToString() == "WAITLISTED")
                         { %>
                        <td style="background-color:orange"><%Response.Write(ds1.Tables[0].Rows[m]["BStatus"].ToString()); %></td>
                        <%}%>
                           <% else if (ds1.Tables[0].Rows[m]["BStatus"].ToString() == "PROPOSED")
                         { %>
                        <td style="background-color:blue"><%Response.Write(ds1.Tables[0].Rows[m]["BStatus"].ToString()); %></td>
                        <%}%>

                        <% else 
                         { %>
                        <td><%Response.Write(ds1.Tables[0].Rows[m]["BStatus"].ToString()); %></td>
                        <%}%>
                    
                        <td><%Response.Write((suite + swb + swob).ToString()); %></td>

                        
                        <td><%Response.Write(pax.ToString()); %></td>
                          <td><%
                                     
                       if (ds1.Tables[0].Rows[m]["StartDate"].ToString() != "")
                       {
                           Response.Write(ds1.Tables[0].Rows[m]["unit"].ToString());
                       }
                                     
                                     
                        %></td>
                            <td><%Response.Write(ds1.Tables[0].Rows[m]["BookingAmount"].ToString()); %></td>
                          <td><%Response.Write(ds1.Tables[0].Rows[m]["BookingCode"].ToString()); %></td>
                    </tr>



                    <tr style="background-color:darkgray">

                        <td colspan="10">Balance Cabins</td>
                        

                        <td style="color:red"><%Response.Write((2 - suite).ToString()); %></td>
                        <td style="color:blue"><%Response.Write((9 - swb).ToString()); %></td>
                        <td style="color:brown"><%Response.Write((12 - swob).ToString());
                                  
                                   
                        %> </td>

                        <td></td>

                          <td><%Response.Write((23 - (swb + suite + swob)).ToString());
                              suite = 0;
                              swb = 0;
                              swob = 0;
                              pax = 0;
                        %></td>
                        <td></td>
                         <td></td>
                        <td></td>
                         <td></td>
                    </tr>



                    <%   group++;
                   }
                    %>




                    <%   
                             
                          }
                          catch
                          {
                          }
                      }
                  }
                      catch
                      {
                      }
                          
                          
                         
                    %>
                </table>

            </div>
            <%--   <% Response.Write("</table>"); %>  --%>
        </div>
        






        </div>
        
    </form>
</body>
</html>
