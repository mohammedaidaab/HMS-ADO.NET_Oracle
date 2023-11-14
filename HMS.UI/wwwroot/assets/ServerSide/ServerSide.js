GetDataTableData();
function GetDataTableData() {
    $("#data").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "orderMulti": false,
        "destroy": true,
        "ordering": true,
        "ajax": {
            "url": '/Home/GetDetails',
            "type": "POST",
            "datatype": "json"
        },

        "columns": [
            { "data": "First_Name", "name": "First_Name", "autoWidth": true }
            , { "data": "User_Name", "name": "User_Name", "autoWidth": true }
            , { "data": "Time_Start", "name": "Time_Start", "autoWidth": true }
            , { "data": "Created_Date", "name": "Created_Date", "autoWidth": true }
            , { "data": "TotalRecords", "name": "TotalRecords", "autoWidth": true }
        ]
    });
}