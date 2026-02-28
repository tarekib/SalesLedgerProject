using SalesLedger.Business.Services;
using System;
using System.Web.UI.WebControls;

namespace SalesLedger.SalesOrders
{
    public partial class List : System.Web.UI.Page
    {
        private readonly SalesOrderService _service = new SalesOrderService();
        private readonly InvoiceService _invoiceService = new InvoiceService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["created"] == "1")
                {
                    litMessage.Text = "Sales order created successfully.";
                    pnlSuccess.Visible = true;
                }

                BindGrid();
            }
        }

        private void BindGrid()
        {
            gvOrders.DataSource = _service.GetAllSalesOrders();
            gvOrders.DataBind();
        }

        protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Release")
            {
                pnlSuccess.Visible = false;
                pnlError.Visible = false;

                int salesOrderId;
                if (!int.TryParse(e.CommandArgument.ToString(), out salesOrderId))
                    return;

                try
                {
                    int invoiceId = _invoiceService.CreateInvoiceFromSalesOrder(salesOrderId);
                    litMessage.Text = string.Format(
                        "Sales order #{0} released successfully. <a href='/Invoices/List.aspx' class='alert-link'>Invoice #{1}</a> created.",
                        salesOrderId, invoiceId);
                    pnlSuccess.Visible = true;
                }
                catch (ArgumentException ex)
                {
                    litError.Text = ex.Message;
                    pnlError.Visible = true;
                }

                BindGrid();
            }
        }
    }
}
