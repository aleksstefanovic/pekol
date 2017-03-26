<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Shop.aspx.cs" Inherits="Pekol.Shop" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="styles/shop.css" type="text/css" />
    <div id="store" class="section">
        <div id="title" class="peckolHeader">Store</div>

        <div id="defensiveGloves" class="peckolHeader">Defensive Gloves</div>
        <div class="peckolText">These special gloves made out of a extremely durable super nano-carbon material are almost indestructable! If you're determined to do bad code practice, but don't want your fingers broken, this the product for you!</div>
        <asp:Button ID="addGloves" runat="server" Text="Add to Cart" CssClass="shopButton" OnClick="addGloves_Click"/>

        <div id="peckolPreventionHandbook" class="peckolHeader">Peckol Prevention Handbook</div>
        <div class="peckolText">Written by our experts here at the Peckol Prevention Project, it contains everything you need to know about Doctor Peckol in an easy to carry easy to read format!</div>
        <asp:Button ID="addBook" runat="server" Text="Add to Cart" CssClass="shopButton" OnClick="addBook_Click"/>


        <div id="antiPeckolDisguise" class="peckolHeader">Anti Peckol Disguise</div>
        <div class="peckolText">Our patented Anti Peckol Disguise is designed to ensure that you don't look like a programmer! You'll look so unlike any programmer ever in existence that Doctor Peckol will never guess that you were the one to perform bad coding practice!</div>
        <asp:Button ID="addDisguise" runat="server" Text="Add to Cart" CssClass="shopButton" OnClick="addDisguise_Click"/>

        <div id="peckolRepellent" class="peckolHeader">Peckol Repellent</div>
        <div class="peckolText">You take it, and spray it at him! It smells so bad its sure to keep him away! **WARNING** The horrid smell may also repel other human beings</div>
        <asp:Button ID="addRepel" runat="server" Text="Add to Cart" CssClass="shopButton" OnClick="addRepel_Click"/>
        <br />
        <br />

        <asp:Button ID="checkout" runat="server" Text="Checkout" CssClass="shopButton checkoutButton" OnClick="checkout_Click"/>
        <br />
        <br />
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>
