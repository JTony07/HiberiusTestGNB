using Common;
using CapaDatos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Globalization;

public class Service : IService
{
    public void ActualizarConversiones(string mC)
    {
        //objeto que recibira los elementos serializados
        XmlSerializer pSerializador = new XmlSerializer(typeof(Conversiones));
        //se crea un lector el cual recibira los datos des-serializados
        StringReader lector = new StringReader(mC);
        //se castea el des-serializador y se obtienen los elementos
        Conversiones pConversiones = (Conversiones)pSerializador.Deserialize(lector);
        //se crea un objeto de clase ServicioProductos 
        ServicioConversiones pServicioConversiones = new ServicioConversiones();
        //se instancea la tabla que contiene los campos a modificar
        GNB_CONVERSIONES pDatos = new GNB_CONVERSIONES();
        NumberFormatInfo proveedorDecimal = new NumberFormatInfo();
        proveedorDecimal.NumberDecimalSeparator = "."; //se asigna el punto como separador
        //se almacenan los datos dentro de los campos correspondientes
        pDatos.ID_CONVERSION = pConversiones.Id_Conversion;
        pDatos.FROM_CURRENCY = pConversiones.From_Currency;
        pDatos.TO_CURRENCY = pConversiones.To_Currency;
        pDatos.RATE = Convert.ToDecimal(pConversiones.Rate, proveedorDecimal);
        //pDatos.RATE = Convert.ToString(pConversiones.Rate); //me equivoque al crear la columna deberia ser "decimal" o "money" ya que se esta trabajando con tasas de conversion de dinero
        //se ACTUALIZAN las conversiones al servicio
        pServicioConversiones.ActualizarConversiones(pDatos);
    }

    public void ActualizarTransacciones(string mP)
    {
        //objeto que recibira los elementos serializados
        XmlSerializer pSerializador = new XmlSerializer(typeof(Transac));
        //se crea un lector el cual recibira los datos des-serializados
        StringReader lector = new StringReader(mP);
        //se castea el des-serializador y se obtienen los elementos
        Transac pTransac = (Transac)pSerializador.Deserialize(lector);
        //se crea un objeto de clase ServicioProductos 
        ServicioTransacciones pServicioProductos = new ServicioTransacciones();
        //se instancea la tabla que contiene los campos a modificar
        GNB_TRANSAC pDatos = new GNB_TRANSAC();
        NumberFormatInfo proveedorDecimal = new NumberFormatInfo();
        proveedorDecimal.NumberDecimalSeparator = "."; //se asigna el punto como separador
        //se almacenan los datos dentro de los campos correspondientes
        pDatos.ID_PRODUCT = pTransac.Id_Product;
        //pDatos.ID_PRODUCTOS = pProductos.Id_Productos;
        pDatos.SKU = pTransac.Sku;
        pDatos.AMOUNT = Convert.ToDecimal(pTransac.Amount, proveedorDecimal);
        //pDatos.AMMOUNT = Convert.ToDecimal(pProductos.Ammount);
        pDatos.CURRENCY = pTransac.Currency;
        //se ACTUALIZAN los productos al servicio
        pServicioProductos.ActualizarProductos(pDatos);
    }

    public void AgregarConversiones(string mC)
    {
        //objeto que recibira los elementos serializados
        XmlSerializer pSerializador = new XmlSerializer(typeof(Conversiones));
        //se crea un lector el cual recibira los datos des-serializados
        StringReader lector = new StringReader(mC);
        //se castea el des-serializador y se obtienen los elementos
        Conversiones pConversiones = (Conversiones)pSerializador.Deserialize(lector);
        //se crea un objeto de clase ServicioProductos 
        ServicioConversiones pServicioConversiones = new ServicioConversiones();
        //se instancea la tabla que contiene los campos a modificar
        GNB_CONVERSIONES pDatos = new GNB_CONVERSIONES();
        NumberFormatInfo proveedorDecimal = new NumberFormatInfo();
        proveedorDecimal.NumberDecimalSeparator = "."; //se asigna el punto como separador
        //se almacenan los datos dentro de los campos correspondientes
        pDatos.ID_CONVERSION = pConversiones.Id_Conversion;
        pDatos.FROM_CURRENCY = pConversiones.From_Currency;
        pDatos.TO_CURRENCY = pConversiones.To_Currency;
        pDatos.RATE = Convert.ToDecimal(pConversiones.Rate,proveedorDecimal);
        //pDatos.RATE = Convert.ToString(pConversiones.Rate); //me equivoque al crear la columna deberia ser "decimal" o "money" ya que se esta trabajando con tasas de conversion de dinero
        //se agregan las conversiones al servicio
        pServicioConversiones.AgregarConversiones(pDatos);
    }

    public void AgregarTransacciones(string mP)
    {
        //objeto que recibira los elementos serializados
        XmlSerializer pSerializador = new XmlSerializer(typeof(Transac));
        //se crea un lector el cual recibira los datos des-serializados
        StringReader lector = new StringReader(mP);
        //se castea el des-serializador y se obtienen los elementos
        Transac pTransac = (Transac)pSerializador.Deserialize(lector);
        //se crea un objeto de clase ServicioProductos 
        ServicioTransacciones pServicioProductos = new ServicioTransacciones();
        //se instancea la tabla que contiene los campos a modificar
        GNB_TRANSAC pDatos = new GNB_TRANSAC();
        NumberFormatInfo proveedorDecimal = new NumberFormatInfo();
        proveedorDecimal.NumberDecimalSeparator = "."; //se asigna el punto como separador
        //se almacenan los datos dentro de los campos correspondientes
        pDatos.ID_PRODUCT = pTransac.Id_Product;
        //pDatos.ID_PRODUCTOS = pProductos.Id_Productos;
        pDatos.SKU = pTransac.Sku;
        pDatos.AMOUNT = Convert.ToDecimal(pTransac.Amount, proveedorDecimal); 
        //pDatos.AMMOUNT = Convert.ToDecimal(pProductos.Ammount);
        pDatos.CURRENCY = pTransac.Currency;
        //se agregan los productos al servicio
        pServicioProductos.AgregarProducto(pDatos);
    }


    public string ObtenerConversiones()
    {
        //se crea una instancia del servicio 
        ServicioConversiones pServicioConversiones = new ServicioConversiones();
        //se crea una lista que almacena los elementos dentro de la tabla
        List<GNB_CONVERSIONES> pConversiones = pServicioConversiones.ObtenerConversiones();
        //se crea una instancia del la lista donde deberan estar los elementos de la base de datos
        ConversionesCollection pConversionesCollection = new ConversionesCollection();
        //se realiza un ciclo donde cada elemento se traspasara desde la base de datos hasta el objeto que lo contendra
        //este objeto es la instancia de tipo "collection"
        NumberFormatInfo proveedorDecimal = new NumberFormatInfo();
        proveedorDecimal.NumberDecimalSeparator = "."; //se asigna el punto como separador
        foreach (GNB_CONVERSIONES indice in pConversiones)
        {
            Conversiones iComun = new Conversiones();
            iComun.Id_Conversion = indice.ID_CONVERSION;
            iComun.From_Currency = indice.FROM_CURRENCY;
            iComun.To_Currency = indice.TO_CURRENCY;
            iComun.Rate = Convert.ToDouble(indice.RATE,proveedorDecimal);
            pConversionesCollection.Add(iComun);
        }
        //se crea un serializador de tipo colection (lista)
        XmlSerializer pSerializador = new XmlSerializer(typeof(ConversionesCollection));
        //se instancia un nuevo escritor de cadenas
        StringWriter escritor = new StringWriter();
        //se serializa la cadena
        pSerializador.Serialize(escritor, pConversionesCollection);
        //se retorna el escritor de tipo string
        return escritor.ToString();
    }

    //posee la misma logica que el ObtenerConversiones()
    public string ObtenerTransacciones()
    {
        ServicioTransacciones pServicioProductos = new ServicioTransacciones();
        List<GNB_TRANSAC> pProductos = pServicioProductos.ObtenerProductos();
        TransacCollection pProductosCollection = new TransacCollection();
        NumberFormatInfo proveedorDecimal = new NumberFormatInfo();
        proveedorDecimal.NumberDecimalSeparator = "."; //se asigna el punto como separador
        foreach (GNB_TRANSAC indice in pProductos)
        {
            Transac iComun = new Transac();
            iComun.Id_Product = indice.ID_PRODUCT;
            //iComun.Id_Productos = indice.ID_PRODUCTOS;
            iComun.Sku = indice.SKU;
            iComun.Amount = Convert.ToDouble(indice.AMOUNT, proveedorDecimal);
            //iComun.Ammount = Convert.ToDouble(indice.AMMOUNT);
            iComun.Currency = indice.CURRENCY;
            pProductosCollection.Add(iComun);
        }
        XmlSerializer pSerializador = new XmlSerializer(typeof(TransacCollection));
        StringWriter escritor = new StringWriter();
        pSerializador.Serialize(escritor, pProductosCollection);
        return escritor.ToString();
    }

    public void ConversionesEnLinea(string xmlLink)
    {
        try
        {   //se crea un objeto de Tipo XMLDocument que permite descargar almacenado en cache los datos del servidor
            XmlDocument DocumentoXml = new XmlDocument();
            //se carga el archivo del link proporcionado
            DocumentoXml.Load(xmlLink);
            //se selecciona el primer elemento padre dentro del XML
            XmlElement ElementoRaiz = DocumentoXml.DocumentElement;
            //se revisan los nodos del elemento raiz
            XmlNodeList Nodos = ElementoRaiz.ChildNodes;
            //se crea un objeto de tipo collection para almacenar la tabla que se solicita desde heroku
            ConversionesCollection pConversionesEnLinea = new ConversionesCollection();
            //se crea una instancia del servicio para consultar si la base de datos esta vacia o llena
            ServicioConversiones pServicioConversiones = new ServicioConversiones();

            List<GNB_CONVERSIONES> pConversiones = new List<GNB_CONVERSIONES>();

            List<GNB_CONVERSIONES> TablaConversiones = pServicioConversiones.ObtenerConversiones();

            GNB_CONVERSIONES pConversion = new GNB_CONVERSIONES();

            NumberFormatInfo proveedorDecimal = new NumberFormatInfo();
            proveedorDecimal.NumberDecimalSeparator = "."; //se asigna el punto como separador

            //se recorren todos los nodos de la raiz
            for (int pNodos = 0; pNodos < Nodos.Count; pNodos++)
            {
                //se crea un objeto de tipo CONVERSIONES que almacenara los datos de cada "rate" en el XML
                Conversiones pConversionesActuales = new Conversiones();
                //se selecciona un nodo de la raiz
                XmlNode NodoActual = Nodos.Item(pNodos);
                //se extraen todos los datos almacenados dentro del nodo
                XmlElement Dato = (XmlElement)NodoActual;
                //se extraen los atributos "from" "to" "rate"
                XmlAttribute Atributo1 = Dato.GetAttributeNode("from");
                XmlAttribute Atributo2 = Dato.GetAttributeNode("to");
                XmlAttribute Atributo3 = Dato.GetAttributeNode("rate");
                pConversion.ID_CONVERSION = pNodos + 1;
                pConversion.FROM_CURRENCY = Atributo1.InnerText.ToString();
                pConversion.TO_CURRENCY = Atributo2.InnerText.ToString();
                pConversion.RATE = Convert.ToDecimal(Atributo3.InnerText.ToString(), proveedorDecimal);
                //pConversion.RATE = Atributo3.InnerText.ToString();

                if (TablaConversiones.Count == 0) pServicioConversiones.AgregarConversiones(pConversion);
                else pServicioConversiones.ActualizarConversiones(pConversion);
            }
        }
        catch (Exception Ex)
        {

        }
    }


    public void TransaccionesEnLinea(string xmlLink)
    {
        try
        {   
            XmlDocument DocumentoXml = new XmlDocument();
            DocumentoXml.Load(xmlLink);         
            XmlElement ElementoRaiz = DocumentoXml.DocumentElement;
            XmlNodeList Nodos = ElementoRaiz.ChildNodes;
            //se crea un formateador para indicar si el separador es un punto o una coma en los numeros del RATE
            NumberFormatInfo proveedorDecimal = new NumberFormatInfo();
            proveedorDecimal.NumberDecimalSeparator = "."; //se asigna el punto como separador
            ServicioTransacciones pServicioProductos = new ServicioTransacciones();
            List<GNB_TRANSAC> pTransacciones = new List<GNB_TRANSAC>();
            List<GNB_TRANSAC> TablaProductos = pServicioProductos.ObtenerProductos();
            GNB_TRANSAC pTransac = new GNB_TRANSAC();

            for (int pNodos = 0; pNodos < Nodos.Count; pNodos++)
            {
                Conversiones pConversionesActuales = new Conversiones();
                XmlNode NodoActual = Nodos.Item(pNodos);
                XmlElement Dato = (XmlElement)NodoActual;
                XmlAttribute Atributo1 = Dato.GetAttributeNode("sku");
                XmlAttribute Atributo2 = Dato.GetAttributeNode("amount");
                XmlAttribute Atributo3 = Dato.GetAttributeNode("currency");
                pTransac.ID_PRODUCT = pNodos + 1;
                //pProducto.ID_PRODUCTOS = pNodos + 1;
                pTransac.SKU = Atributo1.InnerText.ToString();
                pTransac.AMOUNT = Convert.ToDecimal(Atributo2.InnerText.ToString(), proveedorDecimal);
                //pProducto.AMMOUNT = Convert.ToDecimal(Atributo2.InnerText.ToString(), proveedorDecimal);
                pTransac.CURRENCY = Atributo3.InnerText.ToString();

                if (TablaProductos.Count == 0) pServicioProductos.AgregarProducto(pTransac);
                //else pServicioProductos.ActualizarProductos(pProducto);
            }
        }
        catch (Exception Ex)
        {

        }
    }

    public string BuscarTransacciones(string mP, string Busqueda)
    {
        Transac TransaccionABuscar = new Transac();
        TransaccionABuscar.Sku = Busqueda;

        XmlSerializer pSerializador = new XmlSerializer(typeof(TransacCollection));
        StringReader lector = new StringReader(mP);
        TransacCollection pTransaccionesEncontradas = (TransacCollection)pSerializador.Deserialize(lector);
        TransacCollection pTransaccionesCoincidiencias = new TransacCollection();

        NumberFormatInfo proveedorDecimal = new NumberFormatInfo();
        proveedorDecimal.NumberDecimalSeparator = "."; //se asigna el punto como separador

        foreach (Transac indice in pTransaccionesEncontradas)
        {
            Transac iComun = new Transac();
            if(TransaccionABuscar.Sku == indice.Sku)
            {
                iComun.Id_Product = indice.Id_Product;
                iComun.Sku = indice.Sku;
                iComun.Amount = Convert.ToDouble(indice.Amount, proveedorDecimal);
                iComun.Currency = indice.Currency;
                pTransaccionesCoincidiencias.Add(iComun);
            }
        }

        StringWriter escritor = new StringWriter();
        pSerializador.Serialize(escritor, pTransaccionesCoincidiencias);
        return escritor.ToString();
    }

    //public string ConsultaProductos(string Producto, List<GNB_TRANSAC> ListaProductos)
    //{
    //    ServicioTransacciones pServicioProductos = new ServicioTransacciones();

    //    TransacCollection pProductosCollection = new TransacCollection();

    //    NumberFormatInfo proveedorDecimal = new NumberFormatInfo();
    //    proveedorDecimal.NumberDecimalSeparator = "."; //se asigna el punto como separador

    //    foreach (GNB_PRODUCTOS indice in ListaProductos)
    //    {
    //        Transac iComun = new Transac();
    //        if(Producto == indice.SKU.ToString())
    //        {
    //            iComun.Id_Productos = indice.ID_PRODUCTS;
    //            iComun.Sku = indice.SKU;
    //            iComun.Ammount = Convert.ToDouble(indice.AMOUNT, proveedorDecimal);
    //            iComun.Currency = indice.CURRENCY;
    //            pProductosCollection.Add(iComun);
    //        }
    //    }

    //    XmlSerializer pSerializador = new XmlSerializer(typeof(TransacCollection));
    //    StringWriter escritor = new StringWriter();
    //    pSerializador.Serialize(escritor, pProductosCollection);
    //    return escritor.ToString();
    //}


    /// <summary>
    /// Consulta a un LINK de HEROKU por los parametros de SKU, AMOUNT y CURRENCY
    /// para luego crear una cadena de datos para trabajar con ella
    /// </summary>
    /// <param name="xmlLink">debe ser el link de heroku de las transacciones</param>
    /// <returns>Cadena de caracteres con el contenido del documento XML serializado</returns>
    public string ConsultaXML(string xmlLink)
    {
        XmlDocument DocumentoXml = new XmlDocument(); //variable para almacenar un link y abrirlo como XML
        DocumentoXml.Load(xmlLink); //carga el archivo XML
        XmlElement ElementoRaiz = DocumentoXml.DocumentElement; //elemento raiz principal
        XmlNodeList Nodos = ElementoRaiz.ChildNodes; //hijos de ese raiz

        NumberFormatInfo proveedorDecimal = new NumberFormatInfo(); //tipo de decimal
        proveedorDecimal.NumberDecimalSeparator = "."; //se asigna el punto como separador decimal

        TransacCollection pTransaccionesCollection = new TransacCollection();

        for (int pNodos = 0; pNodos < Nodos.Count; pNodos++)
        {
            Transac pTransac = new Transac();

            XmlNode NodoActual = Nodos.Item(pNodos);
            XmlElement Dato = (XmlElement)NodoActual;
            XmlAttribute Atributo1 = Dato.GetAttributeNode("sku");
            XmlAttribute Atributo2 = Dato.GetAttributeNode("amount");
            XmlAttribute Atributo3 = Dato.GetAttributeNode("currency");

            pTransac.Id_Product = pNodos + 1;
            pTransac.Sku = Atributo1.InnerText.ToString();
            pTransac.Amount = Convert.ToDouble(Atributo2.InnerText.ToString(), proveedorDecimal);
            pTransac.Currency = Atributo3.InnerText.ToString();

            pTransaccionesCollection.Add(pTransac);
        }

        XmlSerializer pSerializador = new XmlSerializer(typeof(TransacCollection));
        StringWriter escritor = new StringWriter();
        pSerializador.Serialize(escritor, pTransaccionesCollection);
        return escritor.ToString();
    }


    /// <summary>
    /// elimina todos los elementos de la tabla productos
    /// </summary>
    public void LimpiarTransacciones()
    {
        ServicioTransacciones pServicioTrnasacciones = new ServicioTransacciones();
        pServicioTrnasacciones.LimpiarProductos();
    }

    public string ListaTransacciones(string mTransaccionesXML)
    {
        XmlSerializer pSerializador = new XmlSerializer(typeof(TransacCollection));
        StringReader lector = new StringReader(mTransaccionesXML);
        TransacCollection pTransaccionesXML = (TransacCollection)pSerializador.Deserialize(lector);

        TransacCollection pTransaccionesExtraidas = new TransacCollection();

        NumberFormatInfo proveedorDecimal = new NumberFormatInfo();
        proveedorDecimal.NumberDecimalSeparator = "."; //se asigna el punto como separador

        //foreach(Transac Fila in pTransaccionesXML)
        //{
        //    foreach(Transac FilaInterna in pTransaccionesExtraidas)
        //    {

        //    }

        //}
 
        for (int index1 = 0; index1 < pTransaccionesXML.Count; index1++)
        {
            Transac Fila = pTransaccionesXML.ElementAt(index1); 
            for (int index2 = 0; index2 <= pTransaccionesExtraidas.Count; index2++)
            {

                if (pTransaccionesExtraidas.Count == 0)
                {
                    Transac FilaAuxiliar = new Transac();
                    FilaAuxiliar.Id_Product = Fila.Id_Product;
                    FilaAuxiliar.Sku = Fila.Sku;
                    FilaAuxiliar.Amount = Fila.Amount;
                    FilaAuxiliar.Currency = Fila.Currency;
                    pTransaccionesExtraidas.Add(FilaAuxiliar);
                    break;
                }
                else if (index2 == pTransaccionesExtraidas.Count) break;
                else
                {
                    
                    Transac FilaInterna = pTransaccionesExtraidas.ElementAt(index2);
                    
                    if (Fila.Sku.ToString() != FilaInterna.Sku.ToString())
                    {
                        FilaInterna.Id_Product = Fila.Id_Product;
                        FilaInterna.Sku = Fila.Sku;
                        FilaInterna.Amount = Fila.Amount;
                        FilaInterna.Currency = Fila.Currency;
                        pTransaccionesExtraidas.Add(FilaInterna);
                        break;
                    }
                }

                
            }
        }
    
        StringWriter escritor = new StringWriter();
        pSerializador.Serialize(escritor, pTransaccionesExtraidas);
        return escritor.ToString();
    }
}
