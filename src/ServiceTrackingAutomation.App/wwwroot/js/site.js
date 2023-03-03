function InitDataTable() {
    var tableAny = document.getElementById("my_DataTable");
    if (tableAny) {
        $(document).ready(function () {
            $('#my_DataTable').DataTable({
                //language: {
                //    url: '//cdn.datatables.net/plug-ins/1.13.2/i18n/tr.json'
                //}
            });
        });

    }
    var tableAny2 = document.getElementById("my_DataTable_2");
    if (tableAny2) {
        $(document).ready(function () {
            $('#my_DataTable_2').DataTable({
                //language: {
                //    url: '//cdn.datatables.net/plug-ins/1.13.2/i18n/tr.json'
                //}
            });
        });

    }
}


InitDataTable();