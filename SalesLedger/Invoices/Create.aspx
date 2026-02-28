<%@ Page Title="Create Invoice" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="SalesLedger.Invoices.Create" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="sl-page-header">
        <h2>Create Invoice from Sales Order</h2>
    </div>

    <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-danger">
        <asp:Literal ID="litError" runat="server" />
    </asp:Panel>

    <asp:Panel ID="pnlSelectOrder" runat="server">
        <div class="card mb-3">
            <div class="card-header bg-white text-muted">Select Sales Order</div>
            <div class="card-body">
                <div class="row g-3 align-items-end">
                    <div class="col-md-5">
                        <label class="form-label">Open Sales Order <span class="text-danger">*</span></label>
                        <asp:DropDownList ID="ddlSalesOrder" runat="server" CssClass="form-select" />
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btnPreview" runat="server" Text="Preview"
                            CssClass="btn btn-primary" OnClick="btnPreview_Click" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlPreview" runat="server" Visible="false">
        <div class="card mb-3">
            <div class="card-header bg-white text-muted">Invoice Preview</div>
            <div class="card-body">
                <div class="row mb-3" style="font-size:0.8125rem;">
                    <div class="col-md-4"><span class="text-muted">Sales Order:</span> #<asp:Literal ID="litSoId" runat="server" /></div>
                    <div class="col-md-4"><span class="text-muted">Customer:</span> <asp:Literal ID="litCustomer" runat="server" /></div>
                    <div class="col-md-4"><span class="text-muted">Invoice Date:</span> <asp:Literal ID="litDate" runat="server" /></div>
                </div>

                <table class="table table-hover mb-3">
                    <thead>
                        <tr>
                            <th>Item</th>
                            <th class="text-end">Qty</th>
                            <th class="text-end">Unit Price</th>
                            <th class="text-end">Line Total</th>
                            <th class="text-end">Tax (11%)</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptLines" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("ItemName") %></td>
                                    <td class="text-end"><%# Eval("Qty", "{0:N2}") %></td>
                                    <td class="text-end"><%# Eval("UnitPrice", "{0:N2}") %></td>
                                    <td class="text-end"><%# Eval("LineTotal", "{0:N2}") %></td>
                                    <td class="text-end"><%# Eval("TaxAmount", "{0:N2}") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                    <tfoot>
                        <tr style="background:#f8f9fa;">
                            <td colspan="3" class="text-end fw-bold">Net Total:</td>
                            <td class="text-end fw-bold"><asp:Literal ID="litNetTotal" runat="server" /></td>
                            <td></td>
                        </tr>
                        <tr style="background:#f8f9fa;">
                            <td colspan="3" class="text-end fw-bold">Tax Total (11%):</td>
                            <td></td>
                            <td class="text-end fw-bold"><asp:Literal ID="litTaxTotal" runat="server" /></td>
                        </tr>
                        <tr style="background:#f3edf2;">
                            <td colspan="3" class="text-end fw-bold">Gross Total:</td>
                            <td colspan="2" class="text-end fw-bold"><asp:Literal ID="litGrossTotal" runat="server" /></td>
                        </tr>
                    </tfoot>
                </table>

                <asp:HiddenField ID="hfSalesOrderId" runat="server" />

                <div class="d-flex gap-2">
                    <asp:Button ID="btnConfirm" runat="server" Text="Confirm"
                        CssClass="btn btn-primary" OnClick="btnConfirm_Click" />
                    <asp:Button ID="btnBack" runat="server" Text="Back"
                        CssClass="btn btn-light" OnClick="btnBack_Click" CausesValidation="false" />
                </div>
            </div>
        </div>
    </asp:Panel>

    <a href="List.aspx" class="btn btn-light">Discard</a>

</asp:Content>
