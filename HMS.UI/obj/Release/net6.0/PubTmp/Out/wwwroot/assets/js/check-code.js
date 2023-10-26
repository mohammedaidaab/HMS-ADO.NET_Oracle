var container = document.getElementsByClassName("inputs-contianer")[0];
//console.log("test co", container);

container.onkeyup = function (e) {
    var target = e.srcElement || e.target;
    console.log("test target", target);
    if (target.id === 'Input_FourthNumber' && document.getElementById("Input_FourthNumber").value !== '') {
        $("#verifyCodeForm").submit();
    }

    var maxLength = 1;
    var trargetLength = target.value.length;
    //console.log("test trargetLength ", trargetLength);

    if (trargetLength >= maxLength) {
        //console.log("if test trargetLength", trargetLength);

        var next = target;
        while (next = next.nextElementSibling) {
            if (next == null)
                break;

            if (next.tagName.toLowerCase() === "input") {
                console.log("if test next", trargetLength);

                next.focus();
                break;
            }
        }
    } else if (trargetLength === 0) {
        var previous = target;
        while (previous = previous.previousElementSibling) {
            if (previous == null)
                break;
            if (previous.tagName.toLowerCase() === "input") {
                previous.focus();
                break;
            }
        }
    }
}