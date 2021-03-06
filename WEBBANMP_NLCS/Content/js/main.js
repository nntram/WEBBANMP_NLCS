/*price range*/

if ($.fn.slider) {
    $('#sl2').slider();
}

var RGBChange = function () {
    $('#RGB').css('background', 'rgb(' + r.getValue() + ',' + g.getValue() + ',' + b.getValue() + ')')
};

/*scroll to top*/

$(document).ready(function () {
    $(function () {
        $.scrollUp({
            scrollName: 'scrollUp', // Element ID
            scrollDistance: 300, // Distance from top/bottom before showing element (px)
            scrollFrom: 'top', // 'top' or 'bottom'
            scrollSpeed: 300, // Speed back to top (ms)
            easingType: 'linear', // Scroll to top easing (see http://easings.net/)
            animation: 'fade', // Fade, slide, none
            animationSpeed: 200, // Animation in speed (ms)
            scrollTrigger: false, // Set a custom triggering element. Can be an HTML string or jQuery object
            //scrollTarget: false, // Set a custom target element for scrolling to the top
            scrollText: '<i class="fa fa-angle-up"></i>', // Text for element, can contain HTML
            scrollTitle: false, // Set a custom <a> title if required.
            scrollImg: false, // Set true to use image
            activeOverlay: false, // Set CSS color to display scrollUp active point, e.g '#00FFFF'
            zIndex: 2147483647 // Z-Index for the overlay
        });
    });
});



jQuery(function ($) {
    $(".SoLuong").bind('keyup mouseup', function () {
        var mydom = $(".SoLuong");
        var maxQua = parseInt(mydom.attr("max"));
        var sl = (mydom.val());
        if (sl > maxQua) {
            mydom.val(maxQua);
            alert("Sản phẩm này trong kho hiện còn " + maxQua + " sản phẩm.")
        }
        else if (sl > Math.floor(sl)) {
            mydom.val(Math.floor(sl));
        }
        return;

    });
});


jQuery(function ($) {
    $(".SoLuong").focusout(
        function () {
            var mydom = $(".SoLuong");
            if (mydom.val().trim() == "" || parseInt(mydom.val()) < 1) { mydom.val('1'); }
            return;
        });


});

$(function () {
    if ($('#success').val()) {
        displayMessage($('#success').val(), 'success');
    }
    if ($('#info').val()) {
        displayMessage($('#info').val(), 'info');
    }
    if ($('#warning').val()) {
        displayMessage($('#warning').val(), 'warning');
    }
    if ($('#error').val()) {
        displayMessage($('#error').val(), 'error');
    }
});
var displayMessage = function (message, msgType) {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "positionClass": "toast-top-right",
        "onClick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "3000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };
    toastr[msgType](message);
};

