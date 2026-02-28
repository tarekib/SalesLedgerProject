using SalesLedger.Business.Models;
using SalesLedger.Business.Services;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SalesLedger.SalesOrders
{
    [Serializable]
    public class LineRow
    {
        public int ItemId { get; set; }
        public decimal Qty { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public partial class Create : System.Web.UI.Page
    {
        private readonly SalesOrderService _service = new SalesOrderService();

        private List<LineRow> Lines
        {
            get
            {
                if (ViewState["Lines"] == null)
                    ViewState["Lines"] = new List<LineRow>();
                return (List<LineRow>)ViewState["Lines"];
            }
            set { ViewState["Lines"] = value; }
        }

        private List<ItemDto> _items;
        private List<ItemDto> Items => _items ?? (_items = _service.GetItems());

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                BindCustomers();
                Lines = new List<LineRow> { new LineRow { Qty = 1 } };
                BindRepeater();
            }
        }

        private void BindCustomers()
        {
            ddlCustomer.Items.Clear();
            ddlCustomer.Items.Add(new ListItem("-- Select Customer --", "0"));
            foreach (var c in _service.GetCustomers())
                ddlCustomer.Items.Add(new ListItem(c.Name, c.Id.ToString()));
        }

        // ?? Repeater helpers ????????????????????????????????????????????????

        private void BindRepeater()
        {
            rptLines.DataSource = Lines;
            rptLines.DataBind();
            UpdateTotal();
        }

        private void PopulateItemDropdown(DropDownList ddl, int selectedItemId)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("-- Select Item --", "0"));
            foreach (var item in Items)
                ddl.Items.Add(new ListItem($"{item.Name}  (${item.UnitPrice:0.00})", item.Id.ToString()));

            if (selectedItemId > 0)
                ddl.SelectedValue = selectedItemId.ToString();
        }

        protected void rptLines_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
                return;

            var ddl = (DropDownList)e.Item.FindControl("ddlItem");
            var row = (LineRow)e.Item.DataItem;
            PopulateItemDropdown(ddl, row.ItemId);
        }

        // Read current values from repeater controls back into ViewState
        private void SyncRepeaterToViewState()
        {
            var list = Lines;
            for (int i = 0; i < rptLines.Items.Count; i++)
            {
                var ddl = (DropDownList)rptLines.Items[i].FindControl("ddlItem");
                var txtQty = (TextBox)rptLines.Items[i].FindControl("txtQty");
                var txtPrice = (TextBox)rptLines.Items[i].FindControl("txtUnitPrice");

                int.TryParse(ddl.SelectedValue, out int itemId);
                decimal.TryParse(txtQty.Text, out decimal qty);
                decimal.TryParse(txtPrice.Text, out decimal price);

                list[i].ItemId = itemId;
                list[i].Qty = qty;
                list[i].UnitPrice = price;
            }
            Lines = list;
        }

        private void UpdateTotal()
        {
            decimal total = 0;
            foreach (var r in Lines) total += r.Qty * r.UnitPrice;
            litTotal.Text = total.ToString("0.00");
        }

        // ?? Events ??????????????????????????????????????????????????????????

        protected void btnAddLine_Click(object sender, EventArgs e)
        {
            SyncRepeaterToViewState();
            Lines.Add(new LineRow { Qty = 1 });
            BindRepeater();
        }

        protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            SyncRepeaterToViewState();

            var ddl = (DropDownList)sender;
            var item = (RepeaterItem)ddl.NamingContainer;
            int idx = item.ItemIndex;

            if (int.TryParse(ddl.SelectedValue, out int itemId) && itemId > 0)
            {
                var match = Items.Find(x => x.Id == itemId);
                if (match != null)
                {
                    Lines[idx].UnitPrice = match.UnitPrice;
                    Lines[idx].ItemId = itemId;
                }
            }
            BindRepeater();
        }

        protected void rptLines_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                SyncRepeaterToViewState();
                int idx = int.Parse(e.CommandArgument.ToString());
                var list = Lines;
                if (list.Count > 1) list.RemoveAt(idx);
                Lines = list;
                BindRepeater();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            SyncRepeaterToViewState();
            pnlError.Visible = false;

            var request = new CreateSalesOrderRequest
            {
                CustomerId = int.Parse(ddlCustomer.SelectedValue),
                Date = DateTime.Parse(txtDate.Text),
                Lines = new List<SalesOrderLineDto>()
            };

            foreach (var row in Lines)
            {
                if (row.ItemId <= 0) continue;
                request.Lines.Add(new SalesOrderLineDto
                {
                    ItemId = row.ItemId,
                    Qty = row.Qty,
                    UnitPrice = row.UnitPrice
                });
            }

            try
            {
                _service.CreateSalesOrder(request);
                Response.Redirect("List.aspx?created=1");
            }
            catch (ArgumentException ex)
            {
                litError.Text = ex.Message;
                pnlError.Visible = true;
                BindRepeater();
            }
        }
    }
}
