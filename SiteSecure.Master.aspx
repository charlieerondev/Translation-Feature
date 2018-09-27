<%@ Master Language="C#" AutoEventWireup="true" CodeBehind="SiteSecure.master.cs" Inherits="Portal.SiteSecure" %>
    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Avesis</title>
        <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <link runat="server" id="favicon" rel="shortcut icon" type="image/x-icon" href="#" />
        <script src="/assets/js/dependencies.min.js"></script>
        <link href="/assets/css/dependencies.min.css" rel="stylesheet" />
        <link id="styleSheet" runat="server" rel="stylesheet" type="text/css" />
        <asp:ContentPlaceHolder ID="head" runat="server">
        </asp:ContentPlaceHolder>
    </head>
    <body>
        <form id="form1" runat="server">
            <asp:ContentPlaceHolder ID="MainContent" runat="server" />
        </form>
        <script src="/assets/js/main.js"></script>
        <asp:ContentPlaceHolder ID="ScriptsContent" runat="server" />
    </body>
    </html>