SELECT count(*) from cajero where id=" + idCajero + " and password='" + textBox2.Text + "'"
INSERT INTO anticipo(id_deudor,total,id_turno) VALUES (" + idCajero + "," + total + "," + miamiDB.id_turno + ")"
SELECT count(*) from cajero where id=" + idCajero + " and password='" + textBox2.Text + "'"
INSERT INTO colacion(id_cajero,total,id_turno,fecha) VALUES (" + idCajero + "," + total + "," + miamiDB.id_turno + ",now() )"
INSERT INTO factura(id_factura,codigo,id_empresa,id_turno,fecha,total) VALUES (DEFAULT," + textBox2.Text + "," + indiceEmpresa + "," + miamiDB.id_turno + ", now()," + textBox1.Text + " )"
SELECT inventario.plu, producto.nombre ,inventario.stock from inventario,producto where producto.plu=inventario.plu order by inventario.stock ASC", ref inventario

     query, ref results
SELECT  plu, barcode, nombre, precio, pesable, id_categoria from producto where id_categoria=" + idCategoria, ref tablaProductos
DELETE from producto where plu=" + plu
      
INSERT INTO inventario (plu,stock) VALUES ({0},0)",plu)
SELECT id,nombre,password from cajero ORDER BY id", ref tablaCajeros
SELECT * from empresa ORDER BY id", ref tablaEmpresas
SELECT id, nombre_categoria as nombre from categoria ORDER BY id", ref tablaCategorias
SELECT * from sucursales ORDER BY id", ref tablaSucursales





UPDATE turno SET retiro = (SELECT total_ventas FROM resumen_turno WHERE id={0}) WHERE id={0}",miamiDB.id_turno)

SELECT plu,nombre,precio,pesable,barcode,id_categoria FROM producto", ref dt, "producto"
SELECT id,nombre FROM empresa order by nombre ASC", ref empresas
SELECT id,nombre FROM sucursales order by nombre ASC", ref sucursales
SELECT id,nombre FROM cajero order by nombre ASC", ref empresas
SELECT id,nombre_categoria FROM categoria order by nombre_categoria ASC ", ref grupos"

INSERT INTO turno(id,id_cajero,fecha,caja_inicial,sucursal) VALUES (" + id_turno_actual + "," + id_cajero + ",now(),"+miamiPOS.Properties.Settings.Default.cajaInicial+","+id_sucursal+")"
SELECT id,id_cajero,fecha,caja_inicial from turno where id=" +id_turno_actual.ToString() ,ref turnoRescatado
UPDATE turno SET fin_turno=now() WHERE id=" + id_turno
SELECT turno.fecha FROM turno WHERE turno.id= " + miamiDB.id_turno

SELECT nextval('venta_id_venta_seq')")
  
INSERT INTO venta_producto(id_venta,plu,cantidad,total) VALUES (" + idVenta + "," + row["plu"] + "," + row["cantidad"]+ "," + row["total"]+")"
UPDATE inventario set stock=stock-"+ row["cantidad"] +" where plu="+row["plu"]

UPDATE turno SET caja_final={0},retiro={1} WHERE id={2}", caja, retiros, miamiDB.id_turno
UPDATE inventario SET stock={0} where plu={1}", cantidad, SELECTedPLU
SELECT id,nombre,admin from cajero where id="+user+"and password='"+pass+"'";
SELECT  plu, barcode, nombre, precio, pesable, id_categoria from producto ";
SELECT nombre,sum(cantidad) as Cantidad,sum(venta_producto.total) as Dinero from producto,venta_producto,venta,turno where venta.id_venta = venta_producto.id_venta and venta_producto.plu=producto.plu and producto.pesable={0} and extract(year from venta.fecha)={1} and extract(doy from venta.fecha)={2} and turno.id=venta.id_turno and turno.sucursal={3} group by nombre order by Dinero DESC"
SELECT factura.codigo,empresa.nombre, factura.total from empresa,factura,turno where factura.id_empresa=empresa.id and factura.id_turno=turno.id and turno.sucursal={2} and extract(year from factura.fecha)={0} and extract(doy from factura.fecha)={1} order by factura.total DESC"
SELECT cajero.nombre, anticipo.total from anticipo,cajero,turno where cajero.id=anticipo.id_deudor and anticipo.id_turno=turno.id and turno.sucursal={2} and extract(year from anticipo.fecha)={0} and extract(doy from anticipo.fecha)={1} order by anticipo.total DESC"
SELECT nombre,sum(cantidad) as Cantidad,sum(venta_producto.total) as Dinero from producto,venta_producto,venta,turno where venta.id_venta = venta_producto.id_venta and venta_producto.plu=producto.plu and producto.pesable={0} and extract(year from venta.fecha)={1} and extract(doy from venta.fecha)={2} and turno.id=venta.id_turno and turno.sucursal={3} group by nombre order by Dinero DESC"
" SELECT id,nombre_cajero,local,comienzo_turno,fin_turno,caja_inicial,caja_final,total_ventas,debito,gastos,retiro,error from resumen_turno where extract(year from comienzo_turno)={0} and extract(doy from comienzo_turno)={1}"
INSERT INTO {2}(id,nombre) SELECT {0},'{1}' WHERE NOT EXISTS (SELECT 1 FROM {2} WHERE id={0}"
INSERT INTO {2}(id,nombre) SELECT {0},'{1}' WHERE NOT EXISTS (SELECT 1 FROM {2} WHERE id={0}"
INSERT INTO {2}(id,nombre_categoria) SELECT {0},'{1}' WHERE NOT EXISTS (SELECT 1 FROM {2} WHERE id={0}"
INSERT INTO cajero(id,nombre,password) SELECT {0},'{1}','{2}' WHERE NOT EXISTS (SELECT 1 FROM cajero WHERE id={0}"
SELECT count(*) from producto where EXTRACT(year from last_change) >= {0} AND EXTRACT(doy from last_change) >= {1} AND EXTRACT(hour from last_change) >= {2}"

INSERT INTO venta(id_venta,total,fecha,id_turno,debito) VALUES ({0},{1},now(),{2},{3})";

SELECT sum( venta.total ) from venta where venta.id_turno = {0}"
SELECT sum( venta.total ) from venta where venta.id_turno = {0} and venta.debito=TRUE"
SELECT sum( anticipo.total ) from anticipo where anticipo.id_turno ={0}"
SELECT sum( colacion.total ) from colacion where colacion.id_turno ={0}"
SELECT sum( factura.total ) from factura where factura.id_turno ={0}"
SELECT turno.caja_inicial from turno where turno.id ={0}"
SELECT sum( venta.total ) from venta,turno where extract(year from venta.fecha)={0} and extract(doy from venta.fecha)={1} and turno.id=venta.id_turno and turno.sucursal={2}"
SELECT sum( venta.total ) from venta,turno where extract(year from venta.fecha)={0} and extract(doy from venta.fecha)={1} and turno.id=venta.id_turno and turno.sucursal={2} and venta.debito=TRUE"
SELECT sum( anticipo.total ) from anticipo,turno where extract(year from anticipo.fecha)={0} and extract(doy from anticipo.fecha)={1} and turno.id=anticipo.id_turno and turno.sucursal={2}"
SELECT sum( colacion.total ) from colacion,turno where extract(year from colacion.fecha)={0} and extract(doy from colacion.fecha)={1} and turno.id=colacion.id_turno and turno.sucursal={2}"
SELECT sum( factura.total ) from factura,turno where extract(year from factura.fecha)={0} and extract(doy from factura.fecha)={1} and turno.id=factura.id_turno and turno.sucursal={2}"
SELECT turno.caja_inicial from turno where extract(year from turno.fecha)={0} and extract(doy from turno.fecha)={1} and turno.sucursal={2}  order by id ASC"
select turno.caja_final from turno where extract(year from turno.fecha)={0} and extract(doy from turno.fecha)={1} and turno.sucursal={2}  order by id DESC"
INSERT INTO producto (plu,  nombre, barcode, precio, id_categoria, pesable) VALUES ({0},'{2}',NULL,{3},{4},{5})"; 
UPDATE producto SET (nombre, precio, id_categoria, pesable, barcode,last_change)=('{1}',{2},{3},{4},'{5}',now()) WHERE plu={0}";
UPDATE producto SET (nombre, precio, id_categoria, pesable, barcode,last_change)=('{1}',{2},{3},{4},{5},now()) WHERE plu={0}"; 
  