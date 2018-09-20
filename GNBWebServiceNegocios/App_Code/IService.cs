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
    void AgregarProductos(string mP);

    [OperationContract]
    void ActualizarProductos(string mP);

    [OperationContract]
    string ObtenerProductos();

    [OperationContract]
    void AgregarConversiones(string mC);

    [OperationContract]
    void ActualizarConversiones(string mC);

    [OperationContract]
    string ObtenerConversiones();

    [OperationContract]
    void ConversionesEnLinea(string xmlLink);


}



