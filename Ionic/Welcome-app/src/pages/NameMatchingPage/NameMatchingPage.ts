import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';

@Component({
    selector:'ht-namematch',
    templateUrl:'NameMatchingPage.html'
})
export class NameMatch {

    name1 : string;
    name2 : string;
    result : number;
    constructor(private navCtr : NavController){
    }

    Calculate(newName){
        console.log(newName);
        this.name2 = newName;
        let sum : number = 0;

        for(let i = 0; i<this.name1.length; i++){
            sum = sum + this.name1.toUpperCase().charCodeAt(i);
        }
        console.log(sum);
        for(let i = 0; i<this.name2.length; i++){
            sum = sum + this.name2.toUpperCase().charCodeAt(i);
        }
        console.log(sum);
         this.result = sum%101;
    }

}