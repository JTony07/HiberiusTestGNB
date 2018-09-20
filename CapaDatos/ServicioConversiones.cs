using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class ServicioConversiones
    {
        //se agregan las conversiones a la base de datos
        public void AgregarConversiones(GNB_CONVERSIONES pConversion)
        {
            BD_GNBEntities pEntidad = new BD_GNBEntities(); //se apunta a la base de datos
            pEntidad.GNB_CONVERSIONES.Add(pConversion); //se agregan los elementos a la tabla GNB_CONVERSIONES
            pEntidad.SaveChanges(); //se guardan los cambios en la base de datos
        }

        //se actualizan las conversiones en la base de datos
        public void ActualizarConversiones(GNB_CONVERSIONES pConversion)
        {
            BD_GNBEntities pEntidad = new BD_GNBEntities(); // se apunta al a base de datos
            //se busca mediante una consulta donde los ID_CONVERSION coincidan y entonces se actualiza si existe coincidencia
            //esto permite que si existe un cambio en el recurso de HEROKU la base de datos se actualice
            //EL ID_CONVERSION ES UN DATO AUTO INCREMENTABLE POR LO TANTO MANTIENE EL MISMO ORDEN DEL RECURSO HEROKU 
            GNB_CONVERSIONES revisionTabla = (from dato in pEntidad.GNB_CONVERSIONES where dato.ID_CONVERSION == pConversion.ID_CONVERSION select dato).ToList<GNB_CONVERSIONES>()[0];
            GNB_CONVERSIONES revisionSecundaria = (from dato in pEntidad.GNB_CONVERSIONES where dato.FROM_CURRENCY == pConversion.FROM_CURRENCY select dato).ToList<GNB_CONVERSIONES>()[0];
            //si la consulta no proporciona ningun resultado se lanzara un excepcion indicando que no existe
            //coincidencia en los ID_CONVERSION Y EN FROM_CURRENCY de ese producto y por lo tanto no se actualiza
            if (revisionTabla == null && revisionSecundaria == null) throw new ArgumentException("El orden de las conversiones pudo haber cambiado");
            else
            {
                revisionTabla.ID_CONVERSION = pConversion.ID_CONVERSION;
                revisionTabla.FROM_CURRENCY = pConversion.FROM_CURRENCY;
                revisionTabla.TO_CURRENCY = pConversion.TO_CURRENCY;
                revisionTabla.RATE = pConversion.RATE;
                pEntidad.SaveChanges();
            }
        }

        //se buscan todos los productos de la tabla GNB_CONVERSIONES
        public List<GNB_CONVERSIONES> ObtenerConversiones()
        {
            BD_GNBEntities pEntidad = new BD_GNBEntities(); //se apunta a la base de datos
            return pEntidad.GNB_CONVERSIONES.ToList<GNB_CONVERSIONES>(); // se convierten todos los elementos a una lista
        }
    }
}
