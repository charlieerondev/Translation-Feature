# Translate Feature Demo

This is a stripped-down version of a feature I built within a portal. This feature translates items on the front-end which are tagged with a class of "e-s". This also works for elements rendered after Page_Load.

## Components

2 processes:

* [JavaScript in /assets/js/main.js](assets/js/main.js) -> [/WebMethods.aspx.cs logic](WebMethods.aspx.cs) -> [SiteConfig logic in /UICode/](UICode/)
* [Control loop in /SiteSecure.Master.cs](SiteSecure.Master.cs) -> [SiteConfig logic in /UICode/](UICode/) (for ASP elements: <asp:ListItem>, etc.)

### JavaScript -> WebMethods -> SiteConfig

On $(document).ready(), we create an array of all elements tagged with the "e-s" class and assign them unique IDs. This array is stringified and sent via AJAX to WebMethods, where it loops through all elements in the array. It runs all items through the SiteConfig logic, sends each value back to WebMethods, then WebMethods returns the modified list to the front-end.

```
<div class="e-s">Translate me!</div>
<div>Don't translate me!</div>
```

### Control loop in /SiteSecure.Master.cs -> SiteConfig

This process previously handled all items on the page. However, it was a heavy-handed approach, and later refactored using JavaScript. It is still necessary for handling certain ASP elements.
On Page_load, we loop through all items on the page. If it's one of the types we're handling, it runs it through the SiteConfig logic, and (if it's able to retrieve a value) sets the text value right then and there.

```
<asp:DropDownList ID="ddlSomeList" runat="server">
    <asp:ListItem Value="translateme" Text="Translate Me" />
</asp:DropDownList>
```

## Authors

* **Charlie Eron**
