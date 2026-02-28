<%@ Page Title="Create Sales Order" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="SalesLedger.SalesOrders.Create" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="mt-4 mb-4">Create Sales Order</h2>

    <asp:ValidationSummary ID="valSummary" runat="server"
        CssClass="alert alert-danger" HeaderText="Please fix the following errors:" DisplayMode="BulletList" />

    <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-danger">
        <asp:Literal ID="litError" runat="server" />
    </asp:Panel>

    <%-- ?? Order Header ?? --%>
    <div class="card mb-4">
        <div class="card-header fw-bold bg-secondary text-white">Order Details</div>
        <div class="card-body">
            <div class="row g-3">
                <div class="col-md-6">
                    <label class="form-label">Customer <span class="text-danger">*</span></label>
                    <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="form-select" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlCustomer" InitialValue="0"
                        ErrorMessage="Customer is required." Text=" " CssClass="text-danger small" Display="Dynamic" />
                </div>
                <div class="col-md-4">
                    <label class="form-label">Order Date <span class="text-danger">*</span></label>
                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" TextMode="Date" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDate"
                        ErrorMessage="Order date is required." Text=" " CssClass="text-danger small" Display="Dynamic" />
                </div>
            </div>
        </div>
    </div>

    <%-- ?? Order Lines ?? --%>
    <div class="card mb-4">
        <div class="card-header fw-bold bg-secondary text-white d-flex justify-content-between align-items-center">
            <span>Order Lines</span>
            <asp:Button ID="btnAddLine" runat="server" Text="+ Add Line"
                CssClass="btn btn-sm btn-light"
                OnClick="btnAddLine_Click"
                CausesValidation="false" />
        </div>
        <div class="card-body p-0">
            <table class="table table-bordered mb-0" id="linesTable">
                <thead class="table-light">
                    <tr>
                        <th style="width:40%">Item <span class="text-danger">*</span></th>
                        <th style="width:15%">Qty <span class="text-danger">*</span></th>
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
                                <td class="text-end align-middle lineTotal">
                                    <%# (((SalesLedger.SalesOrders.LineRow)Container.DataItem).Qty *
                                        ((SalesLedger.SalesOrders.LineRow)Container.DataItem).UnitPrice).ToString("0.00") %>
                                </td>
                                <td class="text-center">
                                    <asp:LinkButton ID="btnRemove" runat="server"
                                        CommandName="Remove"
                                        CommandArgument='<%# Container.ItemIndex %>'
                                        CssClass="btn btn-sm btn-outline-danger"
                                        CausesValidation="false"
                                        Text="?" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr class="table-light fw-bold">
                        <td colspan="3" class="text-end">Total:</td>
                        <td class="text-end">
                            <asp:Literal ID="litTotal" runat="server" /></td>
                        <td></td>
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>

    <asp:Button ID="btnSave" runat="server" Text="?? Save Sales Order"
        CssClass="btn btn-success me-2" OnClick="btnSave_Click" />
    <a href="List.aspx" class="btn btn-secondary">Cancel</a>

</asp:Content>
