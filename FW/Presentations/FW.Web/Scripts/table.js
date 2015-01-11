$(function () {
    var title = [
        { text: "No.", prop: "index" },
        { text: "Name", prop: "name" },
        { text: "Age", prop: "age" }
    ];
    var json = [
        { index: 1, name: "Henry1", age: 28 },
        { index: 2, name: "Hello world1", age: 200 }
    ];

    var head = [],
        prop = [],
        data = [];

    for (var i = 0, iMax = title.length; i < iMax; i++) {
        head.push(title[i].text);
        prop.push(title[i].prop);
    }

    for (var i = 0, iMax = json.length; i < iMax; i++) {
        var row = [];
        for (var j = 0, jMax = prop.length; j < jMax; j++) {
            row.push(json[i][prop[j]]);
        }
        data.push(row);
    }

    $("#tb").html(template("tb-tmpl", { head: head, data: data }));
});