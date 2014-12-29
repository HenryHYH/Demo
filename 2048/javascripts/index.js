$(function () {
    var game = new Game();
    game.Start();
    Show(game.data);

    $(document).delegate('body', 'keyup', function (e) {
        switch (e.keyCode) {
            case 37: //left
                game.Move(false, false, true, false);
                Show(game.data);
                break;
            case 38: //up
                game.Move(true, false, false, false);
                Show(game.data);
                break;
            case 39: //right
                game.Move(false, false, false, true);
                Show(game.data);
                break;
            case 40: //down
                game.Move(false, true, false, false);
                Show(game.data);
                break;
            default :
                break;
        }
    });
});

function Show(data) {
    var html = '';

    for (var i = 0, iMax = data.length; i < iMax; i++) {
        for (var j = 0, jMax = data[i].length; j < jMax; j++) {
            var val = data[i][j];
            html += '<div class="cell cell-' + data[i][j] + '">' + (val == 0 ? '' : val) + '</div>';
        }
    }

    $(".table").html(html);
}