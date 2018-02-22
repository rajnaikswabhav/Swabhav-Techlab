class Point {
    private _x: number;
    private _y: number;

    public set X(val: number) {
        if (val > 0) { this._x = val; }
    }

    public get X() {
        return this._x;
    }

    public set Y(val: number) {
        if (val > 0) { this._y = val; }
    }

    public get Y() { return this._y; }
}

let p = new Point();
p.X = 10;
p.Y = 5;
console.log(p.X);
console.log(p.Y);