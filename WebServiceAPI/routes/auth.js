var jwt = require('jwt-simple');
 var db= require('../models/database.js')

var auth = {
 
  login: function(req, res) {
 
    var userid = req.body.userid || '';
    var password = req.body.password || '';
 
    if (userid == '' || password == '') {
      res.status(401);
      res.json({
        "status": 401,
        "message": "Invalid credentials"
      });
      return;
    }
 
    // Fire a query to your DB and check if the credentials are valid
    auth.validate(userid, password, function(dbUserObj)
      {
        if (!dbUserObj) { // If authentication fails, we send a 401 back
          res.status(401);
          res.json({
            "status": 401,
            "message": "Invalid credentials"
          });
          return;
        }
        if (dbUserObj) {
     
          // If authentication is success, we will generate a token
          // and dispatch it to the client
     
          res.json(genToken(dbUserObj));
        }
      }
    );
  },
 
  validate: function(userid, password,callback) {
    var dbUserObj = { 
      name: '',
      role: 'cashier',
      userid: -1
    };
    db.query("SELECT nombre,admin FROM cajero WHERE id=$1 and password=$2"
    , [userid,password]
    , function(queryReturn){
        if(queryReturn["success"] && queryReturn["rows"].length > 0)
        {
          dbUserObj["name"]=queryReturn["rows"][0]["nombre"];
          dbUserObj["userid"]=userid;
          if (queryReturn["rows"][0]["admin"]) 
            {
              dbUserObj["role"]='admin';
            }
        }
        else
        {
          dbUserObj = false;
        }
        return callback(dbUserObj);
      }
    );
  },
 
  validateUser: function(username,callback) {
    var dbUserObj = { 
      name: '',
      role: 'cashier',
      userid: -1
    };
    db.query("SELECT id,nombre,admin FROM cajero WHERE nombre=$1"
    , [username]
    , function(queryReturn){
        if(queryReturn["success"] && queryReturn["rows"].length > 0)
        {
          dbUserObj["name"]=queryReturn["rows"][0]["nombre"];
          dbUserObj["userid"]=queryReturn["rows"][0]["id"];
          if (queryReturn["rows"][0]["admin"]) 
            {
              dbUserObj["role"]='admin';
            }
        }
        else
        {
          dbUserObj = false;
        }
        return callback(dbUserObj);
      }
    );
  },
}
 
// private method
function genToken(user) {
  var expires = expiresIn(7); // 7 days
  var token = jwt.encode({
    exp: expires,
    user: user
  }, require('../config/secret')());
 
  return {
    token: token,
    expires: expires,
    user: user
  };
}
 

function expiresIn(numDays) {
  var dateObj = new Date();
  return dateObj.setDate(dateObj.getDate() + numDays);
}
 
module.exports = auth;