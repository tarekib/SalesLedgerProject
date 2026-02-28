<%@ Page Title="Sales Orders" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="SalesLedger.SalesOrders.List" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="sl-page-header">
        <h2>Sales Orders</h2>
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
            <asp:GridView ID="gvOrders" runat="server"
                AutoGenerateColumns="false"
                CssClass="table table-hover mb-0"
                DataKeyNames="Id"
                OnRowCommand="gvOrders_RowCommand"
                EmptyDataText="No sales orders found."
                EmptyDataRowStyle-CssClass="text-center text-muted p-4">
                <Columns>
                    <asp:BoundField DataField="Id"           HeaderText="Number"   ItemStyle-CssClass="fw-bold" ItemStyle-Width="70" />
                    <asp:BoundField DataField="CustomerName" HeaderText="Customer" />
                    <asp:BoundField DataField="Date"         HeaderText="Order Date" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="LineCount"    HeaderText="Lines"     ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="70" />
                    <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="100">
                        <ItemTemplate>
                            <span class='badge badge-<%# Eval("Status").ToString().ToLower() %>'>
                                <%# Eval("Status") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" ItemStyle-Width="140" ItemStyle-CssClass="text-end">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnRelease" runat="server"
                                CommandName="Release"
                                CommandArgument='<%# Eval("Id") %>'
                                CssClass="btn btn-sm btn-outline-secondary"
                                Text="Release"
                                Visible='<%# Eval("Status").ToString() == "Open" %>'
                                OnClientClick="return confirm('Release this sales order and create an invoice?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>
