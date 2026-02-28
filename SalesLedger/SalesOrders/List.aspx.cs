using SalesLedger.Business.Services;
using System;

namespace SalesLedger.SalesOrders
{
    public partial class List : System.Web.UI.Page
    {
        private readonly SalesOrderService _service = new SalesOrderService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["created"] == "1")
                {
                    litMessage.Text = "Sales order created successfully.";
                    pnlSuccess.Visible = true;
                }

                gvOrders.DataSource = _service.GetAllSalesOrders();
                gvOrders.DataBind();
            }
        }
    }
}
