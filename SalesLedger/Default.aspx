<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SalesLedger._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">
        <h2>Dashboard</h2>
    </div>

    <div class="row g-4">
        <div class="col-md-4">
            <div class="card border-primary h-100">
                <div class="card-body">
                    <h5 class="card-title">Sales Orders</h5>
                    <p class="card-text text-muted">View and manage all sales orders.</p>
                    <a href="/SalesOrders/List.aspx" class="btn btn-primary">View Sales Orders</a>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="card border-success h-100">
                <div class="card-body">
                    <h5 class="card-title">New Sales Order</h5>
                    <p class="card-text text-muted">Create a new sales order for a customer.</p>
                    <a href="/SalesOrders/Create.aspx" class="btn btn-success">Create Sales Order</a>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
