var ruta = window.location.origin;
$(document).ready(function () {
    $('#examples').datatable({
        "ajax":
        {
            "url": ruta + `/mantenedorexcluidos/paginar`,
            "type": "get",
            "datatype": "json"
        },
        "columns": [
            { "data": "cedula" },
            { "data": "motivo" },
            { "data": "fecha" },
        ],

    });
});