$(function () {
    $(".table").fixedHeader();
});

; (function ($) {
    $.fn.fixedHeader = function (options) {
        var defaultOptions = {
        };
        if (options) $.extend(defaultOptions, options);

        return this.each(function (i) {
            var cur = $(this); // 当前table
            var parent = cur.parent(); // 父级容器
            var panelHeading = parent.siblings(".panel-heading");

            // Panel标题的高度
            var panelHeadingHeight = panelHeading.height()
                            + parseFloat(panelHeading.css("padding-top"))
                            + parseFloat(panelHeading.css("padding-bottom"))
                            + parseFloat(panelHeading.css("border-top"))
                            + parseFloat(panelHeading.css("border-bottom"))
                            + 1;

            // 表头容器
            var fixedHeaderTable = cur.clone()
                                        .addClass("table-fixed-header")
                                        .attr({ id: "table-fixed-header-" + i })
                                        .width(cur.width() + 2)
                                        .css({ top: panelHeadingHeight })
                                        .find("tbody").detach()
                                        .end().prependTo(parent);

            var scrollMin = (cur.height()) * -1;

            parent.scroll(function () {
                var scrollTop = cur.position().top - panelHeadingHeight;

                if (scrollMin < scrollTop && scrollTop < 0)
                    fixedHeaderTable.show();
                else
                    fixedHeaderTable.hide();
            });

        });
    };
})(jQuery);