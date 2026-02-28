<%@ Page Title="Payments" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="SalesLedger.Payments.List" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="sl-page-header">
        <h2>Payments</h2>
        <a href="Create.aspx" class="btn btn-primary">New</a>
    </div>

    <asp:Panel ID="pnlSuccess" runat="server" Visible="false" CssClass="alert alert-success alert-dismissible fade show" role="alert">
        <asp:Literal ID="litMessage" runat="server" />
        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    </asp:Panel>

    <div class="card">
        <div class="card-body p-0">
            <asp:GridView ID="gvPayments" runat="server"
                AutoGenerateColumns="false"
                CssClass="table table-hover mb-0"
                EmptyDataText="No payments recorded."
                EmptyDataRowStyle-CssClass="text-center text-muted p-4">
                <Columns>
                    <asp:BoundField DataField="Id"           HeaderText="Number"    ItemStyle-CssClass="fw-bold" ItemStyle-Width="70" />
                    <asp:BoundField DataField="CustomerName" HeaderText="Customer" />
                    <asp:BoundField DataField="InvoiceId"    HeaderText="Invoice Ref" ItemStyle-Width="90" />
                    <asp:BoundField DataField="Date"         HeaderText="Date"       DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100" />
                    <asp:BoundField DataField="Amount"       HeaderText="Amount"     DataFormatString="{0:N2}"
                        ItemStyle-CssClass="text-end fw-bold" HeaderStyle-CssClass="text-end" ItemStyle-Width="120" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>
