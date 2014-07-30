CREATE TABLE cajero(
	id SERIAL PRIMARY KEY,
	password VARCHAR NOT NULL,
	nombre VARCHAR NOT NULL,
	admin BOOLEAN
);
CREATE TABLE categoria(
	id INTEGER PRIMARY KEY,
	nombre_categoria VARCHAR
);
CREATE TABLE producto(
	plu SERIAL PRIMARY KEY,
	barcode VARCHAR ,
	nombre VARCHAR NOT NULL,
	precio INTEGER NOT NULL,
	pesable BOOLEAN NOT NULL,
	id_categoria INTEGER REFERENCES categoria(id),
	last_change TIMESTAMP,
	iva BOOLEAN
);
CREATE TABLE turno(
	id SERIAL PRIMARY KEY,
	fecha TIMESTAMP NOT NULL,
	id_cajero INTEGER REFERENCES cajero(id),
	caja_inicial INTEGER,
	zeta INTEGER,
	fin_turno TIMESTAMP
);

CREATE TABLE venta(
	id_venta SERIAL PRIMARY KEY,
	total INTEGER NOT NULL,
	fecha TIMESTAMP,
	id_turno INTEGER REFERENCES turno(id)
);
CREATE TABLE venta_producto(
	id_venta INTEGER REFERENCES venta(id_venta),
	plu INTEGER REFERENCES producto(plu),
	cantidad INTEGER NOT NULL,
	PRIMARY KEY(id_venta,plu)
);
CREATE TABLE inventario(
	plu INTEGER REFERENCES producto(plu),
	stock INTEGER NOT NULL
);

CREATE TABLE empresa(
	id SERIAL PRIMARY KEY,
	nombre VARCHAR NOT NULL
);
CREATE TABLE factura(
	id_factura SERIAL PRIMARY KEY,
	codigo VARCHAR,
	id_empresa INTEGER REFERENCES empresa(id),
	id_turno INTEGER REFERENCES turno(id),
	fecha TIMESTAMP NOT NULL,
	total INTEGER NOT NULL
);
