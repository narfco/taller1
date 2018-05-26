
exports.llamarDispatcher = function (idfactura,valorFactura, empresa, servicio, operacion) {


    var request = require('sync-request');
    try {
        var res = request('POST', "http://172.20.10.2:32771/api/Dispatcher", {
            json: {

                "transformation": {
                    "idFactura": idfactura,
                    "valorFactura": valorFactura,
                    "formato": (servicio.formato.toLowerCase() == "xml" ? 1 : 0),
                    "operacion": operacion
                },
                "serviceUri": servicio.endpointUrl
            }



        });
        var res = (res.getBody('utf8'));
        return res;
    } catch (error) {
        return undefined;
    }

}
