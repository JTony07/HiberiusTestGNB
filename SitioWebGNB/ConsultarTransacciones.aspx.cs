﻿using System;
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
        GNBServiciosNegocio.ServiceClient pServicio = new GNBServiciosNegocio.ServiceClient();
        Uri uriHerokuC = new Uri ("http://quiet-stone-2094.herokuapp.com/rates.xml");
        Uri uriHerokuP = new Uri("http://quiet-stone-2094.herokuapp.com/transactions.xml");

        //pServicio.LimpiarProductos();

        //pServicio.ConversionesEnLinea(uriHerokuC.ToString());
        
        //pServicio.TransaccionesEnLinea(uriHerokuP.ToString()); //ACTUALIZA LA BASE DE DATOS
        //pServicio.LimpiarProductos(); FUNCIONA

        string ResultadoXML = pServicio.ConsultaXML(uriHerokuP.ToString());

        string prueba = pServicio.ListaTransacciones(ResultadoXML);

        XmlSerializer pSerializador = new XmlSerializer(typeof(TransacCollection));
        StringReader lector = new StringReader(prueba);
        TransacCollection ProductosEncontrados = (TransacCollection)pSerializador.Deserialize(lector);
        DropDownList1.DataSource = ProductosEncontrados;
        DropDownList1.DataValueField = "Sku";
        DropDownList1.DataBind();



    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        GNBServiciosNegocio.ServiceClient pServicio = new GNBServiciosNegocio.ServiceClient();
        Uri uriHerokuP = new Uri("http://quiet-stone-2094.herokuapp.com/transactions.xml");

        string ResultadoXML = pServicio.ConsultaXML(uriHerokuP.ToString());

        string prueba = pServicio.ListaTransacciones(ResultadoXML);


        string PruebaResultadoBD = pServicio.ObtenerTransacciones();

        string ResultadoBusqueda = pServicio.BuscarTransacciones(PruebaResultadoBD, TextBox1.Text);





        XmlSerializer pSerializador = new XmlSerializer(typeof(TransacCollection));
        StringReader lector = new StringReader(ResultadoBusqueda);

        TransacCollection ProductosEncontrados = (TransacCollection)pSerializador.Deserialize(lector);
        GridView1.DataSource = ProductosEncontrados;
        GridView1.DataBind();



        //pServicio.ConsultaProductos(uriHerokuP.ToString());

        //GNBServiciosNegocio.ServiceClient pServicio = new GNBServiciosNegocio.ServiceClient();
        ////string BusquedaProductos = pServicio.BuscarProductos(TextBox1.Text);
        //string RecuperarLista = pServicio.ObtenerProductos();
        //XmlSerializer pSerializador = new XmlSerializer(typeof(ProductosCollection));
        ////StringReader lector = new StringReader(BusquedaProductos);
        //StringReader lector = new StringReader(RecuperarLista);
        //ProductosCollection ProductosEncontrados = (ProductosCollection)pSerializador.Deserialize(lector);
        //GridView1.DataSource = ProductosEncontrados;
        //GridView1.DataBind();
    }

    
}