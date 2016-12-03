<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerMaster.aspx.cs" Inherits="Cruise_Masters_CustomerMaster" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta http-equiv="Content-Type" content="text/html; charset=UTF-8"><meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1"><meta name="author" content="Pandaw Cruises Ltd">
 
    <link rel="icon" type="image/png" href="/favicon.ico"><link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/css/bootstrap.min.css" rel="stylesheet" type="text/css"><link href="css/style.css" rel="stylesheet"><link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.0.3/css/font-awesome.min.css" rel="stylesheet" type="text/css">

    <script async="" src="//www.google-analytics.com/analytics.js"></script><script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/1.11.3/jquery.min.js" type="text/javascript"></script>   
    <script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/js/bootstrap.min.js" type="text/javascript"></script>   
    <script src="js/QueryString.js" type="text/javascript"></script>
    
    

    <script src="js/pandaw.js" type="text/javascript"></script>

    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

    <!-- Google Analytics -->
    <script type="text/javascript">
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
            m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-1319647-1', 'auto');
        ga('send', 'pageview');

    </script>
    <!-- End Google Analytics -->

    

    <title>Pandaw.com - Register for a Pandaw Account</title>

    <meta name="description" content="Register your details in order to make a booking and manage your account.">

    <script src="js/register.js"></script>



    <!--ZOOMSTOP-->

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <section>

                <div class="row">

                    <div class="col-md-12 text-center topbotPadding">

                        <h2 class="goldLine"><span>NEW USER REGISTER</span></h2>

                    </div>

                    <div class="col-md-12 text-left noTopPadding">

                        <div class="insideSkin bottomPadding30">

                            <p></p>

                        </div>

                    </div>

                </div>

            </section>
        <asp:UpdatePanel ID="upd1" runat="server">
            <ContentTemplate>


    <div id="formTop" class="row noSideMargin backgroundPaperDark innerDropShadow topbotPadding">

                    <div class="col-xs-12">

                        <div class="insideSkin">
                    
                            <div id="ContentPlaceHolder1_MessageErr" class="alert alert-danger alert-dismissable hide">
                                <i class="fa fa-warning"></i>
                                <b>MESSAGE ERROR</b> Please correct the below error with sending your message:
                                <ul>
                                    <li><span id="ContentPlaceHolder1_ErrorMsg"></span></li>
                                </ul>
                            </div>

                        </div>

                    </div>
                    
                    <div class="col-md-12 text-left">

                        <div class="insideSkin">
                        
                            <div class="col-md-12 noSidePadding">
                            
                                <div class="col-md-6">

                                    <div class="form-group">

                                        <label for="ddlTitle" class="control-label">Title</label>

                                        <asp:DropDownList ID="ddltitle" runat="server" class="form-control">
                                            <asp:ListItem></asp:ListItem>
                                             <asp:ListItem>Mr</asp:ListItem>
                                             <asp:ListItem>Mrs</asp:ListItem>
                                             <asp:ListItem>Miss</asp:ListItem>
                                             <asp:ListItem>Ms</asp:ListItem>

                                        </asp:DropDownList>
                                        
	

</select>

                                    </div>

                                </div>

                                <div class="col-md-6"></div>

                            </div>
                            
                            <div class="col-md-12 noSidePadding">
                            
                                <div class="col-md-6">

                                    <div class="form-group">

                                        <label for="txtName" class="control-label">Firstname</label>
                                      
                                        <asp:TextBox ID="txtFirstName" runat="server" class="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtFirstName" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                    </div>

                                </div>

                                <div class="col-md-6">

                                    <div class="form-group">

                                        <label for="txtName" class="control-label">Lastname</label>
                                          <asp:TextBox ID="txtLastName" runat="server" class="form-control"></asp:TextBox>
                                       

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtLastName" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                       

                                    </div>

                                </div>

                            </div>

                            <div class="col-md-12 noSidePadding">

                                <div class="col-md-6">

                                    <div class="form-group">

                                        <label for="txtEmailAddress" class="control-label">Email Address</label>
                                           <asp:TextBox ID="txtMailAddress" runat="server" class="form-control" OnTextChanged="txtMailAddress_TextChanged" AutoPostBack="True"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtMailAddress" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                    </div>

                                </div>

                                <div class="col-md-6">

                                    <div class="form-group">

                                        <label for="txtEmailAddress" class="control-label">Telephone</label>
                                        <asp:TextBox ID="txtTelephone" runat="server" class="form-control"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtTelephone" ValidationGroup="Cust"></asp:RequiredFieldValidator>

                                    </div>

                                </div>

                            </div>

                              <div class="col-md-12 noSidePadding">

                                   <div class="col-md-6">

                                    <div class="form-group">

                                        <label for="txtAddress1" class="control-label">Address 1</label>

                                          <asp:TextBox ID="txtAddress1" runat="server" class="form-control"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtAddress1" ValidationGroup="Cust"></asp:RequiredFieldValidator>

                                    </div>

                                </div>

                                <div class="col-md-6">

                                    <div class="form-group">

                                        <label for="txtAddress2" class="control-label">Address 2</label>
                                         <asp:TextBox ID="txtaddress2" runat="server" class="form-control"></asp:TextBox>

                                    </div>

                                </div>
                                  </div>


                           
                            <div id="ContentPlaceHolder1_addressField" class="">
                        
                               

                                <div class="col-md-6">

                                    <div class="form-group">

                                        <label for="txtCity" class="control-label">City</label>
                                       <asp:TextBox ID="txtCity" runat="server" class="form-control"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtCity" ValidationGroup="Cust"></asp:RequiredFieldValidator>

                                    </div>

                                </div>

                                <div class="col-md-6">

                                    <div class="form-group">

                                        <label for="txtState" class="control-label">State</label>
                                        <asp:TextBox ID="txtState" runat="server" class="form-control"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtState" ValidationGroup="Cust"></asp:RequiredFieldValidator>

                                    </div>

                                </div>

                                <div class="col-md-6">

                                    <div class="form-group">

                                        <label for="txtPostcode" class="control-label">Postcode</label>
                                         <asp:TextBox ID="txtPostcode" runat="server" class="form-control"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtPostcode" ValidationGroup="Cust"></asp:RequiredFieldValidator>

                                    </div>

                                </div>

                                <div class="col-md-6">

                                    <div class="form-group">

                                        <label for="ddlCountry" class="control-label">Country</label>
                                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" ></asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlCountry" InitialValue="0" ValidationGroup="Cust"></asp:RequiredFieldValidator>

                                    </div>

                                </div>

                                <div class="col-md-12 topbotPadding">

                                    <div class="form-group">

                                        <p class="text-right">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info font16" OnClick="btnSubmit_Click" ValidationGroup="Cust" /></p>

                                    </div>

                                </div>

                            </div>

                        </div>

                    </div>
                    
                    <div class="puller"></div>

                </div>

                
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
