var Game = function () {
    this.data = new Array();
    this.direction = {left: 0, up: 1, right: 2, down: 3};

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

        return this.data;
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
    };

    this.Move = function (direction) {
        var isMoved = false;

        switch (direction) {
            case this.direction.left:
                for (var i = 0, iMax = this.data.length; i < iMax; i++) {
                    for (var j = 0, jMax = this.data[i].length; j < jMax; j++) {
                        for (var k = j + 1; k < jMax; k++) {
                            if (this.data[i][k] > 0) {
                                if (this.data[i][j] == 0) {
                                    this.data[i][j] = this.data[i][k];
                                    this.data[i][k] = 0;
                                    j--;
                                    isMoved = true;
                                }
                                else if (this.data[i][j] == this.data[i][k]) {
                                    this.data[i][j] *= 2;
                                    this.data[i][k] = 0;
                                    isMoved = true;
                                }
                                break;
                            }
                        }
                    }
                }
                break;
            case this.direction.right:
                for (var i = 0, iMax = this.data.length; i < iMax; i++) {
                    for (var j = this.data[i].length - 1, jMin = 0; j >= jMin; j--) {
                        for (var k = j - 1; k >= jMin; k--) {
                            if (this.data[i][k] > 0) {
                                if (this.data[i][j] == 0) {
                                    this.data[i][j] = this.data[i][k];
                                    this.data[i][k] = 0;
                                    j++;
                                    isMoved = true;
                                }
                                else if (this.data[i][j] == this.data[i][k]) {
                                    this.data[i][j] *= 2;
                                    this.data[i][k] = 0;
                                    isMoved = true;
                                }
                                break;
                            }
                        }
                    }
                }
                break;
            case this.direction.top:
                break;
            case this.direction.down:
                break;
            default :
                break;
        }
    };

    this.Test = function () {
        var row = new Array();
        row.push(0);
        row.push(0);
        row.push(0);
        row.push(4);

        this.data = new Array();
        this.data.push(row);

        this.Move(this.direction.right);

        return this.data;
    }
};