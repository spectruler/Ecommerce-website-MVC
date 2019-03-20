$(function () {
    var registerUserCheckBox = $('#AcceptUserAgreement').click(
        onToggleRegisterUserDisabledClick
    );

    function onToggleRegisterUserDisabledClick() {
        $('.register-user-panel button').toggleClass('disabled');
    }

    var registerUserButton = $('.register-user-panel button').click(onRegisterUserClick);

    function onRegisterUserClick() {
        var url = '/Account/RegisterUserAsync';
        var antiforgery = $('[name="__RequestVerificationToken"]').val() ;
        var f_name = $('.register-user-panel .first-name').val();
        var l_name = $('.register-user-panel .last-name').val();
        var email = $('.register-user-panel .email').val();
        var password = $('.register-user-panel .password').val();

        $.post(url, { __RequestVerificationToken: antiforgery, email: email, firstname: f_name, lastname: l_name, password: password, acceptUserAgreement: true },
            function (data) {
                var parsed = $.parseHTML(data);
                var hasErrors = $(parsed).find('[data-valmsg-summary]').text().replace(/\n|\r/g, "").length > 0;
               
                if (hasErrors == true) {
                    $('.register-user-panel').html(data);
                    registerUserCheckBox = $('#AcceptUserAgreement').click(
                        onToggleRegisterUserDisabledClick
                    );  
                    registerUserButton = $('.register-user-panel button').click(onRegisterUserClick);
                    $('.register-user-panel button').removeClass('disabled');

                } else {
                    registerUserCheckBox = $('#AcceptUserAgreement').click(
                        onToggleRegisterUserDisabledClick
                    );
                    registerUserButton = $('.register-user-panel button').click(onRegisterUserClick);
                    alert('elese')
                    location.href = "/Home/Index";
                }
            }).fail(function (xhr, status, error) {
                alert('Post unsuccessfull')
            })
    }
});