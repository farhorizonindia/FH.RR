<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Menu.aspx.cs" Inherits="MasterUI_Menu" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />    
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />    
    <title>Master Menu</title>
</head>
<body>    
    <form id="form1" runat="server">
        <phc:PageHeaderControl id="pageheader1" runat="server" PageTitle="Master Menu" />
        <table id="linkssection" class="linkssection" style="margin-left:5px">
        <tr>
           <td align="left">
            <a href="RegionMaster.aspx">Regions</a></td>
        </tr>
        <tr>
           <td align="left">
               <a href="CityMaster.aspx">Cities</a></td>
        </tr>
        <tr>
        <td align="left">
            <a href="AccomTypeMaster.aspx">Accomodation Type</a></td>
        </tr>
        <tr>
        <td align="left">
            <a href="AccomMaster.aspx">Accomodations</a></td>
        </tr>
        <tr>
           <td align="left">
               <a href="AccomWiseActivitiesMaster.aspx">Accomodation Wise Activities</a></td>
        </tr>
        <tr>
           <td align="left">
            <a href="floormaster.aspx">Floors</a></td>
        </tr>
        <tr>
           <td align="left">
             <a href ="RoomCategoryMaster.aspx">Room Category</a></td>
        </tr>
        <tr>
        <td align="left">
            <a href="RoomTypeMaster.aspx">Room Type</a></td>
        </tr>

        <tr>
        <td align="left">
            <a href="RoomMaster.aspx">Rooms</a></td>
        </tr>
        <tr>
        <td align="left">
            <a href="AgentMaster.aspx">Agents</a></td>
        </tr>
        <tr>
        <td align="left">
            <a href="MealPlanMaster.aspx">Meal Plans</a></td>
        </tr>
        <tr>
        <td align="left">
            <a href="NationalityMaster.aspx">Nationality</a></td>
        </tr>                           
        <tr>
           <td align="left">
               <a href="ChangeTreeType.aspx">Change Booking Tree Type</a></td>
        </tr>
        </table>    
    </form>
</body>
</html>
