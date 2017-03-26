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
    <asp:UpdatePanel ID="UpdatePanelTable" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
              <asp:GridView ID="grdTips" runat="server" AutoGenerateColumns="False" DataKeyNames="Tip" DataSourceID="SqlDataSource1" AllowPaging="True" AllowSorting="True" PageSize="5" CssClass="tipsTable">
                <Columns>
                    <asp:BoundField DataField="Tip" ReadOnly="True"/>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:pekolDB %>" SelectCommand="SELECT * FROM [Tips]"></asp:SqlDataSource>

    <div id="addTip" runat="server">
        <asp:Label ID="lblTip" runat="server" Text="Add Tip:"></asp:Label>
        <asp:TextBox ID="tip" runat="server" Text="" CssClass="inputText"></asp:TextBox>
        <asp:Button ID="saveTip" runat="server" Text="Submit" CssClass="tipButton" OnClick="saveTip_Click"/>
    </div>


        <%--<div class="peckolHeader">
            Peckol Code Test
        </div>
        <div class="peckolText">
            This is our patented Pekol certified code analyzer tool. Just upload some C# code you have and we'll tell you whether Peckol will break your fingers or not!
        </div>
        <asp:RadioButtonList ID="languageList" runat="server" CssClass="radialButtons">
            <asp:ListItem Text="Javascript"></asp:ListItem>
            <asp:ListItem Text="C#"></asp:ListItem>
            <asp:ListItem Text="Java"></asp:ListItem>
        </asp:RadioButtonList>
        <asp:FileUpload ID="codeUpload" runat="server" CssClass="fileUpload" OnDataBinding="codeUpload_DataBinding" OnLoad="codeUpload_Load"/>
        <br />
        <asp:Button ID="submitBtn" runat="server" Text="Submit" OnClick="submitBtn_Click" CssClass="codeButton"/>
        <asp:Label ID="lblCodeContent" runat="server" Text=""></asp:Label>--%>
    </div>
</asp:Content>
