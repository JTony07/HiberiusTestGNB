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

// NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
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
        //se almacenan los datos dentro de los campos correspondientes
        pDatos.ID_CONVERSION = pConversiones.Id_Conversion;
        pDatos.FROM_CURRENCY = pConversiones.From_Currency;
        pDatos.TO_CURRENCY = pConversiones.To_Currency;
        pDatos.RATE = Convert.ToString(pConversiones.Rate); //me equivoque al crear la columna deberia ser "decimal" o "money" ya que se esta trabajando con tasas de conversion de dinero
        //se ACTUALIZAN las conversiones al servicio
        pServicioConversiones.ActualizarConversiones(pDatos);
    }

    public void ActualizarProductos(string mP)
    {
        //objeto que recibira los elementos serializados
        XmlSerializer pSerializador = new XmlSerializer(typeof(Productos));
        //se crea un lector el cual recibira los datos des-serializados
        StringReader lector = new StringReader(mP);
        //se castea el des-serializador y se obtienen los elementos
        Productos pProductos = (Productos)pSerializador.Deserialize(lector);
        //se crea un objeto de clase ServicioProductos 
        ServicioProductos pServicioProductos = new ServicioProductos();
        //se instancea la tabla que contiene los campos a modificar
        GNB_PRODUCTOS pDatos = new GNB_PRODUCTOS();
        //se almacenan los datos dentro de los campos correspondientes
        pDatos.ID_PRODUCTOS = pProductos.Id_Productos;
        pDatos.SKU = pProductos.Sku;
        pDatos.AMMOUNT = Convert.ToDecimal(pProductos.Ammount);
        pDatos.CURRENCY = pProductos.Currency;
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
        //se almacenan los datos dentro de los campos correspondientes
        pDatos.ID_CONVERSION = pConversiones.Id_Conversion;
        pDatos.FROM_CURRENCY = pConversiones.From_Currency;
        pDatos.TO_CURRENCY = pConversiones.To_Currency;
        pDatos.RATE = Convert.ToString(pConversiones.Rate); //me equivoque al crear la columna deberia ser "decimal" o "money" ya que se esta trabajando con tasas de conversion de dinero
        //se agregan las conversiones al servicio
        pServicioConversiones.AgregarConversiones(pDatos);
    }

    public void AgregarProductos(string mP)
    {
        //objeto que recibira los elementos serializados
        XmlSerializer pSerializador = new XmlSerializer(typeof(Productos));
        //se crea un lector el cual recibira los datos des-serializados
        StringReader lector = new StringReader(mP);
        //se castea el des-serializador y se obtienen los elementos
        Productos pProductos = (Productos)pSerializador.Deserialize(lector);
        //se crea un objeto de clase ServicioProductos 
        ServicioProductos pServicioProductos = new ServicioProductos();
        //se instancea la tabla que contiene los campos a modificar
        GNB_PRODUCTOS pDatos = new GNB_PRODUCTOS();
        //se almacenan los datos dentro de los campos correspondientes
        pDatos.ID_PRODUCTOS = pProductos.Id_Productos;
        pDatos.SKU = pProductos.Sku;
        pDatos.AMMOUNT = Convert.ToDecimal(pProductos.Ammount);
        pDatos.CURRENCY = pProductos.Currency;
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
        foreach (GNB_CONVERSIONES indice in pConversiones)
        {
            Conversiones iComun = new Conversiones();
            iComun.Id_Conversion = indice.ID_CONVERSION;
            iComun.From_Currency = indice.FROM_CURRENCY;
            iComun.To_Currency = indice.TO_CURRENCY;
            iComun.Rate = Convert.ToDouble(indice.RATE);
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
    public string ObtenerProductos()
    {
        ServicioProductos pServicioProductos = new ServicioProductos();
        List<GNB_PRODUCTOS> pProductos = pServicioProductos.ObtenerProductos();
        ProductosCollection pProductosCollection = new ProductosCollection();
        foreach (GNB_PRODUCTOS indice in pProductos)
        {
            Productos iComun = new Productos();
            iComun.Id_Productos = indice.ID_PRODUCTOS;
            iComun.Sku = indice.SKU;
            iComun.Ammount = Convert.ToDouble(indice.AMMOUNT);
            iComun.Currency = indice.CURRENCY;
            pProductosCollection.Add(iComun);
        }
        XmlSerializer pSerializador = new XmlSerializer(typeof(ProductosCollection));
        StringWriter escritor = new StringWriter();
        pSerializador.Serialize(escritor, pProductosCollection);
        return escritor.ToString();
    }
}
