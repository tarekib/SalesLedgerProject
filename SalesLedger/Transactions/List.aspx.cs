using SalesLedger.Business.Services;
using System;
using System.Web.UI.WebControls;

namespace SalesLedger.Transactions
{
    public partial class List : System.Web.UI.Page
    {
        private readonly PaymentService _service = new PaymentService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            gvTransactions.DataSource = _service.GetAllGLTransactions();
            gvTransactions.DataBind();
        }

        protected void gvTransactions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetail")
            {
                int txId;
                if (!int.TryParse(e.CommandArgument.ToString(), out txId))
                    return;

                litTxId.Text = txId.ToString();
                gvLines.DataSource = _service.GetGLTransactionLines(txId);
                gvLines.DataBind();

                pnlList.Visible = false;
                pnlDetail.Visible = true;
            }
        }

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            pnlDetail.Visible = false;
            pnlList.Visible = true;
            BindGrid();
        }
    }
}
