using SalesLedger.Business.Services;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace SalesLedger.Payments
{
    public partial class Create : System.Web.UI.Page
    {
        private readonly PaymentService _service = new PaymentService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                BindInvoices();

                // Pre-select invoice if coming from Invoices list
                string invoiceIdParam = Request.QueryString["invoiceId"];
                if (!string.IsNullOrEmpty(invoiceIdParam))
                {
                    var item = ddlInvoice.Items.FindByValue(invoiceIdParam);
                    if (item != null)
                    {
                        ddlInvoice.SelectedValue = invoiceIdParam;
                        ShowInvoiceInfo(int.Parse(invoiceIdParam));
                    }
                }
            }
        }

        private void BindInvoices()
        {
            ddlInvoice.Items.Clear();
            ddlInvoice.Items.Add(new ListItem("-- Select Invoice --", "0"));
            foreach (var inv in _service.GetPayableInvoices())
            {
                ddlInvoice.Items.Add(new ListItem(
                    string.Format("Invoice #{0} — {1} (Due: {2:N2})",
                        inv.Id, inv.CustomerName, inv.BalanceDue),
                    inv.Id.ToString()));
            }
        }

        private void ShowInvoiceInfo(int invoiceId)
        {
            var inv = _service.GetPayableInvoices().FirstOrDefault(x => x.Id == invoiceId);
            if (inv == null)
            {
                pnlInvoiceInfo.Visible = false;
                return;
            }

            litCustomer.Text = inv.CustomerName;
            litGrossTotal.Text = inv.GrossTotal.ToString("N2");
            litAmountPaid.Text = inv.AmountPaid.ToString("N2");
            litBalanceDue.Text = inv.BalanceDue.ToString("N2");
            txtAmount.Text = inv.BalanceDue.ToString("0.00");
            pnlInvoiceInfo.Visible = true;
        }

        protected void ddlInvoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            int invoiceId;
            if (!int.TryParse(ddlInvoice.SelectedValue, out invoiceId) || invoiceId <= 0)
            {
                pnlInvoiceInfo.Visible = false;
                return;
            }

            ShowInvoiceInfo(invoiceId);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            pnlError.Visible = false;

            int invoiceId;
            if (!int.TryParse(ddlInvoice.SelectedValue, out invoiceId) || invoiceId <= 0)
            {
                litError.Text = "Please select an invoice.";
                pnlError.Visible = true;
                return;
            }

            decimal amount;
            if (!decimal.TryParse(txtAmount.Text, out amount) || amount <= 0)
            {
                litError.Text = "Please enter a valid payment amount greater than zero.";
                pnlError.Visible = true;
                return;
            }

            DateTime date;
            if (!DateTime.TryParse(txtDate.Text, out date))
            {
                litError.Text = "Please enter a valid date.";
                pnlError.Visible = true;
                return;
            }

            try
            {
                _service.CreateAndPostPayment(invoiceId, amount, date);
                Response.Redirect("List.aspx?created=1");
            }
            catch (ArgumentException ex)
            {
                litError.Text = ex.Message;
                pnlError.Visible = true;
            }
        }
    }
}
