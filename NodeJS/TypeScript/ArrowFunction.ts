let printMsg = (msg) => console.log(msg);

printMsg("Hello Brijesh");
printMsg("Hello Akash");

function getData(fn) {
    let randomNumber = Math.random() * 10;
    if (randomNumber > 5) {
        fn({ result: "number: " + randomNumber });
    }
    else {
        fn({ result: "Error " + randomNumber });
    }
}

getData(r => console.log(r.result));
getData(r => console.log(r));
setTimeout(() => console.log("After 3 seconds.."), 3000);
setTimeout(() => {
    getData(r => {
        console.log("Inside Timeout"),
        console.log(r)
    })
}, 5000);
