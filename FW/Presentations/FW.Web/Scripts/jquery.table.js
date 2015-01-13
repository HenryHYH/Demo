﻿; (function ($) {
    var Table = function (element, opt) {
        this.current = element;
        this.defaults = {
            customTemplate: false,
            template: "tb-tmpl",
            url: "",
            columns: [],
            condition: null
        };
        this.options = $.extend({}, this.defaults, opt);
    }

    Table.prototype = {
        Load: function (newPageIndex) {

            var options = this.options;
            if (newPageIndex)
                options.condition.pageIndex = newPageIndex;

            return this.current.each(function () {

                if (options.url && options.template && options.columns && 0 < options.columns.length) {

                    var current = this;

                    $.ajax({
                        url: options.url,
                        data: options.condition,
                        success: function (result) {

                            var data = result.Data,
                                pager = result.Pager;

                            if (!options.customTemplate) {
                                data = [];

                                for (var i = 0, iMax = result.Data.length; i < iMax; i++) {
                                    var row = [];
                                    for (var j = 0, jMax = options.columns.length; j < jMax; j++) {
                                        row.push(result.Data[i][options.columns[j].field]);
                                    }
                                    data.push(row);
                                }
                            }

                            var pageIndex = pager.PageIndex,
                                totalPages = pager.TotalPages,
                                firstPageIndex = 1,
                                lastPageIndex = totalPages,
                                pageCnt = 5;

                            if (totalPages >= 5) {
                                if (pageIndex == 1) {
                                    lastPageIndex = 5;
                                }
                                else if (pageIndex == 2) {
                                    lastPageIndex = 5;
                                }
                                else if (pageIndex < totalPages - 2) {
                                    lastPageIndex = pageIndex + 2;
                                }

                                if (pageIndex == totalPages) {
                                    firstPageIndex = totalPages - 4;
                                }
                                else if (pageIndex == totalPages - 1) {
                                    firstPageIndex = totalPages - 4;
                                }
                                else if (pageIndex > 2) {
                                    firstPageIndex = pageIndex - 2;
                                }
                            }

                            pager.Pages = [];
                            for (var i = firstPageIndex; i <= lastPageIndex; i++) {
                                pager.Pages.push(i);
                            }

                            $(current).html(template(options.template, {
                                columns: options.columns,
                                data: data,
                                pager: pager
                            }));
                        }
                    });

                }
            });
        }
    };

    $.fn.table = function (options) {
        var plugin = new Table(this, options);

        $(this).delegate(".pagination li:not(.disabled) a", "click", function () {

            var pageIndex = parseInt($(this).attr("data-page"));
            plugin.Load(pageIndex);

            return false;

        });

        return plugin.Load();
    }
})(jQuery);