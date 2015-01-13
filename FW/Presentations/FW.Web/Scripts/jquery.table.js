; (function ($) {
    var Table = function (element, opt) {
        this.current = element;
        this.defaults = {
            customTemplate: false,
            template: "tb-tmpl",
            url: "",
            columns: [],
            condition: null,
            pager: { pageIndex: 1, pageSize: 20 }
        };
        this.options = $.extend({}, this.defaults, opt);
    }

    Table.prototype = {
        Load: function (newPageIndex, newPageSize) {

            var options = this.options;
            if (newPageIndex)
                options.pager.pageIndex = newPageIndex;
            if (newPageSize)
                options.pager.pageSize = newPageSize;
            var cond = $.extend({}, options.pager, options.condition);

            return this.current.each(function () {

                if (options.url && options.template && options.columns && 0 < options.columns.length) {

                    var current = this;

                    $.ajax({
                        url: options.url,
                        data: $.extend({}, options.pager, options.condition),
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
                                pageCnt = 7;

                            if (totalPages >= pageCnt) {

                                var halfLen = parseInt(pageCnt / 2);

                                if (pageIndex <= halfLen) {
                                    lastPageIndex = pageCnt;
                                }
                                else if (pageIndex < totalPages - halfLen) {
                                    lastPageIndex = pageIndex + halfLen;
                                }

                                if (pageIndex >= totalPages - halfLen) {
                                    firstPageIndex = totalPages - pageCnt + 1;
                                }
                                else if (pageIndex > halfLen) {
                                    firstPageIndex = pageIndex - halfLen;
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
                            })).find(".page-size").val(options.pager.pageSize);
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

        }).delegate(".page-size", "change", function () {

            var pageSize = parseInt($(this).val());
            plugin.Load(1, pageSize);

            return false;
        });

        var table = plugin.Load();
        return table;
    }
})(jQuery);