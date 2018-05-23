
exports.obtenerUrlServicioRegistry = function (empresa) {


    var request = require('sync-request');
    try {
        var res = request('GET', ("http://localhost:32770/api/Registry?id=" + empresa.ID));
        return JSON.parse(res.getBody('utf8'));
    } catch (error) {
        return undefined;
    }

}
