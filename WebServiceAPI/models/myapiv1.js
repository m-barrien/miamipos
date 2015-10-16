//1
var http = require('http'),
	express = require('express');
var app = express();

var pg = require('pg');
var conString = "postgres://marcelo:marcelo@blanco.mbarrien.com/panaderia";

//this initializes a connection pool
//it will keep idle connections open for a (configurable) 30 seconds
//and set a limit of 20 (also configurable)
pg.connect(conString, function(err, client, done) {
  if(err) {
    return console.error('error fetching client from pool', err);
  }
  client.query('SELECT $1::int AS number', ['1'], function(err, result) {
    //call `done()` to release the client back to the pool
    done();
    console.log("Psql Online!");
    if(err) {
      return console.error('error running query', err);
    }
    //console.log(result.rows[0].number);
    //output: 1
  });
});


app.set('port', process.env.PORT || 3000);	 

app.get('/', function(req, res){
  res.send('hello world');

});
app.get('/admin/resumen/:yy/:mm/:dd', function(req, res) { //I
   var params = req.params;
   var sucursal = params.sucursal;
   var yy = params.yy;
   var mm = params.mm;
   var dd = params.dd;
   if (yy && mm && dd) {
       pg.connect(conString, function(err, client, done) {
          if(err) {
            return console.error('error fetching client from pool', err);
          }
          client.query('SELECT id,nombre_cajero,local,comienzo_turno,fin_turno,caja_inicial,caja_final,total_ventas,debito,gastos,retiro,error from resumen_turno where extract(year from comienzo_turno)=$1 and extract(month from comienzo_turno)=$2 and extract(day from comienzo_turno)=$3', [yy,mm,dd], function(err, result) {
            //call `done()` to release the client back to the pool
            done();
            if(err) {
              return console.error('error running query', err);
            }
            console.log('Executed query.');
            res.send(result.rows  );
          });
          });//""
   } else {
      res.send(400, {error: 'bad url', url: req.url});
   }
});

http.createServer(app).listen(
	app.get('port'), 
	function(){
  		console.log('Express server listening on port ' + app.get('port'));
	}
);

