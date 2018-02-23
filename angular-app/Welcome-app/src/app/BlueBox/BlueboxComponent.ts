import { Component } from "@angular/core";


@Component({
    selector : 'ht-bluebox',
    templateUrl : 'BlueboxComponent.html'
})
export class BlueboxComponent{
    len:number;
    size = [];
    attempts:number;
    ranNum : number

    constructor(){
        this.len = 100;
        this.attempts = 3;
        this.ranNum = Math.floor(Math.random() * 100);
        console.log(this.ranNum);
        for(let i=1; i<=100; i++){
            this.size.push(i);
        }
    }

    ChangeStatus($event){
        console.log("Inside Change Status...");
        console.log($event);
    }
}