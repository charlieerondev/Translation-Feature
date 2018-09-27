<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../SiteSecure.Master" CodeBehind="Index.aspx.cs" Inherits="Portal.Secure.Index" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <title class="e-s"></title>
    </asp:Content>

    <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="container">
            <div class="jumbotron">
                <h1>Translate Feature</h1>
                <p>This is a stripped-down demo of a translation feature, created in 2018 by Charlie Eron.</p>
            </div>
            <p class="e-s">This text would be targeted for translation.</p>
            <p>This text would NOT be targeted for translation.</p>
            <div class="alert alert-info e-s htmlBlock">
                <h2>This entire div.alert block would be targeted for translation, and would be returned from the database as a string of HTML.</h2>
                <small>I'm also getting translated!</small>
            </p>
        </div>
    </asp:Content>
    <asp:Content runat="server" ContentPlaceHolderID="ScriptsContent">
    </asp:Content>