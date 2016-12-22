<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HotelInvoice.aspx.cs" Inherits="Hotel_HotelInvoice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 406px">
                <tr>
                    <td>
                        <asp:Label ID="lblacm" runat="server" Text="lblAccomName"></asp:Label>
                    </td>
                    <td></td>

                </tr>
                <tr>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Arrival Date</td>
                    <td>
                        <asp:Label ID="lblArrvDate" runat="server"></asp:Label>
                    </td>

                </tr>

                <tr>
                    <td>Departure Date</td>
                    <td>
                        <asp:Label ID="lblDepartDate" runat="server"></asp:Label>
                    </td>


                </tr>

            </table>
            <div style="width: 30px"></div>

         
<asp:GridView ID="gdvSelectedRooms" runat="server" Width="50%" AutoGenerateColumns="False" Font-Size="Medium" style="width:100%" OnRowDataBound="gdvSelectedRooms_RowDataBound" >
            <Columns>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblRoomCatId" runat="server" Text='<%# Bind("RoomCategoryId") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               <asp:BoundField DataField="Rooms" HeaderText="Rooms" />
                <asp:BoundField DataField="categoryName" HeaderText="Room Name" />
                <asp:BoundField DataField="Pax" HeaderText="Guests" />
                <asp:BoundField DataField="Price" HeaderText="Rate" />
             
                <asp:BoundField DataField="Nights" HeaderText="Nights" />
                <asp:BoundField DataField="Total" HeaderText="Total" />
                <asp:BoundField  HeaderText="Tax(%)" />
                <asp:BoundField  HeaderText="Tax Amount" />
            </Columns>
            <HeaderStyle BackColor="#CCCCCC" />
        </asp:GridView>
       
            <hr />
            <table>
                <tr>
                    <td>Total Amount</td>
                    <td></td>
                    <td>
                        <asp:Label ID="lblTotoAmt" runat="server"></asp:Label></td>

                </tr>
            </table>
            <hr />
            <table>
                <tr>
                    <td>Total Balance</td>
                    <td></td>
                    <td>
                        <asp:Label ID="lblBalance" runat="server"></asp:Label></td>

                </tr>
            </table>


        </div>
         <p>
        Valid With Computer Print Only</p>
        <p>Thanks For Choosing <asp:Label ID="lblAccomName" runat="server"></asp:Label></p>
    </form>
   
</body>
</html>
