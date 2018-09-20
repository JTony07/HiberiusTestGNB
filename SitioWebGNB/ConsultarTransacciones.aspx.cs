using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;


public partial class ConsultarTransacciones : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GNBServiciosNegocio.ServiceClient pServicio = new GNBServiciosNegocio.ServiceClient();
        Uri uriHerokuC = new Uri ("http://quiet-stone-2094.herokuapp.com/rates.xml");
        Uri uriHerokuP = new Uri("http://quiet-stone-2094.herokuapp.com/transactions.xml");
        pServicio.ConversionesEnLinea(uriHerokuC.ToString());
        pServicio.ProductosEnLinea(uriHerokuP.ToString());
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        

    }
}