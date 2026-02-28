using SalesLedger.Business.Services;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace SalesLedger.Invoices
{
    public partial class Create : System.Web.UI.Page
    {
        private const decimal VatRate = 0.11m;
        private readonly InvoiceService _service = new InvoiceService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSalesOrders();
            }
        }

        private void BindSalesOrders()
        {
            ddlSalesOrder.Items.Clear();
            ddlSalesOrder.Items.Add(new ListItem("-- Select Sales Order --", "0"));
            foreach (var so in _service.GetOpenSalesOrders())
            {
                ddlSalesOrder.Items.Add(new ListItem(
                    string.Format("SO #{0} - {1} ({2:dd MMM yyyy}, {3} lines)",
                        so.Id, so.CustomerName, so.Date, so.LineCount),
                    so.Id.ToString()));
            }
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            pnlError.Visible = false;

            int salesOrderId;
            if (!int.TryParse(ddlSalesOrder.SelectedValue, out salesOrderId) || salesOrderId <= 0)
            {
                litError.Text = "Please select a sales order.";
                pnlError.Visible = true;
                return;
            }

            var detail = _service.GetSalesOrderDetail(salesOrderId);
            if (detail == null)
            {
                litError.Text = "Sales order not found.";
                pnlError.Visible = true;
                return;
            }

            var previewLines = detail.Lines.Select(l =>
            {
                decimal lineTotal = l.Qty * l.UnitPrice;
                decimal taxAmount = lineTotal * VatRate;
                return new
                {
                    l.ItemName,
                    l.Qty,
                    l.UnitPrice,
                    LineTotal = lineTotal,
                    TaxAmount = taxAmount
                };
            }).ToList();

            decimal netTotal = previewLines.Sum(x => x.LineTotal);
            decimal taxTotal = netTotal * VatRate;
            decimal grossTotal = netTotal + taxTotal;

            litSoId.Text = detail.Id.ToString();
            litCustomer.Text = detail.CustomerName;
            litDate.Text = DateTime.Today.ToString("dd MMM yyyy");

            rptLines.DataSource = previewLines;
            rptLines.DataBind();

            litNetTotal.Text = netTotal.ToString("N2");
            litTaxTotal.Text = taxTotal.ToString("N2");
            litGrossTotal.Text = grossTotal.ToString("N2");

            hfSalesOrderId.Value = salesOrderId.ToString();
            pnlSelectOrder.Visible = false;
            pnlPreview.Visible = true;
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            pnlError.Visible = false;

            int salesOrderId;
            if (!int.TryParse(hfSalesOrderId.Value, out salesOrderId) || salesOrderId <= 0)
            {
                litError.Text = "Invalid sales order reference.";
                pnlError.Visible = true;
                return;
            }

            try
            {
                _service.CreateInvoiceFromSalesOrder(salesOrderId);
                Response.Redirect("List.aspx?created=1");
            }
            catch (ArgumentException ex)
            {
                litError.Text = ex.Message;
                pnlError.Visible = true;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            pnlPreview.Visible = false;
            pnlSelectOrder.Visible = true;
            BindSalesOrders();
        }
    }
}
