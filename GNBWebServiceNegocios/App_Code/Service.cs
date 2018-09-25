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
using System.Collections;

public class Service : IService
{
    /// <summary>
    /// Servicio que permite ACTUALIZAR la tabla GNB_CONVERSIONES con elementos dentro de un String de tipo XML
    /// </summary>
    /// <param name="mC">Cadena de caracteres de tipo XML</param>
    public void ActualizarConversiones(string mC)
    {
        XmlSerializer pSerializador = new XmlSerializer(typeof(Conversiones)); //objeto que recibira los elementos serializados
        StringReader lector = new StringReader(mC); //se crea un lector el cual recibira los datos des-serializados
        Conversiones pConversiones = (Conversiones)pSerializador.Deserialize(lector); //se castea el des-serializador y se obtienen los elementos
        ServicioConversiones pServicioConversiones = new ServicioConversiones(); //se crea un objeto de clase ServicioProductos 
        GNB_CONVERSIONES pDatos = new GNB_CONVERSIONES(); //se instancea la tabla que contiene los campos a modificar


        //PROVEEDOR DE SIGNO DECIMAL
        NumberFormatInfo proveedorDecimal = new NumberFormatInfo();
        proveedorDecimal.NumberDecimalSeparator = "."; 

        //ALMACENAMIENTO DE VARIABLES DESERIALIZADAS
        pDatos.ID_CONVERSION = pConversiones.Id_Conversion;
        pDatos.FROM_CURRENCY = pConversiones.From_Currency;
        pDatos.TO_CURRENCY = pConversiones.To_Currency;
        pDatos.RATE = Convert.ToDecimal(pConversiones.Rate, proveedorDecimal);

        //se ACTUALIZAN las conversiones al servicio
        pServicioConversiones.ActualizarConversiones(pDatos);
    }

    /// <summary>
    /// Servicio que permite ACTUALIZAR la tabla GNB_TRANSAC con elementos dentro de un String de tipo XML
    /// </summary>
    /// <param name="mC">Cadena de caracteres de tipo XML</param>
    public void ActualizarTransacciones(string mP)
    {
        
        XmlSerializer pSerializador = new XmlSerializer(typeof(Transac));//objeto que recibira los elementos serializados
        StringReader lector = new StringReader(mP);//se crea un lector el cual recibira los datos des-serializados
        Transac pTransac = (Transac)pSerializador.Deserialize(lector); //se castea el des-serializador y se obtienen los elementos
        ServicioTransacciones pServicioProductos = new ServicioTransacciones(); //se crea un objeto de clase ServicioProductos 
        GNB_TRANSAC pDatos = new GNB_TRANSAC(); //se instancea la tabla que contiene los campos a modificar


        //PROVEEDOR DE SIGNO DECIMAL
        NumberFormatInfo proveedorDecimal = new NumberFormatInfo();
        proveedorDecimal.NumberDecimalSeparator = ".";

        //ALMACENAMIENTO DE VARIABLES DESERIALIZADAS
        pDatos.ID_PRODUCT = pTransac.Id_Product;
        pDatos.SKU = pTransac.Sku;
        pDatos.AMOUNT = Convert.ToDecimal(pTransac.Amount, proveedorDecimal);
        pDatos.CURRENCY = pTransac.Currency;
        
        //se ACTUALIZAN los productos al servicio
        pServicioProductos.ActualizarTransaccion(pDatos);
    }

    /// <summary>
    /// Servicio que permite AGREGAR la tabla GNB_CONVERSIONES con elementos dentro de un String de tipo XML
    /// </summary>
    /// <param name="mC">Cadena de caracteres de tipo XML</param>
    public void AgregarConversiones(string mC)
    {
        ServicioConversiones pServicioProductos = new ServicioConversiones(); //servicio que permite comunicar con BD

        XmlSerializer pSerializador = new XmlSerializer(typeof(ConversionesCollection)); //objeto que recibira los elementos serializados
        StringReader lector = new StringReader(mC); //se crea un lector el cual recibira los datos des-serializados
        ConversionesCollection pConversiones = (ConversionesCollection)pSerializador.Deserialize(lector); //se castea el des-serializador y se obtienen los elementos

        //PROVEEDOR DE SIGNO DECIMAL
        NumberFormatInfo proveedorDecimal = new NumberFormatInfo();
        proveedorDecimal.NumberDecimalSeparator = ".";

        for(int indice = 0; indice < pConversiones.Count; indice++)
        {
            GNB_CONVERSIONES pDatos = new GNB_CONVERSIONES(); //se instancea la tabla que contiene los campos a modificar
            Conversiones pConversion = pConversiones.ElementAt(indice);

            //ALMACENAMIENTO DE VARIABLES DESERIALIZADAS
            pDatos.ID_CONVERSION = indice + 1;
            pDatos.FROM_CURRENCY = pConversion.From_Currency;
            pDatos.TO_CURRENCY = pConversion.To_Currency;
            pDatos.RATE = Convert.ToDecimal(pConversion.Rate, proveedorDecimal);
            
            //se agregan los productos al servicio
            pServicioProductos.AgregarConversiones(pDatos);
        }
    }

    /// <summary>
    /// Servicio que permite AGREGAR la tabla GNB_TRANSAC con elementos dentro de un String de tipo XML
    /// </summary>
    /// <param name="mC">Cadena de caracteres de tipo XML</param>
    public void AgregarTransacciones(string mP)
    {
        ServicioTransacciones pServicioProductos = new ServicioTransacciones(); //servicio que permite comunicar con BD

        XmlSerializer pSerializador = new XmlSerializer(typeof(TransacCollection)); //objeto que recibira los elementos serializados
        StringReader lector = new StringReader(mP); //se crea un lector el cual recibira los datos des-serializados
        TransacCollection pTransacciones = (TransacCollection)pSerializador.Deserialize(lector); //se castea el des-serializador y se obtienen los elementos

        //PROVEEDOR DE SIGNO DECIMAL
        NumberFormatInfo proveedorDecimal = new NumberFormatInfo();
        proveedorDecimal.NumberDecimalSeparator = ".";

        for (int indice = 0; indice < pTransacciones.Count; indice++)
        {
            GNB_TRANSAC pDatos = new GNB_TRANSAC(); //se instancea la tabla que contiene los campos a modificar
            Transac pTransaccion = pTransacciones.ElementAt(indice);

            //ALMACENAMIENTO DE VARIABLES DESERIALIZADAS
            pDatos.ID_PRODUCT = indice + 1;
            pDatos.SKU = pTransaccion.Sku;
            pDatos.AMOUNT = Convert.ToDecimal(pTransaccion.Amount, proveedorDecimal); 
            pDatos.CURRENCY = pTransaccion.Currency;

            //se agregan los productos al servicio
            pServicioProductos.AgregarTransaccion(pDatos);
        }
    }


    /// <summary>
    /// se OBTIENEN o RECUPERAN todos los elementos dentro de la tabla GNB_CONVERSIONES
    /// </summary>
    /// <returns>candena de caracteres XML que contienen los elementos dentro de la tabla</returns>
    public string ObtenerConversiones()
    {
        ServicioConversiones pServicioConversiones = new ServicioConversiones(); //se crea una instancia del servicio 
        List<GNB_CONVERSIONES> pConversiones = pServicioConversiones.ObtenerConversiones(); //se crea una lista que almacena los elementos dentro de la tabla
        ConversionesCollection pConversionesCollection = new ConversionesCollection(); //se crea una instancia del la lista donde deberan estar los elementos de la base de datos
        
        //PROVEEDOR DE SIGNO DECIMAL
        NumberFormatInfo proveedorDecimal = new NumberFormatInfo();
        proveedorDecimal.NumberDecimalSeparator = "."; 

        //se realiza un ciclo donde cada elemento se traspasara desde la base de datos hasta el objeto que lo contendra
        //este objeto es la instancia de tipo "collection"
        foreach (GNB_CONVERSIONES indice in pConversiones)
        {
            Conversiones iComun = new Conversiones();
            iComun.Id_Conversion = indice.ID_CONVERSION;
            iComun.From_Currency = indice.FROM_CURRENCY;
            iComun.To_Currency = indice.TO_CURRENCY;
            iComun.Rate = Convert.ToDouble(indice.RATE,proveedorDecimal);
            pConversionesCollection.Add(iComun);
        }
        
        
        XmlSerializer pSerializador = new XmlSerializer(typeof(ConversionesCollection)); //se crea un serializador de tipo colection (lista)
        StringWriter escritor = new StringWriter();//se instancia un nuevo escritor de cadenas
        pSerializador.Serialize(escritor, pConversionesCollection);//se serializa la cadena
        return escritor.ToString(); //se retorna el escritor de tipo string
    }

    /// <summary>
    /// se OBTIENEN o RECUPERAN todos los elementos dentro de la tabla GNB_TRANSAC
    /// </summary>
    /// <returns>candena de caracteres XML que contienen los elementos dentro de la tabla</returns>
    public string ObtenerTransacciones()
    {
        ServicioTransacciones pServicioProductos = new ServicioTransacciones(); //se crea una instancia del servicio 
        List<GNB_TRANSAC> pProductos = pServicioProductos.ObtenerTransaccion();//se crea una lista que almacena los elementos dentro de la tabla
        TransacCollection pProductosCollection = new TransacCollection();//se crea una instancia del la lista donde deberan estar los elementos de la base de datos

        //PROVEEDOR DE SIGNO DECIMAL
        NumberFormatInfo proveedorDecimal = new NumberFormatInfo();
        proveedorDecimal.NumberDecimalSeparator = "."; //se asigna el punto como separador


        //se realiza un ciclo donde cada elemento se traspasara desde la base de datos hasta el objeto que lo contendra
        //este objeto es la instancia de tipo "collection"
        foreach (GNB_TRANSAC indice in pProductos)
        {
            Transac iComun = new Transac();
            iComun.Id_Product = indice.ID_PRODUCT;
            iComun.Sku = indice.SKU;
            iComun.Amount = Convert.ToDouble(indice.AMOUNT, proveedorDecimal);
            iComun.Currency = indice.CURRENCY;
            pProductosCollection.Add(iComun);
        }

        XmlSerializer pSerializador = new XmlSerializer(typeof(TransacCollection));//se crea un serializador de tipo colection (lista)
        StringWriter escritor = new StringWriter();//se instancia un nuevo escritor de cadenas
        pSerializador.Serialize(escritor, pProductosCollection);//se serializa la cadena
        return escritor.ToString(); //se retorna el escritor de tipo string
    }

    /// <summary>
    /// Permite la busqueda de las transacciones de un SKU
    /// </summary>
    /// <param name="mP">Cadena XML donde se encuentran los datos de la tabla GNB_TRANSAC</param>
    /// <param name="Busqueda">SKU A Buscar</param>
    /// <returns>TODAS LAS TRANSACCIONES REALIZADAS CON ESE SKU</returns>
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


    /// <summary>
    /// Consulta a un LINK de HEROKU por los parametros de SKU, AMOUNT y CURRENCY
    /// para luego crear una cadena de datos para trabajar con ella
    /// </summary>
    /// <param name="xmlLink">debe ser el link de heroku de las transacciones</param>
    /// <returns>Cadena de caracteres con el contenido del documento XML serializado</returns>
    public string ConsultaXMLTransac(string xmlLink)
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
    /// Consulta a un LINK de HEROKU por los parametros de FROM, TO y RATE
    /// para luego crear una cadena de datos para trabajar con ella
    /// </summary>
    /// <param name="xmlLink">debe ser el link de heroku de las transacciones</param>
    /// <returns>Cadena de caracteres con el contenido del documento XML serializado</returns>
    public string ConsultaXMLConver(string xmlLink)
    {
        XmlDocument DocumentoXml = new XmlDocument(); //variable para almacenar un link y abrirlo como XML
        DocumentoXml.Load(xmlLink); //carga el archivo XML
        XmlElement ElementoRaiz = DocumentoXml.DocumentElement; //elemento raiz principal
        XmlNodeList Nodos = ElementoRaiz.ChildNodes; //hijos de ese raiz

        NumberFormatInfo proveedorDecimal = new NumberFormatInfo(); //tipo de decimal
        proveedorDecimal.NumberDecimalSeparator = "."; //se asigna el punto como separador decimal

        ConversionesCollection pConversionesCollection = new ConversionesCollection(); //es donde se almacenaran las conversiones encontradas en el XML
        
        //ciclo para recorrer los nodos dentro de la consulta XML
        for (int pNodos = 0; pNodos < Nodos.Count; pNodos++)
        {
            Conversiones pConversion = new Conversiones();

            XmlNode NodoActual = Nodos.Item(pNodos);
            XmlElement Dato = (XmlElement)NodoActual;
            XmlAttribute Atributo1 = Dato.GetAttributeNode("from");
            XmlAttribute Atributo2 = Dato.GetAttributeNode("to");
            XmlAttribute Atributo3 = Dato.GetAttributeNode("rate");

            pConversion.Id_Conversion = pNodos + 1;
            pConversion.From_Currency = Atributo1.InnerText.ToString();
            pConversion.To_Currency = Atributo2.InnerText.ToString();
            pConversion.Rate = Convert.ToDouble(Atributo3.InnerText.ToString(), proveedorDecimal);

            pConversionesCollection.Add(pConversion);
        }


        //segmento para seriralizar los resultados y entregar una cadena de caracteres
        XmlSerializer pSerializador = new XmlSerializer(typeof(ConversionesCollection));
        StringWriter escritor = new StringWriter();
        pSerializador.Serialize(escritor, pConversionesCollection);
        return escritor.ToString();
    }

    /// <summary>
    /// elimina todos los elementos de la tabla GNB_TRANSAC
    /// </summary>
    public void LimpiarTransacciones()
    {
        ServicioTransacciones pServicioTrnasacciones = new ServicioTransacciones();
        pServicioTrnasacciones.LimpiarTransaccion(); //limpia todos los campos de la tabla GNB_TRANSAC
    }

    /// <summary>
    /// elimina todos los elementos de la tabla GNB_CONVERSIONES
    /// </summary>
    public void LimpiarConversiones()
    {
        ServicioConversiones pServicioTrnasacciones = new ServicioConversiones();
        pServicioTrnasacciones.LimpiarConversiones(); //limpia todos los campos de la tabla GNB_CONVERSIONES
    }


    /// <summary>
    /// Obtiene una lista de transacciones (SKU) dentro de una cadena XML 
    /// </summary>
    /// <param name="mTransaccionesXML">Una cadena XML que almacena datos de Transacciones</param>
    /// <returns>los elementos SKU dentro de la tabla</returns>
    public string ListaTransacciones(string mTransaccionesXML)
    {
        //segmento para deserializar la cadena de caracteres pasada como parametro
        XmlSerializer pSerializador = new XmlSerializer(typeof(TransacCollection));
        StringReader lector = new StringReader(mTransaccionesXML);
        TransacCollection pTransaccionesXML = (TransacCollection)pSerializador.Deserialize(lector);

        TransacCollection pTransaccionesExtraidas = new TransacCollection(); //elemento buffer de las transacciones solicitadas

        NumberFormatInfo proveedorDecimal = new NumberFormatInfo();
        proveedorDecimal.NumberDecimalSeparator = "."; //se asigna el punto como separador

        //ciclo para recorrer los elementos dentro del XML y crear un buffer con solo un SKU por elemento existente
        for (int index1 = 0; index1 < pTransaccionesXML.Count; index1++)
        {
            Transac Fila = pTransaccionesXML.ElementAt(index1); 
            for (int index2 = 0; index2 <= pTransaccionesExtraidas.Count; index2++)
            {
                //si el objeto "pTransaccionesExtraidas esta vacio entonces guarda el primer elemento
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
                else if (index2 == pTransaccionesExtraidas.Count) break; //si el elemento a evaluar es el ultimo se salta (ya que el recorrido debe ser menor al .count del objeto ya que comienza en 0)
                else
                {
                    Transac FilaInterna = new Transac();//objeto que servira de buffer momentaneo

                    //se pregunta con una consulta si el SKU existe dentro del buffer al menos una vez
                    //si no existe entonces se guarda
                    if (pTransaccionesExtraidas.Any(o => o.Sku == Fila.Sku)) break;
                    else
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
    
        //segmento para serializar el resultado y enviarlo como cadena de caracteres
        StringWriter escritor = new StringWriter();
        pSerializador.Serialize(escritor, pTransaccionesExtraidas);
        return escritor.ToString();
    }

    /// <summary>
    /// Permite totalizar todos los elementos en la transaccion del campo "AMOUNT" en EUR haciendo
    /// las conversiones respectivas segun la tabla HEROKU de conversiones
    /// </summary>
    /// <param name="pTransaccionesXML"> cadena de caracteres de tipo XML que contenga transacciones</param>
    /// <returns>el total en formato redondeado "AwayFromZero"</returns>
    public double  TotalizadoEUR(string pTransaccionesXML)
    {
        //segmento para deserializar el XML entrante como parametro
        XmlSerializer pSerializadorTransac = new XmlSerializer(typeof(TransacCollection));
        StringReader LectorTransac = new StringReader(pTransaccionesXML);
        TransacCollection pTransacciones = (TransacCollection)pSerializadorTransac.Deserialize(LectorTransac);

        //PROVEEDOR DECIMAL
        NumberFormatInfo proveedorDecimal = new NumberFormatInfo();
        proveedorDecimal.NumberDecimalSeparator = "."; //se asigna el punto como separador

        //Servicios necesarios para consultar las conversiones almacenadas en la BD
        ServicioConversiones ServicioConversiones = new ServicioConversiones();
        List<GNB_CONVERSIONES> ListaConversiones = new List<GNB_CONVERSIONES>();

        ListaConversiones = ServicioConversiones.ObtenerConversiones(); //se obtienen las conversiones de la BD

        ArrayList TasasDeConeversiones = new ArrayList(); //Arreglo para almacenar solo los RATE de conversiones

        double Totalizar = 0; //variable de totalizado

        try
        {
            //por cada elemento en la tabla de conversion se extraera el campo "RATE"
            foreach (GNB_CONVERSIONES mConversion in ListaConversiones) TasasDeConeversiones.Add(Convert.ToDouble(mConversion.RATE, proveedorDecimal));

             //se almacenan las conversiones necesarias
             /* segun el XML  HEROKU
             *  0 CAD - EUR
             *  1 EUR - CAD
             *  2 CAD - USD
             *  3 USD - CAD
             *  4 EUR - AUD
             *  5 AUD - EUR
             */
            double USD_CAD = (double)TasasDeConeversiones[3];
            double CAD_EUR = (double)TasasDeConeversiones[0];
            double AUD_EUR = (double)TasasDeConeversiones[5];

            //por cada elemento en las transacciones seleccionadas se revisara el campo "CURRENCY"
            foreach (Transac pTransaccion in pTransacciones)
            {
                //SI EL CURRENCY ES DISTINTO DE EUR SE APLICA UNA CONVERSION
                switch (pTransaccion.Currency.ToString())
                {
                    case "USD": //pasa por CAD primero luego a EUR
                        Totalizar += (pTransaccion.Amount * USD_CAD) * CAD_EUR;
                        break;
                    case "CAD": //directo a EUR
                        Totalizar += (pTransaccion.Amount * USD_CAD) * CAD_EUR;
                        break;
                    case "AUD": //directo a EUR
                        Totalizar += pTransaccion.Amount * AUD_EUR;
                        break;
                    case "EUR":
                        Totalizar += pTransaccion.Amount;
                        break;
                    default:
                        break;
                }
            }
        }
        catch(Exception Ex)
        {

        }

        //se realiza un redondeo de tipo "bank" o Gaussiano
        Totalizar = Math.Round(Totalizar, 1, MidpointRounding.AwayFromZero);
        return Totalizar;
    }
}
