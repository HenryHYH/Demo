$(function () {
    var columns = eval("(" + $("#tb-columns").val() + ")");
    var json = [
        { index: 1, name: 'Henry1', age: 28 },
        { index: 2, name: 'Hello world1', age: 200 }
    ];

    var head = [],
        field = [],
        data = [];

    for (var i = 0, iMax = columns.length; i < iMax; i++) {
        head.push(columns[i].title);
        field.push(columns[i].field);
    }

    for (var i = 0, iMax = json.length; i < iMax; i++) {
        var row = [];
        for (var j = 0, jMax = field.length; j < jMax; j++) {
            row.push(json[i][field[j]]);
        }
        data.push(row);
    }

    $('#tb').html(template('tb-tmpl', { head: head, data: data }));
});