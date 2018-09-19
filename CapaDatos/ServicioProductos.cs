using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class ServicioProductos
    {
        //se agregan los productos a la base de datos
        public void AgregarProducto(GNB_PRODUCTOS pProducto)
        {
            BD_GNBEntities1 pEntidad = new BD_GNBEntities1(); //se apunta a la base de datos
            pEntidad.GNB_PRODUCTOS.Add(pProducto); //se agregan los elementos a la tabla GNB_PRODUCTOS
            pEntidad.SaveChanges(); //se guardan los cambios en la base de datos
        }

        //se actualizan los productos en la base de datos
        public void ActualizarProductos(GNB_PRODUCTOS pProducto)
        {
            BD_GNBEntities1 pEntidad = new BD_GNBEntities1(); // se apunta al a base de datos
            //se busca mediante una consulta donde los ID_SKU coincidan y entonces se actualiza si existe coincidencia
            //esto permite que si existe un cambio en el recurso de HEROKU la base de datos se actualice
            //
            GNB_PRODUCTOS revisionTabla = (from dato in pEntidad.GNB_PRODUCTOS where dato.ID_PRODUCTOS == pProducto.ID_PRODUCTOS select dato).ToList<GNB_PRODUCTOS>()[0];

            //si la consulta no proporciona ningun resultado se lanzara un excepcion indicando que no existe
            //coincidencia en los ID_SKU de ese producto y por lo tanto no se actualiza
            if (revisionTabla == null) throw new ArgumentException("NO existe esa ID_PRODUCTOS");
            else
            {
                revisionTabla.ID_PRODUCTOS = pProducto.ID_PRODUCTOS;
                revisionTabla.SKU = pProducto.SKU;
                revisionTabla.AMMOUNT = pProducto.AMMOUNT;
                revisionTabla.CURRENCY = pProducto.CURRENCY;
                pEntidad.SaveChanges();
            }
        }

        //se buscan todos los productos de la tabla GNB_PRODUCTOS
        public List<GNB_PRODUCTOS> ObtenerProductos()
        {
            BD_GNBEntities1 pEntidad = new BD_GNBEntities1(); //se apunta a la base de datos
            return pEntidad.GNB_PRODUCTOS.ToList<GNB_PRODUCTOS>(); // se convierten todos los elementos a una lista
        }


    }
}
