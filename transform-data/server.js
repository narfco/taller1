'use strict';

const express = require('express'), bodyParser = require('body-parser'), DOMParser = require('xmldom').DOMParser;
const PORT = 8081;
const HOST = '0.0.0.0'
const version = 'v1';


const app = express();
app.use(bodyParser.json());

app.post('/servicios/transformar/' + version + '/transformdata', (req, res) => {
    console.log(req.body);
    if (req.body.operacion != undefined) {

        if (req.body.idFactura != undefined || req.body.valorFactura != undefined || req.body.formato != undefined) {

            switch (req.body.formato.toLowerCase()) {
                case 'xml':
                    console.log(req.body);
                    res.send(200, { "mensaje": crearMensajeXML(req.body.idFactura, req.body.valorFactura, req.body.operacion) });
                    break;
                case 'json':
                    console.log(req.body);
                    if (req.body.operacion.toLowerCase() == "pagar") {
                        res.send(200, { "mensaje": crearMensajeJSON(req.body.idFactura, req.body.valorFactura, req.body.operacion) });
                    } else {
                        res.send(404, { "mensaje": "Operacion invalida para rest" });
                    }
                    break;
                default:
                    console.log("Formato invalida");
                    res.send(404, { "mensaje": "Formato invalida" });
            }
        } else {
            console.log("ParamaInco");
            res.send(404, { "mensaje": "Parametros incompletos" });
        }
    } else {
        console.log("op invalida");
        res.send(404, { "mensaje": "Operacion invalida" });
    }
});

function crearMensajeXML(idFactura, valorFactura, operacion) {


    switch (operacion.toLowerCase()) {

        case 'consultar':
            var doc = 
                '<sch:ReferenciaFactura>' +
                '<sch:referenciaFactura>' + idFactura + '</sch:referenciaFactura>' +
                '</sch:ReferenciaFactura>';
            return doc;
            break;
        case 'pagar':
            var doc =
                '<sch:Pago>' +
                '<sch:referenciaFactura>' +
                '<sch:referenciaFactura>' + idFactura + '</sch:referenciaFactura>' +
                '</sch:referenciaFactura>' +
                '<sch:totalPagar>' + valorFactura + '</sch:totalPagar>' +
                '</sch:Pago>';
            return doc;
            break;
        case 'compensar':
            var doc =
                '<sch:Pago>' +
                '<sch:referenciaFactura>' +
                '<sch:referenciaFactura>' + idFactura + '</sch:referenciaFactura>' +
                '</sch:referenciaFactura>' +
                '<sch:totalPagar>' + valorFactura + '</sch:totalPagar>' +
                '</sch:Pago>';
            return doc;
            break;
        default:
            res.send(404, { "mensajeBad": "Operacion invalida" });
    }
}

function crearMensajeJSON(idFactura, valorFactura, operacion) {
    switch (operacion.toLowerCase()) {
        case 'pagar':
            return {
                "valorFactura": valorFactura
            };
            break;
    }
}
app.listen(PORT, HOST);
console.log(`Running on http://${HOST}:${PORT}`);