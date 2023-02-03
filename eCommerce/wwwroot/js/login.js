$(document).ready(function () {


    var loginFunction = function (event) {
        var btn = event.target;

        $(btn).removeAttr('enabled');
        $(btn).attr('disabled', 'disabled');

        var loginUrl = $('#loginUrl').data('login-url');
        var username = document.getElementById('username').value;
        var password = document.getElementById('password').value;

        var loginModel = { email: username, password: password };
        if (username.length && password.length) {
            loginModel.email = username;
            loginModel.password = password;
            loginModel.areCredentialsInvalid = true;

            if (loginModel) {
                $.ajax({
                    type: 'POST',
                    url: loginUrl,
                    data: {
                        model: loginModel
                    },
                    success: function (response) {
                        if (response && response.flag) {
                            location.reload(true);
                        }
                        else {
                            alert("Error at login");
                        }

                        $(btn).removeAttr('disabled');
                        $(btn).attr('enabled', 'enabled');
                    },
                    error: function (error) {
                    }
                });
            }
        }
        else {
            // adaugare span pentru a spune sa introduca unde este liber
            return;
        }
        
    };


    //main
    $('.loginBtn').on('click', loginFunction)
});