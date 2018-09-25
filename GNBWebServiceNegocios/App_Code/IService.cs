using CapaDatos;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Services;
using System.Xml;

[ServiceContract]
public interface IService
{

    [OperationContract]
    void AgregarTransacciones(string mP);

    [OperationContract]
    void ActualizarTransacciones(string mP);

    [OperationContract]
    string ObtenerTransacciones();

    [OperationContract]
    void AgregarConversiones(string mC);

    [OperationContract]
    void ActualizarConversiones(string mC);

    [OperationContract]
    string ObtenerConversiones();

    [OperationContract]
    string BuscarTransacciones(string mP, string Busqueda);

    [OperationContract]
    string ConsultaXMLTransac(string xmlLink);

    [OperationContract]
    string ConsultaXMLConver(string xmlLink);

    [OperationContract]
    void LimpiarTransacciones();

    [OperationContract]
    void LimpiarConversiones();

    [OperationContract]
    string ListaTransacciones(string mTransaccionesXML);

    [OperationContract]
    double TotalizadoEUR(string pTransaccionesXML);

    //[OperationContract]
    //void ConversionesEnLinea(string xmlLink);

    //[OperationContract]
    //void TransaccionesEnLinea(string xmlLink);

    ////[OperationContract]
    ////string Busqueda(string Producto, List<GNB_PRODUCTOS> ListaProductos);

    ////[OperationContract]
    ////string ConsultaXML(string xmlLink);


}



