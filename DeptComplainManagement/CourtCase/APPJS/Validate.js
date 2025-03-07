
$(document).ready(function () {
    $('#Old_Password').change(function () {
        OldPasswordvalidate();

    });
    $('#btnpassChange').click(function () {
        SaveChangePassword();
    });
    $('#password').change(function () {
        CureentPasswordvalidate();

    });
    $('#confirm_password').change(function () {
        ConfrmPasswordvalidate();

    });
 
});
function CheckOldPassword() {
    var OldPassword = $('#Old_Password').val();
    $.ajax({
        type: 'POST',
        url: '/Law/CheckPassword',
        data: { OldPassword: OldPassword },
        success: function (Data) {
            debugger
            if (Data !== "Error") {
                $('#Old_Password').addClass("form-control-input is-valid");
                $('#Old_Password').attr("aria-invalid", "false");
            }
            else {
                $('#Old_Password').addClass("form-control-input is-invalid");
                $('#Old_Password').attr("aria-invalid", "true");
                toastr.options = {
                    "closeButton": true,
                    "timeout": 1000,
                    "progressBar": true,
                    "positionClass": "toast-top-center"
                };
                toastr.error("Your Password Does not Match !");
            }
        },
        error: function (errorData) {
            toastr.options = {
                "closeButton": true,
                "timeout": 1000,
                "progressBar": true,
                "positionClass": "toast-top-center"
            };
            toastr.warning("Something Went  Wrong !");
        }
    });
}
function SaveChangePassword() {
    debugger;
    var OldPass = $('#Old_Password').val();
    var Pass = $('#password').val();
    var CnfrmPass = $('#confirm_password').val();
    $('#Old_Password').removeClass("is-valid is-invalid");
    $('#Old_Password').attr("aria-invalid", "false");
    $('#erroeMsg').text("");
    if (OldPass === "") {
        $('#Old_Password').addClass("is-invalid");
        $('#Old_Password').attr("aria-invalid", "true");
        $('#erroeMsg').text("Please enter your Old password");
        $('#Old_Password').focus();
        return false;
    } else {
        $('#Old_Password').addClass("is-valid");
    }
    if (Pass === "") {
        $('#password').addClass("is-invalid");
        toastr.error("Please enter your New password");
        return false;
    } else {
        $('#password').removeClass("is-invalid");
    }

    if (CnfrmPass === "") {
        $('#confirm_password').addClass("is-invalid");
        toastr.error("Please confirm your New password");
        return false;
    } else {
        $('#confirm_password').removeClass("is-invalid");
    }

    if (Pass !== CnfrmPass) {
        $('#password, #confirm_password').addClass("is-invalid");
        toastr.error("Passwords do not match");
        return false;
    } else {
        $('#password, #confirm_password').removeClass("is-invalid");
    }
    var data = {
        OldPassword: OldPass,
        NewPassword: Pass,
        ConfirmPassword: CnfrmPass
    };
    var Fdata = JSON.stringify(data);
    $.ajax({
        type: 'POST',
        url: '/Law/ChangePassword',
        contentType: 'application/json',
        data: Fdata,
        success: function (response) {
            debugger;
            if (response === "Success") {
                setTimeout(
                    toastr.success("Password changed successfully"), 5000);
                location.reload(true);
            } else {
                toastr.error("Your Password Does not Match !");
                $('#Old_Password').addClass("is-invalid").focus();
            }
        },
        error: function (error) {
            toastr.error("Something went wrong. Please try again later.");
        }
    });

   // return false; 
}

function OldPasswordvalidate() {
    debugger;
    //var p = document.getElementById("Old_Password").value;
    //var errors = [];

    //if (p.length < 8) {

    //    errors.push("Your password must be at least 8 characters");
    //}
    //if (p.search(/[a-z]/i) < 0) {
    //    errors.push("Your password must contain at least one letter.");
    //}
    //if (p.search(/[0-9]/) < 0) {
    //    errors.push("Your password must contain at least one digit.");
    //}
    //if (p.search(/[^a-zA-Z0-9]/) < 0) {
    //    errors.push("Your password must contain at least one special character.");
    //}

    //if (errors.length > 0) {
    //    toastr.options = {
    //        "closeButton": true,
    //        "timeout": 1000,
    //        "progressBar": true,
    //        "positionClass": "toast-top-center"
    //    };
    //    toastr.error(errors.join("<br>"));
    //    $('#Old_Password').val("");
    //    $('#Old_Password').focus();
    //    return false;
    //} else {
        CheckOldPassword();
    //}
}


function CureentPasswordvalidate() {
    //  debugger;
    var p = document.getElementById("ContentPlaceHolder1_txtoldPassword").value,
        errors = [];
    if (p.length < 8) {
        errors.push("Your password must be at least 8 characters");
    }
    if (p.search(/[a-z]/i) < 0) {
        errors.push("Your password must contain at least one letter.");
    }
    if (p.search(/[0-9]/) < 0) {
        errors.push("Your password must contain at least one digit.");
    }
    if (p.search(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*(\W|_)).{5,}$/) < 0) {
        errors.push("Your password must contain at least one special Character.");
    }
    if (errors.length > 0) {
        alert(errors.join("\n"));
        $('#ContentPlaceHolder1_txtoldPassword').val("");
        $('#ContentPlaceHolder1_txtoldPassword').focus();
        return false;

    }
    else {
        return true;
    }
}
function ConfrmPasswordvalidate() {
    //  debugger;
    var p = document.getElementById("ContentPlaceHolder1_txtoldPassword").value,
        errors = [];
    if (p.length < 8) {
        errors.push("Your password must be at least 8 characters");
    }
    if (p.search(/[a-z]/i) < 0) {
        errors.push("Your password must contain at least one letter.");
    }
    if (p.search(/[0-9]/) < 0) {
        errors.push("Your password must contain at least one digit.");
    }
    if (p.search(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*(\W|_)).{5,}$/) < 0) {
        errors.push("Your password must contain at least one special Character.");
    }
    if (errors.length > 0) {
        alert(errors.join("\n"));
        $('#ContentPlaceHolder1_txtoldPassword').val("");
        $('#ContentPlaceHolder1_txtoldPassword').focus();
        return false;

    }
    else {
        return true;
    }
}

function loadJSFunction() {
    Swal.fire({
        title: "Susccess!",
        text: "Your password has been changed successfully!",
        icon: "success",
        showConfirmButton: true
    }).then((result) => {
        if (result.isConfirmed) {
            url = "/Home/UserLogin";
            window.location.href = url;
        }
    });
}

$(document).ready(function () {
    $("#CaseRelatedTo").change(function () {
        var CaseRelatedTo = $("#CaseRelatedTo").val();
        //if (CaseRelatedTo === "Employee") {
            bindSubjectMatter('SubjectMatter', -1, CaseRelatedTo);
       // }
    });
});
function bindSubjectMatter(id, defaultSelectionValues, CaseRelatedTo) {
    debugger
    defaultSelectionValues = typeof defaultSelectionValues !== 'undefined' ? defaultSelectionValues : '';
    if (defaultSelectionValues === -1 || defaultSelectionValues === 0 || defaultSelectionValues === '-1' || defaultSelectionValues === '') {
        defaultSelectionValues = '-1';
    }
    var obj = new Object();
    obj.Action = "bindSubjectMatter";
    obj.CaseRelatedTo = CaseRelatedTo;

    $.ajax({
        type: 'POST',
        url: '/DropdownBinder/fillDropdown',
        data: { Action: obj.Action, CaseRelatedTo: obj.CaseRelatedTo },
        success: function (Data) {
            var ddl = $('.' + id);
            ddl.empty();
            ddl.append($('<option/>', {
                value: "-1",
                text: "Choose option "
            }));

            $.each(Data, function (index, rowData) {
                ddl.append($('<option/>', {
                    value: rowData.id,
                    text: rowData.value
                }));
            });
            ddl.val(defaultSelectionValues);
        },
        error: function (errorData) {
            console.log(errorData);
        }
    });

}