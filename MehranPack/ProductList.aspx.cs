using System;
using System.Linq;
using System.Web.UI.WebControls;
using Common;
using Repository.DAL;

namespace MehranPack
{
    public partial class ProductList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ProductRepository productRepository = new ProductRepository();

                if (Utility.IsInOutMode())
                    gridProduct.DataSource = productRepository.GetAllWithCatNameForInOut();
                else
                    gridProduct.DataSource = productRepository.GetAllWithCatName();

                gridProduct.DataBind();
                Session["Result"] = gridProduct.DataSource;
            }
        }

        protected void gridProduct_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditProduct")
            {
                Response.Redirect("Product.aspx?Id=" + e.CommandArgument);
            }
            else
                if (e.CommandName == "DeleteProduct")
                {
                    var msg = Utility.AesEncrypt("آیا از حذف محصول اطمینان دارید؟");
                    var table = Utility.AesEncrypt("Products");
                    Response.Redirect("Confirmation.aspx?Id=" + e.CommandArgument + "&m=" + msg + "&t=" + table);
                }
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            Response.Redirect("Product.aspx");
        }

        protected void gridProduct_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridProduct.PageIndex = e.NewPageIndex;
            gridProduct.DataSource = Session["Result"];
            gridProduct.DataBind();
        }
    }
}