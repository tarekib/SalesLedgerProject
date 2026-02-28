using SalesLedger.Business.Services;
using System;

namespace SalesLedger.Inventory
{
    public partial class List : System.Web.UI.Page
    {
        private readonly PaymentService _service = new PaymentService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvItems.DataSource = _service.GetInventory();
                gvItems.DataBind();
            }
        }
    }
}
