var table;

$(document).ready(function () {
    table = $('.custom-Datatable').dataTable({
        responsive: true,
        "lengthMenu": [[10, 25, 50, 100, -1], [10, 25, 50, 100, "Todos"]],
        "oLanguage": {
            "sEmptyTable": "",
            "sZeroRecords": "Sem registro",
            "sInfoEmpty": "",
            "sLengthMenu": "_MENU_ ",
            "sInfo": "Mostrando <b>_START_ - _END_</b> de um total de <b>_MAX_</b> registros",
            "sInfoFiltered": "",
            "sSearch": "Pesquisar: ",
            "sLengthMenu": "Exibir  _MENU_  registros por página",
            "oPaginate": {
                "sNext": "Próxima",
                "sPrevious": "Anterior"
            }
        },
        "iDisplayLength": 10
    });
});

function DatatableConfig() {
    debugger;
    var config =
        {
            responsive: true,
            "lengthMenu": [[10, 25, 50, 100, -1], [10, 25, 50, 100, "Todos"]],
            "oLanguage": {
                "sEmptyTable": "",
                "sZeroRecords": "Sem registro",
                "sInfoEmpty": "",
                "sLengthMenu": "_MENU_ ",
                "sInfo": "Mostrando <b>_START_ - _END_</b> de um total de <b>_MAX_</b> registros",
                "sInfoFiltered": "",
                "sSearch": "Pesquisar: ",
                "sLengthMenu": "Exibir  _MENU_  registros por página",
                "oPaginate": {
                    "sNext": "Próxima",
                    "sPrevious": "Anterior"
                }
            },
            "iDisplayLength": 10
        };

    return config;
};