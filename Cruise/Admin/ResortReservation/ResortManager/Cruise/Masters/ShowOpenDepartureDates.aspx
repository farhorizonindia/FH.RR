<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowOpenDepartureDates.aspx.cs" Inherits="Cruise_ShowOpenDepartureDates" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="../../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../../style.css" />
    <link href="../../css/roleRights.css" rel="stylesheet" type="text/css" />
    <title>Open Dates Master</title>
    <style>
        .controlsCss
        {
            Width: 161px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Cruise Open Departure Dates" />
        <div>
            <asp:ScriptManager ID="scmgrLocation" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="updatepanel1" runat="server">
                <ContentTemplate>
                    <div id="gridsection" class="gridsection">
                        <asp:GridView ID="GridOpenDates" ForeColor="#333333" AutoGenerateColumns="false" BorderStyle="Ridge" GridLines="Both" runat="server" Width="950px">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingRowStyle BackColor="White" />
                            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sn.">
                                    <ItemTemplate><%#Container.DataItemIndex+1 %> </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="AccomName">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdfnOpenDateId" runat="server" Value='<%#Eval("Id") %>' />
                                        <asp:HiddenField ID="hdnfAccomId" runat="server" Value='<%#Eval("AccomId") %>' />
                                        <asp:Label runat="server" ID="lbAccomName" Text='<%#Eval("AccomName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="River">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnfRiverId" runat="server" Value='<%#Eval("RiverId") %>' />
                                        <asp:Label runat="server" ID="lbRiverName" Text='<%#Eval("RiverName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Country">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnfCountryName" runat="server" Value='<%#Eval("CountryId") %>' />
                                        <asp:Label runat="server" ID="lbCountryName" Text='<%#Eval("CountryName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Boarding Loc.">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnfBoardingFrom" runat="server" Value='<%#Eval("BoardingFrom") %>' />
                                        <asp:Label runat="server" ID="lbBoardingFrom" Text='<%#Eval("BoardingFrom1") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deboarding Loc.">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnfBoardingTo" runat="server" Value='<%#Eval("BoardingTo") %>' />
                                        <asp:Label runat="server" ID="lbBoardingTo" Text='<%#Eval("BoardingTo1") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Boarding Date">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbBoardingDate" Text='<%#Eval("BoardingDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deboarding Date">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbDeboardingDate" Text='<%#Eval("DeboardingDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cruise Direction">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbCruiseDirection" Text='<%#Eval("CruiseDirection") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
