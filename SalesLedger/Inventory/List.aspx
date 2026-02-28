<%@ Page Title="Inventory" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="SalesLedger.Inventory.List" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="sl-page-header">
        <h2>Inventory</h2>
    </div>

    <div class="card">
        <div class="card-body p-0">
            <asp:GridView ID="gvItems" runat="server"
                AutoGenerateColumns="false"
                CssClass="table table-hover mb-0"
                EmptyDataText="No items found."
                EmptyDataRowStyle-CssClass="text-center text-muted p-4">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Number" ItemStyle-CssClass="fw-bold" ItemStyle-Width="70" />
                    <asp:BoundField DataField="Name" HeaderText="Product" />
                    <asp:BoundField DataField="UnitPrice" HeaderText="Unit Price" DataFormatString="{0:N2}"
                        ItemStyle-CssClass="text-end" HeaderStyle-CssClass="text-end" ItemStyle-Width="120" />
                    <asp:TemplateField HeaderText="On Hand" HeaderStyle-CssClass="text-end" ItemStyle-CssClass="text-end" ItemStyle-Width="120">
                        <ItemTemplate>
                            <span class='<%# Convert.ToDecimal(Eval("OnHandQuantity")) <= 0 ? "text-danger fw-bold" : Convert.ToDecimal(Eval("OnHandQuantity")) <= 10 ? "text-warning fw-bold" : "" %>'>
                                <%# Eval("OnHandQuantity", "{0:N2}") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>
