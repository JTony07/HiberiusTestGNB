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
            GNB_BDEntities pEntidad = new GNB_BDEntities();
            //BD_GNBEntities pEntidad = new BD_GNBEntities(); //se apunta a la base de datos
            pEntidad.GNB_PRODUCTOS.Add(pProducto); //se agregan los elementos a la tabla GNB_PRODUCTOS
            pEntidad.SaveChanges(); //se guardan los cambios en la base de datos
        }

        //se actualizan los productos en la base de datos
        public void ActualizarProductos(GNB_PRODUCTOS pProducto)
        {
            GNB_BDEntities pEntidad = new GNB_BDEntities();
            
            //BD_GNBEntities pEntidad = new BD_GNBEntities(); // se apunta al a base de datos
            //se busca mediante una consulta donde los ID_SKU coincidan y entonces se actualiza si existe coincidencia
            //esto permite que si existe un cambio en el recurso de HEROKU la base de datos se actualice
            //
            GNB_PRODUCTOS revisionTabla = (from dato in pEntidad.GNB_PRODUCTOS where dato.ID_PRODUCTS == pProducto.ID_PRODUCTS select dato).ToList<GNB_PRODUCTOS>()[0];

            //si la consulta no proporciona ningun resultado se lanzara un excepcion indicando que no existe
            //coincidencia en los ID_SKU de ese producto y por lo tanto no se actualiza
            if (revisionTabla == null) throw new ArgumentException("NO existe esa ID_PRODUCTOS");
            else
            {
                revisionTabla.ID_PRODUCTS = pProducto.ID_PRODUCTS;
                //revisionTabla.ID_PRODUCTOS = pProducto.ID_PRODUCTOS;
                revisionTabla.SKU = pProducto.SKU;
                revisionTabla.AMOUNT = pProducto.AMOUNT;
                //revisionTabla.AMMOUNT = pProducto.AMMOUNT;
                revisionTabla.CURRENCY = pProducto.CURRENCY;
                pEntidad.SaveChanges();
            }
        }

        //se buscan todos los productos de la tabla GNB_PRODUCTOS
        public List<GNB_PRODUCTOS> ObtenerProductos()
        {
            GNB_BDEntities pEntidad = new GNB_BDEntities();
            //BD_GNBEntities pEntidad = new BD_GNBEntities(); //se apunta a la base de datos
            return pEntidad.GNB_PRODUCTOS.ToList<GNB_PRODUCTOS>(); // se convierten todos los elementos a una lista
        }

        //se buscan todos los productos de la tabla GNB_PRODUCTOS
        public List<GNB_PRODUCTOS> BuscarProductos(GNB_PRODUCTOS pProducto)
        {
            GNB_BDEntities pEntidad = new GNB_BDEntities();
            //BD_GNBEntities pEntidad = new BD_GNBEntities(); //se apunta a la base de datos
            List<GNB_PRODUCTOS> revisionTabla = (from dato in pEntidad.GNB_PRODUCTOS where dato.SKU == pProducto.SKU select dato).ToList<GNB_PRODUCTOS>();
            return revisionTabla; // se convierten todos los elementos a una lista
        }

        /// <summary>
        /// Procedimiento que limpia todos los elementos dentro de la tabla en la base de datos
        /// </summary>
        public void LimpiarProductos()
        {
            GNB_BDEntities pEntidad = new GNB_BDEntities(); //se crea una entidad que tenga las caracteristicas de la tabla
            var Todos = from c in pEntidad.GNB_PRODUCTOS select c; //seleccionan todos los objetos dentro de la tabla actual
            pEntidad.GNB_PRODUCTOS.RemoveRange(Todos); //se eliminan todos los elementos encontrados
            pEntidad.SaveChanges(); // se guardan los cambios
        }
        

    }
}
