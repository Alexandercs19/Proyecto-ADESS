var ruta = window.location.origin;
$(document).ready(function () {
    $('#examples').DataTable({
        "ajax":
        {
            "url": ruta + `/MantenedorExcluidos/Paginar`,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "cedula" },
            { "data": "motivo" },
            { "data": "fecha" },
        ],

    });
});