<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SalesLedger._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="sl-page-header">
        <h2>Dashboard</h2>
    </div>

    <%-- KPI Cards --%>
    <div class="row g-3 mb-4">
        <div class="col-md-3">
            <div class="card h-100" style="border-left: 3px solid #714B67;">
                <div class="card-body py-3">
                    <div class="text-muted" style="font-size:0.7rem;text-transform:uppercase;letter-spacing:0.05em;">Open Orders</div>
                    <h3 class="mb-0 mt-1" style="font-weight:700;"><asp:Literal ID="litOpenOrders" runat="server" Text="0" /></h3>
                </div>
                <a href="/SalesOrders/List.aspx" class="card-footer text-decoration-none d-flex justify-content-between align-items-center py-2 px-3 bg-transparent" style="font-size:0.75rem;color:#714B67;">
                    <span>View all</span><span>&rarr;</span>
                </a>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card h-100" style="border-left: 3px solid #0dcaf0;">
                <div class="card-body py-3">
                    <div class="text-muted" style="font-size:0.7rem;text-transform:uppercase;letter-spacing:0.05em;">Unpaid Invoices</div>
                    <h3 class="mb-0 mt-1" style="font-weight:700;"><asp:Literal ID="litUnpaidInvoices" runat="server" Text="0" /></h3>
                </div>
                <a href="/Invoices/List.aspx" class="card-footer text-decoration-none d-flex justify-content-between align-items-center py-2 px-3 bg-transparent" style="font-size:0.75rem;color:#0dcaf0;">
                    <span>View all</span><span>&rarr;</span>
                </a>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card h-100" style="border-left: 3px solid #198754;">
                <div class="card-body py-3">
                    <div class="text-muted" style="font-size:0.7rem;text-transform:uppercase;letter-spacing:0.05em;">Payments</div>
                    <h3 class="mb-0 mt-1" style="font-weight:700;"><asp:Literal ID="litPayments" runat="server" Text="0" /></h3>
                </div>
                <a href="/Payments/List.aspx" class="card-footer text-decoration-none d-flex justify-content-between align-items-center py-2 px-3 bg-transparent" style="font-size:0.75rem;color:#198754;">
                    <span>View all</span><span>&rarr;</span>
                </a>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card h-100" style="border-left: 3px solid #6c757d;">
                <div class="card-body py-3">
                    <div class="text-muted" style="font-size:0.7rem;text-transform:uppercase;letter-spacing:0.05em;">GL Transactions</div>
                    <h3 class="mb-0 mt-1" style="font-weight:700;"><asp:Literal ID="litGLTx" runat="server" Text="0" /></h3>
                </div>
                <a href="/Transactions/List.aspx" class="card-footer text-decoration-none d-flex justify-content-between align-items-center py-2 px-3 bg-transparent" style="font-size:0.75rem;color:#6c757d;">
                    <span>View all</span><span>&rarr;</span>
                </a>
            </div>
        </div>
    </div>

    <%-- Quick Actions --%>
    <div class="mb-2" style="font-size:0.75rem;text-transform:uppercase;letter-spacing:0.05em;color:#6c757d;font-weight:600;">Quick Actions</div>
    <div class="row g-3 mb-4">
        <div class="col-md-3">
            <a href="/SalesOrders/Create.aspx" class="card text-decoration-none h-100" style="border-left:3px solid #714B67;">
                <div class="card-body py-3">
                    <div class="fw-bold" style="color:#714B67;">New Sales Order</div>
                    <div class="text-muted" style="font-size:0.75rem;">Create a new order for a customer</div>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a href="/Invoices/Create.aspx" class="card text-decoration-none h-100" style="border-left:3px solid #0dcaf0;">
                <div class="card-body py-3">
                    <div class="fw-bold" style="color:#0b5ed7;">Create Invoice</div>
                    <div class="text-muted" style="font-size:0.75rem;">Release a sales order to invoice</div>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a href="/Payments/Create.aspx" class="card text-decoration-none h-100" style="border-left:3px solid #198754;">
                <div class="card-body py-3">
                    <div class="fw-bold" style="color:#198754;">Receive Payment</div>
                    <div class="text-muted" style="font-size:0.75rem;">Apply payment against an invoice</div>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a href="/Inventory/List.aspx" class="card text-decoration-none h-100" style="border-left:3px solid #6c757d;">
                <div class="card-body py-3">
                    <div class="fw-bold" style="color:#6c757d;">View Inventory</div>
                    <div class="text-muted" style="font-size:0.75rem;">Check item stock levels</div>
                </div>
            </a>
        </div>
    </div>

    <%-- Workflow --%>
    <div class="card bg-white">
        <div class="card-body py-3">
            <div class="mb-2" style="font-size:0.75rem;text-transform:uppercase;letter-spacing:0.05em;color:#6c757d;font-weight:600;">Workflow</div>
            <div class="d-flex align-items-center flex-wrap gap-2">
                <span class="badge" style="background:#714B67;font-size:0.75rem;padding:0.4em 0.8em;">1. Create SO</span>
                <span class="text-muted">&rarr;</span>
                <span class="badge" style="background:#fd7e14;font-size:0.75rem;padding:0.4em 0.8em;">2. Release to Invoice</span>
                <span class="text-muted">&rarr;</span>
                <span class="badge" style="background:#0dcaf0;color:#212529;font-size:0.75rem;padding:0.4em 0.8em;">3. Post Invoice</span>
                <span class="text-muted">&rarr;</span>
                <span class="badge" style="background:#198754;font-size:0.75rem;padding:0.4em 0.8em;">4. Receive Payment</span>
            </div>
        </div>
    </div>

</asp:Content>
