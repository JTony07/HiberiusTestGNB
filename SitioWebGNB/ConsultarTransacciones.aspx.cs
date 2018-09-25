using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Xml.Serialization;
using System.IO;

public partial class ConsultarTransacciones : System.Web.UI.Page
{ 

    protected void Page_Load(object sender, EventArgs e)
    {
        

        
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        GNBServiciosNegocio.ServiceClient pServicio = new GNBServiciosNegocio.ServiceClient();
        try
        {
            // SE BUSCAN TODAS LAS COINCIDENCIAS CON EL ELEMENTO SELECCIONADO DEL DROPDOWN
            string TransaccionesEnBD = pServicio.ObtenerTransacciones();

            string ResultadoBusqueda = pServicio.BuscarTransacciones(TransaccionesEnBD, DropDownList1.SelectedItem.ToString());
            double TotalizadoEUR = pServicio.TotalizadoEUR(TransaccionesEnBD);

            XmlSerializer pSerializador = new XmlSerializer(typeof(TransacCollection));
            StringReader lector = new StringReader(ResultadoBusqueda);
            TransacCollection TransaccionesEncontradas = (TransacCollection)pSerializador.Deserialize(lector);

            //MUESTRA EN EL LABEL2
            Label2.Visible = true;
            
            TotalizadoEUR = Math.Round(TotalizadoEUR, 1, MidpointRounding.AwayFromZero);
            
            Label2.Text = "TOTAL VENTAS DE TRANSACCION: " + DropDownList1.SelectedItem.ToString() + "<br>" + "ES: " + TotalizadoEUR.ToString() + " EUR"+ "<br>"; 

            //SE MUESTRA LA TABLA DE ELEMENTOS DE COINCIDENCIA SKU
            GridView1.DataSource = TransaccionesEncontradas;
            GridView1.DataBind();
        }
        catch(Exception Ex)
        {

        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Label2.Text = "";
        Label2.Visible = false;
        GNBServiciosNegocio.ServiceClient pServicio = new GNBServiciosNegocio.ServiceClient(); //se crea una instancia con los servicios ofrecidos
        Uri uriHerokuC = new Uri("http://quiet-stone-2094.herokuapp.com/rates.xml"); //link para las tasas de conversiones
        Uri uriHerokuT = new Uri("http://quiet-stone-2094.herokuapp.com/transactions.xml"); //link para las transacciones

        XmlSerializer pSerializador = new XmlSerializer(typeof(TransacCollection)); //objeto comun necesario para trabajar con EntityFramework
        StringReader lector; //un lector que nos permitira leer el documento serializado 
        TransacCollection TransaccionesEncontradas; //variable donde se vaciaran los datos de las Transacciones

        //INTENTO CON URL HEROKU PARA TRANSACCIONES
        try
        {
            //-----------------------------------------------------------------------------------------
            // CASO #1 : SE BUSCA POR LA INFORMACION DEL SERVIDOR HEROKU PARA ACTUALIZAR LA BASE DE DATOS
            //-----------------------------------------------------------------------------------------


            //PASO 1: SE EXTRAEN LOS ELEMENTOS EN FORMA DE XML EN CADENA DESDE LA PAGINA DE HEROKU PARA "TRANSACTIONS"
            string ResultadoXMLTransac = pServicio.ConsultaXMLTransac(uriHerokuT.ToString()); //se buscan los elementos del XML

            //PASO 2: SE LIMPIAN AMBAS BASES DE DATOS
            pServicio.LimpiarTransacciones();

            //PASO 3: SE ALMACENAN LOS NUEVOS VALORES EN LA BASE DE DATOS
            pServicio.AgregarTransacciones(ResultadoXMLTransac);

            //PASO 4: SE EXTRAEN LOS ELEMENTOS DISPONIBLES EN LA COLUMNA SKU (LOS QUE SE PUEDEN BUSCAR)
            string ListaDeTransacciones = pServicio.ListaTransacciones(ResultadoXMLTransac); //se buscan las transacciones disponibles

            //PASO 5: SE MUESTRAN LOS DIFERENTES SKU POSIBLES A BUSCAR
            lector = new StringReader(ListaDeTransacciones);
            TransaccionesEncontradas = (TransacCollection)pSerializador.Deserialize(lector);
            DropDownList1.DataSource = TransaccionesEncontradas;
            DropDownList1.DataValueField = "Sku";
            DropDownList1.DataBind();
        }
        catch (Exception Ex)
        {
            //-----------------------------------------------------------------------------------------
            // CASO #2 : EL SERVIDOR DE HEROKU NO RESPONDE Y POR ENDE HAY QUE BUSCAR LOS DATOS DENTRO DE LA BD
            //-----------------------------------------------------------------------------------------

            //PASO 1: SE BUSCAN LAS TRANSACCIONES DENTRO DE LA BASE DE DATOS
            string UltimasTransacciones = pServicio.ObtenerTransacciones();

            //PASO 2: SE EXTRAEN LOS ELEMENTOS DISPONIBLES EN LA COLUMNA SKU (LOS QUE SE PUEDEN BUSCAR)
            string ListaDeTransaccionesBD = pServicio.ListaTransacciones(UltimasTransacciones);

            //PASO 3: SE MUESRAN LOS DIFERENTES SKU POSIBLES A BUSCAR
            lector = new StringReader(ListaDeTransaccionesBD);
            TransaccionesEncontradas = (TransacCollection)pSerializador.Deserialize(lector);
            DropDownList1.DataSource = TransaccionesEncontradas;
            DropDownList1.DataTextField = "Sku";
            DropDownList1.DataValueField = "Sku";
            DropDownList1.DataBind();
        }

        //INTENTO CON URL HEROKU PARA CONVERSIONES
        try
        {
            //PASO 2: SE EXTRAEN LOS ELEMENTOS EN FORMA DE XML EN CADENA DESDE LA PAGINA HEROKU PARA "CONVERSIONS"
            string ResultadoXMLConver = pServicio.ConsultaXMLConver(uriHerokuC.ToString());

            //PASO 2: SE LIMPIAN AMBAS BASES DE DATOS
            pServicio.LimpiarConversiones();

            //PASO 3: SE ALMACENAN LOS NUEVOS VALORES EN LA BASE DE DATOS
            pServicio.AgregarConversiones(ResultadoXMLConver);
        }
        catch (Exception Ex)
        {

        }
    }
}