; (function ($) {
    $.fn.table = function (options) {
        var defaults = {
            customTemplate: false,
            template: "tb-tmpl",
            url: "",
            columns: [],
            condition: null
        };

        var current = $(this),
            opts = $.extend(defaults, options);

        if (opts.url &&
            opts.template &&
            opts.columns &&
            0 < opts.columns.length) {

            $.ajax({
                url: opts.url,
                data: opts.condition,
                success: function (result) {

                    var data = result.Data;

                    if (!opts.customTemplate) {
                        data = [];

                        for (var i = 0, iMax = result.Data.length; i < iMax; i++) {
                            var row = [];
                            for (var j = 0, jMax = opts.columns.length; j < jMax; j++) {
                                row.push(result.Data[i][opts.columns[j].field]);
                            }
                            data.push(row);
                        }
                    }

                    current.html(template(opts.template, {
                        columns: opts.columns,
                        data: data
                    }));
                }
            });
        }
    }
})(jQuery);