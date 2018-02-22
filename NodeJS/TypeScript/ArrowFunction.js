var printMsg = function (msg) { return console.log(msg); };
printMsg("Hello Brijesh");
printMsg("Hello Akash");
function getData(fn) {
    var randomNumber = Math.random() * 10;
    if (randomNumber > 5) {
        fn({ result: "number: " + randomNumber });
    }
    else {
        fn({ resutl: "Error " + randomNumber });
    }
}
getData(function (r) { return console.log(r.result); });
getData(function (r) { return console.log(r); });
setTimeout(function () { return console.log("After 3 seconds.."); }, 3000);
setTimeout(function () {
    getData(function (r) {
        console.log("Inside Timeout"),
            console.log(r);
    });
}, 5000);
