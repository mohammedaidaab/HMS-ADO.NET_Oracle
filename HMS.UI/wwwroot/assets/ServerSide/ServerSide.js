
    function GetDataTableData() {
        $("#data").DataTable({
            "processing": true,
            "serverSide": true,
            "filter": true,
            "orderMulti": false,
            "destroy": true,
            "ordering": true,
            "ajax": {
                "url": '/Reservation/GetDetails',
                "type": "POST",
                "datatype": "json"
            },

            "columns": [
                { "data": "name", "name": "Name", "autoWidth": true },
                { "data": "time_Start", "name": "time_Start", "autoWidth": true },
                { "data": "time_End", "name": "time_End", "autoWidth": true },
                { "data": "date", "name": "date", "autoWidth": true },
                { "data": "hall_name", "name": "hall_name", "autoWidth": true },
                { "data": "user_Name", "name": "user_Name", "autoWidth": true },
            ]
        });
    }
