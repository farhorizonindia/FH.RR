<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DiscountEntry.aspx.cs" Inherits="MasterUI_DiscountEntry" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    <title>Discount Master</title>

    <style>
        .controlsCss {
            Width: 161px;
        }

        .auto-style1 {
            height: 24px;
        }

        .auto-style2 {
            height: 19px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Discount Master" />
        <div style="margin-left: 21%; margin-top: 1%;">
            <table>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlPackage" runat="server"></asp:DropDownList></td>
                    <td>
                        <asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList></td>
                    <td>
                        <asp:DropDownList ID="ddlMonth" runat="server"></asp:DropDownList></td>
                    <td>
                        <asp:Button ID="btnSelect" runat="server" Text="Select" Width="114px" OnClick="btnSelect_Click" /></td>
                </tr>
            </table>
        </div>
        <br />
        <div style="width: 63%; margin-left: 18%;">

            <asp:GridView ID="gvPaymentEntryInfo" runat="server" AutoGenerateColumns="false"
                EmptyDataText="No Data" Width="90%" ForeColor="#333333" BorderStyle="Ridge"
                PageSize="20" OnRowCommand="gvPaymentEntryInfo_RowCommand">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <AlternatingRowStyle BackColor="White" />
                <RowStyle BackColor="#EFF3FB" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Boarding Date" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblBoardingDate" runat="server" Text='<%#Eval("Boarding Date") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                        <HeaderStyle></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="De-Boarding Date" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblDeBoardingDate" runat="server" Text='<%#Eval("De-Boarding Date") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                        <HeaderStyle></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nights" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblNight" runat="server" Text='<%#Eval("Nights") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                        <HeaderStyle></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Price" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblPrice" runat="server" Text='<%#Eval("Price") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                        <HeaderStyle></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Availability" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblAvailbility" runat="server" Text='<%#Eval("Availability") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotalDueAmount" runat="server" Text=""></asp:Label>
                        </FooterTemplate>
                        <HeaderStyle></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Discount %" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lbldiscountpercentage" runat="server" Text='<%#Eval("Discount %") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotalDueAmount" runat="server" Text=""></asp:Label>
                        </FooterTemplate>
                        <HeaderStyle></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Last Minutes Sale %" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblLastminutes" runat="server" Text='<%#Eval("LastMinutessale %") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotalDueAmount" runat="server" Text=""></asp:Label>
                        </FooterTemplate>
                        <HeaderStyle></HeaderStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Enter Discount %">
                        <ItemTemplate>
                            <asp:TextBox ID="txtreceive" Width="98%" runat="server"></asp:TextBox>
                        </ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                        <HeaderStyle></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Last Minute Discount %" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="txtlastminute" runat="server"></asp:TextBox>
                        </ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                        <HeaderStyle></HeaderStyle>
                    </asp:TemplateField>
                    <%--     <asp:TemplateField HeaderStyle-BackColor="#2FBDF1">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkOpen" runat="server" Text='<%#Eval("openclose") %>'  CommandName='<%#Eval("DepurtureId") %>'   CommandArgument='<%#Eval("Id") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                        <HeaderStyle BackColor="#2FBDF1"></HeaderStyle>
                    </asp:TemplateField>--%>
                </Columns>

            </asp:GridView>
        </div>
        <div style="margin-left: 35%; margin-top: 1%;">
            <asp:Button ID="btnSave" runat="server" Width="112px" Visible="false" Text="Save" OnClick="btnSave_Click" />
        </div>
        <br />
        <br />
    </form>
</body>
</html>
