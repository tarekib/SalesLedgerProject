using SalesLedger.Business.Services;
using System;
using System.Web.UI.WebControls;

namespace SalesLedger.Invoices
{
    public partial class List : System.Web.UI.Page
    {
        private readonly InvoiceService _service = new InvoiceService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["created"] == "1")
                {
                    litMessage.Text = "Invoice created successfully.";
                    pnlSuccess.Visible = true;
                }
                if (Request.QueryString["posted"] == "1")
                {
                    litMessage.Text = "Invoice posted successfully. Inventory updated and GL transaction created.";
                    pnlSuccess.Visible = true;
                }

                BindGrid();
            }
        }

        private void BindGrid()
        {
            gvInvoices.DataSource = _service.GetAllInvoices();
            gvInvoices.DataBind();
        }

        protected void gvInvoices_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Post")
            {
                pnlSuccess.Visible = false;
                pnlError.Visible = false;

                int invoiceId;
                if (!int.TryParse(e.CommandArgument.ToString(), out invoiceId))
                    return;

                try
                {
                    _service.PostInvoice(invoiceId);
                    litMessage.Text = string.Format(
                        "Invoice #{0} posted. Inventory updated and GL transaction created.", invoiceId);
                    pnlSuccess.Visible = true;
                }
                catch (ArgumentException ex)
                {
                    litError.Text = ex.Message;
                    pnlError.Visible = true;
                }

                BindGrid();
            }
            else if (e.CommandName == "Pay")
            {
                Response.Redirect("/Payments/Create.aspx?invoiceId=" + e.CommandArgument);
            }
        }
    }
}
