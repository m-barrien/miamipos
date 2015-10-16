var pg = require('pg');
var conString = "postgres://marcelo:marcelo@localhost/panaderia";


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

module.exports = {
    conString,
    query: function (query,params,callback) {
        var RESULTS=new Array;
        pg.connect(conString, function(err, client, done) {
            if(err) {
                console.error('error fetching client from pool', err);
                callback({"success":false,"message": error.toString(),"rows":{}});
            }
            var output = client.query(query, params);

            output.on('error', function(error) {
                done();
                callback({"success":false,"message": error.toString(),"rows":{}});
            });
            output.on('row', function(row) {
                RESULTS.push(row);
            });
            output.on('end', function() {
                done();
                var jsonOut= {"success":true, "message":query, "rows":RESULTS};
                callback(jsonOut);
            });

        });//""
    }
}