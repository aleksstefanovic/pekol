<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CodingBestPractice.aspx.cs" Inherits="Pekol.CodingBestPractice" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="styles/codingbestpractice.css" type="text/css" />
    <div id="tipstratgy" class="section">
        <div class="peckolHeader">
            Peckol Prevention Tips
        </div>
        <div class="peckolText">
            Often the best way to prevent an unfortunate encounter with Doctor Peckol is to follow best coding practice. Here are the most important ones to follow.
        </div>
        <ul id="tips">
            <li class="peckolText">All because a variable is "Goatblower" doesn't mean you should name it "Goatblower". Name it something that represents what its used for!</li>
            <li class="peckolText">Don't cast, don't do it. There's libraries that do it better. Don't cast.</li>
            <li class="peckolText">If you are javaing and you're overriding the finalize method, you're javaing wrong.</li>
        </ul>

        <div class="peckolHeader">
            Peckol Code Test
        </div>
        <div class="peckolText">
            This is our patented Pekol certified code analyzer tool. Just upload some code you have and we'll tell you whether Peckol will break your fingers or not!
        </div>
        <asp:RadioButtonList ID="languageList" runat="server" CssClass="radialButtons">
            <asp:ListItem Text="Javascript"></asp:ListItem>
            <asp:ListItem Text="C#"></asp:ListItem>
            <asp:ListItem Text="Java"></asp:ListItem>
        </asp:RadioButtonList>
        <asp:FileUpload ID="codeUpload" runat="server" CssClass="fileUpload"/>
        <asp:Button ID="submitBtn" runat="server" Text="Submit" OnClick="submitBtn_Click" CssClass="codeButton"/>
    </div>
</asp:Content>
