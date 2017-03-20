<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="Pekol.LogIn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="styles/login.css" type="text/css" />
    <div id="login" class="section">
        <asp:Login ID="Login1" runat="server" OnAuthenticate="Login1_Authenticate" ></asp:Login>
        <asp:Button ID="CreateUser" runat="server" Text="Create New User" CssClass="createUser" OnClick="CreateUser_Click"/>
        <div id="userPage" runat="server">
            <div id="title" class="peckolHeader">User Profile</div>
            <asp:Label ID="lblUserName" runat="server" Text="User Name:"></asp:Label>
            <asp:TextBox ID="userName" runat="server" Text="" CssClass="inputText"></asp:TextBox>
            <br />

            <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>
            <asp:TextBox ID="password" runat="server" Text="" CssClass="inputText"></asp:TextBox>
            <br />

            <asp:Label ID="lblAddress" runat="server" Text="Address:"></asp:Label>
            <asp:TextBox ID="address" runat="server" Text="" CssClass="inputText"></asp:TextBox>

            <asp:Label ID="lblPostalCode" runat="server" Text="Postal Code:"></asp:Label>
            <asp:TextBox ID="postalCode" runat="server" Text="" CssClass="inputText"></asp:TextBox>
            <br />

            <asp:Label ID="lblPhoneNumber" runat="server" Text="Phone Number:"></asp:Label>
            <asp:TextBox ID="phoneNumber" runat="server" Text="" CssClass="inputText"></asp:TextBox>
            <br />

            <asp:Label ID="lblCreditCard" runat="server" Text="Credit Card Number:"></asp:Label>
            <asp:TextBox ID="creditCard" runat="server" Text="" CssClass="inputText"></asp:TextBox>

            <asp:Label ID="lblCVV" runat="server" Text="CVV:"></asp:Label>
            <asp:TextBox ID="cvv" runat="server" Text="" CssClass="inputText"></asp:TextBox>

            <asp:Label ID="lblExpDate" runat="server" Text="Expiry Date:"></asp:Label>
            <asp:TextBox ID="expiryDate" runat="server" Text="" CssClass="inputText"></asp:TextBox>
            <br />

            <asp:Button ID="saveUser" runat="server" Text="Save" />


            <asp:RegularExpressionValidator ID="regexUserName" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="userName" ForeColor="Red" ValidationExpression="^[a-zA-Z0-9_]*$">* You must enter a valid user name</asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="reqUserName" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="userName" ForeColor="Red">* You must enter a user name</asp:RequiredFieldValidator>

            <asp:RegularExpressionValidator ID="regexPassword" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="password" ForeColor="Red" ValidationExpression="^([a-zA-Z0-9_])*">* You must enter a valid password</asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="reqPassword" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="password" ForeColor="Red">* You must enter a valid password</asp:RequiredFieldValidator>

            <asp:RegularExpressionValidator ID="regexAddress" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="address" ForeColor="Red" ValidationExpression="^([a-zA-Z0-9_. ])*$">* You must enter a valid address</asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="regexPostalCode" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="postalCode" ForeColor="Red" ValidationExpression="^([a-zA-Z0-9])*$">* You must enter a valid postal code</asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="regexPhoneNumber" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="phoneNumber" ForeColor="Red" ValidationExpression="^([a-zA-Z0-9])*$">* You must enter a valid phone number</asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="regexCreditCard" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="creditCard" ForeColor="Red" ValidationExpression="^[0-9]*$">* You must enter a valid credit card</asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="regexCVV" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="cvv" ForeColor="Red" ValidationExpression="^[0-9]*$">* You must enter a valid CVV</asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="regexExpDate" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="expiryDate" ForeColor="Red" ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$">* You must enter a valid expiry date</asp:RegularExpressionValidator>
        </div>
    </div>
</asp:Content>
