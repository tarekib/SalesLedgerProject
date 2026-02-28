<%@ Page Title="GL Transactions" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="SalesLedger.Transactions.List" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="sl-page-header">
        <h2>Journal Entries</h2>
    </div>

    <asp:Panel ID="pnlList" runat="server">
        <div class="card">
            <div class="card-body p-0">
                <asp:GridView ID="gvTransactions" runat="server"
                    AutoGenerateColumns="false"
                    CssClass="table table-hover mb-0"
                    DataKeyNames="Id"
                    OnRowCommand="gvTransactions_RowCommand"
                    EmptyDataText="No journal entries found."
                    EmptyDataRowStyle-CssClass="text-center text-muted p-4">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Number" ItemStyle-CssClass="fw-bold" ItemStyle-Width="80" />
                        <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100" />
                        <asp:BoundField DataField="LineCount" HeaderText="Lines" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="70" />
                        <asp:BoundField DataField="TotalDebit" HeaderText="Total Debit" DataFormatString="{0:N2}"
                            ItemStyle-CssClass="text-end" HeaderStyle-CssClass="text-end" ItemStyle-Width="120" />
                        <asp:BoundField DataField="TotalCredit" HeaderText="Total Credit" DataFormatString="{0:N2}"
                            ItemStyle-CssClass="text-end" HeaderStyle-CssClass="text-end" ItemStyle-Width="120" />
                        <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" ItemStyle-Width="90">
                            <ItemTemplate>
                                <span class='badge <%# Convert.ToDecimal(Eval("TotalDebit")) == Convert.ToDecimal(Eval("TotalCredit")) ? "badge-posted" : "bg-danger" %>'>
                                    <%# Convert.ToDecimal(Eval("TotalDebit")) == Convert.ToDecimal(Eval("TotalCredit")) ? "Balanced" : "Unbalanced" %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" ItemStyle-Width="100" ItemStyle-CssClass="text-end">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnView" runat="server"
                                    CommandName="ViewDetail"
                                    CommandArgument='<%# Eval("Id") %>'
                                    CssClass="btn btn-sm btn-outline-secondary"
                                    CausesValidation="false"
                                    Text="View" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlDetail" runat="server" Visible="false">
        <div class="card">
            <div class="card-header bg-white text-muted d-flex justify-content-between align-items-center">
                <span>Journal Entry #<asp:Literal ID="litTxId" runat="server" /></span>
                <asp:Button ID="btnBackToList" runat="server" Text="Back"
                    CssClass="btn btn-sm btn-light" OnClick="btnBackToList_Click" CausesValidation="false" />
            </div>
            <div class="card-body p-0">
                <asp:GridView ID="gvLines" runat="server"
                    AutoGenerateColumns="false"
                    CssClass="table table-hover mb-0"
                    EmptyDataText="No lines.">
                    <Columns>
                        <asp:BoundField DataField="Account" HeaderText="Account" />
                        <asp:BoundField DataField="Debit" HeaderText="Debit" DataFormatString="{0:N2}"
                            ItemStyle-CssClass="text-end" HeaderStyle-CssClass="text-end" ItemStyle-Width="140" />
                        <asp:BoundField DataField="Credit" HeaderText="Credit" DataFormatString="{0:N2}"
                            ItemStyle-CssClass="text-end" HeaderStyle-CssClass="text-end" ItemStyle-Width="140" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
