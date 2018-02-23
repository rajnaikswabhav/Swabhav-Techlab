import { Component } from "@angular/core";


@Component({
    selector: 'ht-bluebox',
    templateUrl: 'BlueboxComponent.html'
})
export class BlueboxComponent {
    len: number;
    size = [];
    attempts: number;
    ranNum: number

    constructor() {
        this.len = 100;
        this.attempts = 0;
        this.ranNum = Math.floor(Math.random() * 100);
        console.log(this.ranNum);
        for (let i = 1; i <= this.len; i++) {
            this.size.push(i);
        }
    }

    ChangeStatus($event) {
        console.log("Inside Change Status...");
        console.log($event);
        let guessNum = $event.target.id;

        if (guessNum > this.ranNum) {
            console.log("less than");
            this.attempts++;
            console.log("Attempts : " + this.attempts);
            $event.target.style.backgroundColor = "Red";
        }
        else if (guessNum == this.ranNum) {
            $event.target.style.backgroundColor = "blue";
            setTimeout(() => {
                alert("You Won....");
                location.reload();
            }, 1000);
        }
        else if (guessNum < this.ranNum) {
            console.log("less than");
            this.attempts++;
            console.log("Attempts : " + this.attempts);
            $event.target.style.backgroundColor = "green";
        }

        if (this.attempts == 3) {
            alert("You are out of attempts..\nReal Number is "+this.ranNum);
            setTimeout(() => {
                location.reload();
            }, 1000);
        }
    }
}