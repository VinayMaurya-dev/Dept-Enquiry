$("input[type='checkbox'][name='statementcheck']").change(function () {
    if ($("input[type='checkbox'][name='statementcheck']:checked").length > 0) {
        $("#statementbutton").prop("disabled", false);
        $(".blink").css({
            animation: "blink-animation 1s infinite",
            color: "red"
        });
    } else {
        $("#statementbutton").prop("disabled", true);
        $(".blink").css({
            animation: "none",
            color: "darkred"
        });
    }
});
$("input[type='checkbox'][name='FinalStatus']").change(function () { 
    if ($("input[type='checkbox'][name='FinalStatus']:checked").length > 0) {
        $("#addFinalStatus").prop("disabled", false);
        $(".blink").css({
            animation: "blink-animation 1s infinite",
            color: "red"
        });
    } else {
        $("#addFinalStatus").prop("disabled", true);
        $(".blink").css({
            animation: "none",
            color: "darkred"
        });
    }
});

