var db= require('../models/database.js')

var products = {
 
  getAll: function(req, res) {
    db.query("SELECT plu,nombre,precio,pesable,barcode,id_categoria FROM producto ORDER BY plu"
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
    var newProduct = req.body;
    var query ="INSERT INTO producto (plu,  nombre, barcode, precio, id_categoria, pesable) VALUES ($1,$2,$3,$4,$5,$6) RETURNING *";
    var params = [newProduct.plu, newProduct.nombre, newProduct.barcode, newProduct.precio, newProduct.id_categoria, newProduct.pesable];
    if (!newProduct|| !newProduct.nombre || newProduct.nombre.length<2 || !newProduct.id_categoria || !newProduct.pesable || !newProduct.precio) 
      {
        newProduct = {  "success": false,
                        "message": "Missing values from product body.",
                        "rows": {}
                      };
        res.json(newProduct);
        return;
      }
    else{
      if (!newProduct.barcode) {
        query = query.replace("barcode,","").replace(",$6","");
        params.splice(2,1);
        if (!newProduct.plu) {
          query = query.replace("plu,","").replace(",$5","");
          params.splice(0,1);
        }
      }
      else
      {
        if (!newProduct.plu) {
          query = query.replace("plu,","").replace(",$6","");
          params.splice(0,1);
        };
      }
      
    }
    db.query(
        query
      , params
      , function(newProduct){
          res.json(newProduct);
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

 
module.exports = products;