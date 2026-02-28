<%@ Page Title="Receive Payment" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="SalesLedger.Payments.Create" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="sl-page-header">
        <h2>Register Payment</h2>
    </div>

    <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-danger">
        <asp:Literal ID="litError" runat="server" />
    </asp:Panel>

    <div class="card mb-3">
        <div class="card-header bg-white text-muted">Payment Details</div>
        <div class="card-body">
            <div class="row g-3">
                <div class="col-md-6">
                    <label class="form-label">Invoice <span class="text-danger">*</span></label>
                    <asp:DropDownList ID="ddlInvoice" runat="server" CssClass="form-select"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlInvoice_SelectedIndexChanged" />
                </div>
                <div class="col-md-3">
                    <label class="form-label">Payment Date <span class="text-danger">*</span></label>
                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" TextMode="Date" />
                </div>
            </div>

            <asp:Panel ID="pnlInvoiceInfo" runat="server" Visible="false">
                <div class="row g-3 mt-3">
                    <div class="col-md-3">
                        <div style="border-left:3px solid #714B67;padding-left:0.75rem;">
                            <div class="text-muted" style="font-size:0.7rem;text-transform:uppercase;">Customer</div>
                            <div class="fw-bold"><asp:Literal ID="litCustomer" runat="server" /></div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div style="border-left:3px solid #dee2e6;padding-left:0.75rem;">
                            <div class="text-muted" style="font-size:0.7rem;text-transform:uppercase;">Invoice Total</div>
                            <div class="fw-bold"><asp:Literal ID="litGrossTotal" runat="server" /></div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div style="border-left:3px solid #dee2e6;padding-left:0.75rem;">
                            <div class="text-muted" style="font-size:0.7rem;text-transform:uppercase;">Already Paid</div>
                            <div class="fw-bold"><asp:Literal ID="litAmountPaid" runat="server" /></div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div style="border-left:3px solid #dc3545;padding-left:0.75rem;">
                            <div class="text-muted" style="font-size:0.7rem;text-transform:uppercase;">Balance Due</div>
                            <div class="fw-bold text-danger" style="font-size:1.1rem;"><asp:Literal ID="litBalanceDue" runat="server" /></div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <div class="row g-3 mt-2">
                <div class="col-md-4">
                    <label class="form-label">Amount <span class="text-danger">*</span></label>
                    <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" placeholder="0.00" />
                </div>
            </div>
        </div>
    </div>

    <div class="card mb-3" style="border-left:3px solid #0dcaf0;">
        <div class="card-body py-2" style="font-size:0.8rem;color:#6c757d;">
            <strong>GL Entry:</strong>
            Dr 1000 Cash &mdash; Cr 1100 AR
        </div>
    </div>

    <div class="d-flex gap-2">
        <asp:Button ID="btnSave" runat="server" Text="Validate"
            CssClass="btn btn-primary" OnClick="btnSave_Click" />
        <a href="List.aspx" class="btn btn-light">Discard</a>
    </div>

</asp:Content>
