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
    var id = req.params.id;
    data[id] = updateProduct // Spoof a DB call
    res.json(updateProduct);
  },
 
  delete: function(req, res) {
    var id = req.params.id;
    data.splice(id, 1) // Spoof a DB call
    res.json(true);
  }
};
 
var data = [{
  name: 'product 1',
  id: '1'
}, {
  name: 'product 2',
  id: '2'
}, {
  name: 'product 3',
  id: '3'
}];
 
module.exports = products;