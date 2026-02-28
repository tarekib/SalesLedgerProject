<%@ Page Title="Sales Orders" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="SalesLedger.SalesOrders.List" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mt-4 mb-3">
        <h2>Sales Orders</h2>
        <a href="Create.aspx" class="btn btn-primary">+ New Sales Order</a>
    </div>

    <asp:Panel ID="pnlSuccess" runat="server" Visible="false" CssClass="alert alert-success alert-dismissible fade show" role="alert">
        <asp:Literal ID="litMessage" runat="server" />
        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    </asp:Panel>

    <asp:GridView ID="gvOrders" runat="server"
        AutoGenerateColumns="false"
        CssClass="table table-bordered table-hover table-striped"
        HeaderStyle-CssClass="table-dark"
        EmptyDataText="No sales orders found."
        EmptyDataRowStyle-CssClass="text-center text-muted">
        <Columns>
            <asp:BoundField DataField="Id"           HeaderText="#"         ItemStyle-CssClass="fw-bold" />
            <asp:BoundField DataField="CustomerName" HeaderText="Customer" />
            <asp:BoundField DataField="Date"         HeaderText="Date"      DataFormatString="{0:dd MMM yyyy}" />
            <asp:BoundField DataField="LineCount"    HeaderText="Lines" />
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <span class='badge bg-success'><%# Eval("Status") %></span>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:Content>
