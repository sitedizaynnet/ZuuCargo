/*
  Start with:  node example.js
  Telnet to:   telnet 127.0.0.1 1337
  Copy/paste:  1203292316,0031698765432,GPRMC,211657.000,A,5213.0247,N,00516.7757,E,0.00,273.30,290312,,,A*62,F,imei:123456789012345,123
*/

const sql = require('mssql')
var tk102 = require ('./tk102');
var net = require ('net');
//Initiallising connection string
let dbConfig = {
user: "sa",
password: "C3xc3xc3x.",
server: "185.141.33.135",
database: "varkargo",
"options": {
"encrypt": false,
"enableArithAbort": true
},
};
var gps = '1203292316,0031698765432,GPRMC,211657.000,A,5213.0247,N,00516.7757,E,0.00,273.30,290312,,,A*62,F,imei:123456789012345,123';

// fancy console log
function output (data) {
  console.log ('\nIncoming GPS data:\n');


    // connect to your database
    sql.connect(dbConfig, function (err) {
    
        if (err) console.log(err);

        // create Request object
        var request = new sql.Request();
            request.input('date', sql.DateTime, data.datetime);
			request.input('lat', sql.VarChar, data.geo.latitude);
			request.input('long', sql.VarChar, data.geo.longitude);
			request.input('direction', sql.VarChar, data.geo.bearing);
			request.input('imei', sql.VarChar, data.imei);
        // query to the database and get the records
        request.query('INSERT INTO GpsData (Date, Latitude, Longitude, DeviceIMEI,Direction) VALUES (@date, @lat, @long, @imei, @direction)', function (err, recordset) {
            
            if (err) console.log(err)

           console.log("sql worked");
            
        });
    });

  
  

}

// report only track event to console
tk102.on ('track', output);

// wait for server to be ready
tk102.on ('listening', function (lst) {
  var client;

  console.log ('TK102 server is ready');

  // Send data with telnet
  client = net.connect (lst.port, function () {
    console.log ('Connected to TK102 server');
    console.log ('Sending GPS data string for processing');

    client.write (gps + '\r\n');
    client.end ();

    console.log ('CTRL+C to exit');
  });
});

// start server
tk102.createServer ({
  port: 1337
});
