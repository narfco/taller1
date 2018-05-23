'use strict';

const express = require('express'), bodyParser = require('body-parser'), fs = require('fs'), path = require('path');
var registry = require('./ServicioRegistry/registry.js');
var dispatcher = require('./ServicioDispatcher/dispatcher.js');



const PORT = 8082;
const HOST = '0.0.0.0'
const version = 'v1';


const app = express();
app.use(bodyParser.json());

app.post('/servicios/pagos/' + version + '/plataforma/consultar', (req, res) => {

    if (req.body.idFactura != undefined) {
        var idFactura = req.body.idFactura;
        var empresa = "";
        console.log("Determinar a que empresa es");

        var empresa = determinarEmpresa(idFactura);
        console.log(empresa);
        if (empresa != undefined) {
            console.log("Llamar al registy para obtener url");

            var servicio = registry.obtenerUrlServicioRegistry(empresa);
            console.log(servicio);
            if (servicio != undefined) {

                console.log("Llamar al dispatcher para obtener url");
                var respuestaDispatcher = dispatcher.llamarDispatcher(idFactura, 0, empresa, servicio, 0);
                if (servicio.formato.toLowerCase() == 'xml') {
                    var cleanedString = respuestaDispatcher.replace("\ufeff", "");
                    var objetoRsta = xml2json(cleanedString.toString('utf8'));

                    res.send(200, {
                        "idFactura": idFactura,
                        "valorFactura": parseInt(objetoRsta['S:Envelope']['S:Body'][0].ResultadoConsulta[0].totalPagar),
                        "empresaFactura": empresa.EMPRESA,
                        "titularFactura": "Javier Carranza",
                        "identificacionTitular": "123456"
                    });
                } else {
                    res.send(200, {
                        "idFactura": idFactura,
                        "valorFactura": parseInt(JSON.parse(respuestaDispatcher).valorFactura),
                        "empresaFactura": empresa.EMPRESA,
                        "titularFactura": "Carlos Corzo",
                        "identificacionTitular": "987654"
                    });
                }

                console.log(respuestaDispatcher);

            } else {
                res.send(404);
            }

        } else {
            res.send(404);
        }
    } else {
        res.send(404, { "mensaje": "Parametros incompletos" });
    }
});
app.post('/servicios/pagos/' + version + '/plataforma/pagar', (req, res) => {
    if (req.body.idFactura != undefined || req.body.valorFactura != undefined) {
        var idFactura = req.body.idFactura;
        var valorFactura = req.body.valorFactura;
        var empresa = "";
        console.log("Determinar a que empresa es");

        var empresa = determinarEmpresa(idFactura);
        console.log(empresa);
        if (empresa != undefined) {
            console.log("Llamar al registy para obtener url");

            var servicio = registry.obtenerUrlServicioRegistry(empresa);
            console.log(servicio);
            if (servicio != undefined) {

                console.log("Llamar al dispatcher para obtener url");
                var respuestaDispatcher = dispatcher.llamarDispatcher(idFactura, valorFactura, empresa, servicio, 1);
                if (servicio.formato.toLowerCase() == 'xml') {
                    var cleanedString = respuestaDispatcher.replace("\ufeff", "");
                    var objetoRsta = xml2json(cleanedString.toString('utf8'));

                    res.send(200, {

                        "idFactura": idFactura,
                        "numeroConfirmacion": Math.round(Math.random() * (1000000 - 100000) + 100000),
                        "mensajeConfirmacion": objetoRsta['S:Envelope']['S:Body'][0].Resultado[0].mensaje[0]
                    });
                } else {
                    res.send(200, {
                        "idFactura": idFactura,
                        "numeroConfirmacion": Math.round(Math.random() * (1000000 - 100000) + 100000),
                        "mensajeConfirmacion": JSON.parse(respuestaDispatcher).mensaje
                    });
                }

                console.log(respuestaDispatcher);

            } else {
                res.send(404);
            }

        } else {
            res.send(404);
        }
    } else {
        res.send(404, { "mensaje": "Parametros incompletos" });
    }
});
app.post('/servicios/pagos/' + version + '/plataforma/compensar', (req, res) => {
    if (req.body.idFactura != undefined || req.body.valorFactura != undefined || req.body.numeroConfirmacionPago != undefined) {
        var idFactura = req.body.idFactura;
        var valorFactura = req.body.valorFactura;
        var numeroConfirmacionPago = req.body.numeroConfirmacionPago;
        var empresa = "";
        console.log("Determinar a que empresa es");

        var empresa = determinarEmpresa(idFactura);
        console.log(empresa);
        if (empresa != undefined) {
            console.log("Llamar al registy para obtener url");

            var servicio = registry.obtenerUrlServicioRegistry(empresa);
            console.log(servicio);
            if (servicio != undefined) {

                console.log("Llamar al dispatcher para obtener url");
                var respuestaDispatcher = dispatcher.llamarDispatcher(idFactura, valorFactura, empresa, servicio, 2);
                if (servicio.formato.toLowerCase() == 'xml') {
                    var cleanedString = respuestaDispatcher.replace("\ufeff", "");
                    var objetoRsta = xml2json(cleanedString.toString('utf8'));

                    res.send(200, {

                        "idFactura": idFactura,
                        "numeroConfirmacionCompensacion": Math.round(Math.random() * (1000000 - 100000) + 100000),
                        "mensajeConfirmacion": objetoRsta['S:Envelope']['S:Body'][0].Resultado[0].mensaje[0]
                    });
                } else {
                    res.send(200, {
                        "idFactura": idFactura,
                        "numeroConfirmacionCompensacion": Math.round(Math.random() * (1000000 - 100000) + 100000),
                        "mensajeConfirmacion": JSON.parse(respuestaDispatcher).mensaje
                    });
                }

                console.log(respuestaDispatcher);

            } else {
                res.send(404);
            }

        } else {
            res.send(404);
        }
    } else {
        res.send(404, { "mensaje": "Parametros incompletos" });
    }
});

function determinarEmpresa(idFactura) {

    var loader = require('csv-load-sync');
    var csv = loader(path.join(__dirname, 'convenios.csv'));
    for (var i in csv) {
        if (idFactura.toString().startsWith(csv[i].EMPIEZACON)) {
            return csv[i];
        }
    }
    return undefined;

}

function xml2json(xml) {
    var parseStringSync = require('xml2js-parser').parseStringSync;;


    return parseStringSync(xml)

}

app.listen(PORT, HOST);
console.log(`Running on http://${HOST}:${PORT}`);