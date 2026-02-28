using SalesLedger.Business.Services;
using System;

namespace SalesLedger.Payments
{
    public partial class List : System.Web.UI.Page
    {
        private readonly PaymentService _service = new PaymentService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["created"] == "1")
                {
                    litMessage.Text = "Payment received and posted successfully.";
                    pnlSuccess.Visible = true;
                }

                gvPayments.DataSource = _service.GetAllPayments();
                gvPayments.DataBind();
            }
        }
    }
}
