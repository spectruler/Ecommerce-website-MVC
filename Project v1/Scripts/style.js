(function ($) {
    $("#search-button").on("click", e => {
        if ($("#search-input-container").hasClass("hdn")) {
            console.log('hello')
            e.preventDefault();
            $("#search-input-container").removeClass("hdn");
            return false;
        }
    })
    $(".card-number-cash").on("click", e => {
        $('.card-number').removeAttr('required')
        $('.card-number').attr('disabled',true)
    })
    $(".card-number-credit").on("click", e => {
        $('.card-number').attr('required', true)
        $('.card-number').removeAttr('disabled')
    })

})(jQuery)