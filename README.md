# HiberiusTestGNB
Ultima version entregada que contiene conexion con base de datos a traves de Entity Framework y Webservices

La base de datos esta desarrollada en SQL Server 2014 por lo que deberas tener la misma version o superior para 
recuperar la base de datos desde el archivo de respaldo dentro del directorio "CAPADATOS/BD_GNB".

si no posee una version de SQL SERVER adecuada aqui esta la informacion necesaria para crear la base de datos

NOMBRE: BD_GNB

TABLA1: GNB_TRANSAC
CREATE TABLE  GNB_TRANSAC
(
  ID_PRODUCT NOT NULL,
  SKU VARCHAR(50) NOT NULL,
  AMOUNT MONEY NOT NULL,
  CURRENCY VARCHAR(50) NOT NULL,
  CONSTRAINTS ID_PRODUCT_PK PRIMARY KEY (ID_PRODUCT)
);

TABLA2: GNB_CONVERSIONES
CREATE TABLE GNB_CONVERSIONES
(
  ID_CONVERSION INT NOT NULL,
  FROM_CURRENCY VARCHAR(50) NOT NULL,
  TO_CURRENCY VARCHAR(50) NOT NULL,
  RATE MONEY NOT NULL,
  CONSTRAINTS ID_CONVERSION_PK PRIMARY KEY (ID_CONVERSION)
);

luego de crear o recuperar la base de datos, debes:

ABRIR EL LA SOLUCION y el la carpeta CAPADATOS borrar el MODEL de conexion a la base de datos
crear una nueva conexion .ADO (ENTITY FRAMEWORK) y buscar la base de datos creada o recuperada 
para luego entonces crear la nueva asociacion de base de datos, luego cambiar el nombre de la entidad en los servicios
de la carpeta CAPADATOS
