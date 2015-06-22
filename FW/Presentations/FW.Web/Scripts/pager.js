$(function () {
    $(".pagination").pager({
        pager: {
            hasPrevious: false,
            hasNext: false,
            totalPages: 10
        }
    });
});

; (function ($) {
    $.fn.pager = function (options) {
        var defaultOptions = {
            pager: {
                hasPrevious: false,
                hasNext: false,
                totalPages: 20
            }
        };
        if (options) $.extend(defaultOptions, options);

        return this.each(function (i) {
            var cur = $(this);
            alert(options.pager);
            cur.html(template("tmpl-pager"), {
                pager: options.pager
            });
        });
    };
})(jQuery);