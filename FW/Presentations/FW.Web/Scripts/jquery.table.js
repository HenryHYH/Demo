; (function ($) {
    var Table = function (element, opt) {
        this.current = element;
        this.defaults = {
            customTemplate: false,
            template: "tb-tmpl",
            pagerTemplate: "pager-tmpl",
            url: "",
            columns: [],
            condition: null
        };
        this.options = $.extend({}, this.defaults, opt);
    }

    Table.prototype = {
        Load: function () {
            var options = this.options;
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
                                lastPageIndex = totalPages;

                            if (pageIndex >= 5) {
                                firstPageIndex = pageIndex - 4;
                            }
                            if (pageIndex <= lastPageIndex - 4) {
                                lastPageIndex = pageIndex + 4;
                            }

                            pager.Pages = [];
                            for (var i = firstPageIndex; i <= lastPageIndex; i++) {
                                pager.Pages.push(i);
                            }

                            var html = template(options.template, {
                                columns: options.columns,
                                data: data,
                                pager: pager
                            });
                            $(current).html(html);
                        }
                    });
                }
            });
        }
    };

    $.fn.table = function (options) {
        var plugin = new Table(this, options);
        return plugin.Load();
    }
})(jQuery);