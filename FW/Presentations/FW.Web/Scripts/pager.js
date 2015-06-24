$(function () {
    var pageChanged = function (pageIndex) {
        alert("Callback " + pageIndex);
        $(".pagination").pager({
            totalPages: pageIndex,
            onPageChanged: pageChanged
        });
    };

    $(".pagination").pager({
        hasPrevious: false,
        hasNext: false,
        totalPages: 1,
        onPageChanged: pageChanged
    });
});

; (function ($) {
    $.fn.pager = function (options) {
        var defaultOptions = {
            hasPrevious: false,
            hasNext: false,
            totalPages: 20,
            onPageChanged: null
        };
        if (options) $.extend(defaultOptions, options);

        return this.each(function (i) {
            var cur = $(this);
            cur.html(template("tmpl-pager", options));
            if (options.onPageChanged) {
                cur.find("a").on("click", function () {
                    options.onPageChanged(parseInt($(this).text()));

                    return false;
                });
            }
        });
    };
})(jQuery);