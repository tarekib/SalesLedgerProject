<%@ Page Title="Create Sales Order" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="SalesLedger.SalesOrders.Create" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="sl-page-header">
        <h2>New Sales Order</h2>
    </div>

    <asp:ValidationSummary ID="valSummary" runat="server"
        CssClass="alert alert-danger" HeaderText="Please fix the following:" DisplayMode="BulletList" />

    <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-danger">
        <asp:Literal ID="litError" runat="server" />
    </asp:Panel>

    <div class="card mb-3">
        <div class="card-header bg-white text-muted">Order Details</div>
        <div class="card-body">
            <div class="row g-3">
                <div class="col-md-5">
                    <label class="form-label">Customer <span class="text-danger">*</span></label>
                    <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="form-select" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlCustomer" InitialValue="0"
                        ErrorMessage="Customer is required." Text=" " CssClass="text-danger small" Display="Dynamic" />
                </div>
                <div class="col-md-3">
                    <label class="form-label">Order Date <span class="text-danger">*</span></label>
                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" TextMode="Date" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDate"
                        ErrorMessage="Order date is required." Text=" " CssClass="text-danger small" Display="Dynamic" />
                </div>
            </div>
        </div>
    </div>

    <div class="card mb-3">
        <div class="card-header bg-white text-muted d-flex justify-content-between align-items-center">
            <span>Order Lines</span>
            <asp:Button ID="btnAddLine" runat="server" Text="+ Add Line"
                CssClass="btn btn-sm btn-outline-secondary"
                OnClick="btnAddLine_Click"
                CausesValidation="false" />
        </div>
        <div class="card-body p-0">
            <table class="table table-hover mb-0">
                <thead>
                    <tr>
                        <th style="width:40%">Item</th>
                        <th style="width:15%">Qty</th>
                        <th style="width:20%">Unit Price</th>
                        <th style="width:15%">Line Total</th>
                        <th style="width:10%"></th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptLines" runat="server" OnItemCommand="rptLines_ItemCommand"
                        OnItemDataBound="rptLines_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlItem" runat="server"
                                        CssClass="form-select form-select-sm"
                                        AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtQty" runat="server"
                                        CssClass="form-control form-control-sm"
                                        Text='<%# ((SalesLedger.SalesOrders.LineRow)Container.DataItem).Qty %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtUnitPrice" runat="server"
                                        CssClass="form-control form-control-sm"
                                        Text='<%# ((SalesLedger.SalesOrders.LineRow)Container.DataItem).UnitPrice.ToString("0.00") %>' />
                                </td>
                                <td class="text-end align-middle">
                                    <%# (((SalesLedger.SalesOrders.LineRow)Container.DataItem).Qty *
                                        ((SalesLedger.SalesOrders.LineRow)Container.DataItem).UnitPrice).ToString("0.00") %>
                                </td>
                                <td class="text-center">
                                    <asp:LinkButton ID="btnRemove" runat="server"
                                        CommandName="Remove"
                                        CommandArgument='<%# Container.ItemIndex %>'
                                        CssClass="btn btn-sm btn-outline-danger"
                                        CausesValidation="false"
                                        Text="&times;" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr style="background:#f8f9fa;">
                        <td colspan="3" class="text-end fw-bold">Total:</td>
                        <td class="text-end fw-bold"><asp:Literal ID="litTotal" runat="server" /></td>
                        <td></td>
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>

    <div class="d-flex gap-2">
        <asp:Button ID="btnSave" runat="server" Text="Save"
            CssClass="btn btn-primary" OnClick="btnSave_Click" />
        <a href="List.aspx" class="btn btn-light">Discard</a>
    </div>

</asp:Content>
