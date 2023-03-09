var ruta = window.location.origin;
$(document).ready(function () {
    $('#example').DataTable({
        "ajax":
        {
            "url": ruta + `/MantenedorIncluidos/Paginar`,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "cedula_add" },
            { "data": "apellido" },
            { "data": "nombre" },
            { "data": "sub" },
            { "data": "monto" },
            { "data": "fecha_add" },
        ],

    });
});
