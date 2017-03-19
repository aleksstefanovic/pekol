<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="Pekol.LogIn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="styles/login.css" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanelLogin" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="login" class="section">
                <asp:Login ID="Login1" runat="server" OnAuthenticate="Login1_Authenticate" ></asp:Login>
                <asp:Button ID="CreateUser" runat="server" Text="Create New User" CssClass="createUser" OnClick="CreateUser_Click"/>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
