$(document).ready(function () {
    InitHijriDates();
});

function InitHijriDates () {
    $(".hijri-date-input").hijriDatePicker({
        showTodayButton: true,
        hijri: true,
        showClose: true,
        viewMode: 'days',
        minDate: '2021-01-01',
        locale: 'ar-SA',
        maxDate: '2023-01-01',
        showClear: true,
        icons: {
            previous: '<',
            next: '>',
            today: 'تاريخ اليوم',
            clear: 'حذف',
            close: 'اغلاق'
        },
        hijriText: "عرض التاريخ الهجري",
        gregorianText: "عرض التاريخ الميلادي"
    });

    $(".hijri-date-input").on('dp.change', function (arg) {
        if (!arg.date) {
            return;
        };

        let date = arg.date;

        //var elementId = arg.target.getAttribute('element-id');

        //arg.target.value = date.format("iD - iMMMM - iYYYY");

        //document.getElementById(elementId).value = date.format("iYYYY-iM-iD");
    });
}
