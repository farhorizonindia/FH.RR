<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgentInfo.aspx.cs" Inherits="Cruise_Masters_AgentInfo" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link rel="stylesheet" type="text/css" media="all" href="../../css/pageheader.css" />    
    <link rel="stylesheet" type="text/css" media="all" href="../../style.css" />   
    
    <title>Agent Details</title>
</head>
<body>
    <form id="form1" runat="server">
         <phc:PageHeaderControl id="pageheader1" runat="server" PageTitle="Agent Master" />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="upd1" runat="server">
            <ContentTemplate>

        
        <div>
            <table>

                <tr>

                    <td>Select Market</td>
                    <td>      <asp:DropDownList ID="ddlMarket" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMarket_SelectedIndexChanged"></asp:DropDownList></td>
                </tr>
            </table>
      </div>

        <div>
            <asp:GridView ID="dgAgents" runat="server" AutoGenerateColumns="False" CellPadding="4"
                DataKeyNames="AgentID" ForeColor="#333333" GridLines="None"
                Width="525px" BorderStyle="Ridge" CellSpacing="5">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <AlternatingRowStyle BackColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:BoundField DataField="AgentName" HeaderText="Agent Name"></asp:BoundField>
                    <asp:BoundField DataField="AgentCode" HeaderText="Agent Code"></asp:BoundField>
                    <asp:BoundField DataField="AgentEmailId" HeaderText="AgentEmailId"></asp:BoundField>
                    <asp:BoundField DataField="oncredit" HeaderText="On Credit"></asp:BoundField>
                    <asp:BoundField DataField="CreditLimit" HeaderText="Credit Limit" />
                    <asp:BoundField DataField="RateCategory" HeaderText="RateCategory"></asp:BoundField>
                    <asp:BoundField DataField="Market" HeaderText="Market"></asp:BoundField>
                    <asp:BoundField DataField="MarketMappedto" HeaderText="MarketMappedto"></asp:BoundField>



                </Columns>
            </asp:GridView>
        </div>

    </ContentTemplate>

        </asp:UpdatePanel>
    </form>
</body>
</html>
