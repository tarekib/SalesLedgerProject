<%@ Page Title="Invoices" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="SalesLedger.Invoices.List" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="sl-page-header">
        <h2>Invoices</h2>
        <a href="Create.aspx" class="btn btn-primary">New</a>
    </div>

    <asp:Panel ID="pnlSuccess" runat="server" Visible="false" CssClass="alert alert-success alert-dismissible fade show" role="alert">
        <asp:Literal ID="litMessage" runat="server" />
        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    </asp:Panel>

    <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-danger alert-dismissible fade show" role="alert">
        <asp:Literal ID="litError" runat="server" />
        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    </asp:Panel>

    <div class="card">
        <div class="card-body p-0">
            <asp:GridView ID="gvInvoices" runat="server"
                AutoGenerateColumns="false"
                CssClass="table table-hover mb-0"
                DataKeyNames="Id"
                OnRowCommand="gvInvoices_RowCommand"
                EmptyDataText="No invoices found."
                EmptyDataRowStyle-CssClass="text-center text-muted p-4">
                <Columns>
                    <asp:BoundField DataField="Id"           HeaderText="Number"   ItemStyle-CssClass="fw-bold" ItemStyle-Width="70" />
                    <asp:BoundField DataField="SalesOrderId" HeaderText="SO Ref"   ItemStyle-Width="70" />
                    <asp:BoundField DataField="CustomerName" HeaderText="Customer" />
                    <asp:BoundField DataField="Date"         HeaderText="Date"     DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100" />
                    <asp:BoundField DataField="GrossTotal"   HeaderText="Total"    DataFormatString="{0:N2}" ItemStyle-CssClass="text-end" HeaderStyle-CssClass="text-end" ItemStyle-Width="100" />
                    <asp:BoundField DataField="AmountPaid"   HeaderText="Paid"     DataFormatString="{0:N2}" ItemStyle-CssClass="text-end" HeaderStyle-CssClass="text-end" ItemStyle-Width="90" />
                    <asp:TemplateField HeaderText="Balance" HeaderStyle-CssClass="text-end" ItemStyle-CssClass="text-end" ItemStyle-Width="90">
                        <ItemTemplate>
                            <span class='<%# Convert.ToDecimal(Eval("BalanceDue")) > 0 ? "text-danger fw-bold" : "" %>'>
                                <%# Eval("BalanceDue", "{0:N2}") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="110">
                        <ItemTemplate>
                            <span class='badge badge-<%# Eval("Status").ToString().ToLower().Replace(" ","") %>'>
                                <%# Eval("Status") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" ItemStyle-Width="150" ItemStyle-CssClass="text-end">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnPost" runat="server"
                                CommandName="Post"
                                CommandArgument='<%# Eval("Id") %>'
                                CssClass="btn btn-sm btn-outline-secondary me-1"
                                Text="Post"
                                Visible='<%# Eval("Status").ToString() == "Open" %>'
                                OnClientClick="return confirm('Post this invoice? This will subtract inventory and create GL entries.');" />
                            <asp:LinkButton ID="btnPay" runat="server"
                                CommandName="Pay"
                                CommandArgument='<%# Eval("Id") %>'
                                CssClass="btn btn-sm btn-outline-secondary"
                                Text="Register Payment"
                                Visible='<%# Eval("Status").ToString() == "Posted" || Eval("Status").ToString() == "PartiallyPaid" %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>
