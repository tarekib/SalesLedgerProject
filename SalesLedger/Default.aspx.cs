using SalesLedger.Business.Services;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SalesLedger
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var soService = new SalesOrderService();
                var invService = new InvoiceService();
                var payService = new PaymentService();

                litOpenOrders.Text = soService.GetAllSalesOrders().Count(x => x.Status == "Open").ToString();
                litUnpaidInvoices.Text = invService.GetAllInvoices().Count(x => x.Status != "Paid").ToString();
                litPayments.Text = payService.GetAllPayments().Count.ToString();
                litGLTx.Text = payService.GetAllGLTransactions().Count.ToString();
            }
        }
    }
}