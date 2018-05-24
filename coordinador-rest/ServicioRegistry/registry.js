
exports.obtenerUrlServicioRegistry = function (empresa) {


    var request = require('sync-request');
    try {
        var res = request('GET', ("http://192.168.5.180:8500/v1/kv/" + empresa.EMPRESA));

        return JSON.parse(new Buffer(JSON.parse(res.getBody('utf8'))[0].Value, 'base64'));
    } catch (error) {
        return undefined;
    }

}
