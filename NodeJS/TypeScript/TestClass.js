class Point {
    set X(val) {
        if (val > 0) {
            this._x = val;
        }
    }
    get X() {
        return this._x;
    }
    set Y(val) {
        if (val > 0) {
            this._y = val;
        }
    }
    get Y() { return this._y; }
}
let p = new Point();
p.X = 10;
p.Y = 5;
console.log(p.X);
console.log(p.Y);
