'use strict';
//
// begin Load and check connection parameters
//
var config;
try {
	config = require('./config.js');
} catch (e) {
	console.log('Error loading config.js. Please rename or copy config.sample.js into config.js');
	process.exit();
} 
var EventEmitter = require('events'); 
var io = require('socket.io-client');
 
var socketLocal;

 


socketLocal = io('http://127.0.0.1:' + config.LWport);

socketLocal.on('connect', () => {
        console.log('Local ws has been opened: ', socketLocal.id);
 console.log('Open StrategyMonitorCenter.exe firstly then check data on tab Test1:');
        socketLocal.emit('message', socketLocal.id);
 socketLocal.emit('message', JSON.stringify({"TableName":"Test,TestTab1","Time":"04:23 03:19:06:389","Content":"Test function ...","Type":"Test Type","Name":"Test name","Id":1234}));
 console.log('Open StrategyMonitorCenter.exe firstly then check data on tab Test1,Below Test data had sent to StrategyMonitorCenter.exe',JSON.stringify({"TableName":"Test,TestTab1","Time":"04:23 03:19:06:389","Content":"Test function ...","Type":"Test Type","Name":"Test name","Id":1234}));

  socketLocal.emit('message', JSON.stringify({"TableName":"Test,TestTab1","Time":"04:23 03:19:06:389","Content":"Test function ...","Type":"Test Type","Name":"Test name","Id":1234}));
 console.log('Open StrategyMonitorCenter.exe firstly then check data on tab Test1,Below Test data had sent to StrategyMonitorCenter.exe',JSON.stringify({"TableName":"Test,TestTab1","Time":"04:23 03:19:06:389","Content":"Test function ...","Type":"Test Type","Name":"Test name","Id":1234}));

   socketLocal.emit('message', JSON.stringify({"TableName":"Test,TestTab1","Time":"04:23 03:19:06:389","Content":"Test function ...","Type":"Test Type","Name":"Test name","Id":1234}));
   socketLocal.emit('message', JSON.stringify({"TableName":"Test,TestTab1","Time":"04:23 03:19:06:389","Content":"Test function ...","Type":"Test Type","Name":"Test name","Id":1234}));
    });
 console.log('Open StrategyMonitorCenter.exe firstly then check data on tab Test1,Below Test data had sent to StrategyMonitorCenter.exe',JSON.stringify({"TableName":"Test,TestTab1","Time":"04:23 03:19:06:389","Content":"Test function ...","Type":"Test Type","Name":"Test name","Id":1234}));
socketLocal.on('disconnect', () => {
        console.log('Local ws disconnected, terminating client.');
        socketLocal = io('http://127.0.0.1:' + + config.LWport);

    });
socketLocal.on('servercall', (serverdata) => {

        console.log(serverdata);
        socketLocal.emit('message', 'helloServer');


    });
 

 
console.log('helloworld'); 

//
// end Main
//