var Game = function () {
    this.data = new Array();

    this.Start = function () {
        var pos = 0;
        for (var i = 0, iMax = 4; i < iMax; i++) {
            var row = new Array();
            for (var j = 0, jMax = 4; j < jMax; j++) {
                row.push(0);
            }
            this.data.push(row);
        }
        this.NextValue();
        this.NextValue();
    };

    this.NextValue = function () {
        var i = this.Random(0, 3);
        var j = this.Random(0, 3);

        if (this.data[i][j] == 0) {
            this.data[i][j] = this.Random(1, 5) == 1 ? 4 : 2;
        }
        else {
            return this.NextValue();
        }
    };

    this.Random = function (min, max) {
        if (!min)
            min = 0;
        if (!max)
            max = 1;
        return Math.round(Math.random() * (max - min) + min);
    }

    this.Move = function (isUp, isDown, isLeft, isRight) {
        
    };
};