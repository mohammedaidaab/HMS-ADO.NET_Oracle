// Add customer //
function AddCustomer() {
    var data = $("#customerForm").serialize();
    //console.log(data);
    //alert(data);
    $.ajax({
        type: 'POST',
        url: '/Customers/AddCustomer',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it
        data: data,
        success: function (result) {
            alert('تم حفظ البيانات بنجاح');
            $("#kt_modal_add_vacStatus").modal('hide');
            window.location.reload();

        },
        error: function () {
            alert(data);
            alert('حدث خطأ أثناء الإضافة');
        }
    })
}

// Edit Customer //
function EditCustomer() {
    var data = $("#editCustomerForm").serialize();
    //console.log(data);
    //alert(data);
    $.ajax({
        type: 'POST',
        url: '/Customers/EditCustomer',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it
        data: data,
        success: function (result) {
            alert('تم تعديل البيانات بنجاح');
            $("#kt_modal_add_vacStatus").modal('hide');
            window.location.reload();

        },
        error: function () {
            alert('حدث خطأ أثناء التعديل');
        }
    })
}
function DeleteCustomer(id) {

    //alert(id);
    
    $.ajax({
        type: 'POST',
        url: '/Customers/DeleteCustomer?id=' + id,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it
        //data: data,
        success: function (result) {
            alert('تم حذف البيانات بنجاح');
            $("#kt_modal_add_vacStatus").modal('hide');
            window.location.reload();

        },
        error: function () {
            alert('حدث خطأ أثناء التعديل');
        }
    })
}



