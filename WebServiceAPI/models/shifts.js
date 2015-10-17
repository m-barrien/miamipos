var db= require('../models/database.js')

var shifts = {
 
  getAll: function(req, res) {
    db.query("SELECT * FROM turno ORDER BY fecha DESC LIMIT 100"
      , []
      , function(queryReturn){
          res.json(queryReturn);
      }
    );
  },
 
  getOne: function(req, res) {
    var id = req.params.id.toString();
    var query='';
    if (id.length>5) {
      query = "SELECT plu,nombre,precio,pesable,barcode,id_categoria FROM producto where barcode=$1 ORDER BY plu";
    }
    else {
      query = "SELECT plu,nombre,precio,pesable,barcode,id_categoria FROM producto where plu=$1 ORDER BY plu";
    }

    db.query(
        query
      , [id]
      , function(queryReturn){
          res.json(queryReturn);
      }
    );    
  },
 
  create: function(req, res) {
    var newShift = req.body;
    var query ="INSERT INTO turno(id_cajero,fecha,caja_inicial,sucursal) VALUES ($1 ,now(),$2,$3) RETURNING *";
    var params = [ newShift.id_cajero, newShift.caja_inicial, newShift.sucursal ];
    if (!newShift|| !newShift.id_cajero || !newShift.caja_inicial || !newShift.sucursal ) 
      {
        newShift = {  "success": false,
                        "message": "Missing values from shift body.",
                        "rows": {}
                      };
        res.json(newShift);
        return;
      }
    db.query(
        query
      , params
      , function(newShift){
          res.json(newShift);
      }
    );    
  },
 
  update: function(req, res) {
    var updateProduct = req.body;
    var plu = req.params.id;

    var query = "UPDATE producto SET (nombre, barcode, precio, id_categoria, pesable,last_change)=($2,$3,$4, $5,$6 ,now()) WHERE plu=$1 RETURNING *"; 
    var params = [plu, updateProduct.nombre, updateProduct.barcode, updateProduct.precio, updateProduct.id_categoria, updateProduct.pesable];
 
    if (!updateProduct|| !updateProduct.nombre || updateProduct.nombre.length<2 || !updateProduct.id_categoria || !updateProduct.pesable || !updateProduct.precio) 
    {
      errorjson = {  "success": false,
                      "message": "Missing values from product body.",
                      "rows": {}
                    };
      res.json(errorjson);
      return;
    }   
    db.query(
        query
      , params
      , function(updatedProduct){
          res.json(updatedProduct);
      }
    ); 
  },
 
  delete: function(req, res) {
    var plu = req.params.id;
    var query ="DELETE FROM producto WHERE plu=$1 RETURNING *";
    db.query(
        query
      , [plu]
      , function(updatedProduct){
          res.json(updatedProduct);
      }
    );     
  }
};

 
module.exports = shifts;